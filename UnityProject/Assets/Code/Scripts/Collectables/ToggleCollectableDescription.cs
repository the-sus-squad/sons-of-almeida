using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ToggleCollectableDescription : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.hoverEntered.AddListener(showDescription);
        grabbable.hoverExited.AddListener(hideDescription);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showDescription(BaseInteractionEventArgs arg)
    {
        transform.Find("Location").gameObject.SetActive(false);
        transform.Find("Description").gameObject.SetActive(true);
    }

    public void hideDescription(BaseInteractionEventArgs arg)
    {
        transform.Find("Location").gameObject.SetActive(true);
        transform.Find("Description").gameObject.SetActive(false);
    }
}
