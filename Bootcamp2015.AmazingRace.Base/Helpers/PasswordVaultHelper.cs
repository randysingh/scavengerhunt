using System.Linq;
using Windows.Security.Credentials;
using Microsoft.WindowsAzure.MobileServices;

namespace Bootcamp2015.AmazingRace.Base.Helpers
{
    public static class PasswordVaultHelper
    {
        public static MobileServiceAuthenticationProvider provider { get; set; }

        public static PasswordCredential GetPasswordCredential()
        {
            PasswordVault vault = new PasswordVault();
            PasswordCredential credentials = null;

            try
            {
                credentials = vault.FindAllByResource(provider.ToString()).FirstOrDefault();
            }
            catch
            {
            }

            if (credentials != null)
            {
                credentials.RetrievePassword();
            }

            return credentials;
        }

        public static void PutInPasswordVault(MobileServiceUser user)
        {
            PasswordVault vault = new PasswordVault();
            PasswordCredential credentials;

            credentials = new PasswordCredential(provider.ToString(), 
                user.UserId, user.MobileServiceAuthenticationToken);

            vault.Add(credentials);
        }

        public static MobileServiceUser GetUser()
        {
            MobileServiceUser user = null;

            var password = GetPasswordCredential();

            if (password == null)
                return user;

            user = new MobileServiceUser(password.UserName)
            {
                MobileServiceAuthenticationToken = password.Password
            };

            return user;
        }
    }
}