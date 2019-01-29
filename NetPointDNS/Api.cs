using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NetPointDNS.Dtos.Request;
using NetPointDNS.Dtos.Response;
using NetPointDNS.Exceptions;
using NetPointDNS.Http;
using NetPointDNS.Resources;
using Newtonsoft.Json;

namespace NetPointDNS
{
    public class Api
    {
        public const string BaseUrl = "https://api.pointhq.com/zones";

        private readonly string _user;
        private readonly string _token;

        private readonly IClient _client;
        private readonly Credentials _credentials;

        public Api(IClient client, string user, string token)
        {
            _user = user;
            _token = token;

            _credentials = new Credentials
            {
                User = _user,
                Token = _token
            };

            _client = client;
            _client.SetCredentials(_credentials);
        }

        public Api(string user, string token)
        {
            _user = user;
            _token = token;

            _credentials = new Credentials
            {
                User = _user,
                Token = _token
            };

            _client = new Client(_credentials);
        }

        private void ErrorHandler(HttpStatusCode code)
        {
            switch (code)
            {
                case HttpStatusCode.Forbidden:
                    throw new ForbiddenException("Access to this resource is not allowed.");
                case HttpStatusCode.NotFound:
                    throw new NotFoundException("Resource not found.");
                case HttpStatusCode.Conflict:
                    throw new ConflictException();
                case HttpStatusCode.InternalServerError:
                    throw new Exception("Internal server error.");
            }

            if ((int) code == 422)
            {
                throw new UnprocessableEntityException("Could not process this entity.");
            }
        }

        #region Zones 

