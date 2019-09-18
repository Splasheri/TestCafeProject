using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/************************************************************************/
//Представление бургера
//Имплементирует функции DishBase
//Отличительная черта - сдвигается burgerTop при добавлении или удалении котлеты
/************************************************************************/

public class Burger : DishBase
{
    private enum BurgerIngredients
    {
        meat = 0
    }
    private int NUM_OF_INGREDIENTS = 1;

    public List<Image> ingredients;
    public List<Image> bread;   //Count=2
    public Image desk;
    public RectTransform burgerTop;

    protected override void NullCheck()
    {
        if (ingredients.Count < NUM_OF_INGREDIENTS || bread.Count < 2 || desk == null || burgerTop == null)
        {
            throw new System.NullReferenceException("Burger exception");
        }
    }

    public override void ChangeToBread()
    {
        dishPhase = DishPhase.onlyBread;
        ImagesToHide.AddRange(ingredients);
        ImagesToShow.AddRange(bread);
        ImagesToShow.Add(desk);
        UpdateView();

        burgerTop.anchoredPosition = new Vector2(0, -60);
    }

    public override void ChangeToDesk()
    {
        dishPhase = DishPhase.emptyDesk;
        ImagesToHide.AddRange(ingredients);
        ImagesToHide.AddRange(bread);
        ImagesToShow.Add(desk);
        UpdateView();
    }

    public override void ChangeToMeat()
    {
        dishPhase = DishPhase.withMeat;
        ImagesToShow.AddRange(bread);
        ImagesToShow.Add(ingredients[(int)BurgerIngredients.meat]);
        ImagesToShow.Add(desk);
        UpdateView();

        burgerTop.anchoredPosition = new Vector2(0, -40);
    }

}
