using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Cypter.Helper
{
    public static class AESHelper
    {
        public static string AesEncrypt(string str, string key, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, string EncordingModeStr = "Base64", string IV = null)
        {
            switch (EncordingModeStr)
            {
                case "UTF-8":
                    {
                        if (string.IsNullOrEmpty(str)) return null;
                        byte[] t = Encoding.UTF8.GetBytes(key);
                        Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);
                        RijndaelManaged rm = new RijndaelManaged
                        {
                            Key = t,
                            Padding = paddingMode,
                            Mode = cipherMode,
                        };
                        if (IV != "" && IV != null)
                        {
                            byte[] v = Encoding.UTF8.GetBytes(IV);
                            rm.IV = v;
                            ICryptoTransform cTransform = rm.CreateEncryptor(t, v);
                            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                            return Convert.ToBase64String(resultArray);
                        }

                        else
                        {
                            ICryptoTransform cTransform = rm.CreateEncryptor();
                            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                            return Convert.ToBase64String(resultArray);
                        }
                    }

                default:
                    {
                        if (string.IsNullOrEmpty(str)) return null;
                        byte[] t = Convert.FromBase64String(key);
                        Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);
                        RijndaelManaged rm = new RijndaelManaged
                        {
                            Key = t,
                            Padding = paddingMode,
                            Mode = cipherMode,

                        };
                        if (IV != "" && IV != null)
                        {
                            byte[] v = Convert.FromBase64String(IV);
                            ICryptoTransform cTransform = rm.CreateEncryptor(t, v);
                            rm.IV = v;
                            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                            return Convert.ToBase64String(resultArray);
                        }

                        else
                        {
                            ICryptoTransform cTransform = rm.CreateEncryptor();
                            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                            return Convert.ToBase64String(resultArray);
                        }
                    }
            }

        }


        public static string AesDecrypt(string str, string key, CipherMode cipherMode = CipherMode.CBC, PaddingMode paddingMode = PaddingMode.PKCS7, string EncordingModeStr = "Base64", string IV = null)
        {
            switch (EncordingModeStr)
            {
                case "UTF-8":
                    {
                        if (string.IsNullOrEmpty(str)) return null;
                        byte[] t = Encoding.UTF8.GetBytes(key);

                        Byte[] toEncryptArray = Convert.FromBase64String(str);
                        RijndaelManaged rm = new RijndaelManaged
                        {
                            Key = t,
                            Mode = cipherMode,
                            Padding = paddingMode,
                        };
                        if (IV != null && IV != "")
                        {
                            byte[] a = Encoding.UTF8.GetBytes(IV);
                            rm.IV = a;
                            ICryptoTransform cTransform = rm.CreateDecryptor(t, a);
                            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                            return Encoding.UTF8.GetString(resultArray);
                        }
                        else
                        {
                            ICryptoTransform cTransform = rm.CreateDecryptor();
                            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                            return Encoding.UTF8.GetString(resultArray);
                        }
                    }
                default:
                    {
                        if (string.IsNullOrEmpty(str)) return null;
                        byte[] t = Convert.FromBase64String(key);

                        Byte[] toEncryptArray = Convert.FromBase64String(str);
                        RijndaelManaged rm = new RijndaelManaged
                        {
                            Key = t,
                            Mode = cipherMode,
                            Padding = paddingMode,
                        };
                        if (IV != null && IV != "")
                        {
                            byte[] a = Convert.FromBase64String(IV);
                            rm.IV = a;
                            ICryptoTransform cTransform = rm.CreateDecryptor(t, a);
                            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                            return Encoding.UTF8.GetString(resultArray);
                        }
                        else
                        {
                            ICryptoTransform cTransform = rm.CreateDecryptor();
                            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                            return Encoding.UTF8.GetString(resultArray);
                        }
                    }
            }
        }




        public static void AesCreateKey(out string key, out string IV, string EncordingMode = "Base64")
        {
            switch (EncordingMode)
            {
                case "UTF-8":
                    {
                        Random random = new Random();
                        key=GetRandomString(16);
                        IV = GetRandomString(16);
                        break;
                    }
                default:
                    {
                        var aes = Aes.Create();
                        key = Convert.ToBase64String(aes.Key);
                        IV = Convert.ToBase64String(aes.IV);
                        break;
                    }

            }
        }

        private static RNGCryptoServiceProvider _random = new RNGCryptoServiceProvider();

        public static string GetRandomString(int stringlength)
        {
            return GetRandomString(null, stringlength);
        }     //获得长度为stringLength的随机字符串，以key为字母表
        public static string GetRandomString(string key, int stringLength)
        {
            if (key == null || key.Length < 8)
            {
                key = "abcdefghijklmnopqrstuvwxyz1234567890";
            }

            int length = key.Length;
            StringBuilder randomString = new StringBuilder(length);
            for (int i = 0; i < stringLength; ++i)
            {
                randomString.Append(key[SetRandomSeeds(length)]);
            }

            return randomString.ToString();
        }

        private static int SetRandomSeeds(int length)
        {
            decimal maxValue = (decimal)long.MaxValue;
            byte[] array = new byte[8];
            _random.GetBytes(array);

            return (int)(Math.Abs(BitConverter.ToInt64(array, 0)) / maxValue * length);
        }
    }
}
