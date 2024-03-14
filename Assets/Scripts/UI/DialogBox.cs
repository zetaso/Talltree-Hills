using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    public TMP_Text text;
    public Image talker_image;
    public InteractionTrigger provider;
    public float characters_per_second, delay_character;
    public GameObject box, hint;

    int current_message = 0, current_character;
    float time_to_next;
    bool writing, waiting_continue;

    void Update()
    {
        if (writing)
        {
            if (current_message == provider.origin_messages.Count)
            {
                EndDialog();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0))
                {
                    text.text = provider.origin_messages[current_message].message.Replace("_", "");
                    current_character = provider.origin_messages[current_message].message.Length;
                }

                time_to_next -= Utils.dialogDeltaTime;
                if (current_character == provider.origin_messages[current_message].message.Length)
                {
                    current_message++;
                    current_character = 0;
                    writing = false;
                    waiting_continue = true;
                }
                else if (time_to_next <= 0)
                {
                    char new_character = provider.origin_messages[current_message].message[current_character];
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
                if (provider.origin_messages.Count > 0 && current_message < provider.origin_messages.Count)
                    talker_image.sprite = Utils.Instance.talker_profiles[(int)provider.origin_messages[current_message].talker];
                time_to_next = 0;
                text.text = "";
            }
        }
    }

    public void EnterDialog(InteractionTrigger dialog_trigger)
    {
        provider = dialog_trigger;
        if (dialog_trigger.origin_messages.Count > 0)
        {
            writing = true;
            waiting_continue = false;
            box.SetActive(true);
            hint.SetActive(false);
            Time.timeScale = 0;
            time_to_next = 0;
            current_message = 0;
            current_character = 0;
            text.text = "";
            Utils.Instance.pause.dialog = true;
            if (provider.origin_messages.Count > 0 && current_message < provider.origin_messages.Count)
                talker_image.sprite = Utils.Instance.talker_profiles[(int)provider.origin_messages[current_message].talker];
        }
        provider.OnInteracting();
    }

    void EndDialog()
    {
        if (provider.origin_messages.Count > 0)
        {
            writing = false;
            box.SetActive(false);
            hint.SetActive(true);
            Time.timeScale = 1;
            Utils.Instance.pause.dialog = false;
        }
        provider.OnEndInteracting();
        provider = null;
    }
}
