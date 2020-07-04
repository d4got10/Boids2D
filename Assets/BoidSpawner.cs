using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _boidPrefab;
    [SerializeField] private Transform _target;

    [SerializeField] private BoidEntityController _boidController;

    private void Start()
    {
        _boidController.Boids = new List<BoidEntity>();
        for (float x = -1f; x <= 1f; x += 1f)
        {
            for(float y = -1f; y <= 1f; y += 1f)
            {
                var boid = Instantiate(_boidPrefab, transform.position + new Vector3(x, y, -1f), Quaternion.identity);
                //if(x == -5 && y == -5)
                var boidEntity = boid.GetComponent<BoidEntity>();
                boidEntity.controller = _boidController;
                boidEntity.Target = _target;
                _boidController.Boids.Add(boidEntity);
            }
        }
        _boidController.gameObject.SetActive(true);
    }
}
