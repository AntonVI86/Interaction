using UnityEngine;

public class EnemyMover : IMoveable
{
    public float Speed { get; private set; }

    private Rigidbody _rigidbody;

    public EnemyMover(float speed, Rigidbody rigidbody)
    {
        Speed = speed;
        _rigidbody = rigidbody;
    }

    public void ProcessMoveTo(Vector3 direction)
    {
        _rigidbody.velocity = direction * Speed;
    }

    public void ResetVelocity()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }
}
