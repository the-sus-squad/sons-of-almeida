using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MainMenuLabels : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    public void GrabInteractable(SelectEnterEventArgs args)
    {
        GetComponent<Canvas>().enabled = false;
    }

    public void ReleaseInteractable(SelectExitEventArgs args)
    {
        GetComponent<Canvas>().enabled = true;
    }
}
