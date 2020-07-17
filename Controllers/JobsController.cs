using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using summer2020_dotnet_gregslist.Services;
using summer2020_dotnet_gregslist.Models;

namespace fullstack_gregslist.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class JobsController : ControllerBase
  {
    private readonly JobsService _cs;
    public JobsController(JobsService cs)
    {
      _cs = cs;
    }
    // NOTE path is https://localhost:5001/api/cars
    [HttpGet]
    public ActionResult<IEnumerable<Job>> GetAll()
    {
      try
      {
        return Ok(_cs.GetAll());
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    //NOTE path does not follow standards https://localhost:5001/api/cars/user
    [HttpGet("user")]
    [Authorize]
    public ActionResult<IEnumerable<Job>> GetJobsByUser()
    {
      try
      {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_cs.GetByUserId(userId));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    // NOTE path is https://localhost:5001/api/cars/id
    [HttpGet("{id}")]
    public ActionResult<Job> GetById(int id)
    {
      try
      {
        return Ok(_cs.GetById(id));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public ActionResult<Job> Create([FromBody] Job newJob)
    {
      try
      {
        newJob.UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_cs.Create(newJob));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public ActionResult<Job> Edit(int id, [FromBody] Job carToUpdate)
    {
      try
      {
        carToUpdate.Id = id;
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_cs.Edit(carToUpdate, userId));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }



    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult<string> Delete(int id)
    {
      try
      {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_cs.Delete(id, userId));
      }
      catch (System.Exception err)
      {
        return BadRequest(err.Message);
      }
    }


  }
}
}
