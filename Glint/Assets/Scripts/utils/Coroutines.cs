using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace utils
{
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

            Debug.Log(seconds);
            yield return new WaitForSeconds(seconds);

            float chrono = 0;

            while (CameraParams.m_LookaheadTime < baseLookAheadTime)
            {
                CameraParams.m_LookaheadTime = Ease.InOutCubic(chrono, 0, baseLookAheadTime, seconds);
                chrono += Time.deltaTime;
                yield return null;
            }
        }
    }
}
