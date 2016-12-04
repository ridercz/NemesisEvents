using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Services;
using Altairis.NemesisEvents.BL.Services.Mailing;
using Altairis.NemesisEvents.DAL;
using Microsoft.AspNetCore.Http.Authentication.Internal;
using Microsoft.AspNetCore.Identity;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.BL.Facades
{
    public class LoginFacade : AppFacadeBase
    {

        public Func<AppUserManager> AppUserManagerFactory { get; set; }

        public AppMailerService MailerService { get; set; }



        public async Task<ClaimsIdentity> Login(LoginDTO data)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                using (var manager = AppUserManagerFactory())
                {
                    var user = await manager.FindByEmailAsync(data.Email);
                    if (user == null)
                    {
                        throw new UIException("Nesprávné uživatelské jméno nebo heslo!");
                    }

                    var result = await manager.CheckPasswordAsync(user, data.Password);
                    if (!result)
                    {
                        throw new UIException("Nesprávné uživatelské jméno nebo heslo!");
                    }

                    return CreateIdentity(user);
                }
            }
        }

        private ClaimsIdentity CreateIdentity(User user)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            }, AppUserManager.AuthenticationScheme);
            return identity;
        }

        public async Task SendResetPasswordEmail(ForgottenPasswordDTO data)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                using (var manager = AppUserManagerFactory())
                {
                    var user = await manager.FindByEmailAsync(data.Email);
                    if (user == null)
                    {
                        throw new UIException("Zadaná e-mailová adresa nebyla nalezena.");
                    }

                    var token = await manager.GeneratePasswordResetTokenAsync(user);
                    MailerService.SendPasswordResetEmail(data, token);
                }
            }
        }

        public async Task ResetPassword(ResetPasswordDTO data, string email, string token)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                using (var manager = AppUserManagerFactory())
                {
                    var user = await manager.FindByEmailAsync(email);
                    if (user == null)
                    {
                        throw new UIException("Zadaná e-mailová adresa nebyla nalezena.");
                    }

                    var result = await manager.ResetPasswordAsync(user, token, data.Password);
                    if (!result.Succeeded)
                    {
                        throw new UIException($"Změna hesla se nezdařila. Ujistěte se, že heslo má alespoň 8 znaků.");
                    }

                    await uow.CommitAsync();
                }
            }
        }

        public async Task RegisterUser(RegisterDTO data)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                using (var manager = AppUserManagerFactory())
                {
                    var user = await manager.FindByEmailAsync(data.Email);
                    if (user == null)
                    {
                        user = new User()
                        {
                            Email = data.Email,
                            DateCreated = DateTime.Now,
                            EmailFrequency = EmailFrequency.Daily,
                            UserName = data.Email,
                            FullName = data.FullName,
                            CompanyName = data.CompanyName,
                            Enabled = true
                        };
                        var createResult = await manager.CreateAsync(user, data.Password);
                        if (!createResult.Succeeded)
                        {
                            throw new UIException($"Registrace se nezdařila. Ujistěte se, že heslo obsahuje alespoň 8 znaků.");
                        }
                    }
                    else if (!user.Enabled)
                    {
                        throw new UIException($"Účet s danou e-mailovou adresou byl již zrušen. Pokud jej chcete obnovit, kontaktujte administrátora.");
                    }
                    else if (user.EmailConfirmed)
                    {
                        throw new UIException($"Účet s danou e-mailovou adresou již existuje.");
                    }
                    else
                    {
                        user.FullName = data.FullName;
                        user.CompanyName = data.CompanyName;
                    }

                    // send activation email
                    var token = await manager.GenerateEmailConfirmationTokenAsync(user);
                    await uow.CommitAsync();

                    MailerService.SendEmailAddressConfirmationEmail(data, token);
                }
            }
        }

        public async Task<ClaimsIdentity> VerifyEmail(string email, string token)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                using (var manager = AppUserManagerFactory())
                {
                    var user = await manager.FindByEmailAsync(email);
                    if (user == null || !user.Enabled)
                    {
                        throw new UIException("Zadaná e-mailová adresa nebyla nalezena.");
                    }

                    if (!user.EmailConfirmed)
                    {
                        var result = await manager.ConfirmEmailAsync(user, token);
                        if (!result.Succeeded)
                        {
                            throw new UIException("Link pro ověření adresy již vypršel.");
                        }

                        await uow.CommitAsync();
                        return CreateIdentity(user);
                    }

                    return null;
                }
            }
        }
    }
}
