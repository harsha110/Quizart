using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tables;
using API_functions;
using System.Net.Http;
using System.Resources;
namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    { 
       // Labels g= new Labels();
       API_methods api = new API_methods();
        // GET api/values
        [HttpGet]
        public ActionResult<List<Template>> Get()
        {
            return api.retrive();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<List<Question_table>> Get(string id,[FromQuery] string topic)
        {
            //API_methods api1 = new API_methods();
            // Console.WriteLine("forllopp");
            return api.retrive1();
            //= api.get_Questions(id);
            //Console.WriteLine(value);
        }

        // POST api/values
        [HttpPost]
        public ActionResult<List<Template>> Post([FromBody] post_object g)
        {
            //Console.WriteLine(g.Label_Id);
            Console.WriteLine("came");
            
            return api.Insert_in_template(g);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            //  bool val =api.remove_in(id ); 
            // if(val==true)
            // return Ok("Data deleted from table");
        
            return BadRequest("Could not delete as the entry does not exist");
        }
    }
}
