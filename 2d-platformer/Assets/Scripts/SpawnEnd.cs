using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SpawnEnd : MonoBehaviour
{
    public GameObject[] level0;
    public GameObject[] level0Enter;
    public GameObject[] level1;
    public GameObject[] level1Enter;
    public GameObject[] level2Enter;
    public GameObject[] level2;
    public GameObject[] enemys;
    public GameObject winWindow;
    public Text text;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        YouLoose.GetData(winWindow, text);
        int i = UnityEngine.Random.Range(0, level0.Length);
        level0[i].SetActive(true);
        level0Enter[i].SetActive(false);
        i = UnityEngine.Random.Range(0, level1.Length);
        level1[i].SetActive(true);
        level1Enter[i].SetActive(false);
        i = UnityEngine.Random.Range(0, level2.Length);
        level2[i].SetActive(true);
        level2Enter[i].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        int size = 0;
        for(int i = 0; i <enemys.Length; i++)
        {
            if (!enemys[i].activeInHierarchy)
            {
                size++;
            }
        }
        if (size == enemys.Length)
        {
            Debug.Log("GameOver");
            if (!winWindow.activeInHierarchy)
            {
                Debug.Log("Size=" + size + "Length=" + enemys.Length);
                Time.timeScale = 0f;
                winWindow.SetActive(true);
                
                text.text = "Your time:\n" + TimeSpan.FromSeconds((double)timer).ToString(@"mm\:ss"); 
            }
        }
    }
}
