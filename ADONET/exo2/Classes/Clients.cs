using System;
using System.Collections.Generic;
using System.Text;

namespace exo2.Classes
{
    internal class Clients
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Prenom {  get; set; }
        public string Adresse { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Telephone { get; set; }

        public Clients() { }

        public Clients(string nom, string prenom, string adresse, string codePostal, string ville, string telephone)
        {
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            CodePostal = codePostal;
            Ville = ville;
            Telephone = telephone;
        }

        public Clients(int clientID, string nom, string prenom, string adresse, string codePostal, string ville, string telephone) : this(nom, prenom, adresse, codePostal, ville,telephone)
        {
            ID = clientID;
        }

        public override string ToString()
        {
            return $"Client n°{ID} : {Nom} {Prenom} | Addresse: {Adresse} {CodePostal} {Ville} | Numéro de téléphone: {Telephone}";
        }
    }
}
