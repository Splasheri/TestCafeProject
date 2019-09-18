using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************************************************************************/
//Таймер для создания нового покупателя
//Наследуется от стандартного таймера
/************************************************************************/

public class CustomerTimer : Timer
{
    private Action<CustomerView> onEnd;
    private CustomerView view;

    //Запуск таймера. 
    //Аргументы: длительность, делегат функции принимающей один аргумент, ссылка на вью, которая будет являться аргументов для onEnd
    public void Set(float _duration, Action<CustomerView> _onEnd, CustomerView _view)
    {
        currentDuration = _duration;
        onEnd = _onEnd;
        view = _view;
    }
    
    //Каждый кадр текущая длительность уменьшается на прошедшее время с прошлого кадра
    //Когда текущее время становится меньше или равно нулю - выполняется делегат и таймер уничтожается
    void Update()
    {
        if(currentDuration > 0)
        {
            currentDuration -= Time.deltaTime;
        }
        else
        {
            currentDuration = 0;
            if (onEnd != null)
            {
                onEnd(view);
            }
            Off();
        }
    }
}
