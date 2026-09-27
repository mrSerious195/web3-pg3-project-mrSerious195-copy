using Microsoft.AspNetCore.Authorization;

namespace VoxAuth;

public class VoxIdentityRequirements(string claim, string role) : IAuthorizationRequirement
{
        public string Claim { get; } = claim;
    public string Role { get; } = role;
}