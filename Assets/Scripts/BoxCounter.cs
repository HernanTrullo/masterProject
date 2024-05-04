using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BoxCounter : Box
{

    [SerializeField] String ColorBox = "White";
    [SerializeField] private AudioClip okSound;
    [SerializeField] private AudioClip noSound;

    private int score = 0;
    private int totalItems = 0;
    public event Action CounterChange;

    private void OnTriggerEnter(Collider other)
    {
        ItemColor itemColor = other.GetComponent<ItemColor>();
        if(itemColor.color == ColorBox)
        {
            Sound_box(okSound);
            score++;
        }
        else{
            Sound_box(noSound);
        }
        totalItems++;
        CounterChange?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        ItemColor itemColor = other.GetComponent<ItemColor>();
        if (itemColor.color == ColorBox)
        {
            score--;
        }
        totalItems--;
        CounterChange?.Invoke();
    }

    public override int GetScore()
    {
        return score;
    }

    public override int GetTotalItems()
    {
        return totalItems;
    }

    public void RestartScore()
    {
        score = 0;
        totalItems = 0;
        CounterChange?.Invoke();
    }

    void Sound_box (AudioClip audioClip){
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.PlayOneShot(audioClip);
    }
    
}
