using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource ShootingChannel;
    public AudioSource ReloadingChannel;
    
    public AudioClip PistolShot;
    public AudioClip M416Shot;

    public AudioClip PistolReload;
    public AudioClip M416Reload;

    public AudioSource emptyMagzineSoundPistol;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlayShootingSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Pistol:
                ShootingChannel.PlayOneShot(PistolShot);
                break;

            case WeaponModel.M416:
                ShootingChannel.PlayOneShot(M416Shot);
                break;
        }

    }

    public void PlayReloadSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.Pistol:
                ReloadingChannel.PlayOneShot(PistolReload);
                break;

            case WeaponModel.M416:
                ReloadingChannel.PlayOneShot(M416Reload);
                break;
        } 

    }


}
