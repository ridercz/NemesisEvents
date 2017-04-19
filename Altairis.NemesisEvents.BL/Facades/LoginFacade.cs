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
using Riganti.Utils.Infrastructure.Services;

namespace Altairis.NemesisEvents.BL.Facades
{
    public class LoginFacade : AppFacadeBase
    {

        public Func<AppUserManager> AppUserManagerFactory { get; set; }

        public AppMailerService MailerService { get; set; }

        public ICurrentUserProvider<int> CurrentUser { get; set; }


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

                    var roles = await manager.GetRolesAsync(user);
                    return CreateIdentity(user, roles);
                }
            }
        }

        private ClaimsIdentity CreateIdentity(User user, IEnumerable<string> roles)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identity = new ClaimsIdentity(claims, AppUserManager.AuthenticationScheme);
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
                    await MailerService.SendPasswordResetEmailAsync(data, token);
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
                        throw new UIException($"Změna hesla se nezdařila. Ujistěte se, že heslo má alespoň 12 znaků.");
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
                            throw new UIException($"Registrace se nezdařila. Ujistěte se, že heslo obsahuje alespoň 12 znaků.");
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
                    await uow.CommitAsync();
                    var token = await manager.GenerateEmailConfirmationTokenAsync(user);

                    await MailerService.SendEmailAddressConfirmationEmailAsync(data, token);
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

                        var roles = await manager.GetRolesAsync(user);
                        return CreateIdentity(user, roles);
                    }

                    return null;
                }
            }
        }

        public async Task RequestEmailChange(string newEmail)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                using (var manager = AppUserManagerFactory())
                {
                    var user = await manager.FindByIdAsync(CurrentUser.Id.ToString());

                    var token = await manager.GenerateChangeEmailTokenAsync(user, newEmail);
                    await MailerService.SendEmailChangeConfirmationEmailAsync(newEmail, token);
                }
            }
        }

        public async Task ChangeEmail(string newEmail, string token)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                using (var manager = AppUserManagerFactory())
                {
                    var user = await manager.FindByIdAsync(CurrentUser.Id.ToString());

                    var result = await manager.ChangeEmailAsync(user, newEmail, token);
                    if (!result.Succeeded)
                    {
                        throw new UIException("Odkaz pro ověření e-mailové adresy není platný nebo již vypršel.");
                    }

                    user.UserName = newEmail;
                    user.NormalizedUserName = newEmail.ToUpper();

                    await uow.CommitAsync();
                }
            }
        }

        public async Task ChangePassword(ChangePasswordDTO data)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                using (var manager = AppUserManagerFactory())
                {
                    var user = await manager.FindByIdAsync(CurrentUser.Id.ToString());

                    var result = await manager.ChangePasswordAsync(user, data.OldPassword, data.NewPassword);
                    if (!result.Succeeded)
                    {
                        throw new UIException("Změna hesla se nezdařila.");
                    }
                    await uow.CommitAsync();
                }
            }
        }
    }
}
