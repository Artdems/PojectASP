using WebApplication3.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity.Validation;
using System.Net.Http;

namespace WebApplication3.Models
{
    public class Dal : IDal
    {
        private SAGA_GASQUETEntities bdd;


        public Dal()
        {
            bdd = new SAGA_GASQUETEntities();
        }

        public void Dispose()
        {
            bdd.Dispose();
        }

        public List<Vehicules> ObtientToutLesVehicules()
        {
            return bdd.Vehicules.ToList();
        }

        public List<Vehicules> ObtientXVehicules(int id, int x, List<Vehicules> list)
        {
            var listDeX= new List<Vehicules>();
            if(id == 1)
            {
                listDeX = list.Take(x).ToList();
            }
            else
            {
                listDeX = list.Skip(x * (id - 1)).Take(x).ToList();
            }
            return listDeX;
        }

        public List<Fournisseurs> ObtientToutLesFournisseur()
        {
            return bdd.Fournisseurs.ToList();
        }

        public List<Fournisseurs> ObtientXFournisseur(int id, int x, List<Fournisseurs> list)
        {
            var listDeX = new List<Fournisseurs>();
            if (id == 1)
            {
                listDeX = list.Take(x).ToList();
            }
            else
            {
                listDeX = list.Skip(x * (id - 1)).Take(x).ToList();
            }
            return listDeX;
        }

        public List<Familles_Vehicules> ObtientToutLesFamilleVehicules()
        {
            return bdd.Familles_Vehicules.ToList();
        }

        public List<Doc_Entete_Four> ObtenirLesCommande()
        {
            return bdd.Doc_Entete_Four.ToList();
        }

        public List<Doc_Entete_Four> ObtenirXCommande(int id, int x)
        {
            var list = from p in bdd.Doc_Entete_Four where p.id_fournisseur == id orderby p.id_doc select p;
            return list.Take(x).ToList();
        }

        public List<Vehicules> ObtientLesVehiculesRecherche(string recherche)
        {
            List<Vehicules> list = bdd.Vehicules.Where(p => p.Designation.Contains(recherche) || p.immat.Contains(recherche) || p.marque.Contains(recherche) || p.modele.Contains(recherche)).ToList();
            return list;
        }

        public List<Fournisseurs> ObtientLesFournisseurRecherche(string recherche)
        {
            List<Fournisseurs> list = bdd.Fournisseurs.Where(p => p.code_fournisseur.Contains(recherche) || p.nom.Contains(recherche) || p.ville.Contains(recherche) || p.telephone1.Contains(recherche)).ToList();
            return list;
        }

