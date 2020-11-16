using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Traps : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)//если попали в триггер ловушки
    {
        if (collision.gameObject.layer == 8)
        {
            Debug.Log("You Dead");//вызываем функцию проигрыша
            YouLoose.StartDying();
        }
    }
}
