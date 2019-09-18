using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/************************************************************************/
//Представление стакана колы
//Может быть в одном из двух состояний full или empty
//Переход между состояниями осуществляется функцией ChangeState
/************************************************************************/

public class Glass : ImageObject
{
    public enum state
    {
        full,
        empty
    }

    public Image        fullGlass;
    public List<Image>  emptyGlass; //Count=2
    public state        currentState;

    protected override void NullCheck()
    {
        if (fullGlass==null || emptyGlass.Count < 2)
        {
            throw new System.NullReferenceException("Cola glass exception");
        }
    }

    //Переход между состояниями заключающийся в скрытии одних изображений 
    //И показе других
    public void ChangeState()
    {
        if (currentState == state.full)
        {
            currentState = state.empty;
            ImagesToHide.Add(fullGlass);
            ImagesToShow.AddRange(emptyGlass);
        }
        else
        {
            currentState = state.full;
            ImagesToHide.AddRange(emptyGlass);
            ImagesToShow.Add(fullGlass);
        }
        UpdateView();
    }

    //Используется прпи отображении в заказе - показ наполненного стакана
    public override void ShowAll()
    {
        currentState = state.full;
        ImagesToHide.AddRange(emptyGlass);
        ImagesToShow.Add(fullGlass);
        UpdateView();
    }
}
