using System;
using System.Collections.Generic;

[Serializable]
public class CollectablesData
{
    public List<string> collectableTags;

    public CollectablesData(List<string> tags)
    {
        collectableTags = tags;
    }
}
