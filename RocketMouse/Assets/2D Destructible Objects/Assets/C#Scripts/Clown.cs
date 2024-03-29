﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Clown : MonoBehaviour
{
    public float maxSpeed = 100f;
    public float Speed = 50f;
    public float jumpForce = 1000f;
    public GameObject Boost;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private Rigidbody2D rigidBody;
    private Animator animator;

    private bool moveLeft = false;
    private bool moveRight = false;
    private bool isMoving = false;
    private bool isGrounded = false;
    bool doubleJump = false;

    // Use this for initialization
    void Start ()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        //else if (CrossPlatformInputManager.GetAxis("Horizontal") == 0)
        //{
        //    isMoving = false;
        //    moveRight = false;
        //    moveLeft = false;
        //    animator.SetTrigger("Stand");
        //}

        if (CrossPlatformInputManager.GetButtonDown("Jump") && (isGrounded || !doubleJump))
        {
            rigidBody.AddForce(new Vector2(0, jumpForce));

            if (!doubleJump && !isGrounded)
            {
                doubleJump = true;
                Boost = Instantiate(Resources.Load("Prefabs/Cloud"), transform.position, transform.rotation) as GameObject;
                //	cloudanim.Play("cloud");		
            }
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.07F, whatIsGround);

        if (isGrounded)
            doubleJump = false;

        float move = 0;

        if (moveLeft)
            move = -1;
        else if (moveRight)
            move = 1;

        float hor = CrossPlatformInputManager.GetAxis("Horizontal");

        rigidBody.velocity = new Vector2(hor * maxSpeed, move * rigidBody.velocity.y);

        if (rigidBody.velocity.y == 0)
        {
            isMoving = false;
            moveRight = false;
            moveLeft = false;
            animator.SetTrigger("Stand");
        }

        if (CrossPlatformInputManager.GetButtonDown("Hammer"))
        {
            moveRight = false;
            moveLeft = false;
            isMoving = false;
            animator.SetTrigger("Attack");
        }
        else if (CrossPlatformInputManager.GetAxis("Horizontal") < 0)
        {
            moveLeft = true;
            moveRight = false;

            if (!isMoving)
            {
                isMoving = true;
                animator.SetFloat("Direction", -1);
                animator.SetTrigger("Move");
            }
        }
        else if (CrossPlatformInputManager.GetAxis("Horizontal") > 0)
        {
            moveRight = true;
            moveLeft = false;

            if (!isMoving)
            {
                isMoving = true;
                animator.SetFloat("Direction", 1);
                animator.SetTrigger("Move");
            }
        }

        //if (move != 0)
        //{


        //if (Mathf.Abs(rigidBody.velocity.x) < maxSpeed)
        //{
        //    Vector2 toMove = new Vector2(move * Speed, rigidBody.velocity.y);
        //    rigidBody.AddForce(toMove);
        //}
        //}
        //else if(move == 0)
        //{
        //    rigidBody.velocity = new Vector2(0, 0);
        //}
    }
}
