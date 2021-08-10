using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampController : MonoBehaviour
{
    [SerializeField]
    GameObject socket, lightSource;

    private Material lightMat;

    void Start()
    {
        lightMat = GetComponent<MeshRenderer>().materials[1];
    }

    // Update is called once per frame
    void Update()
    {
        var dist = Vector3.Distance(socket.transform.position, transform.position);

        if (dist < 0.15f)
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
