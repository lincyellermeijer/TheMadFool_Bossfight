using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Boss_Controller : EnemyController
{
    private Vector2 direction;
    private Vector2 movement;
    private float minDistance = 1f;
    private float range;
    Transform target;
    private AudioSource[] allAudioSources;
    bool followPlayer = false;

    public override void Start()
    {
        direction = new Vector2(Random.value, Random.value);
        direction.Normalize();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        allAudioSources = FindObjectsOfType<AudioSource>();

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

        if (cur_health < 30)
        {
            GetComponent<SpriteRenderer>().color = new Color32(255, 83, 83, 255);
            damage = 15;
        }
        if (cur_health < 10)
        {
            GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
            moveSpeed = 1.5f;
        }

        base.Update();
    }

    public override void Die()
    {
        Application.LoadLevel("Win_screen");
        for (int i = 0; i < allAudioSources.Length; i++)
        {
            allAudioSources[i].Stop();
        }

        base.Die();
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
