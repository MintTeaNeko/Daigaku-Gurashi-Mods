using System;
using System.IO;
using System.Reflection;
using ModLoader;
using UnityEngine;

namespace firstmod
{
    public class firstmod : MonoBehaviour
    {
        private EventManager eventManager;
        private Character character;
        private bool isDebounce = false;

        private void Start()
        {
            eventManager = FindObjectOfType<EventManager>(true);
            eventManager.NotifyPlayer("first mod ever has loaded!", 0);
            character = eventManager.Student[0];
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (!isDebounce)
                {
                    eventManager.NotifyPlayer("nice new oppais <3", 0);
                    int random = UnityEngine.Random.Range(0, 6);
                    for (int i = 0; i < 2; i++)
                    {
                        character.OppaiGO[i].GetComponent<Oppai>().ChangeCupSize(random);
                    }
                }

                isDebounce = true;
            }
            else
            {
                isDebounce = false;
            }
        }
    }
}