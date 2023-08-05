namespace SharpGrip.AuthorizationChecker
{
    public enum AccessDecisionStrategy
    {
        /// <summary>
        /// Grants access as soon as there is one <see cref="IVoter"/> instance granting access.
        /// </summary>
        Affirmative = 1,

        /// <summary>
        /// Grants access if there are more <see cref="IVoter"/> instances granting access than denying it.
        /// </summary>
        Majority = 2,

        /// <summary>
        /// Grants access if there are no <see cref="IVoter"/> instances denying access.
        /// </summary>
        Unanimous = 3
    }
}