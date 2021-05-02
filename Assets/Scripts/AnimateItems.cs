using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateItems : MonoBehaviour
{


    bool isDrawerOpen = false;
    bool isDroneAwake = false;
    bool isRobotWalking = false;
    bool isSpartanRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    public void animateVanity(Animator anim)
    {
        if (!isDrawerOpen)
        {
            anim.SetTrigger("open");
            isDrawerOpen = true;
        }
        else
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
        }
        else
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

        if (isSpartanRunning)
        {
            anim.SetTrigger("run");
            isSpartanRunning = true;
        }
        else
        {
            anim.SetTrigger("attack");
            isSpartanRunning = false;
        }
    }

    public void animateLeftCarDoor(Animator anim)
    {
        anim.SetTrigger("leftDoor");
    }
    public void animateRightCarDoor(Animator anim)
    {
        anim.SetTrigger("rightDoor");
    }


}
