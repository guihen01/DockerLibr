//define #DEBUG

using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System;

namespace DockerLibr
{
    public static partial class Dck
    {
        //GET /v2/repositories/(namespace)/(repository)/tags
        /// <summary>
        /// Obtain all tags or specific tag of a repository: (repname is the Docker repository name) in format json compact
        /// </summary>
        /// <param name="repname"> name of the Docker repository on whish you want to get all tags </param>
        /// <returns>List of images tags  </returns>
        public static async System.Threading.Tasks.Task<String> DockGetTags(string repname)
        {
            string url;
            url = "https://hub.docker.com/v2/repositories/" + repname + "/tags";

            string Response;
            Response = await GetHttpRep(url);

//#if DEBUG
            //Console.WriteLine(Response);   // liste des tags au format json 
            //Console.WriteLine("   ");
            //JObject Ob;
            //Ob = JObject.Parse(Response);
            //Console.WriteLine(Ob.ToString());  // liste des tags au format texte (plus lisible) 
//#endif


            return Response;
        }

        //GET /v2/repositories/(namespace)/(repository)/tags
        /// <summary>
        /// Obtain all tags or specific tag of a repository: (repname is the Docker repository name) in format json text
        /// </summary>
        /// <param name="repname"> name of the Docker repository on whish you want to get all tags </param>
        /// <returns>List of images tags  </returns>
        public static async System.Threading.Tasks.Task<String> DockGetTagsOb(string repname)
        {
            string url;
            url = "https://hub.docker.com/v2/repositories/" + repname + "/tags";

            string Response;
            Response = await GetHttpRep(url);

            JObject Ob;
            Ob = JObject.Parse(Response);
            
            string Rep;
            Rep = Ob.ToString(); // liste des tags au format texte (plus lisible) 

//#if DEBUG
            //Console.WriteLine(Response);   // liste des tags au format json 
            //Console.WriteLine("   ");
            //Console.WriteLine(Rep);        // liste des tags au format texte (plus lisible) 
//#endif
            return Rep;

        }
    }
}
