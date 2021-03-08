using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;

namespace LarryWeatherAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            //API
            JArray jsondata = getJson("https://opendata.cwb.gov.tw/api/v1/rest/datastore/F-C0032-001?&Authorization=CWB-987F9E27-2C33-4961-9FA3-F6789FDE1FE4");

            foreach (JObject data in jsondata)
            {
                string loactionname = (string)data["locationName"]; //縣市
                string weathdescrible = (string)data["weatherElement"][0]["time"][0]["parameter"]["parameterName"]; //天氣狀況
                string pop = (string)data["weatherElement"][1]["time"][0]["parameter"]["parameterName"];  //降雨%
                string mintemperature = (string)data["weatherElement"][2]["time"][0]["parameter"]["parameterName"]; //最低溫
                string maxtemperature = (string)data["weatherElement"][4]["time"][0]["parameter"]["parameterName"]; //最高溫
                Console.WriteLine(loactionname + " 天氣狀況:" + weathdescrible + " 溫度區間:" + mintemperature + "°c-" + maxtemperature + "°c 降雨%:" + pop + "%");
            }
            Console.ReadLine();
        }

        static public JArray getJson(string uri)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri); 
            req.Timeout = 10000; 
            req.Method = "GET"; 
            HttpWebResponse respone = (HttpWebResponse)req.GetResponse(); 
            StreamReader streamReader = new StreamReader(respone.GetResponseStream(), Encoding.UTF8); 
            string result = streamReader.ReadToEnd(); 
            respone.Close();
            streamReader.Close();
            JObject jsondata = JsonConvert.DeserializeObject<JObject>(result); 
            return (JArray)jsondata["records"]["location"]; 

        }
    }
}
