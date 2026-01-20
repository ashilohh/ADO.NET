using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADONET.Classes
{
    internal class Repo
    {
        private static string connectionString = "Server=localhost;Database=exo1 ;User ID=root;Password=root";

        public static void AjouterLivre(Livre livre)
        {
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

        public static void AfficherTousLesLivres()
        {
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
                            reader.GetInt32("anneePublication"),
                            reader.GetString("isbn")
                            );

                        Console.WriteLine(livre);
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

        public static void RechercherLivreParID(int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                Livre livre = null;
                string queryCheck = "SELECT COUNT(*) FROM Personne WHERE id = @id";
                MySqlCommand cmdCheck = new MySqlCommand(queryCheck, connection);
                cmdCheck.Parameters.AddWithValue("@id", id);
                int count = Convert.ToInt32(cmdCheck.ExecuteScalar());
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

        public static void ModifierLivre(int id, string newTitre, string newAuteur, int newAnnee, string newIsbn)
        {

            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                string queryCheck = "SELECT COUNT(*) FROM Personne WHERE id = @id";
                MySqlCommand cmdCheck = new MySqlCommand(queryCheck, connection);
                cmdCheck.Parameters.AddWithValue("@id", id);
                int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

                if (count == 0)
                {
                    Console.WriteLine("Aucun livre trouvée avec cet ID");
                    return;
                }


                string query = "UPDATE Livre SET titre = @titre , auteur = @auteur , anneePublication = @anneePublication , isbn = @isbn WHERE id = @id";


                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@titre", newTitre);
                cmd.Parameters.AddWithValue("@auteur", newAuteur);
                cmd.Parameters.AddWithValue("@anneePublication", newAnnee);
                cmd.Parameters.AddWithValue("@isbn", newIsbn);

                int rowsAffected = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Erreur : " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public static void SupprimerLivre(int id)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();

                string query = "DELETE FROM Livre WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);

                int rowsAffected = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Erreur :" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
