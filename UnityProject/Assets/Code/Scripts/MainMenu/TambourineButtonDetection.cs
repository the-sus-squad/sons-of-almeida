using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TambourineButtonDetection : MonoBehaviour
{
    private enum ButtonType { Plus, Minus };

    [SerializeField] private ButtonType buttonType;
    [SerializeField] private MainMenuManager mainMenuManager;

    private void OnTriggerEnter(Collider other)
    {
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
}
