using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace utils
{
    // espace de fonction et d'objets utilitaires 

    public static class Extentions
    {
        public static string CamelCaseTo_snake_case(this string str)
        {
            char[] charArray = str.ToCharArray();
            for (int i = 0; i < charArray.Length - 1; i++)
            {
                if (char.IsUpper(charArray[i]))
                {
                    charArray[i] = char.ToLower(charArray[i]);
                    if (i != 0)
                    {
                        char[] newCharArray = new char[charArray.Length + 1];
                        int offset = 0;
                        for (int j = 0; j < newCharArray.Length - 1; j++)
                        {
                            if (j == i)
                            {
                                offset = 1;
                                newCharArray[j] = "_".ToCharArray()[0];
                            }
                            newCharArray[j + offset] = charArray[j];
                        }
                        charArray = newCharArray;
                    }
                }
            }
            return new string(charArray);
        }

        public static string SnakeToSpace(this string str)
        {
            string[] strSplit = str.Split("_".ToCharArray()[0]);
            string returningString = "";
            foreach (string sStr in strSplit)
            {
                returningString += sStr + " ";
            }
            return returningString.TrimEnd(" ".ToCharArray());
        }

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

