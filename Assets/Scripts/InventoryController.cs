using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InventoryController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public bool shouldFollowCamera;

    [SerializeField]
    private float cameraTiltXThreshold;

    void FixedUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y * 0.6f, target.position.z) + Vector3.up * offset.y
                            + Vector3.ProjectOnPlane(target.right, Vector3.up).normalized * offset.x
                            + Vector3.ProjectOnPlane(target.forward, Vector3.up).normalized * offset.z;

        if (shouldFollowCamera && ClampAngle(target.eulerAngles.x) > cameraTiltXThreshold)
            transform.eulerAngles = new Vector3(0f, target.eulerAngles.y, 0f);
    }

    private float ClampAngle(float angle)
    {
        if (angle > 180f)
            return 360f - angle;
        else
            return 0f - angle;
    }
}
