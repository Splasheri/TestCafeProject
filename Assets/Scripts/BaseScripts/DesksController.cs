using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************/
//Класс для управления досками для блюд
//Имплементирует интерфейс IController
/************************************/

public class DesksController : MonoBehaviour, IController
{
    public List<Burger> burgerList;
    public List<Hotdog> hotdogList;
    public CustomersController customersManager;

    //В начале игры проверяет на null
    public void StartGame()
    {
        NullCheck();
    }

    //В конце игры устанавливает все блюда в состояние "только доска"
    public void EndGame()
    {
        foreach (var burger in burgerList)
        {
            burger.ChangeToDesk();
        }
        foreach (var hotdog in hotdogList)
        {
            hotdog.ChangeToDesk();
        }
    }

    public void NullCheck()
    {
        if (burgerList.Count==0 || hotdogList.Count==0 || customersManager==null)
        {
            throw new System.NullReferenceException("Desk controller exception");
        }
    }

    //Добавляет сосиску в первую булку для хотдогов и возвращает true
    //Если нет пустых булок - возвращает false
    public bool AddSausage()
    {
        foreach (var hotdog in hotdogList)
        {
            if (hotdog.dishPhase == DishPhase.onlyBread)
            {
                hotdog.ChangeToMeat();
                return true;
            }
        }
        return false;
    }

    //Добавляет булку для хотдогов на первую попавшеюся пустую доску
    public void AddHotdogBread()
    {
        foreach (var hotdog in hotdogList)
        {
            if (hotdog.dishPhase == DishPhase.emptyDesk)
            {
                hotdog.ChangeToBread();
                return;
            }
        }
    }

    //Добавляет котлету в первую булку для бургеров и возвращает true
    //Если нет пустых булок - возвращает false
    public bool AddMeat()
    {
        foreach (var burger in burgerList)
        {
            if (burger.dishPhase == DishPhase.onlyBread)
            {
                burger.ChangeToMeat();
                return true;
            }
        }
        return false;
    }

    //Добавляет булку для бургеров на первую попавшеюся пустую доску
    public void AddBurgerBread()
    {
        foreach (var burger in burgerList)
        {
            if (burger.dishPhase == DishPhase.emptyDesk)
            {
                burger.ChangeToBread();
                return;
            }
        }
    }

    //Отдать бургер покупателю
    //Проверяет бургеры до первого готового и пытается отдать методом TryDeliverDish
    public void TryRemoveBurger(Burger burger)
    {
        if (burger.dishPhase == DishPhase.withMeat)
        {
            if (customersManager.TryDeliverDish(DishType.Burger))
            {
                burger.ChangeToDesk();
            }
        }
    }

    //Отдать хотдог покупателю
    //Проверяет хотдоги до первого готового и пытается отдать методом TryDeliverDish
    public void TryRemoveHotdog(Hotdog hotdog)
    {
        if (hotdog.dishPhase == DishPhase.withMeat)
        {
            if (customersManager.TryDeliverDish(DishType.HotDog))
            {
                hotdog.ChangeToDesk();
            }
        }
        
    }
}
