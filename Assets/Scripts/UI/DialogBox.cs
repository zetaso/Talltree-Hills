using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogBox : MonoBehaviour
{
    public TMP_Text text;
    public DialogTrigger provider;
    public float characters_per_second, delay_character;
    public GameObject box;

    int current_message = 0, current_character;
    float time_to_next;
    bool writing, waiting_continue, pressing;

    void Update()
    {
        if (writing)
        {
            if (current_message == provider.messages.Count)
            {
                EndDialog();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0))
                {
                    text.text = provider.messages[current_message].Replace("_", "");
                    current_character = provider.messages[current_message].Length;
                }

                time_to_next -= Utils.unpausedDeltaTime;
                if (current_character == provider.messages[current_message].Length)
                {
                    current_message++;
                    current_character = 0;
                    writing = false;
                    waiting_continue = true;
                }
                if (time_to_next <= 0)
                {
                    char new_character = provider.messages[current_message][current_character];
                    current_character++;
                    if (new_character != '_')
                    {
                        text.text += new_character.ToString();
                        time_to_next = 1f / characters_per_second;
                    }
                    else
                        time_to_next = 1f / characters_per_second * delay_character;
                }
            }
        }
        else if (!writing && waiting_continue)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0))
            {
                writing = true;
                waiting_continue = false;
                time_to_next = 0.125f;
                text.text = "";
                pressing = true;
            }
        }
    }

    public void EnterDialog(DialogTrigger dialog_trigger)
    {
        provider = dialog_trigger;
        writing = true;
        waiting_continue = false;
        box.SetActive(true);
        Time.timeScale = 0;
        time_to_next = 0.5f;
        current_message = 0;
        current_character = 0;
        text.text = "";
    }

    void EndDialog()
    {
        writing = false;
        box.SetActive(false);
        Time.timeScale = 1;
    }
}
