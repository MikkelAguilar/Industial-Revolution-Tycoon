using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameTag : MonoBehaviour
{
    public TMP_Text nameText;
    private Employee employee;
    void Start()
    {
        nameText.text = GetComponent<Employee>().employeeName;
    }
}
