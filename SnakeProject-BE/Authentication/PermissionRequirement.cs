using Microsoft.AspNetCore.Authorization;

namespace SnakeProject.API.Authentication
{
    public class PermissionRequirement(string permission) : IAuthorizationRequirement
    {
        public string Permission { get; } = permission;
    }
}
