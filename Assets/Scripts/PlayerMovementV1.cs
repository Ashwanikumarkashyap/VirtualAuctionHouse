using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(CharacterController))]
//public class PlayerMovementV1 : MonoBehaviourPunCallbacks, IPunObservable
public class PlayerMovementV1 : MonoBehaviourPunCallbacks
{

    //public int currentBid = 100;

    //public float speed = 3.5f;

    ////public Transform avatar;
    //private float gravity = 10f;


    ////now the camera so we can move it up and down
    //public Transform cameraTransform;
    //float pitch = 0f;
    //[Range(1f, 90f)]
    //public float maxPitch = 85f;
    //[Range(-1f, -90f)]
    //public float minPitch = -85f;
    //[Range(0.5f, 5f)]
    //public float mouseSensitivity = 2f;

    ////the charachtercompononet for moving us
    //CharacterController cc;

    //public Animator animator;
    //bool isMoving = false;
    //public bool customMoving = false;

    //private void Start()
    //{

    //    cc = GetComponent<CharacterController>();
    //    if (!photonView.IsMine)
    //    {
    //        GetComponentInChildren<Camera>().enabled = false;
    //        GetComponentInChildren<AudioListener>().enabled = false;
    //    }
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (photonView.IsMine)
    //    {
    //        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;

    //        if (Physics.Raycast(ray, out hit) && Input.GetKeyDown("space"))
    //        {
    //            var selection = hit.transform;

    //            if (selection.CompareTag("AuctionItem"))
    //            {
    //                //int itemId = int.Parse(selection.name.Split('_')[0]);
    //                currentBid += 100;
    //                Transform itemCurrentBid = selection.Find("CurrentBid");
    //                TMPro.TextMeshPro bidTextObj = itemCurrentBid.GetComponent<TMPro.TextMeshPro>();
    //                bidTextObj.SetText("Current Bid $" + currentBid.ToString());
    //            }
    //        }
    //        Move();
    //        Look();
    //    }
    //}


    //void Look()
    //{
    //    var CharacterRotation = cameraTransform.transform.rotation;
    //    transform.eulerAngles = new Vector3(0.0f, CharacterRotation.y, 0f);
    //}

    //void Move()
    //{


    //    float horizontal = Input.GetAxis("Horizontal");
    //    float vertical = Input.GetAxis("Vertical");
    //    Vector3 direction = new Vector3(horizontal, 0, vertical);
    //    Vector3 velocity = direction * speed;

    //    if (velocity.magnitude > 0.01f)
    //    {
    //        isMoving = true;
    //        //animator.SetBool("Moving", true);
    //    }
    //    else
    //    {
    //        isMoving = false;
    //        //animator.SetBool("Moving", false);
    //    }

    //    ChangeWalkingAnimation();

    //    velocity = Camera.main.transform.TransformDirection(velocity);
    //    velocity.y -= gravity;
    //    cc.Move(velocity * Time.deltaTime);

    //    var CharacterRotation = cameraTransform.transform.rotation;
    //    CharacterRotation.x = 0;
    //    CharacterRotation.z = 0;

    //    //avatar.rotation = CharacterRotation;
    //}
    //public void ChangeWalkingAnimation()
    //{
    //    if (customMoving)
    //    {
    //        animator.SetBool("Moving", true);
    //    }
    //    else
    //    {
    //        animator.SetBool("Moving", false);
    //    }
    //}

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(currentBid);
    //        stream.SendNext(isMoving);
    //        stream.SendNext(customMoving);
    //    }
    //    else
    //    {
    //        currentBid = (int)stream.ReceiveNext();
    //        isMoving = (bool)stream.ReceiveNext();
    //        customMoving = (bool)stream.ReceiveNext();

    //        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

    //        foreach (GameObject player in players)
    //        {
    //            PlayerMovement pScript = player.GetComponent<PlayerMovement>();
    //            pScript.currentBid = currentBid;
    //        }

    //        GameObject auctItem = GameObject.Find("CurrentBid");
    //        TMPro.TextMeshPro textObj = auctItem.GetComponent<TMPro.TextMeshPro>();
    //        textObj.SetText("Current Bid $" + currentBid.ToString());
    //    }
    //}
}

