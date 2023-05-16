using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Button myButtom;
    bool free = true;
    private void Update()
    {
        if (free == true)
        {
            myMixer.SetFloat("Master", 80 * 20f);
        }
        else
        {
            myMixer.SetFloat("Master", 0 * 20f);
        }
    }
    public void changeaudio()
    {
        free = !free;
    }
}
