using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using VueContactsAPI.Entities;
using VueContactsAPI.Repositories;
using VueContactsAPI.ViewModels;

namespace VueContactsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class LoginController : Controller
    {
        private ILoginRepository _loginRepository;
        private ResponseSingleVM<LoginVM> _responseSingleVM;
        private ResponseVM<LoginVM> _responseVM;

        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
            _responseSingleVM = new ResponseSingleVM<LoginVM>();
            _responseVM = new ResponseVM<LoginVM>();
        }

        // GET api/login
        [HttpGet]
        public ActionResult<ResponseVM<LoginVM>> Get()
        {
            var logins = _loginRepository.GetAll();
            var loginList = Mapper.Map<IEnumerable<LoginVM>>(logins);

            _responseVM.Payload = loginList.ToList();
            return _responseVM;
        }

        // GET api/login/5
        [HttpGet("{search}")]
        public ActionResult<IEnumerable<LoginVM>> Get(string search)
        {
            var logins = _loginRepository.GetAll().Where(x => x.Name.Contains(search));
            var loginList = Mapper.Map<IEnumerable<LoginVM>>(logins);

            return loginList.ToList();
        }

        // POST api/login
        [HttpPost]
        public IActionResult Post([FromBody] LoginVM LoginVM)
        {
            if (!ModelState.IsValid)
            {
                _responseSingleVM.Error = new ErrorVM
                {
                    Message = "Please provide all required data for a Login.",
                    InnerMessage = ""
                };

                return BadRequest(_responseSingleVM);
            }

            try
            {
                var newContact = Mapper.Map<Login>(LoginVM);
                _loginRepository.Save(newContact);

                return Ok();
            }

            catch (Exception ex)
            {

                _responseSingleVM.Error = new ErrorVM
                {
                    Message = "Failed to save new Login.",
                    InnerMessage = ex.Message
                };

                return UnprocessableEntity(_responseSingleVM);
            }

        }

        // PUT api/login/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] LoginVM Login)
        {
            var loginFound = _loginRepository.GetById(Login.Id);
            if (loginFound != null)
            {
                // Map updated details to the found Login
                loginFound.Name = Login.Name;
                loginFound.LoginName = Login.LoginName;
                loginFound.LoginPassword = Login.LoginPassword;
                loginFound.WebSiteUrl = Login.WebSiteUrl;
                loginFound.Notes = Login.Notes;

                _loginRepository.Update();

                return Ok();
            }

            return NotFound();
        }

        // DELETE api/login/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var loginFound = _loginRepository.GetById(id);

            if (loginFound != null)
            {
                _loginRepository.Delete(id);

                return Ok();
            }

            return NotFound();
        }
    }
}