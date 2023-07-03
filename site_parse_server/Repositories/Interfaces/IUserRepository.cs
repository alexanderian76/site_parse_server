using System;
using site_parse_server.Models;

namespace site_parse_server.Repositories.Interfaces
{
	public interface IUserRepository: IBaseEditableRepository<User>
	{
		Task<bool> CheckByLoginAndPassword(string login, string password);
		Task<User> GetByLogin(string login);
        Task<List<ParseTask>> GetUserTasks(string login);
        Task AddUserTask(User user);
    }
}

