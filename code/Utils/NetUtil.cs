using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ZI.Utils
{
    class NetUtil
    {
        /// <summary>
        /// 上传
        /// </summary>
        public static void uploadImgToFtp()
        {
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://200.197.17.79");
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential("zi", "hilong.com");

            // Copy the contents of the file to the request stream.
            string[] files = Directory.GetFiles("images", ".png", SearchOption.AllDirectories);
            
            foreach (string file in files)
            {
                StreamReader sourceStream = new StreamReader(file, true);
                byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                sourceStream.Close();
                request.ContentLength = fileContents.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);
                response.Close();
            }               

        }
    }
}
