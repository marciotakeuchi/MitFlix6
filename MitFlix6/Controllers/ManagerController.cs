using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MitFlix6.Models;
using MitFlix6.Models.ManagerViewModel;

namespace MitFlix6.Controllers
{
    public class ManagerController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ManagerController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager; 
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Não foi possível carregar o usuário com o ID{_userManager.GetUserId(User)}");

            }

            var model = new IndexViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = StatusMessage
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Não foi possível carregar o usuário com o ID{_userManager.GetUserId(User)}");

            }

            var email = user.Email;
            if(email != model.Email)
            {
                var changedEmailStatus = await _userManager.SetEmailAsync(user, model.Email);

                if (!changedEmailStatus.Succeeded)
                {
                    throw new ApplicationException($"Erro inesperado ao atribuir o email para o usuario com ID{user.Id}");
                }
            }

            var phoneNumber = user.PhoneNumber;
            if (phoneNumber != model.PhoneNumber)
            {
                var changedPhoneNumberStatus = await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);

                if (!changedPhoneNumberStatus.Succeeded)
                {
                    throw new ApplicationException($"Erro inesperado ao atribuir o telefone para o usuario com ID{user.Id}");
                }
            }
            StatusMessage = "Su perfil foi atualizado!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);

            if(user == null)
            {
                throw new ApplicationException($"Não foi possível recuperar os dados do usuario com ID{_userManager.GetUserId(User)}");
            }

            var model = new ChangePasswordViewModel()
            {
                StatusMessage = StatusMessage
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Não foi possível recuperar os dados do usuairo de ID {_userManager.GetUserId(User)}");
            }

            var changePasswordresult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordresult.Succeeded)
            {
                foreach (var error in changePasswordresult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            StatusMessage = "Senha alterada com sucesso!";

            return RedirectToAction(nameof(ChangePassword));
        }
    }
}
