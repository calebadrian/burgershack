using System;
using System.Collections.Generic;
using burger_shack.Models;
using burger_shack.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace burger_shack
{
    [Route("api/[controller]")]
    public class BurgersController : Controller
    {
        private readonly BurgerRepository _repo;
        public BurgersController(BurgerRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<Burger> Get()
        {
            return _repo.GetBurgers();
        }

        [HttpGet("{id}")]
        public Burger Get(int id)
        {
            return _repo.GetById(id);
        }

        [HttpPost]
        public Burger AddBurger([FromBody]Burger burger)
        {
            if (ModelState.IsValid)
            {
               return _repo.Add(burger);
            }
            return null;  
        }

        // [HttpPut("{id}")]
        // public IEnumerable<Burger> EditBurger([FromBody]Burger burger, int id)
        // {
        //     if (id > -1 && id < Burgers.Count && ModelState.IsValid)
        //     {
        //         Burgers[id] = burger;
        //     }
        //     return Burgers;
        // }

        // [HttpDelete("{id}")]
        // public IEnumerable<Burger> DeleteBurger(int id)
        // {
        //     if (id > -1 && id < Burgers.Count)
        //     {
        //         Burgers.RemoveAt(id);
        //     }
        //     return Burgers;
        // }
    }
}