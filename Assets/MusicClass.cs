using System.Collections;
using System.Collections.Generic;
 using UnityEngine;
 
 public class MusicClass : MonoBehaviour
 {
     public static MusicClass instance;
     private AudioSource _audioSource;
     private void Awake()
     {
         if (instance == null)
         {
            DontDestroyOnLoad(this);
            instance = this;
         }

        else if (instance != this)
        {
             Destroy(gameObject);
        }

        _audioSource = GetComponent<AudioSource>();
    }
 
     public void PlayMusic()
     {
         if (_audioSource.isPlaying) return;
         _audioSource.Play();
     }
 
     public void StopMusic()
     {
         _audioSource.Stop();
     }
 }