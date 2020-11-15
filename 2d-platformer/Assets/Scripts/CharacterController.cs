using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float swordDamage = 25f;
    bool attackActive=false;
    public float speed = 5f;
    public float jumpForce = 15f;
    private Rigidbody2D rigibody;
    private SpriteRenderer sprite;
    public Joystick joystick;
    public GameObject bullet;
    public GameObject GunPoint;
    Animator animator;
    public float range;
    public Transform attackPoint;
    public Transform attackPoint2;
    public LayerMask enemyLayer;
    bool onGround;
    // Start is called before the first frame update
    void Start()
    {
        rigibody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        YouLoose.GetAnimator(animator);
    }
    public void Jump()
    {
        if (onGround)
        {
            animator.SetTrigger("Jump");
            rigibody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    public void DieAlready()
    {
        YouLoose.EndGame();
    }
    public void Attack()
    {
        animator.SetTrigger("Attack");
        attackActive = true;
 
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere((sprite.flipX ? attackPoint2.position : attackPoint.position), range);
    }
    public void StopAttack()
    {
        attackActive = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (attackActive)
        {
            Collider2D[] hitEnemys = Physics2D.OverlapCircleAll((sprite.flipX ? attackPoint2.position : attackPoint.position), range, enemyLayer);
            foreach (Collider2D enemy in hitEnemys)
            {
                enemy.GetComponent<Enemy>().Damage(swordDamage,1);
            }
        }
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
        animator.SetTrigger("Shoot");
        //animator.SetBool("isFire", false);
    }
        public void Fire()
    {
        Vector3 position = transform.position;
        position.y += 1;
        GameObject newBullet = Instantiate(bullet, GunPoint.transform.position, bullet.transform.rotation);
        newBullet.GetComponentInChildren<SpriteRenderer>().flipX = sprite.flipX;
        newBullet.GetComponent<Bullet>().direction = newBullet.transform.right * (sprite.flipX ? -1.0f : 1.0f);
    }
    void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        onGround = colliders.Length > 1;
    }
}
