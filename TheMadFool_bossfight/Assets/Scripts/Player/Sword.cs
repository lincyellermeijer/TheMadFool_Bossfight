using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

    public int damage;

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().TakeDamage(damage);
        }
    }
}
