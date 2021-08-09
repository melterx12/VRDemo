using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovementController : MonoBehaviour
{
    public XRNode inputSource;
    private Vector2 inputAxis;
    private XRRig rig;
    private CharacterController character;
    private float fallSpeed;

    [SerializeField]
    float speed = 1f, additionalHeight = 0.2f;
    [SerializeField]
    LayerMask collisionLayer;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
    }

    // Update is called once per frame
    void Update()
    {
        var device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    void FixedUpdate()
    {
        CharacterFollowHeadset();

        //Move player using left analog stick and headset facing direction
        var headYaw = Quaternion.Euler(0f, rig.cameraGameObject.transform.eulerAngles.y, 0f);
        var direction = headYaw * new Vector3(inputAxis.x, 0f, inputAxis.y);
        character.Move(direction * speed * Time.fixedDeltaTime);

        //Apply gravity if player is not grounded
        if (CheckIfGrounded())
            fallSpeed = 0f;
        else
        {
            fallSpeed += Physics.gravity.y * Time.fixedDeltaTime;
            character.Move(Vector3.up * fallSpeed * Time.fixedDeltaTime);
        }
    }

    void CharacterFollowHeadset()
    {
        character.height = rig.cameraInRigSpaceHeight + additionalHeight;
        var capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, (character.height * 0.5f) + character.skinWidth, capsuleCenter.z);
    }

    bool CheckIfGrounded()
    {
        var rayStart = transform.TransformPoint(character.center);
        var rayLength = character.center.y + 0.01f;
        return Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, collisionLayer);
    }
}
