using System;
using System.Collections.Generic;
using UnityEngine;

namespace utils
{
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

        public static void DrawLine(this Texture2D a_Texture, Vector2 v1, Vector2 v2, Color a_Color)
        {
            float xPix = v1.x;
            float yPix = v1.y;

            float width = v2.x - v1.x;
            float height = v2.y - v1.y;
            float length = Mathf.Abs(width);

            if (Mathf.Abs(height) > length)
            {
                length = Mathf.Abs(height);
            }

            int intLength = (int)length;
            float dx = width / (float)length;
            float dy = height / (float)length;

            for (int i = 0; i <= intLength; i++)
            {
                a_Texture.SetPixel((int)xPix, (int)yPix, a_Color);

                xPix += dx;
                yPix += dy;
            }
        }

        public static Vector2 Rotate(this Vector2 v, float rad)
        {
            float sin = Mathf.Sin(rad);
            float cos = Mathf.Cos(rad);

            float tx = v.x;
            float ty = v.y;
            v.x = (cos * tx) - (sin * ty);
            v.y = (sin * tx) + (cos * ty);
            return v;
        }
    }
}
