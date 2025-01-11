using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Core.Entities
{
    public class Product: IEntitiy
    {
        public int Id { get; set; }
        [Display(Name = "Adı")]
        public String Name { get; set; }
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
        [Display(Name = "Resim")]
        public string? İmage { get; set; }
        [Display(Name = "Fiyat")]
        public decimal Price { get; set; }
        [Display(Name = "Stok")]
        public string? Stock { get; set; }
        [Display(Name = "Ürün kodu")]
        public string? ProductCode { get; set; }
        [Display(Name = "Aktif?")]
        public bool IsActive { get; set; }
        [Display(Name = "Kayıt Tarihi"), ScaffoldColumn(false)]
        public DateTime CreateTime { get; set; } = DateTime.Now;
        [Display(Name = "Anasayfa")]
        public bool IsHome{ get; set; }
        [Display(Name = "Kategori")]
        public int? CategoryId { get; set; }
        [Display(Name = "Kategori")]
        public Category? category { get; set; }
        [Display(Name = "Marka")]
        public  int BrandId  { get; set; }
        [Display(Name = "Marka")]
        public  Brand? brand  { get; set; }
        [Display(Name = "Sıra Numarası")]
        public int OrderNo { get; set; }
    }
}
