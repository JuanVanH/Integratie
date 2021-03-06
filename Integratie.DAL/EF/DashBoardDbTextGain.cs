﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Integratie.DAL.EF
{
    public class DashBoardDbTextGain
    {
        public HttpWebRequest httpWebRequest { get; set; }

        public DashBoardDbTextGain()
        {
            httpWebRequest = (HttpWebRequest)WebRequest.Create("https://kdg.textgain.com/query");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("X-API-Key", "aEN3K6VJPEoh3sMp9ZVA73kkr");
        }
        
        public String postJson()
        {
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                
                string stream=streamReader.ReadToEnd().Replace("\"geo\": false", "\"geo\": [null, null]").Replace("[","\"[").Replace("]", "]\"").Trim('"');

                char[] test = new char[] { '[',']'};
                string[] filter = stream.Split(test);
                
                for (int i = 2; i < filter.Length; i = i + 2)
                {
                    filter[i]=filter[i].Replace("\"",string.Empty);
                }

                stream = "[" + string.Join("", filter) + "]";
                
                return stream;
            }
        }
    }
}
