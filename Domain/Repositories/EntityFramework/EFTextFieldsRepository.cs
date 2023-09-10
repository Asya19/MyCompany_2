using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MyCompany_2.Domain.Entities;
using MyCompany_2.Domain.Repositories.Abstract;

namespace MyCompany_2.Domain.Repositories.EntityFramework
{
    public class EFTextFieldsRepository : ITextFieldsRepository
    {
        private readonly AppDBContext context;
        public EFTextFieldsRepository(AppDBContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Удаление текстового поля по Id.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTextField(Guid id)
        {
            context.TextFields.Remove(new TextField() { Id = id });
            context.SaveChanges();
        }

        /// <summary>
        /// Поиск по ключевому слову.
        /// </summary>
        /// <param name="codeWord"></param>
        /// <returns></returns>
        public TextField GetTextFieldByCodeWord(string codeWord)
        {
            return context.TextFields.FirstOrDefault(el => el.CodeWord == codeWord);
        }

        /// <summary>
        /// Поиск по Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TextField GetTextFieldById(Guid id)
        {
            return context.TextFields.FirstOrDefault(el => el.Id == id);
        }

        /// <summary>
        /// Выборка всех полей.
        /// </summary>
        /// <returns></returns>
        public IQueryable<TextField> GetTextFields()
        {
            return context.TextFields;
        }

        /// <summary>
        /// Сохранение записи.
        /// Если у записи нет Id, то новая запись добавляется в БД, 
        /// если Id есть, тогда сохранются изменения.
        /// </summary>
        /// <param name="entity"></param>
        public void SaveTextField(TextField entity)
        {
            if (entity.Id == default)
                context.Entry(entity).State = EntityState.Added;
            else
                context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
