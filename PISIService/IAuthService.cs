using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PISIAssessment.Model;

namespace PISIAssessment.PISIService
{
    public interface IAuthService
    {
        Task<ResponseService<int>> Register(Service user, string password);
        Task<ResponseService<string>> Login(string username, string password);
        
        Task<bool> UserExists(string username);
    }

    public class ServiceResponse<T>
    {
    }
}