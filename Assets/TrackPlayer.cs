using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{

    public Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() // This script seems useless, this could probably be combined with another file
    {
        Vector3 position = new Vector3(Player.position.x, transform.position.y, Player.position.z);
        transform.LookAt(position);
    }
}
