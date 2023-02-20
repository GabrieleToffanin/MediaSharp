namespace MediaSharp.Core.Model
{
    /// <summary>
    /// This a template for the step
    /// that will be executed.
    /// For the moment this allows only the Execute async
    /// </summary>
    public abstract class HandlerExecutionPipeStep
    {
        protected HandlerExecutionPipeStep()
        {
        }

        /// <summary>
        /// Executes a transformation or operation through the
        /// current <see cref="IRequest{TResult}"/>
        /// </summary>
        /// <typeparam name="TResult">The current expected TResult</typeparam>
        /// <param name="request">Current <see cref="IRequest{TResult}"/> passed in the execute</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The transformed <see cref="IRequest{TResult}"/></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual Task<IRequest<TResult>> ExecuteAsync<TResult>(IRequest<TResult> request, CancellationToken cancellationToken)
            where TResult : class
        {
            throw new NotImplementedException();
        }

        //Not implemented at the moment.
        public virtual IRequest<TResult> Execute<TResult>(IRequest<TResult> request)
            where TResult : class
        {
            throw new NotImplementedException();
        }
    }
}
