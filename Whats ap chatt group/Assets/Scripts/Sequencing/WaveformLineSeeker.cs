using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sequencing
{
    /// <summary>
    /// There is a little seeker marker that can be seen over the wave form that will just follow with time. 
    /// If you set <see cref="moveToMouse"/> it will then be seeking the track manually 
    /// </summary>
    [RequireComponent(typeof(RectTransform), typeof(Image))]
    public class WaveformLineSeeker : MonoBehaviour
    {
        [SerializeField] 
        private bool moveToMouse;

        [SerializeField] private Toggle mouseSeekingToggle; 
        
        private SimpleTrackController simpleTrackController;
        private PointerEnteringExiting pointerEvents;
        private RectTransform rectTransform;
        private AudioSource audioSource;
        private Image seekerImage;

        private float trackWaveformSize;
        private float magicWhy = 160; //I dunno, it just needs this for some reason. 
        private bool allowMouseSeeking;
        private bool allowMouseSeekingAndShow;

        private void Awake()
        {
            simpleTrackController = GetComponentInParent<SimpleTrackController>();
            pointerEvents = GetComponentInParent<PointerEnteringExiting>();
            
            rectTransform = GetComponent<RectTransform>();
            seekerImage = GetComponent<Image>();

            if (mouseSeekingToggle != null)
            {
                mouseSeekingToggle.onValueChanged.AddListener(OnMouseSeekingToggled);
                OnMouseSeekingToggled(mouseSeekingToggle.isOn);
            }

            if (pointerEvents != null)
            {
                pointerEvents.MouseEnterExit += OnMouseEnterExit;
                
            }
            else
            {
                Debug.LogWarning("Cant subscribe to pointer events.");
            }
        }

        private void OnMouseSeekingToggled(bool allow)
        {
            allowMouseSeekingAndShow = allow;
        }

        private void OnMouseEnterExit(bool mouseDidEnter)
        {
            if (!moveToMouse)
            {
                return;
            }
            
            allowMouseSeeking = mouseDidEnter;
            if (moveToMouse)
            {
                seekerImage.enabled = allowMouseSeeking && allowMouseSeekingAndShow;
            }
        }

        private void Start()
        {
            if (simpleTrackController != null)
            {
                audioSource = simpleTrackController.TargetAudioSource;
                trackWaveformSize = simpleTrackController.GetComponent<RectTransform>().rect.width;
            }
        }

        private void Update()
        {
            if (!moveToMouse)
            {
                UpdateToAudioSource();
                return;   
            }

            if (!allowMouseSeeking)
            {
                return;
            }
            
            UpdateToMouse();
            
            if (Input.GetMouseButtonDown(0))
            {
                SendSeekPoint();
            }
        }

        private void SendSeekPoint()
        {
            var seekPosition = PointOnWaveformToTime(rectTransform.anchoredPosition.x);
            
            //Trim the edges if we clicked off
            if (seekPosition > audioSource.clip.length || seekPosition < 0)
            {
                seekPosition = 0;
            }

            if (simpleTrackController != null)
            {
                simpleTrackController.SeekToPoint(seekPosition);
            }
        }

        private void UpdateToMouse()
        {
            var mousePosition = Input.mousePosition;
           rectTransform.position =  new Vector3(mousePosition.x, magicWhy, 0);
        }

        private void UpdateToAudioSource()
        {
            var anchorPositionForWaveform = TimeToPoint(audioSource.time);
            rectTransform.anchoredPosition = new Vector2(anchorPositionForWaveform, rectTransform.anchoredPosition.y);
        }

        private float TimeToPoint(float currentTime)
        {
            //current % of the way through the track 
            var p = currentTime / audioSource.clip.length;
            //Get this as a ratio of Seeking track width 
            var ratio = trackWaveformSize * p;

            return ratio;
        }

        private float PointOnWaveformToTime(float anchorPosition)
        {
            //Basically just the reverse of the UpdateToAudioSource method 
            var positionToWaveformSize = anchorPosition / trackWaveformSize;
            var seekPosition = positionToWaveformSize * audioSource.clip.length;

            return seekPosition;
        }

        private void OnDestroy()
        {
            if (pointerEvents != null)
            {
                pointerEvents.MouseEnterExit -= OnMouseEnterExit;
            }
        }
    }
}