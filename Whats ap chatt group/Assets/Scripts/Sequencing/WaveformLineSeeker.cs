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
        
        private SimpleTrackController simpleTrackController;
        private PointerEnteringExiting pointerEvents;
        private RectTransform rectTransform;
        private AudioSource audioSource;
        private Image seekerImage;

        private float trackWaveformSize;
        private float magicWhy = 160; //I dunno, it just needs this for some reason. 
        private bool allowMouseSeeking;

        private void Awake()
        {
            simpleTrackController = GetComponentInParent<SimpleTrackController>();
            pointerEvents = GetComponentInParent<PointerEnteringExiting>();
            
            rectTransform = GetComponent<RectTransform>();
            seekerImage = GetComponent<Image>();

            if (pointerEvents != null)
            {
                pointerEvents.MouseEnterExit += OnMouseEnterExit;
                
            }
            else
            {
                Debug.LogWarning("Cant subscribe to pointer events.");
            }
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
                seekerImage.enabled = allowMouseSeeking;
            }
            else
            {
                
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
            //Basically just the reverse of the UpdateToAudioSource method 
            var positionToWaveformSize = rectTransform.anchoredPosition.x / trackWaveformSize;
            var seekPosition = positionToWaveformSize * audioSource.clip.length;
            
            //Trim the edges if we clicked off
            if (seekPosition < 0)
            {
                seekPosition = 0;
            }

            if (seekPosition > audioSource.clip.length)
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
            //current % of the way through the track 
            var currentTime = audioSource.time / audioSource.clip.length;
            //Get this as a ratio of Seeking track width 
            var ratio = trackWaveformSize * currentTime;
            rectTransform.anchoredPosition = new Vector2(ratio, rectTransform.anchoredPosition.y);
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