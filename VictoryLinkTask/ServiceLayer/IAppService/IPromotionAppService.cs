using System.Collections.Generic;
using System.Threading.Tasks;
using VictoryLinkTask.ServiceLayer.Dto.CommonDtos;
using VictoryLinkTask.ServiceLayer.Dto.Request;

namespace VictoryLinkTask.ServiceLayer.IAppService
{
    public interface IPromotionAppService
    {
        Task<ResponseDto> ReceiveRequest(ReceiveRequestInputDto receiveRequestInputDto);
        Task<ResponseDto> HandelRequest(HandelRequestInputDto handelRequestInputDto);
        Task<List<GetAllUnHandeledRequestsResponseDto>> GetAllUnHandeledRequests();
    }
}
