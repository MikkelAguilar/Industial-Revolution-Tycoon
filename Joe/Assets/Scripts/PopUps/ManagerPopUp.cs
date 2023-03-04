using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManagerPopUp : MonoBehaviour
{
    [SerializeField] Button _exitButton;
    [SerializeField] TMP_Text _exitText;
    [SerializeField] Button _hireButton;
    [SerializeField] TMP_Text _hireText;
    [SerializeField] TMP_Text _title;
    [SerializeField] TMP_Text _hireCost;
    [SerializeField] TMP_Text _happinessText;
    [SerializeField] GameObject _employeeLOPrefab;
    [SerializeField] GameObject _employeePanel;
    
    Spawner spawner;
    GlobalVars globals;
    Transform _canvas;
    Employee[] _employees;
    List<Button> _employeeButtons;
    Button _lastClickedEmployee;
    Employee _employeeAssigned;

    public void Init(Transform canvas, string title, string exitText, string hireText, string assignText) {
        GameObject manager = GameObject.Find("MainManager");
        spawner = manager.GetComponent<Spawner>();
        globals = manager.GetComponent<GlobalVars>();

        _employeeButtons = new List<Button>();
        _employees = findEmployees();

        drawEmployeeButtons(_employees);

        _exitText.text = exitText;
        _hireText.text = hireText;
        _hireCost.text = "$ " + globals.costToHire.ToString();
        _title.text = title;
        _canvas = canvas;

        transform.SetParent(canvas);
        transform.localScale = Vector3.one;
        
        GetComponent<RectTransform>().offsetMin = Vector2.zero;
        GetComponent<RectTransform>().offsetMax = Vector2.zero;

        _exitButton.onClick.AddListener(() => {
            GameObject.Destroy(this.gameObject);
        });

        _hireButton.onClick.AddListener(() => {
            globals.currentCash -= globals.costToHire;
            spawnCharacter();
        });
    }
    Employee[] findEmployees() {
        Employee[] employees = GameObject.Find("Employees").GetComponentsInChildren<Employee>();

        return employees;
    }
    EmployeeButton createEmployeeButton(Employee employee) {
        GameObject newObject = Instantiate(_employeeLOPrefab) as GameObject;
        EmployeeButton eb = newObject.GetComponent<EmployeeButton>();
        eb.Init(employee);

        Button button = newObject.GetComponent<Button>();
        button.onClick.AddListener(() => onEmployeeButtonClick(employee, button));
        _employeeButtons.Add(button);

        return eb;
    }
    void drawEmployeeButtons(Employee[] employees) {
        foreach (Employee employee in employees) {
            EmployeeButton eb = createEmployeeButton(employee);
            eb.transform.SetParent(_employeePanel.transform);
        }
    }
    void spawnCharacter() {
        spawner.spawnCharacter();
    }
    void onEmployeeButtonClick(Employee employee, Button button) {
        if (employee != null) {
            AgentMovement movement = employee.GetComponent<AgentMovement>();
            _happinessText.text = employee.employeeName + " happiness: " + movement.happinessSystem.getHappiness();
            _employeeAssigned = employee;
            _lastClickedEmployee = button;
        }
        else {
            _happinessText.text = employee.employeeName + " has died";
        }
    }
}
