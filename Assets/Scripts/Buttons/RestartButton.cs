using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

/******************/
//Кнопка рестарта
/******************/

public class RestartButton : ButtonBase
{
    private GameObject menu;

    public void Start()
    {
        menu = this.transform.parent.gameObject;
        SetUIController();
        NullCheck();
        OnCLick = uc.PressRestart;
        OnCLick += DestroyMenu;
    }

    protected override void NullCheck()
    {
        if (buttonNorm==null || buttonPress==null || uc==null || menu==null)
        {
            throw new System.NullReferenceException("Start button exception");
        }
    }
    
    //При нажатии - закрывает меню
    public void DestroyMenu()
    {
        GameObject.Destroy(menu);
    }
}
