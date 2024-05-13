using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAuth.DataAccess.Repositories;
using UserAuth.Domain.Entities;
using UserAuth.Model.Users;
using UserAuth.Service.Helpers.Hasher;
using UserAuth.Service.Helpers.Jwt;

namespace UserAuth.Service.Services;

public class UserService(IRepository<User> repository, IMapper mapper, IJwtProvider jwtProvider) : IUserService
{
    public async ValueTask<UserViewModel> CreateAsync(UserCreateModel model)
    {
        var user = await repository.SelectAsync(u => u.Email == model.Email);
        if (user is not null)
            throw new Exception("This user is already exists");

        var createdModel = mapper.Map<User>(model);
        createdModel.Password = Hasher.Hash(model.Password);
        await repository.InsertAsync(createdModel);
        await repository.SaveChangesAsync();
        return mapper.Map<UserViewModel>(createdModel);
    }

    public async ValueTask<UserViewModel> DeleteAsync(long id)
    {
        var user = await repository.SelectAsync(u => u.Id == id);
        if (user is null)
            throw new Exception("This user is not found");

        await repository.DeleteAsync(user);
        await repository.SaveChangesAsync();
        return mapper.Map<UserViewModel>(user);
    }

    public async ValueTask<IEnumerable<UserViewModel>> GetAllAsync()
    {
        return await Task.FromResult(mapper.Map<IEnumerable<UserViewModel>>(repository.SelectAllAsEnumerableAsync()));
    }

    public async ValueTask<UserViewModel> GetAsync(long id)
    {
        var user = await repository.SelectAsync(u => u.Id == id);
        if (user is null)
            throw new Exception("This user is not found");

        return mapper.Map<UserViewModel>(user);
    }

    public async ValueTask<UserViewModel> LoginAsync(string email, string password)
    {
        var user = await repository.SelectAsync(u => u.Email == email);
        if (email is null)
            throw new Exception("User with this email is not found");

        var checkPassword = Hasher.Verify(password, user.Password);
        if (!checkPassword)
            throw new Exception("Email or Password is incorrect");

        return mapper.Map<UserViewModel>(user);
    }

    public async ValueTask<string> LoginWithTokenAsync(string email, string password)
    {
        var user = await repository.SelectAsync(u => u.Email == email);
        if (email is null)
            throw new Exception("User with this email is not found");

        var checkPassword = Hasher.Verify(password, user.Password);
        if (!checkPassword)
            throw new Exception("Email or Password is incorrect");

        return jwtProvider.GenerateToken(user);
    }
}
