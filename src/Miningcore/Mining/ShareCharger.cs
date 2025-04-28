using Microsoft.Extensions.Hosting;
using Miningcore.Messaging;
using Miningcore.Persistence;

namespace Miningcore.Mining;

public class ShareCharger : BackgroundService
{
    private readonly IConnectionFactory connectionFactory;
    private readonly IMessageBus messageBus;
    
    public ShareCharger(IConnectionFactory connectionFactory, IMessageBus messageBus)
    {
        this.connectionFactory = connectionFactory;
        this.messageBus = messageBus;
    }
    
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        await Task.CompletedTask;
    }
}