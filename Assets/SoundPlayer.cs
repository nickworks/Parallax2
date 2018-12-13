using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {

    public AudioSource Jetpack;
    public AudioSource PlayerLanding;
    public AudioSource PlayerPhase;
    public AudioSource PlayerPhaseFail;
    public AudioSource PlayerDamage;
    public AudioSource PlayerDeath;
    public AudioSource EnemyProjectile;
    public AudioSource EnemyDeath;

    public static SoundPlayer main;

    void Start()
    {
        if(main == null) // if no singleton exists
        {
            main = this; // this is our singleton
            DontDestroyOnLoad(gameObject); // don't destoy this ever
        }
        else
        {
            Destroy(gameObject); // destoy this because one already exists
        }
        
    }
}
