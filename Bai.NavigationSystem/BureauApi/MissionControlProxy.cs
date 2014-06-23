using System;
using System.Collections.Generic;
using System.Globalization;
using Autofac;
using Bai.NavigationSystem.SearchArea;
using RestSharp;

namespace Bai.NavigationSystem.BureauApi
{
    public class MissionControlProxy : MissionControlProxyBase, IMissionControlProxy
    {
        private readonly ILifetimeScope _lifetimeScope;
        private readonly IRestRequest _restRequest;

        public MissionControlProxy(ILifetimeScope lifetimeScope, IRestClient aRestClient, string anEmail,
                                   IRestRequest aRestRequest) : base(aRestClient, anEmail)
        {
            _lifetimeScope = lifetimeScope;
            _restRequest = aRestRequest;
        }

        public List<string> GetDirections()
        {
            using (_lifetimeScope.BeginLifetimeScope())
            {
                _restRequest.Resource = "api/spaceprobe/getdata/{email}";

                var response = Execute<GetDirectionsResponse>(_restRequest);

                if (response == null || response.Directions == null || response.Directions.Count <= 0)
                {
                    throw new ApplicationException("No directions were recieved from mission control.");
                }

                return response.Directions;
            }
        }

        public string LaunchProbe(Point aPoint)
        {
            using (_lifetimeScope.BeginLifetimeScope())
            {
                _restRequest.Resource = "api/spaceprobe/submitdata/{email}/{x}/{y}";
                _restRequest.AddParameter("x", aPoint.X, ParameterType.UrlSegment);
                _restRequest.AddParameter("y", aPoint.Y, ParameterType.UrlSegment);

                var response = Execute<LaunchProbeResponse>(_restRequest);
                return string.Format("Status code: {0}, Message: {1}",
                                     response.StatusCode.ToString(CultureInfo.InvariantCulture), response.Message);
            }
        }
    }
}