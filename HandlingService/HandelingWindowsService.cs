using HandlingService.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HandlingService
{
    public class HandelingWindowsService
    {

        private async Task DoWork()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44361/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsync("api/GetAllUnHandeledRequests", null);
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    StreamReader streamReader = new StreamReader(stream);
                    var content = streamReader.ReadToEnd();
                    if (!string.IsNullOrWhiteSpace(content))
                    {
                        List<GetAllUnHandeledRequestsResponseDto> getAllUnHandeledRequestsResponseDtos = JsonConvert.DeserializeObject<List<GetAllUnHandeledRequestsResponseDto>>(content);
                        if (getAllUnHandeledRequestsResponseDtos?.Any() ?? default)
                        {
                            foreach (GetAllUnHandeledRequestsResponseDto unhandeledRequest in getAllUnHandeledRequestsResponseDtos)
                            {
                                var json = JsonConvert.SerializeObject(new HandelRequestInputDto { MobileNumber = unhandeledRequest.MobileNumber });
                                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                                HttpResponseMessage httpResponseMessage = await client.PostAsync("api/HandelRequest", stringContent);
                                if (httpResponseMessage.IsSuccessStatusCode)
                                {
                                    var result = await response.Content.ReadAsStreamAsync();
                                    StreamReader reader = new StreamReader(result);
                                    var eof = reader.ReadToEnd();
                                    if (!string.IsNullOrWhiteSpace(eof))
                                    {
                                        ResponseDto responseDto = JsonConvert.DeserializeObject<ResponseDto>(eof);
                                        if (responseDto.Status == 1)
                                        {
                                            Console.WriteLine($"{unhandeledRequest.MobileNumber } Handeled Successfully");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public async Task Start()
        {
            while (true)
            {
                await DoWork();
                Thread.Sleep(30000);
            }
            // write code here that runs when the Windows Service starts up.  
        }
        public void Stop()
        {
            // write code here that runs when the Windows Service stops.  
        }
    }
}
