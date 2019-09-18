using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/******************/
//Кнопка выхода
/******************/

public class QuitButton : ButtonBase
{
    public Text dishesToWin;
    
    public void Start()
    {
        SetUIController();
        NullCheck();
        OnCLick = uc.PressQuit;
        SetText(uc.dishesDelivered, uc.DISHES_TO_WIN);
    }

    protected override void NullCheck()
    {
        if (buttonNorm==null || buttonPress==null || uc==null || dishesToWin==null)
        {
            throw new System.NullReferenceException("Start button exception");
        }
    }

    //При загрузке отображает на экране количество отданных блюд
    public void SetText(int dishesDelivered, int DISHES_TOTAL)
    {
        dishesToWin.text = dishesDelivered.ToString() + "/" + DISHES_TOTAL.ToString();
    }
}
