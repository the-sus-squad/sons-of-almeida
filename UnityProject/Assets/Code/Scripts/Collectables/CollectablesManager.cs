using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesManager : MonoBehaviour
{
    private static CollectablesManager _instance;

    public static CollectablesManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public List<string> collectableTags;

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        CollectablesData data = SaveSystem.LoadCollectables();
        if (data != null)
        {
            collectableTags = data.collectableTags;
        }
        else
        {
            collectableTags = new List<string>();
            SaveSystem.SaveCollectables(collectableTags);
        }
    }

    public void AddCollectable(string collectableTag)
    {
        collectableTags.Add(collectableTag);
    }
}
