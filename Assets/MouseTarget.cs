using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTarget : MonoBehaviour, IBoidInteractable
{
    public void Interact(BoidEntity boid)
    {
        Debug.Log(boid);
    }

    private void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0,0,Camera.main.transform.position.z);
    }
}
