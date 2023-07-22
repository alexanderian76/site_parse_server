using System;
using site_parse_server.Models;
using site_parse_server.Repositories.Interfaces;
using site_parse_server.Services.Interfaces;

namespace site_parse_server.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _repository;

        public RequestService(IRequestRepository repository)
        {
            _repository = repository;
        }
        public async Task<IBaseResponse<Request>> Create(Request request)
        {
            var baseResponse = new BaseResponse<Request>();

            baseResponse.Description = "Create Request";


            try
            {
                await _repository.Create(request);
                baseResponse.StatusCode = StatusCode.OK;
            }
            catch (Exception e)
            {
                baseResponse.StatusCode = StatusCode.InternalServerError;
            }

            return baseResponse;
        }
    }
}

