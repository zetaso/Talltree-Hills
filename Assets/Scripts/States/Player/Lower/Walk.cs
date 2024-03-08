using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : State
{
    [SerializeField] Direction direction;
    [SerializeField] protected Stamina stamina;
    [SerializeField] SpriteRenderer legs;
    [SerializeField] Transform torso;
    public string clip_name;
    public float speed;

    protected Movement movement;

    public override void Enter()
    {
        movement.animator.Play(clip_name);
        is_complete = true;
    }

    public override void Do()
    {
        movement.rb.velocity = Utils.Warp(new Vector2(movement.input.x, movement.input.y)) * speed;
        //movement.transform.position += (Vector3)movement.rb.velocity * Time.deltaTime;

        float angle = Vector3.SignedAngle(Vector3.right, movement.rb.velocity, Vector3.forward);

        if (angle < 0)
            angle += 360f;
        else if (angle > 360f)
            angle -= 360f;

        direction.SetDirection(angle / 360f);

        string name = legs.sprite.name;
        string number = name.Substring(name.Length - 2, 2);
        if (number[0] == '_')
            number = number[1].ToString();
        int index = int.Parse(number);
        if (index % 2 == 1)
            torso.transform.localPosition = Vector3.zero;
        else if (index % 4 == 0)
            torso.transform.localPosition = Vector3.up * 0.125f;
        else
            torso.transform.localPosition = Vector3.down * 0.125f;
    }

    public override State Next()
    {
        if (movement.input == Vector2.zero)
            return movement.idle;
        else if (Input.GetKeyDown(KeyCode.LeftShift) && stamina.stamina == 1)
            return movement.run;
        return null;
    }

    public override void Exit()
    {
    }

    public override void Setup(MonoBehaviour provider)
    {
        movement = provider as Movement;
    }
}
