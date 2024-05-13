using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAuth.Model.Users;

namespace UserAuth.Service.Services;

public interface IUserService
{
    ValueTask<UserViewModel> CreateAsync(UserCreateModel model);
    ValueTask<UserViewModel> DeleteAsync(long id);
    ValueTask<UserViewModel> GetAsync(long id);
    ValueTask<IEnumerable<UserViewModel>> GetAllAsync();
    ValueTask<UserViewModel> LoginAsync(string email, string password);
    ValueTask<string> LoginWithTokenAsync(string email, string password);
}
