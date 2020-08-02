using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Rendering;

public class QuestManager : MonoBehaviour
{
    int questType = 1;
    // Quest Types
    // 0 = None
    // 1 = Gold Hoarders
    // 2 = Merchant Alliance
    // 3 = Order of Souls
    bool active = false;
    public TextMeshProUGUI text;
    public GameObject enemy;
    public Transform Player;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("=== QUEST MANAGER START ===");
       // Spawn(enemy, Player, 3, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            switch (questType)
            {
                case 0:
                    text.SetText("No quest active");
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
    }

    private void Quest_GH()
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
        Spawn(enemy, position, 5, 100);
    }
    private void Quest_MA()
    {

    }
    private void Quest_OS()
    {

    }

    // I should make a command c# file and put a bunch of commands in there but maybe later?
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
