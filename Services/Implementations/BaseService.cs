using MyToDo.Services.Core;
using MyToDo.Services.Interfaces;
using MyToDo.Shared.Dtos;
using MyToDo.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MyToDo.Services.Implementations
{
    public class BaseService<TDto> : IBaseService<TDto> where TDto : class, IBaseDto
    {
        public async Task<string?> AddDtoAsync(string action, TDto dto, ObservableCollection<TDto> dtos)
        {
            var response = await HttpClientService.TryPostAsync<TDto>(action, dto);
            if (response == null || !response.Status)
                return response?.Message;

            dtos.Add(response.Result!);
            return response.Message;
        }

        public async Task<string?> DeleteDtoAsync(string action, int id, ObservableCollection<TDto> dtos)
        {
            var response = await HttpClientService.TryDeleteAsync(action, id);
            if (response == null || !response.Status)
                return response?.Message;

            var idx = dtos.Select((x, i) => new { x, i })
                .FirstOrDefault(g => g.x.Id.Equals(id))?.i;
            if (idx != null)
                dtos.RemoveAt(idx.Value);

            return response.Message;
        }

        public async Task<string?> GetDtosAsync(string action, ObservableCollection<TDto> dtos)
        {
            var response = await HttpClientService.TryGetAsync<ICollection<TDto>>(action);
            if (response == null || !response.Status)
                return response?.Message;

            dtos.Clear();
            foreach (var dto in response.Result!)
                dtos.Add(dto);

            return response.Message;

        }

        public async Task<string?> UpdateDtoAsync(string action, TDto dto, ObservableCollection<TDto> dtos)
        {
            var response = await HttpClientService.TryPostAsync<TDto>(action, dto);
            if (response == null || !response.Status)
                return response?.Message;

            var updatedDto = response.Result!;
            var idx = dtos.Select((x, i) => new { x, i })
                .FirstOrDefault(g => g.x.Id.Equals(updatedDto.Id))?.i;
            if (idx != null)
                dtos[idx.Value] = updatedDto;

            return response.Message;
        }
    }
}
