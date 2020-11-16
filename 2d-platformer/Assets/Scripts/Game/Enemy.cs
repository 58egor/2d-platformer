using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float HP=100f;//здоровье противника
    bool isDead = false;//мертв ли он
    public bool mage;//класс-маг
    public float speed = 15;
    public bool knight;//класс-рыцарь
    public float maxTime;//кд действие противника
    public float distance = 20;//расстояние при котором определяет героя
    public float range = 20;//радиус урона от рукопашки
    public GameObject fireball;//пуля
    public GameObject gunPoint;//место спавна пули если смотрим влево
    public GameObject gunPoint2;//место спавна пули если смотрим вправо
    private SpriteRenderer sprite;
    private Rigidbody2D rigibody;
    BoxCollider2D collider;
    bool isAttacking = false;//аттакуем?
    float timer=0;//таймер
    public LayerMask enemyLayer;//слой противника
    Animator animator;
    AudioManager manager;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.sortingOrder = 10;
        animator = GetComponent<Animator>();
        collider=GetComponent<BoxCollider2D>();
        rigibody = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<AudioManager>();
    }
    public void Damage(float damage,int TypeOfDamage) {
        if (!knight)
        {
            HP -= damage;//уменьшаем хп если это маг
            manager.Play("MageDies");
        }
        if((knight && TypeOfDamage == 1))//проверка что рыцарь и что урон от меча
        {
            HP -= damage;//если урон не от дальнего боя уменьшаем хп
            manager.Play("KnightDies");
        }
        else
        {
            animator.SetTrigger("Block");//если урон от выстрела то блокируем
            manager.Play("KnightBlock");
        }
        if (HP <= 0)//если хп кончилось
        {
            isDead = true;
            animator.SetTrigger("Die");//активируем анимацию смерти
            rigibody.constraints = RigidbodyConstraints2D.FreezePositionY;
            collider.enabled=false;//деактивируем коллайдер
        }
    }
    public void Die()
    {
        gameObject.SetActive(false);//если анимаци смерти кончилась то выключаем противника
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
            if (mage)//если маг
            {
                hit = Physics2D.RaycastAll(sprite.flipX ? gunPoint2.transform.position : gunPoint.transform.position, gunPoint.transform.right * (sprite.flipX ? 1.0f : -1.0f), distance);//определяем в какую сторону идет луч
            }
            if (knight)
            {
                hit = Physics2D.RaycastAll((sprite.flipX ? gunPoint.transform.position : gunPoint2.transform.position), gunPoint.transform.right * (sprite.flipX ? -1.0f : 1.0f), distance);//определяем в какую сторону луч
            }
            int targets = 0;
            for (int i = 0; i < hit.Length; i++)//проверяем цели в которые попали
            {
                Debug.Log(hit[i].collider.gameObject.name);
                if (hit[i].collider.gameObject.layer != 8)
                {
                    targets++;//если данная переменная равна длинне то мы не видим героя
                }
            }
            timer -= Time.deltaTime;//уменьшаем таймер
            if (timer <= 0 && targets != hit.Length && mage)//если это маг и видим героя
            {
                animator.SetTrigger("Fire");//запускаем анимацию выстрела
                timer = maxTime;//стартовое значение таймера
            }
            if (timer <= 0 && targets != hit.Length && knight)
            {
                animator.SetTrigger("Attack");
                timer = maxTime;//стартовое значение таймера
            }

            if (knight && isAttacking)//если это рыцарь и атакуем
            {
                Attack();//запуска проверку попали ли в героя
            }
        }
    }
    public void Fire()//функция создание огненного шара
    {
        manager.Play("MageShoot");
        GameObject newBullet = Instantiate(fireball, sprite.flipX ? gunPoint2.transform.position : gunPoint.transform.position, fireball.transform.rotation);//определяем точку выстрела
        newBullet.GetComponentInChildren<SpriteRenderer>().flipX = sprite.flipX;//определяем смотрим вправо или влево
        newBullet.GetComponent<Bullet>().Damagelayer = 8;//слой героя
        newBullet.GetComponent<Bullet>().speed = speed;//скорость пули
        newBullet.GetComponent<Bullet>().direction = newBullet.transform.right * (sprite.flipX ? 1.0f : -1.0f);//определяем сторону полета
    }
    public void Attack()//функция атаки
    {
        Collider2D[] hitEnemys = Physics2D.OverlapCircleAll((sprite.flipX ? gunPoint.transform.position : gunPoint2.transform.position), range, enemyLayer);//создаем круг
        foreach (Collider2D col in hitEnemys)//и смотрим попали ли в героя
        {
            Debug.Log("You Dead");
            YouLoose.StartDying();//вызываем функция смерти
        }
    }
    public void StopAttack()
    {
        isAttacking = false;//анимация удара закончилась
    }
    public void StartAttack()//анимация атаки началась
    {
        manager.Play("HitKnight");
        isAttacking = true;
        Collider2D[] hitEnemys = Physics2D.OverlapCircleAll((sprite.flipX ? gunPoint.transform.position : gunPoint2.transform.position), range, enemyLayer);
        foreach (Collider2D knight in hitEnemys)
        {
            Debug.Log("You Dead");
            YouLoose.StartDying();
        }
    }
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawSphere((sprite.flipX ? gunPoint.transform.position : gunPoint2.transform.position), range);
    //}
}
