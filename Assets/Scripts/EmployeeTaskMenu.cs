using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeTaskMenu : MonoBehaviour
{
    public Transform taskItemPrefab;
    public Transform taskList;
    public EmployeeManager employeeManager;
    public EmployeeBehaviour selectedEmployee;


    private void Start()
    {
        _reloadMenu();
    }

    public void OpenMenu(EmployeeBehaviour employee)
    {
        _reloadMenu();
        gameObject.SetActive(true);
        selectedEmployee = employee;
    }
    
    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    private void _reloadMenu()
    {
        foreach (Transform child in taskList)
        {
            Destroy(child.gameObject);
        }
        
        foreach (var (task, i) in employeeManager.tasks.Select((v, i) => (v, i)))
        {
            _addTaskListItem(task, i);
        }
    }

    private void _executeTask(EmployeeTaskInfo task)
    {
        CloseMenu();
    }
    
    private void _addTaskListItem(EmployeeTaskInfo task, int listPosition)
    {
        var item = Instantiate(taskItemPrefab, taskList);
        item.Find("Name").GetComponent<TextMeshProUGUI>().text = task.name;
        item.Find("Subtitle").GetComponent<TextMeshProUGUI>().text = "";
        item.Find("ExecuteButton").GetComponent<Button>().onClick.AddListener(() => _executeTask(task));

        var itemPositionY = -25.0f - (46.0f * listPosition);
        item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, itemPositionY);
    }
}