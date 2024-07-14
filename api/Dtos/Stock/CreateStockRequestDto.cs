using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 characters")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MinLength(2, ErrorMessage = "Company name cannot be less 2 characters")]
        [MaxLength(20, ErrorMessage = "Company name cannot be over 20 characters")]
        public string CompanyName { get; set; } = string.Empty ;
        [Required]
        [Range(1, 1000000000, ErrorMessage = "The purchase must be between 1 and 1000000000.")]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.01, 100, ErrorMessage = "The Last div value between 0.01 and 100.")]
        public decimal LastDiv { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "Industry cannot be less 4 characters")]
        [MaxLength(15, ErrorMessage = "Industry cannot be over 15 characters")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(1, 5000000000, ErrorMessage = "Market cap value between 1 and 5000000000.")]
        public long MarketCap { get; set; }
    }
}