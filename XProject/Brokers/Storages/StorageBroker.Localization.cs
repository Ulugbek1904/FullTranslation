using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XProject.Models;

namespace XProject.Brokers.Storages
{
    public partial class StorageBroker
    {
        DbSet<Localization> Localizations { get; set; }
        DbSet<LocalizationValue> LocalizationsValue { get; set; }

        public async Task<List<Localization>> SelectLocalizationsByLanguageAsync(string language)
        {
            return await this.Set<Localization>()
                .Where(l => l.Values.Any(v => v.Language == language))
                .ToListAsync();
        }

        public ValueTask<Localization> InsertLocalizationAsync(Localization localization) =>
            InsertAsync(localization);

        public ValueTask<LocalizationValue> InsertLocalizationValueAsync(LocalizationValue localizationValue) =>
        InsertAsync(localizationValue);

        public async ValueTask<Localization> SelectLocalizationByKeyAsync(string key)
        {
            return await this.Set<Localization>()
                .FirstOrDefaultAsync(localization => localization.Key == key);
        }

        public async ValueTask<Localization> SelectLocalizationByIdAsync(int id)
        {
            return await this.Set<Localization>()
                .FirstOrDefaultAsync(lv => lv.Id == id);
        }

        public ValueTask<Localization> UpdateLocalizationAsync(Localization localization) =>
            UpdateAsync(localization);

        public ValueTask<LocalizationValue> UpdateLocalizationValueAsync(LocalizationValue localizationValue) =>
        UpdateAsync(localizationValue);

        public ValueTask<Localization> DeleteLocalizationAsync(Localization localization) =>
            DeleteAsync(localization);

        public ValueTask<LocalizationValue> DeleteLocalizationValueAsync(LocalizationValue localizationValue) =>
        DeleteAsync(localizationValue);

        public async ValueTask<List<Localization>> SelectLocalizationsByProjectIdAsync(Guid projectId)
        {
            var localizations = await this.Set<Localization>()
                .Where(l => l.ProjectId == projectId).ToListAsync();

            return localizations;
        }
    }
}
