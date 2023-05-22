using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowDetails : MonoBehaviour
{
    public GameObject information;
    public List<GameObject> collectablesList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        XRSimpleInteractable grabbable = GetComponent<XRSimpleInteractable>();
        grabbable.hoverExited.AddListener(showDetails);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showDetails(BaseInteractionEventArgs arg)
    {
        information.gameObject.SetActive(true);
        for (int i = 0; i < collectablesList.Count; i++)
        {
            collectablesList[i].gameObject.SetActive(false);
        }
    }
}
