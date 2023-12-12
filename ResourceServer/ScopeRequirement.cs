using Microsoft.AspNetCore.Authorization;

namespace ResourceServer
{
    public class ScopeRequirement : IAuthorizationRequirement
    {
        public string Scope { get; }

        public ScopeRequirement(string scope)
        {
            Scope = scope;
        }
    }

    public class ScopeHandler : AuthorizationHandler<ScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ScopeRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "scopes" && c.Value.Split(' ').Contains(requirement.Scope)))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public static class AuthorizationPolicyExtensions
    {
        public static void RequireScope(this AuthorizationPolicyBuilder policy, string scope)
        {
            policy.Requirements.Add(new ScopeRequirement(scope));
        }
    }
}
