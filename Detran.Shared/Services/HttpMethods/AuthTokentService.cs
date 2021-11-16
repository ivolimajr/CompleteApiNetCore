using System;
using System.Text;

namespace Detran.Shared.Services.HttpMethods
{
    public abstract class AuthTokentService
    {
        private int _expiresIn;
        private string _password;
        private string _username;

        public string Username
        {
            set
            {
                _username = value;
            }
        }
        public string Password
        {
            set
            {
                _password = value;
            }
        }

        public string BasicCredentials
        {
            get
            {
                var byteArray = new UTF8Encoding().GetBytes(string.Format("{0}:{1}", _username, _password));
                return string.Format("Basic {0}", Convert.ToBase64String(byteArray));
            }
        }

        public string Token { get; set; }
        public string BaseAddress { get; set; }
        public int ExpiresIn
        {
            get
            {
                return _expiresIn;
            }
            set
            {
                _expiresIn = value;
                ExpirationDate = DateTime.Now.AddSeconds(_expiresIn);
            }
        }
        public DateTime ExpirationDate { get; private set; }

        public bool Expired()
        {
            return ExpirationDate <= DateTime.Now;
        }

    }
}
