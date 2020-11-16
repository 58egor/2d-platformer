using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class YouLoose
{
    static GameObject winWindow;
    static Animator animator;
    static Text loseText;
    static AudioManager manager;
    public static void StartDying()//вызываем анимацию смерти у игрока
    {
        manager.Stop("Background");
        manager.Play("KingDies");
        animator.SetTrigger("Die");
    }
    public static void EndGame()//открываем меню проигрыша
    {
        Time.timeScale = 0f;
        winWindow.SetActive(true);
        loseText.gameObject.SetActive(true);
    }
    public static void GetData(GameObject winWindow1, Text text1)//получаем нужные данные
    {
        winWindow = winWindow1;
        loseText = text1;
    }
    public static void GetAnimator(Animator animator1, AudioManager manager1)//получаем нужные данные
    {
        animator = animator1;
        manager = manager1;
    }
}
