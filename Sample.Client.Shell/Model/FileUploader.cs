using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;

namespace Sample.Client.Shell.Model
{
    public interface IFileUploader
    {
        void UploadFile(string filePath, string url);
    }

    public class HttpWebRequestFileUploader : IFileUploader
    {
        public void UploadFile(string filePath, string url)
        {
            NameValueCollection nvc = new NameValueCollection { { "id", "TTR" }, { "btn-submit-photo", "Upload" } };
            HttpUploadFile(url, filePath, "file", MimeMapping.GetMimeMapping(filePath), nvc);
        }

        private static void HttpUploadFile(string url, string file, string paramName, string contentType, NameValueCollection nvc)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            webRequest.Method = "POST";
            webRequest.KeepAlive = true;
            webRequest.Credentials = CredentialCache.DefaultCredentials;

            Stream rs = webRequest.GetRequestStream();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, paramName, file, contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse webResponse = null;
            try
            {
                webResponse = webRequest.GetResponse();
            }
            catch (Exception ex)
            {
                if (webResponse != null)
                {
                    webResponse.Close();
                }
            }
            finally
            {
                webRequest = null;
            }
        }
    }

    public class HttpClientUploader : IFileUploader
    {
        private readonly HttpClient _httpClient;

        public HttpClientUploader(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async void UploadFile(string filePath, string url)
        {
            var fi = new FileInfo(filePath);
            var form = new MultipartFormDataContent
            {
                {new StreamContent(fi.OpenRead()), "\"file\"", "\"" + fi.Name + "\""}
            };
            await _httpClient.PostAsync(url, form);                   
        }
    }
}