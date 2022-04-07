using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sequencing
{
    [RequireComponent(typeof(AudioSource))]
    public class SimpleTrackController : MonoBehaviour
    {
        [SerializeField] 
        private Button playButton;
        
        [SerializeField] 
        private Button pauseButton;
        
        [SerializeField] 
        private Button stopButton;

        [SerializeField] 
        private KeyCode pauseKey = KeyCode.Space;

        [SerializeField] 
        private TextMeshProUGUI trackTimeText;
        
        private AudioSource targetAudioSource;

        public float CurrentTime => targetAudioSource.time;

        public AudioSource TargetAudioSource => targetAudioSource;

        public Action Stopped;
        public Action Played;
        public Action Paused;

        private void Awake()
        {
            targetAudioSource = GetComponent<AudioSource>();
            
            if (playButton != null)
            {
                playButton.onClick.AddListener(Play);
            }

            if (pauseButton != null)
            {
                pauseButton.onClick.AddListener(Pause);
            }

            if (stopButton != null)
            {
                stopButton.onClick.AddListener(Stop);
            }
        }

        public void SeekToPoint(float time)
        {
            targetAudioSource.time = time;
        }

        private void Update()
        {
            UpdateText();

            ManageInput();
        }

        private void ManageInput()
        {
            if (Input.GetKeyDown(pauseKey))
            {
                if (targetAudioSource.isPlaying)
                {
                    Pause();
                }
                else
                {
                    Play();
                }
            }
        }

        private void UpdateText()
        {
            var currentTrackTime = TimeSpan.FromSeconds(targetAudioSource.time);
            trackTimeText.text = $"{currentTrackTime.Minutes}:{currentTrackTime.Seconds}:{currentTrackTime.Milliseconds}";
        }

        private void Play()
        {
            targetAudioSource.Play();
            Played?.Invoke();
        }

        private void Stop()
        {
            targetAudioSource.Stop();
            targetAudioSource.time = 0.0f;
            Stopped?.Invoke();
        }

        private void Pause()
        {
            targetAudioSource.Pause();
            Paused?.Invoke();
        }

        private void OnDestroy()
        {
            if (playButton != null)
            {
                playButton.onClick.RemoveListener(Play);
            }

            if (pauseButton != null)
            {
                pauseButton.onClick.RemoveListener(Pause);
            }

            if (stopButton != null)
            {
                stopButton.onClick.RemoveListener(Stop);
            }
        }
    }
}