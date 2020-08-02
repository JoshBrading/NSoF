using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public CharacterController controller;
    float gravity = -1, speed = 24;
    Vector3 velocity;
    bool atHelm = false, moveable = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 74f) //This just emulates buoyancy and I'm sure there is a much better way to do it
            gravity = 1f;
        else if (transform.position.y > 74f)
            gravity = -1f;

 
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.F) && atHelm)
        {
            //PlayerMovement.speed = 0;
            PlayerMovement.lockMovement = true;
            moveable = true;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            PlayerMovement.lockMovement = false;
            moveable = false;
        }
        if (moveable)
        {
            //float z = Input.GetAxis("Horizontal");
            

            Vector3 move = transform.right * -Input.GetAxis("Vertical"); ;
            controller.Move(move * speed * Time.deltaTime);
            transform.Rotate(0, Input.GetAxis("Horizontal") * 12f * Time.deltaTime, 0f);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        atHelm = true;
    }
    void OnTriggerExit(Collider other)
    {
        atHelm = false;
    }


}
