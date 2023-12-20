using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    public CharacterController2D controller;
    public float HorizontalMove = 0f;
    public float run_speed = 40f;
    bool jump = false;
    public Animator animator;
    public Vector3 respawnPoint;

    //Start is called before the first frame update
    void Start()
    {
        respawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMove = CrossPlatformInputManager.GetAxisRaw("Horizontal") * run_speed;
        animator.SetFloat("speed", Mathf.Abs(HorizontalMove));
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("Jumping", true);
        }
    }

    public void OnLanding()
    {
        animator.SetBool("Jumping", false);
        animator.SetBool("ground", true);
    }
    
    void FixedUpdate()
    {
        //Move our Character
        controller.Move(HorizontalMove*Time.fixedDeltaTime,jump);
        jump = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Moving Platform"))
            this.transform.parent = col.gameObject.transform;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Moving Platform"))
            this.transform.parent = null;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Fall Zone")
            transform.position = respawnPoint;
        if (col.tag == "EnemyAttack")
            transform.position = respawnPoint;
        if (col.tag == "Respawn")
            respawnPoint = col.transform.position;
    }
}
