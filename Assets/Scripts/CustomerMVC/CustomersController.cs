using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************/
//Класс для управления посетителями
//Имплементирует интерфейс IController
/************************************/

public class CustomersController : MonoBehaviour, IController
{
    public GameManager gm;
    public List<CustomerView> customerViews;
    public List<CustomerModel> customerModels;     
    public bool GameOver { get { return EndGameCheck(); } }
    private CustomerView currentCustomer;

    //На старте список моделей получается от GameManager, где он был сгенерирован
    public void StartGame()
    {
        customerModels = gm.customers;
        NullCheck();
        StartTimer(customerViews[0]);
    }

    //В конце игры все запущенные таймеры на этом объекте выключаются
    public void EndGame()
    {
        customerModels.Clear();
        foreach (var timer in GetComponents<CustomerTimer>())
        {
            timer.Off();
        }
    }

    public void NullCheck()
    {
        if (gm==null || customerViews.Count<GameData.MAX_VISITORS || customerModels.Count==0)
        {
            throw new System.NullReferenceException("Customers controller exception");
        }
    }

    //К этому объекту добавляется таймер, отображающий нового покупателя во view
    public void StartTimer(CustomerView view)
    {
        var timer = this.gameObject.AddComponent<CustomerTimer>();
        timer.Set(GameData.NEW_VISITOR_PERIOD, AddCustomer, view);
    }

    //Отобразить покупателя во view, если там уже не отображен покупатель
    //И если в списке моделей еще есть модели покупателей.
    //Если моделей не осталось - проверить на конец игры
    //После отображения можели покупателя - сортировка всех по оставшемуся времени до окончания ожидания
    //После сортировки - попытка найти новый пустой view
    private void AddCustomer(CustomerView view)
    {
        if (!view.Active)
        {
            if (customerModels.Count>0)
            {
                view.CreateView(customerModels[0]);
                customerModels.RemoveAt(0);
                SortCustomers();
                TryStartTimer();
            }
            else
            {
                if (GameOver)
                {
                    gm.EndGame();
                }
            }
        }
    }

    public void TryStartTimer()
    {
        foreach (var view in customerViews)
        {
            if (!view.Active)
            {
                StartTimer(view);                
                return;
            }
        }
    }

    //Проверка на конец игры
    //Если не осталось в списке моделей игры и все view не активны
    private bool EndGameCheck()
    {
        if (customerModels.Count==0)
        {
            foreach (var customer in customerViews)
            {
                if (customer.Active)
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }

    //Попытка доставить еду
    //Для первого найденного покупателя с указанным типом блюд в заказе вызывается TryRemoveDish
    //После чего выполняется сортировка и подтверждение доставки блюда в GameManager
    public bool TryDeliverDish(DishType dish)
    {
        foreach (var customer in customerViews)
        {
            if (customer.Active && customer.TryRemoveDish(dish))
            {
                SortCustomers();
                gm.ConfirmDelivery();
                return true;
            }
        }
        return false;
    }

    //Сортировка списка view
    //Функция сравнения выполнена лямда-выражением
    //Сравнение выполняется на основе текущего значения на таймерах сравниваемых view
    private void SortCustomers()
    {
        customerViews.Sort(
            (CustomerView a, CustomerView b) =>
            {
                if (a.timer.ActualTime == b.timer.ActualTime)
                    return 0;
                else
                    return (int)(a.timer.ActualTime - b.timer.ActualTime);
            }
        );
    }
    
}
