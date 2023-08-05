using SharpGrip.AuthorizationChecker.Results;

namespace SharpGrip.AuthorizationChecker
{
    public interface IAuthorizationChecker
    {
        /// <summary>
        /// Determines if the provided <paramref name="mode"/> is allowed on the provided <paramref name="subject"/>.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="subject">The subject.</param>
        /// <typeparam name="TSubject">The subject's type.</typeparam>
        /// <returns>True if the provided <paramref name="mode"/> is allowed on the provided <paramref name="subject"/>, False otherwise.</returns>
        public bool IsAllowed<TSubject>(string mode, TSubject subject);

        /// <summary>
        /// Determines if the provided <paramref name="mode"/> is allowed on the provided <paramref name="subject"/>.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="user">The user.</param>
        /// <typeparam name="TSubject">The subject's type.</typeparam>
        /// <typeparam name="TUser">The user's type.</typeparam>
        /// <returns>True if the provided <paramref name="mode"/> is allowed on the provided <paramref name="subject"/>, False otherwise.</returns>
        public bool IsAllowed<TSubject, TUser>(string mode, TSubject subject, TUser user);

        /// <summary>
        /// Determines if the provided <paramref name="mode"/> is allowed on the provided <paramref name="subject"/>.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="accessDecisionStrategy">The access decision strategy.</param>
        /// <typeparam name="TSubject">The subject's type.</typeparam>
        /// <returns>True if the provided <paramref name="mode"/> is allowed on the provided <paramref name="subject"/>, False otherwise.</returns>
        public bool IsAllowed<TSubject>(string mode, TSubject subject, AccessDecisionStrategy accessDecisionStrategy);

        /// <summary>
        /// Determines if the provided <paramref name="mode"/> is allowed on the provided <paramref name="subject"/>.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="user">The user.</param>
        /// <param name="accessDecisionStrategy">The access decision strategy.</param>
        /// <typeparam name="TSubject">The subject's type.</typeparam>
        /// <typeparam name="TUser">The user's type.</typeparam>
        /// <returns>True if the provided <paramref name="mode"/> is allowed on the provided <paramref name="subject"/>, False otherwise.</returns>
        public bool IsAllowed<TSubject, TUser>(string mode, TSubject subject, TUser user, AccessDecisionStrategy accessDecisionStrategy);

        /// <summary>
        /// Returns the authorization result.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="subject">The subject.</param>
        /// <typeparam name="TSubject">The subject's Type</typeparam>
        /// <returns>The authorization result.</returns>
        public IAuthorizationResult<TSubject> GetAuthorizationResult<TSubject>(string mode, TSubject subject);

        /// <summary>
        /// Returns the authorization result.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="user">The user.</param>
        /// <typeparam name="TSubject">The subject's Type</typeparam>
        /// <typeparam name="TUser">The user's Type</typeparam>
        /// <returns>The authorization result.</returns>
        public IAuthorizationResult<TSubject> GetAuthorizationResult<TSubject, TUser>(string mode, TSubject subject, TUser user);

        /// <summary>
        /// Returns the authorization result.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="accessDecisionStrategy">The access decision strategy.</param>
        /// <typeparam name="TSubject">The subject's Type</typeparam>
        /// <returns>The authorization result.</returns>
        public IAuthorizationResult<TSubject> GetAuthorizationResult<TSubject>(string mode, TSubject subject, AccessDecisionStrategy accessDecisionStrategy);

        /// <summary>
        /// Returns the authorization result.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="user">The user.</param>
        /// <param name="accessDecisionStrategy">The access decision strategy.</param>
        /// <typeparam name="TSubject">The subject's Type</typeparam>
        /// <typeparam name="TUser">The user's Type</typeparam>
        /// <returns>The authorization result.</returns>
        public IAuthorizationResult<TSubject> GetAuthorizationResult<TSubject, TUser>(string mode, TSubject subject, TUser user, AccessDecisionStrategy accessDecisionStrategy);
    }
}