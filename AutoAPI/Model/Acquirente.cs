using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace AutoAPI.Model
{
    public class Acquirente
    {
        [Microsoft.Build.Framework.Required]
        public int ID { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }

        List<Auto> listAuto = new List<Auto>();
    }
}
