using InsightIn.Api.Helpers;
using InsightIn.Api.Interfaces;
using InsightIn.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsightIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        ApiReturnObj returnObj = new ApiReturnObj();

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("signup")]
        public IActionResult Signup(User model)
        {
                try
                {
                    if (model == null || string.IsNullOrEmpty(model.UserName))
                    {
                        return BadRequest("Missing login details");
                    }
                    var msg = _userService.SignUp(model);
                   

                    return Ok(msg);
                }
                catch (Exception)
                {
                    
                    return BadRequest("An Error Occured");
                }   
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(User loginRequest)
        {
            try
            {
                if (loginRequest == null || string.IsNullOrEmpty(loginRequest.UserName) || string.IsNullOrEmpty(loginRequest.Password))
                {
                    return BadRequest("Missing login details");
                }
                var loginResponse = _userService.LogIn(loginRequest);

                if (loginResponse == null)
                {
                    return BadRequest($"Invalid credentials");
                }
                if (loginResponse != null)
                {
                    returnObj.IsExecute = true;
                    returnObj.ApiData = loginResponse;
                    return Ok(returnObj);
                }
                else
                {
                    returnObj.IsExecute = false;
                    returnObj.ApiData = null;
                    return Ok(returnObj);
                }
            }
            catch (Exception ex)
            {
                returnObj.Message = ex.Message;
                returnObj.IsExecute = false;
                returnObj.ApiData = null;
                return Ok(returnObj);
            }

        }
    }
}
