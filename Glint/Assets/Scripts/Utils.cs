using System;
using System.Collections;
using UnityEngine;

namespace utils
{
    // espace de fonction et d'objets utilitaires 

    public static class Extentions
    {

    }

    // type référence lorsqu'on peut pas utiliser les références.
    public class Ref<T>
    {
        public T Value { get; set; }
        public Ref(T reference)
        {
            this.Value = reference;
        }
    }


    // contient des fonction de transition a utiliser dans des coroutines
    public class Transition
    {
        // singleton mechanic
        public static Transition instance;
        public static Transition Do
        {
            get
            {
                if (instance == null)
                    instance = new Transition();

                return instance;
            }
        }
        private Transition() { }
        // 

        public IEnumerator Lerp(Ref<float> origin, float target, float smoothing)
        {
            origin.Value = Mathf.Lerp(origin.Value, target, smoothing);
            yield return null;
        }

        public IEnumerator Lerp(Ref<Vector2> origin, Vector2 target, float smoothing)
        {
            origin.Value = Vector2.Lerp(origin.Value, target, smoothing);
            yield return null;
        }

        public IEnumerator Wait(float time)
        {
            yield return new WaitForSeconds(time);
        }

        public IEnumerator Wait(float time, Action callback)
        {
            yield return new WaitForSeconds(time);
            callback();
        }
    }



}

