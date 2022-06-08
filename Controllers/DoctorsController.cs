using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication11.Models;
using WebApplication11.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        IDbService db;

        public DoctorsController(IDbService db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("{id}")]
        public Doctor GetDoctor(int id)
        {
           return db.GetDoctor(id);
        }

        
        [HttpPost]
        public IActionResult PostDoctor([FromBody] Doctor value)
        {
            
           if(db.AddDoctor(value))
           {
                return Ok();
           }
           return BadRequest();
        }

        
        [HttpPut("{id}")]
        public IActionResult ModifyDoctor(int id, [FromBody] Doctor value)
        {
            if(db.ModifyDoctor(id, value))
            {
                return Ok();
            }
            return BadRequest("Brak doktora");
        }

        
        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            if(db.RemoveDoctor(id))
            {
                return Ok();
            }
            return BadRequest("Nie znaleziono takiego doktora");
        }
    }
}
