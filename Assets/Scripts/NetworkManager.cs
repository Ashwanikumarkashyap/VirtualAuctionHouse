using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    int AvatarId = 0;
    readonly int AvatersCount = 2;
    public string closingTimeStr = "20210413T06:05:00Z";
    public string startTimeStr = "20210413T06:05:00Z";
    public bool closingTime = false;

    void Start()
    {
        if (!PhotonNetwork.IsConnected) 
        { 
            Connect();
        }    
    }

    public void JoinAuctionHouse(int avatar_id)
    {
        //do we already have a team?
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("id"))
        {
            //we already have a team- so switch teams
            PhotonNetwork.LocalPlayer.CustomProperties["id"] = avatar_id;
        }
        else
        {
            //we dont have a team yet- create the custom property and set it
            //0 for blue, 1 for red
            //set the player properties of this client to the team they clicked
            ExitGames.Client.Photon.Hashtable playerProps = new ExitGames.Client.Photon.Hashtable
        {
            { "id", avatar_id }
        };
            //set the property of Team to the value the user wants
            PhotonNetwork.SetPlayerCustomProperties(playerProps);
        }

        //join the random room and launch game- the GameManager will spawn the correct model in based on the property
        PhotonNetwork.JoinRandomRoom();
    }

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void IncAvatarId()
    {
        AvatarId = (AvatarId+1) % AvatersCount;
    }

    public void DecAvatarId()
    {
        AvatarId--;
        if (AvatarId < 0)
        {
            AvatarId = AvatersCount;
        }
    }

    public void Play()
    {
        if (closingTime)
        {
            System.DateTime closingTime = System.DateTime.ParseExact(closingTimeStr, "yyyyMMddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture);
            System.DateTime startTime = System.DateTime.ParseExact(startTimeStr, "yyyyMMddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture);
            if (startTime <= System.DateTime.Now && System.DateTime.Now <= closingTime)
            {
                JoinAuctionHouse(AvatarId);
            }
        } else
        {
            JoinAuctionHouse(AvatarId);
        }
        
        //PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to join the room and failed");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room!");
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
