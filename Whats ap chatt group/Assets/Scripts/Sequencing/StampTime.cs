using Messages;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sequencing
{
    /// <summary>
    /// Adds a time to a selected <see cref="Message"/> asset.
    /// You will have to manually add this asset for now in inspector
    /// </summary>
    public class StampTime : MonoBehaviour
    {
        [SerializeField] private Message currentMessage;

        [Header("UI")] 
        [SerializeField] 
        private TextMeshProUGUI currentMessageLabel;
        
        [SerializeField] 
        private TextMeshProUGUI currentMessageTime;
        
        [SerializeField] 
        private TextMeshProUGUI newMessageTime;

        [SerializeField] 
        private Button stampMessageTimeButton;

        [SerializeField] 
        private Button refreshButton;

        [Header("Sequencing")]
        [SerializeField] 
        private SimpleTrackController trackController;

        private void Awake()
        {
            if (stampMessageTimeButton != null)
            {
                stampMessageTimeButton.onClick.AddListener(OnStampMessage);
            }

            if (refreshButton != null)
            {
                refreshButton.onClick.AddListener(OnRefresh);
            }
        }

        private void OnRefresh()
        {
            if (currentMessage != null)
            {
                currentMessageTime.text = currentMessage.sequence.StageTime.ToString();
                currentMessageLabel.text = currentMessage.name;
            }
        }

        private void Update()
        {
            if (trackController != null)
            {
                if (newMessageTime != null)
                {
                    newMessageTime.text = trackController.CurrentTime.ToString();
                }
            }
        }

        private void OnStampMessage()
        {
            if (currentMessage == null)
            {
                return;
            }

            if (trackController == null)
            {
                Debug.LogError("Track controller is missing. ");
                return;
            }
            
            currentMessage.sequence.StageTime = trackController.CurrentTime;
            currentMessage.sequence.State = MessageState.ReadyToPlay;
            
            Debug.Log($"Message {currentMessage.name }stamped! : Time {currentMessage.sequence.StageTime} ");
        }
    }
}