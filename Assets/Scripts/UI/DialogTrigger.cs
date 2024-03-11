using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public SpriteRenderer ren;
    public Interact player;
    public float interact_distance;
    public List<string> messages;

    void Start()
    {

    }

    void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < interact_distance)
        {
            Action action = player.GetComponent<Action>();
            Movement movement = player.GetComponent<Movement>();
            //action.SetNextState(action.dialog)
            //movement.SetNextState(action.dialog)
            player.TrySetTrigger(this);
        }
    }
}
