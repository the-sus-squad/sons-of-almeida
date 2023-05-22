using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenMenu : MonoBehaviour
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
