using Exo_01_ADO;
using MySql.Data.MySqlClient;

Console.WriteLine("Exo_01_ADO");

string connectionString = "Server=localhost;Database=exo_01_ADO ;User Id=root;Password=root";

void AjouterLivre()
{
    Console.WriteLine("Ajouter d'un livre !");
    Console.WriteLine("Titre: ");
    string titre = Console.ReadLine()!;
    Console.WriteLine("Auteur: ");
    string auteur = Console.ReadLine()!;
    Console.WriteLine("Annés de publication: ");
    int anneePublication = int.Parse(Console.ReadLine()!);
    Console.WriteLine("ISBN: ");
    string isbn = Console.ReadLine()!;

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

void AfficherToutesLesLivres()
{
    Console.WriteLine("Liste des livres");
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

void RechercherLivreParId()
{
    Console.WriteLine("Rechercher des livres par Id");
    Console.WriteLine("Id du livre Recherché : ");
    var id = int.Parse(Console.ReadLine()!);
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

void UpdateLivre()
{
    Console.WriteLine("Modifier un livre");
    Console.WriteLine("Id du livre Modifier : ");
    var id = int.Parse(Console.ReadLine()!);

    MySqlConnection connection = new MySqlConnection(connectionString);
    try
    {
        connection.Open();
        string queryCheck = "SELECT COUNT(*) FROM Livre WHERE id = @id";

        MySqlCommand cmdCheck = new MySqlCommand(queryCheck, connection);
        cmdCheck.Parameters.AddWithValue("@id", id);
        int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

        //MySqlDataReader reader = cmdCheck.ExecuteReader();

        if (count == 0)
        {
            Console.WriteLine("Aucun livre avec cet ID");
            return;
        }

        Console.WriteLine("Titre: ");
        string titre = Console.ReadLine()!;
        Console.WriteLine("Auteur: ");
        string auteur = Console.ReadLine()!;
        Console.WriteLine("Annés de publication: ");
        int anneePublication = int.Parse(Console.ReadLine()!);
        Console.WriteLine("ISBN: ");
        string isbn = Console.ReadLine()!;

        string query = "UPDATE Livre SET titre = @titre, auteur = @auteur, anneePublication = @anneePublication, isbn = @isbn WHERE @id = id";

        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@titre", titre);
        cmd.Parameters.AddWithValue("@auteur", auteur);
        cmd.Parameters.AddWithValue("@anneePublication", anneePublication);
        cmd.Parameters.AddWithValue("@isbn", isbn);

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

void DeleteLivre()
{
    Console.WriteLine("--- Supprimer une personne ---");
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
            Console.WriteLine("Personne supprimé avec succès");
        }
        else
        {
            Console.WriteLine("Aucune personne trouvée a cet ID");
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
AjouterLivre();
AfficherToutesLesLivres();
//RechercherLivreParId();
//UpdateLivre();
DeleteLivre();
