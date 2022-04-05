using UnityEngine;
using UnityEngine.UI;

namespace Sequencing
{
    [RequireComponent(typeof(RawImage))]
    public class WaveFormRenderer : MonoBehaviour
    {
        private SimpleTrackController simpleTrackController;
        private RawImage rawImage;

        private void Awake()
        {
            rawImage = GetComponent<RawImage>();
            
            simpleTrackController = GetComponentInParent<SimpleTrackController>();
        }

        private void Start()
        {
            SetUpRender();
        }

        private void SetUpRender()
        {
            var audioSourceTarget = simpleTrackController.TargetAudioSource;
            
            if (audioSourceTarget == null || audioSourceTarget.clip == null)
            {
                return;
            }
            
            var audioClipTarget = audioSourceTarget.clip;
            var imageRect = rawImage.GetComponent<RectTransform>().rect;
            rawImage.texture = PaintWaveformSpectrum(audioClipTarget, 0.5f, (int) imageRect.width,
                (int) imageRect.height, Color.white);
        }

        //Jacked from this guy https://answers.unity.com/questions/189886/displaying-an-audio-waveform-in-the-editor.html
        private Texture2D PaintWaveformSpectrum(AudioClip audio, float saturation, int width, int height, Color col) {
            Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
            float[] samples = new float[audio.samples];
            float[] waveform = new float[width];
            audio.GetData(samples, 0);
            int packSize = ( audio.samples / width ) + 1;
            int s = 0;
            for (int i = 0; i < audio.samples; i += packSize) {
                waveform[s] = Mathf.Abs(samples[i]);
                s++;
            }
 
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    tex.SetPixel(x, y, Color.black);
                }
            }
 
            for (int x = 0; x < waveform.Length; x++) {
                for (int y = 0; y <= waveform[x] * ((float)height * .75f); y++) {
                    tex.SetPixel(x, ( height / 2 ) + y, col);
                    tex.SetPixel(x, ( height / 2 ) - y, col);
                }
            }
            tex.Apply();
 
            return tex;
        }
    }
}