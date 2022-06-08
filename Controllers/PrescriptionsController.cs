using Microsoft.AspNetCore.Mvc;
using WebApplication11.Models;
using WebApplication11.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        IDbService db;

        public PrescriptionsController(IDbService db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
           var x = db.GetPrescription(id);
           return Ok(x);
        }

       
    }
}
