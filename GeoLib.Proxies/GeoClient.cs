﻿using GeoLib.Contracts;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace GeoLib.Proxies
{
    public class GeoClient : ClientBase<IGeoService>, IGeoService
    {
        public GeoClient(string endPointName):base(endPointName)
        {}

        public GeoClient(Binding binding, EndpointAddress endpointAddress):base(binding, endpointAddress)
        {}

        public IEnumerable<string> GetStates(bool primaryOnly)
        {
            return Channel.GetStates(primaryOnly);
        }

        public ZipCodeData GetZipInfo(string zip)
        {
            return Channel.GetZipInfo(zip);
        }

        public IEnumerable<ZipCodeData> GetZips(string state)
        {
            return Channel.GetZips(state);
        }

        public IEnumerable<ZipCodeData> GetZips(string zip, int range)
        {
            return Channel.GetZips(zip, range);
        }
    }
}
