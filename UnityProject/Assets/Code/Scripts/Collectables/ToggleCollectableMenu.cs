using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Allows the user to use the right controller's primary button to toggle on/off the collectable UI items
public class ToggleCollectableMenu : MonoBehaviour
{

    public InputActionProperty openMenuAction;
    public GameObject canvas;

    // Start is called before the first frame update
    void Awake()
    {
        openMenuAction.action.started += Toggle;
    }

    void OnDestroy()
    {
        openMenuAction.action.started -= Toggle;
    }

    // Update is called once per frame
    void Toggle(InputAction.CallbackContext context)
    {
        canvas.gameObject.SetActive(!canvas.gameObject.activeInHierarchy);
    }
}
