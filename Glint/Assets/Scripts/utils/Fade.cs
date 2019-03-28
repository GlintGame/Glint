using System.Collections;
using System;
using UnityEngine;

namespace utils
{
    public class FadeFunc
    {
        public delegate void ColorDelegate(Color32 color);

        public static IEnumerator DoFade(ColorDelegate colorDelegate, uint fromOpacity, uint toOpacity, float fadeDuration)
        {
            Color32 fromColor = new Color32(0, 0, 0, Convert.ToByte(fromOpacity));
            Color32 toColor = new Color32(0, 0, 0, Convert.ToByte(toOpacity));
            float currentTime = 0f;

            while (currentTime <= fadeDuration)
            {
                float timeProportion = currentTime / fadeDuration;

                Color32 newColor = Color.Lerp(fromColor, toColor, timeProportion);

                colorDelegate(newColor);
                currentTime += Time.timeScale == 0f ? Time.fixedUnscaledDeltaTime : Time.fixedDeltaTime;

                yield return null;
            }
        }
    }
}
