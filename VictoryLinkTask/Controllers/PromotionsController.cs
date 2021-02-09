using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VictoryLinkTask.Data;
using VictoryLinkTask.ServiceLayer.Dto.CommonDtos;
using VictoryLinkTask.ServiceLayer.Dto.Request;
using VictoryLinkTask.ServiceLayer.IAppService;

namespace VictoryLinkTask.Controllers
{
    public class PromotionsController : ApiController
    {
        private readonly IPromotionAppService _promotionAppService;

        public PromotionsController(IPromotionAppService promotionAppService)
        {
            _promotionAppService = promotionAppService;
        }
        [HttpPost]
        [Route("api/ReceiveRequest")]
        public async Task<IHttpActionResult> ReceiveRequest(ReceiveRequestInputDto receiveRequestInputDto)
        {
            ResponseDto responseDto = await _promotionAppService.ReceiveRequest(receiveRequestInputDto);
            return Ok(responseDto);
        }

        [HttpPost]
        [Route("api/HandelRequest")]
        public async Task<IHttpActionResult> HandelRequest(HandelRequestInputDto handelRequestInputDto)
        {
            ResponseDto responseDto = await _promotionAppService.HandelRequest(handelRequestInputDto);
            return Ok(responseDto);
        }
        [HttpPost]
        [Route("api/GetAllUnHandeledRequests")]
        public async Task<IHttpActionResult> GetAllUnHandeledRequests()
        {
            List<GetAllUnHandeledRequestsResponseDto> responseDto = await _promotionAppService.GetAllUnHandeledRequests();
            return Ok(responseDto);
        }
    }
}