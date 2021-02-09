using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VictoryLinkTask.Data;
using VictoryLinkTask.Repositories.Interfaces;
using VictoryLinkTask.ServiceLayer.Dto.CommonDtos;
using VictoryLinkTask.ServiceLayer.Dto.Request;
using VictoryLinkTask.ServiceLayer.IAppService;

namespace VictoryLinkTask.ServiceLayer.AppService
{
    public class PromotionAppService : IPromotionAppService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PromotionAppService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetAllUnHandeledRequestsResponseDto>> GetAllUnHandeledRequests()
        {
            List<Request> requests = await _unitOfWork.RequestRepository.GetWhereAsync(ur => ur.Handled == false);
            List<GetAllUnHandeledRequestsResponseDto> getAllUnHandeledRequestsResponseDtos = requests?.Select(r => new GetAllUnHandeledRequestsResponseDto
            {
                MobileNumber = r.MobileNumber
            }).ToList();
            return getAllUnHandeledRequestsResponseDtos;
        }

        public async Task<ResponseDto> HandelRequest(HandelRequestInputDto handelRequestInputDto)
        {
            ResponseDto responseDto = new ResponseDto
            {
                Message = "Failed",
                Status = 2
            };
            Request request = await _unitOfWork.RequestRepository.FirstOrDefaultAsync(r => r.MobileNumber == handelRequestInputDto.MobileNumber);
            if (request != null)
            {
                request.Handled = true;
                request.HandlingDate = DateTime.Now;
                _unitOfWork.RequestRepository.Update(request);
                bool handled = await _unitOfWork.Commit() > default(byte);
                if (handled)
                    responseDto = new ResponseDto
                    {
                        Message = "Success",
                        Status = 1
                    };
            }
            return responseDto;
        }

        public async Task<ResponseDto> ReceiveRequest(ReceiveRequestInputDto receiveRequestInputDto)
        {
            ResponseDto responseDto = new ResponseDto
            {
                Message = "Failed",
                Status = 2
            };
            Request request = new Request
            {
                RequestDate = DateTime.Now,
                MobileNumber = receiveRequestInputDto.MobileNumber,
                Action = receiveRequestInputDto.Action
            };
            _unitOfWork.RequestRepository.CreateAsyn(request);
            bool created = await _unitOfWork.Commit() > default(byte);
            if (created)
                responseDto = new ResponseDto
                {
                    Message = "Success",
                    Status = 1
                };
            return responseDto;
        }
    }
}