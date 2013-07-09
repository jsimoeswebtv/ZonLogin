using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using ZonLogin.Models;

namespace ZonLogin
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {


           
            Dictionary<string, object> FacebooksocialData = new Dictionary<string, object>();
            FacebooksocialData.Add("Icon", "../Images/facebook.png");
            Dictionary<string, object> TwittersocialData = new Dictionary<string, object>();
            TwittersocialData.Add("Icon", "../Images/twitter.png");
            Dictionary<string, object> LivesocialData = new Dictionary<string, object>();
            LivesocialData.Add("Icon", "../Images/live.png");
            Dictionary<string, object> GooglesocialData = new Dictionary<string, object>();
            GooglesocialData.Add("Icon", "../Images/google.png");

            OAuthWebSecurity.RegisterTwitterClient(
                consumerKey: "bHzby3xmQRk4KMaKXGYQ",
                consumerSecret: "6hIgEoQM5eqx8iT23r6ValcaAcmD5W92fmmOtIjMWE",
                displayName: "Twitter",
                extraData: TwittersocialData);

            OAuthWebSecurity.RegisterFacebookClient(
                appId: "369548886479452",
                appSecret: "b7335b34e72228ad5268730c983955d7",
                displayName: "Facebook",
                extraData: FacebooksocialData);

            OAuthWebSecurity.RegisterMicrosoftClient(
                clientId: "00000000480F7D1B",
                clientSecret: "J4hQ2KlpMgmYqvIqhQS1WWI-gvP8L-Wn",
                displayName: "Live",
                extraData: LivesocialData);

            OAuthWebSecurity.RegisterGoogleClient(
                displayName: "Google",
                extraData: GooglesocialData);








            


           


        }
    }
}
