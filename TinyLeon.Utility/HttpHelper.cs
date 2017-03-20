using System;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.IO.Compression;

namespace TinyLeon.Component.Utility
{
    /// <summary>
    /// Http请求帮助类
    /// </summary>
    public class HttpHelper
    {
        #region 访问远程URL

        /// <summary>
        /// 通过HttpWebRequest方法从服务器取方法
        /// </summary>
        /// <param name="url">需请求的URL</param>
        /// <returns>返回的字符串</returns>
        public static string GetDataFromServer(string url)
        {
            return GetDataFromServer(url, string.Empty);
        }

        /// <summary>
        /// 通过HttpWebRequest方法从服务器取方法
        /// </summary>
        /// <param name="url">需请求的URL</param>
        /// <param name="inputCharset">字符集</param>
        /// <returns>返回的字符串</returns>
        public static string GetDataFromServer(string url, string inputCharset)
        {
            HttpWebRequest webRequest = null;
            try
            {
                webRequest = (HttpWebRequest)WebRequest.Create(url);
                using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (Stream resStream = response.GetResponseStream())
                    {
                        Encoding encoding = null;

                        if (!string.IsNullOrEmpty(inputCharset))
                        {
                            try
                            {
                                encoding = Encoding.GetEncoding(inputCharset);
                            }
                            catch (Exception)
                            {
                            }
                        }

                        StreamReader reader = null;

                        if (encoding != null)
                        {
                            reader = new StreamReader(resStream, encoding);
                        }
                        else
                        {
                            reader = new StreamReader(resStream);
                        }

                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        #endregion

        private static Encoding encoding = new UTF8Encoding();

        public static string GetRequest(string requestXml, string mQuestUrl)
        {
            return GetRequest(requestXml, mQuestUrl, 0);
        }

        /// <summary>
        /// 发起HTTP请求
        /// </summary>
        /// <param name="m_Doc"></param>
        /// <param name="mQuestUrl"></param>
        /// <returns></returns>
        public static string GetRequest(string requestXml, string mQuestUrl, int timeout)
        {
            string errMessage = string.Empty;
            string result = "";
            //Post请求地址
            try
            {
                HttpWebRequest m_Request = (HttpWebRequest)WebRequest.Create(mQuestUrl);
                //相应请求的参数
                byte[] data = Encoding.GetEncoding("UTF-8").GetBytes(requestXml);
                m_Request.Method = "Post";
                m_Request.ContentType = "application/x-www-form-urlencoded";
                m_Request.ContentLength = data.Length;
                m_Request.Headers.Add("Accept-Encoding", "gzip");
                if (timeout > 0)
                {
                    m_Request.Timeout = timeout;
                }
                //请求流
                Stream requestStream = m_Request.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();

                //响应流
                HttpWebResponse m_Response = (HttpWebResponse)m_Request.GetResponse();
                Stream st = m_Response.GetResponseStream();
                if (m_Response.ContentEncoding.ToLower().Contains("gzip"))
                {
                    st = new GZipStream(st, CompressionMode.Decompress);
                }

                StreamReader streamReader = new StreamReader(st, Encoding.GetEncoding("UTF-8"));
                //获取返回的信息
                result = streamReader.ReadToEnd();
                streamReader.Close();
            }
            catch (WebException webEx)
            {
                if (webEx.Status != WebExceptionStatus.Timeout)
                {
                    HttpWebResponse res = null;
                    try
                    {
                        res = (HttpWebResponse)webEx.Response;
                        StreamReader sr = new StreamReader(res.GetResponseStream(), encoding);
                        errMessage = sr.ReadToEnd();
                        res.Close();
                    }
                    catch (Exception ex)
                    {
                        errMessage = ex.Message;
                        throw ex;
                    }
                    throw webEx;
                }
                else
                {
                    result = string.Empty;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// http请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="reqContentType">请求内容类型：application/json application/xml application/x-www-form-urlencoded </param>
        /// <returns></returns>
        public static string GetResponse(string url, string data, string reqContentType)
        {
            string result = string.Empty;

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new System.Exception("url为空");
            }

            data = data ?? string.Empty;
            byte[] packet = Encoding.UTF8.GetBytes(data);

            var req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = reqContentType;
            req.ContentLength = packet.Length;
            req.Timeout = 5000;
            if (packet.Length > 0)
            {
                using (var reqStream = req.GetRequestStream())
                {
                    reqStream.Write(packet, 0, packet.Length);
                }
            }
            using (var res = req.GetResponse())
            {
                var response = res as HttpWebResponse;
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    using (var resStream = response.GetResponseStream())
                    {
                        if (resStream != null)
                        {
                            var reader = new StreamReader(resStream, Encoding.UTF8);
                            result = reader.ReadToEnd();
                        }
                    }
                }
            }

            return result;
        }
    }
}