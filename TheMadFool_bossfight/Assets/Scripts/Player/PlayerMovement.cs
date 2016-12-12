using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rbody2D;
    Animator anim;
    public bool attacking = false;
    public AudioClip attackSound;
    AudioSource audio;

	// Use this for initialization
	void Start ()
    {
        rbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (movement != Vector2.zero)
        {
            anim.SetBool("is_walking", true);
            anim.SetFloat("input_x", movement.x);
            anim.SetFloat("input_y", movement.y);
        }
        else
        {
            anim.SetBool("is_walking", false);
        }

        rbody2D.MovePosition(rbody2D.position + movement * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            attacking = true;
            anim.SetBool("is_attacking", true);
            audio.PlayOneShot(attackSound, 0.5f);
        }
        else
        {
            anim.SetBool("is_attacking", false);
        }
	}
}
