using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject fadeOut;

    [SerializeField] private HingeJoint door;
    [SerializeField] private float doorExitAngle = 10.0f;

    // Update is called once per frame
    void Update()
    {
        if (door.angle > doorExitAngle)
        {
            ExitGame();
        }
    }

    private void ExitGame()
    {
        fadeOut.SetActive(true);
        fadeOut.GetComponent<Animator>().Play("FadeOut");

        Debug.Log("Exit Game");
    }
}
