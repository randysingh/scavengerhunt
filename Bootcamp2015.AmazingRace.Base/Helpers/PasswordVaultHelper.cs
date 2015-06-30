using System.Linq;
using Windows.Security.Credentials;
using Microsoft.WindowsAzure.MobileServices;

namespace Bootcamp2015.AmazingRace.Base.Helpers
{
    public static class PasswordVaultHelper
    {
        public static PasswordCredential RetriveGooglePasswordCredential()
        {
            var vault = new PasswordVault();
            PasswordCredential passCred = null;

            try
            {
                passCred = vault.FindAllByResource(MobileServiceAuthenticationProvider.Google.ToString()).FirstOrDefault();
            }
            catch
            {
                //TODO log
            }

            if(passCred != null)
                passCred.RetrievePassword();

            return passCred;
        }

        public static void PutGooglePasswordToPasswordVault(MobileServiceUser user)
        {
            var passwordCredetials = new PasswordCredential(MobileServiceAuthenticationProvider.Google.ToString(), user.UserId, user.MobileServiceAuthenticationToken);
            var vault = new PasswordVault();
            vault.Add(passwordCredetials);
        }

        public static MobileServiceUser RetriveGoogleUser()
        {
            MobileServiceUser user = null;
            var password = RetriveGooglePasswordCredential();
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