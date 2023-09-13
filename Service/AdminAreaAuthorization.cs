using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace MyCompany_2.Service
{

    public class AdminAreaAuthorization : IControllerModelConvention
    {
        private readonly string area;
        private readonly string policy;

        public AdminAreaAuthorization(string area, string policy)
        {
            this.area = area;
            this.policy = policy;
        }

        /// <summary>
        /// Проверка атрибутов для контроллера. Если атрибут присутствует, area атрибут, 
        /// то добавляеся фильтер для данного контроллера - AuthorizeFilter. 
        /// Отправляем юзера на авторизацию.
        /// 
        /// Далее в файле Startup.cs мы это соглашение - AdminAreaAuthorization - 
        /// добавляем, когда регистрируем сервисы для контроллера и представления.
        /// 
        /// Там же прописываем политику.
        /// </summary>
        /// <param name="controller"></param>
        public void Apply(ControllerModel controller)
        {
            if (controller.Attributes.Any(a => 
                    a is AreaAttribute && (a as AreaAttribute).RouteValue.Equals(area, StringComparison.OrdinalIgnoreCase))
                || controller.RouteValues.Any(r =>
                    r.Key.Equals("area", StringComparison.OrdinalIgnoreCase) && r.Value.Equals(area, StringComparison.OrdinalIgnoreCase)))
            {
                controller.Filters.Add(new AuthorizeFilter(policy));
            }
        }
    }
}
