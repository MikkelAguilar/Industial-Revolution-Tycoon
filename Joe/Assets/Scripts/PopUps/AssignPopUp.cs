using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AssignPopUp : MonoBehaviour
{
    Machine _machine;
    Transform _canvas;
    [SerializeField] TMP_Text _details;
    [SerializeField] Button _assignButton;
    [SerializeField] Button _unassignButton;
    [SerializeField] Button _unassignAllButton;
    [SerializeField] Button _exitButton;
    [SerializeField] GameObject _employeeLOPrefab;
    [SerializeField] GameObject _employeePanel;
    [SerializeField] GameObject _employeePanel2;
    Machine[] _machines;
    Employee[] _allEmployees;
    Employee[] _employeesAssigned;
    List<Button> _employeeButtons1;
    List<Button> _employeeButtons2;
    Button _lastClickedEmployee1;
    Button _lastClickedEmployee2;
    Employee _employeeAssigned1;
    Employee _employeeAssigned2;
    public void Init(Transform canvas, Machine machine) {
        _canvas = canvas;
        _machine = machine;

        _machines = findMachines();
        _allEmployees = findEmployees();
        _employeesAssigned = machine.employeesAssigned.ToArray();

        _employeeButtons1 = new List<Button>();
        _employeeButtons2 = new List<Button>();

        drawEmployeeButtons(_allEmployees, _employeePanel, 1);
        drawEmployeeButtons(_employeesAssigned, _employeePanel2, 2);

        transform.SetParent(canvas);
        transform.localScale = Vector3.one;

        GetComponent<RectTransform>().offsetMin = Vector2.zero;
        GetComponent<RectTransform>().offsetMax = Vector2.zero;

        _exitButton.onClick.AddListener(() => {
            GameObject.Destroy(this.gameObject);
        });

        _assignButton.onClick.AddListener(() => {
            if (!_machine.machineFull()) {
                if (!_machine.employeesAssigned.Contains(_employeeAssigned1)) {
                    clearEmployeeFromMachines(_employeeAssigned1);
                    _machine.employeesAssigned.Add(_employeeAssigned1);

                    AgentMovement employeeMovement = _employeeAssigned1.GetComponent<AgentMovement>();

                    employeeMovement.changeMachineTarget(_machine);
                    employeeMovement.changeState(1);
                    _details.text = _employeeAssigned1.employeeName + " is assigned to this machine";
                }
                else {
                    _details.text = _employeeAssigned1.employeeName + " is already assigned to this machine";
                }
            }
            else {
                _details.text = "Machine is full";
            }
        });

        _unassignButton.onClick.AddListener(() => {
            AgentMovement employeeMovement = _employeeAssigned2.GetComponent<AgentMovement>();
            employeeMovement.changeState(2);
            _machine.employeesAssigned.Remove(_employeeAssigned2);
            _details.text = _employeeAssigned2.employeeName + " has been unassigned from this machine";
        });
        _unassignAllButton.onClick.AddListener(() => {
            foreach (Employee emp in _allEmployees)
            {
                AgentMovement employeeMovement = emp.GetComponent<AgentMovement>();
                employeeMovement.changeState(2);
            }
            _machine.employeesAssigned.Clear();
            _details.text = "All employees have been unassigned from this machine";
        });
    }

    Employee[] findEmployees() {
        Employee[] employees = GameObject.Find("Employees").GetComponentsInChildren<Employee>();

        return employees;
    }
    Machine[] findMachines() {
        Machine[] machines = GameObject.Find("Machines").GetComponentsInChildren<Machine>();

        return machines;
    }
    EmployeeButton createEmployeeButton(Employee employee, int type = 1) {
        GameObject newObject = Instantiate(_employeeLOPrefab) as GameObject;
        EmployeeButton eb = newObject.GetComponent<EmployeeButton>();
        eb.Init(employee);

        Button button = newObject.GetComponent<Button>();
        if (type == 1) {
            button.onClick.AddListener(() => onEmployeeButtonClick1(employee, button));
            _employeeButtons1.Add(button);
        }
        else {
            button.onClick.AddListener(() => onEmployeeButtonClick2(employee, button));
            _employeeButtons2.Add(button);
        }

        return eb;
    }
    void drawEmployeeButtons(Employee[] employees, GameObject panel, int type = 1) {
        foreach (Employee employee in employees) {
            EmployeeButton eb;
            if (type == 1) {
                eb = createEmployeeButton(employee, 1);
            }
            else {
                eb = createEmployeeButton(employee, 2);
            }
            eb.transform.SetParent(panel.transform);
        }
    }
    void clearEmployeeFromMachines(Employee employee) {
        foreach (Machine m in _machines) {
            if (m.employeesAssigned.Contains(employee)) {
                m.employeesAssigned.Remove(employee);
            }
        }
    }
    void onEmployeeButtonClick1(Employee employee, Button button) {
        _employeeAssigned1 = employee;
        _lastClickedEmployee1 = button;
    }
    void onEmployeeButtonClick2(Employee employee, Button button) {
        _employeeAssigned2 = employee;
        _lastClickedEmployee2 = button;
    }
    void Update()
    {
        if (_employeeAssigned1 != null) {
            _assignButton.interactable = true;
        }
        else {
            _assignButton.interactable = false;
        }
        
        if (_employeeAssigned2 != null) {
            _unassignButton.interactable = true;
        }
        else {
            _unassignButton.interactable = false;
        }

        foreach (Button b in _employeeButtons1) {
            if (b == _lastClickedEmployee1) {
                b.GetComponent<Image>().color = Color.grey;
            }
            else {
                b.GetComponent<Image>().color = Color.white;
            }
        }

        foreach (Button b in _employeeButtons2) {
            if (b == _lastClickedEmployee2) {
                b.GetComponent<Image>().color = Color.grey;
            }
            else {
                b.GetComponent<Image>().color = Color.white;
            }
        }
    }
}
