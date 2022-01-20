using Microsoft.AspNetCore.Authorization;

namespace InsightIn.Api.Requirements
{
    public class CustomerBlockedStatusRequirement : IAuthorizationRequirement
    {
        public bool IsBlocked { get; }
        public CustomerBlockedStatusRequirement(bool isBlocked)
        {
            IsBlocked = isBlocked;
        }

    }
}
