using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabInteractableExtended : XRGrabInteractable
{
    [SerializeField]
    Vector3 LeftHandAttachOffset;

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);

        if (selectingInteractor.tag.Equals("HandLeft"))
            attachTransform.localPosition += LeftHandAttachOffset;
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        if (selectingInteractor.tag.Equals("HandLeft"))
            attachTransform.localPosition -= LeftHandAttachOffset;

        base.OnSelectExiting(args);
    }
}
