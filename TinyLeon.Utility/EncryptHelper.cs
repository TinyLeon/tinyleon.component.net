using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TinyLeon.Component.Utility
{
    public class EncryptHelper
    {
        //默认密钥向量
        private static byte[] _key1 = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        private static string keys = "tinyleon.utility";//密钥,128位

        /// <summary>
        /// AES加密算法
        /// </summary>
        /// <param name="plainText">明文字符串</param>
        /// <param name="strKey">密钥</param>
        /// <returns>返回加密后的密文字节数组</returns>
        public static string AESEncrypt(string plainText)
        {
            try
            {
                //分组加密算法
                SymmetricAlgorithm des = Rijndael.Create();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);//得到需要加密的字节数组
                //设置密钥及密钥向量
                des.Key = Encoding.UTF8.GetBytes(keys);
                des.IV = _key1;
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                byte[] cipherBytes = ms.ToArray();//得到加密后的字节数组
                cs.Close();
                ms.Close();
                return BitConverter.ToString(cipherBytes).Replace("-", "").ToLower();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="str">密文</param>
        /// <returns>返回解密后的字符串</returns>
        public static string AESDecrypt(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return string.Empty;
            }
            string strResult = string.Empty;
            try
            {
                byte[] data = new byte[(str.Length) / 2];
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = (byte)(
                       "0123456789abcdef".IndexOf(str[i * 2]) * 16 +
                       "0123456789abcdef".IndexOf(str[i * 2 + 1])
                    );
                }

                SymmetricAlgorithm des = Rijndael.Create();
                des.Key = Encoding.UTF8.GetBytes(keys);
                des.IV = _key1;
                byte[] decryptBytes = new byte[data.Length];
                MemoryStream ms = new MemoryStream(data);
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read);
                StreamReader streamReader = new StreamReader(cs);
                strResult = streamReader.ReadToEnd();

                //cs.Read(decryptBytes, 0, decryptBytes.Length);
                cs.Close();
                ms.Close();
            }
            catch
            {
                return string.Empty;
            }
            return strResult;
        }
    }
}
