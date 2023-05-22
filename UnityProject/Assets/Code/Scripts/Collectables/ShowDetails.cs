using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowDetails : MonoBehaviour
{
    public GameObject information;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        XRSimpleInteractable grabbable = GetComponent<XRSimpleInteractable>();
        grabbable.hoverExited.AddListener(checkDetails);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void checkDetails(BaseInteractionEventArgs arg)
    {
        information.gameObject.SetActive(true);
        canvas.gameObject.SetActive(false);
    }
}
