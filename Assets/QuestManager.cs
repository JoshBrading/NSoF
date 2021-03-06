﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Rendering;

// This file is pretty unorganized, PlayerMovement.cs also has a lot of questing stuff that should be moved here
public class QuestManager : MonoBehaviour
{
    public int questType;
    // Quest Types
    // 0 = None
    // 1 = Gold Hoarders
    // 2 = Merchant Alliance
    // 3 = Order of Souls
    bool active = false;
    public TextMeshProUGUI text;
    public GameObject enemy, enemy_boss, crate, chest;
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            switch (questType)
            {
                case 0:
                    text.SetText("");
                    break;
                case 1:
                    text.SetText("Quest: Gold Hoarders");
                    active = true;
                    Quest_GH();
                    break;
                case 2:
                    text.SetText("Quest: Merchant Alliance");
                    active = true;
                    Quest_MA();
                    break;
                case 3:
                    text.SetText("Quest: Order of Souls");
                    active = true;
                    Quest_OS();
                    break;

            }
        }

        if (Input.GetKeyDown(KeyCode.P))    // Ok so yea, basically we never check if the quest is over so this way the player can force quit the quest
        {                                   // I would like to remove this and setup a system that checks if the player has completed the quest but that sounds like a couple hours of coding :)
            questType = 0;
            active = false;
        }
    }

    private void Quest_GH()
    {
        int index;
        // Possible locations for gold hoarder chest to spawn... this should be relayed to an in game map at some point... 
        // also setup zones and then pick a random location in those zones to make this less predicatable... how to make these zones? google.com
        Vector3[] position_array = { 
            new Vector3 { x = -562, y = 110, z = 1267 }, 
            new Vector3 { x = -320 , y = 88, z = 1066 }, 
            new Vector3 { x = -437, y = 97, z = 1197 } 
        };
        index = Random.Range(0, position_array.Length);
        Debug.Log("Position: " + index);
        Vector3 position = position_array[index];
        Spawn(enemy, position, 5, 100); // This is lame, instantiate the chest under the ground and make the player dig it up, once the player "hits" the chest then spawn enemies
        Instantiate(chest, position, Quaternion.identity);

    }

    private void Quest_MA()
    {
        // FIXME: These shouldnt spawn on top of each other, add an offset so the player doesnt get confused on why there is 3 in 1
        Instantiate(crate, transform.position, Quaternion.identity);
        Instantiate(crate, transform.position, Quaternion.identity);
        Instantiate(crate, transform.position, Quaternion.identity);

    }
    private void Quest_OS()
    {
        int index;
        Vector3[] position_array = { // Possible locations for gold hoarder chest to spawn
            new Vector3 { x = -562, y = 110, z = 1267 },
            new Vector3 { x = -320 , y = 88, z = 1066 },
            new Vector3 { x = -437, y = 97, z = 1197 }
        };
        index = Random.Range(0, position_array.Length);
        Debug.Log("Position: " + index);
        Vector3 position = position_array[index];
        Spawn(enemy, position, 9, 100);
        Spawn(enemy_boss, position, 3, 200);
    }

    // I should make a command file and put a bunch of commands in there but maybe later?
    // FIXME: Move to seperate cmd file
    public void Spawn(GameObject entity, Vector3 pos, int count, float health) 
    {               // Spawns "count" of "entity" with "health" at "pos" + offset
        Vector3 position = pos;
        Debug.Log("=== SPAWN CALLED ===");
        for (int i = 0; i < count; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-10.0F, 10.0F), 0, Random.Range(-10.0F, 10.0F));
            position = position + offset;
            GameObject spawned_enemy = Instantiate(entity, position, Quaternion.identity);
            spawned_enemy.GetComponent<EnemyAI>().Player = Player;
            spawned_enemy.GetComponent<EnemyAI>().health = health;
            spawned_enemy.GetComponentInChildren<TrackPlayer>().Player = Player;
        }
    }
}
