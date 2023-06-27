using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyAI : MonoBehaviour
{

    // Target
    public GameObject targetCollider;
    public TeleportationManager target;


    // Vision
    [Range(0, 10)]
    public float searchRadius;
    protected bool hasTarget = false;
    
    // Camera
    public Camera targetCamera;
    private bool isOnCamera = false;

    private bool isCapturingPlayer = false;
    
    [SerializeField] private GameObject fadeOut;
    private Animator fadeOutAnimator;

    // Audio
    public EnemyAudio audioPlayer;

    // Timers
    private Timer timer;
    public float jumpscareTime = 1.0f;
    
    // Navigation
    public EnemyNavigation navigation;

    protected virtual void Start() {
        fadeOutAnimator = fadeOut.GetComponent<Animator>();
        timer = gameObject.AddComponent<Timer>();
    }

    protected virtual void CapturePlayer() {
        if (isCapturingPlayer) return;

        
        // Change to capture clara animation
        // navigation.PlayAnimation("Idle");

        target.isBeingCaptured = true;

        navigation.SetAnimationBool("isCatching", true);
        navigation.BlockAnimations();
        audioPlayer.StopTheme();
        audioPlayer.PlayGameOverTheme();

        isCapturingPlayer = true;
        timer.SetTimer(jumpscareTime, GameOver);

        navigation.Stop();

        // Change destination to targets's front if he is not visible
        if (!isOnCamera) {
            navigation.TeleportTo(targetCamera.transform.position + targetCamera.transform.forward);
        }
    }

    public virtual void seeTarget(bool value) {
        hasTarget = value;
    }

    public virtual void HearTarget(Vector3 position) {
        // if (!gameObject.activeSelf) return;
    }

    void OnBecameInvisible() {
        isOnCamera = false;
    }

    void OnBecameVisible() {
        isOnCamera = true;
    }

    void GameOver() {
        Debug.Log("Game Over");
        StartCoroutine(GameOverRoutine());
    }

    private IEnumerator GameOverRoutine()
    {
        Debug.Log("Game Over Routine");
        fadeOut.SetActive(true);
        fadeOutAnimator.Play("FadeOut");

        yield return new WaitForSeconds(fadeOutAnimator.GetCurrentAnimatorStateInfo(0).length + fadeOutAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);

        SceneManager.LoadScene("GameOverScene");
    }

    private void OnTriggerEnter(Collider t) {
        if (t.gameObject == targetCollider) {
            CapturePlayer();
        }
    }
}
