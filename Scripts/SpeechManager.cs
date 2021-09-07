using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechManager : MonoBehaviour
{
    private bool fishNeedSpeak = false;
    private bool fishSpeak = false;
    private int numberClick = 0;
    public int countSpeech = 4;
    private AudioSource[] audioSource;
    private AudioSource speech;

    void Awake()
    {
        audioSource = GetComponents<AudioSource>();
    }

    public void PlaySpeech()
    {   
        if(!fishNeedSpeak && !fishSpeak)
        {
            fishNeedSpeak = true;
            if(numberClick < countSpeech)
            {
                numberClick++;
            }
            else
            {
                numberClick = 1;
            }

        }
    }


    void Update()
    {
        if(fishNeedSpeak && !fishSpeak)
        {
            foreach(AudioSource fishSpeech in audioSource)
            {
                if (fishSpeech.clip.name == numberClick.ToString())
                {
                    speech = fishSpeech;
                }
            }
        }

        Speak();
    }

    private void Speak()
    {
        if (speech != null)
        {
            if (!fishSpeak)
            {
                if (!speech.isPlaying)
                {
                    fishSpeak = true;
                    speech.Play();
                }
            }
            else
            {
                if (!speech.isPlaying)
                {
                    fishSpeak = false;
                    fishNeedSpeak = false;
                    speech = null;
                }
            }
        }

    }
}
