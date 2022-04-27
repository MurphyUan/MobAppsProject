using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class WaveController : MonoBehaviour
{
    [Header("Arrays")]
    [SerializeField] private BaseEnemy[] enemies;

    [Header("Misc")]
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip waveComplete;

    private int currentWave = 0;
    public int scoreToReach = 25;
    private GameObject trashCan;
    private BaseEnemy enemy;
    private AudioSource source;
    private GameObject waveTextObject;
    
    void Start()
    {
        source = GetComponent<AudioSource>();
        trashCan = GameObject.Find("Projectiles");

        TransitionWave();
    }

    private void TransitionWave(){
        waveText.text = "Wave " + (currentWave + 1);
        animator.SetTrigger("fade");
        Invoke("StartWave", 2.0f);
    }

    private void StartWave(){
        animator.SetTrigger("fade");
        Utils.PublishStartWaveEvent();
        StartCoroutine(SpawnEnemies(0.25f));
    }

    private void CalculateNeededScore(){
        scoreToReach *= 3;
    }

    private IEnumerator SpawnEnemies(float delay){
        while(true){
            if(currentWave < enemies.Length) enemy = Instantiate(enemies[Random.Range(0, currentWave + 1)], trashCan.transform);
            else enemy = Instantiate(enemies[Random.Range(0, enemies.Length)], trashCan.transform);

            enemy.transform.position = enemy.GetSpawnLocation();

            yield return new WaitForSeconds(delay);
        }
    }

    // Event Handling
    private void OnWaveComplete(){
        currentWave++;
        StopAllCoroutines();
        source.PlayOneShot(waveComplete);
        CalculateNeededScore();
        TransitionWave();
    }

    private void OnPlayerDeath(){
        StopAllCoroutines();
    }

    private void OnPlayerSpawn(){
        StartCoroutine(SpawnEnemies(0.25f));
    }

    private void OnEnable() {
        Utils.WaveCompleteEvent += OnWaveComplete;
        Utils.PlayerKilledEvent += OnPlayerDeath;
        Utils.PlayerSpawnEvent += OnPlayerSpawn;
    }

    private void OnDisable() {
        Utils.WaveCompleteEvent -= OnWaveComplete;
        Utils.PlayerKilledEvent -= OnPlayerDeath;
        Utils.PlayerSpawnEvent -= OnPlayerSpawn;
    }
}
