using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float speed = 10.0F;
    public float gravity = 10.0F;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


// Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 dircetion = new Vector3(horizontal, 0, vertical);
        Vector3 velocity = dircetion * speed;
        velocity = Camera.main.transform.TransformDirection(velocity);
        velocity.y = -gravity;
        controller.Move(velocity* Time.deltaTime);

    }
}
