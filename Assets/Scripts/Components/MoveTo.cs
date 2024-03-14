using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    public Transform[] objects_to_move;
    public Transform dest;

    public void Move()
    {
        for (int i = 0; i < objects_to_move.Length; i++)
        {
            objects_to_move[i].position = dest.position;
        }
    }
}
