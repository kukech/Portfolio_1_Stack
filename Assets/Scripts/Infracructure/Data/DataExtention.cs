﻿using UnityEngine;

namespace Assets.Scripts.Infracructure.Data
{
    public static class DataExtention
    {
        public static Vector3 AddY(this Vector3 vector, float y)
        {
            vector.y += y;
            return vector;
        }

        public static T ToDeserialized<T>(this string json) =>
            JsonUtility.FromJson<T>(json);
    }
}
