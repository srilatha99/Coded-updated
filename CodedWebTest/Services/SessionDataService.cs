using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CodedWebTest.Services
{
    public interface ISessionDataService
    {
        public void ClearSession();
        public void DeleteSession();

        public string EmailAddress { get; set; }
    }

    public class SessionDataService : ISessionDataService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public SessionDataService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            // TODO: Get IHttpContextAccessor via Dependency Injection

        }
        public void ClearSession()
        {
            _session.Clear();
        }

        public void DeleteSession()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(".CodedWebTest.Session");
        }

        // TODO: Implement storing and retriving email address from session
        public string EmailAddress
        {
            get => _session.GetString("EmailAddress");
            set => _session.SetString("EmailAddress", value);
        }
    }
}
