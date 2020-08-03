using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform boat;
    public GameObject gun, sword;
    public bool onBoat = false;
    public Camera cam;
    public TextMeshProUGUI text, dialog, gold;
    public float speed = 12f, groundDistance = 0.4f, jumpHeight = 3f, health;
    public Transform groundCheck;
    public LayerMask groundMask;
    public static bool lockMovement;
    public int goldCount;
    float gravity = -18f;
    Vector3 velocity;
    bool isGrounded, isHolding = false;
    Transform holding;
    public bool godmode;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            gun.SetActive(true);
            sword.SetActive(false);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            gun.SetActive(false);
            sword.SetActive(true);
        }

            text.SetText("Health: " + health);
        gold.SetText("Gold: " + goldCount);


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


        if (Input.GetKeyDown(KeyCode.F) && !isHolding)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 4))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if(interactable != null)
                {
                    isHolding = true;
                    hit.transform.parent = transform;
                    hit.transform.position = transform.position + transform.forward + new Vector3 { x = 0, y = 0.75f, z = 0 };
                    holding = hit.transform;
                }
            }
        }
        if(holding != null && Input.GetKeyDown(KeyCode.X))
        {
            isHolding = false;
            holding.position = transform.position - new Vector3 { x = 0, y = 2, z = 0 };
            if (onBoat)
            {
                holding.parent = boat;
            }
            else
            {
                holding.parent = null;
            }
            
                holding = null;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(transform.position);
        }

        if (true) // Yes I know this is extremely stupid lmao
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 5))
            {
                if (hit.transform.tag == "Merchant")
                {
                    if (holding != null && holding.tag == "Merchant_Item")
                    {
                        dialog.SetText("Deliver to Name on Island");
                    }
                    else if (isHolding)
                    {
                        dialog.SetText("This item cannot be sold to Merchant Alliance");
                    }
                    else
                    {
                        dialog.SetText("Press F to claim Merchant Alliance quest || 1000 Gold");
                        if (Input.GetKeyDown(KeyCode.F) && GetComponent<QuestManager>().questType == 0)
                        {
                            GetComponent<QuestManager>().questType = 2;
                            // Quest Types
                            // 0 = None
                            // 1 = Gold Hoarders
                            // 2 = Merchant Alliance
                            // 3 = Order of Souls
                            goldCount += -1000;
                        }
                    }

                }
                else if (hit.transform.tag == "SellMerchant")
                {
                    if (holding != null && holding.tag == "Merchant_Item")
                    {
                        dialog.SetText("Sell crate for 1250 Gold");
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            goldCount += 1250;
                            Destroy(holding.gameObject);
                            holding = null;
                            isHolding = false;
                        }
                    }
                    else if (isHolding)
                    {
                        dialog.SetText("This item cannot be sold to Merchant Alliance");
                    }
                    else
                    {
                        dialog.SetText("Pick up a quest to sell crates to me from the outpost!");
                    }

                }
                else if (hit.transform.tag == "Gold")
                {
                    if (holding != null && holding.tag == "Gold_Item")
                    {
                        dialog.SetText("Sell chest for 1250 Gold");
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            goldCount += 1250;
                            Destroy(holding.gameObject);
                            holding = null;
                            isHolding = false;
                        }
                    }
                    else if (isHolding)
                    {
                        dialog.SetText("This item cannot be sold to Gold Hoarders");
                    }
                    else
                    {
                        dialog.SetText("Press F to claim Gold Hoarders quest || 1000 Gold");
                        if (Input.GetKeyDown(KeyCode.F) && GetComponent<QuestManager>().questType == 0)
                        {
                            GetComponent<QuestManager>().questType = 1;
                            goldCount += -1000;
                        }
                    }
                }
                else if (hit.transform.tag == "Order")
                {
                    if (holding != null && holding.tag == "Order_Item")
                    {
                        dialog.SetText("Sell skull for 1250 Gold");
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            goldCount += 1250;
                            Destroy(holding.gameObject);
                            holding = null;
                            isHolding = false;
                        }
                    }
                    else if (isHolding)
                    {
                        dialog.SetText("This item cannot be sold to Order of Souls");
                    }
                    else
                    {
                        dialog.SetText("Press F to claim Order of Souls quest || 1000 Gold");
                        if (Input.GetKeyDown(KeyCode.F) && GetComponent<QuestManager>().questType == 0)
                        {
                            GetComponent<QuestManager>().questType = 3;
                            goldCount += -1000;
                        }
                    }
                }
                else
                {
                    dialog.SetText("");
                }
            }
            else
            {
                dialog.SetText("");
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            dialog.SetText("Are you sure you want to quit? (Left click)");
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Application.Quit();
                //SceneManager.LoadScene("Start");

            }
        }

    }
    public void TakeDamage(int damage)
    {
        if (godmode)
        {
            return;
        }
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
