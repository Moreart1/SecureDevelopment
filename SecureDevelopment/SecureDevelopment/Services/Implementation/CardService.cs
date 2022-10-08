using CardStorageServiceProtos;
using Grpc.Core;
using static CardStorageServiceProtos.CardService;

namespace SecureDevelopment.Services.Implementation
{
    public class CardService : CardServiceBase
    {
        #region Services

        private readonly ICardRepositoryService _cardRepositoryService;

        #endregion


        public CardService(ICardRepositoryService cardRepositoryService)
        {
            _cardRepositoryService = cardRepositoryService;
        }

        public override Task<GetByClientIdResponse> GetByClientId(GetByClientIdRequest request, ServerCallContext context)
        {
            var response = new GetByClientIdResponse();

            response.Cards.AddRange(_cardRepositoryService.GetByClientId(request.ClientId.ToString())
                .Select(card => new Card
                {
                    CardNo = card.CardNo,
                    CVV2 = card.CVV2,
                    ExpDate = card.ExpDate.ToShortDateString(),
                    Name = card.Name
                }).ToList());

            return Task.FromResult(response);

        }
    }
}
