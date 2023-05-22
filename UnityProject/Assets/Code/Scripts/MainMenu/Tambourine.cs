using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Tambourine : MonoBehaviour
{
    [SerializeField] private GameObject leftHandUIDirectInteractor;
    [SerializeField] private GameObject rightHandUIDirectInteractor;

    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(DisableUIInteractor);
        grabInteractable.selectExited.AddListener(EnableUIInteractor);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("UI Direct Interactor"))
        {
            GetComponent<AudioSource>().Play();
        }
    }

    private void DisableUIInteractor(SelectEnterEventArgs args)
    {
        HandData interactor = args.interactorObject.transform.GetComponentInChildren<HandData>();
        if (interactor.handType == HandData.HandModelType.Left)
            leftHandUIDirectInteractor.SetActive(false);
        else
            rightHandUIDirectInteractor.SetActive(false);
    }

    private void EnableUIInteractor(SelectExitEventArgs args)
    {
        HandData interactor = args.interactorObject.transform.GetComponentInChildren<HandData>();
        if (interactor.handType == HandData.HandModelType.Left)
            leftHandUIDirectInteractor.SetActive(true);
        else
            rightHandUIDirectInteractor.SetActive(true);
    }
}
