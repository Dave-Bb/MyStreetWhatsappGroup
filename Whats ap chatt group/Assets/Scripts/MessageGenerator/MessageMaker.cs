using System;
using System.IO;
using Messages;
using Sequencing;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace MessageGenerator
{
    public class MessageMaker : MonoBehaviour
    {
        [SerializeField] 
        private SimpleTrackController trackController;
        
        [SerializeField] 
        private string messagesPath;
        [SerializeField] 
        private string contactsPath;

        [SerializeField] 
        private Contact currentContact;

        [Header("Contact UI")] 
        
        [SerializeField] 
        private Button makeNewContactButton;

        [SerializeField] 
        private TMP_InputField nameInputField;
        
        [SerializeField] 
        private TMP_Dropdown colorDropDown;

        [Header("Message UI")] 
        
        [SerializeField] 
        private Button makeNewMessageButton;
        
        [SerializeField]
        private TMP_InputField messageInputField;
        
        [SerializeField] 
        private TextMeshProUGUI contactNameLabel;

        private string messageNamePrexix = "SequenceMessage_";

        private void Awake()
        {
            if (makeNewContactButton != null)
            {
                makeNewContactButton.onClick.AddListener(MakeNewContactWithDetails);
            }

            if (makeNewMessageButton != null)
            {
                makeNewMessageButton.onClick.AddListener(MakeNewMessage);
            } 

            if (trackController == null)
            {
                trackController = FindObjectOfType<SimpleTrackController>();
            }

            if (currentContact != null || contactNameLabel != null)
            {
                contactNameLabel.text = currentContact.NameNumber;
            }
        }

        private Contact GetContactDetails()
        {
            Contact newContact = ScriptableObject.CreateInstance<Contact>();
            if (nameInputField != null && nameInputField.text != string.Empty)
            {
                newContact.NameNumber = nameInputField.text;
            }
            else
            {
                Debug.LogError("Unable to make contact, name field is empty!");
                return null;
            }
            
            newContact.Color = Color.magenta;
            
            return newContact;
        }
        
        private void MakeNewContactWithDetails()
        {
            currentContact = GetContactDetails();
            
            if (currentContact != null || contactNameLabel != null)
            {
                contactNameLabel.text = currentContact.NameNumber;
            }

            AssetDatabase.CreateAsset(currentContact, $"{contactsPath}/{currentContact.NameNumber}.asset");
            AssetDatabase.SaveAssets();
            
            Debug.Log($"New Contact Created: Name: {currentContact.NameNumber}");
        }

        private void MakeNewMessage()
        {
            if (currentContact == null)
            {
                Debug.LogError("Unable to make new message, contact field is null");
                return;
            }

            TextMessage textMessage = ScriptableObject.CreateInstance<TextMessage>();

            var numberOfMessages = AssetDatabase.LoadAllAssetsAtPath(messagesPath).Length;
            numberOfMessages++;

            textMessage.contact = currentContact;
            if (messageInputField != null)
            {
                textMessage.MessageBody = messageInputField.text;
            }
            
            
            AssetDatabase.CreateAsset(textMessage, $"{messagesPath}/{messageNamePrexix}{numberOfMessages}.asset");
            AssetDatabase.SaveAssets();
            
            Debug.Log($"New Contact Created: Name: {currentContact.NameNumber}");
        }

        private void OnDestroy()
        {
            if (makeNewContactButton != null)
            {
                makeNewContactButton.onClick.RemoveListener(MakeNewContactWithDetails);
            }
            
            if (makeNewMessageButton != null)
            {
                makeNewMessageButton.onClick.RemoveListener(MakeNewMessage);
            } 
        }
    }
}