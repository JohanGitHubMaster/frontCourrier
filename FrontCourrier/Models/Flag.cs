using System.Collections.Generic;

namespace FrontCourrier.Models
{
    public class Flag
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public List<Courriers> Courriers { get; set; }
    }
}
