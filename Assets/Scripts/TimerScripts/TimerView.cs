using UnityEngine;
using UnityEngine.UI;

/************************************************************************/
//Представление таймера
//Может иметь два цвета, а может и не иметь
/************************************************************************/

public class TimerView : ImageObject
{
    public enum color
    {
        red,
        green
    }

    public color currentColor;
    public Image redFiller;
    public Image greenFiller;
    public Image redBg;
    public Image greenBg;

    //При обновлении таймера изменяет степень закрашенности полоски в зависимости от отношения
    //Пройденного времени к начальному времени    
    public void UpdateTimer(float percentage)
    {
        if (currentColor==color.green)
        {
            greenFiller.fillAmount = percentage;
        }
        else
        {
            redFiller.fillAmount = percentage;
        }
    }

    //Изменение цвета таймера
    //Выполняется скрытием изображений одного цвета
    //И показа изображений другого цвета
    //Средствами ImageObject
    public void ChangeColor()
    {
        if (currentColor == color.green)
        {
            currentColor = color.red;

            ImagesToHide.Add(greenBg);
            ImagesToHide.Add(greenFiller);

            ImagesToShow.Add(redBg);
            ImagesToShow.Add(redFiller);
        }
        else
        {
            currentColor = color.green;

            ImagesToHide.Add(redBg);
            ImagesToHide.Add(redFiller);

            ImagesToShow.Add(greenBg);
            ImagesToShow.Add(greenFiller);
        }
        UpdateView();
    }
        
}
