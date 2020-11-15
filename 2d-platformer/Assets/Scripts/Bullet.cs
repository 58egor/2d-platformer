using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;
    public Vector3 direction;
    public float damage=20f;
    public int Damagelayer = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name+" "+ collision.gameObject.layer);
        if(collision.gameObject.layer == Damagelayer)
        {
            if (collision.gameObject.layer == 10)
            {
                collision.gameObject.GetComponent<Enemy>().Damage(damage,0);
            }
            else
            {
                Debug.Log("You Dead");
                YouLoose.StartDying();
            }
        }
        if (collision.gameObject.layer == Damagelayer || collision.gameObject.layer == 9)
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
