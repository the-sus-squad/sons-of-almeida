using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering;

public class UISize : MonoBehaviour
{
    [SerializeField] private float minScale = 0.5f;
    [SerializeField] private float maxScale = 2.0f;

    [SerializeField] private BarsDisplay barsDisplay;

    private GameObject[] scalableUI;

    private int maxUISize = 10;
    private int minUISize = 0;

    // Start is called before the first frame update
    void Start()
    {
        scalableUI = GameObject.FindGameObjectsWithTag("ScalableUIElement");
        barsDisplay.SetBars(SettingsManager.Instance.uiSize);
        UpdateUIScale();
    }

    public void IncreaseUISize()
    {
        if (SettingsManager.Instance.uiSize < maxUISize)
            SettingsManager.Instance.uiSize++;

        UpdateUIScale();
    }

    public void DecreaseUISize()
    {
        if (SettingsManager.Instance.uiSize > minUISize)
            SettingsManager.Instance.uiSize--;

        UpdateUIScale();
    }

    private void UpdateUIScale()
    {
        // convert the scale from [0, 10] to [0.5 to 2.0]
        float newScale = (SettingsManager.Instance.uiSize / (maxUISize / (maxScale - minScale))) + minScale;

        foreach (GameObject uiElement in scalableUI)
        {
            uiElement.transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }
}
