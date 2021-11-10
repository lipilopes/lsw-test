using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMoviment : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.5f;
    //float horizontalMoviment;
    //float verticalMoviment;

    Vector2 movement;

    Rigidbody2D rb;
    Animator anim;

    void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update() 
    {
        //horizontalMoviment  = Input.GetAxisRaw("Horizontal");
        //verticalMoviment    = Input.GetAxisRaw("Vertical");

        movement.x  = Input.GetAxisRaw("Horizontal");
        movement.y  = Input.GetAxisRaw("Vertical"); 

        anim.SetFloat("Horizontal",movement.x);
        anim.SetFloat("Vertical",movement.y);
        anim.SetFloat("Speed",movement.sqrMagnitude);
    }

    private void FixedUpdate() 
    {
        //rb.velocity  = new Vector2(horizontalMoviment,verticalMoviment)*moveSpeed;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
