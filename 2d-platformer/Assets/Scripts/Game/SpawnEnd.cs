using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SpawnEnd : MonoBehaviour
{
    int aliveTraitors = 0;
    public GameObject[] level0;//уровни 0
    public GameObject[] level0Enter;//входы в уровни 0
    public GameObject[] level1;//уровни 1
    public GameObject[] level1Enter;//входы в уровни 1
    public GameObject[] level2Enter;//входы в уровни 2
    public GameObject[] level2;//уровни 2
    public GameObject[] enemys;//все противники
    public GameObject winWindow;//окно выигрыша/проигрыша
    public Text winText;//текст при выигрыше
    public Text loseText;//текст при проигрыше
    float timer = 0;//время
    public Text info;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        YouLoose.GetData(winWindow, loseText);
        int i = UnityEngine.Random.Range(0, level0.Length);//рандомно выбираем один из 0 уровней
        level0[i].SetActive(true);//активирем его
        level0Enter[i].SetActive(false);//активируем вход,убирая преграду
        i = UnityEngine.Random.Range(0, level1.Length);//рандомно выбираем один из 1 уровней
        level1[i].SetActive(true);//активирем его
        level1Enter[i].SetActive(false);//активиреум вход к нему,убирая преграду
        i = UnityEngine.Random.Range(0, level2.Length);//рандомно выбираем один из 2 уровней
        level2[i].SetActive(true);//активирем его
        level2Enter[i].SetActive(false);//активиреум вход к нему,убирая преграду
        int size = 0;
        for (i = 0; i < enemys.Length; i++)//если противник не активен
        {
            if (!enemys[i].activeInHierarchy)//увеличиваем счетчик 
            {
                size++;
            }
        }
        aliveTraitors = enemys.Length - size;
        info.text = "Alive traitors:" + aliveTraitors;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        int size = 0;
        for(int i = 0; i <enemys.Length; i++)//если противник не активен
        {
            if (!enemys[i].activeInHierarchy)//увеличиваем счетчик 
            {
                size++;
            }
        }
        aliveTraitors -=aliveTraitors-(enemys.Length - size);
        info.text = "Alive traitors:" + aliveTraitors;
        if (size == enemys.Length)//если насчтили всех противниками неактивными то игрок выиграл
        {
            Debug.Log("GameOver");
            if (!winWindow.activeInHierarchy)
            {
                FindObjectOfType<AudioManager>().Stop("Background");
                FindObjectOfType<AudioManager>().Play("Win");
                Debug.Log("Size=" + size + "Length=" + enemys.Length);
                Time.timeScale = 0f;
                winWindow.SetActive(true);

                winText.text = "Your time:\n" + TimeSpan.FromSeconds((double)timer).ToString(@"mm\:ss");
                winText.gameObject.SetActive(true);
            }
        }
    }
}
