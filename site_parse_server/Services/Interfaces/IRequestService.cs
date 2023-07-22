using System;
using site_parse_server.Models;

namespace site_parse_server.Services.Interfaces
{
	public interface IRequestService
	{
        Task<IBaseResponse<Request>> Create(Request request);
    }
}

