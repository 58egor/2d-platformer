using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float swordDamage = 25f;
    public float speed = 5f;
    public float jumpForce = 15f;
    private Rigidbody2D rigibody;
    private SpriteRenderer sprite;
    public Joystick joystick;
    public GameObject bullet;
    public GameObject GunPoint;
    Animator animator;
    bool onGround;
    // Start is called before the first frame update
    void Start()
    {
        rigibody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    public void Jump()
    {
        if(onGround)
        rigibody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    public void Attack()
    {
        animator.SetBool("isAttacking", true);
    }
    public void StopAttack()
    {
        animator.SetBool("isAttacking", false);
    }
    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        CheckGround();
        Run();
    }
    void Run()
    {
        Vector3 direction = transform.right*joystick.Horizontal;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        if (joystick.Horizontal != 0)
        {
            animator.SetBool("isMoving", true);
            sprite.flipX = direction.x < 0.0f;
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
    public void StartFire()
    {
        animator.SetBool("isFire", true);
        //animator.SetBool("isFire", false);
    }
        public void Fire()
    {
        animator.SetBool("isFire", false);
        Vector3 position = transform.position;
        position.y += 1;
        GameObject newBullet = Instantiate(bullet, GunPoint.transform.position, bullet.transform.rotation);
        newBullet.GetComponent<Bullet>().direction = newBullet.transform.right * (sprite.flipX ? -1.0f : 1.0f);
    }
    void CheckGround()
    {
        Debug.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - 0.15f));
        RaycastHit2D[] hit;
        Ray ray = new Ray(transform.position, -transform.up);
        hit = Physics2D.RaycastAll(transform.position, -transform.up, 0.15f);
        int targets=0;
        for (int i = 0; i < hit.Length; i++)
        {
            Debug.Log(hit[i].collider.gameObject.name);
            if (hit[i].collider.gameObject.layer!=9)
            {
                targets++;
            }
        }
        if (targets == hit.Length)
        {
            onGround = false;
        }
        else
        {
            onGround = true;
        }
    }
}
