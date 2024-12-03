using System;
using System.Collections.Generic;
using System.Text;

namespace Projet.users
{
    public class User
    {
        public long Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public User(long id, string nom, string prenom, string username, string password)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Username = username;
            Password = password;
           
        }

















        public static implicit operator long(User v)
        {
            throw new NotImplementedException();
        }
    }
}
