using System.Collections.Generic;
using UnityEngine;

namespace Messages
{
    /// <summary>
    /// Gif message type, like an image message only with a list of sprites 
    /// </summary>
    [CreateAssetMenu(fileName = "Message", menuName = "ScriptableObjects/Messages/Gif Message", order = 1)]

    public class GifMessage : Message, IImage
    {
        public List<Sprite> Frames;
        public float ImageScale = 1.0f;

        public Vector2 GetImageSize()
        {
            var firstFrame = Frames[0];
            return new Vector2(firstFrame.texture.width, firstFrame.texture.height) * ImageScale;
        }
    }
}