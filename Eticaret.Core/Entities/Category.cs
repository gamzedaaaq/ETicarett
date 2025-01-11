using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Core.Entities
{
    public class Category: IEntitiy
    {
        public int Id { get; set; }
        [Display(Name = "Adı")]
        public String Name { get; set; }
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        [Display(Name = "Resim")]
        public string? İmage { get; set; }
        [Display(Name = "Aktif?")]
        public bool IsActive { get; set; }
        [Display(Name = "Kayıt Tarihi"), ScaffoldColumn(false)]
        public DateTime CreateTime { get; set; }= DateTime.Now;
        [Display(Name = "Üst Menüde Göster")]
        public bool IsTopMenu { get; set; }
        [Display(Name = "Üst Kategori")]
        public int ParentId { get; set; }
        [Display(Name = "Sıra Numarası")]
        public int OrderNo { get; set; }
        public IList<Product>? Products { get; set; }
       
    }
}
