using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Интерфейс для контроллеров
public interface IController
{
    //Вызываемый в начале игры метод
    void StartGame();
    //Вызываемый в конце игры метод
    void EndGame();
    //Проверка переданных в класс ссылок
    void NullCheck();
}
