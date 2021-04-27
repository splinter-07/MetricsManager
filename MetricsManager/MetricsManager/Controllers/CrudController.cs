using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
    [Route("api/crud")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly ValuesHolder holder;

        public CrudController(ValuesHolder holder)
        {
            this.holder = holder;
        }

        [HttpPost("create")]
        public IActionResult Create([FromQuery] DateTime date, [FromQuery] int temperature)
        {
            holder.SaveTemperature(date, temperature);
            
            return Ok();
        }
       
        [HttpGet("read")]
        public IActionResult Read([FromQuery] DateTime dateBegin, [FromQuery] DateTime dateEnd)
        {
            return Ok(holder.ReadTemperature(dateBegin,dateEnd));
        }
        [HttpGet("readall")]
        public IActionResult ReadAll()
        {
            return Ok(holder.listTemperature);
        }

        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime stringsToUpdate, [FromQuery] int newValue)
        {
            holder.UpdateTemperature(stringsToUpdate, newValue);
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime dateBegin, [FromQuery] DateTime dateEnd)
        {
            holder.DeleteTemperature(dateBegin, dateEnd);
            return Ok();
        }
    }
}
