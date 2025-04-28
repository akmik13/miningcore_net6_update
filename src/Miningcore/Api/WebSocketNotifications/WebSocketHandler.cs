using System.Net.WebSockets;
using System.Text;
using WebSocketManager.Common;

namespace WebSocketManager;

/// <summary>
/// Базовый класс обработчика WebSocket, совместимый с .NET 6
/// </summary>
public abstract class WebSocketHandler
{
    protected WebSocketHandler(WebSocketConnectionManager webSocketConnectionManager, 
        Miningcore.Api.WebSocketNotifications.IMethodInvocationStrategy methodInvocationStrategy)
    {
        WebSocketConnectionManager = webSocketConnectionManager;
    }

    public WebSocketConnectionManager WebSocketConnectionManager { get; }

    public abstract Task OnConnected(WebSocket socket);

    public virtual Task OnDisconnected(WebSocket socket)
    {
        return Task.CompletedTask;
    }

    protected async Task SendMessageToAllAsync(Message message)
    {
        foreach(var pair in WebSocketConnectionManager.GetAll())
        {
            if(pair.Value.State == WebSocketState.Open)
            {
                var data = Encoding.UTF8.GetBytes(message.Data);
                var buffer = new ArraySegment<byte>(data);
                
                await pair.Value.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}