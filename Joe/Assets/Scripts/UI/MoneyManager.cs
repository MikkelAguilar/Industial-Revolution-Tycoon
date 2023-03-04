using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] TMP_Text _moneyText;
    GlobalVars globals;
    void Start()
    {
        GameObject manager = GameObject.Find("MainManager");
        globals = manager.GetComponent<GlobalVars>();
    }
    void Update()
    {
        _moneyText.text = "$ " + globals.currentCash;
    }
}
