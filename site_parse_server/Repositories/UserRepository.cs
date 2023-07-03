using System;
using site_parse_server.Models;
using Microsoft.EntityFrameworkCore;
using site_parse_server.Repositories.Interfaces;

namespace site_parse_server.Repositories
{
	public class UserRepository : IUserRepository
	{
        private readonly DataBaseContext _db;
		public UserRepository(DataBaseContext db)
		{
            _db = db;
		}

        public async Task Create(User obj)
        {
            var count = _db.Users.Where(u => u.Login == obj.Login).Count();
            if (count == 0)
            {
                await _db.Users.AddAsync(obj);
                await _db.SaveChangesAsync();

            }
             else
                throw new Exception("My msg");
        }

        public async Task<bool> CheckByLoginAndPassword(string login, string password)
        {
            var isValid = _db.Users.Where(u => u.Login == login && u.Password == password).Count() > 0 ? true : false;
            return isValid;
        }

        public async Task<User> Get(int id)
        {
            return await _db.Users.FirstAsync(u => u.Id == id);
        }

        public async Task Update(User obj)
        {
            var user = await _db.Users.FirstAsync(u => u.Id == obj.Id);
            user = obj;
        }

        public async Task<User> GetByLogin(string login)
        {
            return await _db.Users.FirstAsync(u => u.Login == login);
        }

        public async Task<List<ParseTask>> GetUserTasks(string login)
        {
            
            var user = await _db.Users.FirstAsync(u => u.Login == login);
            var tasks = _db.Tasks.Where(t => t.UserId == user.Id);
            return tasks.ToList();
        }

        public async Task AddUserTask(User user)
        {
            var userDb = await _db.Users.FirstAsync(u => u.Login == user.Login);
            try
            {
                _db.Tasks.Add(new ParseTask() { Description = user.Tasks[0].Description, UserId = userDb.Id });
                await _db.SaveChangesAsync();
            }
            catch(Exception e)
            {
                throw new Exception("Failed to add task");
            }
            return;
        }
    }
}

