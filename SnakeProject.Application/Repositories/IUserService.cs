using SnakeProject.Application.DTOs;
using SnakeProject.Application.DTOs.User;

namespace SnakeProject.Application.Repositories
{
    public interface IUserService
    {
        Task<Result<UserProfileResponse>> GetProfileAsync(string userId);
        Task<Result> UpdateProfileAsync(string userId, UpdateProfileRequest request);
        Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request);
    }
}
