using MyToDo.Services.Core;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Parameters;
using System.Collections.ObjectModel;
using System.Web;

namespace MyToDo.Services.Implementations
{
    public class ToDoService : BaseService<TodoDto>
    {
        public async Task<string?> GetDtosAsync(string action, int uid, ObservableCollection<TodoDto> dtos, QueryParameter? queryParameter = null)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            if (queryParameter != null)
            {
                if (!string.IsNullOrEmpty(queryParameter.Keyword))
                    query["keyword"] = queryParameter.Keyword;
                if (queryParameter.Status.HasValue)
                    query["status"] = queryParameter.Status.Value.ToString();
            }

            var response = await HttpClientService.TryGetAsync<ICollection<TodoDto>>(action, uid, query.ToString());
            if (response == null || !response.Status)
                return response?.Message;

            dtos.Clear();
            foreach (var dto in response.Result!)
                dtos.Add(dto);

            return response.Message;
        }
    }
}
