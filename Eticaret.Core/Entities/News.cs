using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace Eticaret.Core.Entities
{
    public class News :IEntitiy
    {
        public int Id { get; set; }
        [Display(Name = "Adı")]
        public string? Name { get; set; }
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        [Display(Name = "Aktif? ")]
        public bool IsActive { get; set; }

        [Display(Name = "Resim")]
        public string? İmage { get; set; }
        [Display(Name = "Kayıt Tarihi"), ScaffoldColumn(false)]

        public DateTime CreateData { get; set; } = DateAndTime.Now;

    }
}
