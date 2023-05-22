using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISize : MonoBehaviour
{
    [SerializeField] private float minScale = 0.5f;
    [SerializeField] private float maxScale = 2.0f;

    private GameObject[] scalableUI;

    private int currentUISize = 5;
    private int maxUISize = 10;
    private int minUISize = 0;

    // Start is called before the first frame update
    void Start()
    {
        scalableUI = GameObject.FindGameObjectsWithTag("ScalableUIElement");
        UpdateUIScale();
    }

    public void IncreaseUISize()
    {
        if (currentUISize < maxUISize)
            currentUISize++;

        UpdateUIScale();
    }

    public void DecreaseUISize()
    {
        if (currentUISize > minUISize)
            currentUISize--;

        UpdateUIScale();
    }

    private void UpdateUIScale()
    {
        // convert the scale from [0, 10] to [0.5 to 2.0]
        float newScale = (currentUISize / (maxUISize / (maxScale - minScale))) + minScale;

        foreach (GameObject uiElement in scalableUI)
        {
            uiElement.transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }
}
