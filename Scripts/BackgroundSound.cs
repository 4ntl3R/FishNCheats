using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    private AudioSource[] audioSources;

    private void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }

    void Update()
    {
        bool alertFinded = false;
        if (GameObject.FindWithTag("Alert") != null)
        {
            alertFinded = true;
        }

        foreach (AudioSource audio in audioSources)
        {
            if (alertFinded)
            {
                Mute(audio, false);
            }
            else
            {
                Mute(audio, true);
            }
        }
    }

    private void Mute(AudioSource audio, bool mute)
    {
        if (audio.clip.name == "Alert")
        {
            audio.mute = mute;
        }

        if (audio.clip.name == "AllGood")
        {
            audio.mute = !mute;
        }
    }
}
