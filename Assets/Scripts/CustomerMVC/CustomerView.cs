using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/************************************/
//Отображение модели покупателя
//Наследуется от ImageObject
/************************************/

public class CustomerView : ImageObject
{
    private bool isHided;
    public CustomersController controller;
    public bool Active { get { return !isHided; } }    
    public Timer timer;
    public Image skinObject;
    public Image orderHolder;
    public TimerView timerCounter;  
    public List<GameObject> dishList;
    public List<int> Order { get; set; }
    public int skin;

    //При запуске - стать неактивным и выполнить проверку на null
    private new void Start()
    {
        isHided = true;
        NullCheck();
    }

    protected override void NullCheck()
    {
        if (isHided != true || timer==null || skinObject==null || orderHolder==null || timerCounter==null || controller==null)
        {
            throw new System.NullReferenceException("CustomerView exception");
        }
    }
    
    //Создать представление модели
    //Поочередно загрузить скин, заказ и азпустить таймер, по заврешению которого посетитель покинет магазин
    public void CreateView(CustomerModel model)
    {
        skin  = model.skin;
        Order = model.order;
        LoadSkin();
        LoadOrder();
        ShowView();
        timer.Set(GameData.VISITOR_WAIT_TIME,UpdateCustomer,timerCounter);
    }

    //Загрузка изображения покупателя
    private void LoadSkin()
    {
        skinObject.sprite = Resources.Load<Sprite>("BurgerArt/Char/char_"+skin);
    }

    //Формирования отображения заказа
    //Префаб каждого блюда загружается из ресурсов
    //После загрузки всех объектов они выравниваются
    private void LoadOrder()
    {
        dishList.Clear();
        foreach (var dishNumber in Order)
        {
            dishList.Add( GameObject.Instantiate(
                Resources.Load("Prefabs/"+(DishType)dishNumber as string),
                orderHolder.transform) as GameObject
            );

            if (dishNumber != (int)DishType.Cola)
            {
                dishList[dishList.Count - 1].GetComponent<RectTransform>().localScale = new Vector3(0.6f, 0.6f, 0.6f);
            }
        }

        AlignItems();
    }

    //В зависимости от количества позиций в заказе происходит выравнивание
    private void AlignItems()
    {
        switch (Order.Count)
        {
            case 1:
                AlignMid(dishList[0]);
                SetPosition(dishList[0], 0, 0);
                break;
            case 2:
                AlignTop(dishList[0]);
                SetPosition(dishList[0], 0, -200);
                AlignBot(dishList[1]);
                SetPosition(dishList[1], 0, 200);
                break;
            case 3:
                AlignTop(dishList[0]);
                SetPosition(dishList[0], 0, -100);
                AlignMid(dishList[1]);
                AlignBot(dishList[2]);
                SetPosition(dishList[2], 0,  100);
                break;
            default:
                break;
        }
    }

    //Попытка убрать блюдо из заказа, если оно в нем присутствует
    public bool TryRemoveDish(DishType dish)
    {
        if (Order.Contains((int)dish))
        {
            int number = Order.IndexOf((int)dish);
            RemoveDish(number);
            return true;
        }
        else
        {
            return false;
        }
    }

    //Убрать блюдо из списка order
    //Удалить объект блюда в облаке
    //Продлить таймер на соответствующее время
    private void RemoveDish(int number)
    {
        Order.RemoveAt(number);
        GameObject.Destroy(dishList[number]);
        dishList.RemoveAt(number);
        timer.ProlongTimer(GameData.VISITOR_WAIT_ADD);
        if (Order.Count > 0)
        {
            AlignItems();
        }
        else
        {
            UpdateCustomer();
        }
    }

    //Очистить покупателя
    //Через контроллер запустить таймер создания нового представления
    public void UpdateCustomer()
    {
        Order.Clear();
        HideView();
        timer.Reset();
        controller.TryStartTimer();
    }

    //Скрыть представление - уничтожить все объекты заказа, скрыть все остальные изображения
    public void HideView()
    {
        isHided = true;
        foreach (var dish in dishList)
        {
            GameObject.Destroy(dish);
        }
        ImagesToHide.Add(orderHolder);
        ImagesToHide.Add(timerCounter.greenFiller);
        ImagesToHide.Add(skinObject);
        HideImages();
    }

    //Показать представление - отобразить все позиции заказа и все остальные изображения
    public void ShowView()
    {
        isHided = false;
        foreach (var dish in dishList)
        {
            dish.GetComponent<ImageObject>().ShowAll();
        }
        ImagesToShow.Add(orderHolder);
        ImagesToShow.Add(timerCounter.greenFiller);
        ImagesToShow.Add(skinObject);
        ShowImages();
    }    
}
