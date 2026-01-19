using ADONET.Classes;
using MySql.Data.MySqlClient;
using System.Linq.Expressions;

string connectionString = "Server=localhost;Database=exo1 ;User ID=root;Password=root";

void AjouterLivre()
{
    Console.WriteLine("###Ajout d'un livre###");
    Console.Write("Titre:");
    string titre = Console.ReadLine();
    Console.Write("Auteur:");
    string auteur = Console.ReadLine();
    Console.Write("Année de publication:");
    int anneePublication = int.Parse(Console.ReadLine());
    Console.Write("ISBN:");
    string isbn = Console.ReadLine();

    Livre livre = new Livre(titre,auteur,anneePublication,isbn);

    MySqlConnection connection = new MySqlConnection(connectionString);

    try
    {
        connection.Open();
        string query = "INSERT INTO Livre (titre,auteur,anneePublication,isbn) VALUES (@titre,@auteur,@anneePublication,@isbn)";

        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@titre", livre.Titre);
        cmd.Parameters.AddWithValue("@auteur", livre.Auteur);
        cmd.Parameters.AddWithValue("@anneePublication", livre.AnneePublication);
        cmd.Parameters.AddWithValue("@isbn", livre.Isbn);

        int rowAffected = cmd.ExecuteNonQuery();
        if (rowAffected > 0)
        {
            Console.WriteLine("Livre ajouté avec succès");
        }
    }
    catch (Exception ex) 
    {
        Console.WriteLine("Erreur: " + ex.Message);
    }
    finally 
    {
        connection.Close();
    }
}


void AfficherTousLesLivres()
{
    Console.WriteLine("###Liste des livres###");
    MySqlConnection connection = new MySqlConnection(connectionString);

    try
    {
        connection.Open();

        string query = "SELECT * FROM Livre";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        MySqlDataReader reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Livre livre = new Livre
                    (
                    reader.GetInt32("id"),
                    reader.GetString("titre"),
                    reader.GetString("auteur"),
                    reader.GetInt32("annePublication"),
                    reader.GetString("isbn")
                    );

                Console.WriteLine(livre);
            }
        }
        else
        {
            Console.WriteLine("Aucun livre n'a été trouvé dans la base de données");
        }
        reader.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erreur : " + ex.Message);
    }
    finally 
    { 
        connection.Close() ;
    }
}

void RechercherLivreParID()
{
    Console.WriteLine("### Recherche par ID ###");
    Console.Write("Id du livre recherché:");
    int id = int.Parse(Console.ReadLine());

    MySqlConnection connection = new MySqlConnection(connectionString);
    try
    {
        connection.Open();
        string query = "SELECT * FROM Livre WHERE id = @id";

        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@id", id);

        MySqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            Livre livre = new Livre
                 (
                    reader.GetInt32("id"),
                    reader.GetString("titre"),
                    reader.GetString("auteur"),
                    reader.GetInt32("annePublication"),
                    reader.GetString("isbn")
                    );

            Console.WriteLine("Livre trouvé: " + livre);
        }
        else
        {
            Console.WriteLine("Aucun livre correspondant à cet ID");
        }
        reader.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erreur : " + ex.Message);
    }
    finally
    {
        connection.Close();
    }
}

void ModifierLivre()
{
    Console.WriteLine("### Modifier par ID ###");
    Console.Write("Id du livre à modifier:");
    int id = int.Parse(Console.ReadLine());

    MySqlConnection connection = new MySqlConnection(connectionString);
    try
    {
        string queryCheck = "SELECT COUNT(*) FROM Personne WHERE id = @id";
        MySqlCommand cmdCheck = new MySqlCommand(queryCheck, connection);
        cmdCheck.Parameters.AddWithValue("@id", id);
        int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

        if(count == 0)
        {
            Console.WriteLine("Aucun livre trouvée avec cet ID");
            return;
        }

        Console.WriteLine("Nouveau Titre :");
        var titre = Console.ReadLine();
        Console.WriteLine("Nouvel Auteur :");
        var auteur = Console.ReadLine();
        Console.WriteLine("nouvel année de publication :");
        var anneePublication = int.Parse(Console.ReadLine());
        Console.WriteLine("Nouvel isbn :");
        var isbn = Console.ReadLine();


        string query = "UPDATE Livre SET titre = @titre , auteur = @auteur , anneePublication = @anneePublication , isbn = @isbn WHERE id = @id";


        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@titre", titre);
        cmd.Parameters.AddWithValue("@auteur", auteur);
        cmd.Parameters.AddWithValue("@anneePublication", anneePublication);
        cmd.Parameters.AddWithValue("@isbn", isbn);

        int rowsAffected = cmd.ExecuteNonQuery();
        if (rowsAffected > 0)
        {
            Console.WriteLine("Livre modifié avec succès");
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine("Erreur : " + ex.Message);
    }
    finally
    {
        connection.Close();
    }
}


void SupprimerLivre()
{
    Console.WriteLine("### Supprimer un Livre ###");
    Console.WriteLine("Id du livre a supprimer :");
    int id = int.Parse(Console.ReadLine());

    MySqlConnection connection = new MySqlConnection(connectionString);
    try
    {
        connection.Open();

        string query = "DELETE FROM Livre WHERE id = @id";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@id", id);

        int rowsAffected = cmd.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
            Console.WriteLine("Livre supprimé avec succès");
        }
        else
        {
            Console.WriteLine("Aucun Livre trouvée a cet ID");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erreur :" + ex.Message);
    }
    finally
    {
        connection.Close();
    }
}
