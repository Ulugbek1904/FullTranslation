using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XProject.Models;

namespace XProject.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<List<Localization>> SelectLocalizationsByProjectIdAsync(Guid projectId);
        Task<List<Localization>> SelectLocalizationsByLanguageAsync(string language);
        ValueTask<Localization> InsertLocalizationAsync (Localization localization);
        ValueTask<Localization> SelectLocalizationByKeyAsync(string key);
        ValueTask<Localization> UpdateLocalizationAsync(Localization localization);
        ValueTask<Localization> DeleteLocalizationAsync(Localization localization);

        ValueTask<LocalizationValue> InsertLocalizationValueAsync(LocalizationValue localizationValue);
        ValueTask<Localization> SelectLocalizationByIdAsync(int id);
        ValueTask<LocalizationValue> UpdateLocalizationValueAsync(LocalizationValue localizationValue);
        ValueTask<LocalizationValue> DeleteLocalizationValueAsync(LocalizationValue localizationValue);
    }
}
