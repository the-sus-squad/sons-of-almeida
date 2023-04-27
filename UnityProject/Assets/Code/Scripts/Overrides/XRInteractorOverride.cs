using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRInteractorOverride : XRRayInteractor {

    protected bool cancelNextTeleport = false;

    public void Update(){
        if ( true ){
            cancelNextTeleport = true;
        }
    }

    protected override void OnSelectExiting( SelectExitEventArgs args )
    {

        if ( cancelNextTeleport ){
            cancelNextTeleport = false;
            Debug.Log("Canceling teleport");
            args.isCanceled = true;   // only avail on the exit event
        }
    }
}
