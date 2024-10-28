using HotelManagementProject.Models;
using HotelManagementProject.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MongoDB.Bson;

namespace HotelManagementProject.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleServices _roleServices;

        public RoleController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }
        public async Task<IActionResult> Index()
        {
            var role = await _roleServices.GetRoleAsync();
            return View(role);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Roles roles)
        {
            if(ModelState.IsValid)
            {
                await _roleServices.CreateRoleAsync(roles);
                return RedirectToAction("Index");
            }
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(ObjectId id)
        {

            // Fetch the role details based on the provided ID
            var role = await _roleServices.GetRoleByIdAsync(id);

            // Check if the role was found
            if (role == null)
            {
                return NotFound();
            }

            // Return the view with the role details
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ObjectId id, [Bind("Id,userName,Password,RoleType")] Roles roleDetails)
        {
            // Check if the provided ID matches the ID of the role being edited
            if (id != roleDetails.Id)
            {
                return BadRequest();
            }

            // Validate the model state
            if (ModelState.IsValid)
            {
                try
                {
                    // Update the role details
                    await _roleServices.UpdateRoleAsync(id, roleDetails);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Handle any exceptions, e.g., logging or displaying an error message
                    ModelState.AddModelError("", $"An error occurred while updating the role: {ex.Message}");
                }
            }

            // Return the view with the role details if model state is invalid
            return View(roleDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(ObjectId Id)
        {
            var guest = await _roleServices.GetRoleByIdAsync(Id);
            if (guest == null)
            {
                return NotFound();
            }
            return View(guest);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(ObjectId id)
        {
            await _roleServices.DeleteRoleAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
