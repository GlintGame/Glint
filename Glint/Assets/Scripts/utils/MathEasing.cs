using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace utils
{
    public static class Ease
    {
        // simple linear tweening - no easing, no acceleration


        public static float LinearTween(float time, float from, float to, float duration)
        {
            return to *time/ duration + from;
        }


        // quadratic easing in - accelerating from zero velocity


        public static float  InQuad(float time, float from, float to, float duration)
        {
           time/= duration;
            return to *time*time+ from;
        }


    // quadratic easing out - decelerating to zero velocity


        public static float  OutQuad(float time, float from, float to, float duration)
        {
           time/= duration;
            return -to *time* (time - 2) + from;
        }



        // quadratic easing in/out - acceleration until halfway, then deceleration


        public static float  InOutQuad(float time, float from, float to, float duration)
        {
           time/= duration / 2;
            if (time < 1) return to / 2 *time*time+ from;
            time--;
            return -to / 2 * (time * (time - 2) - 1) + from;
        }


        // cubic easing in - accelerating from zero velocity


        public static float  InCubic(float time, float from, float to, float duration)
        {
           time/= duration;
            return to *time*time*time+ from;
        }



        // cubic easing out - decelerating to zero velocity


        public static float  OutCubic(float time, float from, float to, float duration)
        {
           time/= duration;
            time--;
            return to * (time *time*time+ 1) + from;
        }



        // cubic easing in/out - acceleration until halfway, then deceleration


        public static float  InOutCubic(float time, float from, float to, float duration)
        {
           time/= duration / 2;
            if (time < 1) return to / 2 *time*time*time+ from;
           time-= 2;
            return to / 2 * (time *time*time+ 2) + from;
        }


        // quartic easing in - accelerating from zero velocity


        public static float  InQuart(float time, float from, float to, float duration)
        {
           time/= duration;
            return to *time*time*time*time+ from;
        }



        // quartic easing out - decelerating to zero velocity


        public static float  OutQuart(float time, float from, float to, float duration)
        {
           time/= duration;
            time--;
            return -to * (time *time*time*time- 1) + from;
        }



        // quartic easing in/out - acceleration until halfway, then deceleration


        public static float  InOutQuart(float time, float from, float to, float duration)
        {
           time/= duration / 2;
            if (time < 1) return to / 2 *time*time*time*time+ from;
           time-= 2;
            return -to / 2 * (time *time*time*time- 2) + from;
        }


        // quintic easing in - accelerating from zero velocity


        public static float  InQuint(float time, float from, float to, float duration)
        {
           time/= duration;
            return to *time*time*time*time*time+ from;
        }



        // quintic easing out - decelerating to zero velocity


        public static float OutQuint(float time, float from, float to, float duration)
        {
           time/= duration;
            time--;
            return to * (time *time*time*time*time+ 1) + from;
        }



        // quintic easing in/out - acceleration until halfway, then deceleration


        public static float  InOutQuint(float time, float from, float to, float duration)
        {
           time/= duration / 2;
            if (time < 1) return to / 2 *time*time*time*time*time+ from;
           time-= 2;
            return to / 2 * (time *time*time*time*time+ 2) + from;
        }


        // sinusoidal easing in - accelerating from zero velocity


        public static float  InSine(float time, float from, float to, float duration)
        {
            return -to * (float)Math.Cos(time / duration * ((float)Math.PI / 2)) + to + from;
        }



        // sinusoidal easing out - decelerating to zero velocity


        public static float  OutSine(float time, float from, float to, float duration)
        {
            return to * (float)Math.Sin(time / duration * ((float)Math.PI / 2)) + from;
        }



        // sinusoidal easing in/out - accelerating until halfway, then decelerating


        public static float  InOutSine(float time, float from, float to, float duration)
        {
            return -to / 2 * ((float)Math.Cos((float)Math.PI *time/ duration) - 1) + from;
        }



        // exponential easing in - accelerating from zero velocity


        public static float  InExpo(float time, float from, float to, float duration)
        {
            return to * (float)Math.Pow(2, 10 * (time / duration - 1)) + from;
        }



        // exponential easing out - decelerating to zero velocity


        public static float OutExpo(float time, float from, float to, float duration)
        {
            return to * (-(float)Math.Pow(2, -10 *time/ duration) + 1) + from;
        }



        // exponential easing in/out - accelerating until halfway, then decelerating


        public static float InOutExpo(float time, float from, float to, float duration)
        {
           time/= duration / 2;
            if (time < 1) return to / 2 * (float)Math.Pow(2, 10 * (time - 1)) + from;
            time--;
            return to / 2 * (-(float)Math.Pow(2, -10 * time) + 2) + from;
        }


        // circular easing in - accelerating from zero velocity


        public static float InCirc(float time, float from, float to, float duration)
        {
           time/= duration;
            return -to * ((float)Math.Sqrt(1 -time* time) - 1) + from;
        }



        // circular easing out - decelerating to zero velocity


        public static float OutCirc(float time, float from, float to, float duration)
        {
           time/= duration;
            time--;
            return to * (float)Math.Sqrt(1 -time* time) + from;
        }



        // circular easing in/out - acceleration until halfway, then deceleration


        public static float InOutCirc(float time, float from, float to, float duration)
        {
           time/= duration / 2;
            if (time < 1) return -to / 2 * ((float)Math.Sqrt(1 -time* time) - 1) + from;
           time-= 2;
            return to / 2 * ((float)Math.Sqrt(1 -time* time) + 1) + from;
        }
    }
}
