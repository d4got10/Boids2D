using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Boid : MonoBehaviour, IBoid
{
    [HideInInspector]
    public static List<IBoid> allBoids;

    public SteeringBehaviorsSettings settings;

    [SerializeField] private LayerMask _boidsLayerMask;

    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _maxForce;

    public Transform Target;

    private Rigidbody2D _rigidbody;

    private List<IBoid> _neighbours;
    private List<RaycastHit2D> _hits;

    private void Awake()
    {
        _hits = new List<RaycastHit2D>();
        _neighbours = new List<IBoid>();
        _rigidbody = GetComponent<Rigidbody2D>();
        if (allBoids == null)
            allBoids = new List<IBoid>();
        allBoids.Add(this);
    }

    private void FixedUpdate()
    {
        var steeringForce = new Vector2();

        if(Target != null && settings.Seek > 0)
            steeringForce = settings.Seek * SteeringBehaviors.Seek(this, Target.position);

        foreach (var boid in allBoids)
        {
            if ((object)boid != this)
            {
                if ((boid.GetPosition() - GetPosition()).sqrMagnitude < 1f)
                {
                    if(settings.Flee > 0)
                        steeringForce += settings.Flee * SteeringBehaviors.Flee(this, boid.GetPosition()) / ((Vector2)transform.position - boid.GetPosition()).magnitude;
                    if (settings.GetClose > 0)
                        steeringForce += settings.GetClose * SteeringBehaviors.Seek(this, boid.GetPosition()); ;
                    if (settings.Align > 0)
                        steeringForce += settings.Align * SteeringBehaviors.Aling(this, boid);
                }
            }
        }

        _rigidbody.AddForce(Vector2.ClampMagnitude(steeringForce, _maxForce));

        TurnTowardsVelocity();
    }

    private void TurnTowardsVelocity()
    {
        if (_rigidbody.velocity.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(_rigidbody.velocity.y, _rigidbody.velocity.x) * Mathf.Rad2Deg - 90f);
        }
    }

    public Vector2 GetPosition() => transform.position;

    public Vector2 GetVelocity() => _rigidbody.velocity;

    public float GetMaxVelocity() => _maxVelocity;

    public float GetMass() => _rigidbody.mass;
}
