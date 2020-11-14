using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class YouLoose
{
    static GameObject winWindow;
    static Text text;
    public static void EndGame()
    {
        Time.timeScale = 0f;
        winWindow.SetActive(true);
        text.text = "YOU DIED:\n";
    }
    public static void GetData(GameObject winWindow1, Text text1)
    {
        winWindow = winWindow1;
        text = text1;
    }
}
