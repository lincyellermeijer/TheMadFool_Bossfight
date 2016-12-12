using UnityEngine;
using System.Collections;

public class Hard_Enemy_Controller : EnemyController
{
    private Vector2 direction;
    private Vector2 movement;
    private float minDistance = 1f;
    private float range;
    Transform target;

    public override void Start()
    {
        direction = new Vector2(Random.value, Random.value);
        direction.Normalize();

        target = GameObject.FindGameObjectWithTag("Player").transform;

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
        range = Vector2.Distance(transform.position, target.position);

        if (range < minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            transform.position.Normalize();
        }
        else
        {
            rb.velocity = movement;
        }
    }
}
