using UnityEngine;
using UnityEngine.UI;

/**********************************************/
//Класс для управления игровыми меню и HUD
//Имплементирует интерфейс IController
/**********************************************/
public class UIController : MonoBehaviour, IController
{
    private bool gameStarted;
    public GameManager gm;
    public Transform MenuHandler;
    public GameObject LoseMenu;
    public GameObject WinMenu;
    public Text  textPlayerProgress;
    public Image imagePlayerProgress;

    public int DISHES_TO_WIN    { get { return gm.DISHES_TO_WIN; } }
    public int dishesDelivered  { get { return gm.dishesDelivered; } }
    public int DISHES_TOTAL     { get { return gm.DISHES_TOTAL; } }

    //Установка переменной, обозначающей начало игры в true
    //Первое обновление полоски прогресса
    public void StartGame()
    {
        gameStarted = true;
        UpdateProgress();
    }

    //Установка переменной, обозначающей начало игры в false
    //В зависимости от переменной win в GamManager отображает одно из меню
    public void EndGame()
    {
        gameStarted = false;
        if (gm.win)
        {
            GameObject.Instantiate(WinMenu,MenuHandler);
        }
        else
        {
            GameObject.Instantiate(LoseMenu, MenuHandler);
        }
    }

    public void NullCheck()
    {
        if (gm == null || MenuHandler == null || LoseMenu == null || WinMenu == null ||
            textPlayerProgress == null || imagePlayerProgress == null)
        {
            throw new System.NullReferenceException("HUDController exception ");
        }
    }

    //Установка переменной, обозначающей начало игры в false
    //Проверка на null
    private void Start()
    {
        NullCheck();
        gameStarted = false;
    }

    //Каждый кадр обновлять полоску прогресса
    private void Update()
    {
        if (gameStarted)
        {
            UpdateProgress();
        }
    }

    //Заполнение полоски прогресса и текстовое отображение прогресса
    private void UpdateProgress()
    {
        imagePlayerProgress.fillAmount = (float)gm.dishesDelivered / (float)gm.DISHES_TOTAL;
        textPlayerProgress.text = gm.dishesDelivered.ToString() + "/" + gm.DISHES_TOTAL.ToString();
    }

    //В зависимости от нажатой в меню кнопки
    //Выполнение одной из функций GameManager
    public void PressStart()
    {
        gm.StartGame();        
    }
    public void PressRestart()
    {
        gm.RestartGame();
    }
    public void PressQuit()
    {
        Application.Quit();
    }
        
}
