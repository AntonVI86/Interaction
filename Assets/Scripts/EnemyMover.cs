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
}
