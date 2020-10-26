using System;
using System.Collections.Generic;
using System.Text;

namespace Visa.Core.Models
{
    public class OurTeam : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Image { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Google { get; set; }
        public string Linkedin { get; set; }
    }
}
