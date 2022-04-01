using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EmployeeCharacterFlipper : MonoBehaviour
{
    public EmployeeBehaviour employeeBehaviour;

    // Update is called once per frame
    void Update()
    {
        if (employeeBehaviour.velocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (employeeBehaviour.velocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
