using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnem : MonoBehaviour  
{
    public float damage;
    public float health;
    public float Speed;
    public float StopDistance;
    float initialSpeed;
    public bool isRight;
    public Rigidbody2D rig;
    public Animator anim;
    Transform player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initialSpeed = Speed;   
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.position);
        float playerpos = transform.position.x - player.position.x;

        if (playerpos > 0)
        {
            isRight = false;
        }
        else
        {
            isRight = true;
        }

        if(distance <= StopDistance)
        {
            Speed = 0f;
            player.GetComponent<player>().OnHit(damage);
        }
        else
        {
            Speed = initialSpeed;
        }
    }

    private void FixedUpdate()
    {
        if (isRight)
        {
            rig.velocity = new Vector2(Speed, rig.velocity.y);
            transform.eulerAngles = new Vector2(0, 0);
        }
        else
        {
            rig.velocity = new Vector2(-Speed, rig.velocity.y);
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    public void OnHit()
    {
        health--;

        if (health <= 0)
        {
            Speed = 0f;
            anim.SetTrigger("Death");
            Destroy(gameObject, 1f);
        }
        else
        {
         
            anim.SetTrigger("Hit");
        }
    }

}
