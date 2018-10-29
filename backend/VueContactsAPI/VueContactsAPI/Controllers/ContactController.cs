using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VueContactsAPI.Entities;
using VueContactsAPI.Infrastructures;
using VueContactsAPI.Repositories;
using VueContactsAPI.ViewModels;

namespace VueContactsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class ContactController : ControllerBase
    {
        private IHostingEnvironment _environment;
        private IContactRepository _contactRepository;
        private ResponseSingleVM<ContactVM> _responseSingleVM;
        private ResponseVM<ContactVM> _responseVM;

        public ContactController(IContactRepository contactRepository, IHostingEnvironment environment)
        {
            _environment = environment;
            _contactRepository = contactRepository;
            _responseSingleVM = new ResponseSingleVM<ContactVM>();
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
                _responseSingleVM.Error = new ErrorVM
                {
                    Message = "Please provide all required data for a contact.",
                    InnerMessage = ""
                };

                return BadRequest(_responseSingleVM);
            }

            try
            {
                var newContact = Mapper.Map<Contact>(contactVM);
                _contactRepository.Save(newContact);

                return Ok();
            }

            catch (Exception ex)
            {

                _responseSingleVM.Error = new ErrorVM
                {
                    Message = "Failed to save new contact.",
                    InnerMessage = ex.Message
                };

                return UnprocessableEntity(_responseSingleVM);
            }

        }

        // PUT api/contact/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ContactVM contact)
        {
            var contactFound = _contactRepository.GetById(contact.Id);
            if (contactFound != null)
            {
                // Map updated details to the found contact
                contactFound.Name = contact.Name;
                contactFound.Company = contact.Company;
                contactFound.JobTitle = contact.JobTitle;
                contactFound.Email = contact.Email;
                contactFound.Phone = contact.Phone;
                contactFound.Notes = contact.Notes;

                _contactRepository.Update();

                return Ok();
            }

            return NotFound();
        }

        // DELETE api/contact/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var contactFound = _contactRepository.GetById(id);

            if (contactFound != null)
            {
                _contactRepository.Delete(id);

                return Ok();
            }

            return NotFound();
        }

        // GET api/contact/export
        [HttpGet]
        [Route("export")]
        public IActionResult Export()
        {
            var fileName = $"{DateTime.Now.Day.ToString()}_{DateTime.Now.Month.ToString()}_{DateTime.Now.Year.ToString()}.xlsx";
            var contactList = _contactRepository.GetAll().ToList();
            byte[] fileContent;

            var fileLocation = Path.Combine(_environment.WebRootPath, $"MyContacts_{fileName}");

            // Download location from the browser
            var result = ExportToExcel.Download<Contact>(fileLocation, contactList);
            var path = _environment.WebRootPath + $"\\MyContacts_{fileName}";
            using (BinaryReader b = new BinaryReader(System.IO.File.Open(path, FileMode.Open)))

            {
                fileContent = new byte[b.BaseStream.Length];
                //stream.Read(fileContent, 0, (int)stream.Length);
                fileContent = b.ReadBytes((int)b.BaseStream.Length);

                System.IO.File.WriteAllBytes(@"c:/temp/junk.xlsx", fileContent);

            }

            System.IO.File.Delete(fileLocation);
            return Ok(fileContent);
        }
    }
}
