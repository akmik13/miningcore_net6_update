using System.Reflection;

namespace Miningcore.Api.WebSocketNotifications;

public class StringMethodInvocationStrategy : IMethodInvocationStrategy
{
    public Task<object> InvokeMethod(MethodInfo method, object obj, object[] args)
    {
        var result = method.Invoke(obj, args);
        return Task.FromResult(result);
    }
}

public interface IMethodInvocationStrategy
{
    Task<object> InvokeMethod(MethodInfo method, object obj, object[] args);
}