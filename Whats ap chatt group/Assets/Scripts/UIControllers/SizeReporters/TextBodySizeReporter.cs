using Messages.UIControllers.SizeReporters;
using TMPro;
using UnityEngine;

public class TextBodySizeReporter : SizeReporter
{
    [SerializeField] private TextMeshProUGUI messageBodyText;
    
    protected override Vector2 GetSizeDelta()
    {
        return messageBodyText.GetRenderedValues(true);
    }
}