        public string ModdVehicule(HttpRequestBase Request, int Id)
        {
            string erreur = "";
            int parseInt;
            double parseDouble;

            Vehicules vehicule = bdd.Vehicules.FirstOrDefault(vehi => vehi.id_vehicule == Id);
            vehicule.num_interne = Request.Form["vehicule.num_interne"];

            vehicule.Designation = Request.Form["vehicule.Designation"];
            vehicule.immat = Request.Form["vehicule.immat"];
            vehicule.Num_serie = Request.Form["vehicule.Num_serie"];

            int.TryParse(Request.Form["vehicule.compteur_heure"], out parseInt);
            if (parseInt != 0)
            {
                vehicule.compteur_heure = parseInt;
            }
            else
            {
                vehicule.compteur_heure = null;
            }

            int.TryParse(Request.Form["vehicule.compteur_KM"], out parseInt);
            if (parseInt != 0)
            {
                vehicule.compteur_KM = parseInt;
            }
            else
            {
                vehicule.compteur_KM = null;
            }
            if (Request.Form["vehicule_date_compteur"] != "")
            {
                vehicule.date_compteur = Convert.ToDateTime(Request.Form["vehicule_date_compteur"]);
            }
            else
            {
                vehicule.date_compteur = null;
            }
            if (Request.Form["vehicule_date_heure"] != "")
            {
                vehicule.date_heure = Convert.ToDateTime(Request.Form["vehicule_date_heure"]);
            }
            else
            {
                vehicule.date_heure = null;
            }
            vehicule.marque = Request.Form["vehicule.marque"];
            vehicule.caracteristique = Request.Form["vehicule.caracteristique"];
            vehicule.modele = Request.Form["vehicule.modele"];
            vehicule.num_analystique = Request.Form["vehicule.num_analystique"];

            if (Request.Form["vehicule_id_famille_vehicule"] != null)
            {
                string nom_famille = Request.Form["vehicule_id_famille_vehicule"];
                vehicule.id_famille_vehicule = GetIdFamille(nom_famille);
            }
            else
            {
                vehicule.id_famille_vehicule = null;
            }


            vehicule.actif = Request.Form["vehicule.actif"];
            vehicule.telephone = Request.Form["vehicule.telephone"];
            if (Request.Form["vehicule_dateEntree"] != "")
            {
                vehicule.dateEntree = Convert.ToDateTime(Request.Form["vehicule_dateEntree"]);
            }
            else
            {
                vehicule.dateEntree = null;
            }

            vehicule.photo = Request.Form["vehicule.photo"];

            double.TryParse(Request.Form["vehicule.valeur_acquisition"], out parseDouble);
            if (parseDouble != 0)
            {
                vehicule.valeur_acquisition = parseDouble;
            }
            else
            {
                vehicule.valeur_acquisition = null;
            }

            vehicule.num_facture = Request.Form["vehicule.num_facture"];
            vehicule.location = Request.Form["vehicule.location"];

            if (Request.Form["vehicule_date_sortie"] != "")
            {
                vehicule.date_sortie = Convert.ToDateTime(Request.Form["vehicule_date_sortie"]);
            }
            else
            {
                vehicule.date_sortie = null;
            }

            int.TryParse(Request.Form["vehicule.id_societe"], out parseInt);
            if (parseInt != 0)
            {
                vehicule.id_societe = parseInt;
            }
            else
            {
                vehicule.id_societe = null;
            }
            try
            {
                bdd.SaveChanges();
            }
            catch (Exception exeption)
            {
                erreur = exeption.InnerException.Message;
            }
            return erreur;
        }

        public string ModdFournisseur(HttpRequestBase Request,int Id)
        {
            string erreur = "";
            int parseInt;

            Fournisseurs fournisseur = bdd.Fournisseurs.FirstOrDefault(four => four.id_fournisseur == Id);
            fournisseur.code_fournisseur = Request.Form["fournisseur.code_fournisseur"];

            fournisseur.nom = Request.Form["fournisseur.nom"];
            fournisseur.adresse1 = Request.Form["fournisseur.adresse1"];
            fournisseur.adresse2 = Request.Form["fournisseur.adresse2"];
            fournisseur.code_postal = Request.Form["fournisseur.code_postal"];
            fournisseur.ville = Request.Form["fournisseur.ville"];
            fournisseur.telephone1 = Request.Form["fournisseur.telephone1"];
            fournisseur.telecopie = Request.Form["fournisseur.telecopie"];
            fournisseur.pays = Request.Form["fournisseur.pays"];
            fournisseur.contact = Request.Form["fournisseur.contact"];
            fournisseur.telephone2 = Request.Form["fournisseur.telephone2"];
            fournisseur.email = Request.Form["fournisseur.email"];
            fournisseur.compte_client = Request.Form["fournisseur.compte_client"];
            fournisseur.actif = Request.Form["fournisseur.actif"];
            fournisseur.compte_auxiliaire = Request.Form["fournisseur.compte_auxiliaire"];
            fournisseur.compte_collectif = Request.Form["fournisseur.compte_collectif"];
            fournisseur.OKCarburant = Request.Form["fournisseur.OKCarburant"];
            int.TryParse(Request.Form["fournisseur.import"], out parseInt);
            if (parseInt != 0)
            {
                fournisseur.import = parseInt;
            }
            else
            {
                fournisseur.import = null;
            }
            int.TryParse(Request.Form["fournisseur.carte"], out parseInt);
            if (parseInt != 0)
            {
                fournisseur.carte = parseInt;
            }
            else
            {
                fournisseur.carte = null;
            }
            int.TryParse(Request.Form["fournisseur.id_famille_fournisseur"], out parseInt);
            if (parseInt != 0)
            {
                fournisseur.id_famille_fournisseur = parseInt;
            }
            else
            {
                fournisseur.id_famille_fournisseur = null;
            }
            try
            {
                //fournisseurs old = bdd.fournisseurs.FirstOrDefault(vehi => vehi.id_fournisseur == Id);
                //bdd.Entry(old).CurrentValues.SetValues(fournisseur);
                bdd.SaveChanges();
            }
            catch(Exception exception)
            {
                erreur = exception.InnerException.Message;
            }
            return erreur;
        }

