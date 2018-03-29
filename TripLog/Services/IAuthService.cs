using System;
using System.Threading.Tasks;

namespace TripLog.Services
{
    public interface IAuthService
    {
        Task SignInAsync(Action<string> tokenCallback, Action<string> errorCallback);
    }
}
