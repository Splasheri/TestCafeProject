using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/************************************************************************/
//Представление хотдога или бургера
//Может быть в одном из трех состояний - пустая доска, доска с хлебом и готовый продукт
//Переход между состояниями осуществляется различными функциями
//В зависимости от состояния отображаются соответствующие изображения
//Функция ShowAll используется при изображении блюда в представлении заказа в CustomerView
/************************************************************************/

public abstract class DishBase : ImageObject
{
    public DishPhase dishPhase;

    public abstract void ChangeToDesk();
    public abstract void ChangeToBread();
    public abstract void ChangeToMeat();
    
    public override void ShowAll()
    {
        ChangeToMeat();
    }
}
