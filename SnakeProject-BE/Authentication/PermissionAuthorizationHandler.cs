using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace SnakeProject.API.Authentication
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User.Identity is not { IsAuthenticated: true })
                return Task.CompletedTask;

            // Admin bypass: support roles claim emitted as JSON array ("roles").
            var rolesClaims = context.User.Claims.Where(c => c.Type == "roles").Select(c => c.Value);
            foreach (var roleClaim in rolesClaims)
            {
                try
                {
                    var roles = JsonSerializer.Deserialize<List<string>>(roleClaim);
                    if (roles?.Any(r => string.Equals(r, DefaultRoles.Admin, StringComparison.OrdinalIgnoreCase)) == true)
                    {
                        context.Succeed(requirement);
                        return Task.CompletedTask;
                    }
                }
                catch (JsonException)
                {
                    // ignore
                }
            }

            // Permission checks: support direct claim value and JSON array value.
            var permissionClaims = context.User.Claims
                .Where(x => x.Type == Permissions.Type)
                .Select(x => x.Value)
                .ToList();

            if (permissionClaims.Count == 0)
                return Task.CompletedTask;

            foreach (var claimValue in permissionClaims)
            {
                if (string.Equals(claimValue, requirement.Permission, StringComparison.OrdinalIgnoreCase))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }

                try
                {
                    var permissions = JsonSerializer.Deserialize<List<string>>(claimValue);
                    if (permissions?.Any(p => string.Equals(p, requirement.Permission, StringComparison.OrdinalIgnoreCase)) == true)
                    {
                        context.Succeed(requirement);
                        return Task.CompletedTask;
                    }
                }
                catch (JsonException)
                {
                    // ignore
                }
            }

            return Task.CompletedTask;
        }
    }
}
