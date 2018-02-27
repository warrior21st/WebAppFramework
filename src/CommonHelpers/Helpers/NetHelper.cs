using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelpers.Helpers
{
    /// <summary>
    /// 网络帮助类
    /// </summary>
    public static class NetHelper
    {
        /// <summary>
        /// get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramsDic"></param>
        /// <param name="timeoutSeconds"></param>
        /// <param name="contentType"></param>
        /// <param name="responseEncoding"></param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url, Dictionary<string, object> paramsDic = null, int timeoutSeconds = 60, string contentType = null, string responseEncoding = "UTF-8")
        {
            url = GenerateGetUrl(url, paramsDic);
            string result = null;
            using (var stream = await GetStreamAsync(url, timeoutSeconds, contentType).ConfigureAwait(false))
            using (var sr = new StreamReader(stream, Encoding.GetEncoding(responseEncoding)))
            {
                result = sr.ReadToEnd();
            }

            return result;
        }

        /// <summary>
        /// get请求，返回响应流
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeoutSeconds"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static async Task<Stream> GetStreamAsync(string url, int timeoutSeconds = 60, string contentType = null)
        {
            Stream stream = null;
            var headers = GenerateDefaultHeaders(contentType);
            using (HttpClient client = GetClient("GET", timeoutSeconds, headers))
            {
                stream = await client.GetStreamAsync(new Uri(url, UriKind.Absolute)).ConfigureAwait(false);
            }

            return stream;
        }

        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <param name="timeoutSeconds"></param>
        /// <param name="contentType"></param>
        /// <param name="responseEncoding"></param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string url, string content, int timeoutSeconds = 60, string contentType = null, string responseEncoding = "UTF-8")
        {
            return await PostAsync(url, Encoding.UTF8.GetBytes(content), timeoutSeconds, contentType, responseEncoding).ConfigureAwait(false);
        }

        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="contentBytes"></param>
        /// <param name="timeoutSeconds"></param>
        /// <param name="contentType"></param>
        /// <param name="responseEncoding"></param>
        /// <returns></returns>

        public static async Task<string> PostAsync(string url, byte[] contentBytes, int timeoutSeconds = 60, string contentType = null, string responseEncoding = "UTF-8")
        {
            string result = null;
            using (var stream = await PostStreamAsync(url, contentBytes, timeoutSeconds, contentType).ConfigureAwait(false))
            using (var sr = new StreamReader(stream, Encoding.GetEncoding(responseEncoding)))
            {
                result = sr.ReadToEnd();
            }

            return result;
        }

        /// <summary>
        /// 获取post流
        /// </summary>
        /// <param name="url"></param>
        /// <param name="contentBytes"></param>
        /// <param name="timeoutSeconds"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static async Task<Stream> PostStreamAsync(string url, byte[] contentBytes, int timeoutSeconds = 60, string contentType = null)
        {
            Stream stream = null;
            var headers = GenerateDefaultHeaders(contentType);
            using (HttpClient client = GetClient("POST", timeoutSeconds, headers))
            using (var resMsg = await client.PostAsync(new Uri(url, UriKind.Absolute), new ByteArrayContent(contentBytes)).ConfigureAwait(false))
            {
                if (resMsg.IsSuccessStatusCode)
                {
                    stream = await resMsg.Content.ReadAsStreamAsync().ConfigureAwait(false);
                }
                else
                    throw new Exception($"{url} POST request failure,status code:{resMsg.StatusCode}");
            }

            return stream;
        }

        /// <summary>
        /// 获取请求对象
        /// </summary>
        /// <param name="method"></param>
        /// <param name="timeoutSeconds"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static HttpClient GetClient(string method, int timeoutSeconds, Dictionary<string, object> headers = null)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Method", method);
            client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
            if (headers != null)
            {
                foreach (var kvp in headers)
                {
                    client.DefaultRequestHeaders.Add(kvp.Key, kvp.Value.ToString());
                }
            }

            return client;
        }

        /// <summary>
        /// 生成get url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramsDic"></param>
        /// <returns></returns>
        private static string GenerateGetUrl(string url, Dictionary<string, object> paramsDic)
        {
            var result = url;
            if (paramsDic != null && paramsDic.Count > 0)
            {
                foreach (var kvp in paramsDic)
                {
                    result = UrlHelper.AddQueryParamToUrl(result, kvp.Key, kvp.Value.ToString());
                }
            }

            return result;
        }

        /// <summary>
        /// 生成默认头信息
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        private static Dictionary<string, object> GenerateDefaultHeaders(string contentType = null)
        {
            var dic = new Dictionary<string, object>();
            dic.Add("KeepAlive", "false");
            dic.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/34.0.1847.116 Safari/537.36");
            if (!string.IsNullOrWhiteSpace(contentType))
                dic.Add("Content-Type", contentType);

            return dic;
        }

        /// <summary>
        /// 使用httprequest的get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramsDic"></param>
        /// <param name="timeoutSeconds"></param>
        /// <param name="contentType"></param>
        /// <param name="responseEncoding"></param>
        /// <returns></returns>
        public static async Task<string> GetByHttpWebRequestAsync(string url, Dictionary<string, object> paramsDic, int timeoutSeconds = 60, string contentType = null, string responseEncoding = "UTF-8")
        {
            try
            {
                string result = null;
                url = GenerateGetUrl(url, paramsDic);
                var headers = GenerateDefaultHeaders();
                var request = GetHttpWebRequest(url, "GET", timeoutSeconds, headers);
                if (!string.IsNullOrWhiteSpace(contentType))
                    request.ContentType = contentType;

                var response = (HttpWebResponse)await request.GetResponseAsync().ConfigureAwait(false);
                result = GetStringFromResponse(response, responseEncoding);

                return result;
            }
            catch (WebException ex)
            {
                using (var response = (HttpWebResponse)ex.Response)
                {
                    var detailInfo = GetStringFromResponse(response);
                    throw new WebException($"{url} GET request faildure,status code:{response.StatusCode},detail:{detailInfo}");
                }
            }
        }

        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <param name="timeoutSeconds"></param>
        /// <param name="contentType"></param>
        /// <param name="responseEncoding"></param>
        /// <returns></returns>
        public static async Task<string> PostByHttpWebRequestAsync(string url, string content, int timeoutSeconds = 60, string contentType = null, string responseEncoding = "UTF-8")
        {
            return await PostByHttpWebRequestAsync(url, Encoding.UTF8.GetBytes(content), timeoutSeconds, contentType, responseEncoding);
        }

        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="contentBytes"></param>
        /// <param name="timeoutSeconds"></param>
        /// <param name="contentType"></param>
        /// <param name="responseEncoding"></param>
        /// <returns></returns>
        public static async Task<string> PostByHttpWebRequestAsync(string url, byte[] contentBytes, int timeoutSeconds = 60, string contentType = null, string responseEncoding = "UTF-8")
        {
            try
            {
                string result = null;
                var headers = GenerateDefaultHeaders();
                var request = GetHttpWebRequest(url, "GET", timeoutSeconds, headers);
                request.ContentLength = contentBytes.Length;
                if (!string.IsNullOrWhiteSpace(contentType))
                    request.ContentType = contentType;

                using (var reqStream = await request.GetRequestStreamAsync().ConfigureAwait(false))
                {
                    await reqStream.WriteAsync(contentBytes, 0, contentBytes.Length);
                    using (var response = (HttpWebResponse)(await request.GetResponseAsync().ConfigureAwait(false)))
                    {
                        result = GetStringFromResponse(response, responseEncoding);
                    }
                }

                return result;
            }
            catch (WebException ex)
            {
                using (var response = (HttpWebResponse)ex.Response)
                {
                    var detailInfo = GetStringFromResponse(response);
                    throw new WebException($"{url} POST request faildure,status code:{response.StatusCode},detail:{detailInfo}");
                }
            }
        }

        /// <summary>
        /// 获取请求对象
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="timeoutSeconds"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        private static HttpWebRequest GetHttpWebRequest(string url, string method, int timeoutSeconds, Dictionary<string, object> headers)
        {
            var request = WebRequest.CreateHttp(new Uri(url, UriKind.Absolute));
            request.Method = method;
            request.Timeout = timeoutSeconds * 1000;
            foreach (var kvp in headers)
            {
                request.Headers.Add(kvp.Key, kvp.Value.ToString());
            }

            return request;
        }

        /// <summary>
        /// 从响应流中获取字符串
        /// </summary>
        /// <param name="response"></param>
        /// <param name="responseEncoding"></param>
        /// <returns></returns>
        private static string GetStringFromResponse(HttpWebResponse response, string responseEncoding = "UTF-8")
        {
            using (var stream = response.GetResponseStream())
            using (var sr = new StreamReader(stream, Encoding.GetEncoding(responseEncoding)))
            {
                return sr.ReadToEnd();
            }
        }

        /// <summary>
        /// 获取指定handler的client
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static HttpClient GetClientByHandler(HttpClientHandler handler)
        {
            var client= new HttpClient(handler);

            return client.AddCommonHeaders();
        }

        /// <summary>
        /// 添加常用头信息
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static HttpClient AddCommonHeaders(this HttpClient client)
        {
            client.DefaultRequestHeaders.Add("KeepAlive", "false");
            client.DefaultRequestHeaders.Add("UserAgent", "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/34.0.1847.116 Safari/537.36");

            return client;
        }
    }
}
