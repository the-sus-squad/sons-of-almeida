using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CloseCollectables : MonoBehaviour
{
    public GameObject collectableMenu;
    public GameObject collectableButton;

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
        collectableMenu.gameObject.SetActive(false);
        collectableButton.gameObject.SetActive(true);
    }
}
