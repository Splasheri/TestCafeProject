using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*********************/
//Кнопка старта игры
/*********************/

public class StartButton : ButtonBase
{
    public Text dishesToWin;
    public GameObject menu;

    public void Start()
    {
        OnCLick = uc.PressStart;
        OnCLick += DestroyMenu;
        NullCheсk();
        SetText(uc.DISHES_TO_WIN);
    }

    private void NullCheсk()
    {
        if (buttonNorm==null || buttonPress==null || uc==null || dishesToWin==null || menu==null)
        {
            throw new System.NullReferenceException("Start button exception");
        }
    }

    //При загрузке отображает на экране количество блюд, которые необходимо отдать
    public void SetText(int DISHES_TO_WIN)
    {
        dishesToWin.text += DISHES_TO_WIN.ToString();
    }
    
    //При нажатии - закрыть меню
    public void DestroyMenu()
    {
        GameObject.Destroy(menu);
    }
}