        public string AddVehicule(HttpRequestBase Request)
        {
            string erreurMessage = ""; 
            int parseInt;
            double parseDouble;
            
            Vehicules vehicule = new Vehicules();
            vehicule.num_interne = Request.Form["vehicule.num_interne"];
            Vehicules existe = bdd.Vehicules.FirstOrDefault(vehi => vehi.num_interne == vehicule.num_interne);
            
            vehicule.Designation = Request.Form["vehicule.Designation"];
            vehicule.immat = Request.Form["vehicule.immat"];
            vehicule.Num_serie = Request.Form["vehicule.Num_serie"];

            int.TryParse(Request.Form["vehicule.compteur_heure"], out parseInt);
            if (parseInt != 0)
            {
                vehicule.compteur_heure = parseInt;
            }
            else
            {
                vehicule.compteur_heure = null;
            }

            int.TryParse(Request.Form["vehicule.compteur_KM"], out parseInt);
            if (parseInt != 0)
            {
                vehicule.compteur_KM = parseInt;
            }
            else
            {
                vehicule.compteur_KM = null;
            }
            if (Request.Form["vehicule_date_compteur"] != "")
            {
                vehicule.date_compteur = Convert.ToDateTime(Request.Form["vehicule_date_compteur"]);
            }
            else
            {
                vehicule.date_compteur = null;
            }
            if (Request.Form["vehicule_date_heure"] != "")
            {
                vehicule.date_heure = Convert.ToDateTime(Request.Form["vehicule_date_heure"]);
            }
            else
            {
                vehicule.date_heure = null;
            }
            vehicule.marque = Request.Form["vehicule.marque"];
            vehicule.caracteristique = Request.Form["vehicule.caracteristique"];
            vehicule.modele = Request.Form["vehicule.modele"];
            vehicule.num_analystique = Request.Form["vehicule.num_analystique"];

            if (Request.Form["vehicule_id_famille_vehicule"] != null)
            {
                string nom_famille = Request.Form["vehicule_id_famille_vehicule"];
                vehicule.id_famille_vehicule = GetIdFamille(nom_famille);
            }
            else
            {
                vehicule.id_famille_vehicule = null;
            }
            

            vehicule.actif = Request.Form["vehicule.actif"];
            vehicule.telephone = Request.Form["vehicule.telephone"];
            if (Request.Form["vehicule_dateEntree"] != "")
            {
                vehicule.dateEntree = Convert.ToDateTime(Request.Form["vehicule_dateEntree"]);
            }
            else
            {
                vehicule.dateEntree = null;
            }

            vehicule.photo = Request.Form["vehicule.photo"];

            double.TryParse(Request.Form["vehicule.valeur_acquisition"], out parseDouble);
            if (parseDouble != 0)
            {
                vehicule.valeur_acquisition = parseDouble;
            }
            else
            {
                vehicule.valeur_acquisition = null;
            }
            
            vehicule.num_facture = Request.Form["vehicule.num_facture"];
            vehicule.location = Request.Form["vehicule.location"];

            if (Request.Form["vehicule_date_sortie"] != "")
            {
                vehicule.date_sortie = Convert.ToDateTime(Request.Form["vehicule_date_sortie"]);
            }
            else
            {
                vehicule.date_sortie = null;
            }

            int.TryParse(Request.Form["vehicule.id_societe"], out parseInt);
            if (parseInt != 0)
            {
                vehicule.id_societe = parseInt;
            }
            else
            {
                vehicule.id_societe = null;
            }
            try
            {
                int last = bdd.Vehicules.Max(vehi => vehi.id_vehicule);
                vehicule.id_vehicule = last + 1;
                bdd.Vehicules.Add(vehicule);
                bdd.SaveChanges();
            }
            catch (Exception exeption)
            {
                erreurMessage = exeption.InnerException.Message;
            }
            return erreurMessage;
            
        }

