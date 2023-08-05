using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpGrip.AuthorizationChecker.Results
{
    public class AuthorizationResult<TSubject> : IAuthorizationResult<TSubject>
    {
        public string Mode { get; }
        public TSubject Subject { get; }
        public AccessDecisionStrategy AccessDecisionStrategy { get; }
        public bool UnanimousVoteAllowIfAllAbstain { get; }
        public IDictionary<Type, bool> Results { get; } = new Dictionary<Type, bool>();

        public AuthorizationResult(string mode, TSubject subject, AccessDecisionStrategy accessDecisionStrategy, bool unanimousVoteAllowIfAllAbstain)
        {
            Mode = mode;
            Subject = subject;
            AccessDecisionStrategy = accessDecisionStrategy;
            UnanimousVoteAllowIfAllAbstain = unanimousVoteAllowIfAllAbstain;
        }

        public bool IsAllowed()
        {
            switch (AccessDecisionStrategy)
            {
                case AccessDecisionStrategy.Affirmative:
                    return Results.Values.Contains(true);
                case AccessDecisionStrategy.Majority:
                    return Results.Values.Count(result => result) > Results.Values.Count(result => !result);
                case AccessDecisionStrategy.Unanimous:
                    if (Results.Values.Count(result => result) == 0 && UnanimousVoteAllowIfAllAbstain)
                    {
                        return true;
                    }

                    return Results.Values.All(result => result);
                default:
                    return false;
            }
        }
    }
}