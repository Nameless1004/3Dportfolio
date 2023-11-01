using RPG.Core;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace RPG.Util
{
    public static class ResourceCache
    {
        private static readonly Dictionary<string, UnityEngine.Object> dict = new Dictionary<string, UnityEngine.Object>();

        public static T Load<T>(string path) where T : UnityEngine.Object
        {
            if(dict.TryGetValue(path, out var obj) == true)
            {
                return dict[path] as T;
            }

            T loaded = Resources.Load<T>(path);
            Logger.Assert(loaded is not null, $"Invalid Prefab Path: {path}");
            dict.Add(path, loaded);
            return loaded;
        }
    }
    public static class MathGraph
    {
        /// <summary>
        /// factor 값이 높을수록 회전 반경이 조금씩 넓어집니다
        /// </summary>
        /// <param name="t"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static Vector2 GetSpiralPosition(float t, float speed = 1f, float factor = 9f)
        {
            // float x = factor * t * Mathf.Cos(t);
            // float y = factor * t * Mathf.Sin(t);
            t = t * speed;
            float x = Mathf.Exp(t / factor) * Mathf.Sin(t);
            float y = Mathf.Exp(t / factor) * Mathf.Cos(t);
            return new Vector2(x, y);
        }


        /// <summary>
        /// factor 값이 높을수록 회전 반경이 조금씩 넓어집니다
        /// </summary>
        /// <param name="t"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static Vector3 GetSpiralPosition(Vector3 position, float t, float speed = 1f, float factor = 9f)
        {
            // float x = factor * t * Mathf.Cos(t);
            // float y = factor * t * Mathf.Sin(t);
            Vector2 sprial = GetSpiralPosition(t, speed, factor);
            Vector3 result = new Vector3(position.x + sprial.x, position.y, position.z + sprial.y);
            return result;
        }
    }

    public static class JsonUtil
    {
        public static string Decrypt(string textToDecrypt, string key)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();

            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;

            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;

            byte[] encryptedData = Convert.FromBase64String(textToDecrypt);
            byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }

            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;

            byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return Encoding.UTF8.GetString(plainText);
        }

        public static string Encrypt(string textToEncrypt, string key)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;

            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;
            byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }

            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;
            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
            return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
        }
    }

    public static class Utility
    {

        public static Transform FindNearestObject(Transform initiator, float range, int layerMask)
        {
            var collided = Physics.OverlapSphere(initiator.position, range, layerMask);
            Collider nearest = null;
            if (collided.Length > 0)
            {
                nearest = collided[0];
                float minDist = float.MaxValue;

                foreach (var enemy in collided)
                {
                    float dist = (initiator.position - enemy.transform.position).sqrMagnitude;
                    if (minDist > dist)
                    {
                        nearest = enemy;
                        minDist = dist;
                    }
                }
                return nearest.transform;
            }
            else
            {
                return null;
            }
        }
    }
}
