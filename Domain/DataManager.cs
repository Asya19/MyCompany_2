using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCompany_2.Domain.Repositories.Abstract;

namespace MyCompany_2.Domain
{
    /// <summary>
    /// Обслуживающий класс, для управления репозиториями
    /// </summary>
    public class DataManager
    {
        public ITextFieldsRepository TextFields { get; set; }
        public IServiceItemsRepository ServiceItems { get; set; }

        public DataManager( ITextFieldsRepository textFieldsRepository, IServiceItemsRepository serviceItemsRepository)
        {
            TextFields = textFieldsRepository;
            ServiceItems = serviceItemsRepository;
        }
    }
}
