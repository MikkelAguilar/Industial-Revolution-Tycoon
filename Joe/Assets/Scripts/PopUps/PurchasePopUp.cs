using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchasePopUp : MonoBehaviour
{
    [SerializeField] Button _exitButton;
    [SerializeField] Button _purchaseButton;
    [SerializeField] TMP_Text _exitText;
    [SerializeField] TMP_Text _purchaseText;
    [SerializeField] TMP_Text _cost;
    Machine _machine;
    Transform _canvas;
    GlobalVars globals;
    void Update() {
        if (globals.currentCash - _machine.costToUpgrade[_machine.MachineType] < 0) {
            _purchaseButton.interactable = false;
        }
        else {
            _purchaseButton.interactable = true;
        }
    }
    public void Init(Transform canvas, Machine machine, string exitText, string purchaseText) { 
        GameObject manager = GameObject.Find("MainManager");
        globals = manager.GetComponent<GlobalVars>();

        _canvas = canvas;
        _machine = machine;
        _exitText.text = exitText;
        _purchaseText.text = purchaseText;
        _cost.text = "Cost to Purchase : $ " + _machine.costToUpgrade[Machine.MachineGeneration.Unbought];

        transform.SetParent(canvas);
        transform.localScale = Vector3.one;

        GetComponent<RectTransform>().offsetMin = Vector2.zero;
        GetComponent<RectTransform>().offsetMax = Vector2.zero;

        _exitButton.onClick.AddListener(() => {
            GameObject.Destroy(this.gameObject);
        });

        _purchaseButton.onClick.AddListener(() => {
            globals.currentCash -= _machine.costToUpgrade[_machine.MachineType];
            _machine.UpgradeSpinningWheel();
            GameObject.Destroy(this.gameObject);
        });
    }
}
