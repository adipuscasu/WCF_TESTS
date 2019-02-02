using System.Collections.Generic;
using System.Linq;
using GeoLib.Contracts;
using GeoLib.Data;

namespace GeoLib.Services
{
    public class GeoManager: IGeoService
    {
        public GeoManager()
        {
            
        }

        private IZipCodeRepository _ZipCodeRepository = null;
        private IStateRepository _StateRepository = null;

        
        public GeoManager(IZipCodeRepository zipCodeRepository)
        {
            _ZipCodeRepository = zipCodeRepository;
        }


        public GeoManager(IStateRepository stateRepository)
        {
            _StateRepository = stateRepository;
        }

        public GeoManager(IZipCodeRepository zipCodeRepository, IStateRepository stateRepository)
        {
            _ZipCodeRepository = zipCodeRepository;
            _StateRepository = stateRepository;
        }

        public ZipCodeData GetZipInfo(string zip)
        {
            ZipCodeData zipCodeData = null;

            IZipCodeRepository zipCodeRepository = _ZipCodeRepository ?? new ZipCodeRepository();

            ZipCode zipCodeEntity = zipCodeRepository.GetByZip(zip);
            if (zipCodeEntity != null)
            {
                zipCodeData = new ZipCodeData()
                {
                    City = zipCodeEntity.City,
                    State = zipCodeEntity.State.Abbreviation,
                    ZipCode = zipCodeEntity.Zip
                };
            }

            return zipCodeData;
        }

        public IEnumerable<string> GetStates(bool primaryOnly)
        {
            var stateData = new List<string>();

            IStateRepository stateRepository = _StateRepository ?? new StateRepository();

            var states = stateRepository.Get(primaryOnly);

            if (states != null) stateData.AddRange(states.Select(state => state.Abbreviation));

            return stateData;
        }

        public IEnumerable<ZipCodeData> GetZips(string state)
        {
            List<ZipCodeData> zipCodeData = new List<ZipCodeData>();

            IZipCodeRepository zipCodeRepository = _ZipCodeRepository ?? new ZipCodeRepository();
            var zips = zipCodeRepository.GetByState(state);
            if (zips != null)
            {
                zipCodeData.AddRange(zips.Select(zipCode => new ZipCodeData
                    {City = zipCode.City, State = zipCode.State.Abbreviation, ZipCode = zipCode.Zip}));
            }

            return zipCodeData;
        }

        public IEnumerable<ZipCodeData> GetZips(string zip, int range)
        {
            List<ZipCodeData> zipCodeData = new List<ZipCodeData>();

            IZipCodeRepository zipCodeRepository = _ZipCodeRepository ?? new ZipCodeRepository();
            var zipEntity = zipCodeRepository.GetByZip(zip);
            var zips = zipCodeRepository.GetZipsForRange(zipEntity, range);
            if (zips != null)
            {
                zipCodeData.AddRange(zips.Select(zipCode => new ZipCodeData
                    {City = zipCode.City, State = zipCode.State.Abbreviation, ZipCode = zipCode.Zip}));
            }

            return zipCodeData;
        }
    }
}
