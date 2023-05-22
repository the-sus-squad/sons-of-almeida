using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CloseDetails : MonoBehaviour
{
    public GameObject information;
    public List<GameObject> collectablesList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        XRSimpleInteractable grabbable = GetComponent<XRSimpleInteractable>();
        grabbable.hoverExited.AddListener(closeDetails);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void closeDetails(BaseInteractionEventArgs arg)
    {
        information.gameObject.SetActive(false);
        for (int i = 0; i < collectablesList.Count; i++)
        {
            collectablesList[i].gameObject.SetActive(true);
        }
    }
}
