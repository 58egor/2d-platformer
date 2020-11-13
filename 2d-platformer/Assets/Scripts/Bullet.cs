using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;
    public Vector3 direction;
    public float damage=20f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name+" "+ collision.gameObject.layer);
        if(collision.gameObject.layer == 10)
        {
            collision.gameObject.GetComponent<Enemy>().Damage(damage);
        }
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 9)
        {
            Debug.Log("Fire");
            Destroy(transform.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }
}
