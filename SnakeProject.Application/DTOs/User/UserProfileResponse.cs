namespace SnakeProject.Application.DTOs.User
{
    public record UserProfileResponse(

         string Email,
         string UserName,
         string FirstName,
         string LastName
    );
}
