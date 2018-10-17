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
        public ActionResult<get_method_object> Get(string id)
        {
            //API_methods api1 = new API_methods();
            // Console.WriteLine("forllopp");
            get_method_object  value = api.get_Questions(id);
            //Console.WriteLine(value);
            return value;
        }

        // POST api/values
        [HttpPost]
        public ActionResult<Template> Post([FromBody] Template g)
        {
            //Console.WriteLine(g.Label_Id);
            
            bool inserted_or_not = api.Insert_in_template(g);
            
            return g;
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
             bool val =api.remove_in(id ); 
            if(val==true)
            return Ok("Data deleted from table");
        
            return BadRequest("Could not delete as the entry does not exist");
        }
    }
}
