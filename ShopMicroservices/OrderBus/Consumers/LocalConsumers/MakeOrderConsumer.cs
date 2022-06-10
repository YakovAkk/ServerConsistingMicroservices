using GlobalContracts.Contracts;
using MassTransit;
using OrderBus.Contracts;

namespace OrderBus.Consumers.LocalConsumers
{
    public class MakeOrderConsumer : IConsumer<OrderContract>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IRequestClient<IsBasketExistContract> _isBasketExistClient;
        private readonly IRequestClient<IsUserExistContract> _isUserExistClient;
        private readonly IRequestClient<DeleteFromBasketByIdContract> _deleteFromBasketByIdClient;
        private readonly IRequestClient<AddToHistoryContract> _addToHistoryClient;
        private readonly IRequestClient<SendEmailContract> _sendEmailClient;

        public MakeOrderConsumer(IPublishEndpoint publishEndpoint, 
            IRequestClient<IsBasketExistContract> isBasketExistClient,
            IRequestClient<IsUserExistContract> isUserExistClient,
            IRequestClient<DeleteFromBasketByIdContract> deleteFromBasketByIdClient, 
            IRequestClient<AddToHistoryContract> addToHistoryClient, 
            IRequestClient<SendEmailContract> sendEmailClient)
        {
            _publishEndpoint = publishEndpoint;
            _isBasketExistClient = isBasketExistClient;
            _isUserExistClient = isUserExistClient;
            _deleteFromBasketByIdClient = deleteFromBasketByIdClient;
            _addToHistoryClient = addToHistoryClient;
            _sendEmailClient = sendEmailClient;
        }

        public async Task Consume(ConsumeContext<OrderContract> context)
        {
            var IsUserExistsModel = new IsUserExistContract { UserId = context.Message.User_Id };

            var IsUserExist = await _isUserExistClient.GetResponse<IsUserExistContract>(IsUserExistsModel);

            bool IsAllOk = true;

            foreach (var basketId in context.Message.BasketIds)
            {
                var IsBasketExistModel = new IsBasketExistContract() { BasketId = basketId };

                var isBasketExist = await _isBasketExistClient.GetResponse<IsBasketExistContract>(IsBasketExistModel);

                if (!isBasketExist.Message.IsBasketyExist)
                {
                    IsAllOk = false;
                    break;
                }
            }

            if (IsAllOk && IsUserExist.Message.IsUserExist)
            {
                var DeleteFromBasketByIdModel = new DeleteFromBasketByIdContract()
                {
                    basketIdList = context.Message.BasketIds
                };

                var DeleteFromBasketByIdData = await
                    _deleteFromBasketByIdClient.GetResponse<DeleteFromBasketByIdContract>(DeleteFromBasketByIdModel);

                if (DeleteFromBasketByIdData.Message.IsEverythingOk)
                {
                    var addToHistoryModel = new AddToHistoryContract()
                    {
                        User_Id = context.Message.User_Id,
                        Orders = DeleteFromBasketByIdData.Message.baskets
                    };

                    var addToHistoryData = await _addToHistoryClient.GetResponse<AddToHistoryContract>(addToHistoryModel);

                    if (addToHistoryData.Message.MessageWhatWrong == null)
                    {
                        var SendEmailModel = new SendEmailContract()
                        {
                            ToEmail = "Ad",
                            Subject = "me",
                            Body = "asd"
                        };

                        var SendEmailData = await _sendEmailClient.GetResponse<SendEmailContract>(SendEmailModel);

                        if (SendEmailData.Message.MessageWhatWrong == null)
                        {
                            var data = new OrderContract()
                            {
                                IsOrderCompleted = true
                            };
                            await context.RespondAsync<OrderContract>(data);
                        }
                        else
                        {
                            var orderResponce = new OrderContract()
                            {
                                MessageWhatWrong = "The Email can't be Sent"
                            };
                            await context.RespondAsync<OrderContract>(orderResponce);
                        }
                    }
                    else
                    {
                        var orderResponce = new OrderContract()
                        {
                            MessageWhatWrong = "The Basket can't be added to history"
                        };
                        await context.RespondAsync<OrderContract>(orderResponce);
                    }
                }
                else
                {
                    var resOrder = new OrderContract()
                    {
                        MessageWhatWrong = "The Basket can't be cleared"
                    };
                    await context.RespondAsync<OrderContract>(resOrder);
                }
            }
            else
            {
                var resOrder = new OrderContract();
                resOrder.MessageWhatWrong = "Basket Or User don't exist";
                await context.RespondAsync<OrderContract>(resOrder);
                // await _publishEndpoint.Publish(resOrder);
            }
        }
    }
}
