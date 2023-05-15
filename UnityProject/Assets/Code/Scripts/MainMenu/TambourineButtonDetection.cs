using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TambourineButtonDetection : MonoBehaviour
{
    private enum ButtonType { Plus, Minus };

    [SerializeField] private ButtonType buttonType;
    [SerializeField] private MainMenuManager mainMenuManager;

    // keep track of how many collider are in the trigger
    // so that the hit registers only once
    private int nCollidersInTrigger = -1;

    private void OnTriggerEnter(Collider other)
    {
        //if (other.GetComponent<XRDirectInteractor>())
        //    return;

        //nCollidersInTrigger += 1;
        //if (nCollidersInTrigger == 0)
        //{
        //    if (buttonType == ButtonType.Plus)
        //    {
        //        mainMenuManager.IncreaseSFXVolume();
        //    }
        //    else if (buttonType == ButtonType.Minus)
        //    {
        //        mainMenuManager.DecreaseSfxVolume();
        //    }
        //}

        if (other.name == "Right Hand Tambourine Collider")
        {
            if (buttonType == ButtonType.Plus)
            {
                mainMenuManager.IncreaseSFXVolume();
            }
            else if (buttonType == ButtonType.Minus)
            {
                mainMenuManager.DecreaseSfxVolume();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.GetComponent<XRDirectInteractor>())
        //    return;

        //nCollidersInTrigger -= 1;
    }
}
