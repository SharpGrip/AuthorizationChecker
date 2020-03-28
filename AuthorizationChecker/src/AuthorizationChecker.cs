using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SharpGrip.AuthorizationChecker.Options;
using SharpGrip.AuthorizationChecker.Results;

namespace SharpGrip.AuthorizationChecker
{
    public class AuthorizationChecker : IAuthorizationChecker
    {
        private readonly IServiceProvider serviceProvider;
        private AccessDecisionStrategy AccessDecisionStrategy { get; }
        private bool UnanimousVoteAllowIfAllAbstain { get; }

        public AuthorizationChecker(IServiceProvider serviceProvider, IOptions<AuthorizationCheckerOptions> options)
        {
            this.serviceProvider = serviceProvider;
            AccessDecisionStrategy = options.Value.AccessDecisionStrategy;
            UnanimousVoteAllowIfAllAbstain = options.Value.UnanimousVoteAllowIfAllAbstain;
        }

        public bool IsAllowed<TSubject>(string mode, TSubject subject)
        {
            return GetAuthorizationResult(mode, subject, AccessDecisionStrategy).IsAllowed();
        }

        public bool IsAllowed<TSubject, TUser>(string mode, TSubject subject, TUser user)
        {
            return GetAuthorizationResult(mode, subject, user, AccessDecisionStrategy).IsAllowed();
        }

        public bool IsAllowed<TSubject>(string mode, TSubject subject, AccessDecisionStrategy accessDecisionStrategy)
        {
            return GetAuthorizationResult(mode, subject, accessDecisionStrategy).IsAllowed();
        }

        public bool IsAllowed<TSubject, TUser>(string mode, TSubject subject, TUser user, AccessDecisionStrategy accessDecisionStrategy)
        {
            return GetAuthorizationResult(mode, subject, user, accessDecisionStrategy).IsAllowed();
        }

        public IAuthorizationResult<TSubject> GetAuthorizationResult<TSubject>(string mode, TSubject subject)
        {
            return GetAuthorizationResult(mode, subject, AccessDecisionStrategy);
        }

        public IAuthorizationResult<TSubject> GetAuthorizationResult<TSubject, TUser>(string mode, TSubject subject, TUser user)
        {
            return GetAuthorizationResult(mode, subject, user, AccessDecisionStrategy);
        }

        public IAuthorizationResult<TSubject> GetAuthorizationResult<TSubject>(
            string mode,
            TSubject subject,
            AccessDecisionStrategy accessDecisionStrategy)
        {
            var voterServices = serviceProvider.GetServices<IVoter<TSubject>>();
            var authorizationResult =
                new AuthorizationResult<TSubject>(mode, subject, accessDecisionStrategy, UnanimousVoteAllowIfAllAbstain);

            foreach (var voterService in voterServices)
            {
                if (voterService.WillVote(mode, subject))
                {
                    authorizationResult.Results.Add(voterService.GetType(), voterService.Vote(mode, subject));
                }
            }

            return authorizationResult;
        }

        public IAuthorizationResult<TSubject> GetAuthorizationResult<TSubject, TUser>(
            string mode,
            TSubject subject,
            TUser user,
            AccessDecisionStrategy accessDecisionStrategy)
        {
            var voterServices = serviceProvider.GetServices<IVoter<TSubject, TUser>>();
            var authorizationResult =
                new AuthorizationResult<TSubject>(mode, subject, accessDecisionStrategy, UnanimousVoteAllowIfAllAbstain);

            foreach (var voterService in voterServices)
            {
                if (voterService.WillVote(mode, subject, user))
                {
                    authorizationResult.Results.Add(voterService.GetType(), voterService.Vote(mode, subject, user));
                }
            }

            return authorizationResult;
        }
    }
}