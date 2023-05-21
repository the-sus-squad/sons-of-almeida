using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public void UpdateCollectableMenu(string tag)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.CompareTag(tag))
            {
                transform.GetChild(i).gameObject.transform.Find("Location").gameObject.SetActive(true);
                transform.GetChild(i).gameObject.transform.Find("Unavailable").gameObject.SetActive(false);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
