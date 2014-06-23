using System;
using RestSharp;

namespace Bai.NavigationSystem.BureauApi
{
    public class MissionControlProxyBase
    {
        private const string BaseUrl = "http://goserver.cloudapp.net:3000";
        private readonly string _email;
        private readonly IRestClient _restClient;

        public MissionControlProxyBase(IRestClient aRestClient, string anEmail)
        {
            _restClient = aRestClient;
            _email = anEmail;
        }

        internal T Execute<T>(IRestRequest aRestRequest) where T : new()
        {
            _restClient.BaseUrl = BaseUrl;
            aRestRequest.AddParameter("email", _email, ParameterType.UrlSegment);
            IRestResponse<T> response = _restClient.Execute<T>(aRestRequest);

            if (response.ErrorException != null)
            {
                const string message =
                    "Error retrieving response from mission control. Check inner details for more info.";
                var missionControlException = new ApplicationException(message, response.ErrorException);
                throw missionControlException;
            }

            return response.Data;
        }
    }
}