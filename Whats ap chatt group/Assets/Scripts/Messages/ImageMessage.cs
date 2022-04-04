using UnityEngine;

namespace Messages
{
    [CreateAssetMenu(fileName = "Message", menuName = "ScriptableObjects/Messages/Image Message", order = 1)]
    public class ImageMessage : ScriptableObject, IImage
    {
        public Message Message;
        public Sprite Image;
        public float ImageScale = 1.0f;

        public Vector2 GetImageSize()
        {
            return new Vector2(Image.texture.width, Image.texture.height) * ImageScale;
        }
    }
}