using UnityEngine;

public class CharacterMover : IMoveable
{
    public float Speed { get; private set; }

    private Camera _camera;
    private Rigidbody _rigidbody;

    public CharacterMover(float speed, Camera camera, Rigidbody rigidbody)
    {
        Speed = speed;
        _camera = camera;
        _rigidbody = rigidbody;
    }

    public void ProcessMoveTo(Vector3 inputDirection)
    {
        Vector3 inputByCameraDirection = _camera.transform.rotation * inputDirection;
        inputByCameraDirection.y = 0;

        _rigidbody.position += inputByCameraDirection * Time.deltaTime * Speed;
    }

    public void ResetVelocity()
    {
        throw new System.NotImplementedException();
    }
}