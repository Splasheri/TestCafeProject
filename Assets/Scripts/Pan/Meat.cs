using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meat : ImageObject
{
    public enum phase
    {
        empty,
        raw,
        normal,
        burned
    }

    public phase currentPhase;

    public Image rawMeat;
    public Image normalMeat;
    public Image burnedMeat;

    public Timer timer;
    public TimerView counter;

    public void ChangePhase()
    {
        switch (currentPhase)
        {
            case phase.empty:
                ImagesToHide.AddRange(
                    new List<Image>() { normalMeat, burnedMeat }
                );
                ImagesToShow.Add(rawMeat);
                timer.Set(GameData.FRY_TIME,ChangePhase,counter);

                currentPhase = phase.raw;
                break;

            case phase.raw:                
                ImagesToHide.AddRange(
                    new List<Image>() { rawMeat, burnedMeat }
                );
                ImagesToShow.Add(normalMeat);
                timer.Set(GameData.BURN_TIME, ChangePhase, counter);
                counter.ChangeColor();

                currentPhase = phase.normal;
                break;

            case phase.normal:
                ImagesToHide.AddRange(
                    new List<Image>() { rawMeat, normalMeat}
                );
                ImagesToShow.Add(burnedMeat);
                timer.Reset();                

                currentPhase = phase.burned;
                break;
        }

        UpdateView();
    }

    public void RemoveMeat()
    {
        ImagesToHide.AddRange(
            new List<Image>() { rawMeat, normalMeat, burnedMeat}
        );
        UpdateView();

        timer.Reset();
        if (counter.currentColor != TimerView.color.green)
        {
            counter.ChangeColor();
        }

        currentPhase = phase.empty;
    }
}
