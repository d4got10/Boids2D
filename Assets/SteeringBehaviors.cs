using UnityEngine;

public static class SteeringBehaviors
{
    public static Vector2 Seek(IBoid boid, Vector2 position)
    {
        var desiredVelocity = (position - boid.GetPosition()).normalized * boid.GetMaxVelocity();
        return desiredVelocity - boid.GetVelocity();
    }

    public static Vector2 Flee(IBoid boid, Vector2 position)
    {
        var desiredVelocity = -(position - boid.GetPosition()).normalized * boid.GetMaxVelocity();
        return desiredVelocity - boid.GetVelocity();
    }

    public static Vector2 Aling(IBoid boid, IBoid target)
    {
        var desiredVelocity = target.GetVelocity();
        return desiredVelocity - boid.GetVelocity();
    }
}
