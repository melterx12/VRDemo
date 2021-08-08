using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresenceController : MonoBehaviour
{
    public bool showController = false;
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;

    private InputDevice targetDevice;
    private GameObject spawnedController, spawnedHand;
    private Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        TryInit();
    }

    void TryInit()
    {
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            //Set target device for this controller as the first device in the list
            targetDevice = devices[0];

            if (showController)
            {
                GameObject prefab = controllerPrefabs.Find(c => c.name == targetDevice.name);
                if (prefab)
                    spawnedController = Instantiate(prefab, transform);
                else
                {
                    Debug.LogError("Could not find corresponding controller model.");
                    spawnedController = Instantiate(controllerPrefabs[0], transform);
                }
            }
            else
            {
                spawnedHand = Instantiate(handModelPrefab, transform);
                handAnimator = spawnedHand.GetComponent<Animator>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetDevice.isValid)
            TryInit();
        else if (!showController)
            UpdateHandAnimation();
    }

    void UpdateHandAnimation()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
            handAnimator.SetFloat("Trigger", triggerValue);
        else
            handAnimator.SetFloat("Trigger", 0f);

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            handAnimator.SetFloat("Grip", gripValue);
        else
            handAnimator.SetFloat("Grip", 0f);
    }
}
