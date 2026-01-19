using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exo_01_ADO;

public class Repository
{
   private string connectionString = "Server=localhost;Database=exo_01_ADO ;User Id=root;Password=root";

    public void AjouterLivre(string titre, string auteur, int anneePublication, string isbn)
    {
        Livre livre = new Livre(titre, auteur, anneePublication, isbn);

        Console.WriteLine(livre);

        MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            connection.Open();

            // injections SQL !!!
            string query = "INSERT INTO Livre (titre, auteur, anneePublication, isbn) VALUES(@titre, @auteur, @anneePublication, @isbn)";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@titre", livre.Titre);
            cmd.Parameters.AddWithValue("@auteur", livre.Auteur);
            cmd.Parameters.AddWithValue("@anneePublication", livre.AnneePublication);
            cmd.Parameters.AddWithValue("@isbn", livre.Isbn);

            int rowAffected = cmd.ExecuteNonQuery();

            if (rowAffected > 0)
            {
                Console.WriteLine("Livre ajouté avec succes");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erreur : {e.Message}");
        }
        finally
        {
            connection.Close();
        }
    }

    public void AfficherToutesLesLivres()
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
                    Livre l = new Livre(
                    reader.GetInt32("id"),
                    reader.GetString("titre"),
                    reader.GetString("auteur"),
                    reader.GetInt32("anneePublication"),
                    reader.GetString("isbn")
                    );
                    Console.WriteLine(l);
                }
            }
            else
            {
                Console.WriteLine("Aucun livre");
            }
            reader.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erreur : {e.Message}");
        }
        finally
        {
            connection.Close();
        }
    }

    public void RechercherLivreParId(int id)
    {
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
                Livre l = new Livre(
                reader.GetInt32("id"),
                reader.GetString("titre"),
                reader.GetString("auteur"),
                reader.GetInt32("anneePublication"),
                reader.GetString("isbn")
                );
                Console.WriteLine($"Livre trouvé : {l}");
            }
            else
            {
                Console.WriteLine("Aucun livre avec cet ID");
            }
            reader.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erreur : {e.Message}");
        }
        finally
        {
            connection.Close();
        }
    }

    public void UpdateLivre(int id, string newTitre, string newAuteur, int newAnneePublication, string newIsbn)
    {   
        MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            connection.Open();
            string queryCheck = "SELECT COUNT(*) FROM Livre WHERE id = @id";

            MySqlCommand cmdCheck = new MySqlCommand(queryCheck, connection);
            cmdCheck.Parameters.AddWithValue("@id", id);
            int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

            if (count == 0)
            {
                Console.WriteLine("Aucun livre avec cet ID");
                return;
            }

            string query = "UPDATE Livre SET titre = @titre, auteur = @auteur, anneePublication = @anneePublication, isbn = @isbn WHERE @id = id";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@titre", newTitre);
            cmd.Parameters.AddWithValue("@auteur", newAuteur);
            cmd.Parameters.AddWithValue("@anneePublication", newAnneePublication);
            cmd.Parameters.AddWithValue("@isbn", newIsbn);

            int rowAffected = cmd.ExecuteNonQuery();

            if (rowAffected > 0)
            {
                Console.WriteLine("Livre ajouté avec succes");
            }

        }
        catch (Exception e)
        {
            Console.WriteLine($"Erreur : {e.Message}");
        }
        finally
        {
            connection.Close();
        }
    }
    
    public void DeleteLivre(int id)
    {
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
                Console.WriteLine("Aucune livre trouvée a cet ID");
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
}
