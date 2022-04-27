using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private TMP_Text lives;
    private TMP_Text score;
    private TMP_Text combo;

    [SerializeField] private Vector2 playerSpawn;

    private int playerScore = 0;
    private int playerLives = 3;

    private int playerCombo = 0;
    private int waveNum = 1;
    private bool waveIP = true;

    private WaveController wc;
    private SceneController sc;
    private GameObject player;
    private GameObject projectiles;

    public int PlayerScore { get { return playerScore;} }

    private void Awake(){
        Time.timeScale = 1;

        wc = GetComponentInChildren<WaveController>();
        sc = GetComponent<SceneController>();

        player = Resources.Load<GameObject>("Player");
        projectiles = GameObject.Find("Projectiles");

        lives = GameObject.Find("Lives").GetComponent<TMP_Text>();
        score = GameObject.Find("Score").GetComponent<TMP_Text>();
        combo = GameObject.Find("Combo").GetComponent<TMP_Text>();

        Utils.SpawnObjectAtLocation(player, playerSpawn);
    }

    private void Update(){

        if(playerScore >= wc.scoreToReach && waveIP == true){
            Utils.PublishWaveCompleteEvent();
            waveIP = false;
        }

        score.text = "Score: " + playerScore.ToString("000000000");

        combo.text = "X " + playerCombo;
    }

    public void OnEnemyKilledEvent(BaseEnemy enemy){
        playerScore += CalculateScore(enemy.ScoreValue);
        playerCombo++;
    }

    public void OnPlayerKilledEvent(){
        playerLives--;
        playerCombo = 0;

        lives.text = "Lives: " + playerLives;
        Utils.ClearChildren(projectiles.transform);

        if(playerLives == 0){
            sc.ShowRecord();
            return;
        }

        Utils.SpawnObjectAtLocation(player, playerSpawn);
        Utils.PublishPlayerSpawnEvent();
    }

    public void OnWaveStart(){
        waveNum++;
        waveIP = true;
    }

    private void OnEnable() {
        //Activate MessageHandlers
        Utils.EnemyKilledEvent += OnEnemyKilledEvent;
        Utils.PlayerKilledEvent += OnPlayerKilledEvent;
        Utils.StartWaveEvent += OnWaveStart;
    }

    private void OnDisable() {
        //Deactivate MessageHandlers    
        Utils.EnemyKilledEvent -= OnEnemyKilledEvent;
        Utils.PlayerKilledEvent -= OnPlayerKilledEvent;
        Utils.StartWaveEvent -= OnWaveStart;
    }

    private int CalculateScore(int score){
        return Mathf.FloorToInt(score + (score * (playerCombo * 0.05f)));
    }
}
