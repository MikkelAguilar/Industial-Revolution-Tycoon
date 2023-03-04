using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager Instance;
    [SerializeField] public Transform canvas;
    void Start()
    {
        if (Instance != null) {
            GameObject.Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }
    public QuitPopUp loadQuitPopUp() {
        GameObject popUpObject = Instantiate(Resources.Load("UI/QuitPopUp") as GameObject);
        return popUpObject.GetComponent<QuitPopUp>();
    }
    public HelpPopUp loadHelpPopUp() {
        GameObject popUpObject = Instantiate(Resources.Load("UI/HelpPopUp") as GameObject);
        return popUpObject.GetComponent<HelpPopUp>();
    }
    public ManagerPopUp loadManagerPopUp() {
        GameObject popUpObject = Instantiate(Resources.Load("UI/ManagerPopUp") as GameObject);
        return popUpObject.GetComponent<ManagerPopUp>();
    }
    public SavePopUp loadSavePopUp()
    {
        GameObject popUpObject = Instantiate(Resources.Load("UI/SavePopUp") as GameObject);
        return popUpObject.GetComponent<SavePopUp>();
    }
    public MachinePopUp loadMachinePopUp()
    {
        GameObject popUpObject = Instantiate(Resources.Load("UI/MachinePopUp") as GameObject);
        return popUpObject.GetComponent<MachinePopUp>();
    }
    public PurchasePopUp loadPurchasePopUp()
    {
        GameObject popUpObject = Instantiate(Resources.Load("UI/PurchasePopUp") as GameObject);
        return popUpObject.GetComponent<PurchasePopUp>();
    }
    public AssignPopUp loadAssignPopUp()
    {
        GameObject popUpObject = Instantiate(Resources.Load("UI/AssignPopUp") as GameObject);
        return popUpObject.GetComponent<AssignPopUp>();
    }
}

