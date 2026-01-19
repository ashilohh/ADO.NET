using System;
using System.Collections.Generic;
using System.Text;

namespace ADONET.Classes
{
    internal class Livre
    {
        public int ID { get; set; }
        public string Titre { get; set; }
        public string Auteur { get; set; }
        public int AnneePublication { get; set; }
        public string Isbn { get; set; }

        public Livre(string titre, string auteur, int anneePublication, string isbn)
        {
            Titre = titre;
            Auteur = auteur;
            AnneePublication = anneePublication;
            Isbn = isbn;
        }
        public Livre(int id) 
        {
            ID = id;
        }

        public override string ToString()
        {
            return $"Titre: {Titre}| Auteur: {Auteur}| Année de publication: {AnneePublication}| ISBN: {Isbn}";
        }
    }
}

