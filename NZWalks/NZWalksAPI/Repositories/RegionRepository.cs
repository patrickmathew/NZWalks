using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
          return await  nZWalksDbCotext.Regions.ToListAsync();
        }
    }
}
