// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
namespace StudentHub.Web.Areas.Identity.Pages.Account
{
#nullable disable

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using StudentHub.Common;
    using StudentHub.Data.Common.Repositories;
    using StudentHub.Data.Models;
    using StudentHub.Services.Data;

    public class StudentRegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserStore<ApplicationUser> userStore;
        private readonly IUserEmailStore<ApplicationUser> emailStore;
        private readonly ILogger<StudentRegisterModel> logger;
        private readonly IEmailSender emailSender;
        private readonly IRoleService roleService;
        private readonly IStudentService studentService;
        private readonly IWebHostEnvironment environment;
        private readonly IRepository<Image> imagesRepository;

        public StudentRegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<StudentRegisterModel> logger,
            IEmailSender emailSender,
            IRoleService roleService,
            IStudentService studentService,
            IWebHostEnvironment environment,
            IRepository<Image> imagesRepository)
        {
            this.userManager = userManager;
            this.userStore = userStore;
            this.emailStore = this.GetEmailStore();
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.roleService = roleService;
            this.studentService = studentService;
            this.environment = environment;
            this.imagesRepository = imagesRepository;
            this.Input = new InputModel();
        }

        /// <summary>
        ///     Gets or sets this API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     Gets or sets this API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     Gets or sets this API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            public InputModel()
            {
                this.AllRolesListItems = new List<SelectListItem>();
            }

            /// <summary>
            ///     Gets or sets this API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     Gets or sets this API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     Gets or sets this API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }


            [Required]
            [Display(Name = "Firstname")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Lastname")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Age")]
            public int Age { get; set; }

            public IFormFile Image { get; set; }

            public string RoleId { get; set; }

            public IEnumerable<SelectListItem> AllRolesListItems { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (this.Input.AllRolesListItems.IsNullOrEmpty())
            {
                this.Input.AllRolesListItems = this.roleService.GetAllRoles();
            }
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= this.Url.Content("~/");
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (this.ModelState.IsValid)
            {
                var user = this.CreateUser();

                await this.userStore.SetUserNameAsync(user, this.Input.Email, CancellationToken.None);
                await this.emailStore.SetEmailAsync(user, this.Input.Email, CancellationToken.None);
                var newUserResult = await this.userManager.CreateAsync(user, this.Input.Password);
                if (newUserResult.Succeeded)
                {
                    var extension = Path.GetExtension(this.Input.Image.FileName);

                    var allowedExtensions = new[] { "jpg", "jpeg", "png", "gif", "JPG", "JPEG" };

                    if (!allowedExtensions.Any(x => extension.EndsWith(x)))
                    {
                        throw new Exception($"Invalin image extension {extension}");
                    }

                    try
                    {
                        var dbImage = new Image()
                        {
                            Extension = extension,
                        };

                        var physicalPath = $"{this.environment.WebRootPath}/images/recipes/{dbImage.Id}.{extension}";
                        var rootPath = this.environment.ContentRootPath;
                        Directory.CreateDirectory($"{this.environment.WebRootPath}/images/recipes/");

                        using (Stream fileStream = new FileStream(physicalPath, FileMode.Create))
                        {
                            await this.Input.Image.CopyToAsync(fileStream);
                        }

                        await this.studentService.CreateStudentAsync(this.Input.FirstName, this.Input.LastName, this.Input.Age, user.Id, dbImage);
                    }
                    catch (Exception ex)
                    {
                        this.ModelState.AddModelError(string.Empty, ex.Message);
                        this.RedirectToPage("StudentRegister");
                    }

                    await this.userManager.AddToRoleAsync(user, GlobalConstants.StudentRoleName);

                    this.logger.LogInformation("User created a new account with password.");

                    var userId = await this.userManager.GetUserIdAsync(user);
                    var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = this.Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: this.Request.Scheme);

                    await this.emailSender.SendEmailAsync(
                        this.Input.Email,
                        "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (this.userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await this.signInManager.SignInAsync(user, isPersistent: false);
                        return this.LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in newUserResult.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!this.userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }

            return (IUserEmailStore<ApplicationUser>)this.userStore;
        }
    }
}
