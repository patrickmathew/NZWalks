using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbCotext nZWalksDbCotext;

        public RegionRepository(NZWalksDbCotext nZWalksDbCotext)
        {
            this.nZWalksDbCotext = nZWalksDbCotext;
        }
        public IEnumerable<Region> GetAll()
        {
          return  nZWalksDbCotext.Regions.ToList();
        }
    }
}
