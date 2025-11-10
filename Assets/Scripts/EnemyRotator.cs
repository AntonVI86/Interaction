using UnityEngine;

public class EnemyRotator : IRotator
{
    private float _rotationSpeed = 800f;

    public void ProcessRotateTo(Vector3 direction, Transform transform)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        float step = _rotationSpeed * Time.deltaTime;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, step);
    }
}
