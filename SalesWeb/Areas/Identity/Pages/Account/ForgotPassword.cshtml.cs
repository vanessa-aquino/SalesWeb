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
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<IdentityUser> userManager, IEmailSender emailSender)
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
            [Required(ErrorMessage = "Deve digitar um {0} v�lido")]
            [EmailAddress(ErrorMessage = "Digite um {0} v�lido")]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                var emailBody = $@"
                        <html> 
                        <body style='font-family: Arial, sans-serif; background-color: #F4F4F4; padding: 20px;'>
                            <div style='max-width: 600px; margin: auto; background-color: #FFF; padding: 30px; border-radius: 8px;'>
                                <h2 style='color: #F24D0D;'>Redefinir Senha</h2>
                                <p>Ol�, Recebemos a sua solicita��o!</p>
                                <p>Para redefinir sua senha, clique no bot�o abaixo:</p>
                                <p style='text-align: center;'>
                                    <a href='{HtmlEncoder.Default.Encode(callbackUrl)}' style='display: inline-block; background-color: #F24D0D; color: white; padding: 12px 24px; text-decoration: none; border-radius: 5px;'>Redefinir senha</a>
                                </p>
                                <p>Se voc� n�o fez essa solicita��o, recomendamos que troque suas senhas e ignore este e-mail.</p>
                                <p style='font-weight: bold'>Equipe SalesWeb</p>
                            </div>
                        </body>
                        </html>";

                await _emailSender.SendEmailAsync(
                    Input.Email,
                    "Redefinir sua senha",
                    emailBody);

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
