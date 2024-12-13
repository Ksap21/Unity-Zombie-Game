using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }
    public AudioSource shootingSoundRifle;
    public AudioSource reloadingSoundRifle;
    public AudioSource emptyMagazineRifle;
    public AudioClip emptyMagazine;
    public AudioClip RifleFire;
    public AudioClip RifleReload;
    public AudioSource shootingSoundPistol;
    public AudioClip PistolFire;

    public AudioClip ZombieWalk;
    public AudioClip ZombieChase;
    public AudioClip ZombieHurt;
    public AudioClip ZombieAttack;
    public AudioClip ZombieDeath;
    public AudioSource ZombieChannel;

    public AudioSource PlayerChannel;
    public AudioClip PlayerDeath;
    public AudioClip PlayerHurt;
    public AudioClip DeathMusic;

    //public AudioClip RifleEmpty;


    public GameObject bulletImpactEffectPrefab;

    private void Awake()
    {

        if (Instance != null && Instance != this)
            {
               Destroy(gameObject);

            }

        else
            {
                Instance = this;
            }
            
    }
}