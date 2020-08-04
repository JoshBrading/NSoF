using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyAI : MonoBehaviour
{
    public bool isBoss;
    public GameObject skull;
    public Transform Player;
    public CharacterController controller;
    public float health;
    public int MoveSpeed;
    int MinDist = 40;
    float gravity = -18f;
    Vector3 velocity;

    void Start()
    {

    }

    void Update()
    {
        // At some point I should check how far into the water the entity moves to prevent it from going under...
        Vector3 position = new Vector3(Player.position.x, transform.position.y, Player.position.z);
        transform.LookAt(position);

        if (Vector3.Distance(transform.position, Player.position) <= MinDist)
        {
            Debug.Log(Vector3.Distance(transform.position, Player.position));
            controller.Move(transform.forward * MoveSpeed * Time.deltaTime);
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
        if(isBoss)
            Instantiate(skull, transform.position, Quaternion.identity); // If the enemy is a boss then drop skulls for order of souls... Make other skulls and randomize the drops soon™
        Destroy(this.gameObject);
    }
}