using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyAI : MonoBehaviour
{

    public Transform Player;
    public CharacterController controller;
    public float health = 50f;

    int MoveSpeed = 5;
    int MaxDist = 10;
    int MinDist = 5;
    float gravity = -18f;
    Vector3 velocity;

    void Start()
    {

    }

    void Update()
    {

        Vector3 position = new Vector3(Player.position.x, transform.position.y, Player.position.z);
        transform.LookAt(position);

        if (Vector3.Distance(transform.position, Player.position) >= MinDist)
        {
            controller.Move(transform.forward * MoveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
            {
                //Here Call any function U want Like Shoot at here or something
            }
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


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
        Destroy(this.gameObject);
    }
}