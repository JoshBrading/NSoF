using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    int questType = 0;
    // Quest Types
    // 0 = None
    // 1 = Gold Hoarders
    // 2 = Merchant Alliance
    // 3 = Order of Souls
    bool active = false;
    public TextMeshProUGUI text;

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
                    text.SetText("No quest active");
                    break;
                case 1:
                    text.SetText("Quest: Gold Hoarders");
                    active = true;
                    break;
                case 2:
                    text.SetText("Quest: Merchant Alliance");
                    active = true;
                    break;
                case 3:
                    text.SetText("Quest: Order of Souls");
                    active = true;
                    break;

            }
        }
    }

    private void Quest_GH()
    {

    }
    private void Quest_MA()
    {

    }
    private void Quest_OS()
    {

    }
}
