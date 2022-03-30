using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int playerScore = 0;
    private int playerLives = 5;

    private void Awake(){
        new Utils().SetupSingleton();
    }

    public void OnEnemyKilledEvent(Enemy enemy){
        playerScore += enemy.ScoreValue;
    }

    public void OnPlayerKilledEvent(){
        playerLives--;
    }

    private void OnEnable() {
        //Activate MessageHandlers
        Enemy.EnemyKilledEvent += OnEnemyKilledEvent;
        Enemy.PlayerKilledEvent += OnPlayerKilledEvent;
    }

    private void OnDisable() {
        //Deactivate MessageHandlers    
        Enemy.EnemyKilledEvent -= OnEnemyKilledEvent;
        Enemy.PlayerKilledEvent -= OnPlayerKilledEvent;
    }
}
