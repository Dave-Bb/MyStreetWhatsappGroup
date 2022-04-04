using UnityEngine;

namespace Messages
{
    /// <summary>
    /// Child of <see cref="Message"/>
    /// This is a message + Image and scale component. 
    /// </summary>
    [CreateAssetMenu(fileName = "Message", menuName = "ScriptableObjects/Messages/Image Message", order = 1)]
    public class ImageMessage : Message, IImage
    {
        public Sprite Image;
        public float ImageScale = 1.0f;

        /// <summary>
        /// Get the size for this image 
        /// </summary>
        /// <returns>The vector size of the image * scale</returns>
        public Vector2 GetImageSize()
        {
            return new Vector2(Image.texture.width, Image.texture.height) * ImageScale;
        }
    }
}