using System.Collections.ObjectModel;

namespace MyToDo.Services.Interfaces
{
    public interface IBaseService<TDto> where TDto : class
    {
        Task<string?> AddDtoAsync(string action, TDto dto, ObservableCollection<TDto> dtos);
        Task<string?> DeleteDtoAsync(string action, int uid, int id, ObservableCollection<TDto> dtos);
        Task<string?> UpdateDtoAsync(string action, TDto dto, ObservableCollection<TDto> dtos);
        Task<string?> GetDtosAsync(string action, int uid, ObservableCollection<TDto> dtos);
    }
}
