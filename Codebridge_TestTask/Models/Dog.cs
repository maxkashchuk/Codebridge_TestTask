using System.ComponentModel.DataAnnotations.Schema;

namespace Codebridge_TestTask.Models
{
    [NotMapped]
    public abstract class Dog
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int TailLength { get; set; }
        public int Weight { get; set; }
    }
}
