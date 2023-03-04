using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnClick : MonoBehaviour
{
    public Machine machine;
    void OnMouseDown() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }
        if(GameObject.FindGameObjectsWithTag("PopUp").Length == 0) {
            if (machine.MachineType != Machine.MachineGeneration.Unbought) {
                MachinePopUp machinePopUp = PopUpManager.Instance.loadMachinePopUp();
                string exitText = "Exit";
                string upgradeText = "Upgrade";
                string modifyText = "Modify";
                machinePopUp.Init(PopUpManager.Instance.canvas, machine, upgradeText, modifyText, exitText);
            }
            else {
                PurchasePopUp purchasePopUp = PopUpManager.Instance.loadPurchasePopUp();
                string exitText = "Exit";
                string purchaseText = "Purchase";
                purchasePopUp.Init(PopUpManager.Instance.canvas, machine, exitText, purchaseText);
            }
        }
    }
}
