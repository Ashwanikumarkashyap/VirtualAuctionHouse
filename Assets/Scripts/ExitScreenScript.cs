using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ExitScreenScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void getToLobby()
    {
        SceneManager.LoadScene(0);
    }
}
