using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float HP=100f;
    bool isDead = false;
    public bool mage;
    public bool knight;
    public float maxTime;
    public float distance = 20;
    public float range = 20;
    public GameObject fireball;
    public GameObject gunPoint;
    public GameObject gunPoint2;
    private SpriteRenderer sprite;
    private Rigidbody2D rigibody;
    BoxCollider2D collider;
    bool isAttacking = false;
    bool king = false;
    float timer=0;
    public LayerMask enemyLayer;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.sortingOrder = 10;
        animator = GetComponent<Animator>();
        collider=GetComponent<BoxCollider2D>();
        rigibody = GetComponent<Rigidbody2D>();
    }
    public void Damage(float damage,int TypeOfDamage) {
        if (!knight)
        {
            HP -= damage;
        }
        if((knight && TypeOfDamage == 1))
        {
            HP -= damage;
        }
        else
        {
            animator.SetTrigger("Block");
        }
        if (HP <= 0)
        {
            isDead = true;
            animator.SetTrigger("Die");
            rigibody.constraints = RigidbodyConstraints2D.FreezePositionY;
            collider.enabled=false;
        }
    }
    public void Die()
    {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (mage)
            {
                Vector3 vec = gunPoint.transform.right * (sprite.flipX ? 1.0f : -1.0f);
                vec.x += distance * (sprite.flipX ? 1.0f : -1.0f);
                Debug.DrawRay(sprite.flipX ? gunPoint2.transform.position : gunPoint.transform.position, vec);
            }
            if (knight)
            {
                Vector3 vec = gunPoint.transform.right * (sprite.flipX ? -1.0f : 1.0f);
                vec.x += distance * (sprite.flipX ? -1.0f : 1.0f);
                Debug.DrawRay(sprite.flipX ? gunPoint.transform.position : gunPoint2.transform.position, vec);
            }
            RaycastHit2D[] hit = null;
            if (mage)
            {
                hit = Physics2D.RaycastAll(sprite.flipX ? gunPoint2.transform.position : gunPoint.transform.position, gunPoint.transform.right * (sprite.flipX ? 1.0f : -1.0f), distance);
            }
            if (knight)
            {
                hit = Physics2D.RaycastAll((sprite.flipX ? gunPoint.transform.position : gunPoint2.transform.position), gunPoint.transform.right * (sprite.flipX ? -1.0f : 1.0f), distance);
            }
            int targets = 0;
            for (int i = 0; i < hit.Length; i++)
            {
                Debug.Log(hit[i].collider.gameObject.name);
                if (hit[i].collider.gameObject.layer != 8)
                {
                    targets++;
                }
            }
            if (targets != hit.Length)
            {
                king = true;
            }
            else
            {
                king = false;
            }
            timer -= Time.deltaTime;
            if (timer <= 0 && targets != hit.Length && mage)
            {
                animator.SetTrigger("Fire");
                timer = maxTime;
            }
            if (timer <= 0 && targets != hit.Length && knight)
            {
                animator.SetTrigger("Attack");
                timer = maxTime;
            }

            if (knight && isAttacking)
            {
                Attack();
            }
        }
    }
    public void Fire()
    {
        Vector3 position = transform.position;
        position.y += 1;
        GameObject newBullet = Instantiate(fireball, sprite.flipX ? gunPoint2.transform.position : gunPoint.transform.position, fireball.transform.rotation);
        newBullet.GetComponentInChildren<SpriteRenderer>().flipX = sprite.flipX;
        newBullet.GetComponent<Bullet>().Damagelayer = 8;
        newBullet.GetComponent<Bullet>().speed = 15;
        newBullet.GetComponent<Bullet>().direction = newBullet.transform.right * (sprite.flipX ? 1.0f : -1.0f);
    }
    public void Attack()
    {
        Collider2D[] hitEnemys = Physics2D.OverlapCircleAll((sprite.flipX ? gunPoint.transform.position : gunPoint2.transform.position), range, enemyLayer);
        foreach (Collider2D knight in hitEnemys)
        {
            Debug.Log("You Dead");
            YouLoose.StartDying();
        }
    }
    public void StopAttack()
    {
        isAttacking = false;
    }
    public void StartAttack()
    {
        isAttacking = true;
        Collider2D[] hitEnemys = Physics2D.OverlapCircleAll((sprite.flipX ? gunPoint.transform.position : gunPoint2.transform.position), range, enemyLayer);
        foreach (Collider2D knight in hitEnemys)
        {
            Debug.Log("You Dead");
            YouLoose.StartDying();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere((sprite.flipX ? gunPoint.transform.position : gunPoint2.transform.position), range);
    }
}
