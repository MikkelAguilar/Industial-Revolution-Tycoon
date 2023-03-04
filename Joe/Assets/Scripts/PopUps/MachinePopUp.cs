using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MachinePopUp : MonoBehaviour
{
    Machine _machine;
    [SerializeField] Button _upgradeButton;
    [SerializeField] TMP_Text _upgradeText;
    [SerializeField] Button _modifyButton;
    [SerializeField] TMP_Text _modifyText;
    [SerializeField] Button _exitButton;
    [SerializeField] TMP_Text _exitText;
    [SerializeField] Button _assignButton;
    [SerializeField] TMP_Text _assignText;
    [SerializeField] TMP_Text _title;
    [SerializeField] TMP_Text _stats;
    [SerializeField] TMP_Text _cost;
    [SerializeField] TMP_Text _modified;
    Transform _canvas;
    GlobalVars globals;
    void Update() { 
        if (_machine.gameObject.tag == "Slide") {
            _stats.text = _machine.productionPerSecond.ToString() + " happiness per second (production rate)";
        }
        else {
            _stats.text = "$ " + _machine.productionPerSecond.ToString() + " per second (production rate)";
        }
        
        if (_machine.modified || !_machine.modifiable || globals.currentCash - _machine.costToModify < 0) {
            _modifyButton.interactable = false;
        }
        else {
            _modifyButton.interactable = true;
        }

        if (globals.currentCash - _machine.costToUpgrade[_machine.MachineType] < 0) {
            _cost.text = "Cost: " + _machine.costToUpgrade[_machine.MachineType];
            _upgradeButton.interactable = false;
        }
        else {
            _upgradeButton.interactable = true;
        }

        if (_machine.finalTypes.Contains(_machine.MachineType)) {
            _cost.text = "MAXED";
            _upgradeButton.interactable = false;
        }
        else {
            _cost.text = "Cost: " + _machine.costToUpgrade[_machine.MachineType];
            _upgradeButton.interactable = true;
        }

        if (_machine.modified) {
            _modified.text = "Already Modified";
        }
    }
    public void Init(Transform canvas, Machine machine, string upgradeText, string modifyText, string exitText) { 
        GameObject manager = GameObject.Find("MainManager");
        globals = manager.GetComponent<GlobalVars>();

        _canvas = canvas;
        _machine = machine;
        _cost.text = "Cost: " + _machine.costToUpgrade[_machine.MachineType];
        _modified.text = "Cost: " + _machine.costToModify;
        _assignText.text = "Assign";
        _title.text = _machine.machineName;
        _exitText.text = exitText;
        _upgradeText.text = upgradeText;
        _modifyText.text = modifyText;

        transform.SetParent(canvas);
        transform.localScale = Vector3.one;

        GetComponent<RectTransform>().offsetMin = Vector2.zero;
        GetComponent<RectTransform>().offsetMax = Vector2.zero;

        _exitButton.onClick.AddListener(() => {
            GameObject.Destroy(this.gameObject);
        });

        _upgradeButton.onClick.AddListener(() => {
            if (globals.currentCash - _machine.costToUpgrade[_machine.MachineType] >= 0) {
                globals.currentCash -= _machine.costToUpgrade[_machine.MachineType];
                if (_machine.gameObject.tag == "Spinning Wheel") { _machine.UpgradeSpinningWheel(); }
                else if (_machine.gameObject.tag == "Water Wheel") { _machine.UpgradeWaterWheel(); }
                else if (_machine.gameObject.tag == "Slide") { _machine.UpgradeSlide(); }
            }
        });

        _modifyButton.onClick.AddListener(() => {
            if (globals.currentCash - _machine.costToModify >= 0) {
                globals.currentCash -= _machine.costToModify;
                _machine.ToggleModdedMachine();
            }
        });

        _assignButton.onClick.AddListener(() => {
            AssignPopUp assign = PopUpManager.Instance.loadAssignPopUp();
            assign.Init(PopUpManager.Instance.canvas, _machine);
        });
    }
}
