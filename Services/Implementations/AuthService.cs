using MyToDo.Common.Extensions;
using MyToDo.Common.Helps;
using MyToDo.Common.Models;
using MyToDo.Services.Core;
using MyToDo.Shared.Dtos;

namespace MyToDo.Services.Implementations
{
    public class AuthService
    {
        public static async Task<(bool?, string?)> LoginAsync(string username, string password)
        {
            var response = await HttpClientService.TryPostAsync<AuthDto>("api/auth/login", new UserDto
            {
                Username = username,
                Password = password.ToMd5()
            });

            if (response?.Status == true)
            {
                var result = response.Result!;
                App.Current.Uid = result.UserInfo!.Id;
                App.Current.Username = username;
                RecordUserHelp.SaveUser(new RecordUserModel
                {
                    Id = result.UserInfo!.Id,
                    AccessToken = result.AccessToken,
                    RefreshToken = result.RefreshToken,
                    Username = username,
                    Password = password
                });
            }

            return (response?.Status, response?.Message);
        }

        public static async Task<(bool?, string?)> RegisterAsync(string username, string password)
        {
            var response = await HttpClientService.TryPostAsync<AuthDto>("api/auth/register", new UserDto
            {
                Username = username,
                Password = password.ToMd5()
            });

            if (response?.Status == true)
            {
                var result = response.Result!;
                App.Current.Uid = result.UserInfo!.Id;
                App.Current.Username = username;
                RecordUserHelp.SaveUser(new RecordUserModel
                {
                    Id = result.UserInfo!.Id,
                    AccessToken = result.AccessToken,
                    RefreshToken = result.RefreshToken,
                    Username = username,
                    Password = password
                });
            }

            return (response?.Status, response?.Message);
        }

        //public static async Task<(bool?, string?)> AutoLoginAsync(string username, string accessToken)
        //{
        //    var response = await HttpClientService.TryPostAsync<AuthDto>("api/auth/register", new UserDto
        //    {
        //        Username = username,
        //        Password = password.ToMd5()
        //    });

        //    if (response?.Status == true)
        //    {
        //        App.Current.Username = username;
        //        var result = response.Result!;
        //        RecordUserHelp.SaveUser(new RecordUserModel
        //        {
        //            AccessToken = result.AccessToken,
        //            RefreshToken = result.RefreshToken,
        //            Username = username,
        //            Password = password
        //        });
        //    }

        //    return (response?.Status, response?.Message);
        //}

    }
}
