namespace MediaSharp.Core.Model
{
    public abstract class HandlerExecutionPipeStep
    {
        protected HandlerExecutionPipeStep()
        {
        }

        public virtual Task<IRequest<TResult>> ExecuteAsync<TResult>(IRequest<TResult> request, CancellationToken cancellationToken)
            where TResult : class
        {
            throw new NotImplementedException();
        }

        public virtual IRequest<TResult> Execute<TResult>(IRequest<TResult> request)
            where TResult : class
        {
            throw new NotImplementedException();
        }
    }
}
