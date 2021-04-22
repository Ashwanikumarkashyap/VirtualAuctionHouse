using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Notifications : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject notificationText;
    public GameObject auctionTimeText;
    void Start()
    {
        notificationText = GameObject.Find("NotificationText");
        TMPro.TMP_Text textObj = notificationText.GetComponent<TMPro.TextMeshProUGUI>();
        textObj.SetText("0 Notifications");
        textObj.fontSizeMax = 16;
    }
    public void setNotifications(int num)
    {
        //notificationText = GameObject.Find("NotificationText");
        TMPro.TMP_Text textObj = notificationText.GetComponent<TMPro.TextMeshProUGUI>();
        textObj.SetText(num.ToString() + " Notifications");
        if (num != 0)
            textObj.fontStyle = TMPro.FontStyles.Bold;
    }

    public void resetNotifications()
    {
        setNotifications(0);
    }

    public void Update()
    {
    }
}
