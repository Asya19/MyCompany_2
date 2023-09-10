using System.ComponentModel.DataAnnotations;

namespace MyCompany_2.Domain.Entities
{
    public class TextField : EntitiesBase
    {
        [Required]
        public string CodeWord { get; set; }

        [Display(Name = "Название страницы (заголовок)")]
        public override string Title { get; set; } = "Информационная страница";

        [Display(Name = "Содержание страницы")]
        public override string Text { get; set; } = "Содержание заполняется админом";
    }
}
