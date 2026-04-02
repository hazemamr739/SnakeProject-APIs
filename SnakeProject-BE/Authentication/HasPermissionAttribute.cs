using Microsoft.AspNetCore.Authorization;

namespace SnakeProject.API.Authentication
{
    public class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
    {
    }
}
