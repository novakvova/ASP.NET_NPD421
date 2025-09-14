using Microsoft.AspNetCore.Identity;

namespace WebATB.Data.Entities.Idenity;

public class RoleEntity : IdentityRole<int>
{
    public ICollection<UserRoleEntity> UserRoles { get; set; } = null!;
}
