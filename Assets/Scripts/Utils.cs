using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    // Event Handling
    public delegate void EnemyKilled(BaseEnemy enemy);
    public static EnemyKilled EnemyKilledEvent;

    public delegate void PlayerKilled();
    public static PlayerKilled PlayerKilledEvent;

    public delegate void PlayerSpawn();
    public static PlayerSpawn PlayerSpawnEvent;

    public delegate void StartWave();
    public static StartWave StartWaveEvent;

    public delegate void WaveComplete();
    public static WaveComplete WaveCompleteEvent;
    
    // Global Accessors
    public static void PublishEnemyKilledEvent(BaseEnemy enemy){
        if(EnemyKilledEvent != null)
            EnemyKilledEvent(enemy);
    }

    public static void PublishPlayerKilledEvent(){
        if(PlayerKilledEvent != null)
            PlayerKilledEvent();
    }

    public static void PublishPlayerSpawnEvent(){
        if(PlayerSpawnEvent != null)
            PlayerSpawnEvent();
    }

    public static void PublishStartWaveEvent(){
        if(StartWaveEvent != null)
            StartWaveEvent();
    }

    public static void PublishWaveCompleteEvent(){
        if(WaveCompleteEvent != null)
            WaveCompleteEvent();
    }

    //Extra Maths Utilities

    // Normalizes the direction between Parent and Target
    public static Vector2 LockOnTarget(Vector2 parent, Vector2 target){
        Vector2 temp = (target - parent).normalized;
        return temp;
    }

    // Gets The Slope of the line in Degrees
    public static float SlopeBetweenTwoPoints(Vector2 target){
        return -Mathf.Atan2(target.x, target.y) * Mathf.Rad2Deg;
    }

    // Play Clip at Camera
    public static void PlaySoundAtCamera(AudioClip clip){
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
    }

    // Instantiates an object at transform.positon
    public static void SpawnObjectAtLocation(GameObject gameObject, Vector2 transform){
        Instantiate(gameObject);

        gameObject.transform.position = transform;
    }

    // Clears all children from Parent transform
    public static void ClearChildren(Transform transform){
        foreach(Transform child in transform){
            Destroy(child.gameObject);
        }
    }
}
