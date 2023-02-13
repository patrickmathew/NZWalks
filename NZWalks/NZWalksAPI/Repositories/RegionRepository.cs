using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await nZWalksDbCotext.AddAsync(region);
          await  nZWalksDbCotext.SaveChangesAsync();
            return region;
         
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
           var region= await nZWalksDbCotext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region==null)
            {
                return null;
            }

             nZWalksDbCotext.Regions.Remove(region);
            await nZWalksDbCotext.SaveChangesAsync(true);
            return region;

        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
          return await  nZWalksDbCotext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await nZWalksDbCotext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
           var Region= await nZWalksDbCotext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(Region==null)
            {
                return null;
            }
            Region.Code = region.Code;
            Region.Name = region.Name;
            Region.Area = region.Area;
            Region.Lat = region.Lat;
            Region.Long = region.Long;
            Region.Population = region.Population;
          await  nZWalksDbCotext.SaveChangesAsync();
            return Region;
        }
    }
}
