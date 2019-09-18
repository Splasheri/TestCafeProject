using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/************************************************************************/
//Представление хотдога
//Имплементирует функции DishBase
/************************************************************************/

public class Hotdog : DishBase
{
    private enum HotdogIngredients
    {
        sausage = 0
    }
    private int NUM_OF_INGREDIENTS;

    public List<Image> ingredients;
    public List<Image> bread; //Count==2
    public Image desk;

    protected override void NullCheck()
    {
        if (ingredients.Count < NUM_OF_INGREDIENTS || bread.Count < 2 || desk == null)
        {
            throw new System.NullReferenceException("Hotdog exception");
        }
    }

    public override void ChangeToBread()
    {
        UpdateLists();
        dishPhase = DishPhase.onlyBread;
        ImagesToHide.AddRange(ingredients);
        ImagesToShow.AddRange(bread);
        ImagesToShow.Add(desk);
        UpdateView();
    }

    public override void ChangeToDesk()
    {
        UpdateLists();
        dishPhase = DishPhase.emptyDesk;
        ImagesToHide.AddRange(ingredients);
        ImagesToHide.AddRange(bread);
        ImagesToShow.Add(desk);
        UpdateView();
    }
        
    public override void ChangeToMeat()
    {
        UpdateLists();
        dishPhase = DishPhase.withMeat;
        ImagesToShow.AddRange(bread);
        ImagesToShow.Add(ingredients[(int)HotdogIngredients.sausage]);
        ImagesToShow.Add(desk);
        UpdateView();
    }
}
