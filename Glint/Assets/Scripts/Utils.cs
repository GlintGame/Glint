using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace utils
{
    // espace de fonction et d'objets utilitaires 

    public static class Extentions
    {
        public static List<T> EnumToList<T>()
        {
            Type enumType = typeof(T);

            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            Array enumValArray = Enum.GetValues(enumType);

            List<T> enumValList = new List<T>(enumValArray.Length);

            foreach (int val in enumValArray)
            {
                enumValList.Add((T)Enum.Parse(enumType, val.ToString()));
            }

            return enumValList;
        }
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


    // contient des fonction de Coroutine a utiliser dans des coroutines
    public class Coroutine
    {
        // singleton mechanic
        public static Coroutine instance;
        public static Coroutine Do
        {
            get
            {
                if (instance == null)
                    instance = new Coroutine();

                return instance;
            }
        }
        private Coroutine() { }
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

        public IEnumerator StopLookAhead(float seconds)
        {

            var CameraParams = GameObject
                .FindGameObjectsWithTag("CinemachineCam")[0]
                .GetComponent<CinemachineVirtualCamera>()
                .GetComponentPipeline()[0]
                as CinemachineFramingTransposer;

            float baseLookAheadTime = CameraParams.m_LookaheadTime;
            CameraParams.m_LookaheadTime = 0;

            yield return new WaitForSeconds(seconds);

            while(CameraParams.m_LookaheadTime < baseLookAheadTime)
            {
                CameraParams.m_LookaheadTime = Mathf.Lerp(CameraParams.m_LookaheadTime, baseLookAheadTime, 0.5f);
                yield return null;
            }
        }
    }    
}

