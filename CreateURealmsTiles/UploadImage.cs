using System;
using System.Text;
using System.IO;
using System.Net;
using System.Collections.Specialized;
using Newtonsoft.Json.Linq;

namespace CreateURealmsTiles
{
    class UploadImage
    {
       
        static public string UploadImageToImgur(string file)
        {

            string jsonResults = uploadimage(file);
            if (jsonResults == null)
            {
                Functions.WriteToLogFile("An error has occurred. Press any key to close the console.");
                Console.ReadKey();
                Environment.Exit(0);
            }

            string url = getURLFromJson(jsonResults);
            return url;

        }


        static public string getURLFromJson(string jsonResults)
        {
            dynamic stuff = JObject.Parse(jsonResults);

            string url = stuff.data.link;
            return url;
        }

        static public string uploadimage(string filename)
        {
            try
            {
                var file = File.ReadAllBytes(filename);

                using (var w = new WebClient())
                {
                    var values = new NameValueCollection
                    {
                        {"image", Convert.ToBase64String(file)},
                        {"type", "base64"}
                    };
                    //client.Headers.Add("Authorization", "BEARER " + accessToken);
                    w.Headers.Add("Authorization", "Client-ID c927064c3cd35e5");
                    var response = w.UploadValues("https://api.imgur.com/3/image", values);

                    string jsonResults = Encoding.UTF8.GetString(response);
                    return jsonResults;
                }
            }
            catch (Exception e)
            {
                Functions.WriteToLogFile(e.ToString());
            }
            return null;

        }
    }
}
