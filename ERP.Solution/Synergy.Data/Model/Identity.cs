using Microsoft.AspNetCore.Identity;

namespace Synergy.Data.Model;

public class User : IdentityUser<Guid>;
public class Role : IdentityRole<Guid>;