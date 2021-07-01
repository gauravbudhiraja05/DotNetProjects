using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Web.Http.Cors;

namespace WideWorldImporters.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        [EnableCors("AllowOrigin", "*", "*")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2", "value 3" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [EnableCors("AllowOrigin", "*", "*")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        [EnableCors("AllowOrigin", "*", "*")]
        public ActionResult<IEnumerable<string>> Post(int id)
        {
            return Get();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [EnableCors("AllowOrigin", "*", "*")]
        public ActionResult<IEnumerable<string>> Put(int id)
        {
            return Get();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [EnableCors("AllowOrigin", "*", "*")]
        public ActionResult<IEnumerable<string>> Delete(int id)
        {
            return Get();
        }
    }
}
