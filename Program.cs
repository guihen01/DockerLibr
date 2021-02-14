using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;

namespace DockerLibr
{
    public static partial class Dck
    {
        /// <summary>
        /// Get the pull count on the Docker repository (repname is the Docker repository name)
        /// </summary>
        /// <param name="repname"> name of the Docker repository on whish you want to know the pulls number </param>
        /// <returns>The count number of pulls </returns>
        public static async System.Threading.Tasks.Task<int> DockPullCount(string repname)
        {
            string url;
            url = "https://hub.docker.com/v2/repositories/" + repname;

            string Response;
            Response = await GetHttpRep(url);
#if DEBUG
            Console.WriteLine(Response);
#endif
            //Parsing the response to get the field "pull_count"
            //----------------------------------------------------
            int Count;
            Count = ParseJson(Response);

            return Count;
        }

        /// <summary>
        /// Get the informations on the Docker repository (repname is the Docker repository name)
        /// </summary>
        /// <param name="repname"> name of the Docker repository on whish you want to know the pulls number </param>
        /// <returns>The Response details  </returns>
        public static async System.Threading.Tasks.Task<string> DockerREpInfos(string repname)
        {
            string url;
            url = "https://hub.docker.com/v2/repositories/" + repname;

            string Response;
            Response = await GetHttpRep(url);
#if DEBUG
            Console.WriteLine(Response);
#endif
            return Response;
        }

        /// <summary>
        /// Write informations obtained from Docker Hub to a file (format json)
        /// </summary>
        /// <param name="Data"> Data in Json format, to write to file </param>
        /// <param name="filename"> name of the data file </param>
        /// <returns></returns>
        public static void DockerWrToJson(string Data, string filename)
        {
            string dir = Directory.GetCurrentDirectory();
            string path = dir + "/" + filename + ".json";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (var tw = new StreamWriter(path, true))
            {
                tw.WriteLine(Data.ToString());
                tw.Close();
            }
#if DEBUG
            Console.WriteLine(Data);
#endif
            
        }

        /// <summary>
        /// Write informations obtained from Docker Hub to a file (format txt)
        /// </summary>
        /// <param name="Ob"> Object of type JObject Json, to write to text file </param>
        /// <param name="filename"> name of the data file </param>
        /// <returns></returns>
        public static void DockerWrToTxt(JObject Ob, string filename)
        {
            string dir = Directory.GetCurrentDirectory();
            string path = dir + "/" + filename + ".txt";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (var tw = new StreamWriter(path, true))
            {
                tw.WriteLine(Ob.ToString());
                tw.Close();
            }
        }



        /// <summary>
        /// Parsing the response to get the field "pull_count
        /// </summary>
        /// <param name="Response"></param>
        /// <returns></returns>
        public static int ParseJson(string Response)
        {
            int Count = 0;
            //Parsing the response to get the field "pull_count"
            //-------------------------------------------------

            JObject Ob;
            Ob = JObject.Parse(Response);

#if DEBUG
            using (var tw1 = new StreamWriter("Response.txt", true))
            {
                tw1.WriteLine(Ob.ToString());
                tw1.Close();
            }
#endif
            string name = Ob["pull_count"].ToString();

#if DEBUG
            Console.WriteLine("count = {0}", name);
#endif
            Count = Convert.ToInt32(name);
            

            return Count;
        }

        /// <summary>
        /// Simple Get Http Request with no authorization needed 
        /// </summary>
        /// <param name="url"> url end point of the web server on which to send the Rest API POST Request</param>
        /// <returns></returns>
        public static async Task<string> GetHttpRep(string url)
        {
            var client = new HttpClient();
            
            HttpResponseMessage response;
            response = new HttpResponseMessage();
            string result = " ";
            try
            {
                response = await client.GetAsync(url);
#if DEBUG
                Console.WriteLine(response.StatusCode);
#endif
                if (response == null)
                {
                    throw new ArgumentNullException();
                }
                if (response.StatusCode == HttpStatusCode.Unauthorized) //https://docs.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=net-5.0
                {
                    throw new Unauthorized();
                }

                result = await response.Content.ReadAsStringAsync();
                client.Dispose();
            }
            catch (Unauthorized e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("account not authorized to this sever or bad account ");

            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Response is null");
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);

            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("The requestUri must be an absolute URI or BaseAddress must be set.");
                Console.WriteLine(e.Message);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("The request failed due to an underlying issue such as network connectivity, DNS failure, server certificate validation or timeout");
                Console.WriteLine(e.Message);
            }
            catch (TaskCanceledException e)
            {
                Console.WriteLine(".NET Core and .NET 5.0 and later only: The request failed due to timeout.");
                Console.WriteLine(e.Message);
            }

            return result;
        }

        [Serializable]
        internal class Unauthorized : Exception
        {
            public Unauthorized()
            {
            }

            public Unauthorized(string message) : base(message)
            {
            }

            public Unauthorized(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected Unauthorized(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }

    }
}
