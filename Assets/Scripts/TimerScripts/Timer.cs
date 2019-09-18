using System;
using System.Collections;
using UnityEngine;

/************************************************************************/
//Стандартный таймер
/************************************************************************/

public class Timer : MonoBehaviour
{
    public  float ActualTime { get { return currentDuration; } } 
    private float startDuration;
    protected float currentDuration;
    private Action onEnd;
    private TimerView timerView;
    
    public bool isStoped { get { return currentDuration == 0; } }

    //Запуск таймера
    //Аргументы: длительность, делегат функции без аргументов, представление таймера
    public void Set(float _duration, Action action, TimerView view = null)
    {
        startDuration = currentDuration = _duration;
        timerView = view;
        onEnd = action;
    }

    //Каждый кадр текущая длительность уменьшается на прошедшее время с прошлого кадра
    //Представление изменяется также каждый кадр
    //Когда текущее время становится меньше или равно нулю - выполняется делегат
    private void Update()
    {
        if (currentDuration > 0)
        {
            currentDuration -= Time.deltaTime;
            if (timerView != null)
            {
                timerView.UpdateTimer(currentDuration / startDuration);
            }
        }
        else
        {
            if (onEnd != null)
            {
                onEnd();
            }
        }
    }

    //Остановка таймера, обнуление делегата
    public void Reset()
    {
        onEnd = null;
        currentDuration = -1;
        startDuration = 0;
    }

    //Уничтожение таймера
    public void Off()
    {
        SelfDestruct();
    }

    //Продление таймера на extendDuration
    public void ProlongTimer(float extendDuration)
    {
        startDuration += extendDuration;
        currentDuration += extendDuration;
    }

    //Метод, служащий для того, чтобы самоуничтожение таймера можно было добавить к делегату
    private void SelfDestruct()
    {
        UnityEngine.Object.Destroy(this);
    }
}
