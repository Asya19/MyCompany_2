using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCompany_2.Domain.Entities;

namespace MyCompany_2.Domain.Repositories.Abstract
{
    public interface ITextFieldsRepository
    {
        IQueryable<TextField> GetTextFields();
        TextField GetTextFieldById(Guid id);
        TextField GetTextFieldByCodeWord(string codeWord);
        void SaveTextField(TextField entity);
        void DeleteTextField(Guid id); 
    }
}
