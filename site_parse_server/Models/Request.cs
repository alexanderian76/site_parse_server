using System;
using System.ComponentModel.DataAnnotations;

namespace site_parse_server.Models
{
	public class Request
	{
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}

