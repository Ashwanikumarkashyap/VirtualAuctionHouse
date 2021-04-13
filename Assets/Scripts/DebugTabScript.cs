using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugTabScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void auctionTabClick() {
        Debug.Log("Button Clicked");
        Image auctionTab = GameObject.Find("AuctionTab").GetComponent<Image>();
        auctionTab.color = new Color32(255, 0, 0, 255);    
            }
    public void onWatchListtabClick()
    {
        Debug.Log("Button Clicked");
        Image auctionTab = GameObject.Find("AuctionTab").GetComponent<Image>();
        auctionTab.color = new Color32(0, 255, 255, 255);
    }
}