        public async Task<IEnumerable<Zone>> GetZonesAsync()
        {
            var response = await _client.Get(BaseUrl);

            ErrorHandler(response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var dtos = JsonConvert.DeserializeObject<List<ZoneResponse>>(content);

            var zoneList = new List<Zone>();

            foreach (var dto in dtos)
            {
                zoneList.Add(dto.Extract());
            }

            return zoneList;
        }

        public async Task<Zone> GetZoneAsync(int zoneId)
        {
            var response = await _client.Get($"{BaseUrl}/{zoneId}");

            ErrorHandler(response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<ZoneResponse>(content);

            return dto.Extract();
        }

        public async Task<Zone> CreateZoneAsync(string name, string group = null, string template = null)
        {
            var dto = new ZoneRequest
            {
                Group = group,
                Name = name,
                Template = template
            };

            var response = await _client.Post(BaseUrl, dto.Prepare());
            ErrorHandler(response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ZoneResponse>(content);

            return responseDto.Extract();
        }

        public async Task<Zone> UpdateZoneAsync(int zoneId, string group = null)
        {
            var dto = new ZoneRequest
            {
                Group = group
            };

            var response = await _client.Put($"{BaseUrl}/{zoneId}", dto.Prepare());
            ErrorHandler(response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ZoneResponse>(content);

            return responseDto.Extract();
        }

        public async Task<bool> DeleteZoneAsync(int zoneId)
        {
            var response = await _client.Delete($"{BaseUrl}/{zoneId}");
            ErrorHandler(response.StatusCode);

            return await Task.FromResult(true);
        }

        #endregion

        #region ZoneRecords

        public async Task<IEnumerable<ZoneRecord>> GetRecordsForZoneAsync(int zoneId)
        {
            var response = await _client.Get($"{BaseUrl}/{zoneId}/records");
            ErrorHandler(response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<List<ZoneRecordResponse>>(content);

            var recordList = new List<ZoneRecord>();

            foreach (var dto in responseDto)
            {
                recordList.Add(dto.Extract());
            }

            return recordList;
        }

        public async Task<ZoneRecord> GetRecordForZoneAsync(int zoneId, int recordId)
        {
            var response = await _client.Get($"{BaseUrl}/{zoneId}/records/{recordId}");
            ErrorHandler(response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ZoneRecordResponse>(content);

            return responseDto.Extract();
        }

        public async Task<IEnumerable<ZoneRecord>> GetFilteredRecordsForZoneAsync(int zoneId,
            RecordType type, string name)
        {
            var response = await _client.Get($"{BaseUrl}/{zoneId}/records?record_type={type}&name={name}");
            ErrorHandler(response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<List<ZoneRecordResponse>>(content);

            var recordList = new List<ZoneRecord>();

            foreach (var dto in responseDto)
            {
                recordList.Add(dto.Extract());
            }

            return recordList;
        }

        public async Task<ZoneRecord> CreateRecordAsync(int zoneId, string name, RecordType type, string data,
            int ttl, string aux = null)
        {
            var dto = new ZoneRecordRequest
            {
                Aux = aux,
                Data = data,
                Name = name,
                RecordType = type,
                Ttl = ttl
            };

            var response = await _client.Post($"{BaseUrl}/{zoneId}/records", dto.Prepare());
            ErrorHandler(response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ZoneRecordResponse>(content);

            return responseDto.ZoneRecord;
        }

        public async Task<ZoneRecord> UpdateRecordAsync(int zoneId, int recordId, string name, RecordType type,
            string data, int ttl, string aux = null)
        {
            var dto = new ZoneRecordRequest
            {
                Aux = aux,
                Data = data,
                Name = name,
                RecordType = type,
                Ttl = ttl
            };

            var response = await _client.Put($"{BaseUrl}/{zoneId}/records/{recordId}", dto.Prepare());
            ErrorHandler(response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ZoneRecordResponse>(content);

            return responseDto.ZoneRecord;
        }

        public async Task<bool> DeleteRecordAsync(int zoneId, int recordId)
        {
            var response = await _client.Delete($"{BaseUrl}/{zoneId}/records/{recordId}");
            ErrorHandler(response.StatusCode);

            return await Task.FromResult(true);
        }

        #endregion

        #region ZoneRedirect

        public async Task<IEnumerable<ZoneRedirect>> GetRedirectsForZoneAsync(int zoneId)
        {
            var response = await _client.Get($"{BaseUrl}/{zoneId}/redirects");
            ErrorHandler(response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<List<ZoneRedirectResponse>>(content);

            var redirectList = new List<ZoneRedirect>();

            foreach (var dto in responseDto)
            {
                redirectList.Add(dto.Extract());
            }

            return redirectList;
        }

        public async Task<ZoneRedirect> GetRedirectForZoneAsync(int zoneId, int redirectId)
        {
            var response = await _client.Get($"{BaseUrl}/{zoneId}/redirects/{redirectId}");
            ErrorHandler(response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ZoneRedirectResponse>(content);

            return responseDto.Extract();
        }

        public async Task<ZoneRedirect> CreateRedirectAsync(int zoneId, string name,
            string redirectTo, RedirectType type, bool redirectQueryString, string iframeTitle = null)
        {
            var requestDto = new ZoneRedirectRequest
            {
                IframeTitle = iframeTitle,
                Name = name,
                RedirectQueryString = redirectQueryString,
                RedirectTo = redirectTo,
                RedirectType = type
            };

            var response = await _client.Post($"{BaseUrl}/{zoneId}/redirects", requestDto.Prepare());
            ErrorHandler(response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ZoneRedirectResponse>(content);

            return responseDto.Extract();
        }

        public async Task<ZoneRedirect> UpdateRedirectAsync(int zoneId, int redirectId, string name,
            string redirectTo, RedirectType type, bool redirectQueryString, string iframeTitle = null)
        {
            var requestDto = new ZoneRedirectRequest
            {
                IframeTitle = iframeTitle,
                Name = name,
                RedirectQueryString = redirectQueryString,
                RedirectTo = redirectTo,
                RedirectType = type
            };

            var response = await _client.Put($"{BaseUrl}/{zoneId}/redirects/{redirectId}", 
                requestDto.Prepare());
            ErrorHandler(response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ZoneRedirectResponse>(content);

            return responseDto.Extract();
        }

        public async Task<bool> DeleteRedirectAsync(int zoneId, int redirectId)
        {
            var response = await _client.Delete($"{BaseUrl}/{zoneId}/redirects/{redirectId}");
            ErrorHandler(response.StatusCode);

            return await Task.FromResult(true);
        }
        #endregion

        #region MailRedirect

        public async Task<IEnumerable<ZoneMailRedirect>> GetMailRedirectsForZoneAsync(int zoneId)
        {
            var response = await _client.Get($"{BaseUrl}/{zoneId}/mail_redirects");
            ErrorHandler(response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var responseDtos = JsonConvert.DeserializeObject<List<ZoneMailRedirectResponse>>(content);

            var mailRedirectList = new List<ZoneMailRedirect>();

            foreach (var dto in responseDtos)
            {
                mailRedirectList.Add(dto.Extract());
            }

            return mailRedirectList;
        }

        public async Task<ZoneMailRedirect> GetMailRedirectForZoneAsync(int zoneId, int mailRedirectId)
        {
            var response = await _client.Get($"{BaseUrl}/{zoneId}/mail_redirects/{mailRedirectId}");
            ErrorHandler(response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ZoneMailRedirectResponse>(content);

            return responseDto.Extract();
        }

        public async Task<ZoneMailRedirect> CreateMailRedirectAsync(int zoneId, string sourceAddress,
            string destinationAddress)
        {
            var requestDto = new ZoneMailRedirectRequest
            {
                DestinationAddress = destinationAddress,
                SourceAddress = sourceAddress
            };

            var response = await _client.Post($"{BaseUrl}/{zoneId}/mail_redirects", requestDto.Prepare());
            ErrorHandler(response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ZoneMailRedirectResponse>(content);

            return responseDto.Extract();
        }

        public async Task<ZoneMailRedirect> UpdateMailRedirectAsync(int zoneId, int mailRedirectId,
            string sourceAddress, string destinationAddress)
        {
            var requestDto = new ZoneMailRedirectRequest
            {
                DestinationAddress = destinationAddress,
                SourceAddress = sourceAddress
            };

            var response = await _client.Put($"{BaseUrl}/{zoneId}/mail_redirects/{mailRedirectId}", 
                requestDto.Prepare());
            ErrorHandler(response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ZoneMailRedirectResponse>(content);

            return responseDto.Extract();
        }

        public async Task<bool> DeleteMailRedirectAsync(int zoneId, int mailRedirectId)
        {
            var response = await _client.Delete($"{BaseUrl}/{zoneId}/mail_redirects/{mailRedirectId}");
            ErrorHandler(response.StatusCode);

            return await Task.FromResult(true);
        }

        #endregion
    }
}
