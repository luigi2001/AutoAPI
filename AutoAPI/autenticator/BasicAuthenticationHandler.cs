﻿using AutoAPI.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

namespace AutoAPI.autenticator
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
            ) : base(options, logger, encoder, clock) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            Response.Headers.Add("WWW-Authenticate", "Basic");

            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.Fail("Autorizzazione mancante"));
            }

            var authorizationHeader = Request.Headers["Authorization"].ToString();

            var authoHeaderRegEx = new Regex("Basic (.*)");

            if (!authoHeaderRegEx.IsMatch(authorizationHeader))
            {
                return Task.FromResult(AuthenticateResult.Fail("Authorization Code, not properly formatted"));
            }

            var authBase64 = Encoding.UTF8.GetString(Convert.FromBase64String(authoHeaderRegEx.Replace(authorizationHeader, "$1")));
            var authSplit = authBase64.Split(Convert.ToChar(":"), 2);

            var authUser = authSplit[0];
            var authPassword = authSplit.Length > 1 ? authSplit[1] : throw new Exception("Unable to get Password");

            string roundtrip = "";
            bool verifica = false;
            var builder = WebApplication.CreateBuilder();
            using (SqlConnection connection = new SqlConnection(builder.Configuration.GetConnectionString("AutoDB").ToString()))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = $"select * from credenziali";
                    command.Connection = connection;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            using (Aes myAes = Aes.Create())
                            {
                                roundtrip = Cryptography.Cryptography.DecryptStringFromBytes_Aes(Convert.FromBase64String(reader["password"].ToString()), Convert.FromBase64String(reader["key"].ToString()), Convert.FromBase64String(reader["IV"].ToString()));
                                if (authPassword == roundtrip && authUser == reader["utente"].ToString())
                                {
                                    verifica = true;
                                }
                            }
                        }
                    }
                    reader.Close();
                }
            }

            if (!verifica)
            {
                return Task.FromResult(AuthenticateResult.Fail("User e/o password errati !!!"));
            }

            var authenticatedUser = new AuthenticatedUser("BasicAuthentication", true, authUser);

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(authenticatedUser));

            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
        }
    }
}
