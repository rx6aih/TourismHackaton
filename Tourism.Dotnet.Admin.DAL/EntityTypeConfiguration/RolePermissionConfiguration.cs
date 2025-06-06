using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tourism.Dotnet.Admin.DAL.Entities;

namespace Tourism.Dotnet.Admin.DAL.EntityTypeConfiguration;

public class RolePermissionConfiguration(AuthorizationOptions options) : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasKey(r => new { r.RoleId, r.PermissionId });
        builder.HasData(ParseRolePermissions()); 
    }
    private RolePermission[] ParseRolePermissions()
    {
        return options.RolePermissions
            .SelectMany(rp => rp.Permission
                .Select(p => new RolePermission
                {
                    RoleId = (int)Enum.Parse<Enums.Role>(rp.Role),
                    PermissionId = (int)Enum.Parse<Enums.Permission>(p),
                }))
            .ToArray();
    }
}