        public string AddFournisseur(HttpRequestBase Request)
        {
            string erreurMessage = "";
            int parseInt;

            Fournisseurs fournisseur = new Fournisseurs();
            fournisseur.code_fournisseur = Request.Form["fournisseur.code_fournisseur"];

            fournisseur.nom = Request.Form["fournisseur.nom"];
            fournisseur.adresse1 = Request.Form["fournisseur.adresse1"];
            fournisseur.adresse2 = Request.Form["fournisseur.adresse2"];
            fournisseur.code_postal = Request.Form["fournisseur.code_postal"];
            fournisseur.ville = Request.Form["fournisseur.ville"];
            fournisseur.telephone1 = Request.Form["fournisseur.telephone1"];
            fournisseur.telecopie = Request.Form["fournisseur.telecopie"];
            fournisseur.pays = Request.Form["fournisseur.pays"];
            fournisseur.contact = Request.Form["fournisseur.contact"];
            fournisseur.telephone2 = Request.Form["fournisseur.telephone2"];
            fournisseur.email = Request.Form["fournisseur.email"];
            fournisseur.compte_client = Request.Form["fournisseur.compte_client"];
            fournisseur.actif = Request.Form["fournisseur.actif"];
            fournisseur.compte_auxiliaire = Request.Form["fournisseur.compte_auxiliaire"];
            fournisseur.compte_collectif = Request.Form["fournisseur.compte_collectif"];
            fournisseur.OKCarburant = Request.Form["fournisseur.OKCarburant"];
            int.TryParse(Request.Form["fournisseur.import"], out parseInt);
            if (parseInt != 0)
            {
                fournisseur.import = parseInt;
            }
            else
            {
                fournisseur.import = null;
            }
            int.TryParse(Request.Form["fournisseur.carte"], out parseInt);
            if (parseInt != 0)
            {
                fournisseur.carte = parseInt;
            }
            else
            {
                fournisseur.carte = null;
            }
            int.TryParse(Request.Form["fournisseur.id_famille_fournisseur"], out parseInt);
            if (parseInt != 0)
            {
                fournisseur.id_famille_fournisseur = parseInt;
            }
            else
            {
                fournisseur.id_famille_fournisseur = null;
            }
            try
            {
                int last = bdd.Fournisseurs.Max(four => four.id_fournisseur);
                fournisseur.id_fournisseur = last + 1;
                bdd.Fournisseurs.Add(fournisseur);
                bdd.SaveChanges();
            }
            catch (Exception exeption)
            {
                erreurMessage = exeption.InnerException.Message;
            }
            return erreurMessage;
        }

        public Vehicules GetVehicule(int id)
        {
            Vehicules vehi = bdd.Vehicules.FirstOrDefault(vehic => vehic.id_vehicule == id);
            if(vehi != null)
            {
                return vehi;
            }
            else
            {
                return null;
            }
        }

        public Fournisseurs GetFournisseur(int id)
        {
            Fournisseurs four = bdd.Fournisseurs.FirstOrDefault(fourn => fourn.id_fournisseur == id);
            if(four != null)
            {
                return four;
            }
            else
            {
                return null;
            }
        }

        public int GetIdFamille(string name)
        {
            Familles_Vehicules famille = bdd.Familles_Vehicules.FirstOrDefault(fam => fam.libelle_famille_vehicules == name);
            return famille.id_famille_vehicule;
        }

        public void DelSuiviMedicale(Suivis_Medicals med)
        {
            IEnumerable<Restrictions_Medicales> ListRestMed = bdd.Restrictions_Medicales.ToList().Where(resMed => resMed.id_suivi_medical == med.id_suivi_medical);
            foreach (Restrictions_Medicales restMed in ListRestMed)
            {
                bdd.Restrictions_Medicales.Remove(restMed);
                bdd.SaveChanges();
            }
            bdd.Suivis_Medicals.Remove(med);
            bdd.SaveChanges();
        }

