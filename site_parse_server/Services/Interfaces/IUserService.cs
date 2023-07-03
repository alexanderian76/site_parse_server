using System;
using site_parse_server.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace site_parse_server.Services.Interfaces
{
	public interface IUserService
	{
        Task<IBaseResponse<User>> GetById(int id);
        Task<IBaseResponse<User>> GetByLogin(string login);
        Task<IBaseResponse<List<ParseTask>>> GetUserTasks(string login);
        Task<IBaseResponse<User>> AddTask(User user);
        Task<bool> CheckUser(string login, string password);
        Task<IBaseResponse<User>> Create(User user);
        Task<IBaseResponse<User>> Update(User user);
    }
}

