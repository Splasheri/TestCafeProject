using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**********************************************/
//Базовый класс для объект, изменнения в которых заключаются в отображении одних изображений и скрытии других
//Так как в игре есть несколько объектов с лимитированным количеством состояний, которые часто изменяются(Desks, Pans)
//Вместо того, чтобы создать много префабов и загружать их выгоднее создать один и скрывать ненужные части
/**********************************************/
public class ImageObject : MonoBehaviour
{
    //Два списка - для скрытых объектов
    //И для отображаемых
    public List<Image> ImagesToShow;
    public List<Image> ImagesToHide;

    protected void Start()
    {
        NullCheck();
    }

    protected void UpdateLists()
    {
        ImagesToHide = new List<Image>();
        ImagesToShow = new List<Image>();
    }

    //Обязательно вызываемый метод, скрывающий объекты из списка скрытых
    //И отображает объекты из списка отображаемых
    protected void UpdateView()
    {
        HideImages();
        ShowImages();
        ImagesToHide.Clear();
        ImagesToShow.Clear();
    }

    //Скрытие изображения - установление альфа канала в 0
    protected void HideImages()
    {
        foreach (var image in ImagesToHide)
        {
            image.color = new Color(255, 255, 255, 0);
        }
    }

    //Отображение объекта - установление белого цвета у изображения
    protected void ShowImages()
    {
        foreach (var image in ImagesToShow)
        {
            image.color = Color.white;
        }
    }

    //Функции, выравнивающие изображения
    protected void AlignTop(GameObject obj)
    {
        if (obj!=null)
        {
            obj.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1f);
            obj.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1f);
        }
    }
    protected void AlignMid(GameObject obj)
    {
        if (obj!=null)
        {
            obj.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
            obj.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        }
    }
    protected void AlignBot(GameObject obj)
    {
        if (obj != null)
        {
            obj.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0f);
            obj.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0f);
        }
    }

    //Аргументы: переданный объект, координата Х, координата У
    protected void SetPosition(GameObject obj, float x, float y)
    {
        if (obj != null)
        {
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
        }
    }

    public virtual void HideAll()
    {
        HideImages();
    }

    public virtual void ShowAll()
    {
        ShowImages();
    }

    //Путой NullCheck, переопределяемый у потомков
    protected virtual void NullCheck()
    {

    }
}
