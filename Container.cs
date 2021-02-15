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
    /// <summary>
    /// Classe Container , for methods on containers
    /// </summary>
    public static class Cont
    {

        //GET /containers/json 
        /// <summary>
        /// List docker containers – Get the Docker container list 
        /// </summary>
        /// <param name="url"> name of the server url ( on whish you want to get the list of containers) </param>
        /// <returns>Container list </returns>
        public static async Task<String> DockContList(string url)
        {
            // url : adress of the local or remote server hosting the containers
            //(on OS unix,linux) curl --unix-socket /var/run/docker.sock http://localhost/containers/json?all=1
            //(on OS Windows ) curl http://localhost:2375/containers/json?all=1
            string url1;
            url1 = url + ":2375" + "/containers/json?all=1";
            string Response;
            Response = await Dck.GetHttpRep(url1);
            return Response;
        }

        

        


    }
}
