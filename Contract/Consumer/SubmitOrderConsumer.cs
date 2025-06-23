using AuthenticationApi.Contract;
using MassTransit;

namespace AuthenticationApi.Contract.Consumer
{
    public class SubmitOrderConsumer : IConsumer<SubmitOrder>
    {
        public Task Consume(ConsumeContext<SubmitOrder> context)
        {
            throw new NotImplementedException();
        }
    };
};