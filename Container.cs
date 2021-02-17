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
        //(on OS unix,linux) curl --unix-socket /var/run/docker.sock http://localhost/containers/json?all=1
        //(on OS Windows ) curl http://localhost:2375/containers/json?all=1
        /// <summary>
        /// List docker containers – Get the Docker container list 
        /// </summary>
        /// <param name="url"> name of the server url ( on whish you want to get the list of containers)
        /// examples : host://localhost   or http://192.168.58.1 </param>
        /// <returns>Container list </returns>
        public static async Task<String> DockContList(string url)
        {
            // url : adress of the local or remote server hosting the containers
            string url1;
            url1 = url + ":2375" + "/containers/json?all=1";
            string Response;
            Response = await Dck.GetHttpRep(url1);
            return Response;
        }

        //GET /containers/json?filters={"status": ["paused"]} 
        //(on OS unix,linux) curl --unix-socket /var/run/docker.sock http://localhost/containers/json?filters=status(paused)
        //(on OS Windows ) curl http://localhost:2375/containers/json?filters={"status":["paused"]}
        /// <summary>
        /// List only docker containers with a status : paused – Get the Docker container list 
        /// </summary>
        /// <param name="url"> name of the server url ( on whish you want to get the list of paused containers)
        /// examples : host://localhost   or http://192.168.58.1 </param>
        /// <returns> paused containers list </returns>
        public static async Task<String> DockContPaused(string url)
        {
            // url : adress of the local or remote server hosting the containers
            string url1;
            url1 = url + ":2375" + "/containers/json?filters={\"status\":[\"paused\"]}";
            string Response;
            Response = await Dck.GetHttpRep(url1);
            return Response;
        }

        //GET /containers/json?filters={"status": ["running"]} 
        //GET /containers/json? filters = { "status":["running"]
        //(on OS unix,linux) curl --unix-socket /var/run/docker.sock http://localhost/containers/json?filters=status=(running)
        //(on OS Windows ) curl http://localhost:2375/containers/json?filters={"status":["running"]}
        /// <summary>
        /// List only docker containers with a status : running – Get the Docker container list 
        /// </summary>
        /// <param name="url"> name of the server url
        /// examples : host://localhost   or http://192.168.58.1 </param>
        /// <returns> running containers list </returns>
        public static async Task<String> DockContRunning(string url)
        {
            // url : adress of the local or remote server hosting the containers
            string url1;
            url1 = url + ":2375" + "/containers/json?filters={\"status\":[\"running\"]}";
            string Response;
            Response = await Dck.GetHttpRep(url1);
            return Response;
        }


        //GET /containers/json?filters=network=(<network id> or <network name>)
        //(on OS unix,linux) curl --unix-socket /var/run/docker.sock http://localhost/containers/json?filters=(networkname)
        //(on OS Windows ) curl http://localhost:2375/containers/json?filters=(networkname)
        /// <summary>
        /// List only docker containers on a given network – Get the Docker container list 
        /// </summary>
        /// <param name="url"> name of the server url 
        /// examples : host://localhost   or http://192.168.58.1 </param>
        /// <param name="network"> network id or network name </param>
        /// <returns>list of containers on network </returns>
        //public static async Task<String> DockContnetwork(string url, string network)
        //{
         //   // url : adress of the local or remote server hosting the containers
         //  string url1;
          //  url1 = url + ":2375" + "/containers/json?filters=network(" + network + ")";
          //  string Response;
          //  Response = await Dck.GetHttpRep(url1);
          //  return Response;
        //}

        //GET /images/json 
        //(on OS unix,linux, we can test with : curl --unix-socket /var/run/docker.sock http://localhost/images/json
        //(on OS Windows , we can test with : curl http://localhost:2375/images/json )
        // port TCP 2375 must be reachable
        /// <summary>
        /// List images 
        /// </summary>
        /// <param name="url"> name of the server url ( on whish you want to get the list of containers)
        /// examples : host://localhost   or http://192.168.58.1 </param>
        /// <returns>Images list </returns>
        public static async Task<String> DockImageList(string url)
        {
            // url : adress of the local or remote server hosting the images
            string url1;
            url1 = url + ":2375" + "/images/json";
            string Response;
            Response = await Dck.GetHttpRep(url1);
            return Response;
        }






    }
}
