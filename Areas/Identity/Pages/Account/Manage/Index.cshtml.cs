using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Michelin.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.IO;
using Michelin.Infrastructure;

namespace Michelin.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Korisnik> _userManager;
        private readonly SignInManager<Korisnik> _signInManager;

        public IndexModel(
            UserManager<Korisnik> userManager,
            SignInManager<Korisnik> signInManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public string SlikaProfila { get; set; }
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Slika profila")]
            public string SlikaProfila { get; set; }

            [Display(Name ="Recite nešto o sebi...")]
            public string biografija { get; set; }
            [Phone]
            [Display(Name = "Broj telefona")]
            public string BrojTelefona { get; set; }
        }

        private async Task LoadAsync(Korisnik user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var brojTelefona = user.brojMobitela;
            var biografija = user.biografija;
            var profilnaSlika = user.profilnaSlika;

            Username = userName;

            Input = new InputModel
            {
                BrojTelefona = brojTelefona,
                SlikaProfila = profilnaSlika,
                biografija = biografija


            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            SlikaProfila = user.profilnaSlika;
            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            
            var user = await _userManager.GetUserAsync(User);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
           
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            if (Input.BrojTelefona != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.BrojTelefona);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
                
            }

            var brojTelefona = user.brojMobitela;
            var profilnaSlika = user.profilnaSlika;
            var biografija = user.biografija;

            if (Input.BrojTelefona != brojTelefona)
            {
                user.brojMobitela = Input.BrojTelefona;
                await _userManager.UpdateAsync(user);
            }

            if (file != null)
            {
                _unitOfWork.UploadImage(file);
                user.profilnaSlika = file.FileName;
                await _userManager.UpdateAsync(user);
            }

            if (Input.biografija!= biografija)
            {
                user.biografija = Input.biografija;
                await _userManager.UpdateAsync(user);
            }


            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
