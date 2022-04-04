using TMPro;
using UnityEngine;

namespace UISizeManagement.SizeReporters
{
    /// <summary>
    /// This is like a <see cref="RectSizeReporter"/> only it can tell the size of a TMP text field of what ever size it happens to be 
    /// </summary>
    public class TextBodySizeReporter : SizeReporter
    {
        [SerializeField] 
        private TextMeshProUGUI messageBodyText;

        protected override Vector2 GetSizeDelta()
        {
            return messageBodyText.GetRenderedValues(true);
        }
    }
}
