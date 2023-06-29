using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;

// Allows for all necessary actions after picking up a collectable scroll in-game
public class Collectable : MonoBehaviour
{
    UIManager uiManager;
    public GameObject collectableMenu;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.selectEntered.AddListener(pickCollectable);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0.5f, 0, Space.World);
        //GetComponent<Animator>().Play("Cylinder|CylinderAction");
        //GetComponent<Animator>().Play("Cylinder|SpiralAction");
    }

    public void pickCollectable(BaseInteractionEventArgs arg)
    {
        // Aqui há-de ser hopefully a lógica pra alterar o pergaminho e papel de parede no quarto, guardar no save system e limpar este do ambiente

        // Save collectable
        CollectablesManager.Instance.AddCollectable(tag);
        SaveSystem.SaveCollectables(CollectablesManager.Instance.collectableTags);

        //uiManager = collectableMenu.GetComponent<UIManager>();
        Destroy(gameObject);
        //uiManager.UpdateCollectableMenu(tag);
    }
}
