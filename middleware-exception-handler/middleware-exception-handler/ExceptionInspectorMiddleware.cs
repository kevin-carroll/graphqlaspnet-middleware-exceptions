namespace Example
{
    using System.Threading;
    using System.Threading.Tasks;
    using Example.Model;
    using GraphQL.AspNet.Execution.Contexts;
    using GraphQL.AspNet.Interfaces.Middleware;
    using GraphQL.AspNet.Middleware;

    /// <summary>
    /// A field execution component that, for each field executed, will
    /// inspect for an exception occuring on the field context and convert it to a friendly
    /// error message attached to the field and clear the exception.
    /// </summary>
    public class ExceptionInspectorMiddleware : IFieldExecutionMiddleware
    {
        // Required interface method
        // this works just like the asp.net http pipeline middleware
        public async Task InvokeAsync(GraphFieldExecutionContext context, GraphMiddlewareInvocationDelegate<GraphFieldExecutionContext> next, CancellationToken cancelToken)
        {
            // PERFORMANCE MATTERS:
            // -------------------------------------------------
            // context represents the current field being executed/rendered for the current
            // data object on the query.
            // There can be A LOT of fields even for the smallest of queries. This method will
            // even run during introspection queries. Make sure this method is
            // optimized for performance as much as possible.

            // if an exception was thrown while processing the field it will be attached to
            // the context object as a critical message
            //
            // in general a successfully executed field will have no messages on the context
            if (context.Messages.Count > 0)
            {
                // if an exception was thrown, convert it to a friendly error message
                // and clear the exception from the context
                foreach (var singleMessage in context.Messages)
                {
                    if (singleMessage.Exception is PublicMessageException pme)
                    {
                        // convert the exception to a friendly error message
                        singleMessage.Message = pme.Message;

                        // clear the exception from the query
                        singleMessage.Exception = null;

                        // change the message response code to something more acceptable
                        singleMessage.Code = "INVALID_RENDER";
                    }
                }
            }

            // continue the middleware pipeline
            await next(context, cancelToken);
        }
    }
}
