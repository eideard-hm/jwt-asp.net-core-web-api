namespace JwtWebApi.Interfaces
{
    public interface IUserService
    {
        Task<bool> Delete(int userId);
        bool UserExists(string username, string email);
    }
}
