using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CloseDetails : MonoBehaviour
{
    public GameObject information;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        XRSimpleInteractable grabbable = GetComponent<XRSimpleInteractable>();
        grabbable.hoverExited.AddListener(pickCollectable);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void pickCollectable(BaseInteractionEventArgs arg)
    {
        information.gameObject.SetActive(false);
        canvas.gameObject.SetActive(true);
    }
}
