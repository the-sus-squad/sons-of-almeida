using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRevealer : MonoBehaviour
{

    // Time in X seconds to reveal the enemy
    public float revealTime = 1.0f;
    private float currentRevealTime;
    
    public AudioSource spawnSound;
    public List<GameObject> enemies;


    // Start is called before the first frame update
    void Start()
    {
        currentRevealTime = revealTime;
        foreach (var enemy in enemies) {
            enemy.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (revealTime < 0) return;

        currentRevealTime -= Time.deltaTime;
        if (currentRevealTime <= 0) {
            ActivateEnemies();
            Destroy(this);
        }
    }

    public void ActivateEnemies() {
        foreach (var enemy in enemies) {
            enemy.SetActive(true);
        }
        // spawnSound.Play();
    }
}
