using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using VueContactsAPI.ViewModels;

namespace VueContactsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class ContactController : ControllerBase
    {
        // GET api/contact
        [HttpGet]
        public ActionResult<IEnumerable<ContactVM>> Get()
        {
            return new ContactVM[]
            {
                new ContactVM
                {
                    Name = "Dory Blue",
                    Company = "Barracuda Pier",
                    JobTitle = "Fish Scaler",
                    Email = "dory@barracudapier.com.au",
                    Phone = "0413304735",
                    Notes = "Aha moment!"
                },
                new ContactVM
                {
                    Name = "Nemo Red",
                    Company = "Dolphin Paradise",
                    JobTitle = "Trainer",
                    Email = "nemo@dolphinparadise.com.au",
                    Phone = "0423289337",
                    Notes = "That is it!"
                },
            };
        }

        // GET api/contact/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/contact
        [HttpPost]
        public IActionResult Post([FromBody] ContactVM value)
        {
            return Ok(value);
        }

        // PUT api/contact/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ContactVM value)
        {
        }

        // DELETE api/contact/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
