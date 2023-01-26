using Microsoft.AspNetCore.Mvc;
using System;
using WSConvertisseur.Modèle;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WSConvertisseur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevisesController : ControllerBase
    {
        List<Devise> listDevises;

        /// <summary>
        /// Get all currencies.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response listDevises="all list devices">Returns every devices in a list</response>

        // GET: api/<DevisesController>
        [HttpGet]
        public IEnumerable<Devise> GetAll()
        {
            return listDevises;
        }


        /// <summary>
        /// Get a single currency.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the currency</param>
        /// <response code="200">When the currency id is found</response>
        /// <response code="404">When the currency id is not found</response>

        // GET api/<DevisesController>/5
        [HttpGet("{id}", Name = "GetDevise")]

        public ActionResult<Devise> GetById(int id)
        {
            Devise? devise =
             (from d in listDevises
              where d.Id == id
             select d).FirstOrDefault();

            if (devise == null)
            {
                return NotFound();
            }
            return devise;
        }

        /// <summary>
        /// Add a device currency.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="devise">The device currency we're adding</param>
        /// <response code="400">When the device format is not valid</response>
        /// <response 1>When the device format is valid</response>

        // POST api/<DevisesController>
        [HttpPost]
        public ActionResult<Devise> Post([FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            listDevises.Append(devise);
            return CreatedAtRoute("GetDevise", new { id = devise.Id }, devise);
        }

        /// <summary>
        /// Update a device currency.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the device currency we're updating</param>
        /// <param name="device">The device currency overall we're updating</param>
        /// <response code="400">When the device format is not valid</response>
        /// <response code="400">When the device id is getting updated</response>
        /// <response code="440">When the device id doesn't exists</response>
        /// <response 1>When everything above is valid, and the device is getting updated</response>

        // PUT api/<DevisesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != devise.Id)
            {
                return BadRequest();
            }
            int index = listDevises.FindIndex((d) => d.Id == id);
            if (index < 0)
            {
                return NotFound();
            }
            listDevises[index] = devise;
            return NoContent();
        }

        /// <summary>
        /// Delete a device currency.
        /// </summary>
        /// <returns>Http response</returns>
        /// <param name="id">The id of the device currency we're deleting</param>
        /// <response code="440">When the device id doesn't exists</response>
        /// <response name="device">When the device is found and is getting deleted</response>

        // DELETE api/<DevisesController>/5
        [HttpDelete("{id}")]
        public ActionResult<Devise> Delete(int id)
        {
            Devise? devise =
             (from d in listDevises
              where d.Id == id
              select d).FirstOrDefault();

            if (devise == null)
            {
                return NotFound();
            }
            else
            {
                listDevises = listDevises.Where(devise => devise.Id != id).ToList();
                return devise;
            }

        }
        
        public DevisesController()
        {
            listDevises = new List<Devise>();

            listDevises.Add(new Devise(1, "Dollar", 1.08));
            listDevises.Add(new Devise(2, "Franc Suisse", 1.07));
            listDevises.Add(new Devise(3, "Yen", 120));

        }
    }
}
