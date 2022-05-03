using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform targetTransform;
    public Transform cameraTransform;
    public Transform cameraPivotTransform;
    private Transform myTransform;
    private Vector3 cameraPos;
    private LayerMask ignoredLayerMask;

    private static CameraHandler instance;
    public static CameraHandler Instance 
    {
        get=> instance;
        set => instance = value;
    }

    public float lookSpeed = 0.1f;
    public float followSpeed = 0.1f;
    public float pivotSpeed = 0.3f;

    private float defaultPos;
    private float lookAngle;
    private float pivotAngle;
    private float minPivot = -35;
    private float maxPivot = 35;

    private void Awake()
    {
        Instance = this;
        myTransform = transform;
        defaultPos = cameraTransform.localPosition.z;
        ignoredLayerMask = ~(1 << 8 | 1 << 9 | 1 << 10);
    }

    public void FollowTarget(float delta) 
    {
        Vector3 targetPos = Vector3.Lerp(myTransform.position, targetTransform.position, delta / followSpeed);
        myTransform.position = targetPos;
    }
    public void HandleCameraRotation(float delta, float mouseInputX, float mouseInputY) 
    {
        //Handle cameraAngle relative to the Pivot
        lookAngle += (mouseInputX * lookSpeed) / delta;
        pivotAngle -= (mouseInputX * pivotSpeed) / delta;
        pivotAngle = Mathf.Clamp(pivotAngle, minPivot, maxPivot);

        //Handle CameraHolderRotation
        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        myTransform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        //Handle Camera Rotation
        targetRotation = Quaternion.Euler(rotation);
        cameraPivotTransform.localRotation = targetRotation;
    }
}
