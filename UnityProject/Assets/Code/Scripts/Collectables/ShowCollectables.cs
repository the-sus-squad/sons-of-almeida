using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowCollectables : MonoBehaviour
{
    public GameObject collectableMenu;

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
        collectableMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
