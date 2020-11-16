using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;//скорочть пули
    public Vector3 direction;//направление пули
    public float damage=20f;//урон от пули
    public int Damagelayer = 10;//слой который дамажим
    private void OnTriggerEnter2D(Collider2D collision)//если пуля попала в объект
    {
        Debug.Log(collision.gameObject.name+" "+ collision.gameObject.layer);
        if(collision.gameObject.layer == Damagelayer)//проверям слой в который попали
        {
            if (collision.gameObject.layer == 10)//это противник?
            {
                collision.gameObject.GetComponent<Enemy>().Damage(damage,0);//если да то вызываем функцию урона у него
            }
            else
            {
                if (collision.gameObject.layer == 11)//это противник для меню?
                {
                    collision.gameObject.GetComponent<EnemyMenu>().Damage(damage, 0);//да-вызываем функцию урона
                }
                else
                {
                    Debug.Log("You Dead");//если везде фалс то значит попали в игрок
                    YouLoose.StartDying();//вызываем функцию проигрыша
                }
            }
        }
        if (collision.gameObject.layer == Damagelayer || collision.gameObject.layer == 9)//удаляем пулу если попали в стену или в цель
        {
            Debug.Log("Fire11");
            Destroy(transform.gameObject);
        }
    }
    private void Awake()
    {
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);//двигаем пулю
    }
}
