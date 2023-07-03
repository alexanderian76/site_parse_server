using System;
using site_parse_server.Models;
using site_parse_server.Repositories.Interfaces;
using site_parse_server.Services.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace site_parse_server.Services
{
    public class UserService: IUserService
	{
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
		{
            _repository = repository;
		}

        public async Task<IBaseResponse<User>> AddTask(User user)
        {
            var baseResponse = new BaseResponse<User>();

            baseResponse.Description = "Add User task";
            try
            {
                await _repository.AddUserTask(user);
                baseResponse.StatusCode = StatusCode.OK;
            }
            catch(Exception e)
            {
                baseResponse.StatusCode = StatusCode.InternalServerError;
            }
            return baseResponse;
        }

        public Task<bool> CheckUser(string login, string password)
        {
            return _repository.CheckByLoginAndPassword(login, password);
        }

        public async Task<IBaseResponse<User>> Create(User user)
        {
            var baseResponse = new BaseResponse<User>();

            baseResponse.Description = "Create User";
            

            try
            {
                await _repository.Create(user);
                baseResponse.StatusCode = StatusCode.OK;
            }
            catch(Exception e)
            {
                baseResponse.StatusCode = StatusCode.InternalServerError;
            }

            return baseResponse;
        }

        public async Task<IBaseResponse<User>> GetById(int id)
        {
            var baseResponse = new BaseResponse<User>();

            baseResponse.Description = "Get User by id";
            
            try
            {
                baseResponse.Data = await _repository.Get(id);
                baseResponse.StatusCode = StatusCode.OK;
            }
            catch (Exception)
            {
                baseResponse.StatusCode = StatusCode.InternalServerError;
            }

            return baseResponse;
        }

        public async Task<IBaseResponse<User>> GetByLogin(string login)
        {
            var baseResponse = new BaseResponse<User>();

            baseResponse.Description = "Get User by login";

            try
            {
                baseResponse.Data = await _repository.GetByLogin(login);
                baseResponse.StatusCode = StatusCode.OK;
            }
            catch (Exception)
            {
                baseResponse.StatusCode = StatusCode.InternalServerError;
            }

            return baseResponse;
        }


        public async Task<IBaseResponse<List<ParseTask>>> GetUserTasks(string login)
        {
            var baseResponse = new BaseResponse<List<ParseTask>>();

            baseResponse.Description = "Get User tasks";

            try
            {
                baseResponse.Data = await _repository.GetUserTasks(login);
                baseResponse.StatusCode = StatusCode.OK;
            }
            catch (Exception)
            {
                baseResponse.StatusCode = StatusCode.InternalServerError;
            }

            return baseResponse;
        }

        public async Task<IBaseResponse<User>> Update(User user)
        {
            var baseResponse = new BaseResponse<User>();

            baseResponse.Description = "Update User";

            try
            {
                await _repository.Update(user);
                baseResponse.StatusCode = StatusCode.OK;
            }
            catch (Exception)
            {
                baseResponse.StatusCode = StatusCode.InternalServerError;
            }

            return baseResponse;
        }
    }
}

