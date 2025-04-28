using Microsoft.Extensions.Hosting;
using Miningcore.Messaging;
using Miningcore.Notifications.Messages;

namespace Miningcore.Payments;

public class PayoutSchemeCoordinator : BackgroundService
{
    private readonly IMessageBus messageBus;
    
    public PayoutSchemeCoordinator(IMessageBus messageBus)
    {
        this.messageBus = messageBus;
    }
    
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        await Task.CompletedTask;
    }
}