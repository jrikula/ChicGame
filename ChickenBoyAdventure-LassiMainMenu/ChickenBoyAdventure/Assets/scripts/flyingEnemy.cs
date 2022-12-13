using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingEnemy : MonoBehaviour
{

    public float flySpeed = 4f;
    public float flyStopRate = 0.2f;
    public float waypointReachDistance= 0.1f;


    public List<Transform> Waipoints;
    int waypointNum =0;
    Transform nextWaypoint;



    Rigidbody2D rb;
    Animator animator;
    TouchingDirections touchingDirections;
    Damageable damageable;

    public DetectionZone attackZone;

 
 
    public bool _hasTarget = false;
    public bool HasTarget
    {
        get
        {
            return _hasTarget;
        }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);

        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public float AttackCooldown {
        get 
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        private set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
        }
    }

    void Awake()
    {
        touchingDirections = GetComponent<TouchingDirections>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();

    }
    
    void Start()
    {
        nextWaypoint = Waipoints[waypointNum];
    }

    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;  
       
          if(AttackCooldown > 0)
             {
                AttackCooldown -= Time.deltaTime;
             } 
    }


    void FixedUpdate()
    {
        if(damageable.IsAlive)
        {
            if(CanMove)
            {
                    Flight();    
            }
             else
            {
                rb.velocity = Vector3.zero;
            }
           
        }
       
        
       

    }
    private void Flight()
    {
            Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;

            float distance = Vector2.Distance(nextWaypoint.position , transform.position);

            rb.velocity = directionToWaypoint * flySpeed;
            updateDir();

            if(distance <= waypointReachDistance)
            {
                waypointNum++;

                if(waypointNum >= Waipoints.Count)
                {
                    waypointNum = 0;
                }

                nextWaypoint = Waipoints[waypointNum];
    
            }
    }



  public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
    private void updateDir()
    {
        Vector3 locScale = transform.localScale;

        if(transform.localScale.x > 0)
        {
            //facing the right
            if(rb.velocity.x < 0)
            {
                //flip
                transform.localScale = new Vector3(-1 * locScale.x , locScale.y, locScale.z);
            }
        }
        else
        {
            //facing the left
            if(rb.velocity.x > 0)
            {
                //flip
                transform.localScale = new Vector3(-1 * locScale.x , locScale.y, locScale.z);
            }
        }
    }

}
