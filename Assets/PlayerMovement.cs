using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Camera cam;
    public TextMeshProUGUI text;
    public float speed = 12f, groundDistance = 0.4f, jumpHeight = 3f, health;
    public Transform groundCheck;
    public LayerMask groundMask;
    public static bool lockMovement;
    float gravity = -18f;
    Vector3 velocity;
    bool isGrounded;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        text.SetText("Health: " + health);


        if (lockMovement)
            controller.enabled = false;
        else
            controller.enabled = true;

        if (groundCheck.position.y < 65f)
            gravity = 10f;
        else if (groundCheck.position.y < 74f)
            gravity = 1f;
        else if (groundCheck.position.y > 74f && groundCheck.position.y < 75f)
            gravity = -1f;
        else
            gravity = -18f;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.F))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if(interactable != null)
                {
                    speed = 100;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(transform.position);
        }


    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Taken" + damage);
        if (health <= 0f)
        {
            Die();
        }
    }
    public void Die()
    {
        Application.LoadLevel(Application.loadedLevel);
        //Destroy(this.gameObject);
    }
}
