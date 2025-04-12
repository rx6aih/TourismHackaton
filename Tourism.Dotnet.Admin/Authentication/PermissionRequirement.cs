using Microsoft.AspNetCore.Authorization;
using Tourism.Dotnet.Admin.DAL.Enums;

namespace Tourism.Dotnet.Admin.Authentication;

public class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(Permission[] permissions)
    {
        Permissions = permissions;
    }
    public Permission[] Permissions { get; set; } = [];
}