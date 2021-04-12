using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviourPunCallbacks, IPunObservable
{

    //public int currentBid = 100;

    public float speed = 20f;

    public Transform avatar;
    private float gravity = 10f;

    //now the camera so we can move it up and down
    public Transform cameraTransform;

    public GameObject dashboard;
    public int[] currentBids;
    bool isDrawerOpen = false;
    bool isDroneAwake = false;
    bool isRobotWalking = false;
    bool isSpartanRunning = false;

    //the charachtercompononet for moving us
    CharacterController cc;

    Animator animator;
    AudioSource boathAudioSource;

    public GameObject playerCameraWrapper;
    public GameObject playerCamera;


    private void Start()
    {
        animator = GetComponentInChildren<Animator>();

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

                if (Physics.Raycast(ray.position, ray.forward, out hit) && (Input.GetButtonDown("js5") || Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)))

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
                                //GameObject.Find("Boat").transform.localPosition = new Vector3(-23f, 0f, -90f);
                                //playerCamera.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                                boathAudioSource.Play();
                                break;
                            case "VanityDeskWhite":
                                anime = selection.Find("drawers").GetComponent<Animator>();
                                animateVanity(anime);
                                break;
                            case "Classic_car":
                                anime = selection.GetComponent<Animator>();
                                animateVanity(anime);
                                break;
                            case "Drone_Guard":
                                anime = selection.GetComponent<Animator>();
                                animateDrone(anime);
                                break;
                            case "RobotSphere":
                                anime = selection.GetComponent<Animator>();
                                animateRobot(anime);
                                break;
                            case "Spartan_Warrior":
                                anime = selection.GetComponent<Animator>();
                                animateSpartan(anime);
                                break;
                            default:
                                Debug.Log("Default case");
                                break;
                        }

                        //// backend change
                        //int itemId = int.Parse(selection.name.Split('_')[1]);
                        ////currentBid += 100;
                        //currentBids[itemId] += 100;

                        //// frontend change
                        //Transform itemCurrentBid = selection.Find("CurrentBid");
                        //TMPro.TextMeshPro bidTextObj = itemCurrentBid.GetComponent<TMPro.TextMeshPro>();
                        ////bidTextObj.SetText("Current Bid $" + currentBid.ToString());
                        //bidTextObj.SetText("Current Bid $" + currentBids[itemId].ToString());
                    }
                }

                //if (input.getkeydown("space"))
                //{
                //    currentbid += 100;
                //    gameobject auctitem = gameobject.find("auctioninfo");
                //    tmpro.textmeshpro textobj = auctitem.getcomponent<tmpro.textmeshpro>();
                //    textobj.settext("current bid $" + currentbid.tostring());
                //}


                if (Input.GetKeyDown("tab") || Input.GetButtonDown("js3"))
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
        else if (Input.GetKeyDown("space") || Input.GetButtonDown("js5") || Input.GetMouseButtonDown(0) || (Input.GetButton("js0")))
            {
                boathAudioSource.Stop();
                playerCamera.transform.parent = playerCameraWrapper.transform;
            }
    }

    public void animateVanity(Animator anim) 
    {
        if (!isDrawerOpen)
        {
            anim.SetTrigger("open");
            isDrawerOpen = true;
        } else
        {
            anim.SetTrigger("close");
            isDrawerOpen = false;
        }
    }


    public void animateCar(Animator anim)
    {

        anim.SetTrigger("leftDoor");
        anim.SetTrigger("rightDoor");
    }


    public void animateDrone(Animator anim)
    {
        if (!isDroneAwake)
        {
            anim.SetTrigger("wake");
            isDroneAwake = true;
        } else
        {
            anim.SetTrigger("destroy");
            isDroneAwake = false;
        }

    }

    public void animateRobot(Animator anim)
    {
        if (!isRobotWalking)
        {
            anim.SetTrigger("Walk");
            isRobotWalking = true;
        }
        else
        {
            anim.SetTrigger("Roll");
            isRobotWalking = false;
        }
    }

    public void animateSpartan(Animator anim)
    {

        if (isSpartanRunning) { 
            anim.SetTrigger("run");
            isSpartanRunning = true;
        } else {
            anim.SetTrigger("attack");
            isSpartanRunning = false;
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
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }


        velocity = Camera.main.transform.TransformDirection(velocity);
        velocity.y -= gravity;
        cc.Move(velocity * Time.deltaTime);

        var CharacterRotation = cameraTransform.transform.rotation;
        //cameraTransform.transform.parent.gameObject.transform.eulerAngles = new Vector3(0.0f, CharacterRotation.y, 0f);
        //var CharacterRotation = cameraTransform.transform.localRotation;
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
            Debug.Log("sending" + currentBids[0]);
            stream.SendNext(currentBids);
        }
        else
        {
            Debug.Log("recieving" + currentBids);
            int[] updatedBids = (int[])stream.ReceiveNext();
            if (isBidChanged(updatedBids))
            {
                Debug.Log("Writing for id: " + PhotonNetwork.LocalPlayer.UserId);
                currentBids = updatedBids;
                UpdateAuctionUi();
                UpdateClientBids(currentBids);
            }

        }
    }
}

