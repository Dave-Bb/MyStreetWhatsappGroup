using System.Collections.Generic;
using UnityEngine;

namespace Messages
{
    [CreateAssetMenu(fileName = "Message", menuName = "ScriptableObjects/Messages/Gif Message", order = 1)]

    public class GifMessage : ScriptableObject, IImage
    {
        public Message Message;
        public List<Sprite> Frames;
        public float ImageScale = 1.0f;

        public Vector2 GetImageSize()
        {
            var firstFrame = Frames[0];
            return new Vector2(firstFrame.texture.width, firstFrame.texture.height) * ImageScale;
        }
    }
}