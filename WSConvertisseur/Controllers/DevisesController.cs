﻿using Microsoft.AspNetCore.Mvc;
using System;
using WSConvertisseur.Modèle;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WSConvertisseur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevisesController : ControllerBase
    {
        List<Devise> listDevises;

        // GET: api/<DevisesController>
        [HttpGet]
        public IEnumerable<Devise> GetAll()
        {
            return listDevises;
        }

        // GET api/<DevisesController>/5
        [HttpGet("{id}", Name = "GetDevise")]

        public ActionResult<Devise> GetById(int id)
        {
            IEnumerable<Devise> devises = GetAll();

            Devise? devise =
             (from d in devises
             where d.Id == id
             select d).FirstOrDefault();

            if (devise == null)
            {
                return NotFound();
            }
            return devise;
        }


        // POST api/<DevisesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DevisesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DevisesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

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
