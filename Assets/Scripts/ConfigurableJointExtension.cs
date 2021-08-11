using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurableJointExtension : MonoBehaviour
{
    [SerializeField]
    Vector3 minLimit;

    [SerializeField]
    Vector3 maxLimit;

    private ConfigurableJoint configurableJoint;
    private Vector3 startPosition;

    void Start()
    {
        configurableJoint = GetComponent<ConfigurableJoint>();
        startPosition = transform.position;    
    }

    void Update()
    {
        CheckCustomLimits();
    }

    private void CheckCustomLimits()
    {
        if (configurableJoint.xMotion == ConfigurableJointMotion.Limited)
        {
            var dist = transform.position.x - startPosition.x;

            if (minLimit.x != 0f && dist < minLimit.x)
                transform.position -= new Vector3(dist - minLimit.x, 0f, 0f);
            else if (maxLimit.x != 0f && dist > maxLimit.x)
                transform.position -= new Vector3(dist - maxLimit.x, 0f, 0f);
        }

        if (configurableJoint.yMotion == ConfigurableJointMotion.Limited)
        {
            var dist = transform.position.y - startPosition.y;

            if (minLimit.y != 0f && dist < minLimit.y)
                transform.position -= new Vector3(dist - minLimit.y, 0f, 0f);
            else if (maxLimit.y != 0f && dist > maxLimit.y)
                transform.position -= new Vector3(dist - maxLimit.y, 0f, 0f);
        }

        if (configurableJoint.zMotion == ConfigurableJointMotion.Limited)
        {
            var dist = transform.position.z - startPosition.z;

            if (minLimit.z != 0f && dist < minLimit.z)
                transform.position -= new Vector3(dist - minLimit.z, 0f, 0f);
            else if (maxLimit.z != 0f && dist > maxLimit.z)
                transform.position -= new Vector3(dist - maxLimit.z, 0f, 0f);
        }
    }
}
