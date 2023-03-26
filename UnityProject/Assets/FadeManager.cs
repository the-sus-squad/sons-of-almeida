using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{

    [SerializeField] private Animator fadeAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAnimationEnd() {
        Debug.Log("Fade In animation end");
        fadeAnimator.Play("FadeOut");
    }
}
