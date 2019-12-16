// -------------------------------------------------------------------------------------------------------------------
// <copyright company="Gemotial"  project="Gmtl.Feedback.Server" date="2019-05-15 07:18">
// 
// </copyright>
// -------------------------------------------------------------------------------------------------------------------

using System.Security.Claims;
using System.Security.Principal;

namespace Gmtl.Feedback.Server.Extensions
{
    public struct IdentityCustomKeys
    {
        public const string Username = "Usernane";
        public const string Name = "Name";
        public const string Id = "Id";
    }

    public static class IdentityExtensions
    {
        public static string GetFullName(this IIdentity identity)
        {
            if (identity == null)
            {
                return null;
            }

            return (identity as ClaimsIdentity)?.FirstOrNull(IdentityCustomKeys.Name);
        }

        internal static string FirstOrNull(this ClaimsIdentity identity, string claimType)
        {
            var val = identity.FindFirst(claimType);

            return val?.Value;
        }
    }
}