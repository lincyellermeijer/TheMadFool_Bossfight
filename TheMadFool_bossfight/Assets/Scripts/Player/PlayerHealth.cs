using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public Image healthBar;
    public Text healthText;
    public float max_health = 100f;
    public float cur_health = 0f;
    public GameObject hurtParticle;
    public AudioClip hurtSound;
    private AudioSource[] allAudioSources;
    [HideInInspector]
    public AudioSource audio;

    void Start()
    {
        cur_health = max_health;
        allAudioSources = FindObjectsOfType<AudioSource>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = ("HP = " + cur_health);
        if (cur_health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        cur_health -= damage;
        float calc_health = cur_health / max_health;
        SetHealth(calc_health);

        audio.PlayOneShot(hurtSound, 2f);
        Instantiate(hurtParticle, transform.position, Quaternion.Euler(-70, -180, -180));
    }

    public void Die()
    {
        Application.LoadLevel("Game_over");

        for (int i = 0; i < allAudioSources.Length; i++)
        {
            allAudioSources[i].Stop();
        }
    }

    void SetHealth(float myHealth)
    {
        healthBar.fillAmount = myHealth;
    }

}
