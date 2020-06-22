using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pandell.Practicum.App.Extensions;
using Pandell.Practicum.App.Models;
using Pandell.Practicum.App.Services;
using Pandell.Practicum.App.Utility;

namespace Pandell.Practicum.App.Controllers
{
    public class RandomSequenceController : Controller
    {
        private readonly IRandomSequenceService randomSequenceService;

        public RandomSequenceController()
        {
            randomSequenceService = Injector.Resolve<IRandomSequenceService>();
        }

        public async Task<IActionResult> Index()
        {
            return View(await randomSequenceService.GetAllAsync().ConfigureAwait(false));
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,RandomSequenceHidden")] RandomSequenceModel modelToAdd)
        {
            if (!ModelState.IsValid) return View(new RandomSequenceModel());
            await randomSequenceService.AddAsync(modelToAdd).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Create()
        {
            var randomSequence = await randomSequenceService.GenerateRandomSequence().ConfigureAwait(false);
            return View(randomSequence);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,RandomSequenceHidden")] RandomSequenceModel modelToUpdate)
        {
            if (id != modelToUpdate.Id) return NotFound();
            modelToUpdate.RandomSequence = modelToUpdate.RandomSequenceHidden.FromJsonObject();
            
            if (!ModelState.IsValid) return View(modelToUpdate);
            
            try
            {
                await randomSequenceService.UpdateAsync(modelToUpdate).ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                var doesModelExist = await randomSequenceService.DoesExistAsync(modelToUpdate.Id).ConfigureAwait(false);
                if (!doesModelExist) return NotFound();
                throw;
            }
                
            return RedirectToAction(nameof(Index));

        }
        
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id.IsEmptyGuid()) return NotFound();

            var foundSavedRandomSequence = await randomSequenceService.GetByPrimaryKeyAsync(id).ConfigureAwait(false);
            
            return foundSavedRandomSequence == null 
                ? (IActionResult) NotFound() 
                : View(foundSavedRandomSequence);
        }
        
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var foundModel = await randomSequenceService.GetByPrimaryKeyAsync(id).ConfigureAwait(false);
            await randomSequenceService.RemoveAsync(foundModel).ConfigureAwait(false);
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id.IsEmptyGuid()) return NotFound();

            var foundModel = await randomSequenceService.GetByPrimaryKeyAsync(id).ConfigureAwait(false);
            
            return foundModel == null
                ? (IActionResult) NotFound()
                : View(foundModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}