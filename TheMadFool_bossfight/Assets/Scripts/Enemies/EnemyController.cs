using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public float max_health;
    public float moveSpeed;
    public GameObject healthBar;
    public int damage;
    private GameObject objSpawn;
    private int SpawnerID;
    public GameObject hurtParticle;
    public AudioClip hurtSound;

    [HideInInspector]
    public float cur_health;
    [HideInInspector]
    public BoxCollider2D collider;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public PlayerHealth playerHealth;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public AudioSource audio;

    // Use this for initialization
    public virtual void Start()
    { 
        cur_health = max_health;
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = FindObjectOfType<PlayerHealth>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

        // Used to find the parent spawner object
        objSpawn = (GameObject)GameObject.FindWithTag("Spawner");
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (cur_health <= 0)
        {
            Die();
        }
    }

    public virtual void TakeDamage(float damage)
    {
        cur_health -= damage;
        // calculate value 0-1 enemyHealth
        float calc_health = cur_health / max_health;
        SetHealthBar(calc_health);
        audio.PlayOneShot(hurtSound, 0.5f);
    }

    public virtual void SetHealthBar(float enemyHealth)
    {
        // mathClamp so no overflow happens
        healthBar.transform.localScale = new Vector3(Mathf.Clamp(enemyHealth, 0f, 1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

    // Call this when you want to kill the enemy
    public virtual void Die()
    {
        Instantiate(hurtParticle, transform.position, Quaternion.Euler(-70, -180, -180));

        objSpawn.BroadcastMessage("killEnemy", SpawnerID);
        Destroy(gameObject);
    }

    // this gets called in the beginning when it is created by the spawner script
    public virtual void setName(int sName)
    {
        SpawnerID = sName;
    }

    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (playerHealth.cur_health > 0)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
