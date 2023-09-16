using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyCompany_2.Domain.Entities;

namespace MyCompany_2.Domain
{
    public class AppDBContext : IdentityDbContext<IdentityUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        // Наши классы текстовое поле и услуга. Проицируем их на БД,
        // будет создана таблица с такими именами
        public DbSet<TextField> TextFields { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }

        /// <summary>
        /// Метод создания БД.
        /// При ее создании заполняется значениями по умолчанию
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Создаем роль для пользователя.
            // Данная роль Админ. Можно добавить другие
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "a177b1cd-d99a-4616-b874-f1ff39d922e8",
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            // Данная роль Технолог.
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "e3944bd0-965e-4b3b-aeb8-c5b584608769",
                Name = "Технолог",
                NormalizedName = "ТЕХНОЛОГ"
            });

            // Создаем пользователя Admin
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "91802ea1-5e98-42d1-9763-7e40452a3f7d",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "adm.pitterina@yandex.ru",
                NormalizedEmail = "ADM.PITTERINA@YANDEX.RU",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "1234"),
                SecurityStamp = string.Empty
            });

            // Промежуточная таблица соотношения юзера с ролью
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "a177b1cd-d99a-4616-b874-f1ff39d922e8",
                UserId = "91802ea1-5e98-42d1-9763-7e40452a3f7d"
            });

            // Текстовые поля в БД. Нужны чтобы изменять содердание этих страниц
            // и прописать SEO метатеги
            modelBuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("63dc8fa6-07ae-4391-8916-e057f71239ce"),
                CodeWord = "PageIndex",
                Title = "Главная"
            });

            modelBuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("70bf165a-700a-4156-91c0-e83fce0a277f"),
                CodeWord = "PageServices",
                Title = "Наши услуги"
            });

            modelBuilder.Entity<TextField>().HasData(new TextField
            {
                Id = new Guid("4aa76a4c-c59d-409a-84c1-06e6487a137a"),
                CodeWord = "PageContacts",
                Title = "Контакты"
            });

        }
    }
}
