using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCompany_2.Domain.Entities;


namespace MyCompany_2.Domain.Repositories.Abstract
{
    public interface IServiceItemsRepository
    {
        IQueryable<ServiceItem> GetServiceItems();
        ServiceItem GetServiceItemById(Guid id);
        void SaveServiceItem(ServiceItem entity);
        void DeleteServiceItem(Guid id);
    }
}
