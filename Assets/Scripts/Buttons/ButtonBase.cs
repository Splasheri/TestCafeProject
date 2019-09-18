using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/************************************/
//Базовый класс для использования кнопок меню
//Имплементирует интерфейсы IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
/************************************/

public class ButtonBase : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
{
    public Sprite buttonNorm;
    public Sprite buttonPress;
    protected System.Action OnCLick;
    public UIController uc;

    protected virtual void NullCheck()
    {
        if (buttonNorm==null || buttonPress==null || OnCLick==null)
        {
            throw new System.NullReferenceException("Menu button exception");
        }
    }

    //Единственный способ получить ссылку контроллер интерфейса для загружаемого меню - найти объект контроллера средствами GameObject
    protected void SetUIController()
    {
        uc = GameObject.Find("UIController").GetComponent<UIController>();
    }

    //Действие при нажатии
    public void OnPointerClick(PointerEventData eventData)
    {
        OnCLick();
    }

    //Когда клавиша мыши опущена - изменить изображение кнопки на нажатое
    public void OnPointerDown(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = buttonPress;
    }

    //Когда клавиша мыши поднята - изменить изображение кнопки на нормальное
    public void OnPointerUp(PointerEventData eventData)
    {
        this.GetComponent<Image>().sprite = buttonNorm;
    }
}
