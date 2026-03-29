using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SnakeProject.Domain.Entities;
using System.Security.Claims;

namespace SnakeProject.API.Authentication;

public class RolePermissionSeeder(
    RoleManager<ApplicationRole> roleManager,
    UserManager<ApplicationUser> userManager,
    IOptions<AdminSeedOptions> adminSeedOptions)
{
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly AdminSeedOptions _adminSeed = adminSeedOptions.Value;

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        await SeedRolesAsync(cancellationToken);
        await SeedPermissionsAsync(cancellationToken);
        await SeedAdminUserAsync(cancellationToken);
    }

    private async Task SeedRolesAsync(CancellationToken cancellationToken)
    {
        foreach (var roleName in DefaultRoles.All)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (await _roleManager.RoleExistsAsync(roleName))
                continue;

            var role = new ApplicationRole
            {
                Name = roleName,
                IsDefault = roleName == DefaultRoles.Member,
                IsDeleted = false
            };

            await _roleManager.CreateAsync(role);
        }
    }

    private async Task SeedPermissionsAsync(CancellationToken cancellationToken)
    {
        var adminRole = await _roleManager.FindByNameAsync(DefaultRoles.Admin);
        var memberRole = await _roleManager.FindByNameAsync(DefaultRoles.Member);

        if (adminRole is not null)
            await EnsureRolePermissionsAsync(adminRole, Permissions.GetAllPermissions()!, cancellationToken);

        if (memberRole is not null)
        {
            var memberPermissions = new[]
            {
                Permissions.ProductsRead,
                Permissions.CategoriesRead,
                Permissions.PsnCodesRead,
                Permissions.PsnCodesInventorySummary,
                Permissions.CartRead,
                Permissions.CartAddItem,
                Permissions.CartRemoveItem,
                Permissions.CartClear,
                Permissions.CartCheckout,
                Permissions.OrdersRead,
                Permissions.OrdersCreate,
                Permissions.OrdersCancel
            };

            await EnsureRolePermissionsAsync(memberRole, memberPermissions, cancellationToken);
        }
    }

    private async Task EnsureRolePermissionsAsync(ApplicationRole role, IEnumerable<string> targetPermissions, CancellationToken cancellationToken)
    {
        var existingClaims = await _roleManager.GetClaimsAsync(role);
        var existingPermissions = existingClaims
            .Where(c => c.Type == Permissions.Type)
            .Select(c => c.Value)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        foreach (var permission in targetPermissions)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (existingPermissions.Contains(permission))
                continue;

            await _roleManager.AddClaimAsync(role, new Claim(Permissions.Type, permission));
        }
    }

    private async Task SeedAdminUserAsync(CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_adminSeed.Email) || string.IsNullOrWhiteSpace(_adminSeed.Password))
            return;

        var admin = await _userManager.FindByEmailAsync(_adminSeed.Email);

        if (admin is null)
        {
            admin = new ApplicationUser
            {
                UserName = _adminSeed.Email,
                Email = _adminSeed.Email,
                EmailConfirmed = true,
                FirstName = _adminSeed.FirstName,
                LastName = _adminSeed.LastName
            };

            var createResult = await _userManager.CreateAsync(admin, _adminSeed.Password);
            if (!createResult.Succeeded)
                return;
        }

        if (!await _userManager.IsInRoleAsync(admin, DefaultRoles.Admin))
            await _userManager.AddToRoleAsync(admin, DefaultRoles.Admin);

        if (!await _userManager.IsInRoleAsync(admin, DefaultRoles.Member))
            await _userManager.AddToRoleAsync(admin, DefaultRoles.Member);
    }
}
