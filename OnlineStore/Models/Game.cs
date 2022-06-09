using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models
{
    public class Game
    {
        [Required(ErrorMessage = "Данное поле не может быть пустым.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Данное поле не может быть пустым.")]
        public string Name { get; set; }

        public string ShortDesc { get; set; }

        public string LongDesc { get; set; }

        [Required(ErrorMessage = "Данное поле не может быть пустым.")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Данное поле не может быть пустым.")]
        public int Price { get; set; } // Так как цена не может быть отрицательной

        [Required(ErrorMessage = "Данное поле не может быть пустым.")]
        public bool Avaliable { get; set; }

        public int TotalViews { get; set; }

        public int TotalSales { get; set; }
    }
}
