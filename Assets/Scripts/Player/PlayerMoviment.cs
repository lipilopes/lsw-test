using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMoviment : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.5f;

    Vector2 movement;

    Rigidbody2D rb;
    Animator anim;
    MobsClothes mobClothes;

    bool canMove = true;
    public bool CanMove {set{    canMove = value; } }

    void Start() 
    {      
        rb          = GetComponent<Rigidbody2D>();
        anim        = GetComponent<Animator>();
        mobClothes  = GetComponent<MobsClothes>();
    }

    private void Update() 
    {
        if(canMove)
        {
            movement.x  = Input.GetAxisRaw("Horizontal");
            movement.y  = Input.GetAxisRaw("Vertical"); 
        }

        Anim();      
    }

    private void FixedUpdate() 
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Anim()
    {
        if(mobClothes!=null)
            mobClothes.AnimClothes(movement.sqrMagnitude, movement.x, movement.y);

        if(anim == null)
            return;

        anim.SetFloat("Horizontal",movement.x);
        anim.SetFloat("Vertical",movement.y);
        anim.SetFloat("Speed",movement.sqrMagnitude);
    }
}