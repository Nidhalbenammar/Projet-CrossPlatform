using System;
using System.Collections.Generic;
using System.Text;

namespace Projet
{
    public static class SessionManager
    {
        private static string _username;
        private static bool _isLoggedIn;

        public static void StartSession(string username)
        {
            _username = username;
            _isLoggedIn = true;
        }

        public static void EndSession()
        {
            _username = null;
            _isLoggedIn = false;
        }

        public static bool IsLoggedIn => _isLoggedIn;

        public static string Username => _username;
    }
}
