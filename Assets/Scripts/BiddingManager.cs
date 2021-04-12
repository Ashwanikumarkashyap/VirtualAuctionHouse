using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class BiddingManager : MonoBehaviourPunCallbacks, IPunObservable
{

    public int[] currentBids;

    private void Start()
    {
        UpdateAuctionUi();
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public void increaseBid()
    {
            string bidClicked = EventSystem.current.currentSelectedGameObject.name;
            string[] bid_id_value = bidClicked.Split('_');
            int id = int.Parse(bid_id_value[1]) - 1;
            int value = int.Parse(bid_id_value[2]);
            currentBids[id] = currentBids[id] + value;

            GameObject auctionItemInfo = GameObject.Find("AuctionItemInfoPanel_" + (id + 1).ToString());
            Transform currentBid = auctionItemInfo.transform.GetChild(1);
            TMPro.TextMeshProUGUI textComponent = currentBid.GetComponent<TMPro.TextMeshProUGUI>();
            textComponent.SetText("Current Bid: $" + currentBids[id].ToString());
            //photonView.RPC("SetAll", RpcTarget.All, currentBids);
    }

    public void UpdateAuctionUi()
    {
        GameObject[] auctionItemInfo = GameObject.FindGameObjectsWithTag("AuctionItemInfoPanel");

        for (int i = 0; i < auctionItemInfo.Length; i++)
        {

            Debug.Log("######################Updateing UI: " + PhotonNetwork.LocalPlayer.UserId);
            int itemId = int.Parse(auctionItemInfo[i].name.Split('_')[1]);
            Transform currentBid = auctionItemInfo[i].transform.GetChild(1);
            TMPro.TextMeshProUGUI textComponent = currentBid.GetComponent<TMPro.TextMeshProUGUI>();
            textComponent.SetText("Current Bid: $" + currentBids[itemId-1].ToString());

        }
    }

    public void UpdateClientBids(int[] currentBids)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("BidManager");

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
            Debug.Log("Sending " + currentBids[0]);
            stream.SendNext(currentBids);
        }
        else
        {
            int[] updatedBids = (int[])stream.ReceiveNext();
            Debug.Log("Writing " + updatedBids[0]);
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

