using System;
using System.Collections.Generic;
using System.Text;

namespace ADONET.Classes
{
    internal class IHM
    {
        private Repo _repo;

        public IHM()
        {
            _repo = new Repo();
        }

        public void Menu()
        {
            while (true)
            {
                Console.WriteLine("MENU");
                Console.WriteLine("1- Ajouter un livre");
                Console.WriteLine("2- Afficher tous les livres");
                Console.WriteLine("3- Recherché un livre par ID");
                Console.WriteLine("4- Modifier un livre");
                Console.WriteLine("5- Supprimer un livre");
                Console.WriteLine("0- Quitter");
                Console.WriteLine("Votre choix :");

                string choix = Console.ReadLine() ?? "";

                switch (choix)
                {
                    case "1":
                        Console.WriteLine("###Ajout d'un livre###");
                        Console.Write("Titre:");
                        string titre = Console.ReadLine();
                        Console.Write("Auteur:");
                        string auteur = Console.ReadLine();
                        Console.Write("Année de publication:");
                        int anneePublication = int.Parse(Console.ReadLine());
                        Console.Write("ISBN:");
                        string isbn = Console.ReadLine();

                        Livre livre = new Livre(titre, auteur, anneePublication, isbn);

                        Repo.AjouterLivre(livre);
                        break;

                    case "2":
                        Console.WriteLine("###Liste des livres###");
                        Repo.AfficherTousLesLivres();
                        break;

                    case "3":
                        Console.WriteLine("### Recherche par ID ###");
                        Console.Write("Id du livre recherché:");
                        int id = int.Parse(Console.ReadLine());

                        Repo.RechercherLivreParID(id);
                        break;

                    case "4":
                        Console.WriteLine("### Modifier par ID ###");
                        Console.Write("Id du livre à modifier:");
                        id = int.Parse(Console.ReadLine());

                        Console.WriteLine("Nouveau Titre :");
                        var newTitre = Console.ReadLine();
                        Console.WriteLine("Nouvel Auteur :");
                        var newAuteur = Console.ReadLine();
                        Console.WriteLine("nouvel année de publication :");
                        var newAnnee= int.Parse(Console.ReadLine());
                        Console.WriteLine("Nouvel isbn :");
                        var newIsbn = Console.ReadLine();

                        Repo.ModifierLivre(id, newTitre, newAuteur, newAnnee, newIsbn);
                        break;

                    case "5":
                        Console.WriteLine("### Supprimer un Livre ###");
                        Console.WriteLine("Id du livre a supprimer :");
                        id = int.Parse(Console.ReadLine());

                        Repo.SupprimerLivre(id);
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("choix pas compris");
                        break;
                }
            }

        }
    }
}
