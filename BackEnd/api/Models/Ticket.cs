
using System.ComponentModel.DataAnnotations;

namespace api.Models 
{
    public class Ticket
    {
        [Key]
        public int Ticket_ID {get; set;}
        public required String Description {get; set;}
        public required String Status {get; set;}
        public DateTime Date {get; set;}

    }
}