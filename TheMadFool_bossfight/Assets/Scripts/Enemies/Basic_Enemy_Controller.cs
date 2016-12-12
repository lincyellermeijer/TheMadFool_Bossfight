using UnityEngine;
using System.Collections;

public class Basic_Enemy_Controller : EnemyController
{
    private Vector2 direction;
    private Vector2 movement;    

    public override void Start()
    {
        direction = new Vector2(Random.value, Random.value);
        direction.Normalize();

        base.Start();
    }

    public override void Update()
    {
        movement = new Vector2(moveSpeed * direction.x, moveSpeed * direction.y);

        if (movement != Vector2.zero)
        {
            anim.SetFloat("pos_x", movement.x);
            anim.SetFloat("pos_y", movement.y);
        }

        base.Update();
    }

    public override void OnCollisionEnter2D(Collision2D other)
    {
        direction.x *= -1;
        direction.y *= -1;

        base.OnCollisionEnter2D(other);
    }

    void FixedUpdate()
    {
        rb.velocity = movement;
    }
}
