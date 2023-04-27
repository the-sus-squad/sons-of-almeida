using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRInteractorOverride : XRRayInteractor {

    protected bool canTeleport = true;


    protected override void OnSelectExiting( SelectExitEventArgs args )
    {
        if ( !canTeleport ){
            args.isCanceled = true;
        }
    }

    public void SetTeleport(bool value) {
        canTeleport = value;
    }

}
