using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sequencing
{
    public class PointerEnteringExiting : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private bool pointerOnWaveform;

        public Action<bool> MouseEnterExit;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            MouseEnterExit?.Invoke(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            MouseEnterExit?.Invoke(false);
        }
        
    }
}