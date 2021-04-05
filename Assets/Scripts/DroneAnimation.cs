using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        // if (Input.GetButtonDown("Fire1"))
        // {
        //     anim.Play("ShutDown");
        // }

        // if (Input.GetButtonDown("Jump"))
        // {
        //     anim.Play("WakeUp");
        // }

        // if (Input.GetButtonDown("Submit"))
        // {
        //     anim.Play("Destroyed");
        // }

        // if (Input.GetMouseButtonDown(0))
        // {
        //     anim.SetTrigger("wake");
        // }
        // if (Input.GetMouseButtonDown(1))
        // {
        //     anim.SetTrigger("destroy");
        // }
    }
}
