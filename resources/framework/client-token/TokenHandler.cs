using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace client_token
{
    public class TokenHandler : BaseScript
    {
        private string _token;

        public delegate string GetTokenDelegate();

        public TokenHandler()
        {
            EventHandlers["onClientResourceStart"] += new Action<string>(OnResourceStart);
            EventHandlers["framework:client:sendToken"] += new Action<string>(OnNewToken);

            GetTokenDelegate getTokenDelegate = GetToken;

            this.Exports.Add("GetToken", getTokenDelegate);
        }

        public string GetToken()
        {
            return _token;
        }

        private void OnResourceStart(string resourceName)
        {
            if (API.GetCurrentResourceName() != resourceName) return;

            TriggerServerEvent("framework:server:requestToken");
        }

        private void OnNewToken(string tempToken)
        {
            _token = tempToken + LocalPlayer.ServerId;
        }
    }
}
