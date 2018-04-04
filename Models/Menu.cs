using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using burger_shack.Interfaces;

namespace burger_shack.Models
{

    public class Menu
    {
        [Required]
        public string Name { get; set; }

        public Dictionary<string, dynamic> MenuItems {get; set;}

        public Menu(string name)
        {
            Name = name;
            MenuItems = new Dictionary<string, dynamic>();
            MenuItems.Add("burgers", new List<Burger>());
        }

    }

}