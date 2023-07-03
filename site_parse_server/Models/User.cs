using System;
using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;

namespace site_parse_server.Models
{
	public class User
	{
        [Key]
        public int Id { get; set; }
       // public WebSocket Connection { get; set; }
        public String Login { get; set; }
        public String Password { get; set; }
        
        public List<ParseTask> Tasks { get; set; } = new List<ParseTask>();
    }
}

