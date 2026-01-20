
using exo2.Classes;
using MySql.Data.MySqlClient;
using System;

string connectionString = "Server=localhost;Database=Exo2 ;User ID=root;Password=root";

void AfficherTousClients()
{
    Console.WriteLine("---- Liste des Clients ----");
    MySqlConnection connection = new MySqlConnection(connectionString);

    try
    {
        connection.Open();

        string query = "SELECT * FROM Clients";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        MySqlDataReader reader = cmd.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Clients c = new Clients(
                    reader.GetInt32("id"),
                    reader.GetString("nom"),
                    reader.GetString("prenom"),
                    reader.GetString("adresse"),
                    reader.GetString("code_postal"),
                    reader.GetString("ville"),
                    reader.GetString("telephone")
                    );

                Console.WriteLine(c);
            }
        }
        else
        {
            Console.WriteLine("Aucun client enregistré");
        }
        reader.Close();

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    finally { connection.Close(); }
}

void AjouterClient()
{
    Console.WriteLine("--- Ajout d'un Client ---");
    Console.Write("Nom: ");
    string nom = Console.ReadLine();
    Console.Write("Prénom: ");
    string prenom = Console.ReadLine();
    Console.Write("Adresse: ");
    string adresse = Console.ReadLine();
    Console.Write("Code postal: ");
    string code_postal = Console.ReadLine();
    Console.Write("Ville: ");
    string ville = Console.ReadLine();
    Console.Write("Numéro de téléphone: ");
    string telephone = Console.ReadLine();

    Clients client = new Clients(nom,prenom,adresse,code_postal,ville,telephone);

    MySqlConnection connection = new MySqlConnection(connectionString);
    try
    {
        connection.Open();

        string query = "INSERT INTO Clients (nom,prenom,adresse,code_postal,ville,telephone) VALUES (@nom,@prenom,@adresse,@code_postal,@ville,@telephone)";

        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@nom", client.Nom);
        cmd.Parameters.AddWithValue("@prenom", client.Prenom);
        cmd.Parameters.AddWithValue("@adresse", client.Adresse);
        cmd.Parameters.AddWithValue("@code_postal", client.CodePostal);
        cmd.Parameters.AddWithValue("@ville", client.Ville);
        cmd.Parameters.AddWithValue("@telephone", client.Telephone);

        int rows = cmd.ExecuteNonQuery();
        if (rows > 0)
        {
            Console.WriteLine("Client ajouté avec succès !");
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

void ModifierClient()
{
    Console.WriteLine("---Modifier les infos du client---");
    Console.Write("ID du client à modifier: ");
    var id = int.Parse(Console.ReadLine());

    MySqlConnection connection = new MySqlConnection(connectionString);

    try
    {
        connection.Open();

        string queryCheck = "SELECT COUNT(*) FROM Clients WHERE id = @id";
        MySqlCommand cmdCheck = new MySqlCommand(queryCheck, connection);
        cmdCheck.Parameters.AddWithValue("@id", id);
        int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

        if (count == 0)
        {
            Console.WriteLine("Aucun client trouvée avec cet Id");
            return;
        }

        Console.WriteLine("Nouveau Nom :");
        var nom = Console.ReadLine();
        Console.WriteLine("Nouveau Prenom :");
        var prenom = Console.ReadLine();
        Console.WriteLine("nouvelle adresse :");
        var adresse = Console.ReadLine();
        Console.WriteLine("Nouvel code postal :");
        var code_postal = Console.ReadLine();
        Console.WriteLine("Nouvel ville :");
        var ville = Console.ReadLine();
        Console.WriteLine("Nouvel téléphone :");
        var telephone = Console.ReadLine();

        string query = "UPDATE Clients SET nom = @nom , prenom = @prenom , adresse = @adresse , code_postal = @code_postal, ville = @ville, telephone = @telephone WHERE id = @id";


        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@nom", nom);
        cmd.Parameters.AddWithValue("@prenom", prenom);
        cmd.Parameters.AddWithValue("@adresse", adresse);
        cmd.Parameters.AddWithValue("@code_postal", code_postal);
        cmd.Parameters.AddWithValue("@ville", ville);
        cmd.Parameters.AddWithValue("@telephone", telephone);

        int rowsAffected = cmd.ExecuteNonQuery();
        if (rowsAffected > 0)
        {
            Console.WriteLine("Client modifié avec succès");
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

void SupprimerClient()
{
    Console.WriteLine("--- Supprimer un client ---");
    Console.WriteLine("Id du client :");
    int id = int.Parse(Console.ReadLine());

    MySqlConnection connection = new MySqlConnection(connectionString);
    try
    {
        connection.Open();

        string query = @"
DELETE FROM Clients WHERE id = @id;
DELETE FROM Commandes WHERE id = @client_id";

        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@client_id", id);

        int rowsAffected = cmd.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
            Console.WriteLine("Client supprimé avec succès");
        }
        else
        {
            Console.WriteLine("Aucun client trouvée a cet ID");
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

void AfficherDetailsClient()
{
    Console.WriteLine("--- Consulter les détails---");
    Console.WriteLine("Id client :");
    int id = int.Parse(Console.ReadLine());

    MySqlConnection connection = new MySqlConnection(connectionString);
    try
    {
        connection.Open();

        string query = @"
SELECT
  c.id AS client_id, c.nom AS client_nom, c.prenom AS client_prenom, c.adresse AS client_adresse, c.code_postal AS client_code_postal, c.ville AS client_ville, c.telephone AS client_telephone,
  cmd.id AS commande_id, cmd.date_ AS date_, cmd.montant AS montant, cmd.client_id AS client_id
FROM Clients c
LEFT JOIN Commandes cmd ON cmd.client_id = c.id
WHERE c.id = @id;
";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@id", id);

        MySqlDataReader reader = cmd.ExecuteReader();

        Clients client = null;

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                if (client == null)
                {
                    client = new Clients(
                        reader.GetInt32("client_id"),
                        reader.GetString("client_nom"),
                        reader.GetString("client_prenom"),
                        reader.GetString("client_adresse"),
                        reader.GetString("client_code_postal"),
                        reader.GetString("client_ville"),
                        reader.GetString("client_telephone")
                    );
                }

                Commandes comd = new Commandes(
                    reader.GetInt32("commande_id"),
                    reader.GetDateTime("date_"),
                    reader.GetDecimal("montant"),
                    reader.GetInt32("client_id")
                    );

                Console.WriteLine(comd.ToString());
            }
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

void AjouterCommande(){
    Console.WriteLine("---Ajouter une commande---");
    Console.WriteLine("Id Client:");
    int clientId = int.Parse(Console.ReadLine());

    MySqlConnection connection = new MySqlConnection(connectionString);
    try
    {
        connection.Open();

        string queryCheck = "SELECT COUNT(*) FROM Clients WHERE id = @id";
        MySqlCommand cmdCheck = new MySqlCommand(queryCheck, connection);
        cmdCheck.Parameters.AddWithValue("@id", clientId);
        int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

        if (count == 0)
        {
            Console.WriteLine("Aucun client trouvé avec cet Id.");
            return;
        }

        Console.WriteLine("Date:" + DateTime.Now.ToString("dd-MM-yyyy"));
        DateTime date_ = DateTime.Now;
        Console.WriteLine("Montant de la commande: ");
        decimal montant = decimal.Parse(Console.ReadLine());

        Commandes comd = new Commandes(date_,montant,clientId);

        string query = "INSERT INTO Commandes (date_,montant,client_id) VALUES (@date_,@montant,@client_id)";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@date_", comd.Date);
        cmd.Parameters.AddWithValue("@montant", comd.Montant);
        cmd.Parameters.AddWithValue("@client_id", comd.ClientID);

        int rows = cmd.ExecuteNonQuery();
        if (rows > 0)
        {
            Console.WriteLine("Commande ajouté avec succès !");
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


AfficherTousClients();
AfficherDetailsClient();