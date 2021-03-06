
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace projectRootNamespace.Api.Infrastructure.Security
{
    /// <summary>
    /// Common interface that authentication providers must
    /// implement (custom, lis, etc...)
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Perform user sign-in and return access token information
        /// if successful.
        /// </summary>
        /// <param name="dto">The DTO containing the sign-in credentials.</param>
        /// <returns>The access token information if successful.</returns>
        Task<AccessTokenDTO> SignIn(SignInCredentialsDTO dto);

        /// <summary>
        /// Perform user sign-out.
        /// </summary>
        Task SignOut();

        /// <summary>
        /// Return a new access token using the refresh token.
        /// </summary>
        /// <returns>The refreshed access token information if successful.</returns>
        Task<AccessTokenDTO> RefeshAccessToken();

        /// <summary>
        /// Return metadata information for the authenticated user.
        /// </summary>
        /// <returns>The metadata for the authenticated user</returns>
        Task<UserInfoDTO> GetUserInfo();
    }

    #region DTOs

    public class SignInCredentialsDTO
    {
        /// <summary>
        /// The user identifier used to sign-in.
        /// </summary>
        [Required]
        [StringLength(64)]
        public string Username { get; set; }

        /// <summary>
        /// The user password used to sign-in.
        /// </summary>
        [Required]
        [StringLength(64)]
        public string Password { get; set; }

    }

    public class AccessTokenDTO
    {
        /// <summary>
        /// The access token that was generated by the auth server (lis in our case).
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// The access token expiration date.
        /// </summary>
        public DateTime AccessTokenExpiration { get; set; }

        /// <summary>
        /// The access token cookie that was generated by the auth server (lis in our case).
        /// </summary>
        public string AccessTokenSourceCookie { get; set; }

        /// <summary>
        /// The refresh token that was generated by the auth server (lis in our case).
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// The refresh token expiration date.
        /// </summary>
        public DateTime RefreshTokenExpiration { get; set; }

        /// <summary>
        /// The refresh token cookie that was generated by the auth server (lis in our case).
        /// </summary>
        public string RefreshTokenSourceCookie { get; set; }
    }

    public class UserInfoDTO
    {
        /// <summary>
        /// The user unique identifier (usualy the username or email)
        /// </summary>
        public string UniqueIdentifier { get; set; }

        /// <summary>
        /// The display name for the user (uaualy the username, email of fullname)
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The name of the user (uaualy the username, email of fullname)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// (Optional) The user email address
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// (Optional) The user photo or avatar
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// (Optional) Aditional information regarding the authenticated user
        /// </summary>
        public IDictionary<string, string> Metadata { get; set; }

        /// <summary>
        /// (Optional) The list of roles the user belongs to
        /// </summary>
        public IEnumerable<string> Roles { get; set; }
    }

    #endregion
}
