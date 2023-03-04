using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void quitButton() {
        QuitPopUp quit = PopUpManager.Instance.loadQuitPopUp();
        string yesText = "Yes";
        string noText = "No";
        string mainText = "Are you sure you want to QUIT? (All data will be lost on exit)";
        quit.Init(PopUpManager.Instance.canvas, yesText, noText, mainText);
    }
    public void helpButton() {
        HelpPopUp help = PopUpManager.Instance.loadHelpPopUp();
        string exitText = "Exit";
        help.Init(PopUpManager.Instance.canvas, exitText);
    }
    public void managerButton() {
        ManagerPopUp manager = PopUpManager.Instance.loadManagerPopUp();
        string title = "Manager";
        string exitText = "Exit";
        string hireText = "Hire";
        string assignText = "Assign";
        manager.Init(PopUpManager.Instance.canvas, title, exitText, hireText, assignText);
    }
    public void saveButton()
    {
        SavePopUp save = PopUpManager.Instance.loadSavePopUp();
        save.Init(PopUpManager.Instance.canvas);
    }
}
