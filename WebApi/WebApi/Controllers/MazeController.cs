using MazeLib;
using WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace WebApi.Controllers
{
    public class MazeController : ApiController
    {
        private IModel<Maze> model = new MazeModel();


        // GET: api/Maze/
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /*   [HttpGet]
           public JObject GenerateMaze(string name, int rows, int cols)
           {
               Maze maze = model.Generate(name, rows, cols);
               JObject obj = JObject.Parse(maze.ToJSON());
               return obj;
           }
           */
        // GET: api/Maze/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Maze
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Maze/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Maze/5
        public void Delete(int id)
        {
        }
    }
}
