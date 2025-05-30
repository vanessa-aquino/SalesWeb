// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace SalesWeb.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResendEmailConfirmationModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ResendEmailConfirmationModel(UserManager<IdentityUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

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
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email de verifica��o enviado. Por favor, cheque sua caixa de email.");
                return Page();
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
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
                Input.Email,
                "Confirme seu email",
                emailBody);
            ModelState.AddModelError(string.Empty, "Email de verifica��o enviado. Por favor, cheque sua caixa de email.");
            return Page();
        }
    }
}
