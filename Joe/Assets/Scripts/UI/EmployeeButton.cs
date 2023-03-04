using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EmployeeButton : MonoBehaviour
{
    Employee _employee;
    [SerializeField] TMP_Text _title;
    public void Init(Employee employee) {
        _employee = employee;
        _title.text = employee.employeeName;
    }
}