        public void DelDetailAccident(Details_Accidents DAcc)
        {
            IEnumerable<Accidents> ListAcc = bdd.Accidents.ToList().Where(acc => acc.id_detail_accident == DAcc.id_detail_accident);
            foreach(Accidents Acc in ListAcc)
            {
                bdd.Accidents.Remove(Acc);
                bdd.SaveChanges();
            }
            IEnumerable<Suivis_Medicals> ListSuivMed = bdd.Suivis_Medicals.ToList().Where(med => med.id_accident == DAcc.id_detail_accident);
            foreach(Suivis_Medicals suivMed in ListSuivMed)
            {
                DelSuiviMedicale(suivMed);
            }
            bdd.Details_Accidents.Remove(DAcc);
            bdd.SaveChanges();
        }

        public string DelVehicule(int id)
        {

            string errorMessage = "";
            Vehicules vehi = bdd.Vehicules.FirstOrDefault(vehic => vehic.id_vehicule == id);
            
            if (vehi != null)
            {
                
                //IEnumerable<Affectations> listaff = bdd.Affectations.ToList().Where(aff => aff.id_vehicule == id);
                //foreach(Affectations affectation in listaff)
                //{
                //    bdd.Affectations.Remove(affectation);
                //    bdd.SaveChanges();
                //}
                //IEnumerable<Details_Accidents> detailAcc = bdd.Details_Accidents.ToList().Where(acc => acc.id_vehicule == id);
                //foreach(Details_Accidents Dacc in detailAcc)
                //{
                //    DelDetailAccident(Dacc);
                //}
                //IEnumerable<Controles_Vehicules> listCon = bdd.Controles_Vehicules.ToList().Where(con => con.id_vehicule == id);
                //foreach (Controles_Vehicules con in listCon)
                //{
                //    bdd.Controles_Vehicules.Remove(con);
                //    bdd.SaveChanges();
                //}
                //IEnumerable<Vehicules_Compteurs> listVehiCont = bdd.Vehicules_Compteurs.ToList().Where(vehiCont => vehiCont.id_vehicule == id);
                //foreach (Vehicules_Compteurs vehiCont in listVehiCont)
                //{
                //    bdd.Vehicules_Compteurs.Remove(vehiCont);
                //    bdd.SaveChanges();
                //}
                //IEnumerable<Doc_Lignes_Four> listDLF = bdd.Doc_Lignes_Four.ToList().Where(dlf => dlf.id_vehicule == id);
                //foreach (Doc_Lignes_Four DLF in listDLF)
                //{
                //    bdd.Doc_Lignes_Four.Remove(DLF);
                //    bdd.SaveChanges();
                //}

                try
                {
                    bdd.Vehicules.Remove(vehi);
                    bdd.SaveChanges();
                }
                //catch (System.Data.Entity.Infrastructure.DbUpdateException exeption)
                catch(Exception exeption)
                {
                    errorMessage = exeption.InnerException.Message;
                }

                
            }

            return errorMessage;

        }

        public string DelFournisseur(int id)
        {
            string errorMessage = "";
            Fournisseurs four = bdd.Fournisseurs.FirstOrDefault(fourn => fourn.id_fournisseur == id);
            try
            {
                bdd.Fournisseurs.Remove(four);
                bdd.SaveChanges();
            }
            catch (Exception exeption)
            {
                errorMessage = exeption.InnerException.Message;
            }

            return errorMessage;
        }

        public Equipements GetEquipement(int id)
        {
            return bdd.Equipements.FirstOrDefault(equip => equip.id_equipement == id);
        }

        public List<Equipements> ObtenirLesCommand()
        {
            return bdd.Equipements.Where(com => com.actif == "N").ToList();
        }

        public void ModifierCommand(Equipements equip)
        {
            equip.actif = "O";
            try
            {
                bdd.SaveChanges();
            }
            catch (Exception exception)
            {
            }
        }
        
    }
}