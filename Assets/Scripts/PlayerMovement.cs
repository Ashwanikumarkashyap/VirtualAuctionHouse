using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviourPunCallbacks, IPunObservable
{

    //public int currentBid = 100;

    public float speed = 20f;

    public Transform avatar;
    private float gravity = 10f;


    //now the camera so we can move it up and down
    public Transform cameraTransform;
    float pitch = 0f;
    [Range(1f, 90f)]
    public float maxPitch = 85f;
    [Range(-1f, -90f)]
    public float minPitch = -85f;
    [Range(0.5f, 5f)]
    public float mouseSensitivity = 2f;




    public GameObject dashboard;
    public int[] currentBids;
    bool isDrawerOpen = false;
    bool isDroneAwake = false;
    bool isRobotWalking = false;
    bool isSpartanRunning = false;

    //the charachtercompononet for moving us
    CharacterController cc;

    //Animator animator;
    //bool isMoving = false;
    //public bool customMoving = false;

    private void Start()
    {
        //animator = GetComponentInChildren<Animator>();

        cc = GetComponent<CharacterController>();
        if (!photonView.IsMine)
        {
            GetComponentInChildren<Camera>().enabled = false;
            GetComponentInChildren<AudioListener>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && Input.GetKeyDown("space"))
            {
                var selection = hit.transform;

                if (selection.CompareTag("AuctionItem"))
                {
                    string AuctionItemName = selection.name;
                    Animator anime;
                    switch (AuctionItemName)
                    {
                        case "VanityDeskWhite":
                            anime  = selection.Find("drawers").GetComponent<Animator>();
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

                    // backend change
                    int itemId = int.Parse(selection.name.Split('_')[1]);
                    //currentBid += 100;
                    currentBids[itemId] += 100;

                    // frontend change
                    Transform itemCurrentBid = selection.Find("CurrentBid");
                    TMPro.TextMeshPro bidTextObj = itemCurrentBid.GetComponent<TMPro.TextMeshPro>();
                    //bidTextObj.SetText("Current Bid $" + currentBid.ToString());
                    bidTextObj.SetText("Current Bid $" + currentBids[itemId].ToString());
                }
            }

            //if (input.getkeydown("space"))
            //{
            //    currentbid += 100;
            //    gameobject auctitem = gameobject.find("auctioninfo");
            //    tmpro.textmeshpro textobj = auctitem.getcomponent<tmpro.textmeshpro>();
            //    textobj.settext("current bid $" + currentbid.tostring());
            //}


            if (Input.GetKeyDown("tab"))
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

        //if (velocity.magnitude > 0.01f)
        //{
        //    isMoving = true;
        //    //animator.SetBool("Moving", true);
        //} else
        //{
        //    isMoving = false;
        //    //animator.SetBool("Moving", false);
        //}

        //changeWalkingAnimation();

        velocity = Camera.main.transform.TransformDirection(velocity);
        velocity.y -= gravity;
        cc.Move(velocity * Time.deltaTime);

        var CharacterRotation = cameraTransform.transform.rotation;
        CharacterRotation.x = 0;
        CharacterRotation.z = 0;

        avatar.rotation = CharacterRotation;
    }

    //public void changeWalkingAnimation()
    //{
    //    if (isMoving)
    //    {
    //        animator.SetBool("Moving", true);
    //    } else
    //    {
    //        animator.SetBool("Moving", false);
    //    }
    //}


    public void UpdateActuinUI()
    {
        for (int itemId = 0; itemId < currentBids.Length; itemId++)
        {
            Transform auctionItem = GameObject.Find("AuctionItem_" + itemId.ToString()).transform;
            Transform itemCurrentBid = auctionItem.Find("CurrentBid");
            TMPro.TextMeshPro bidTextObj = itemCurrentBid.GetComponent<TMPro.TextMeshPro>();
            bidTextObj.SetText("Current Bid $" + currentBids[itemId].ToString());
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
            //stream.SendNext(currentBid);
            stream.SendNext(currentBids);
            //stream.SendNext(isMoving);
            //stream.SendNext(customMoving);
        }
        else
        {
            //currentBid = (int)stream.ReceiveNext();
            int[] updatedBids = (int[])stream.ReceiveNext();
            if (isBidChanged(updatedBids))
            {
                Debug.Log("Writing for id: " + PhotonNetwork.LocalPlayer.UserId);
                currentBids = updatedBids;
                UpdateActuinUI();
                UpdateClientBids(currentBids);
            }


            //isMoving = (bool)stream.ReceiveNext();
            //customMoving = (bool)stream.ReceiveNext();

            //GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            //foreach (GameObject player in players)
            //{
            //    PlayerMovement pScript = player.GetComponent<PlayerMovement>();
            //    pScript.currentBid = currentBid;
            //}

            //GameObject auctItem = GameObject.Find("CurrentBid");
            //TMPro.TextMeshPro textObj = auctItem.GetComponent<TMPro.TextMeshPro>();
            //textObj.SetText("Current Bid $" + currentBid.ToString());
        }
    }
}

