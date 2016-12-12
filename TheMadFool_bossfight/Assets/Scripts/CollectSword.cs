using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollectSword : MonoBehaviour
{
    public NPC_Dialog collectSword;
    public AudioClip collectSound;
    float volume = 0.5f;

    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        if (collectSword.hasAccepted)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && collectSword.hasAccepted)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position, volume);
            Destroy(gameObject);
            collectSword.foundSword = true;
            collectSword.showSword = true;
        }
    }
}