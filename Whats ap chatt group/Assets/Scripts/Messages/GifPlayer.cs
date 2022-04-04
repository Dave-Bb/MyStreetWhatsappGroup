using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Messages
{
    /// <summary>
    /// Play gifs, or rather List of Sprites as frames. 
    /// </summary>
    public class GifPlayer : MonoBehaviour
    {
        [SerializeField] 
        private List<Sprite> gifFrames;

        [SerializeField] 
        private float frameRate = 20;

        [SerializeField] 
        private bool playOnAwake;

        [SerializeField] 
        private Image image;

        private bool isPlaying;

        private int currentIndex;
        private int count;
        private int currentFrameCount;

        private float lastFrameTime;

        private void Awake()
        {
            isPlaying = playOnAwake;
            count = gifFrames.Count;
        }

        public void SetGifFrames(List<Sprite> frames)
        {
            gifFrames = frames;
            count = gifFrames.Count;
            isPlaying = true;
        }

        private void Update()
        {
            if (!isPlaying)
            {
                return;
            }

            if (lastFrameTime >= 1/frameRate)
            {
                var currentFrame = gifFrames[currentFrameCount];
                image.sprite = currentFrame;

                currentFrameCount++;
                currentFrameCount = (currentFrameCount + count) % count;
                lastFrameTime = 0.0f;
            }

            lastFrameTime += Time.deltaTime;
        }
    }
}