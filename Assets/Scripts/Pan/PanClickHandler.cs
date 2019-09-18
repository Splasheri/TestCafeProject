using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanClickHandler : MonoBehaviour
{
    public PanView pan;
    public PansController pansManager;
    private float lastClickTime;

    public void OnClick()
    {
        switch (pan.meat.currentPhase)
        {
            case Meat.phase.empty:
                pan.StartFrying();
                break;
            case Meat.phase.normal:
                pansManager.RemoveMeat(pan, false);
                break;
            case Meat.phase.burned:
                if (Time.time - lastClickTime < GameData.DOUBLE_CLICK_TIME)
                {
                    pansManager.RemoveMeat(pan, true);
                }
                break;
        }

        lastClickTime = Time.time;
    }
}
