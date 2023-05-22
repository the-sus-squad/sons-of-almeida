using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System;

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
        transform.Rotate(0, 0.5f, 0);
    }

    public void pickCollectable(BaseInteractionEventArgs arg)
    {
        uiManager = collectableMenu.GetComponent<UIManager>();
        Destroy(gameObject);
        uiManager.UpdateCollectableMenu(tag);
    }
}
