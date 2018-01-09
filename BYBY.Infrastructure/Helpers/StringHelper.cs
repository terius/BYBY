﻿using System;
using System.ComponentModel;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace BYBY.Infrastructure.Helpers
{
    public class StringHelper
    {
        public static string Sha256(string plainText)
        {
            SHA256Managed _sha256 = new SHA256Managed();
            byte[] _cipherText = _sha256.ComputeHash(Encoding.Default.GetBytes(plainText));
            return Convert.ToBase64String(_cipherText);
        }

        /// <summary>
        /// 字符串的安全转换
        /// </summary>
        /// <param name="oInt"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public static string SafeGetStringFromObj(object oStr)
        {
            return oStr == null ? "" : oStr.ToString();
        }


    }
}
