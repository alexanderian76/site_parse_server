using System;
using System.ComponentModel.DataAnnotations;

namespace site_parse_server.Models
{
	public class ParseTask
	{
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; } = "В обработке";
        public int UserId { get; set; }
    }
}

