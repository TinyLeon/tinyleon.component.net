using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyLeon.Component.Utility
{
    public class IOHelper
    {
        /// <summary>
        /// stream流转换为byte数组
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns></returns>
        public static byte[] ConvertStreamToBytes(Stream stream)
        {
            if (stream == null || stream.Length == 0)
                return null;
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }
        /// <summary>
        /// byte数组转换为流
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Stream ConvertBytesToStream(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return null;
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
        /// <summary>
        /// byte数组转换为base64Str
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ConvertBytesToBase64Str(byte[] bytes)
        {
            string base64Str = string.Empty;
            if (bytes == null || bytes.Length == 0)
                return base64Str;
            try
            {
                base64Str = Convert.ToBase64String(bytes);
            }
            catch
            {
                return base64Str;
            }
            return base64Str;
        }
        /// <summary>
        /// base64Str转换为byte数组
        /// </summary>
        /// <param name="base64Str"></param>
        /// <returns></returns>
        public static byte[] ConvertBase64StrToBytes(string base64Str)
        {
            byte[] bytes = null;
            if (string.IsNullOrEmpty(base64Str))
                return bytes;
            try
            {
                bytes = Convert.FromBase64String(base64Str);
            }
            catch
            {
                return bytes;
            }
            return bytes;
        }
        /// <summary>
        /// 流转换为文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileFullPath"></param>
        public static void StreamToFile(Stream stream, string fileFullPath)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            // 把 byte[] 写入文件
            FileStream fs = new FileStream(fileFullPath, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }
        /// <summary>
        /// 文件转换为流
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <returns></returns>
        public static Stream FileToStream(string fileFullPath)
        {
            FileStream fileStream = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[]
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        /// <summary>
        /// 从txt读取列表
        /// </summary>
        /// <param name="fileFullPath"></param>
        /// <returns></returns>
        public static List<string> ReadContentFromTxt(string fileFullPath)
        {
            List<string> result = new List<string>();
            using (System.IO.StreamReader sr = new System.IO.StreamReader(fileFullPath))
            {
                string str;
                while ((str = sr.ReadLine()) != null)
                {
                    result.Add(str);
                }
            }
            return result;
        }
        /// <summary>
        /// 将信息写入文件
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="fileFullPath"></param>
        /// <param name="isAppend"></param>
        public static void WriteContentIntoTxt(List<string> contents, string fileFullPath, bool isAppend)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileFullPath, isAppend))
            {
                if (contents == null || !contents.Any() || string.IsNullOrEmpty(contents.FirstOrDefault()))
                {
                    file.WriteLine(string.Empty);
                    return;
                }
                foreach (var item in contents)
                {
                    file.WriteLine(item);
                }
            }
        }
    }
}
