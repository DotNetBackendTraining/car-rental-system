using CarRentalSystem.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Core;

public class CustomUserValidator<TUser> : UserValidator<TUser> where TUser : ApplicationUser
{
    public CustomUserValidator(IdentityErrorDescriber? describer = null) : base(describer)
    {
    }

    public override async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
    {
        var errors = new List<IdentityError>();
        await ValidatePhoneNumber(manager, user, errors);
        return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
    }

    private async Task ValidatePhoneNumber(UserManager<TUser> manager, TUser user, ICollection<IdentityError> errors)
    {
        var phoneNumber = user.PhoneNumber;
        if (!string.IsNullOrWhiteSpace(phoneNumber))
        {
            var existingUser = await manager.Users.FirstOrDefaultAsync(u =>
                u.PhoneNumber == phoneNumber && u.Id != user.Id);

            if (existingUser != null)
            {
                errors.Add(new IdentityError
                {
                    Code = "DuplicatePhoneNumber",
                    Description = "Phone number is already used."
                });
            }
        }
    }
}