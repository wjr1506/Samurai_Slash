using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public float AtkRadius;
    public float recoverTime;
    public float Health;
    public Image healfBar;

    float recoverCout;
    bool isJumpig;
    bool isAtak;
    bool isDeath;

    public Rigidbody2D rig;
    public Animator anim;
    public Transform FirePoint;
    public LayerMask EnemyLayer;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(isDeath == false)
        {
            Jump();
            OnAtak();
        }
    }

    void FixedUpdate()
    {
        if (isDeath == false)
        {
            onMove();
        }
    }

    public void OnHit(float damage)
    {
        recoverCout += Time.deltaTime;
        if(recoverCout >= recoverTime && isDeath == false)
        {

            
            anim.SetTrigger("Hit");
            Health -= damage;
            
            
            healfBar.fillAmount = Health / 100;
            gameOver();
            recoverCout = 0f;
        }
    }
    void gameOver()
    {
        if(Health <= 0)
        {
            anim.SetTrigger("Death");
            isDeath = true;
        }
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isJumpig == false)
            {
                rig.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                anim.SetInteger("Transition", 2);
                isJumpig = true;
            }
        }
    }
    void OnAtak()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isAtak = true;
            anim.SetInteger("Transition", 3);

            StartCoroutine(OnAtaking());

            Collider2D hit = Physics2D.OverlapCircle(FirePoint.position, AtkRadius, EnemyLayer);

            if (hit != null)
            {
                hit.GetComponent<FlyEnem>().OnHit();
            }
        }
    }

    void onMove()
    {
        float direction = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(direction * Speed, rig.velocity.y);

        if (direction > 0 && isJumpig == false && isAtak == false)
        {
            transform.eulerAngles = new Vector2(0, 0);
            anim.SetInteger("Transition", 1);
        }
        if (direction < 0 && isJumpig == false && isAtak == false)
        {
            transform.eulerAngles = new Vector2(0, 180);
            anim.SetInteger("Transition", 1);
        }
        if (direction == 0 && isJumpig == false && isAtak == false)
        {
            anim.SetInteger("Transition", 0);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(FirePoint.position, AtkRadius);
    }

    IEnumerator OnAtaking()
    {
        yield return new WaitForSeconds(0.5f);
        isAtak = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumpig = false;
        }
    }



}