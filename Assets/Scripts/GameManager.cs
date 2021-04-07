using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{

    public GameObject player1;
    public GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        if (NetworkPlayerManager.localPlayerInstance == null)
        {
            //instantiate the correct player based on the team
            int avatarId = (int)PhotonNetwork.LocalPlayer.CustomProperties["id"];
            Debug.Log($"Team number {avatarId} is being instantiated");
            //instantiate the blue player if team is 0 and red if it is not
            if (avatarId == 0)
            {
                //get a spawn for the correct team
                PhotonNetwork.Instantiate(player1.name, new Vector3(0, 5, 0), Quaternion.identity);
            }
            else
            {
                //now for the red team
                PhotonNetwork.Instantiate(player2.name, new Vector3(0, 5, 0), Quaternion.identity);
            }

        }
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
