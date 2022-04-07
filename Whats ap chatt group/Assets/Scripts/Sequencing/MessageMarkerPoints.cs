using Messages;
using UnityEngine;

namespace Sequencing
{
    public class MessageMarkerPoints : MonoBehaviour
    {
        [SerializeField] 
        private MessageSequencer sequencer;

        [SerializeField] 
        private GameObject markerPointPrefab;
        
        private SimpleTrackController simpleTrackController;
        private AudioSource audioSource;
        private float trackWaveformSize;
        
        private void Awake()
        {
            simpleTrackController = GetComponentInParent<SimpleTrackController>();
        }
        
        private void Start()
        {
            if (simpleTrackController == null)
            {
                Debug.LogError("NO Track controller for marker points");
                return;
            }
            
            audioSource = simpleTrackController.TargetAudioSource;
            trackWaveformSize = simpleTrackController.GetComponent<RectTransform>().rect.width;
                
            MakeMarkers();
        }

        private void MakeMarkers()
        {
            var currentMessages = sequencer.Messages;
            foreach (var message in currentMessages)
            {
                var newMarker = Instantiate(markerPointPrefab, transform).GetComponent<MarkerPoint>();
                var anchorPoint = TimeToPoint(message.sequence.StageTime);
                newMarker.SetMarker(anchorPoint, message);
                
                newMarker.PointerClicked += OnMarkerPointerClicked;

            }
        }

        private void OnMarkerPointerClicked(Message obj)
        {
            //TODO make the marker points be able to edit the message from that point 
            Debug.Log("SHOW MESSAGE TO EDIT "+obj.MessageBody);
        }

        private float TimeToPoint(float currentTime)
        {
            //current % of the way through the track 
            var p = currentTime / audioSource.clip.length;
            //Get this as a ratio of Seeking track width 
            var ratio = trackWaveformSize * p;

            return ratio;
        }
    }
}