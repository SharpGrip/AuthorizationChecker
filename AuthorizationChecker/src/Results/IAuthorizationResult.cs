using System;
using System.Collections.Generic;

namespace SharpGrip.AuthorizationChecker.Results
{
    public interface IAuthorizationResult<out TSubject>
    {
        /// <summary>
        /// The mode.
        /// </summary>
        public string Mode { get; }

        /// <summary>
        /// The subject.
        /// </summary>
        public TSubject Subject { get; }

        /// <summary>
        /// The access decision strategy.
        /// </summary>
        public AccessDecisionStrategy AccessDecisionStrategy { get; }

        /// <summary>
        /// The authorization results.
        /// </summary>
        public IDictionary<Type, bool> Results { get; }

        /// <summary>
        /// Determines if the provided mode is allowed on the provided subject.
        /// </summary>
        /// <returns>True if the provided mode is allowed on the provided subject, False otherwise.</returns>
        public bool IsAllowed();
    }
}