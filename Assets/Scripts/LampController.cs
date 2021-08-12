using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampController : MonoBehaviour
{
    [SerializeField]
    GameObject socket, lightSource;

    private Material lightMat;

    void Awake()
    {
        lightMat = GetComponent<MeshRenderer>().materials[1];
    }

    public void ToggleLamp(bool state)
    {
        if (state)
        {
            lightMat.EnableKeyword("_EMISSION");
            lightSource.SetActive(true);
        }
        else
        {
            lightMat.DisableKeyword("_EMISSION");
            lightSource.SetActive(false);
        }
    }
}
