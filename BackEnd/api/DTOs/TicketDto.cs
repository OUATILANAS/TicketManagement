
using System.ComponentModel.DataAnnotations;

namespace api.DTOs
{
    public class TicketDto
    {
        public int Ticket_ID { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MinLength(5, ErrorMessage = "Description must be at least 5 characters long")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [RegularExpression("^(Open|Closed)$", ErrorMessage = "Status must be either 'Open' or 'Closed'")]
        public required string Status { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }
    }
}