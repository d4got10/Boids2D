using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraDragger : MonoBehaviour
{
    private Vector2 _previusPosition;

    private bool _wasDragged;

    public void OnEnable()
    {
        _previusPosition = Camera.main.transform.position;
    }

    public void Update()
    {
        var input = new Vector3();
        input.x += Input.GetAxis("Horizontal");
        input.y += Input.GetAxis("Vertical");

        input.z += Input.GetKey(KeyCode.Space) ? 1 : 0;
        input.z += Input.GetKey(KeyCode.LeftShift) ? -1 : 0;

        input *= 0.2f;

        Camera.main.orthographicSize += input.z * 0.1f;
        if(Camera.main.orthographicSize < 1)
        {
            Camera.main.orthographicSize = 1;
        }
        input.z = 0;

        transform.position += input;
    }
}
