using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMenu : MonoBehaviour
{
    Animator animator;
    public float time;
    float timer;
    public GameObject bullet;
    public GameObject gunPoint;
    AudioManager manager;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        manager = FindObjectOfType<AudioManager>();
        animator = GetComponent<Animator>();
        timer = time;
        manager.Play("Background2");
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            animator.SetTrigger("Shoot");
            timer = time;
        }
    }
    public void Fire()
    {
        manager.Play("KingShoot");
        Vector3 position = transform.position;
        position.y += 1;
        GameObject newBullet = Instantiate(bullet, gunPoint.transform.position, bullet.transform.rotation);
        newBullet.GetComponent<Bullet>().Damagelayer = 11;
        newBullet.GetComponent<Bullet>().direction = newBullet.transform.right;
    }
}
