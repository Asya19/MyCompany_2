using System.ComponentModel.DataAnnotations;

namespace MyCompany_2.Domain.Entities
{
    public class ServiceItem : EntitiesBase
    {
        [Required(ErrorMessage = "Заполните название услуги")]
        public string CodeWord { get; set; }
        
        [Display(Name = "Название услуги")]
        public override string Title { get; set; }

        [Display(Name = "Краткое описание услуги")]
        public override string Subtitle { get; set; }

        [Display(Name = "Полное описание услуги")]
        public override string Text { get; set; }
    }
}
