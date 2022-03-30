using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Constructor incase of emergency
    public Projectile(int hit){
        hitPoints = hit;
    }

    [SerializeField] private int hitPoints = 1;

    public int Damage { 
        get { return hitPoints;} 
        set { 
            if(hitPoints < value){
                Destroy(gameObject);
                return;
            }
            hitPoints -= value; 
        }
    }
}
