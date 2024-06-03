using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace C__scripts
{
    public class Settings : MonoBehaviour
    {
        private bool isFullScreen;
        public AudioMixer am;

        void Start()
        {
            isFullScreen = true;
        }
    
        public void FullScreenToggle()
        {
            isFullScreen = !isFullScreen;
            Screen.fullScreen = isFullScreen;
            Debug.Log("Change FullScreen");
        }
        
        public void AudioVolume(Slider slider)
        {
            am.SetFloat("masterVolume", slider.value);
            Debug.Log($"Change volume to {slider.value}");
        }
    }
}
