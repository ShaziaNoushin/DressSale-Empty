using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Mid1273286.Models;

namespace Mid1273286.ViewModels.Input
{
    public class DressEditModel
    {
        public int DressId { get; set; }
        [Required, StringLength(50)]
        public string DressName { get; set; } = default!;
        [Required, EnumDataType(typeof(Size))]
        public Size Size { get; set; } = default!;
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }
       
        public IFormFile? Picture { get; set; } = default!;

        public bool OnSale { get; set; }
    }
}
