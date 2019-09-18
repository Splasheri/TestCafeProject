using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************/
//Класс для управления жаркой блюд
//Имплементирует интерфейс IController
/************************************/

public class PansController : MonoBehaviour, IController
{
    public List<PanView> meatPans;
    public List<PanView> sausagePans;
    public DesksController DesksManager;

    //В начале игры проверка на null
    public void StartGame()
    {
        NullCheck();
    }

    //В конце игры убирается мясо со всех блюд
    public void EndGame()
    {
        foreach (var meatPan in meatPans)
        {
            meatPan.RemoveMeat();
        }
        foreach (var sausagePan in sausagePans)
        {
            sausagePan.RemoveMeat();
        }
    }

    public void NullCheck()
    {
        if (meatPans.Count==0 || sausagePans.Count==0 || DesksManager==null)
        {
            throw new System.NullReferenceException("Pans controller exception");
        }
    }

    //Положить котлету на первую пустую сковороду
    public void FryMeat()
    {
        foreach (var pan in meatPans)
        {
            if (pan.isEmpty)
            {
                pan.StartFrying();
                return;
            }
        }
    }

    //Положить сосиску на первую пустую сковороду
    public void FrySausage()
    {
        foreach (var pan in sausagePans)
        {
            if (pan.isEmpty)
            {
                pan.StartFrying();
                return;
            }
        }
    }

    //Убрать мясо с сковороды thisPan
    //Если это не было двойное нажатие(убрать сгоревшее мясо), то попытаться добавить мясо в булку
    public void RemoveMeat(PanView thisPan, bool doubleClick)
    {
        if (doubleClick)
        {
            thisPan.RemoveMeat();
        }
        else
        {
            if (AddMeatToDish(thisPan))
            {
                thisPan.RemoveMeat();
            }
        }
    }

    //В зависимости от типа мяса в pan - попытаться положить мясо в соответствующую булку
    public bool AddMeatToDish(PanView pan)
    {
        if (sausagePans.Contains(pan))
        {
            return DesksManager.AddSausage();
        }
        else
        {
            return DesksManager.AddMeat();
        }
    }

}
