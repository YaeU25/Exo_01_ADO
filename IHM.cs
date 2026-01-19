using System;
using System.Collections.Generic;
using System.Text;

namespace Exo_01_ADO;

public class IHM
{
    public static void start()
    {
        Console.WriteLine("Exo_01_ADO");
        int choix;

        while (true)
        {
            Console.WriteLine("===== Menu =====");
            Console.WriteLine("[1]: Ajouter un nouveau livre");
            Console.WriteLine("[2]: Afficher tous les livres");
            Console.WriteLine("[3]: Rechercher un livre par id");
            Console.WriteLine("[4]: Modifier un livre par id");
            Console.WriteLine("[5]: Supprimer un livre par id");
            Console.WriteLine("[0]: Quitter");
            Console.WriteLine("Choix : ");
            choix = int.Parse(Console.ReadLine()!);

            switch (choix) {
                case 1:
                    Repository ReAjout = new Repository();
                    Console.WriteLine("Ajouter d'un livre !");
                    Console.WriteLine("Titre: ");
                    string titre = Console.ReadLine()!;
                    Console.WriteLine("Auteur: ");
                    string auteur = Console.ReadLine()!;
                    Console.WriteLine("Annés de publication: ");
                    int anneePublication = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("ISBN: ");
                    string isbn = Console.ReadLine()!;
                    ReAjout.AjouterLivre(titre, auteur, anneePublication, isbn);
                    break;
                case 2:
                    Repository ReAffiche = new Repository();
                    ReAffiche.AfficherToutesLesLivres();
                    break;
                case 3:
                    Repository ReRecherche = new Repository();
                    Console.WriteLine("Rechercher des livres par Id");
                    Console.WriteLine("Id du livre Recherché : ");
                    int id_1 = int.Parse(Console.ReadLine()!);
                    ReRecherche.RechercherLivreParId(id_1);
                    break;
                case 4:
                    Repository ReModif = new Repository();
                    Console.WriteLine("Modifier un livre");
                    Console.WriteLine("Id du livre Modifier : ");
                    int id_2 = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("Titre: ");
                    string newTitre = Console.ReadLine()!;
                    Console.WriteLine("Auteur: ");
                    string newAuteur = Console.ReadLine()!;
                    Console.WriteLine("Annés de publication: ");
                    int newAnneePublication = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("ISBN: ");
                    string newIsbn = Console.ReadLine()!;
                    ReModif.UpdateLivre(id_2, newTitre, newAuteur, newAnneePublication, newIsbn);
                    break;
                case 5:
                    Repository ReSup = new Repository();
                    Console.WriteLine("--- Supprimer une personne ---");
                    Console.WriteLine("Id du livre a supprimer :");
                    int id_3 = int.Parse(Console.ReadLine()!);
                    ReSup.DeleteLivre(id_3);
                    break;
                case 0:
                    return;
            
            }
        }
    }
}
