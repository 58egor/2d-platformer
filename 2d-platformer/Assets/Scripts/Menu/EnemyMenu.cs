using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMenu : MonoBehaviour
{
    Animator animator;
    public bool mage = false;
    public float time;
    float timer;
    public GameObject fireball;
    public GameObject gunPoint;
    AudioManager manager;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        manager = FindObjectOfType<AudioManager>();
        timer = time;
    }
    public void Damage(float damage, int TypeOfDamage)
    {
        manager.Play("KnightBlock");
      animator.SetTrigger("Menu");
        
    }
    public void SwordSound()
    {
        manager.Play("HitKnight");
    }
    // Update is called once per frame
    void Update()
    {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (mage)
                {
                    animator.SetTrigger("Fire");
                    
                }
            timer = time;
        }
    }
    public void Fire()
    {
        manager.Play("MageShoot");
        Vector3 position = transform.position;
        position.y += 1;
        GameObject newBullet = Instantiate(fireball, gunPoint.transform.position, fireball.transform.rotation);
        //newBullet.GetComponentInChildren<SpriteRenderer>().flipX = sprite.flipX;
        newBullet.GetComponent<Bullet>().Damagelayer = 8;
        newBullet.GetComponent<Bullet>().speed = 15;
        newBullet.GetComponent<Bullet>().direction = newBullet.transform.right * -1;
    }
}
