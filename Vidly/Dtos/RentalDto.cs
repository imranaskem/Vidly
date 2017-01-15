using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Dtos
{
    public class RentalDto
    {
        public int Id { get; set; }
        public string DateRented { get; set; }
        
        [Required]
        public string MovieName { get; set; }

        [Required]
        public string CustomerName { get; set; }
    }
}