using UnityEngine;

public class CharacterRotator
{
    private Camera _camera;

    public CharacterRotator(Camera camera)
    {
        _camera = camera;
    }

    public void ProcessRotate(Transform transform)
    {
        Vector3 inputByCameraDirection = _camera.transform.forward;
        inputByCameraDirection.y = 0;

        transform.rotation = Quaternion.LookRotation(inputByCameraDirection);
    }
}
