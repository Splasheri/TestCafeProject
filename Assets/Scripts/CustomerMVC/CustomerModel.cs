using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************/
//Модель посетителя
//Генерируется при запуске игры
/*********************/

public class CustomerModel 
{
    public int skin;
    public List<int> order;
    
    public CustomerModel()
    {
        order = new List<int>();
    }

    //Создать модель, исходя из переданных аргументов - количества скинов персонажей, количества блюд в заказе, количества блюд
    public CustomerModel Create(int max_skin, int max_dish, int max_dish_types)
    {
        skin     =  GenerateNumber(max_skin);
        order    =  GenerateList(max_dish, max_dish_types);
        return this;
    }

    //Сгенерировать номер блюда от 1 до max
    private int GenerateNumber(int max)
    {
        return Random.Range(1,max+1);
    }

    //Сгенерировать список блюд длиной max_length, номера блюд от 1 до max_types
    private List<int> GenerateList(int max_length, int max_types)
    {
        List<int> numberList = new List<int>();
        int length = GenerateNumber(max_length);
        for (int i = 0; i < length; i++)
        {
            numberList.Add(GenerateNumber(max_types));
        }

        return numberList;
    }
}
