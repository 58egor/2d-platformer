using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float HP=100f;
    public bool mage;
    public bool knight;
    public float maxTime;
    public GameObject fireball;
    public GameObject gunPoint;
    private SpriteRenderer sprite;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = maxTime;
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    public void Damage(float damage) {
        HP -= damage;
        if (HP <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (mage)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Fire();
                timer = maxTime;
            }
        }
    }
    public void Fire()
    {
        Vector3 position = transform.position;
        position.y += 1;
        GameObject newBullet = Instantiate(fireball, gunPoint.transform.position, fireball.transform.rotation);
        newBullet.GetComponentInChildren<SpriteRenderer>().flipX = sprite.flipX;
        newBullet.GetComponent<Bullet>().Damagelayer = 8;
        newBullet.GetComponent<Bullet>().speed = 15;
        newBullet.GetComponent<Bullet>().direction = newBullet.transform.right * (sprite.flipX ? -1.0f : 1.0f);
    }
}
