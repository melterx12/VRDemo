using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class XRSocketInteractorWithTagCheck : XRSocketInteractor
{
    public string targetTag = string.Empty;

    public override bool CanHover(XRBaseInteractable interactable)
    {
        return base.CanHover(interactable) && MatchOnTag(interactable);
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        return base.CanSelect(interactable) && MatchOnTag(interactable);
    }

    private bool MatchOnTag(XRBaseInteractable interactable)
    {
        if (targetTag != string.Empty)
            return interactable.CompareTag(targetTag);
        else
            return true;
    }
}
