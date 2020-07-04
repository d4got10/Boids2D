using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

[BurstCompile]
public struct BoidUpdateJob : IJobParallelForTransform
{
    [ReadOnly]
    public float deltaTime;
    [ReadOnly]
    public float FleeValue;
    [ReadOnly]
    public float SeekValue;


    [NativeDisableParallelForRestriction]
    public NativeArray<BoidEntity.Data> BoidDataArray;

    public void Execute(int index, TransformAccess transform)
    {
        var data = BoidDataArray[index];
        float2 steering = SeekValue * data.Seek(data.targetPosition);
        for(int i = 0; i < BoidDataArray.Length; i++)
        {
            if (i == index) continue;
            float2 distance = BoidDataArray[i].position - data.position;
            
            float mag = distance.x * distance.x + distance.y * distance.y;
            mag += 0.0001f;
            if (mag < 4f)
            {
                steering += FleeValue * data.Flee(BoidDataArray[i].position) / mag;
            }
        }

        float magnitude = math.sqrt(steering.x * steering.x + steering.y * steering.y);
        if (magnitude > data.maxForce)
            steering *= data.maxForce / magnitude;
        data.Update(steering);
        BoidDataArray[index] = data;

        magnitude = data.velocity.x * data.velocity.x + data.velocity.y * data.velocity.y;
        if (magnitude > 0.01f)
        {
            transform.rotation = Quaternion.Euler(0, 0, math.degrees(math.atan2(data.velocity.y, data.velocity.x)) - 90f);
        }
    }
}

public class BoidEntityController : MonoBehaviour
{
    private List<BoidEntity> _boids;
    public List<BoidEntity> Boids
    {
        get => _boids;
        set
        {           
            _boids = value;
            OnBoidCountChanged?.Invoke(_boids.Count);
        }
    }

    public float FleeValue = 0.5f;
    public float SeekValue = 1f;

    private NativeArray<BoidEntity.Data> _boidDataArray;
    private TransformAccessArray _boidTransformsArray;

    public Action<int> OnBoidCountChanged;

    private void OnEnable()
    {
        
    }

    public void Update()
    {
        _boidDataArray = new NativeArray<BoidEntity.Data>(Boids.Count, Allocator.Persistent);
        _boidTransformsArray = new TransformAccessArray(Boids.Count);

        for (int i = 0; i < _boidDataArray.Length; i++)
        {
            _boidTransformsArray.Add(Boids[i].transform);
        }

        for (int i = 0; i < _boidDataArray.Length; i++)
        {
            if (Boids[i] != null)
            {
                _boidDataArray[i] = new BoidEntity.Data(Boids[i]);
                _boidTransformsArray[i] = Boids[i].transform;
            }
        }

        var job = new BoidUpdateJob
        {
            deltaTime = Time.deltaTime,
            FleeValue = FleeValue,
            SeekValue = SeekValue,
            BoidDataArray = _boidDataArray
        };
        var jobHandle = job.Schedule(_boidTransformsArray);
        jobHandle.Complete();

        for (int i = 0; i < _boidDataArray.Length; i++)
        {
            Boids[i].Rigidbody.velocity = _boidDataArray[i].velocity;
        }

        _boidDataArray.Dispose();
        _boidTransformsArray.Dispose();
    }

    public void OnDestroy()
    {
        
    }
}
