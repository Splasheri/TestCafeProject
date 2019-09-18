using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanView : MonoBehaviour
{
    public Meat  meat;  
    public bool isEmpty { get { return meat.currentPhase == Meat.phase.empty; } }
    
    public void StartFrying()
    {
        meat.ChangePhase();       
    }

    public void RemoveMeat()
    {
        meat.RemoveMeat();
    }
}
