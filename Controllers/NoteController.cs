using InsightIn.Api.Helpers;
using InsightIn.Api.Interfaces;
using InsightIn.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InsightIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        ApiReturnObj returnObj = new ApiReturnObj();
        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }
        [HttpGet("get-all")]
        [Authorize(Policy = "OnlyNonBlockedCustomer")]
        public IActionResult GetAll()
        {
            try
            {
                var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                string userName = claim.Value ;
                var data = _noteService.GetList(userName);
                if (data.Any())
                {
                    returnObj.IsExecute = true;
                    returnObj.ApiData = data;
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
                returnObj.IsExecute = false;
                returnObj.Message = ex.Message;
                returnObj.ApiData = null;
                return Ok(returnObj);
            }

        }
        [HttpGet("get-note-type-dropdown")]
        [Authorize(Policy = "OnlyNonBlockedCustomer")]
        public IActionResult GetNoteTypes()
        {
            try
            {
                var data = _noteService.GetNoteTypes();
                if (data.Any())
                {
                    returnObj.IsExecute = true;
                    returnObj.ApiData = data;
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
                returnObj.IsExecute = false;
                returnObj.Message = ex.Message;
                returnObj.ApiData = null;
                return Ok(returnObj);
            }

        }
        [HttpPost("add")]
        [Authorize(Policy = "OnlyNonBlockedCustomer")]
        public IActionResult Add(Note model)
        {
           
                try
                {
                    var data = _noteService.AddNote(model);
                    if (data)
                    {
                       
                        returnObj.IsExecute = true;
                        returnObj.ApiData = true;
                       
                        return Ok(returnObj);
                    }
                    else
                    {
                      
                        returnObj.IsExecute = false;
                        returnObj.ApiData = null;
                        return Ok(returnObj);
                    }
                }
                catch (Exception)
                {
                    returnObj.IsExecute = false;
                    returnObj.ApiData = null;
                    return Ok(returnObj);
                }

        }

        [HttpPut("update/{noteId}")]
        [Authorize(Policy = "OnlyNonBlockedCustomer")]
        public IActionResult Update(long noteId, Note model)
        {
            
                try
                {
                    var data = _noteService.UpdateNote(noteId, model);
                    if (data)
                    {
                       
                        returnObj.IsExecute = true;
                        returnObj.ApiData = true;
                       
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

        [HttpGet("delete/{noteId}")]
        [Authorize(Policy = "OnlyNonBlockedCustomer")]
        public IActionResult Delete(long noteId)
        {
            var data = _noteService.DeleteNote(noteId);
            if (data)
            {
                returnObj.IsExecute = true;
                returnObj.ApiData = true;
                
                return Ok(returnObj);
            }
            else
            {
                returnObj.IsExecute = false;
                returnObj.ApiData = null;
                return Ok(returnObj);
            }
        }
    }
}
