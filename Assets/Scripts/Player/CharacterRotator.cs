using UnityEngine;

public class CharacterRotator
{
    private Camera _camera;
    private Transform _transform;

    public CharacterRotator(Camera camera, Transform transform)
    {
        _camera = camera;
        _transform = transform;
    }

    public void ProcessRotate()
    {
        Vector3 inputByCameraDirection = _camera.transform.forward;
        inputByCameraDirection.y = 0;

        _transform.rotation = Quaternion.LookRotation(inputByCameraDirection);
    }
}
