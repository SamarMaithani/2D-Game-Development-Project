using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    Transform castPoint;
    [SerializeField]
    float lineOfSight;
    public Animator animator;
    bool FacingLeft = false;
    bool isAgro = false;
    bool isLooking = false;
    const string LEFT = "left";
    const string RIGHT = "right";
    string facing;
    public float speed = 5f;
    public Rigidbody2D rb;
    Vector3 baseScale;
    Vector3 laserScale;
    public GameObject laser;
    public float fireRate;
    private float fireStart;

    [SerializeField]
    Transform frontcheck;
    [SerializeField]
    float groundcheck;

    void Start()
    {
        baseScale = transform.localScale;
        laserScale = laser.transform.localScale;
        fireStart = fireRate;
        facing = RIGHT;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (VisualRange(lineOfSight))
        {
            isAgro = true;
        }
        else
        {
            if(isAgro)
            {
                if(!isLooking)
                {
                    isLooking = true;
                    Invoke("Patrol", 3);
                }
            }
        }
        if (isAgro)
            AttackPlayer();
        else
            Patrol();
    }

    public void Patrol()
    {       
            isAgro = false;
            isLooking = false;
            float s = speed;
            if (facing == LEFT)
                s = -speed;
            rb.velocity = new Vector2(s, rb.velocity.y);
            if (HitWall() || Edge())
            {
                if (facing == LEFT)
                {
                    Flip(RIGHT);
                    FacingLeft = false;
                }
                else
                {
                    Flip(LEFT);
                    FacingLeft = true;
                }
            }
    }

    public void Flip(string Dir)
    {
        Vector3 newScale = baseScale;
        Vector3 newlaser = laserScale;
        if (Dir == LEFT)
        {
            newScale.x = -baseScale.x;
            newlaser.x = -laserScale.x;
        }
        else
        {
            newScale.x = baseScale.x;
            newlaser.x = laserScale.x;
        }

        transform.localScale = newScale;
        laser.transform.localScale = newlaser;
        facing = Dir;
    }

    public bool HitWall()
    {
        bool val = false;
        float castDist = groundcheck;
        if (facing == LEFT)
            castDist = -groundcheck;
        else
            castDist = groundcheck;
        Vector3 targetPos = frontcheck.position;
        targetPos.x += castDist;

        Debug.DrawLine(frontcheck.position, targetPos, Color.red);

        if (Physics2D.Linecast(frontcheck.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
            val = true;
        else
            val = false;
        return val;
    }

    public bool Edge()
    {
        bool val = true;
        float castDist = groundcheck;

        Vector3 targetPos = frontcheck.position;
        targetPos.y -= castDist;

        Debug.DrawLine(frontcheck.position, targetPos, Color.green);

        if (Physics2D.Linecast(frontcheck.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
            val = false;
        else
            val = true;
        return val;
    }

    bool VisualRange(float distance)
    {
        bool val = false;
        float castDist = distance;
        if (FacingLeft)
            castDist = -distance;
        Vector3 endPos = castPoint.position;
        endPos.x += castDist;

        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Default"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
                val = true;
            else
                val = false;
            Debug.DrawLine(castPoint.position, hit.point, Color.blue);
        }
        else
            Debug.DrawLine(castPoint.position, endPos, Color.yellow);
        return val;
    }

    void AttackPlayer()
    {
        if (transform.position.x < player.position.x)
        {
            rb.velocity = new Vector2(0, 0);
            if(fireRate <= 0)
            {
                animator.SetBool("isAttacking", true);
                Instantiate(laser, castPoint.position, Quaternion.identity);
                fireRate = fireStart;
            }
            else
                fireRate -= Time.deltaTime;
            Flip(RIGHT);
            FacingLeft = false;
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            if (fireRate <= 0)
            {
                animator.SetBool("isAttacking", true);
                Instantiate(laser, castPoint.position, Quaternion.identity);
                fireRate = fireStart;
            }
            else
                fireRate -= Time.deltaTime;
            Flip(LEFT);
            FacingLeft = true;
        }
        animator.SetBool("isAttacking", false);
    }
}