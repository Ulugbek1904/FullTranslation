using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using System;
using System.Linq;
using System.Threading.Tasks;
using XProject.Brokers.Storages;
using XProject.Models;

namespace XProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocalizationController : RESTFulController
    {
        private readonly IStorageBroker _storageBroker;

        public LocalizationController(IStorageBroker storageBroker)
        {
            _storageBroker = storageBroker;
        }

        [HttpGet("projects/{projectId}/get-localizationsby-projectid")]
        public async Task<IActionResult> GetLocalizationsByProjectAsync(Guid projectId)
        {
            var localizations = await _storageBroker.SelectLocalizationsByProjectIdAsync(projectId);

            if (localizations == null || !localizations.Any())
                return NotFound("No localizations found for this project.");

            var result = localizations.Select(l => new
            {
                l.Key,
                Values = l.Values.Select(v => new
                {
                    v.Language,
                    v.Value
                })
            });

            return Ok(result);
        }

        [HttpGet("projects/{projectId}/get-localizations/by-language")]
        public async Task<IActionResult> GetLocalizationByLanguageAsync(Guid projectId, string language)
        {
            var localizations = await _storageBroker.SelectLocalizationsByProjectIdAsync(projectId);

            if (localizations == null || !localizations.Any())
                return NotFound("No localizations found for this project.");

            var result = localizations
                .Select(l => new
                {
                    l.Key,
                    Value = l.Values.FirstOrDefault(v => v.Language == language)?.Value
                })
                .Where(l => !string.IsNullOrEmpty(l.Value));

            return Ok(result);
        }

        [HttpPost("projects/{projectId}/localizations")]
        public async Task<IActionResult> AddLocalizationAsync(Guid projectId, [FromBody] Localization localization)
        {
            if (localization == null)
                return BadRequest("Localization data is required");

            localization.ProjectId = projectId;
            await _storageBroker.InsertLocalizationAsync(localization);

            return Created("", localization);
        }

        [HttpPut("projects/{projectId}/localizations")]
        public async Task<IActionResult> UpdateLocalizationAsync(int projectId, [FromBody] Localization localization)
        {
            var existingLocalization = await _storageBroker.SelectLocalizationByKeyAsync(localization.Key);

            if (existingLocalization == null)
                return NotFound("Localization not found.");

            existingLocalization.Values = localization.Values;

            await _storageBroker.UpdateLocalizationAsync(existingLocalization);

            return Ok(existingLocalization);
        }

        [HttpDelete("projects/{projectId}/localizations/{id}")]
        public async Task<IActionResult> DeleteLocalizationAsync(int id)
        {
            var existingLocalization = await _storageBroker.SelectLocalizationByIdAsync(id);

            if (existingLocalization == null)
                return NotFound("Localization not found.");

            await _storageBroker.DeleteLocalizationAsync(existingLocalization);

            return Ok(existingLocalization);
        }

    }
}
