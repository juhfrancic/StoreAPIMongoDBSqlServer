using System;
using System.Collections.Generic;
using System.Text;

namespace TomadaStore.Models.DTOs.Payment
{
    public class PaymentDTO
    {
        public string? SaleId { get; init; }
        public string? ProductId { get; init; }
        public decimal TotalPrice { get; init; }
        public string? Situacao { get; init; } 
    }
}
