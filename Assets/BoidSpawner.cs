using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _boidPrefab;
    [SerializeField] private Transform _target;

    private void Start()
    {
        for(int x = -5; x <= 5; x++)
        {
            for(int y = -5; y <= 5; y++)
            {
                var boid = Instantiate(_boidPrefab, new Vector3(x, y, -1f), Quaternion.identity);
                //if(x == -5 && y == -5)
                boid.GetComponent<Boid>().Target = _target;
            }
        }
    }
}
