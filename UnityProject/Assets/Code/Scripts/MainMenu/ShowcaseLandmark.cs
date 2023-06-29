using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;


public class ShowcaseLandmark : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<XRGrabInteractable>().selectEntered.AddListener(showcaseLandmark);
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void showcaseLandmark(BaseInteractionEventArgs arg)
    {
        if (transform.CompareTag("PortasSaoFrancisco") || transform.CompareTag("PortasSaoFrancisco2"))
            SceneManager.LoadScene(transform.tag);
    }
}
