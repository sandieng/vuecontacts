using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using VueContactsAPI.Entities;
using VueContactsAPI.Repositories;
using VueContactsAPI.ViewModels;

namespace VueContactsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class ContactController : ControllerBase
    {
        private IContactRepository _contactRepository;
        private ResponseVM<ContactVM> _responseVM;
        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
            _responseVM = new ResponseVM<ContactVM>();
        }

        // GET api/contact
        [HttpGet]
        public ActionResult<ResponseVM<ContactVM>> Get()
        {
            var contacts = _contactRepository.GetAll();
            var contactList = Mapper.Map<IEnumerable<ContactVM>>(contacts);

            _responseVM.Payload = contactList.ToList();
            return _responseVM;
          
        }

        // GET api/contact/5
        [HttpGet("{search}")]
        public ActionResult<IEnumerable<ContactVM>> Get(string search)
        {
            var contacts = _contactRepository.GetAll().Where(x => x.Name.Contains(search));
            var contactList = Mapper.Map<IEnumerable<ContactVM>>(contacts);

            return contactList.ToList();
        }

        // POST api/contact
        [HttpPost]
        public IActionResult Post([FromBody] ContactVM contactVM)
        {
            if (!ModelState.IsValid)
            {
                _responseVM.Error = new ErrorVM
                {
                    Message = "Please provide all required data for a contact.",
                    InnerMessage = ""
                };

                return BadRequest(_responseVM);
            }

            try
            {
                var newContact = Mapper.Map<Contact>(contactVM);
                _contactRepository.Save(newContact);

                return Ok();
            }

            catch (Exception ex)
            {

                _responseVM.Error = new ErrorVM
                {
                    Message = "Failed to save new contact.",
                    InnerMessage = ex.Message
                };

                return UnprocessableEntity(_responseVM);
            }

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
