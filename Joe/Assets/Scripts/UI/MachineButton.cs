using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MachineButton : MonoBehaviour
{
    Machine _machine;
    [SerializeField] TMP_Text _title;
    public void Init(Machine machine) {
        _machine = machine;
        _title.text = machine.machineName;
    }
    
}
