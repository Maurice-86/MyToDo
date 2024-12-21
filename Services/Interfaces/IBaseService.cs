using MyToDo.Shared.Dtos;
using MyToDo.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Services.Interfaces
{
    public interface IBaseService<TDto> where TDto : class
    {
        Task<string?> AddDtoAsync(string action, TDto dto, ObservableCollection<TDto> dtos);
        Task<string?> DeleteDtoAsync(string action, int id, ObservableCollection<TDto> dtos);
        Task<string?> UpdateDtoAsync(string action, TDto dto, ObservableCollection<TDto> dtos);
        Task<string?> GetDtosAsync(string action, ObservableCollection<TDto> dtos);
    }
}
