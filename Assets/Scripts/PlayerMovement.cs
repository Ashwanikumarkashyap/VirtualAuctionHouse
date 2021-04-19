using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviourPunCallbacks, IPunObservable
{
    public float speed = 20f;

    public Transform avatar;
    private float gravity = 10f;

    //now the camera so we can move it up and down
    public Transform cameraTransform;

    public GameObject dashboard;
    public int[] currentBids;
    public string[] currentBidders;

    //the charachtercompononet for moving us
    CharacterController cc;

    Animator walkingAnimator;
    AudioSource boathAudioSource;

    public GameObject playerCameraWrapper;
    public GameObject playerCamera;

    AnimateItems animateItemsScript;


    private void Start()
    {

        animateItemsScript = GetComponent<AnimateItems>();
        walkingAnimator = GetComponentInChildren<Animator>();

        boathAudioSource = GameObject.Find("Boat").GetComponent<AudioSource>();
        boathAudioSource.Stop();
        cc = GetComponent<CharacterController>();
        if (!photonView.IsMine)
        {
            GetComponentInChildren<Camera>().enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
        }
        UpdateAuctionUi();
    }

    // Update is called once per frame
    void Update()
    {
        Camera c = gameObject.GetComponentInChildren<Camera>();
        if (gameObject.GetComponentInChildren<Camera>() != null)
        {
            if (photonView.IsMine)
            {
                var ray = playerCamera.transform;
                RaycastHit hit;

                if (Physics.Raycast(ray.position, ray.forward, out hit) && (Input.GetButtonUp("js5") || Input.GetKeyUp("space") || Input.GetMouseButtonUp(0)))

                {
                    var selection = hit.transform;
                    if (selection.CompareTag("BidAmount"))
                    {
                        string bidClicked = selection.name;
                        string[] bid_id_value = bidClicked.Split('_');
                        int id = int.Parse(bid_id_value[1]) - 1;
                        int value = int.Parse(bid_id_value[2]);
                        currentBids[id] = currentBids[id] + value;

                        GameObject auctionItemInfo = GameObject.Find("AuctionItemInfoPanel_" + (id + 1).ToString());
                        Transform currentBid = auctionItemInfo.transform.GetChild(1);
                        TMPro.TextMeshProUGUI textComponent = currentBid.GetComponent<TMPro.TextMeshProUGUI>();
                        textComponent.SetText("Current Bid: $" + currentBids[id].ToString());

                        string name = (string)PhotonNetwork.LocalPlayer.CustomProperties["name"];

                        TMPro.TextMeshProUGUI currentBidderText = auctionItemInfo.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
                        currentBidders[id] = name;
                        currentBidderText.SetText("Current Bidder: " + currentBidders[id]);
                        //UpdateAuctionUi();
                    }
                    if (selection.CompareTag("AuctionItem"))
                    {
                        string AuctionItemName = selection.name;
                        Animator anime;
                        switch (AuctionItemName)
                        {
                            case "OldBoat":
                                playerCamera.transform.parent = GameObject.Find("BoatCameraWrapper").transform;
                                //GameObject.Find("Boat").transform.localPosition = new Vector3(-0f, -0f, 0f);
                                //playerCamera.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                                boathAudioSource.Play();
                                break;
                            case "VanityDeskWhite":
                                animateItemsScript.animateVanity(selection.Find("drawers").GetComponent<Animator>());
                                break;
                            case "Classic_car":
                                animateItemsScript.animateVanity(selection.GetComponent<Animator>());
                                break;
                            case "Drone_Guard":
                                animateItemsScript.animateDrone(selection.GetComponent<Animator>());
                                break;
                            case "RobotSphere":
                                animateItemsScript.animateRobot(selection.GetComponent<Animator>());
                                break;
                            case "Spartan_Warrior":
                                animateItemsScript.animateSpartan(selection.GetComponent<Animator>());
                                break;
                            default:
                                Debug.Log("Default case");
                                break;
                        }
                    }
                }

                if (Input.GetKeyUp("tab") || Input.GetButtonUp("js3"))
                {
                    if (!dashboard.activeInHierarchy)
                    {
                        dashboard.SetActive(true);
                    }
                    else
                    {
                        dashboard.SetActive(false);
                    }
                }

                if (!dashboard.activeInHierarchy)
                {
                    Move();
                }
            }
        }
        else if (Input.GetKeyUp("space") || Input.GetButtonUp("js5") || Input.GetMouseButtonUp(0) || (Input.GetButtonUp("js0")))
        {
            boathAudioSource.Stop();
            playerCamera.transform.parent = playerCameraWrapper.transform;
        }
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        Vector3 velocity = direction * speed;

        if (velocity.magnitude > 0.01f)
        {
            walkingAnimator.SetBool("Moving", true);
        }
        else
        {
            walkingAnimator.SetBool("Moving", false);
        }


        velocity = Camera.main.transform.TransformDirection(velocity);
        velocity.y -= gravity;
        cc.Move(velocity * Time.deltaTime);

        var CharacterRotation = cameraTransform.transform.rotation;
        CharacterRotation.x = 0;
        CharacterRotation.z = 0;

        avatar.rotation = CharacterRotation;
    }

    public void UpdateAuctionUi()
    {
        GameObject[] auctionItemInfo = GameObject.FindGameObjectsWithTag("AuctionItemInfoPanel");

        for (int i = 0; i < auctionItemInfo.Length; i++)
        {
            int itemId = int.Parse(auctionItemInfo[i].name.Split('_')[1]);
            Transform currentBid = auctionItemInfo[i].transform.GetChild(1);
            
            TMPro.TextMeshProUGUI textComponent = currentBid.GetComponent<TMPro.TextMeshProUGUI>();
            textComponent.SetText("Current Bid: $" + currentBids[itemId-1].ToString());

            TMPro.TextMeshProUGUI currentBiddder = auctionItemInfo[i].transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>();
            currentBiddder.SetText("Current Bidder: " + currentBidders[itemId - 1]);

        }
    }

    public void UpdateClientBids(int[] currentBids)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            PlayerMovement pScript = player.GetComponent<PlayerMovement>();
            pScript.currentBids = currentBids;
        }
    }

    public bool isBidChanged(int[] updatedBids)
    {
        for (int i = 0; i < updatedBids.Length; i++)
        {
            if (updatedBids[i] > currentBids[i])
            {
                return true;
            }
        }

        return false;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentBids);
            stream.SendNext(currentBidders);
        }
        else
        {
            int[] updatedBids = (int[])stream.ReceiveNext();
            string[] updatedBidders = (string[])stream.ReceiveNext();
            if (isBidChanged(updatedBids))
            {
                currentBids = updatedBids;
                currentBidders = updatedBidders;
                UpdateAuctionUi();
                UpdateClientBids(currentBids);
            }

        }
    }
}