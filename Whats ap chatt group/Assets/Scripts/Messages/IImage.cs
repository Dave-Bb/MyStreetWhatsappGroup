using UnityEngine;

namespace Messages
{
    /// <summary>
    /// Interface for Image/gif or video type messages, where the message sets the
    /// size of a rect based on the image size, this is then used to scale the message boundaries
    /// </summary>
    public interface IImage
    {
        public Vector2 GetImageSize();
    }
}