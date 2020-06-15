using Castle.DynamicProxy;
using Is4.Common.CustomAttributes;
using Is4.Domain.Repostitory;
using System.Reflection;
using System.Threading.Tasks;

namespace Is4.Service.Interceptor
{
    /// <summary>
    /// 完成自动提交工作单元
    /// </summary>
    public class AutoUnitOfWorkInterceptor : IAsyncInterceptor
    {
        private readonly IUnitOfWork _unitOfWork;
        public AutoUnitOfWorkInterceptor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void InterceptAsynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous(invocation);
        }

        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous<TResult>(invocation);
        }

        private async Task InternalInterceptAsynchronous(IInvocation invocation)
        {
            // Step 1. Do something prior to invocation.
            invocation.Proceed();
            var task = (Task)invocation.ReturnValue;
            await task;

            // Step 2. Do something after invocation.
            await Commit(invocation);
        }

        public void InterceptSynchronous(IInvocation invocation)
        {
            // Step 1. Do something prior to invocation.       
            invocation.Proceed();

            // Step 2. Do something after invocation.
            Commit(invocation).Wait();
        }

        private async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation)
        {
            // Step 1. Do something prior to invocation.      
            invocation.Proceed();
            var task = (Task<TResult>)invocation.ReturnValue;
            TResult result = await task;

            // Step 2. Do something after invocation.
            await Commit(invocation);
            return result;
        }

        private async Task Commit(IInvocation invocation)
        {
            var attr = invocation.GetConcreteMethodInvocationTarget().GetCustomAttribute<UnitOfWorkAttribute>();
            if (attr == null || attr.Enable)
            {
                await _unitOfWork.Commit();
            }
        }
    }
}
