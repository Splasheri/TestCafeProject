using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************/
//Класс для управления диспенсером
//Имплементирует интерфейс IController
/************************************/
public class ColaController : MonoBehaviour, IController
{
    public Timer timer;
    public TimerView counter;
    public List<Glass> glassesList;
    public CustomersController customersManager;

    //В начале игры запускает таймер и проверяет на null
    public void StartGame()
    {
        NullCheck();
        timer.Set(GameData.COLA_POUR_TIME, TryFillGlass, counter);
    }

    //В конце игры устанавливает все стаканы в "пусто" и останавливает таймер
    public void EndGame()
    {
        timer.Reset();
        foreach (var glass in glassesList)
        {
            if (glass.currentState != Glass.state.empty)
            {
                glass.ChangeState();
            }
        }
    }

    public void NullCheck()
    {
        if (timer==null || counter==null || glassesList.Count==0 || customersManager==null)
        {
            throw new System.NullReferenceException("ColaMachine controller exception");
        }
    }

    //Попытаться наполнить стакан
    //Проверяет стаканы до первого и наливает
    public void TryFillGlass()
    {
        foreach (var glass in glassesList)
        {
            if (glass.currentState == Glass.state.empty)
            {
                glass.ChangeState();
                timer.Set(GameData.COLA_POUR_TIME, TryFillGlass, counter);
                return;
            }
        }
    }

    //Отдать стакан посетителю
    //Проверяяет стаканы до первого наполненного и пытается отдать его методом TryDeliverDish
    public void RemoveGlass()
    {
        foreach (var glass in glassesList)
        {
            if (glass.currentState == Glass.state.full)
            {
                if (customersManager.TryDeliverDish(DishType.Cola))
                {
                    glass.ChangeState();
                    if (timer.isStoped)
                    {
                        timer.Set(GameData.COLA_POUR_TIME, TryFillGlass, counter);
                    }
                    return;
                }
            }
        }
    }
}
