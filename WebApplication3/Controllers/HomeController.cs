using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using WebApplication3.Models.ViewModel;
using WebApplication3.DAL;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {

        public int tailleTableau = 10;
        public static string recherche;

        public ActionResult TableauVehicule(string id)
        {
            int Id;
            bool ID = int.TryParse(id,out Id);
            if (ID)
            {
                Id = int.Parse(id);
                using (IDal dal = new Dal())
                {
                    ViewBag.Command = CommandeNumber();

                    List<Vehicules> ListeDesVehicule = dal.ObtientToutLesVehicules();
                    List<Vehicules> ListeDe10 = dal.ObtientXVehicules(Id, tailleTableau, ListeDesVehicule);
                    
                    if (ListeDesVehicule.Count() % 10 == 0)
                    {
                        ViewBag.PageMax = ListeDesVehicule.Count() / 10;
                    }
                    else
                    {
                        ViewBag.PageMax = (ListeDesVehicule.Count() / 10) +1;
                    }
                    ViewBag.PageMin = 1;
                    //List<Vehicules> ListeDe10 = new List<Vehicules>();
                    //int i;
                    //int max;
                    //if (Id == ViewBag.Max)
                    //{
                    //    max = ListeDesVehicule.Count()-1;
                    //}
                    //else
                    //{
                    //    max = 10 * Id;
                    //}
                    //for (i = (10*Id)-10; i < max; i++)
                    //{
                    //    ListeDe10.Add(ListeDesVehicule[i]);
                    //}
                    ViewBag.Page = Id;
                    return View(ListeDe10);
                }
            }
            else
            {
                return View("Error");
            }
            

        }

        public ActionResult RechercheVehicule(string id)
        {
            ViewBag.Command = CommandeNumber();

            int Id;
            bool ID = int.TryParse(id, out Id);
            if (ID)
            {
                Id = int.Parse(id);
                using (IDal dal = new Dal())
                {
                    if(Request.HttpMethod == "POST")
                    {
                        recherche = Request.Form["searchInput"];
                    }

                    List<Vehicules> ListeDesVehicule = dal.ObtientLesVehiculesRecherche(recherche);
                    ViewBag.recherche = recherche;
                    if (ListeDesVehicule.Count() % 10 == 0)
                    {
                        ViewBag.PageMax = ListeDesVehicule.Count() / 10;
                    }
                    else
                    {
                        ViewBag.PageMax = (ListeDesVehicule.Count() / 10)+1;
                    }
                    
                    ViewBag.PageMin = 1;
                    List<Vehicules> ListeDe10 = dal.ObtientXVehicules(Id, tailleTableau, ListeDesVehicule);
                    //int i;
                    //int max;
                    //if (Id == ViewBag.Max || ListeDesVehicule.Count() < 10)
                    //{
                    //    max = ListeDesVehicule.Count();
                    //}
                    //else
                    //{
                    //    max = 10 * Id;
                    //}
                    //for (i = (10 * Id) - 10; i < max; i++)
                    //{
                    //    ListeDe10.Add(ListeDesVehicule[i]);
                    //}
                    ViewBag.Page = Id;
                    return View(ListeDe10);

                }
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult RechercheFournisseur(string id)
        {
            ViewBag.Command = CommandeNumber();

            int Id;
            bool ID = int.TryParse(id, out Id);
            if (ID)
            {
                Id = int.Parse(id);
                using (IDal dal = new Dal())
                {
                    if (Request.HttpMethod == "POST")
                    {
                        recherche = Request.Form["searchInput"];
                    }

                    List<Fournisseurs> ListeDesFournisseur = dal.ObtientLesFournisseurRecherche(recherche);
                    ViewBag.recherche = recherche;
                    if(ListeDesFournisseur.Count() % 10 == 0)
                    {
                        ViewBag.PageMax = ListeDesFournisseur.Count() / 10;
                    }
                    else
                    {
                        ViewBag.PageMax = (ListeDesFournisseur.Count() / 10)+1;
                    }
                    ViewBag.PageMin = 1;
                    List<Fournisseurs> ListeDe10 = dal.ObtientXFournisseur(Id, tailleTableau, ListeDesFournisseur);
                    //int i;
                    //int max;
                    //if (Id == ViewBag.Max || ListeDesFournisseur.Count() < 10)
                    //{
                    //    max = ListeDesFournisseur.Count();
                    //}
                    //else
                    //{
                    //    max = 10 * Id;
                    //}
                    //for (i = (10 * Id) - 10; i < max; i++)
                    //{
                    //    ListeDe10.Add(ListeDesFournisseur[i]);
                    //}
                    ViewBag.Page = Id;
                    return View(ListeDe10);

                }
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult TableauCommande()
        {
            ViewBag.Command = "0";
            using (IDal dal = new Dal())
            {
                List<Equipements> ListeDe5 = dal.ObtenirLesCommand();

                //dal.ModifiéLesCommand(ListeDe5);

                return View(ListeDe5);
            }
            
        }

        public ActionResult TableauCommand(string id)
        {
            ViewBag.Command = CommandeNumber();
            int Id;
            bool ID = int.TryParse(id, out Id);
            if (ID)
            {
                Id = int.Parse(id);
                using (IDal dal = new Dal())
                {
                    List<Doc_Entete_Four> ListeDe5 = dal.ObtenirXCommande(Id,5);

                    return View(ListeDe5);
                }
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult TableauFournisseur(string id)
        {
            ViewBag.Command = CommandeNumber();
            int Id;
            bool ID = int.TryParse(id, out Id);
            if (ID)
            {
                Id = int.Parse(id);
                using (IDal dal = new Dal())
                {
                    List<Fournisseurs> ListeDesFournisseur = dal.ObtientToutLesFournisseur();
                    if(ListeDesFournisseur.Count() %10 == 0)
                    {
                        ViewBag.PageMax = ListeDesFournisseur.Count() / 10;
                    }
                    else
                    {
                        ViewBag.PageMax = (ListeDesFournisseur.Count() / 10) + 1;
                    }
                    ViewBag.PageMin = 1;
                    List<Fournisseurs> ListeDe10 = dal.ObtientXFournisseur(Id, tailleTableau, ListeDesFournisseur);
                    //int i;
                    //int max;
                    //if (Id == ViewBag.Max)
                    //{
                    //    max = ListeDesFournisseur.Count() - 1;
                    //}
                    //else
                    //{
                    //    max = 10 * Id;
                    //}
                    //for (i = (10 * Id) - 10; i < max; i++)
                    //{
                    //    ListeDe10.Add(ListeDesFournisseur[i]);
                    //}
                    ViewBag.Page = Id;
                    return PartialView(ListeDe10);
                }
            }
            else
            {
                return View("Error");
            }


        }

        public ActionResult EditerVehicule(string id)
        {
            ViewBag.Command = CommandeNumber();
            int Id;
            bool ID = int.TryParse(id, out Id);
            if (ID)
            {
                Id = int.Parse(id);
                using (IDal dal = new Dal())
                {
                    if (Request.HttpMethod == "POST")
                    {

                        string erreur = dal.ModdVehicule(Request,Id);
                        if (erreur == "")
                        {
                            return new EmptyResult();
                        }
                        else
                        {
                            ViewBag.list = erreur;
                            return PartialView("Modal/ErreurAjout");
                        }


                    }
                    else
                    {
                        AfficheVehiculeViewModel model = new AfficheVehiculeViewModel();
                        model.familles = dal.ObtientToutLesFamilleVehicules();
                        model.vehicule = dal.GetVehicule(Id);
                        return PartialView("Modal/ModalView/EditerVehicule", model);
                    }
                    
                }
            }
            else
            {
                return View("Error");
            }

        }

        public ActionResult AjouterVehicule()
        {
            ViewBag.Command = CommandeNumber();
            using (IDal dal = new Dal())
            {
                if (Request.HttpMethod == "POST")
                {

                    string erreur = dal.AddVehicule(Request);
                    if (erreur == "")
                    {
                        return null;
                    }
                    else
                    {
                        ViewBag.list = erreur;
                        return PartialView("Modal/ErreurAjout");
                    }


                }
                else
                {
                    AfficheVehiculeViewModel model = new AfficheVehiculeViewModel();
                    model.familles = dal.ObtientToutLesFamilleVehicules();
                    model.vehicule = new Vehicules();
                    return PartialView("Modal/ModalView/AjouterVehicule", model);
                }
            }
        }

        public ActionResult SuprimerVehicule(string id)
        {
            ViewBag.Command = CommandeNumber();
            int Id;
            bool ID = int.TryParse(id, out Id);
            if (ID)
            {
                Id = int.Parse(id);
                if (Request.HttpMethod == "POST")
                {
                    using (IDal dal = new Dal())
                    {
                        string erreur = dal.DelVehicule(Id);
                        ViewBag.erreur = erreur;
                        return PartialView("Modal/Succes");
                    }
                }
                else
                {
                    return PartialView("Modal/ModalView/SuprimerVehicule");
                }
                    
                
            }
            else
            {
                return PartialView("Error");
            }

        }

        public ActionResult EditerFournisseur(string id)
        {
            ViewBag.Command = CommandeNumber();
            int Id;
            bool ID = int.TryParse(id, out Id);
            if (ID)
            {
                Id = int.Parse(id);
                using (IDal dal = new Dal())
                {
                    if (Request.HttpMethod == "POST")
                    {

                        string erreur = dal.ModdFournisseur(Request, Id);
                        if (erreur == "")
                        {
                            return new EmptyResult();
                        }
                        else
                        {
                            ViewBag.list = erreur;
                            return PartialView("Modal/ErreurAjout");
                        }


                    }
                    else
                    {
                        AfficheFournisseurViewModel model = new AfficheFournisseurViewModel();
                        model.fournisseur = dal.GetFournisseur(Id);
                        return PartialView("Modal/ModalView/EditerFournisseur", model);
                    }

                }
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult AjouterFournisseur()
        {
            ViewBag.Command = CommandeNumber();
            using (IDal dal = new Dal())
            {
                if (Request.HttpMethod == "POST")
                {

                    string erreur = dal.AddFournisseur(Request);
                    if (erreur == "")
                    {
                        return null;
                    }
                    else
                    {
                        ViewBag.list = erreur;
                        return PartialView("Modal/ErreurAjout");
                    }


                }
                else
                {
                    AfficheFournisseurViewModel model = new AfficheFournisseurViewModel();
                    model.fournisseur = new Fournisseurs();
                    return PartialView("Modal/ModalView/AjouterFournisseur", model);
                }
            }
        }

        public ActionResult SuprimerFournisseur(string id)
        {
            ViewBag.Command = CommandeNumber();
            int Id;
            bool ID = int.TryParse(id, out Id);
            if (ID)
            {
                Id = int.Parse(id);
                if (Request.HttpMethod == "POST")
                {
                    using (IDal dal = new Dal())
                    {
                        string erreur = "";
                        erreur = dal.DelFournisseur(Id);
                        ViewBag.erreur = erreur;
                        return PartialView("Modal/Succes");
                    }
                }
                else
                {
                    return PartialView("Modal/ModalView/SuprimerFournisseur");
                }


            }
            else
            {
                return PartialView("Error");
            }
        }

        public ActionResult Connexion()
        {
            ViewBag.Command = CommandeNumber();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Command = CommandeNumber();
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Command = CommandeNumber();
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void ActiveCommand(string id){
            int Id;
            bool ID = int.TryParse(id, out Id);
            if (ID)
            {
                Id = int.Parse(id);
                using (IDal dal = new Dal())
                {
                    Equipements equip = dal.GetEquipement(Id);
                    dal.ModifierCommand(equip);
                }


            }
        }

        public int CommandeNumber()
        {
            using (IDal dal = new Dal())
            {
                return dal.ObtenirLesCommand().Count();
            }
        }
    }


}