using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Core.Entities
{
    public class Slider:IEntitiy
    {
        public int Id { get; set; }
        [Display(Name = "Başlık")]
        public string? Title { get; set; }
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        [Display(Name = "Resim")]
        public string? İmage { get; set; }
        [Display(Name = "Bağlantı Adresi")]
        public string? Link { get; set; }


    }
}
