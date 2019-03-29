using System.Collections;
using UnityEngine;

namespace utils
{
    public class FadeFunc
    {
        public delegate void ColorDelegate(Color32 color);

        static Color32 white = new Color32(255, 255, 255, 255);
        static Color32 whiteT = new Color32(255, 255, 255, 0);
        static Color32 black = new Color32(0, 0, 0, 255);
        static Color32 blackT = new Color32(0, 0, 0, 0);

        public static IEnumerator DoFadeInWhite(ColorDelegate colorDelegate, float fadeDuration)
        { yield return FadeFunc.DoFade(colorDelegate, FadeFunc.white, FadeFunc.whiteT, fadeDuration); }

        public static IEnumerator DoFadeOutWhite(ColorDelegate colorDelegate, float fadeDuration)
        { yield return FadeFunc.DoFade(colorDelegate, FadeFunc.whiteT, FadeFunc.white, fadeDuration); }

        public static IEnumerator DoFadeInBlack(ColorDelegate colorDelegate, float fadeDuration)
        { yield return FadeFunc.DoFade(colorDelegate, FadeFunc.black, FadeFunc.blackT, fadeDuration); }

        public static IEnumerator DoFadeOutBlack(ColorDelegate colorDelegate, float fadeDuration)
        { yield return FadeFunc.DoFade(colorDelegate, FadeFunc.blackT, FadeFunc.black, fadeDuration); }

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
