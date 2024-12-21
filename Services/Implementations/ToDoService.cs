using MyToDo.Services.Core;
using MyToDo.Services.Interfaces;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Models;
using MyToDo.Shared.Parameters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyToDo.Services.Implementations
{
    public class ToDoService : BaseService<TodoDto>
    {
        public async Task<string?> GetDtosAsync(string action, ObservableCollection<TodoDto> dtos, QueryParameter? queryParameter = null)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            if (queryParameter != null)
            {
                if (!string.IsNullOrEmpty(queryParameter.Keyword))
                    query["keyword"] = queryParameter.Keyword;
                if (queryParameter.Status.HasValue)
                    query["status"] = queryParameter.Status.Value.ToString();
            }

            var url = string.IsNullOrEmpty(query.ToString()) ? action : action + "?" + query.ToString();

            var response = await HttpClientService.TryGetAsync<ICollection<TodoDto>>(url);
            if (response == null || !response.Status)
                return response?.Message;

            dtos.Clear();
            foreach (var dto in response.Result!)
                dtos.Add(dto);

            return response.Message;
        }
    }
}
