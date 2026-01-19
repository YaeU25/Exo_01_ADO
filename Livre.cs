using System;
using System.Collections.Generic;
using System.Text;

namespace Exo_01_ADO;

public class Livre
{
    public int Id { get; set; }
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

    public Livre(int id, string titre, string auteur, int anneePublication, string isbn) : this(titre, auteur, anneePublication, isbn)
    {
        Id = id;
    }

    public override string ToString()
    {
        return $"Id: {this.Id}, Titre : {this.Titre}, Auteur : {this.Auteur}, Anné : {this.AnneePublication}, ISBN : {this.Isbn}";
    }
}
