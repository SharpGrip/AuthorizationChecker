namespace SharpGrip.AuthorizationChecker
{
    public interface IVoter<in TSubject> : IVoter
    {
        /// <summary>
        /// Determines if this <see cref="IVoter{TSubject}"/> instance will vote on the mode and subject.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="subject">The subject.</param>
        /// <returns>True if this <see cref="IVoter{TSubject}"/> instance will vote, False otherwise.</returns>
        public bool WillVote(string mode, TSubject subject);
        
        /// <summary>
        /// Votes on the mode and subject.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="subject">The subject.</param>
        /// <returns>True if this <see cref="IVoter{TSubject}"/> instance allows access, False otherwise.</returns>
        public bool Vote(string mode, TSubject subject);
    }    
    
    public interface IVoter<in TSubject, in TUser> : IVoter
    {
        /// <summary>
        /// Determines if this <see cref="IVoter{TSubject, TUser}"/> instance will vote on the mode and subject.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="user">The user.</param>
        /// <returns>True if this <see cref="IVoter{TSubject, TUser}"/> instance will vote, False otherwise.</returns>
        public bool WillVote(string mode, TSubject subject, TUser user);
        
        /// <summary>
        /// Votes on the mode and subject.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="user">The user.</param>
        /// <returns>True if this <see cref="IVoter{TSubject, TUser}"/> instance allows access, False otherwise.</returns>
        public bool Vote(string mode, TSubject subject, TUser user);
    }

    public interface IVoter
    {
    }
}