using System.ComponentModel.DataAnnotations;

namespace AutoAPI.Model
{
    public class Auto
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Casa { get; set; }
        [Required]
        public string serie { get; set; }
        [Required]
        public string modello { get; set; }

        public Acquirente acquirente { get; set; }
    }
}
