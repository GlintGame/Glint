using System.Collections;
using System;
using UnityEngine;

namespace utils
{
    public class FadeFunc
    {
        public delegate void ColorDelegate(Color32 color);

        public static IEnumerator DoFade(ColorDelegate colorDelegate, Color32 fromColor, Color32 toColor, float fadeDuration)
        {
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
