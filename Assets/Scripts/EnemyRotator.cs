using UnityEngine;

public class EnemyRotator : IRotator
{
    private float _rotationSpeed = 800f;

    private Transform _transform;

    public EnemyRotator(Transform transform)
    {
        _transform = transform;
    }

    public void ProcessRotateTo(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        float step = _rotationSpeed * Time.deltaTime;

        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, step);
    }
}
