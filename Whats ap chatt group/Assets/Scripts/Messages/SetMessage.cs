using System;
using Messages;
using TMPro;
using UnityEngine;

public class SetMessage : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI nameNumber;
    
    [SerializeField] 
    private TextMeshProUGUI messageBody;
    
    [SerializeField] 
    private TextMeshProUGUI time;

    public Message testMessage;

    private void Awake()
    {
       // SetContent(testMessage);
       var randomMessage = "Hello, how are you?";
       string multiplied = String.Empty;
       int randomCount = UnityEngine.Random.Range(1, 6);
       for (int i = 0; i < randomCount; i++)
       {
           multiplied += randomMessage;
       }

       messageBody.text = multiplied;
    }

    public void SetContent(Message message)
    {
        //Set name and colour
        nameNumber.text = message.contact.NameNumber;
        nameNumber.color = message.contact.Color;

        messageBody.text = message.MessageBody;

        time.text = message.Time;
    }
}
