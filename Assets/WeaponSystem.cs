﻿using UnityEngine;
using TMPro;

public class WeaponSystem : MonoBehaviour // A lot of this was borrowed from a youtube tutorial, if I can find the link I'll source it here: ____
{
    public Camera cam;
    public GameObject EnemyShootingPoint;
    public RaycastHit hit;
    public LayerMask enemy;
    public TextMeshProUGUI text;

    // A lot of these variables arent useful for this game but oh well
    public int damage, magSize, bulletsPerTap;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public bool allowButtonHold, isEnemy;
    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;


    void OnDrawGizmosSelected() // This is cool, I didnt know this was a thing
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void Start()
    {
        bulletsLeft = magSize;
        readyToShoot = true;
    }
    private void Update()
    {
        PlayerInput();
        if(!isEnemy)
            text.SetText(bulletsLeft + "  /  " + magSize);
    }
    private void PlayerInput()
    {
        if (!isEnemy)
        {
            if (allowButtonHold)
                shooting = Input.GetKey(KeyCode.Mouse0);
            else
                shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }
        else
        {
            shooting = true;
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magSize && !reloading)
            Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            if (!isEnemy)
                Shoot();
            else
                EnemyShoot();
        }
    }
    private void Shoot()
    {
        Animator anim = gameObject.GetComponent<Animator>();

        anim.SetTrigger("Shoot");
        
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = cam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(cam.transform.position, direction, out hit, range, enemy))
        {
            Debug.Log(hit.collider.name);

            if (hit.transform.tag == "Enemy")
            {
                hit.collider.GetComponent<EnemyAI>().TakeDamage(damage);
                Debug.Log("Hit");
            }
        }
        else
        {
            Debug.Log("Missed");
        }

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    private void EnemyShoot()   // Using a seperate function for enemy hits just cause I didnt like so many if statements to check if it was the player shooting or an enemy
    {                           // Also melees should not be rays they should be collision detections... fix that later?
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = EnemyShootingPoint.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(EnemyShootingPoint.transform.position, direction, out hit, range, enemy))
        {
            Debug.Log(hit.collider.name);

            if (hit.transform.tag == "Player")
            {
                hit.collider.GetComponent<PlayerMovement>().TakeDamage(damage);
                Debug.Log("Hit");
            }
        }
    //    else
    //    {
    //        Debug.Log("Missed");
    //    }

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        Animator anim = gameObject.GetComponent<Animator>();

        anim.SetTrigger("Reload");
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magSize;
        reloading = false;
    }
}
