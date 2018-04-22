using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RESTUtil
{
    public class RESTapi
    {

        private string baseUri;
        public RESTapi(string _baseUri) {
            this.baseUri = _baseUri;
        }

        // Get the REST API information from ist.rit.edu/api
        public string getRESTData(string url)
        {
            //const string baseUri = "http://ist.rit.edu/api";

            // connect to the API
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(baseUri + url);

            try
            {
                // wait and get the response for this web request
                WebResponse response = request.GetResponse();

                // Using the response stream from the web request
                // get the information requested
                using (Stream responseStream = response.GetResponseStream())
                {
                    // Get the information from the response
                    StreamReader reader =
                        new StreamReader(responseStream, Encoding.UTF8);

                    return reader.ReadToEnd();
                }

            }  // Just because we have an exception, we still have info
            catch (WebException we)      // for debugging 
            {
                // Get the response
                WebResponse err = we.Response;
                // get information out of the stream
                using (Stream responseStream =
                            err.GetResponseStream())
                {
                    // read what we have for an error
                    StreamReader r =
                            new StreamReader(responseStream, Encoding.UTF8);

                    // Do something with the error
                    string errorText = r.ReadToEnd();
                    Console.WriteLine("ERROR: " + errorText);

                    // Can't do anything more with this exception, throw it
                    // let it be someone else's problem
                    throw;
                }

            }
        }  // end getRestData()
    }
}
