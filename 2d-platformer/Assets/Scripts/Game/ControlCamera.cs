﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public float speed = 2.0f;
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = player.transform.position;//получаем позицию игрока
        pos.z = transform.position.z;//сохраняем старое значение з
        //transform.position =Vector3.Lerp(transform.position,pos,speed*Time.deltaTime);
        transform.position = pos;//передвигаем камеру
    }
}
