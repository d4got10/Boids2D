using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTarget : MonoBehaviour
{
    private void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position;
    }
}
