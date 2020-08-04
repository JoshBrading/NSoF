using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatChild : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void  OnTriggerEnter(Collider other)    // Do triggers only work on character controllers? They dont detect other objects being placed on the boat? 
    {                                       //Will enemies be made children to the boat? Probably but their character controller doesnt allow them to "stick" but this is still not good probably?
        other.transform.parent = transform;
        player.GetComponent<PlayerMovement>().onBoat = true;
    }
    void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
        player.GetComponent<PlayerMovement>().onBoat = false;
    }
}
