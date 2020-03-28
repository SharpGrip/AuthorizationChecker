namespace SharpGrip.AuthorizationChecker.Options
{
    public class AuthorizationCheckerOptions
    {
        /// <summary>
        /// The default access decision strategy.
        /// </summary>
        public AccessDecisionStrategy AccessDecisionStrategy { get; set; } = AccessDecisionStrategy.Affirmative;
        
        /// <summary>
        /// Determines to allow access in case of an unanimous access decision strategy and all <see cref="IVoter{TSubject}"/> instances abstained from voting.
        /// </summary>
        public bool UnanimousVoteAllowIfAllAbstain { get; set; }
    }
}