using UnityEngine;

public class FollowToTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _offsetZ;
    [SerializeField] private float _offsetY;
    
    private void LateUpdate()
    {
        Vector3 offset = transform.rotation * new Vector3(0, _offsetY, _offsetZ);

        transform.position = _target.position + offset;
    }
}