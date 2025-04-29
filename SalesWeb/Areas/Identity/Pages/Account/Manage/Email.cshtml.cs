// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace SalesWeb.Areas.Identity.Pages.Account.Manage
{
    public class EmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public EmailModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required (ErrorMessage = "{0} obrigat�rio")]
            [EmailAddress (ErrorMessage = "Digite um email v�lido")]
            [Display(Name = "Novo email")]
            public string NewEmail { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var email = await _userManager.GetEmailAsync(user);
            Email = email;

            Input = new InputModel
            {
                NewEmail = email,
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.NewEmail != email)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateChangeEmailTokenAsync(user, Input.NewEmail);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmailChange",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, email = Input.NewEmail, code = code },
                    protocol: Request.Scheme);

                var emailBody = $@"
                        <html> 
                        <body style='font-family: Arial, sans-serif; background-color: #F4F4F4; padding: 20px;'>
                            <div style='max-width: 600px; margin: auto; background-color: #FFF; padding: 30px; border-radius: 8px;'>
                                <h2 style='color: #F24D0D;'>Troca de Email</h2>
                                <p>Ol�, Recebemos a sua solicita��o!</p>
                                <p>Para confirmar seu novo email, clique no bot�o abaixo:</p>
                                <p style='text-align: center;'>
                                    <a href='{HtmlEncoder.Default.Encode(callbackUrl)}' style='display: inline-block; background-color: #F24D0D; color: white; padding: 12px 24px; text-decoration: none; border-radius: 5px;'>Confirmar Email</a>
                                </p>
                                <p>Se voc� n�o solicitou a mudan�a, ignore este e-mail.</p>
                                <p style='font-weight: bold'>Equipe SalesWeb</p>
                            </div>
                        </body>
                        </html>";

                await _emailSender.SendEmailAsync(
                    Input.NewEmail,
                    "Confirme seu novo email",
                    $"Por favor, confirme sua conta <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicando aqui</a>.");

                StatusMessage = "Link de confirma��o enviado. Por favor, verifique sua caixa de entrada";
                return RedirectToPage();
            }

            StatusMessage = "Seu email n�o foi alterado.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, code = code },
                protocol: Request.Scheme);

            var emailBody = $@"
                        <html> 
                        <body style='font-family: Arial, sans-serif; background-color: #F4F4F4; padding: 20px;'>
                            <div style='max-width: 600px; margin: auto; background-color: #FFF; padding: 30px; border-radius: 8px;'>
                                <h2 style='color: #F24D0D;'>Confirma��o de Email</h2>
                                <p>Ol�, Recebemos o seu cadastro!</p>
                                <p>Para confirmar seu email, clique no bot�o abaixo:</p>
                                <p style='text-align: center;'>
                                    <a href='{HtmlEncoder.Default.Encode(callbackUrl)}' style='display: inline-block; background-color: #F24D0D; color: white; padding: 12px 24px; text-decoration: none; border-radius: 5px;'>Confirmar Email</a>
                                </p>
                                <p>Se voc� n�o criou essa conta, ignore este e-mail.</p>
                                <p style='font-weight: bold'>Equipe SalesWeb</p>
                            </div>
                        </body>
                        </html>";

            await _emailSender.SendEmailAsync(
                email,
                "Confirme seu email",
                emailBody);

            StatusMessage = "Email de verifica��o enviado. Por favor, verifique sua caixa de entrada.";
            return RedirectToPage();
        }
    }
}
