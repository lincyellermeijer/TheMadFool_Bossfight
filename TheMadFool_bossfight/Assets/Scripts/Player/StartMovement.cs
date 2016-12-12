using UnityEngine;
using System.Collections;

public class StartMovement : MonoBehaviour
{
    private Rigidbody2D rbody2D;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
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
    }
}
