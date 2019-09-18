using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/************************************/
//Класс, реализующий важнейшую логику игры
//В списке gameControllers находятся все контроллеры игры
/************************************/

public class GameManager : GameData, IController
{
    public bool win { get; private set; }
    public List<IController> gameControllers;
    public const int NUM_OF_CONTROLLERS = 5; //Customers, desks, pans, cola, hud
    public int dishesDelivered;

    //Инициализация списка контроллеров и проверка на null
    private void Start()
    {
        gameControllers = new List<IController>();
        SetControllerList();
        NullCheck();
    }

    //Заполнение списка контроллеров, учитывая, что все контроллеры распологаются в дочерных объектах GameManager
    private void SetControllerList()
    {
        var controllerList = this.transform.GetComponentsInChildren<IController>();
        foreach (var controller in controllerList)
        {
            if (controller!=this)
            {
                gameControllers.Add(controller);
            }
        }
    }

    //Запуск игры - выполнение метода StartGame у всех контроллеров
    public void StartGame()
    {
        dishesDelivered = 0;
        foreach (var controller in gameControllers)
        {
            controller.StartGame();
        }

    }

    //Перезапуск игры - генерация нового списка покупателей и запуск игры
    public void RestartGame()
    {
        GenerateVisitorList();
        StartGame();
    }

    //Запуск игры - выполнение метода EndGame у всех контроллеров
    //Установка переменной win в зависимости от количества отданных блюд
    public void EndGame()
    {
        if (dishesDelivered>=DISHES_TO_WIN)
        {
            win = true;
        }
        else
        {
            win = false;
        }

        foreach (var controller in gameControllers)
        {
            controller.EndGame();
        }
    }

    public void NullCheck()
    {
        if (gameControllers.Count < NUM_OF_CONTROLLERS)
        {
            throw new System.NullReferenceException("GameManager exception");
        }
    }

    //Подтвердить доставку блюда - увеличить на 1 dishesDelivered
    public void ConfirmDelivery()
    {
        dishesDelivered++;
    }

}
