using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;


public class MenuController : MonoBehaviour
{

    public MenuController NextMenu;

    public List<Button> Buttons = new List<Button>();

    public Button ActualSelected
    {
        get { return Buttons[actualSelected]; }
        set { Buttons[actualSelected] = value; }
    }

    int _actualSelected;
    int actualSelected
    {
        get { return _actualSelected; }
        set
        {
            if (value >= Buttons.Count)
                _actualSelected = 0;
            else
                _actualSelected = value;
        }
    }


    /// <summary>
    /// Funzione da chiamare per andare al menù successi
    /// </summary>
    public void GoToNextMenu()
    {

    }

    /// <summary>
    /// Select the next menù item
    /// </summary>
    public void SelectNext()
    {
        actualSelected++;

    }

    /// <summary>
    /// Select the next menù item
    /// </summary>
    public void SelectPrevious()
    {
        ActualSelected.Select();

        actualSelected--;
    }

}
