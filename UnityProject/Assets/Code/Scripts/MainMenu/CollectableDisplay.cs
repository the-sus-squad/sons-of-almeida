using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!CollectablesManager.Instance.collectableTags.Contains(tag))
        {
            Destroy(gameObject);
        }
    }
}
