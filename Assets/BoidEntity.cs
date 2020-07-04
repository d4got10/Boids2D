using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BoidEntity : MonoBehaviour
{
    public static Action OnAnyBoidBeenDestroyed;

    [SerializeField] private GameObject _explosionPrefab;

    public BoidEntityController controller;
    public Rigidbody2D Rigidbody;

    public float MaxSpeed;
    public float MaxForce;

    public Transform Target;

    public struct Data
    {
        public float2 position;
        public float2 velocity;
        public float maxSpeed;
        public float maxForce;

        public float2 targetPosition;

        public float deltaTime;

        public Data(BoidEntity entity)
        {
            position.x = entity.transform.position.x;
            position.y = entity.transform.position.y;

            velocity.x = entity.Rigidbody.velocity.x;
            velocity.y = entity.Rigidbody.velocity.y;

            maxSpeed = entity.MaxSpeed;
            maxForce = entity.MaxForce;

            targetPosition.x = entity.Target.position.x;
            targetPosition.y = entity.Target.position.y;

            deltaTime = Time.deltaTime;
        }

        public void Update(float2 steering)
        {
            velocity.x += steering.x * deltaTime;
            velocity.y += steering.y * deltaTime;
        }

        public float2 Seek(float2 targetPosition)
        {
            float2 desired; 
            desired.x = targetPosition.x - position.x;
            desired.y = targetPosition.y - position.y;

            float magnitude = math.sqrt(desired.x * desired.x + desired.y * desired.y);

            if (magnitude != 0)
            {
                desired *= maxSpeed / magnitude;
            }

            return desired - velocity;
        }

        public float2 Flee(float2 targetPosition)
        {
            float2 desired;
            desired.x = targetPosition.x - position.x;
            desired.y = targetPosition.y - position.y;

            float magnitude = math.sqrt(desired.x * desired.x + desired.y * desired.y);

            if (magnitude != 0)
            {
                desired *= maxSpeed / magnitude;
            }

            return -desired - velocity;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        var component = collision.collider.GetComponent<IBoidInteractable>();
        if (component != null)
        {
            component.Interact(this);
        }
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity).transform.localScale = transform.localScale * 0.1f;
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        OnAnyBoidBeenDestroyed?.Invoke();
        controller.Boids.Remove(this);
    }
}
