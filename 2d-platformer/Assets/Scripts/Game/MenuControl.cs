using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    // Start is called before the first frame update
    public void Restart()//кнопка перезапуска
    {
        SceneManager.LoadScene(1);
    }
    public void Menu()//кнопка возврата в меню
    {
        SceneManager.LoadScene(0);
    }
    public void Exit()//кнопка выхода
    {
        Application.Quit();
    }
}
