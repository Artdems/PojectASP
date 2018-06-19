using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication3.Models;

namespace WebApplication3.DAL
{
    public interface IDal : IDisposable
    {
        List<Vehicules> ObtientToutLesVehicules();
        List<Vehicules> ObtientXVehicules(int id, int x, List<Vehicules> list);
        Vehicules GetVehicule(int id);
        Fournisseurs GetFournisseur(int id);
        string DelVehicule(int id);
        string DelFournisseur(int id);
        List<Familles_Vehicules> ObtientToutLesFamilleVehicules();
        string AddVehicule(HttpRequestBase Request);
        string AddFournisseur(HttpRequestBase Request);
        string ModdVehicule(HttpRequestBase Request, int id);
        string ModdFournisseur(HttpRequestBase Request, int Id);
        List<Fournisseurs> ObtientToutLesFournisseur();
        List<Fournisseurs> ObtientXFournisseur(int id, int x, List<Fournisseurs> list);
        List<Doc_Entete_Four> ObtenirLesCommande();
        List<Doc_Entete_Four> ObtenirXCommande(int id, int x);
        List<Vehicules> ObtientLesVehiculesRecherche(string recherche);
        List<Fournisseurs> ObtientLesFournisseurRecherche(string recherche);
        List<Equipements> ObtenirLesCommand();
        void ModifierCommand(Equipements equip);
        Equipements GetEquipement(int id);
    }
}