using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NewBehaviourScript;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }


    public AudioSource ShootingChannel;

    public AudioClip M1911Shot;
    public AudioClip AK74Shot;
    public AudioClip M4Shot;
    public AudioClip UziShot;

    public AudioSource reloadingSoundAK74;
    public AudioSource reloadingSoundM1911;
    public AudioSource reloadingSoundM4;
    public AudioSource reloadingSoundUzi;

    public AudioSource emptyMagazineSoundM1911;
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

    public void PlayShootingSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.M1911:
                ShootingChannel.PlayOneShot(M1911Shot);
                break;
            case WeaponModel.AK74:
                ShootingChannel.PlayOneShot(AK74Shot);
                break;
            case WeaponModel.M4:
                ShootingChannel.PlayOneShot(M4Shot);
                break;
            case WeaponModel.Uzi:
                ShootingChannel.PlayOneShot(UziShot);
                break;
        }
    }

    public void PlayReloadSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.M1911:
                reloadingSoundM1911.Play();
                break;
            case WeaponModel.AK74:
                reloadingSoundAK74.Play();
                break;
            case WeaponModel.M4:
                reloadingSoundM4.Play();
                break;
            case WeaponModel.Uzi:
                reloadingSoundUzi.Play();
                break;
        }
    }
}
