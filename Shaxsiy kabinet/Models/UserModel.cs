using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Shaxsiy_kabinet.Models
{
    public class UserModel
    {
      
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Birthday { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }

    }
}
