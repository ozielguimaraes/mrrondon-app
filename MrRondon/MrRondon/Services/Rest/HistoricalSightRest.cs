﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MrRondon.Entities;

namespace MrRondon.Services.Rest
{
    public class HistoricalSightRest : BaseRest
    {
        public async Task<IList<HistoricalSight>> GetAsync(string search)
        {
            var url = $"{UrlService}/historicalsight/{search}";
            var content = await GetObjectAsync<IList<HistoricalSight>>(url);

            return content;
        }
    }
}