using System;
using site_parse_server.Models;
using Microsoft.EntityFrameworkCore;
using site_parse_server.Repositories.Interfaces;

namespace site_parse_server.Repositories
{
    public class RequestRepository : IRequestRepository
    {

        private readonly DataBaseContext _db;
        public RequestRepository(DataBaseContext db)
        {
            _db = db;
        }
        public async Task Create(Request obj)
        {
            await _db.Requests.AddAsync(obj);
            await _db.SaveChangesAsync();
        }

        public async Task<Request> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Request obj)
        {
            throw new NotImplementedException();
        }
    }
}