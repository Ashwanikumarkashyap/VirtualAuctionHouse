using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Voice.Unity;

public class PushToTalk : MonoBehaviourPun
{
    public Recorder VoiceRecorder;
    private PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        view = photonView;
        VoiceRecorder.TransmitEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("v") || Input.GetButtonDown("js2"))
        {
            if (view.IsMine)
            {
                VoiceRecorder.TransmitEnabled = true;
            }
        }
        else if (Input.GetKeyUp("v") || Input.GetButtonUp("js2"))
        {
            if (view.IsMine)
            {
                VoiceRecorder.TransmitEnabled = false;
            }
        }
    }
}