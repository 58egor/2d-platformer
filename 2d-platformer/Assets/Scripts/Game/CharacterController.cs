using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float swordDamage = 25f;//урон от меча
    bool attackActive=false;//активна ли анимация атаки мечом
    public float speed = 5f;//скорость героя
    float speedX = 0;//скорость которую будем передавать
    public float jumpForce = 15f;//сила прыжка
    private Rigidbody2D rigibody;
    private SpriteRenderer sprite;
    public GameObject bullet;//пуля
    public GameObject GunPoint;//точка для выстрела пулей
    Animator animator;
    public float range;//радиус атаки меча
    public Transform attackPoint;//точка удара меча если смотрим врпаво
    public Transform attackPoint2;//точка удара меча если смотрим влево
    public LayerMask enemyLayer;//слой противника
    bool onGround;//на земле?
    AudioManager manager;
    // Start is called before the first frame update
    void Start()
    {
        rigibody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        manager = FindObjectOfType<AudioManager>();
        YouLoose.GetAnimator(animator,manager);
        manager.Play("Background");
    }
    public void Jump()//кнопка прыжка
    {
        if (onGround)//если на земле
        {
            manager.Play("KingJump");
            animator.SetTrigger("Jump");//вызываем анимацию прыжка
            rigibody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);//пинаем вверх
        }
    }
    public void DieAlready()//вызов окна проигрыша
    {
        YouLoose.EndGame();
    }
    public void Attack()//активируем анимацию атаки
    {
        animator.SetTrigger("Attack");
        manager.Play("Hit");
        attackActive = true;
 
    }
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawSphere((sprite.flipX ? attackPoint2.position : attackPoint.position), range);
    //}
    public void StopAttack()//атака закончилась
    {
        attackActive = false;
    }
    public void RightButton()//нажали кнопку вправо
    {
        animator.SetBool("isMoving", true);
        speedX = speed;
        sprite.flipX = false;
    }
    public void LefttButton()//нажали кнопку влево
    {
        animator.SetBool("isMoving", true);
        speedX = -speed;
        sprite.flipX = true;
    }
    public void StopMoving()//отпустили кнопку движения
    {
        speedX = 0;
        animator.SetBool("isMoving",false);
    }
    // Update is called once per frame
    void Update()
    {
        if (attackActive)//если атака активна
        {
            Collider2D[] hitEnemys = Physics2D.OverlapCircleAll((sprite.flipX ? attackPoint2.position : attackPoint.position), range, enemyLayer);//создаем круг
            foreach (Collider2D enemy in hitEnemys)//и все противники которые попали записываем в массив и вызываем функцию урона у них
            {
                manager.Play("HitDamage");
                enemy.GetComponent<Enemy>().Damage(swordDamage,1);
            }
        }
    }
    private void FixedUpdate()
    {
        CheckGround();//проверям на земле ли
        transform.Translate(speedX, 0, 0);//двигаем героя
        //Run();
    }
    //void Run()
    //{
    //    Vector3 direction = transform.right*joystick.Horizontal;
    //    transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    //    if (joystick.Horizontal != 0)
    //    {
    //        animator.SetBool("isMoving", true);
    //        sprite.flipX = direction.x < 0.0f;
    //    }
    //    else
    //    {
    //        animator.SetBool("isMoving", false);
    //    }
    //}
    public void StartFire()//начать выстрел
    {
        animator.SetTrigger("Shoot");
    }
        public void Fire()//генерируем пулю
    {
        manager.Play("KingShoot");
        GameObject newBullet = Instantiate(bullet, GunPoint.transform.position, bullet.transform.rotation);//иницилизируем пулю
        newBullet.GetComponentInChildren<SpriteRenderer>().flipX = sprite.flipX;//устанавливаем в какую сторону смотрит
        newBullet.GetComponent<Bullet>().direction = newBullet.transform.right * (sprite.flipX ? -1.0f : 1.0f);//устанавливаем сторону полета
    }
    void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);//создаем круг
        onGround = colliders.Length > 1;//если в нем кто то есть то на чем то стоим
    }
}
