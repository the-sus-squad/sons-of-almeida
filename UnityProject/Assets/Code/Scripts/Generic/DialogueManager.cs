using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    public SubtitleInteraction subtitle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SayHello() {
        Debug.Log("Hello Prologers!");
        subtitle.ShowMessage("Hello Prologers!");
    }
}
