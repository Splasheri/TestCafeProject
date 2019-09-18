using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************/
//Класс, хранящий игровые константы
//В начале игры генерируется список посетителей методом GenerateVisitorList
/************************************/

public class GameData : MonoBehaviour
{
    public const int    VISITORS_NUMBER     = 15;
    public const float  NEW_VISITOR_PERIOD  = 3;
    public const int    MAX_VISITORS        = 4;
    public const float  VISITOR_WAIT_TIME   = 18;
    public const float  VISITOR_WAIT_ADD    = 6;
    public const int    DISHES_MAY_LOST     = 2;
    public int          DISHES_TOTAL        { get; private set; }
    public int          DISHES_TO_WIN       { get; private set; }    

    public const float  FRY_TIME            = 5;
    public const float  BURN_TIME           = 7;

    public const float  COLA_POUR_TIME      = 5;

    public const int    CUSTOMER_SKINS_NUM  = 4;
    public const int    DISHES_TYPES_NUM    = 3;
    public const int    MAX_DISHES_ORDERED  = 3;     

    public const float  DOUBLE_CLICK_TIME   = 0.2f;

    public List<CustomerModel> customers;

    public void Awake()
    {
        GenerateVisitorList();
    }

    protected void GenerateVisitorList()
    {
        DISHES_TOTAL = 0;
        customers = new List<CustomerModel>();
        for (int i = 0; i < VISITORS_NUMBER; i++)
        {
            var newCustomer = new CustomerModel().Create
              (CUSTOMER_SKINS_NUM, MAX_DISHES_ORDERED, DISHES_TYPES_NUM);
            DISHES_TOTAL += newCustomer.order.Count;
            customers.Add(newCustomer);
        }

        DISHES_TO_WIN = DISHES_TOTAL - DISHES_MAY_LOST;
    }

}
