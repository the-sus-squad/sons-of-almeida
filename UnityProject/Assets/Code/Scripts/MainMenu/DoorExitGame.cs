using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorExitGame : MonoBehaviour
{
    [SerializeField] private GameObject fadeOut;

    [SerializeField] private float doorExitAngle = 10.0f;

    private Animator fadeOutAnimator;
    private HingeJoint doorHinge;

    // Start is called before the first frame update
    void Start()
    {
        fadeOutAnimator = fadeOut.GetComponent<Animator>();
        doorHinge = GetComponent<HingeJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(doorHinge.angle) > doorExitAngle)
        {
            SaveSystem.SaveSettings(SettingsManager.Instance.sfxVolume, SettingsManager.Instance.uiSize);
            StartCoroutine(ExitGameRoutine());
        }
    }

    private IEnumerator ExitGameRoutine()
    {
        fadeOut.SetActive(true);
        fadeOutAnimator.Play("FadeOut");

        yield return new WaitForSeconds(fadeOutAnimator.GetCurrentAnimatorStateInfo(0).length + fadeOutAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);

        // The functions for exiting editor play mode and quitting a game build are different
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
