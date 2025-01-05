using idefny.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using idefny.Models;


namespace idefny.Controllers
{
  // Restrict access to admin users only
    public class AdminController : Controller
    {
        private readonly UserManager<Users> _userManager;

        public AdminController(UserManager<Users> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Dashboard()
          {
        // Admin dashboard logic
        return View();
}

        // GET: /Admin/Users
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList(); // Get all users
            return View(users);
        }

        // GET: /Admin/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound("User ID is missing.");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "User deleted successfully.";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "Failed to delete user.";
            return RedirectToAction(nameof(Index));
        }


        // GET: /Admin/ChangePassword/5
        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return View(new ChangePasswordViewModel2 { UserId = user.Id });
        }

        // POST: /Admin/ChangePassword/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel2 model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    // If current password is not required for admin reset
                    var removePasswordResult = await _userManager.RemovePasswordAsync(user);
                    if (removePasswordResult.Succeeded)
                    {
                        var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
                        if (addPasswordResult.Succeeded)
                        {
                            TempData["SuccessMessage"] = "Password changed successfully.";
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Failed to add the new password.";
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Failed to remove the current password.";
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "User not found.";
                }
            }

            return View(model);
        }
    }
}








