using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    public Vector3 offset;
    public float smoothRotation = 5f;
    public bool lookAtTarget = true;

    private Vector3 targetRotation;

    void Start()
    {
        offset = transform.position - target.position;
        targetRotation = target.eulerAngles;
    }

    void LateUpdate()
    {
        targetRotation.y = target.eulerAngles.y;

        Quaternion rotation = Quaternion.Euler(targetRotation);

        Vector3 newPosition = target.position + rotation * offset;

        transform.position = Vector3.Lerp(transform.position, newPosition, smoothRotation * Time.deltaTime);

        if (lookAtTarget)
        {
            transform.LookAt(target);
        }

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            lookAtTarget = false;
        }
    }
}
