using UnityEngine;

public interface IMoveable
{
    float Speed { get; }

    void ProcessMoveTo(Vector3 direction);
}
