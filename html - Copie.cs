
using GEDCOM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
//using GH;
//using System.Drawing;

namespace HTML
{
    public class HTMLClass
    {
        public class CodeStructNote
        {
            public string N0_ID;
            public int numero;
            public string N0_NOTE_Texte;
            public GEDCOMClass.USER_REFERENCE_NUMBER N1_REFN_liste;
            public string N1_RIN;
            public List<int> N1_SOUR_citation_liste_ID;
            public List<int> N1_SOUR_source_liste_ID;
            public GEDCOMClass.CHANGE_DATE N1_CHAN;
        }
        public class ListeSUBMITTERxx
        {
            public int numero;
            public string ID;
        }
        private class ID_numero
        {
            public string ID;
            public int numero;
        }
        private static int Avoir_numero_de_la_source(string ID, List<ID_numero> source_liste_ID_numero)
        {
            foreach(ID_numero info in source_liste_ID_numero)
            {
                if (ID == info.ID) return info.numero;
            }
            return 0;
        }
        private static (int, List<ID_numero>) Avoir_numero_pour_liste(string ID, List<ID_numero> liste)
        {
            int numero = 0;
            ID_numero temp = new ID_numero();
            if (liste.Count == 0)
            {
                temp.ID = ID;
                temp.numero = 1;
                liste.Add(temp);
                return (1, liste);
            }
            // verifie si déjà dans la liste et retourne le numero si c'est le cas
            foreach (ID_numero info in liste)
            {
                if (info.ID == ID)
                {
                    temp.ID = ID;
                    temp.numero = info.numero;
                    return (1, liste);
                }
            }
            // trouver le plus grand numero
            foreach (ID_numero info in liste)
            {
                if (info.numero >= numero) numero = info.numero + 1;
            }
            temp.ID = ID;
            temp.numero = numero;
            liste.Add(temp);
            return (numero, liste);
        }
        private static (string, List<ID_numero>, List<ID_numero>, List<ID_numero>, List<ID_numero>) Avoir_chercheur(
            List<ID_numero> liste, 
            string DossierSortie, 
            string sousDossier, 
            List<ID_numero> note_liste_ID_numero,
            List<ID_numero> citation_liste_ID_numero,
            List<ID_numero> source_liste_ID_numero,
            List<ID_numero> repo_liste_ID_numero,
            int tab)
        {
            if (liste == null) 
                return ("", note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
            if (liste.Count == 0) 
                return( "", note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
            string espace = Tabulation(tab);
            string texte = "";
            string temp;
            foreach (ID_numero item in liste)
            {
                GEDCOMClass.SUBMITTER_RECORD info = GEDCOMClass.Avoir_info_chercheur(item.ID);
                if (info == null) 
                    return ("", note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
                texte += Separation("mince", tab);
                texte += espace + "<table class=\"tableau\">\n";
                texte += espace + "\t<tr>\n";
                texte += espace + "\t\t<td style=\"background-color:#BBB;border:2px solid #000;margin-left:5px;vertical-align:top;\" colspan=2>\n";// ne pas enlever Border.
                texte += espace + "\t\t\t<span style=\"display: table-cell;\"><a id=\"RefSubmitter" + item.numero.ToString() + "\"></a>\n";
                texte += espace + "\t\t\t<span class=\"chercheur\">" + item.numero.ToString() + "</span>\n";
                texte += espace + "\t\t\t\t<span style=\"display:table-cell;text-align:top; padding-left:5px\">\n";
                texte += espace + "\t\t\t\t\t" + info.N1_NAME + "/n";
                if (GH.Properties.Settings.Default.deboguer) texte += espace + "\t\t\t\t\t\t\t\t{" + info.N0_ID + "}\n";
                texte += espace + "\t\t\t\t</span>\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t<tr>\n";
                if ((info.N1_OBJE_ID_liste.Count > 0) && (GH.Properties.Settings.Default.photo_principal))
                {
                    texte += espace + "\t<tr>\n";
                    texte += espace + "\t\t<td  class=\"indexCol1\">\n";
                    texte += espace + "\t\t\t&nbsp;\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td>\n";
                    // avoir le portrait
                    string fichierPortrait;
                    string IDPortrait;
                    if (info.N1_OBJE_ID_liste.Count > 0)
                    {
                        IDPortrait = info.N1_OBJE_ID_liste[0];
                        GEDCOMClass.MULTIMEDIA_RECORD portraitInfo = GEDCOMClass.AvoirMedia(IDPortrait);
                        fichierPortrait = CopierObjet(portraitInfo.N1_FILE, "individus", DossierSortie);
                        if (fichierPortrait == null )
                        {
                            if (sousDossier == "index")
                            {
                                fichierPortrait = "commun\\media_manquant.svg";
                            }
                            else
                            {
                                fichierPortrait = "../commun\\media_manquant.svg";
                            }
                        }
                        else
                        {
                            if (sousDossier == "index")
                            {
                                fichierPortrait = "individus\\medias\\" + fichierPortrait;
                            }
                            else
                            {
                                fichierPortrait = "medias\\" + fichierPortrait;
                            }
                        }
                        texte += espace + "\t\t\t<br /><img class=\"portrait\" src=\"" + fichierPortrait + "\" alt=\"\" />\n";
                    }
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                // adresse
                GEDCOMClass.ADDRESS_STRUCTURE InfoAdresse = info.N1_ADDR;
                if (InfoAdresse != null)
                {
                    texte += espace + "\t<tr>\n";
                    texte += espace + "\t\t<td  class=\"indexCol1\">\n";
                    texte += espace + "\t\t\tAdresse\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td>\n";
                    if (InfoAdresse.N0_ADDR != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N0_ADDR + "\n";
                    if (InfoAdresse.N1_ADR1 != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N1_ADR1 + "\n";
                    if (InfoAdresse.N1_ADR1 != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N1_ADR2 + "\n";
                    if (InfoAdresse.N1_ADR3 != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N1_ADR3 + "\n";
                    if (InfoAdresse.N1_CITY != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N1_CITY + "\n";
                    if (InfoAdresse.N1_STAE != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N1_STAE + "\n";
                    if (InfoAdresse.N1_CTRY != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N1_CTRY + "\n";
                    if (InfoAdresse.N1_POST != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N1_POST + "\n";
                    // note pour GRAMP
                    if (InfoAdresse.N1_NOTE_liste_ID != null)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td colspan=2>\n";
                        (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                            Avoir_note_lien(
                                InfoAdresse.N1_NOTE_liste_ID,
                                note_liste_ID_numero,
                                citation_liste_ID_numero,
                                source_liste_ID_numero,
                                repo_liste_ID_numero,
                                6);
                            texte += temp;
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                // téléphone
                if (info.N1_PHON_liste != null)
                {
                    foreach (string Tel in info.N1_PHON_liste)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\tTéléphone\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\t" + Tel + "\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                }
                // fax
                if (info.N1_FAX_liste != null)
                {
                    foreach (string fax in info.N1_FAX_liste)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\tFax\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\t" + fax + "\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                }
                //  courriel
                if (info.N1_EMAIL_liste != null)
                {
                    foreach (string courriel in info.N1_EMAIL_liste)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\tCourriel;\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\t" + courriel + "\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                }
                // www
                if (info.N1_WWW_liste != null)
                {
                    foreach (string www in info.N1_WWW_liste)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\tPage Web\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\t" + www + "\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                }
                // langue
                if (info.N1_LANG != "")
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tLangue\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + info.N1_LANG + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // RFN numéro de fichier d'enregistrement permanent
                if (info.N1_RFN != "")
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td colspan=2>\n";
                    texte += "\t\t\t\t\t\tNuméro d'enregistrement permanent(RFN) " + info.N1_RFN + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // RIN id d'enregistrement automatisé
                if (info.N1_RIN != "")
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td colspan=2>\n";
                    texte += "\t\t\t\t\t\tID d'enregistrement automatisé(RIN) " + info.N1_RIN + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // note du sumiteur
                if (info.N1_NOTE_liste_ID != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td colspan=2>\n";
                    (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                    Avoir_note_lien(
                        info.N1_NOTE_liste_ID,
                        note_liste_ID_numero,
                        citation_liste_ID_numero,
                        source_liste_ID_numero,
                        repo_liste_ID_numero,
                        tab + 1);
                    texte += temp;
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // si changement de date
                if (info.N1_CHAN != null && GH.Properties.Settings.Default.voir_date_changement)
                {
                    GEDCOMClass.CHANGE_DATE N1_CHAN = info.N1_CHAN;
                    if (N1_CHAN.N1_CHAN_DATE != "")
                    {
                        (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) = 
                            Date_Changement_Bloc(
                                N1_CHAN, 
                                note_liste_ID_numero, 
                                citation_liste_ID_numero, 
                                source_liste_ID_numero,
                                repo_liste_ID_numero,
                                tab + 3,
                                false);
                        texte += espace + "\t<tr>\n";
                        texte += espace + "\t\t<td colspan=2>\n";
                        texte += temp;
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                }
                texte += espace + "</table>\n";
            }
            return (texte, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
        }
        private (string, int, bool, List<ID_numero>, List<ID_numero>, List<ID_numero>, List<ID_numero>) Groupe_attribut(
            List<GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE> liste,
            int numeroCarte,
            string sousDossier,
            string dossierSortie,
            List<ID_numero> note_liste_ID_numero,
            List<ID_numero> citation_liste_ID_numero,
            List<ID_numero> source_liste_ID_numero,
            List<ID_numero> repo_liste_ID_numero,
            int tab)
        {
            if (liste == null) 
                return ("", numeroCarte, false, note_liste_ID_numero, citation_liste_ID_numero, 
                    source_liste_ID_numero, repo_liste_ID_numero);
            if (liste.Count == 0) 
                return ("", numeroCarte, false, note_liste_ID_numero, citation_liste_ID_numero,
                    source_liste_ID_numero, repo_liste_ID_numero);
            string texte = "";
            texte += Separation("large", 3);
            texte += "<a id=\"groupe_attribut\"></a>\n";
            texte += Groupe("debut", 3);
            texte += "\t\t\t\t<table class=\"titre\">\n";
            texte += "\t\t\t\t\t<tr>\n";
            texte += "\t\t\t\t\t\t<td>\n";
            if (liste.Count == 1)
                texte += "\t\t\t\t\t\t\tAttribut\n";
            else 
                texte += "\t\t\t\t\t\t\tAttributs\n";
            texte += "\t\t\t\t\t\t</td>\n";
            texte += "\t\t\t\t\t</tr>\n";
            texte += "\t\t\t\t</table>\n";
            Liste_par_date par_date = new Liste_par_date();
            liste.Sort(par_date);
            foreach (GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE info in liste)
            {
                string temp = null;
                (temp, numeroCarte, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                    Avoir_evenement(
                        info,
                        null,
                        null,
                        null,
                        numeroCarte, 
                        sousDossier, 
                        dossierSortie,
                        note_liste_ID_numero,
                        citation_liste_ID_numero,
                        source_liste_ID_numero,
                        repo_liste_ID_numero,
                        tab);
                texte += temp;
                texte += Groupe("fin", tab);
            }
            return (texte, numeroCarte, true, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
        }
        public string AssemblerPatronymePrenom(string patronyme, string prenom)
        {
            if (prenom == "" && patronyme == "") return "";
            if (prenom == "") prenom = "?";
            if (patronyme == "") patronyme = "?";
            return patronyme + ", " + prenom;
        }
        private static (string, List<ID_numero>, List<ID_numero>, List<ID_numero>, List<ID_numero>) Avoir_citation_source_lien
            (List<string> liste_citation_ID,
                List<ID_numero> note_liste_ID_numero,
                List<ID_numero> citation_liste_ID_numero,
                List<ID_numero> source_liste_ID_numero,
                List<ID_numero> repo_liste_ID_numero,
                int tab)
        {
            if (!GH.Properties.Settings.Default.voir_reference) 
                return ("", note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
            string espace = Tabulation(tab);
            string texte = "";
            int compteur_source;
            // liste de citation et souce
            List<string> citation_liste_ID = new List<string>();
            List<string> source_liste_ID = new List<string>();
            List<string> source_liste_ID_temp = new List<string>();
            // fait une liste de citation et une liste de source
            foreach (string ID in liste_citation_ID)
            {
                GEDCOMClass.SOURCE_CITATION info_citation = GEDCOMClass.Avoir_info_citation(ID);
                if (info_citation.N0_ID_source != null)
                {
                    // Source seulement
                    if (info_citation.N0_Titre == null &&
                        info_citation.N1_DATA == null &&
                        info_citation.N1_EVEN == null &&
                        info_citation.N1_NOTE_liste_ID.Count == 0 &&
                        info_citation.N1_OBJE_ID_liste.Count == 0 &&
                        info_citation.N1_PAGE == null &&
                        info_citation.N1_QUAY == null &&
                        info_citation.N1_TEXT == null &&
                        info_citation.N2_DATA_DATE == null &&
                        info_citation.N2_DATA_TEXT == null &&
                        info_citation.N2_EVEN_ROLE == null
                        )
                    {
                        source_liste_ID.Add(info_citation.N0_ID_source);
                        (compteur_source, source_liste_ID_numero) = Avoir_numero_pour_liste(
                            info_citation.N0_ID_source, source_liste_ID_numero);
                    }
                    else
                    {
                        // citation avec une source
                        citation_liste_ID.Add(info_citation.N0_ID_citation);
                        if (info_citation.N0_ID_source != null)
                        {
                            source_liste_ID_temp.Add(info_citation.N0_ID_source);
                        }
                    }
                }
                else
                {
                    // citation sans source ID
                    citation_liste_ID.Add(info_citation.N0_ID_citation);
                }
            }
            Separation("mince", 6);
            texte += espace + "<div style=\"font-Size:11px;font-weight:normal;\">\n";
            if (citation_liste_ID.Count > 0)
            {
                int compteur_citation;
                if (citation_liste_ID.Count == 1)
                {
                    texte += espace + "\tVoir la citation \n";
                }
                else
                {
                    texte += espace + "\tVoir les citations \n";
                }
                foreach (string ID in citation_liste_ID)
                {
                    (compteur_citation, citation_liste_ID_numero) = Avoir_numero_pour_liste(ID, citation_liste_ID_numero);
                    texte += "<a class=\"citation\" href=\"#citation-" + compteur_citation.ToString() + "\">" + compteur_citation.ToString() + "</a>, ";
                }
                texte = texte.TrimEnd(' ', ',') + "\n";
            }
            if (source_liste_ID.Count > 0 )
            {
                
                if (source_liste_ID.Count == 1)
                {
                    texte += espace + "\t Voir la source \n";
                }
                else
                {
                    texte += espace + "\t Voir les sources \n";
                }
                foreach (string ID in source_liste_ID)
                {
                    (compteur_source, source_liste_ID_numero) = Avoir_numero_pour_liste(ID, source_liste_ID_numero);
                    texte += "<a class=\"source\" href=\"#source-" + compteur_source.ToString() + "\">" + compteur_source.ToString() + "</a>, ";
                }
                texte = texte.TrimEnd(' ', ',') + "\n";
            }
            texte += espace + "</div>\n";
            // ajouter dans liste les citatioons avec source seulement
            foreach(string ID in source_liste_ID_temp)
            {
                (compteur_source, source_liste_ID_numero) = Avoir_numero_pour_liste(ID, source_liste_ID_numero);
            }

            return (texte, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
        }
        private static (string, int, List<ID_numero>, List<ID_numero>, List<ID_numero>, List<ID_numero>) Avoir_evenement(
            GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE info,
            string date_individu_naissance,
            string date_conjoint_naissance,
            string date_conjointe_naissance,
            int numeroCarte,
            string sousDossier, 
            string dossierSortie,
            List<ID_numero> note_liste_ID_numero,
            List<ID_numero> citation_liste_ID_numero,
            List<ID_numero> source_liste_ID_numero,
            List<ID_numero> repo_liste_ID_numero,
            int tab)
        {
            string temp;
            string texte = "";
            string espace = Tabulation(tab);
            texte += Separation("mince", tab);
            texte += Groupe("debut", tab);
            // restriction
            if (info.N2_RESN != null)
            {
                texte += espace + "<div class=\"blink2\">\n";
                texte += espace + "\tRestriction\n";
                texte += espace + "\t" + info.N2_RESN + "\n";
                texte += espace + "</div>\n";
            }
            texte += espace + "<table class=\"tableau\">\n";
            if (info.titre != null)
            {
                texte += espace + "\t<tr class=\"caseEvenement\" style=\"background-color:#BBB;border:2px solid #000;\">\n";
                texte += espace + "\t\t<td class=\"caseEvenement evenement\" colspan=2>\n";
                // titre
                if (info.titre != null)
                {
                    if (info.N1_EVEN == "FACT")
                    {
                        texte += espace + "\t\t\t<strong>" + info.N2_TYPE + " " + info.titre + "</strong>\n";
                    }
                    else texte += espace + "\t\t\t<strong>" + info.titre + "</strong>\n";
                }
                else
                {
                    texte += espace + "\t\t\t&nbsp;\n";
                }
                // date
                string date;
                if (GH.Properties.Settings.Default.date_longue)
                {
                    date = GEDCOMClass.ConvertirDateTexte(info.N2_DATE);
                }
                else
                {
                    date = info.N2_DATE;
                }
                // si date
                if (date != "")
                {
                    if (
                        date.Contains("de ") ||
                        date.Contains("autour ") ||
                        date.Contains("avant ") ||
                        date.Contains("estimé ") ||
                        date.Contains("calculé ") ||
                        date.Contains("jusqu") ||
                        date.Contains("après ") ||
                        date.Contains("entre ") ||
                        date.Contains(" à ") ||
                        date.Contains(" et ")
                        )
                    {
                        texte += espace + "\t\t\t " + date;
                    }
                    else
                    {
                        texte += espace + "\t\t\t le " + date;
                    }
                }
                // si age
                if (info.N1_EVEN.ToUpper() != "BIRT")
                {
                    if (info.N2_AGE != null)
                    {
                        texte += espace + " à l'age de " + info.N2_AGE;
                    }
                    else
                    {
                        string age = Avoir_age(date_individu_naissance, info.N2_DATE);
                        if (age != null) texte += espace + " à l'age de " + age;
                    }
                    string age_HUSB;
                    if (info.N3_HUSB_AGE != null)
                    {
                        age_HUSB = info.N3_HUSB_AGE;
                    }
                    else
                    {
                        age_HUSB = Avoir_age(date_conjoint_naissance, info.N2_DATE);
                    }
                    string age_WIFE;
                    if (info.N3_WIFE_AGE != null)
                    {
                        age_WIFE = info.N3_WIFE_AGE;
                    }
                    else
                    {
                        age_WIFE = Avoir_age(date_conjointe_naissance, info.N2_DATE);
                    }
                    if (age_HUSB != null)
                    {
                        texte += ", conjoint à l'age de " + age_HUSB;
                        if (age_WIFE == null) texte += ".";
                    }
                    if (age_WIFE != null)
                    {
                        texte += ", conjointe à l'age de " + age_WIFE + ".";
                    }
                }
                // si source
                (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) = 
                    Avoir_citation_source_lien(
                        info.N2_SOUR_citation_liste_ID,
                        note_liste_ID_numero,
                        citation_liste_ID_numero,
                        source_liste_ID_numero,
                        repo_liste_ID_numero,
                        tab + 3);
                texte += temp;
                texte += espace + "\n\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            // si type 
            if (info.N2_TYPE != null && info.N1_EVEN != "FACT")
            {
                texte += espace + "\t<tr class=\"caseEvenement\">\n";
                texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                texte += espace + "\t\t\tType \n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t\t<td class=\"caseEvenement\">\n";
                texte += espace + "\t\t\t" + info.N2_TYPE + "\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            // si Place lieu
            if ((info.N2_PLAC != null) && (info.N1_EVEN != "RESI"))
            {
                if (info.N2_PLAC.N0_PLAC != null)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                    texte += espace + "\t\t\tLieu\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td class=\"caseEvenement\">\n";
                    texte += espace + "\t\t\t" + info.N2_PLAC.N0_PLAC + "\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                    // si map
                    if ((info.N2_PLAC.N2_MAP_LATI != null) || (info.N2_PLAC.N2_MAP_LONG != null))
                    {
                        numeroCarte++;
                        texte += espace + "\t<tr class=\"caseEvenement\">\n";
                        texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                        texte += espace + "\t\t\t&nbsp;\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"caseEvenement\">\n";
                        texte += espace + "\t\t\tLatitude " + info.N2_PLAC.N2_MAP_LATI + " Longitude " + info.N2_PLAC.N2_MAP_LONG + "\n";
                        texte += Carte(numeroCarte, info.N2_PLAC.N2_MAP_LATI, info.N2_PLAC.N2_MAP_LONG, info.N2_PLAC.N0_PLAC, 8);
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                    // si FONE
                    if (info.N2_PLAC.N1_FONE != null)
                    {
                        texte += espace + "\t<tr class=\"caseEvenement\">\n";
                        texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                        texte += espace + "\t\t\t&nbsp;\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"caseEvenement\">\n";
                        texte += espace + "\t\t\tPhonétisation " + info.N2_PLAC.N1_FONE + "\n";
                        if (info.N2_PLAC.N2_FONE_TYPE != "")
                        {
                            texte += espace + "\t\t\t " + " type " + info.N2_PLAC.N2_FONE_TYPE + "\n";
                        }
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                    // si ROMN
                    if (info.N2_PLAC.N1_ROMN != null)
                    {
                        texte += espace + "\t<tr class=\"caseEvenement\">\n";
                        texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\"> \n";
                        texte += espace + "\t\t\t&nbsp;\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"caseEvenement\">\n";
                        texte += espace + "\t\t\tRomanisation " + info.N2_PLAC.N1_ROMN + "\n";
                        if (info.N2_PLAC.N2_ROMN_TYPE != "")
                        {
                            texte += espace + "\t\t\t " + " type " + info.N2_PLAC.N2_ROMN_TYPE + "\n";
                        }
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                    // Place NOTE
                    if (info.N2_PLAC.N1_NOTE_liste_ID.Count() > 0)
                    {
                        texte += espace + "\t<tr>\n";
                        texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                        texte += espace + "\t\t\t&nbsp;\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td style=\"border:0px solid black;\">\n";
                        (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                            Avoir_note_lien(
                                info.N2_PLAC.N1_NOTE_liste_ID,
                                note_liste_ID_numero,
                                citation_liste_ID_numero,
                                source_liste_ID_numero,
                                repo_liste_ID_numero,
                                tab + 3);
                        texte += temp;
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                    // SOUR pour GEDitCOM
                    if (info.N2_PLAC.N1_SOUR_citation_liste_ID.Count() > 0)
                    {
                        texte += espace + "\t<tr>\n";
                        texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                        texte += espace + "\t\t\t&nbsp;\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td style=\"border:0px solid black;\">\n";
                        (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) = 
                            Avoir_citation_source_lien(
                                info.N2_PLAC.N1_SOUR_citation_liste_ID,
                                note_liste_ID_numero,
                                citation_liste_ID_numero,
                                source_liste_ID_numero,
                                repo_liste_ID_numero,
                                tab + 3);
                        texte += temp;
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                }
            }
            // adresse
            if (info.N2_ADDR != null)
            {
                texte += espace + "\t<tr class=\"caseEvenement\">\n";
                texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                texte += espace + "\t\t\tAdresse\n";
                texte += espace + "\t\t\t</td>\n";
                texte += espace + "\t\t\t<td>\n";
                texte += espace + "\t\t\t\t&nbsp;\n";
                texte += espace + "\t\t\t</td>\n";
                texte += espace + "\t</tr>\n";
                if (info.N2_ADDR.N0_ADDR != null)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t\t<td class=\"caseEvenement evenementCol1\">\n";
                    texte += espace + "\t\t\t\t&nbsp;\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\t" + info.N2_ADDR.N0_ADDR + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                if (info.N2_ADDR.N1_ADR1 != null)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t\t<td class=\"caseEvenement evenementCol1\">\n";
                    texte += espace + "\t\t\t\t&nbsp;\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\t" + info.N2_ADDR.N1_ADR1 + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                if (info.N2_ADDR.N1_ADR2 != null)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t\t<td class=\"caseEvenement evenementCol1\">\n";
                    texte += espace + "\t\t\t\t&nbsp;\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\t" + info.N2_ADDR.N1_ADR2 + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                if (info.N2_ADDR.N1_ADR3 != null)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t\t<td class=\"caseEvenement evenementCol1\">\n";
                    texte += espace + "\t\t\t\t&nbsp;\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\t" + info.N2_ADDR.N1_ADR3 + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                if (info.N2_ADDR.N1_CITY != null)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t\t<td class=\"caseEvenement evenementCol1 texteR\">\n";
                    texte += espace + "\t\t\t\tVille \n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\t" + info.N2_ADDR.N1_CITY + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                if (info.N2_ADDR.N1_STAE != null)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t\t<td class=\"caseEvenement evenementCol1 texteR\">\n";
                    texte += espace + "\t\t\t\tProvince/État\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\t" + info.N2_ADDR.N1_STAE + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                if (info.N2_ADDR.N1_CTRY != null)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t\t<td class=\"caseEvenement evenementCol1 texteR\">\n";
                    texte += espace + "\t\t\t\tPays\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\t" + info.N2_ADDR.N1_CTRY + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                if (info.N2_ADDR.N1_POST != null)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t\t<td class=\"caseEvenement evenementCol1 texteR\">\n";
                    texte += espace + "\t\t\t\tCode postal\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\t" + info.N2_ADDR.N1_POST + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                
                // note adresse pour GRAMPS
                if (info.N2_ADDR.N1_NOTE_liste_ID != null)
                {
                    texte += "\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t&nbsp;\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                        Avoir_note_lien(
                            info.N2_ADDR.N1_NOTE_liste_ID,
                            note_liste_ID_numero,
                            citation_liste_ID_numero,
                            source_liste_ID_numero,
                            repo_liste_ID_numero,
                            6);
                    texte += temp;
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t</tr>\n";
                }
            }
            // si téléphone
            if (info.N2_PHON_liste != null)
            {
                bool premier = true;
                foreach (string PHON in info.N2_PHON_liste)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                    if (premier)
                    {
                        texte += espace + "\t\t\tTéléphone \n";
                        premier = false;
                    }
                    else
                    {
                        texte += espace + "\t\t\t&nbsp;\n";
                    }
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td>\n";
                    texte += espace + "\t\t\t\t" + PHON + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
            }
            if (info.N2_FAX_liste != null)
            {
                bool premier = true;
                foreach (string FAX in info.N2_FAX_liste)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                    if (premier)
                    {
                        texte += espace + "\t\t\tTélécopieur \n";
                        premier = false;
                    }
                    else
                    {
                        texte += espace + "\t\t\t&nbsp;\n";
                    }
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\t" + FAX + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
            }
            if (info.N2_EMAIL_liste != null)
            {
                bool premier = true;
                foreach (string EMAIL in info.N2_EMAIL_liste)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                    if (premier)
                    {
                        texte += espace + "\t\t\tCourriel \n";
                        premier = false;
                    }
                    else
                    {
                        texte += espace + "\t\t\t&nbsp;\n";
                    }
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td>\n";
                    texte += espace + "\t\t\t" + EMAIL + "\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
            }
            if (info.N2_WWW_liste != null)
            {
                bool premier = true;
                foreach (string WWW in info.N2_WWW_liste)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t\t<td class=\"caseEvenement evenementCol1\">\n";
                    if (premier)
                    {
                        texte += espace + "\t\t\tSite Web \n";
                        premier = false;
                    }
                    else
                    {
                        texte += espace + "\t\t\t&nbsp;\n";
                    }
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td>\n";
                    texte += espace + "\t\t\t" + WWW + "\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
            }
            // si religion
            if (info.N2_RELI != null)
            {
                texte += espace + "\t<tr class=\"caseEvenement\">\n";
                texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\" >\n";
                texte += espace + "\t\t\tReligion \n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t\t<td class=\"caseEvenement\">\n";
                texte += espace + "\t\t\t" + info.N2_RELI + "\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            // si cause
            if (info.N2_CAUS != null)
            {
                texte += espace + "\t<tr class=\"caseEvenement\">\n";
                texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\" >\n";
                texte += espace + "\t\t\tCause de l'événement \n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t\t<td class=\"caseEvenement\">\n";
                texte += espace + "\t\t\t" + info.N2_CAUS + "\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            // si adoption
            if (info.N2_FAMC != null)
            {
                string pereIDAdoption = GEDCOMClass.Avoir_famille_conjoint_ID(info.N2_FAMC);
                string nomPereAdoption = GEDCOMClass.AvoirPrenomPatronymeIndividu(pereIDAdoption);
                string mereIDAdoption = GEDCOMClass.Avoir_famille_conjointe_ID(info.N2_FAMC);
                string nomMereAdoption = GEDCOMClass.AvoirPrenomPatronymeIndividu(mereIDAdoption);
                texte += espace + "\t<tr class=\"caseEvenement\">\n";
                texte += espace + "\t\t<td class=\"caseEvenement\">\n";
                texte += espace + "\t\t\tAdopter par\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t\t<td class=\"caseEvenement\">\n";
                if (info.N2_FAMC_ADOP != null)
                {
                    if (info.N2_FAMC_ADOP.ToUpper() == "BOTH")
                    {
                        texte += espace + "\t\t\t" + nomPereAdoption + " et " + nomMereAdoption + "\n";
                    }
                    else if (info.N2_FAMC_ADOP.ToUpper() == "HUSB")
                    {
                        texte += espace + "\t\t\t" + nomPereAdoption + "\n";
                    }
                    else if (info.N2_FAMC_ADOP.ToUpper() == "WIFE")
                    {
                        texte += espace + "\t\t\t" + nomMereAdoption + "\n";
                    }
                }
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            // si a description
            if (info.description != null)
            {
                texte += espace + "\t<tr class=\"caseEvenement\">\n";
                texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\" style=\"text-align:right;\">\n";
                texte += espace + "\t\t\tDescription \n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t<td class=\"caseEvenement\">\n";
                texte += espace + "\t\t\t" + info.description + "\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            // si a agence
            if (info.N2_AGNC != null)
            {
                texte += espace + "\t<tr class=\"caseEvenement\">\n";
                texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                texte += espace + "\t\t\tAgence\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t\t<td class=\"caseEvenement\" >\n";
                texte += espace + "\t\t\t" + info.N2_AGNC + "\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            // Si résidence 
            if (info.N1_EVEN_texte == "RESI")
            {
                if (info.N2_ADDR != null)
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\" style=\"text-align:right;\">\n";
                    texte += espace + "\t\t\tAdresse: \n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td class=\"caseEvenement\" >\n";
                    texte += espace + "\t\t\t" + info.N2_ADDR.N0_ADDR + "\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                else
                {
                    if (info.N2_ADDR.N1_ADR1 != "")
                    {
                        texte += espace + "\t<tr class=\"caseEvenement\">\n";
                        texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\" style=\"text-align:right;\">\n";
                        texte += espace + "\t\t\tAdresse: \n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t<td class=\"caseEvenement\" >\n";
                        texte += espace + "\t\t\t" + info.N2_ADDR.N1_ADR1 + "\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                    if (info.N2_ADDR.N1_ADR2 != "")
                    {
                        texte += espace + "\t<tr class=\"caseEvenement\">\n";
                        texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\" style=\"text-align:right;\">\n";
                        texte += espace + "\t\t\tAdresse: \n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"caseEvenement\" >\n";
                        texte += espace + "\t\t\t" + info.N2_ADDR.N1_ADR2 + "\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                    if (info.N2_ADDR.N1_ADR3 != "")
                    {
                        texte += espace + "\t<tr class=\"caseEvenement\">\n";
                        texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\" style=\"text-align:right;\" colspan=2>\n";
                        texte += espace + "\t\t\tAdresse: \n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"caseEvenement\" >\n";
                        texte += espace + "\t\t\t" + info.N2_ADDR.N1_ADR3 + "\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                    if (info.N2_ADDR.N1_CITY != "")
                    {
                        texte += espace + "\t<tr class=\"caseEvenement\">\n";
                        texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\" style=\"text-align:right;\" colspan=2>\n";
                        texte += espace + "\t\t\t\tVille: \n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"caseEvenement\" >\n";
                        texte += espace + "\t\t\t" + info.N2_ADDR.N1_CITY + "\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                    if (info.N2_ADDR.N1_STAE != "")
                    {
                        texte += espace + "\t<tr class=\"caseEvenement\">\n";
                        texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\" style=\"text-align:right;\" colspan=2>\n";
                        texte += espace + "\t\t\tProvince: \n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"caseEvenement\" >\n";
                        texte += espace + "\t\t\t" + info.N2_ADDR.N1_STAE + "\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                    if (info.N2_ADDR.N1_CTRY != "")
                    {
                        texte += espace + "\t<tr class=\"caseEvenement evenementCol1\">\n";
                        texte += espace + "\t\t<td class=\"caseEvenement\" style=\"text-align:right;\" colspan=2>\n";
                        texte += espace + "\t\t\tPays: \n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"caseEvenement\" >\n";
                        texte += espace + "\t\t\t" + info.N2_ADDR.N1_CTRY + "\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                    if (info.N2_ADDR.N1_POST != "")
                    {
                        texte += espace + "\t<tr class=\"caseEvenement evenementCol1\">\n";
                        texte += espace + "\t\t<td class=\"caseEvenement\" style=\"text-align:right;\" colspan=2>\n";
                        texte += espace + "\t\t\tCode postal: \n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t\t<td class=\"caseEvenement\" >\n";
                        texte += espace + "\t\t\t" + info.N2_ADDR.N1_POST + "\n";
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                }
            }
            // si Ancestrologie
            // _ANCES_ORDRE
            if (info.N2__ANCES_ORDRE != null)
            {
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                    texte += espace + "\t\t\tOrdre\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td class=\"caseEvenement\" >\n";
                    texte += espace + "\t\t\t" + info.N2__ANCES_ORDRE + " (" + GEDCOMClass.Info_HEADER.N2_SOUR_NAME + ")\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
            }
            // _ANCES_XINSEE
            if (info.N2__ANCES_XINSEE != null)
            {
                {
                    texte += espace + "\t<tr class=\"caseEvenement\">\n";
                    texte += espace + "\t\t<td class=\"caseEvenement evenementCol1\">\n";
                    texte += espace + "\t\t\tCode géographique\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td class=\"caseEvenement\" >\n";
                    texte += espace + "\t\t\t" + info.N2__ANCES_XINSEE + " (" + GEDCOMClass.Info_HEADER.N2_SOUR_NAME + ")\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
            }
            // media
            if (GH.Properties.Settings.Default.voir_media)
            {
                if (info.N2_OBJE_liste_ID.Count > 0)
                {
                    texte += espace + "\t\t<tr>\n";
                    texte += espace + "\t\t\t<td colspan=2>\n";
                    texte += espace + "\t\t\t\t<table class=\"tableauMedia retrait_point5in\" style=\"border:2px;width:480px\">\n";
                    texte += espace + "\t\t\t\t\t<tr>\n";
                    int cRanger = 0;
                    int totalOBJE = info.N2_OBJE_liste_ID.Count;
                    foreach (string media_ID in info.N2_OBJE_liste_ID)
                    {
                        GEDCOMClass.MULTIMEDIA_RECORD OBJE = GEDCOMClass.AvoirMedia(media_ID);
                        texte += espace + "\t\t\t\t\t\t<td class=\"tableauMedia\" style=\"width:50%\">\n";
                        (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) = 
                            Objet(
                                OBJE,
                                sousDossier,
                                dossierSortie,
                                note_liste_ID_numero,
                                citation_liste_ID_numero,
                                source_liste_ID_numero,
                                repo_liste_ID_numero,
                                tab + 6);
                        texte += temp;
                        texte += espace + "\t\t\t\t\t\t</td>\n";
                        cRanger++;
                        if (cRanger % 2 == 0 && cRanger != totalOBJE)
                        {
                            texte += espace + "\t\t\t\t\t</tr>\n";
                            texte += espace + "\t\t\t\t\t<tr>\n";
                        }
                    }
                    if (cRanger % 2 == 1)
                    {
                        texte += espace + "\t\t\t\t\t\t<td class=\"tableauMedia\" style=\"border:0px;width:50%\">\n";
                        texte += "\t\t\t\t\t\t</td>\n";
                    }
                    texte += espace + "\t\t\t\t\t</tr>\n";
                    texte += espace + "\t\t\t\t</table>\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t</tr>\n";
                }
            }
            // si source
            if (info.N2_SOUR_citation_liste_ID.Count > 0)
            {
                texte += espace + "\t<tr class=\"caseEvenement\">\n";
                texte += espace + "\t\t<td class=\"caseEvenement\" colspan=2>\n";
                (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) = 
                        Avoir_citation_source_lien(
                            info.N2_SOUR_citation_liste_ID,
                            note_liste_ID_numero, 
                            citation_liste_ID_numero,
                            source_liste_ID_numero,
                            repo_liste_ID_numero,
                            tab + 3);
                texte += temp;
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            // si note
            if (info.N2_NOTE_liste_ID.Count > 0)
            {
                texte += espace + "\t<tr class=\"caseEvenement\">\n";
                texte += espace + "\t\t<td class=\"caseEvenement\" colspan=2>\n";
                (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                    Avoir_note_lien(
                        info.N2_NOTE_liste_ID,
                        note_liste_ID_numero,
                        citation_liste_ID_numero,
                        source_liste_ID_numero,
                        repo_liste_ID_numero,
                        tab + 3);
                texte += temp;
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            texte += espace + "\t</table>\n";
            return (texte, numeroCarte, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
        }
        private static string Avoir_age(string Naissance, string dateEvenement)
        {
            string r = null;
            try
            {
                if (Naissance == null || dateEvenement == null)
                {
                    return null;
                }
                else if (Naissance == null || dateEvenement == null)
                {
                    return null;
                }
                else if (Naissance.Length != 10 || dateEvenement.Length != 10)
                {
                    return null;
                }
                else
                {
                    // vérifie si alphabet dans Naissance et Décès
                    int c = Naissance.Length;
                    int w = 0;
                    while (w < c)
                    {
                        if ((Naissance[w] >= 'a' && Naissance[w] <= 'z') || (Naissance[w] >= 'A' && Naissance[w] <= 'Z'))
                        {
                            return null;
                        }
                        w++;
                    }
                    c = dateEvenement.Length;
                    w = 0;
                    while (w < c)
                    {
                        if ((dateEvenement[w] >= 'a' && dateEvenement[w] <= 'z') || (dateEvenement[w] >= 'A' && dateEvenement[w] <= 'Z'))
                        {
                            return null;
                        }
                        w++;
                    }
                }
                DateTime nDate = Convert.ToDateTime(Naissance);
                DateTime dDate = Convert.ToDateTime(dateEvenement);
                TimeSpan difference = dDate.Subtract(nDate);
                DateTime age = DateTime.MinValue + difference;
                string annee = (age.Year - 1).ToString();
                string mois = (age.Month - 1).ToString();
                string jour = (age.Day - 1).ToString();
                if (annee == "1")
                {
                    r = annee + " an";
                }
                if (annee == "0")
                {
                    r = null;
                }
                else if (annee != null)
                {
                    r = annee + " ans";
                }

                if (mois != "0")
                {
                    r = r + " " + mois + " mois";
                }
                if (jour == "1")
                {
                    r = r + " " + jour + " jour,";
                }
                else if (jour != "0")
                {
                    r = r + " " + jour + " jours";
                }
                return r;
            }
            catch
            {
                return null;
            }
        }
        /*
        List<ID_numero> note_liste_ID_numero = new List<ID_numero>();
        List<ID_numero> citation_liste_ID_numero = new List<ID_numero>();
        List<ID_numero> source_liste_ID_numero = new List<ID_numero>();
        List<ID_numero> repo_liste_ID_numero = new List<ID_numero>();
        List<ID_numero> temp_liste = new List<ID_numero>();*/
        private static (List<ID_numero>, List<ID_numero>, List<ID_numero>, List<ID_numero>) Avoir_liste_reference(
            string type,
            string ID,
            List<ID_numero> note_liste_ID_numero,
            List<ID_numero> citation_liste_ID_numero,
            List<ID_numero> source_liste_ID_numero,
            List<ID_numero> repo_liste_ID_numero)
        {
            if (type == "individu")
            {

            }
            return (note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
        }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="liste"></param>
            /// <param name="note_liste_ID_numero"></param>
            /// <param name="citation_liste_ID_numero"></param>
            /// <param name="source_liste_ID_numero"></param>
            /// <param name="repo_liste_ID_numero"></param>
            /// <param name="tab"></param>
            /// <returns></returns>
            private static (string, List<ID_numero>, List<ID_numero>, List<ID_numero>, List<ID_numero>) Avoir_note_lien(
            List<string> liste_ID,
            List<ID_numero> note_liste_ID_numero,
            List<ID_numero> citation_liste_ID_numero, 
            List<ID_numero> source_liste_ID_numero,
            List<ID_numero> repo_liste_ID_numero,
            int tab)
        {
            if (!GH.Properties.Settings.Default.voir_note) 
                return ("", note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
            if (liste_ID == null) 
                return ("", note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
            if (liste_ID.Count == 0) 
                return ("", note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
            string temp;
            string espace = Tabulation(tab);
            string texte = espace + "<div style=\"font-Size:11px;font-weight:normal;\">\n";
            texte += espace + "Voir note \n";
            texte += espace;
            int compteur_note;
            foreach (string note_ID in liste_ID)
            {
                GEDCOMClass.NOTE_RECORD info_Note = GEDCOMClass.Avoir_Info_Note(note_ID);
                if (info_Note == null) 
                    return ("", note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
                (compteur_note, note_liste_ID_numero) = Avoir_numero_pour_liste(
                            note_ID, note_liste_ID_numero);
                
                texte += "<a class=\"note\" href=\"#note-" + compteur_note + "\">" + compteur_note + "</a>" + ", ";
                // if note a source
                (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) = 
                    Avoir_citation_source_lien(
                        info_Note.N1_SOUR_citation_liste_ID,
                        note_liste_ID_numero,
                        citation_liste_ID_numero,
                        source_liste_ID_numero,
                        repo_liste_ID_numero,
                        tab);
                texte += temp;
            }
            texte = texte.TrimEnd(' ', ',');
            texte += "\n" + espace + "</div>\n";
            return (texte, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
        }
        public string Avoir_premier_nom_individu(GEDCOMClass.INDIVIDUAL_RECORD info_individu)
        {
            if (info_individu == null) return "";
            if (info_individu.N1_NAME_liste == null) return "";
            if (info_individu.N1_NAME_liste[0].N1_GIVN != null || info_individu.N1_NAME_liste[0].N1_SURN != null)
            {
                string patronyme = info_individu.N1_NAME_liste[0].N1_SURN;
                string prenom = info_individu.N1_NAME_liste[0].N1_GIVN;
                if (prenom == "" && patronyme == "") return info_individu.N1_NAME_liste[0].N0_NAME;
                if (prenom == null && patronyme == null) return info_individu.N1_NAME_liste[0].N0_NAME;
                if (prenom == "") prenom = "?";
                if (patronyme == "") patronyme = "?";
                return patronyme + ", " + prenom;
            }
            else
            {
                return info_individu.N1_NAME_liste[0].N0_NAME;
            }
        }
        private static (string, List<ID_numero>) Avoir_repo_lien(string ID_repo, List<ID_numero> repo_liste_ID_numero, int tab)
        {
            ID_numero infoPlus = new ID_numero();
            if (ID_repo == null) return ("", repo_liste_ID_numero);
            if (ID_repo == "") return ("", repo_liste_ID_numero);
            string espace = Tabulation(tab);
            string texte = espace + "<span style=\"font-Size:11px;font-weight:normal;\">\n";
            texte += espace + "\tVoir le dépôt \n";
            bool trouverRepo = false;
            // si la liste est vide
            if (repo_liste_ID_numero.Count == 0)
            {
                infoPlus.numero = 1;
                infoPlus.ID = ID_repo;
                texte += espace + "\t<a class=\"depot\" href=\"#depot-1\">1</a>, ";
                repo_liste_ID_numero.Add(infoPlus);
            }
            // ajout dans la liste
            else
            {
                // voir si le ID est déjà dans la liste
                texte += espace + "\t";
                foreach (ID_numero f in repo_liste_ID_numero)
                {
                    if (f.ID == ID_repo)  // si la source existe déjà
                    {
                        trouverRepo = true;
                        texte += "<a class=\"depot\" href=\"#depot-" + f.numero.ToString() + "\">" + f.numero.ToString() + "</a>, ";
                    }
                }
                // le ID n'est pas la
                if (!trouverRepo)
                {
                    int compteurRepo = 0;
                    // trouver plus grand numero
                    foreach (ID_numero i in repo_liste_ID_numero)
                    {
                        if (i.numero >= compteurRepo) compteurRepo = i.numero + 1;
                    }
                    infoPlus.numero = compteurRepo;
                    infoPlus.ID = ID_repo;
                    texte += "<a class=\"depot\" href=\"#depot-" + compteurRepo + "\">" + compteurRepo + "</a>, ";
                    repo_liste_ID_numero.Add(infoPlus);
                }
                texte += "\n";
            }
            texte = texte.TrimEnd(' ', ',');
            texte += "\n";
            texte += espace + "</span>\n";
            return (texte, repo_liste_ID_numero);
        }
        private string AvoirFamilleConjointIndex()
        {
            ListView lvChoixFamille = Application.OpenForms["GH"].Controls["lvChoixFamille"] as ListView;
            bool[] alphabette = new bool[27];
            for (int f = 0; f < 27; f++)
            {
                alphabette[f] = false;
            }
            string texte = "";
            // détermine si lettre est utiliser
            for (int f = 0; f < lvChoixFamille.Items.Count; f++)
            {
                if (lvChoixFamille.Items[f].SubItems[1].Text != "")
                {
                    char l = lvChoixFamille.Items[f].SubItems[1].Text[0];
                    int v = (int)l - 65;
                    if (v > -1 && v < 26)
                    {
                        alphabette[v] = true;
                    }
                    else
                    {
                        alphabette[26] = true;
                    }
                }
                else
                {
                    alphabette[26] = true;
                }
            }
            texte += "\t\t\t<div style=\"width:860px;border-radius:10px;background-color:#6464FF;margin-left:auto;margin-right:auto;padding:5px;\">\n";
            for (int f = 0; f < 26; f++)
            {
                if (alphabette[f])
                {
                    texte += "\t\t\t\t<a class=\"index\" href=\"m" + (char)(f + 65) + ".html\" >\n";
                    texte += "\t\t\t\t\t" + (char)(f + 65) + "\n";
                    texte += "\t\t\t\t</a>\n";
                }
                else
                {
                    texte += "\t\t\t\t<div class=\" index\">\n";
                    texte += "\t\t\t\t\t" + (char)(f + 65) + "\n";
                    texte += "\t\t\t\t</div>\n";
                }
            }
            if (alphabette[26])
            {
                texte += "\t\t\t\t<a class=\"index\" href=\"mDiver.html\" >\n";
                texte += "\t\t\t\t\t&\n";
                texte += "\t\t\t\t</a>\n";
            }
            else
            {
                texte += "\t\t\t\t<div class=\" index\">\n";
                texte += "\t\t\t\t\t&\n";
                texte += "\t\t\t\t\t</div>\n";
            }
            texte += "\t\t\t</div>\n";
            return texte;
        }
        private string AvoirFamilleConjointeIndex()
        {
            bool[] alphabette = new bool[27];
            ListView lvChoixFamille = Application.OpenForms["GH"].Controls["lvChoixFamille"] as ListView;
            for (int f = 0; f < 27; f++)
            {
                alphabette[f] = false;
            }
            string texte = "";
            // détermine si lettre est utiliser
            for (int f = 0; f < lvChoixFamille.Items.Count; f++)
            {
                if (lvChoixFamille.Items[f].SubItems[2].Text != "")
                {
                    char l = lvChoixFamille.Items[f].SubItems[2].Text[0];
                    int v = (int)l - 65;
                    if (v > -1 && v < 26)
                    {
                        alphabette[v] = true;
                    }
                    else
                    {
                        alphabette[26] = true;
                    }
                }
                else
                {
                    alphabette[26] = true;
                }
            }
            texte += "\t\t\t<div style=\"width:860px;border-radius:10px;background-color:#6464FF;margin-left:auto;margin-right:auto;padding:5px;\">\n";
            for (int f = 0; f < 26; f++)
            {
                if (alphabette[f])
                {
                    texte += "\t\t\t\t<a class=\"index\" href=\"f" + (char)(f + 65) + ".html\" >\n";
                    texte += "\t\t\t\t\t" + (char)(f + 65) + "\n";
                    texte += "\t\t\t\t</a>\n";
                }
                else
                {
                    texte += "\t\t\t\t<div class=\" index\">\n";
                    texte += "\t\t\t\t\t" + (char)(f + 65) + "\n";
                    texte += "\t\t\t\t</div>\n";
                }
            }
            if (alphabette[26])
            {
                texte += "\t\t\t\t<a class=\"index\" href=\"fDiver.html\" >\n";
                texte += "\t\t\t\t\t&\n";
                texte += "\t\t\t\t</a>\n";
            }
            else
            {
                texte += "\t\t\t\t<div class=\" index\">\n";
                texte += "\t\t\t\t\t&\n";
                texte += "\t\t\t\t</div>\n";
            }
            texte += "\t\t\t</div>\n";
            return texte;
        }
        private string Bas_Page()
        {
            string texte = "";
            texte += "\t\t\t<footer>\n";
            texte += "\t\t\t\t<br />\n";
            texte += "\t\t\t\t<hr style=\"border: 1px solid black;width:300px;margin-left:0;\" />\n";
            DateTime laDate = DateTime.Now;
            texte += "\t\t\t\tGénéré par GH (GEDCOM-Html) V0.1B, le " + laDate.ToString("dd MMMMM yyyy") + "\n";
            texte += "\t\t\t</footer>\n";
            texte += "\t\t</div><!-- fin de page-->\n";
            texte += "\t</body>\n";
            texte += "</html>\n";
            return texte;
        }
        private static string Carte(int NumeroCarte, string LATI, string LONG, string info, int tab)
        {
            if (!GH.Properties.Settings.Default.voir_carte) return "";
            string espace = Tabulation(tab); ;
            string texte = espace + "<div id=\"map" + NumeroCarte.ToString() + "\" class=\"carte\"></div>\n";
            texte += espace + "<script>\n";
            texte += espace + "\t// Initialiser Leaflet\n";
            texte += espace + "\tvar map" + NumeroCarte.ToString() + " = L.map('map" + NumeroCarte.ToString() + "').setView({lon: " + LONG + ", lat: " + LATI + "}, 7);\n";
            texte += espace + "\t// ajouter titre OpenStreetMap\n";
            texte += espace + "\tL.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 25, attribution: '&copy;<a href=\"https://openstreetmap.org/copyright\">Contribution OpenStreetMap</a>'}).addTo(map" + NumeroCarte.ToString() + ");\n";
            texte += espace + "\t// Montrer échelle en bas à gauche\n";
            texte += espace + "\tL.control.scale().addTo(map" + NumeroCarte.ToString() + ");\n";
            texte += espace + "\t// montrer le lieu sur la carte\n";
            texte += espace + "\tL.marker({lon: " + LONG + ", lat: " + LATI + "}).bindPopup('" + info + "').addTo(map" + NumeroCarte.ToString() + ");\n";
            texte += espace + "</script>\n";
            return texte;
        }
        private static string CopierObjet(string fichier, string sousDossier, string DossierSortie)
        {
            if (fichier == null) return null;
            if (fichier == "") return null;
            if (!DossierHTMLValide()) return null;
            string fichierSource = Path.GetFileName(fichier);
            string fichierDestination = fichierSource.Replace(" ", "_");
            string source = null;
            string RepertoireDestination = DossierSortie + "/" + sousDossier + "/medias/";
            if (sousDossier == "commun") RepertoireDestination = DossierSortie + "/commun/";
            if (sousDossier == "index") RepertoireDestination = DossierSortie + "/commun/";
            if (!Directory.Exists(RepertoireDestination))
            {
                MessageErreur("Le dossier de destination\r\n\r\n« " + RepertoireDestination + " »\r\n\r\nn'existe pas, corrigez dans vos paramètres.\r\n\r\n");
                return "";
            }
            if (File.Exists(fichier) && fichier != null)
            {
                source = fichier;
            }
            else
            {
                if (GH.Properties.Settings.Default.DossierMedia != "")
                {
                    source = GH.Properties.Settings.Default.DossierMedia + "/" + fichierSource;
                }
            }
            if (File.Exists(source) && fichierSource != "")
            {
                File.Copy(source, RepertoireDestination + fichierDestination, true);
                return fichierDestination;
            }
            return null;
        }
        private static (string, List<ID_numero>, List<ID_numero>, List<ID_numero>, List<ID_numero>) Date_Changement_Bloc(
            GEDCOMClass.CHANGE_DATE info,
            List<ID_numero> note_liste_ID_numero,
            List<ID_numero> citation_liste_ID_numero,
            List<ID_numero> source_liste_ID_numero,
            List<ID_numero> repo_liste_ID_numero,
            int tab, 
            bool ligne = false)
        {
            string temp;
            if (GH.Properties.Settings.Default.voir_date_changement == false) 
                return ("", note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
            if (
                info.N1_CHAN_DATE != null ||
                info.N2_CHAN_DATE_TIME != null ||
                info.N1_CHAN_NOTE_ID_liste != null
                )
            {
                
                string texte = "";
                string espace = Tabulation(tab);
                // date dernier changement
                texte += Separation("mince", tab);
                texte += espace + "<div class=\"dateChangement\" >\n";
                texte += espace + "\tDate du dernier changement,\n";
                if (ligne) texte += espace + "\t<br/>\n";
                texte += espace + "\t" + GEDCOMClass.ConvertirDateTexte(info.N1_CHAN_DATE) + " " + info.N2_CHAN_DATE_TIME + "\n";
                (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) = 
                    Avoir_note_lien(
                        info.N1_CHAN_NOTE_ID_liste,
                        note_liste_ID_numero,
                        citation_liste_ID_numero,
                        source_liste_ID_numero,
                        repo_liste_ID_numero,
                        tab + 1);
                texte += temp;
                texte += espace + "</div>\n";
                return (texte, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
            }
            else return ("", note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
        }
        private static bool DossierHTMLValide()
        {
            if (!Directory.Exists(GH.Properties.Settings.Default.DossierHTML))
            {
                MessageErreur("S.V.P. Spécifiez dans les paramêtres, le dossier des fiches HTML.");
                return false;
            }
            return true;
        }
        private (string, bool, List<ID_numero>, List<ID_numero>, List<ID_numero>, List<ID_numero>) Groupe_depot(
            List<ID_numero> note_liste_ID_numero,
            List<ID_numero> citation_liste_ID_numero,
            List<ID_numero> source_liste_ID_numero,
            List<ID_numero> repo_liste_ID_numero,
            int tab = 0)
        {
            if (repo_liste_ID_numero == null) 
                return ("", false, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
            if (repo_liste_ID_numero.Count == 0) 
                return ("", false, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
            string espace = Tabulation(tab);
            string texte = Separation("large", tab);
            string temp;
            texte += "<a id=\"groupe_depot\"></a>\n";
            texte += Groupe("debut", tab);
            texte += espace + "\t<table class=\"titre\">\n";
            texte += espace + "\t\t<tr>\n";
            texte += espace + "\t\t\t<td>\n";
            texte += espace + "\t\t\t\t<img  style=\"vertical-align:middle;heigh:64px\" src=\"..\\commun\\depot.svg\" />\n";
            if (repo_liste_ID_numero.Count == 1) texte += espace + "\t\t\t\tDépôt\n";
            else texte += espace + "\t\t\t\tDépôts\n";
            texte += espace + "\t\t\t</td>\n";
            texte += espace + "\t\t</tr>\n";
            texte += espace + "\t</table>\n";
            texte += Separation("mince", tab + 1);
            foreach (ID_numero record in repo_liste_ID_numero)
            {
                GEDCOMClass.REPOSITORY_RECORD info = GEDCOMClass.Avoir_Info_Repo(record.ID);
                {
                    if (info != null)
                    {
                        texte += espace + "\t<a id=\"depot-" + record.numero.ToString() + "\"></a>\n";
                        texte += espace + "\t<table class=\"tableau\">\n";
                        texte += espace + "\t\t<tr>\n";
                        texte += espace + "\t\t\t<td style=\"background-color:#BBB;border:2px solid #000;margin-left:5px;vertical-align:top;\" colspan=2>\n";// ne pas enlever Border.
                        texte += espace + "\t\t\t\t<span class=\"depot\">" + record.numero.ToString() + "</span> \n";
                        texte += espace + "\t\t\t\t<span style=\"display:table-cell;text-align:top; padding-left:5px\">\n";
                        texte += espace + "\t\t\t\t\t" + info.N1_NAME + "\n";


                        if (GH.Properties.Settings.Default.deboguer) texte += espace + "\t\t\t\t\t" + " {" + info.N0_ID + "}\n";
                        texte += espace + "\t\t\t\t</span>\n";
                        texte += espace + "\t\t\t</td>\n";
                        texte += espace + "\t\t</tr>\n";
                        // adresse
                        GEDCOMClass.ADDRESS_STRUCTURE InfoAdresse = info.N1_ADDR;
                        if (InfoAdresse != null)
                        {
                            texte += espace + "\t<tr>\n";
                            texte += espace + "\t\t<td  class=\"indexCol1\">\n";
                            texte += espace + "\t\t\tAdresse\n";
                            texte += espace + "\t\t</td>\n";
                            texte += espace + "\t\t<td>\n";
                            if (InfoAdresse.N0_ADDR != null) texte += espace + "\t\t\t<br/>1" + InfoAdresse.N0_ADDR + "\n";
                            if (InfoAdresse.N1_ADR1 != null) texte += espace + "\t\t\t<br/>2" + InfoAdresse.N1_ADR1 + "\n";
                            if (InfoAdresse.N1_ADR1 != null) texte += espace + "\t\t\t<br/>3" + InfoAdresse.N1_ADR2 + "\n";
                            if (InfoAdresse.N1_ADR3 != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N1_ADR3 + "\n";
                            if (InfoAdresse.N1_CITY != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N1_CITY + "\n";
                            if (InfoAdresse.N1_STAE != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N1_STAE + "\n";
                            if (InfoAdresse.N1_CTRY != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N1_CTRY + "\n";
                            if (InfoAdresse.N1_POST != null) texte += espace + "\t\t\t<br/>" + InfoAdresse.N1_POST + "\n";
                            texte += espace + "\t\t</td>\n";
                            texte += espace + "\t</tr>\n";
                        }
                        // REPO téléphone
                        if (info.N1_PHON_liste != null)
                        {
                            bool premier = true;
                            foreach (string s in info.N1_PHON_liste)
                            {
                                texte += "\t\t\t\t<tr>\n";
                                texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                                if (premier)
                                {
                                    texte += "\t\t\t\t\t\tTéléphone\n";
                                    premier = false;
                                }
                                else texte += "\t\t\t\t\t\t&nbsp;\n";
                                texte += "\t\t\t\t\t</td>\n";
                                texte += "\t\t\t\t\t<td>\n";
                                texte += "\t\t\t\t\t\t" + s + "\n";
                                texte += "\t\t\t\t\t</td>\n";
                                texte += "\t\t\t\t</tr>\n";
                            }
                        }
                        // REPO fax
                        if (info.N1_FAX_liste != null)
                        {
                            bool premier = true;
                            foreach (string s in info.N1_FAX_liste)
                            {
                                texte += "\t\t\t\t<tr>\n";
                                texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                                if (premier)
                                {
                                    texte += "\t\t\t\t\t\tTélécopieur\n";
                                    premier = false;
                                }
                                else texte += "\t\t\t\t\t\t&nbsp;\n";
                                texte += "\t\t\t\t\t</td>\n";
                                texte += "\t\t\t\t\t<td>\n";
                                texte += "\t\t\t\t\t\t" + s + "\n";
                                texte += "\t\t\t\t\t</td>\n";
                                texte += "\t\t\t\t</tr>\n";
                            }
                        }
                        // REPO courriel
                        if (info.N1_EMAIL_liste != null)
                        {
                            bool premier = true;
                            foreach (string s in info.N1_EMAIL_liste)
                            {
                                texte += "\t\t\t\t<tr>\n";
                                texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                                if (premier)
                                {
                                    texte += "\t\t\t\t\t\tCourriel\n";
                                    premier = false;
                                }
                                else texte += "\t\t\t\t\t\t&nbsp;\n";
                                texte += "\t\t\t\t\t</td>\n";
                                texte += "\t\t\t\t\t<td>\n";
                                texte += "\t\t\t\t\t\t" + s + "\n";
                                texte += "\t\t\t\t\t</td>\n";
                                texte += "\t\t\t\t</tr>\n";
                            }
                        }
                        // REPO WWW
                        if (info.N1_WWW_liste != null)
                        {
                            bool premier = true;
                            foreach (string s in info.N1_WWW_liste)
                            {
                                texte += "\t\t\t\t<tr>\n";
                                texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                                if (premier)
                                {
                                    texte += "\t\t\t\t\t\tSite Web\n";
                                    premier = false;
                                }
                                else texte += "\t\t\t\t\t\t&nbsp;\n";
                                texte += "\t\t\t\t\t</td>\n";
                                texte += "\t\t\t\t\t<td>\n";
                                texte += "\t\t\t\t\t\t" + s + "\n";
                                texte += "\t\t\t\t\t</td>\n";
                                texte += "\t\t\t\t</tr>\n";
                            }
                        }
                        // REFN
                        if (info.N1_REFN_liste != null)
                        {
                            foreach (GEDCOMClass.USER_REFERENCE_NUMBER info_REFN in info.N1_REFN_liste)
                            {
                                texte += espace + "\t<tr>\n";
                                texte += espace + "\t\t<td colspan=2>\n";
                                texte += espace + "\t\t\tNuméro de fichier d'enregistrement permanent(REFN) " + info_REFN.N0_REFN + " Type " + info_REFN.N1_TYPE + "\n";
                                texte += espace + "\t\t</td>\n";
                                texte += espace + "\t</tr>\n";

                            }
                        }
                        // RIN
                        if (info.N1_RIN != null)
                        {
                            texte += espace + "\t<tr>\n";
                            texte += espace + "\t\t<td colspan=2>\n";
                            texte += espace + "\t\t\tID d'enregistrement automatisé(RIN) " + info.N1_RIN + "\n";
                            texte += espace + "\t\t</td>\n";
                            texte += espace + "\t</tr>\n";
                        }
                        // note
                        if (info.N1_NOTE_liste_ID != null)
                        {
                            if (info.N1_NOTE_liste_ID.Count > 0)
                            {
                                texte += espace + "\t\t<tr>\n";
                                texte += espace + "\t\t\t<td style=\"border:0px solid #0e0;text-align:left;\" colspan=2>\n";
                                (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                                    Avoir_note_lien(
                                        info.N1_NOTE_liste_ID,
                                        note_liste_ID_numero,
                                        citation_liste_ID_numero,
                                        source_liste_ID_numero,
                                        repo_liste_ID_numero,
                                        tab + 4);
                                texte += temp;
                                texte += espace + "\t\t\t</td>\n";
                                texte += espace + "\t\t</tr>\n";
                            }
                        }
                        // date changement *********************************************************************************************
                        if (info.N1_CHAN != null && GH.Properties.Settings.Default.voir_date_changement)
                        {
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td style=\"border:0px solid #0e0;text-align:left;\" colspan=2>\n";
                            (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                            Date_Changement_Bloc(
                                info.N1_CHAN,
                                note_liste_ID_numero,
                                citation_liste_ID_numero,
                                source_liste_ID_numero,
                                repo_liste_ID_numero,
                                tab + 4,
                                false);
                            texte += temp;
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        texte += espace + "\t</table>\n";
                    }
                    else
                    {
                        texte += espace + "\t<a id=\"depot-" + record.numero.ToString() + "\"></a>\n";
                        texte += espace + "\t<table class=\"tableau\">\n";
                        texte += espace + "\t\t<tr>\n";
                        texte += espace + "\t\t\t<td style=\"background-color:#BBB;border:2px solid #000;margin-left:5px;vertical-align:top;\" colspan=2>\n";// ne pas enlever Border.
                        texte += espace + "\t\t\t\t<span class=\"depot\">" + record.numero.ToString() + "</span> \n";
                        texte += espace + "\t\t\t\t<span style=\"display:table-cell;text-align:top; padding-left:5px\">\n";
                        texte += espace + "\t\t\t\t\tInformation manquante dans le fichier GEDCOM\n";
                        if (GH.Properties.Settings.Default.deboguer) texte += espace + "\t\t\t\t" + " {" + record.ID + "}\n";
                        texte += espace + "\t\t\t\t</span>\n";
                        texte += espace + "\t\t\t</td>\n";
                        texte += espace + "\t\t</tr>\n";
                        texte += espace + "\t</table>\n";
                    }
                }
                texte += Separation("mince", 0);
            }
            texte += Groupe("fin", tab);
            return (texte, true, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
        }
        private (string, int, bool, List<ID_numero> ,List<ID_numero>, List<ID_numero>, List<ID_numero>) Groupe_evenement(
            List<GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE> liste,
            string date_naissance_individu,
            string date_naissace_conjoint,
            string date_naissace_conjointe,
            int numeroCarte,
            string sousDossier,
            string dossierSortie,
            List<ID_numero> note_liste_ID_numero,
            List<ID_numero> citation_liste_ID_numero,
            List<ID_numero> source_liste_ID_numero,
            List<ID_numero> repo_liste_ID_numero,
            int tab)
        {
            if (liste == null) 
                return ("", numeroCarte, false, note_liste_ID_numero, citation_liste_ID_numero,
                    source_liste_ID_numero, repo_liste_ID_numero);
            if (liste.Count == 0) 
                return ("", numeroCarte, false, note_liste_ID_numero, citation_liste_ID_numero,
                    source_liste_ID_numero, repo_liste_ID_numero);
            string espace = Tabulation(tab);
            string texte = "";
            texte += Separation("large", tab);
            texte += espace + "<a id=\"groupe_evenement\"></a>\n";
            texte += Groupe("debut", tab);
            texte += espace + "\t<table class=\"titre\">\n";
            texte += espace + "\t\t<tr>\n";
            texte += espace + "\t\t\t<td>\n";
            if(liste.Count == 1)
                texte += espace + "\t\t\t\t\tÉvénement\n";
            else
                texte += espace + "\t\t\t\t\tÉvénements\n";
            texte += espace + "\t\t\t</td>\n";
            texte += espace + "\t\t</tr>\n";
            texte += espace + "\t</table>\n";
            Liste_par_date par_date = new Liste_par_date();
            liste.Sort(par_date);
            foreach (GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE info in liste)
            {
                string temp = "";
                (temp, numeroCarte, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) = Avoir_evenement(
                        info, 
                        date_naissance_individu,
                        date_naissace_conjoint,
                        date_naissace_conjointe,
                        numeroCarte,
                        sousDossier,
                        dossierSortie,
                        note_liste_ID_numero,
                        citation_liste_ID_numero,
                        source_liste_ID_numero,
                        repo_liste_ID_numero,
                        tab + 1);
                texte += temp;
                texte += Groupe("fin", tab);
            }
            return (texte, numeroCarte, true, note_liste_ID_numero, citation_liste_ID_numero, 
                source_liste_ID_numero, repo_liste_ID_numero);
        }
        private (string, int, bool, List<ID_numero>, List<ID_numero>, List<ID_numero>, List<ID_numero>) Groupe_ordonnance(
            List<GEDCOMClass.LDS_INDIVIDUAL_ORDINANCE> liste,
            string dateNaissance,
            int numeroCarte,
            List<ID_numero> note_liste_ID_numero,
            List<ID_numero> citation_liste_ID_numero,
            List<ID_numero> source_liste_ID_numero,
            List<ID_numero> repo_liste_ID_numero,
            int tab)
        {
            if (liste == null) 
                return ("", numeroCarte, false, note_liste_ID_numero, citation_liste_ID_numero,
                    source_liste_ID_numero, repo_liste_ID_numero);
            if (liste.Count == 0) 
                return ("", numeroCarte, false, note_liste_ID_numero, citation_liste_ID_numero,
                    source_liste_ID_numero, repo_liste_ID_numero);
            string temp;
            string texte = "";
            string date;

            string espace = Tabulation(tab);
            texte += Separation("large", tab);
            texte += espace + "<a id=\"groupe_ordonnance\"></a>\n";
            texte += Groupe("debut", tab);
            texte += espace + "\t<table class=\"titre\">\n";
            texte += espace + "\t\t<tr>\n";
            texte += espace + "\t\t\t<td>\n";
            if (liste.Count == 1)
                texte += espace + "\t\t\t\t\tOrdonnance\n";
            else
                texte += espace + "\t\t\t\t\tOrdonnances\n";
            texte += espace + "\t\t\t</td>\n";
            texte += espace + "\t\t</tr>\n";
            texte += espace + "\t</table>\n";
            foreach (GEDCOMClass.LDS_INDIVIDUAL_ORDINANCE info_LDS in liste)
            {
                texte += Separation("moyen", tab);
                texte += espace + "<table class=\"tableau\">\n";
                if (info_LDS.N0_Type != null)
                {
                    texte += espace + "\t<tr>\n";
                    texte += espace + "\t\t<td class=\"cellule2LMB\" colspan=2>\n";
                    texte += espace + "\t\t\t" + info_LDS.N0_Type + "\n";
                    string age = Avoir_age(dateNaissance, info_LDS.N1_DATE);
                    if (age != "") texte += espace + "\t\t\t à l'age de " + age + "\n";
                    (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) = 
                        Avoir_citation_source_lien(
                            info_LDS.N1_SOUR_citation_liste_ID,
                            note_liste_ID_numero,
                            citation_liste_ID_numero,
                            source_liste_ID_numero,
                            repo_liste_ID_numero,
                            6);
                    texte += temp;
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t\t\t\t\t\t</tr>\n";
                }
                if (info_LDS.N1_DATE != null)
                {
                    if (GH.Properties.Settings.Default.date_longue)
                    {
                        date = GEDCOMClass.ConvertirDateTexte(info_LDS.N1_DATE);
                    }
                    else
                    {
                        date = info_LDS.N1_DATE;
                    }
                    texte += espace + "\t<tr>\n";
                    texte += espace + "\t\t<td style=\"width:70px\">\n";
                    texte += espace + "\t\t\tDate\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td>\n";
                    texte += espace + "\t\t\t" + date + "\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                if (info_LDS.N1_TEMP != null)
                {
                    texte += espace + "\t<tr>\n";
                    texte += espace + "\t\t<td>\n";
                    texte += espace + "\t\t\tTemple\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td>\n";
                    texte += espace + "\t\t\t" + info_LDS.N1_TEMP + "\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                if (info_LDS.N1_PLAC != null)
                {
                    texte += espace + "\t<tr>\n";
                    texte += espace + "\t\t<td>\n";
                    texte += espace + "\t\t\tLieu\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td>\n";
                    texte += espace + "\t\t\t" + info_LDS.N1_PLAC + "\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                if (info_LDS.N1_STAT != null)
                {
                    texte += espace + "\t<tr>\n";
                    texte += espace + "\t\t<td>\n";
                    texte += espace + "\t\t\tStatus\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t\t<td>\n";
                    texte += espace + "\t\t\t" + info_LDS.N1_STAT + "\n";
                    if (info_LDS.N2_STAT_DATE != null)
                    {
                        if (GH.Properties.Settings.Default.date_longue)
                        {
                            date = GEDCOMClass.ConvertirDateTexte(info_LDS.N2_STAT_DATE);
                        }
                        else
                        {
                            date = info_LDS.N2_STAT_DATE;
                        }

                        texte += espace + "\t\t\t En date du " + date + "\n";

                    }
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                // note source
                if (info_LDS.N1_SOUR_citation_liste_ID.Count > 0)
                {
                    texte += espace + "\t<tr>\n";
                    texte += espace + "\t\t<td colspan=2>\n";
                    (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) = 
                        Avoir_citation_source_lien(
                            info_LDS.N1_SOUR_citation_liste_ID,
                            note_liste_ID_numero,
                            citation_liste_ID_numero,
                            source_liste_ID_numero,
                            repo_liste_ID_numero,
                            6);
                    texte += temp;
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                // note ordonnance
                if (info_LDS.N1_NOTE_liste_ID.Count > 0)
                {
                    texte += espace + "\t<tr>\n";
                    texte += espace + "\t\t<td colspan=2>\n";
                    (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                    Avoir_note_lien(
                        info_LDS.N1_NOTE_liste_ID,
                        note_liste_ID_numero,
                        citation_liste_ID_numero,
                        source_liste_ID_numero,
                        repo_liste_ID_numero,
                        6);
                    texte += temp;
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
                texte += espace + "</table>\n";
            }
            return (texte, numeroCarte, true, note_liste_ID_numero, citation_liste_ID_numero,
                source_liste_ID_numero, repo_liste_ID_numero);
        }
        public void Famille(string IDFamille, bool menu, string dossierSortie)
        {
            List<ID_numero> note_liste_ID_numero = new List<ID_numero>();
            List<ID_numero> citation_liste_ID_numero = new List<ID_numero>();
            List<ID_numero> source_liste_ID_numero = new List<ID_numero>();
            List<ID_numero> repo_liste_ID_numero = new List<ID_numero>();
            List<ID_numero> temp_liste = new List<ID_numero>();
            temp_liste.Clear();
            string temp;
            TextBox Tb_Status = Application.OpenForms["GH"].Controls["Tb_Status"] as TextBox;
            Tb_Status.Text = "Génération de la fiche famille ID " + IDFamille;
            Application.DoEvents();
            string sousDossier = "familles";
            List<ID_numero> liste_SUBMITTER = new List<ID_numero>();
            int numeroSUBMITTER = 0;
            bool groupe_enfant = false;
            bool groupe_media = false;
            bool groupe_evenement;
            bool groupe_attribut;
            bool groupe_chercheur;
            bool groupe_citation = false;
            bool groupe_source = false;
            bool groupe_note = false;
            bool groupe_depot;
            int numeroCarte = 0;
            if (IDFamille == "") return;
            // dossier
            string nomFichierFamille = @dossierSortie + "/familles/" + IDFamille + ".html";
            if (!menu) nomFichierFamille = @dossierSortie + "/familles/page.html";
            if (File.Exists(nomFichierFamille))
            {
                File.Delete(nomFichierFamille);
            }
            // info famille
            GEDCOMClass.FAM_RECORD infoFamille = GEDCOMClass.Avoir_info_famille(IDFamille);
            // mariage
            // conjoint
            bool Ok;
            GEDCOMClass.INDIVIDUAL_RECORD infoHUSB;
            (Ok, infoHUSB) = GEDCOMClass.Avoir_info_individu(infoFamille.N1_HUSB);
            GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE NaissanceConjoint = new GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE();
            GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE DecesConjoint = new GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE();
            if (Ok)
            {
                (_, NaissanceConjoint) = GEDCOMClass.AvoirEvenementNaissance(infoHUSB.N1_EVENT_Liste);
                (_, DecesConjoint) = GEDCOMClass.AvoirEvenementDeces(infoHUSB.N1_EVENT_Liste);
            }
            string nomConjoint = GEDCOMClass.AvoirPremierNomIndividu(infoFamille.N1_HUSB);
            // conjointe
            GEDCOMClass.INDIVIDUAL_RECORD infoWIFE;
            (_, infoWIFE) = GEDCOMClass.Avoir_info_individu(infoFamille.N1_WIFE);
            GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE NaissanceConjointe = new GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE();
            GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE DecesConjointe = new GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE();
            if (Ok)
            {
                (_, NaissanceConjointe) = GEDCOMClass.AvoirEvenementNaissance(infoWIFE.N1_EVENT_Liste);
                (_, DecesConjointe) = GEDCOMClass.AvoirEvenementDeces(infoWIFE.N1_EVENT_Liste);
            }
            string nomConjointe = GEDCOMClass.AvoirPremierNomIndividu(infoFamille.N1_WIFE);
            // @Parent
            string texte = null;
            string IDConjointTexte = null;
            string IDConjointeTexte = null;
            if (GH.Properties.Settings.Default.VoirID == true)
            {
                if (infoFamille.N1_HUSB != "")
                    IDConjointTexte = " [" + infoFamille.N1_HUSB + "]";
                if (infoFamille.N1_WIFE != "")
                    IDConjointeTexte = " [" + infoFamille.N1_WIFE + "]";
            }
            texte += Haut_Page("../", menu);
            // restriction
            if (infoFamille.N1_RESN != null)
            {
                texte += "\t\t\t\t<div class=\"blink3\">\n";
                texte += "\t\t\t\t\tRestriction\n";
                texte += infoFamille.N1_RESN + "\n";
                texte += "\t\t\t\t</div>\n";
            }
            texte += Separation("large", 3);
            texte += "<a id=\"groupe_famille\"></a>\n";
            texte += "\t\t\t<table class=\"titre\">\n";
            texte += "\t\t\t\t<tr>\n";
            texte += "\t\t\t\t\t<td>\n";
            texte += "\t\t\t\t\t\tFamille\n";

            (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) = 
                Avoir_citation_source_lien(
                infoFamille.N1_SOUR_citation_liste_ID,
                note_liste_ID_numero,
                citation_liste_ID_numero,
                source_liste_ID_numero,
                repo_liste_ID_numero,
                6);
            texte += temp;
            if (GH.Properties.Settings.Default.VoirID == true)
            {
                texte += "\t\t\t\t\t\t<div style=\"font-size:small\">[" + IDFamille + "]</div>" + "\n";
            }
            texte += "\t\t\t\t\t</td>\n";
            texte += "\t\t\t\t</tr>\n";
            texte += "\t\t\t</table>\n";
            texte += Separation("large", 3);
            // nom parents
            texte += "\t\t\t<table class=\"tableau\">\n";
            texte += "\t\t\t\t<thead>\n";
            texte += "\t\t\t\t\t<tr>\n";
            texte += "\t\t\t\t\t\t<th class=\"cellule2LMB\" style=\"width:85px\">&nbsp;</th>\n";
            texte += "\t\t\t\t\t\t<th class=\"cellule2LMB\">Nom</th>\n";
            texte += "\t\t\t\t\t\t<th class=\"cellule2LMB date\">Naissance</th>\n";
            texte += "\t\t\t\t\t\t<th class=\"cellule2LMB date\">Décès</th>\n";
            texte += "\t\t\t\t\t</tr>\n";
            texte += "\t\t\t\t</thead>\n";
            // conjoint
            texte += "\t\t\t\t<tr>\n";
            texte += "\t\t\t\t\t<td class=\"cellule2LTF\">\n";
            if (infoFamille.N1_HUSB != "")
            {
                if (menu)
                {
                    texte += "\t\t\t\t\t<a class=\"ficheIndividu\" href=\"../individus/" + infoFamille.N1_HUSB + ".html\"></a>\n";
                }
                else
                {
                    texte += "\t\t\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                }
                texte += "\t\t\t\t\t\tPère\n";
            }
            else
            {
                texte += "\t\t\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                texte += "\t\t\t\t\t\tPère\n";
            }
            texte += "\t\t\t\t\t</td>\n";
            texte += "\t\t\t\t\t<td class=\"cellule2LTF\">\n";
            texte += "\t\t\t\t\t\t" + nomConjoint + IDConjointTexte + "\n";
            texte += "\t\t\t\t\t</td>\n";
            string date_naissance_conjoint;
            if (GH.Properties.Settings.Default.date_longue)
            {
                date_naissance_conjoint = GEDCOMClass.ConvertirDateTexte(NaissanceConjoint.N2_DATE);
            }
            else
            {
                date_naissance_conjoint = NaissanceConjoint.N2_DATE;
            }
            texte += "\t\t\t\t\t<td class=\"cellule2LTF date\">\n";
            texte += "\t\t\t\t\t\t" + date_naissance_conjoint + "\n";
            texte += "\t\t\t\t\t</td>\n";
            string date_deces_conjoint;
            if (GH.Properties.Settings.Default.date_longue)
            {
                date_deces_conjoint = GEDCOMClass.ConvertirDateTexte(DecesConjoint.N2_DATE);
            }
            else
            {
                date_deces_conjoint = DecesConjoint.N2_DATE;
            }
            texte += "\t\t\t\t\t<td class=\"cellule2LTF\" date>\n";
            texte += "\t\t\t\t\t\t" + date_deces_conjoint + "\n";
            texte += "\t\t\t\t\t</td>\n";
            texte += "\t\t\t\t</tr>\n";
            // conjointe
            texte += "\t\t\t\t<tr>\n";
            texte += "\t\t\t\t\t<td class=\"cellule2LTF\">\n";
            if (infoFamille.N1_WIFE != null)
            {
                if (menu)
                {
                    texte += "\t\t\t\t\t\t<a class=\"ficheIndividu\"  href=\"" + infoFamille.N1_WIFE + ".html\"></a>\n";
                }
                else
                {
                    texte += "\t\t\t\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                }
                texte += "\t\t\t\t\t\t\tMère\n";
            }
            else
            {
                texte += "\t\t\t\t\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                texte += "\t\t\t\t\t\t\tMère\n";
            }
            texte += "\t\t\t\t\t</td>\n";
            texte += "\t\t\t\t\t<td class=\"cellule2LTF\">\n";
            texte += "\t\t\t\t\t\t" + nomConjointe + IDConjointeTexte + "\n";
            // ordonnance
            if (infoFamille.N1_SLGS.N1_DATE != null ||
                infoFamille.N1_SLGS.N1_TEMP != null ||
                infoFamille.N1_SLGS.N1_PLAC != null ||
                infoFamille.N1_SLGS.N1_STAT != null ||
                infoFamille.N1_SLGS.N2_STAT_DATE != null
                )
            {
                texte += Separation("mince", 6);
                texte += "\t\t\t\t\t\t<table>\n";
                texte += "\t\t\t\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t\t\t\t<td class=\"cellule2LMB\">Ordonnance</td>";
                texte += "\t\t\t\t\t\t\t\t</td>";
                texte += "\t\t\t\t\t\t\t</tr>\n";
                texte += "\t\t\t\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t\t\t\t<td class=\"cellule2LMF\">\n";
                string date_SLGS;
                if (infoFamille.N1_SLGS.N1_DATE != null)
                {
                    if (GH.Properties.Settings.Default.date_longue)
                    {
                        date_SLGS = GEDCOMClass.ConvertirDateTexte(infoFamille.N1_SLGS.N1_DATE);
                    }
                    else
                    {
                        date_SLGS = infoFamille.N1_SLGS.N1_DATE;
                    }
                    texte += Separation("mince", 6);
                    texte += "\t\t\t\t\t\tDate:  " + date_SLGS;
                }
                if (infoFamille.N1_SLGS.N1_TEMP != null)
                {
                    texte += Separation("mince", 6);
                    texte += "\t\t\t\t\t\tTemple: " + infoFamille.N1_SLGS.N1_TEMP;
                }
                if (infoFamille.N1_SLGS.N1_PLAC != null)
                {
                    texte += Separation("mince", 6);
                    texte += "\t\t\t\t\t\tLieu: " + infoFamille.N1_SLGS.N1_PLAC;
                }
                if (infoFamille.N1_SLGS.N1_STAT != null)
                {
                    texte += Separation("mince", 6);
                    texte += "\t\t\t\t\t\tStatus: " + infoFamille.N1_SLGS.N1_STAT;
                }
                if (infoFamille.N1_SLGS.N2_STAT_DATE != null)
                {
                    if (GH.Properties.Settings.Default.date_longue)
                    {
                        date_SLGS = GEDCOMClass.ConvertirDateTexte(infoFamille.N1_SLGS.N2_STAT_DATE);
                    }
                    else
                    {
                        date_SLGS = infoFamille.N1_SLGS.N2_STAT_DATE;
                    }

                    texte += "&nbsp;En date du " + date_SLGS;

                }
                (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                    Avoir_note_lien(
                        infoFamille.N1_SLGS.N1_NOTE_liste_ID,
                        note_liste_ID_numero,
                        citation_liste_ID_numero,
                        source_liste_ID_numero,
                        repo_liste_ID_numero,
                        6);
                texte += temp;
                (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) = 
                    Avoir_citation_source_lien
                        (infoFamille.N1_SLGS.N1_SOUR_citation_liste_ID,
                        note_liste_ID_numero,
                        citation_liste_ID_numero,
                        source_liste_ID_numero,
                        repo_liste_ID_numero,
                        6);
                texte += temp;
                texte += "\t\t\t\t\t\t\t\t</td>";
                texte += "\t\t\t\t\t\t\t</tr>\n";
                texte += "\t\t\t\t\t\t</table>\n";
            }
            // date naissance conjointe
            texte += "\t\t\t\t\t</td class=\"cellule2LTF date\">\n";
            string date_naissance_conjointe;
            if (GH.Properties.Settings.Default.date_longue)
            {
                date_naissance_conjointe = GEDCOMClass.ConvertirDateTexte(NaissanceConjointe.N2_DATE);
            }
            else
            {
                date_naissance_conjointe = NaissanceConjointe.N2_DATE;
            }
            texte += "\t\t\t\t\t<td class=\"cellule2LTF date\">\n";
            texte += "\t\t\t\t\t\t" + date_naissance_conjointe + "\n";
            texte += "\t\t\t\t\t</td>\n";
            string date_deces_conjointe;
            if (GH.Properties.Settings.Default.date_longue)
            {
                date_deces_conjointe = GEDCOMClass.ConvertirDateTexte(DecesConjointe.N2_DATE);
            }
            else
            {
                date_deces_conjointe = DecesConjointe.N2_DATE;
            }
            texte += "\t\t\t\t\t<td class=\"cellule2LTF date\">\n";
            texte += "\t\t\t\t\t\t" + date_deces_conjointe + "\n";
            texte += "\t\t\t\t\t</td>\n";
            texte += "\t\t\t\t</tr>\n";
            texte += "\t\t\t</table>\n";

            bool OkDate = false;
            if ((infoFamille.N1_CHAN.N1_CHAN_DATE != null ||
                infoFamille.N1_CHAN.N2_CHAN_DATE_TIME !=null ||
                infoFamille.N1_CHAN.N1_CHAN_NOTE_ID_liste != null)
                 && GH.Properties.Settings.Default.voir_date_changement
                )
            {
                OkDate = true;
            }
            if (infoFamille.N1_NCHI != null ||
                infoFamille.N1_REFN_liste.Count > 0 ||
                infoFamille.N1_RIN != null ||
                infoFamille.N1_NOTE_liste_ID.Count > 0 ||
                infoFamille.N1_SUBM_liste_ID.Count > 0 ||
                OkDate
                )
            {
                texte += "\t\t\t<table class=\"tableau tableauFin\">\n";
                texte += " \t\t\t\t<tr>\n";
                texte += " \t\t\t\t\t<td>\n";

                //nombre d'enfant
                if (infoFamille.N1_NCHI != null)
                {
                    texte += "\t\t\t\t\t\t<div>\n";
                    texte += "\t\t\t\t\t\t\tNombre d'enfant " + infoFamille.N1_NCHI + "\n";
                    texte += "\t\t\t\t\t\t</div>\n";
                }
                // REFN
                if (infoFamille.N1_REFN_liste.Count > 0)
                {
                    foreach (GEDCOMClass.USER_REFERENCE_NUMBER info in infoFamille.N1_REFN_liste)
                    {
                        texte += "\t\t\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t\tNuméro de fichier d'enregistrement permanent(REFN) " + info.N0_REFN + " Type " + info.N1_TYPE + "\n";
                        texte += "\t\t\t\t\t\t</div>\n";
                    }
                }
                // RIN
                if (infoFamille.N1_RIN != null)
                {
                    texte += "\t\t\t\t\t\t<div>\n";
                    texte += "\t\t\t\t\t\tID d'enregistrement automatisé(RIN) " + infoFamille.N1_RIN + "\n";
                    texte += "\t\t\t\t\t\t</div>\n";
                }
                // Source ********************************************************************************************************
                if (infoFamille.N1_SOUR_citation_liste_ID.Count > 0)
                {
                    texte += Separation("mince", 4);
                    texte += "\t\t\t\t<div>\n";
                    (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) = 
                        Avoir_citation_source_lien(
                            infoFamille.N1_SOUR_citation_liste_ID,
                            note_liste_ID_numero,
                            citation_liste_ID_numero,
                            source_liste_ID_numero,
                            repo_liste_ID_numero,
                            6);
                    texte += temp;
                    texte += "\t\t\t\t</div>\n";
                }
                // note ********************************************************************************************************
                if (infoFamille.N1_NOTE_liste_ID.Count > 0)
                {
                    texte += Separation("mince", 4);
                    texte += "\t\t\t\t<div>\n";
                    (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                        Avoir_note_lien(
                            infoFamille.N1_NOTE_liste_ID,
                            note_liste_ID_numero,
                            citation_liste_ID_numero,
                            source_liste_ID_numero,
                            repo_liste_ID_numero,
                            5);
                    texte += temp;
                    texte += "\t\t\t\t</div>\n";
                }
                if(infoFamille.N1_SUBM_liste_ID.Count > 0 && GH.Properties.Settings.Default.voir_chercheur)
                {
                    texte += Separation("mince", 5);
                    texte += "\t\t\t\t\t<div>\n";
                    texte += "\t\t\t\t\t\t" + " Voir chercheur ";
                    string texteTemp = null;
                    foreach (string ID in infoFamille.N1_SUBM_liste_ID)
                    {
                        if (liste_SUBMITTER.Count == 0)
                        {
                            numeroSUBMITTER = 1;
                            ID_numero info_ANCI = new ID_numero
                            {
                                numero = numeroSUBMITTER,
                                ID = ID
                            };
                            liste_SUBMITTER.Add(info_ANCI);
                            texteTemp += "<a class=\"chercheur\" href=\"#RefSubmitter" + numeroSUBMITTER.ToString() + "\">" + numeroSUBMITTER.ToString() + "</a>" + ", ";
                            numeroSUBMITTER++;
                        }
                        else
                        {
                            bool existe = false;
                            foreach (ID_numero IDListe in liste_SUBMITTER)
                            {
                                if (ID == IDListe.ID)
                                {
                                    existe = true;
                                    numeroSUBMITTER = IDListe.numero;
                                }
                            }
                            if (existe)
                            {
                                texteTemp += "<a class=\"chercheur\" href=\"#RefSubmitter" + numeroSUBMITTER.ToString() + "\">" + numeroSUBMITTER.ToString() + "</a>" + ", ";
                            }
                            else
                            {
                                // trouver nouveau numero
                                numeroSUBMITTER = liste_SUBMITTER.Count + 1;
                                ID_numero info_ANCI = new ID_numero
                                {
                                    numero = numeroSUBMITTER,
                                    ID = ID
                                };
                                liste_SUBMITTER.Add(info_ANCI);
                                texteTemp += "<a class=\"chercheur\" href=\"#RefSubmitter" + numeroSUBMITTER.ToString() + "\">" + numeroSUBMITTER.ToString() + "</a>" + ", ";
                            }
                        }
                    }
                    texte += texteTemp.TrimEnd(' ', ',') + ".";
                    texte += "\t\t\t\t\t</div>\n";
                }
                // date changement *********************************************************************************************
                (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) = 
                    Date_Changement_Bloc(
                        infoFamille.N1_CHAN, 
                        note_liste_ID_numero, 
                        citation_liste_ID_numero, 
                        source_liste_ID_numero,
                        repo_liste_ID_numero,
                        6, 
                        false);
                texte += temp;
                texte += "\t\t\t\t\t</td>\n";
                texte += " \t\t\t\t</tr>\n";
                texte += "\t\t\t</table>\n";
            }

            // @enfant *****************************************************************************************************
            //List<string> listIDEnfant = new List<string>();
            List<string> listIDEnfant = GEDCOMClass.AvoirListIDEnfant(IDFamille);
            if (listIDEnfant.Count() > 0)
            {
                texte += Separation("large", 3);
                texte += "<a id=\"groupe_enfant\"></a>\n";
                groupe_enfant = true;
                texte += Groupe("debut", 3);
                texte += "\t\t\t\t<table class=\"titre\">\n";
                texte += "\t\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t\t<td>\n";
                texte += "\t\t\t\t\t\t\tEnfant\n";
                texte += "\t\t\t\t\t\t</td>\n";
                texte += "\t\t\t\t\t</tr>\n";
                texte += "\t\t\t\t</table>\n";
                texte += Separation("mince", 4);
                texte += "\t\t\t\t<!-- Table enfant -->\n";
                texte += "\t\t\t\t<table class=\"tableau\">\n";
                texte += "\t\t\t\t\t<thead>\n";
                texte += "\t\t\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t\t\t<th class=\"listeEnfant\" style='width:85px'>Voir</th>\n";
                texte += "\t\t\t\t\t\t\t<th class=\"listeEnfant\">Non</th>\n";
                texte += "\t\t\t\t\t\t\t<th class=\"listeEnfant date\">Naisannce</th>\n";
                texte += "\t\t\t\t\t\t\t<th class=\"listeEnfant date\">Décès</th>\n";
                texte += "\t\t\t\t\t\t</tr>\n";
                texte += "\t\t\t\t\t</thead>\n";
                string txtIDEfant;
                foreach (string IDEnfant in listIDEnfant)
                {
                    if (IDEnfant != null)
                    {
                        if (GH.Properties.Settings.Default.VoirID)
                        {
                            txtIDEfant = " [" + IDEnfant + "]";
                        }
                        else
                        {
                            txtIDEfant = null;
                        }
                        GEDCOMClass.INDIVIDUAL_RECORD infoEnfant;
                        (Ok, infoEnfant) = GEDCOMClass.Avoir_info_individu(IDEnfant);
                        bool OkNEnfant;
                        GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE NaissanceEnfant;
                        (OkNEnfant, NaissanceEnfant) = GEDCOMClass.AvoirEvenementNaissance(infoEnfant.N1_EVENT_Liste);
                        bool OkDEnfant;
                        GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE DecesEnfant;
                        (OkDEnfant, DecesEnfant) = GEDCOMClass.AvoirEvenementDeces(infoEnfant.N1_EVENT_Liste);
                        string nomEnfant = GEDCOMClass.AvoirPremierNomIndividu(IDEnfant);
                        List<GEDCOMClass.PERSONAL_NAME_STRUCTURE> InfoNomEnfant;
                        (_, InfoNomEnfant) = GEDCOMClass.AvoirListeNom(IDEnfant);
                        texte += "\t\t\t\t\t<tr class=\"listeEnfant\">\n";
                        texte += "\t\t\t\t\t\t<td class=\"listeEnfant\">\n";
                        if (IDEnfant != null)
                        {
                            if (menu)
                            {
                                texte += "\t\t\t\t\t\t\t<a class=\"ficheIndividu\"  href=\"../individus/" + 
                                    IDEnfant + ".html\"></a>\n";
                            }
                            else
                            {
                                texte += "\t\t\t\t\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                            }
                        }
                        texte += "\t\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t\t<td class=\"listeEnfant\">\n";
                        texte += "\t\t\t\t\t\t\t" + nomEnfant + txtIDEfant + "\n";
                        string adoptionID = GEDCOMClass.AvoirIDAdoption(IDEnfant);
                        if (adoptionID != null)
                        {
                            GEDCOM.GEDCOMClass.INDIVIDUAL_RECORD infoIndividuEnfant;
                            (Ok, infoIndividuEnfant) = GEDCOMClass.Avoir_info_individu(IDEnfant);
                            string pereIDAdoption = GEDCOMClass.Avoir_famille_conjoint_ID(adoptionID);
                            string nomPereAdoption = GEDCOMClass.AvoirPrenomPatronymeIndividu(pereIDAdoption);
                            string mereIDAdoption = GEDCOMClass.Avoir_famille_conjointe_ID(adoptionID);
                            string nomMereAdoption = GEDCOMClass.AvoirPrenomPatronymeIndividu(mereIDAdoption);
                            texte += "\t\t\t\t\t\t\t< br />Famille de " + nomPereAdoption + " et " + nomMereAdoption + "\n";
                        }
                        texte += "\t\t\t\t\t\t</td>\n";
                        string date_naissance_enfant;
                        if (GH.Properties.Settings.Default.date_longue)
                        {
                            date_naissance_enfant = GEDCOMClass.ConvertirDateTexte(NaissanceEnfant.N2_DATE);
                        }
                        else
                        {
                            date_naissance_enfant = NaissanceEnfant.N2_DATE;
                        }
                        texte += "\t\t\t\t\t\t<td class=\"listeEnfant\">\n";
                        texte += "\t\t\t\t\t\t\t" + date_naissance_enfant + "\n";
                        texte += "\t\t\t\t\t\t</td>\n";
                        string date_deces_enfant;
                        if (GH.Properties.Settings.Default.date_longue)
                        {
                            date_deces_enfant = GEDCOMClass.ConvertirDateTexte(DecesEnfant.N2_DATE);
                        }
                        else
                        {
                            date_deces_enfant = DecesEnfant.N2_DATE;
                        }
                        texte += "\t\t\t\t\t\t<td class=\"listeEnfant\">\n";
                        texte += "\t\t\t\t\t\t\t" + date_deces_enfant + "\n";
                        texte += "\t\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t</tr>\n";
                    }
                }
                if (listIDEnfant.Count == 0)
                {
                    for (int f = 0; f < 2; f++)
                    {
                        texte += "    \t\t\t\t<tr>\n";
                        texte += "                        <td class=\"listeEnfant\">\n";
                        texte += "                            <a class=\"ficheIndividuGris\"></a>\n";
                        texte += "    \t\t\t\t\t</td>\n";
                        texte += "                        <td class=\"listeEnfant\">\n";
                        texte += "    \t\t\t\t\t</td>\n";
                        texte += "                        <td class=\"listeEnfant\">\n";
                        texte += "    \t\t\t\t\t</td>\n";
                        texte += "                        <td class=\"listeEnfant\">\n";
                        texte += "    \t\t\t\t\t</td>\n";
                        texte += "    \t\t\t\t<tr>\n";
                    }
                }
                texte += "                </table>\n";
                texte += Groupe("fin", 3);
            }
            // média *******************************************************************************************************
            if (GH.Properties.Settings.Default.voir_media == true && infoFamille.N1_OBJE_liste.Count > 0)
            {
                if (infoFamille.N1_OBJE_liste != null)
                {

                    texte += Separation("large", 3);
                    texte += "<a id=\"groupe_media\"></a>\n";
                    groupe_media = true;
                    texte += Groupe("debut", 3);
                    texte += "\t\t\t\t<table class=\"titre\">\n";
                    texte += "\t\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t\t<td>\n";
                    if (infoFamille.N1_OBJE_liste.Count == 1) texte += "\t\t\t\t\t\t\tMédia\n";
                    else texte += "\t\t\t\t\t\t\tMédias\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t</tr>\n";
                    texte += "\t\t\t\t</table>\n";
                    texte += Separation("mince", 4);
                    texte += "\t\t\t\t<table class=\"tableauMedia\">\n";
                    texte += "\t\t\t\t\t<tr>\n";
                    int cRanger = 0;
                    foreach (string IDMedia in infoFamille.N1_OBJE_liste)
                    {
                        GEDCOMClass.MULTIMEDIA_RECORD mediaInfo = GEDCOMClass.AvoirMedia(IDMedia);
                        texte += "\t\t\t\t\t\t<td class=\"tableauMedia\">\n";
                        (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) = Objet(
                            mediaInfo, "familles", dossierSortie,
                            note_liste_ID_numero,
                            citation_liste_ID_numero,
                            source_liste_ID_numero,
                            repo_liste_ID_numero,
                            6);
                        texte += temp;
                        texte += "\t\t\t\t\t\t</td>\n";
                        cRanger++;
                        if (cRanger % 3 == 0)
                        {
                            texte += "\t\t\t\t\t</tr>\n";
                            texte += "\t\t\t\t\t<tr>\n";
                        }
                    }
                    if (cRanger % 3 == 1)
                    {
                        texte += "\t\t\t\t\t\t<td class=\"tableauMedia\">\n";
                        texte += "\t\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t\t<td class=\"tableauMedia\">\n";
                        texte += "\t\t\t\t\t\t</td>\n";
                    }
                    if (cRanger % 3 == 2)
                    {
                        texte += "\t\t\t\t\t\t<td class=\"tableauMedia\">\n";
                        texte += "\t\t\t\t\t\t</td>\n";
                    }
                    texte += "\t\t\t\t\t</tr>\n";
                    texte += "\t\t\t\t</table>\n";
                    texte += Groupe("fin", 3);
                }
            }
            //@Événement ****************************************************************************************************************************
            (temp, _, groupe_evenement, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                Groupe_evenement(
                    infoFamille.N1_EVENT_Liste,
                    "", 
                    NaissanceConjoint.N2_DATE, 
                    NaissanceConjointe.N2_DATE, 
                    numeroCarte, 
                    sousDossier, 
                    dossierSortie,
                    note_liste_ID_numero,
                    citation_liste_ID_numero,
                    source_liste_ID_numero,
                    repo_liste_ID_numero,
                    0);
            texte += temp;
            //@Attribut *************************************************************************************
            (temp, _, groupe_attribut, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                Groupe_attribut(
                    infoFamille.N1_ATTRIBUTE_liste, 
                    numeroCarte, 
                    sousDossier, 
                    dossierSortie,
                    note_liste_ID_numero,
                    citation_liste_ID_numero,
                    source_liste_ID_numero,
                    repo_liste_ID_numero,
                    0);
            texte += temp;
            //@chercheur Groupe
            (temp, groupe_chercheur, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                Groupe_chercheur(
                    liste_SUBMITTER, 
                    dossierSortie, 
                    "individus",
                    note_liste_ID_numero,
                    citation_liste_ID_numero,
                    source_liste_ID_numero,
                    repo_liste_ID_numero,
                    3);
            texte += temp;
            // @Citation **************************************************************************************************************
            if (citation_liste_ID_numero.Count() > 0)
            {
                (temp, groupe_citation, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                    Groupe_citation(
                        //repo_liste_ID_numero,
                        dossierSortie,
                        "familles",
                        note_liste_ID_numero,
                        citation_liste_ID_numero,
                        source_liste_ID_numero,
                        repo_liste_ID_numero,
                        3);
                texte += temp;
            }
            // @Source **************************************************************************************************************
            
            if (source_liste_ID_numero.Count() > 0)
            {
                (temp, groupe_source, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                    Groupe_source
                        (//liste_SOURCE_CITATION_plus,
                        dossierSortie,
                        "familles",
                        note_liste_ID_numero,
                        citation_liste_ID_numero,
                        source_liste_ID_numero,
                        repo_liste_ID_numero,
                        3);
                texte += temp;
            }
            // @Depot Groupe **********************************************************************************************
            (temp, groupe_depot, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                Groupe_depot(
                    //liste_REPOSITORY_RECORDPlus,
                    note_liste_ID_numero,
                    citation_liste_ID_numero,
                    source_liste_ID_numero,
                    repo_liste_ID_numero,
                    3);
            texte += temp;

            // @ Group note **********************************************************************************************
            if (note_liste_ID_numero.Count() > 0)
            {
                (temp, groupe_note, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) = 
                    Groupe_note("famille", note_liste_ID_numero, citation_liste_ID_numero, 
                    source_liste_ID_numero, repo_liste_ID_numero, 3);
                texte += temp;
            }
            temp_liste = note_liste_ID_numero;
            temp_liste.Clear();
            temp_liste = citation_liste_ID_numero;
            temp_liste.Clear();
            temp_liste = source_liste_ID_numero;
            temp_liste.Clear();
            temp_liste = repo_liste_ID_numero;
            temp_liste.Clear();
            //@menu hamburger
            texte += "\t\t\t\t<div class=\"hamburger\">\n";
            texte += "\t\t\t\t\t<button class=\"boutonHamburger\"></button>\n";
            texte += "\t\t\t\t\t<div class=\"hamburger-contenu\">\n";
            texte += "\t\t\t\t\t\t<a href=\"#groupe_famille\">Famille</a>\n";
            if (groupe_enfant) texte += "\t\t\t\t\t\t<a href=\"#groupe_enfant\">Enfant</a>\n";
            if (groupe_media) texte += "\t\t\t\t\t\t<a href=\"#groupe_media\">Média</a>\n";
            if (groupe_evenement) texte += "\t\t\t\t\t\t<a href=\"#groupe_evenement\">Événement</a>\n";
            if (groupe_attribut) texte += "\t\t\t\t\t\t<a href=\"#groupe_attribut\">Attribut</a>\n";
            if (groupe_chercheur) texte += "\t\t\t\t\t\t<a href=\"#groupe_chercheur\">Chercheur</a>\n";
            if (groupe_citation) texte += "\t\t\t\t\t\t<a href=\"#groupe_citation\">Citation</a>\n";
            if (groupe_source) texte += "\t\t\t\t\t\t<a href=\"#groupe_source\">Source</a>\n";
            if (groupe_depot) texte += "\t\t\t\t\t\t<a href=\"#groupe_depot\">Dépot</a>\n";
            if (groupe_note) texte += "\t\t\t\t\t\t<a href=\"#groupe_note\">Note</a>\n";
            texte += "\t\t\t\t\t</div>\n";
            texte += "\t\t\t\t</div>\n";
            texte += Bas_Page();
            try
            {
                System.IO.File.WriteAllText(nomFichierFamille, texte);
            }
            catch (Exception msg)
            {
                MessageBox.Show("Ne peut pas écrire le fichier" + nomFichierFamille + ".\r\n\r\n" + msg.Message, "Erreur HF2870 Problème ?",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning);
            }
        }
        private static string Groupe(string g, int tab)
        {
            string espace = Tabulation(tab);
            if (g == "debut") return espace + "<div class=\"grouper\"><!-- Début du groupe -->\n";
            if (g == "fin") return espace + "</div><!-- Fin du groupe -->\n";
            return "";
        }
        private string Haut_Page(string origine, bool menu)
        {
            string texte = "<!DOCTYPE html>\n";
            texte += "<html lang=\"fr\" style=\"background-color:#FFF;\">\n";
            texte += "\t<head>\n";
            texte += "\t\t<meta charset = 'UTF-8' />\n";
            texte += "<title>GH</title>\n";
            texte += "\t\t<link rel=\"stylesheet\" href=\"" + origine + "commun/dapam.css\" type=\"text/css\" />\n";
            texte += "\t\t<link rel=\"stylesheet\" href=\"" + origine + "commun/leaflet.css\" type=\"text/css\" />\n";
            texte += "\t\t<script  src=\"../commun/modal.js\"></script>\n";
            texte += "\t\t<script  src=\"../commun/leaflet.js\"></script>\n";
            texte += "\t</head>\n";
            texte += "\t<body>\n";
            texte += "\t\t<div id=\"modal-arriere\">\n";
            texte += "\t\t\t<div id=\"modal-image\">\n";
            texte += "\t\t\t</div>\n";
            texte += "\t\t</div>\n";
            if (menu)
            {
                texte += "\t\t<header>\n";
                texte += "\t\t\t<div class=\"navbar\">\n";
                texte += "\t\t\t\t<a class=\"poussoir\" href=\"" + origine + "index.html\">\n";
                texte += "\t\t\t\t\t<img style=\"height:40px\" src=\"" + origine + "commun/accueil.svg\" alt=\"\" />\n";
                texte += "\t\t\t\t\tAccueil\n";
                texte += "\t\t\t\t</a>\n";
                texte += "\t\t\t\t<a class=\"poussoir\" href=\"" + origine + "individus/index.html\">\n";
                texte += "\t\t\t\t\t<img style=\"height:40px\" src=\"" + origine + "commun/neutre.svg\" alt=\"\" />\n";
                texte += "\t\t\t\t\tIndividu\n";
                texte += "\t\t\t\t</a>\n";
                texte += "\t\t\t\t<a class=\"poussoir\" href=\"" + origine + "familles/indexConjoint.html\">\n";
                texte += "\t\t\t\t\t<img style=\"height:40px\" src=\"" + origine + "commun/familleConjoint.svg\" alt=\"\" />\n";
                texte += "\t\t\t\t\tFamille\n";
                texte += "\t\t\t\t\t<img style =\"height:40px\" src=\"" + origine + "commun/male.svg\" alt=\"\" />\n";
                texte += "\t\t\t\t</a>\n";
                texte += "\t\t\t\t<a class=\"poussoir\" href=\"" + origine + "familles/indexConjointe.html\">\n";
                texte += "\t\t\t\t\t<img style=\"height:40px\" src=\"" + origine + "commun/familleConjointe.svg\" alt=\"\" />\n";
                texte += "\t\t\t\t\tFamille\n";
                texte += "\t\t\t\t\t<img style =\"height:40px\" src=\"" + origine + "commun/femelle.svg\" alt=\"\" />\n";
                texte += "\t\t\t\t</a>\n";
                texte += "\t\t\t</div>\n";
                texte += "\t\t</header>\n";
            }
            texte += "\t\t<div class=\"page\"><!-- haut de page -->\n";
            return texte;
        }
        public void Index(string fichierGEDCOM, string nombreIndividu, string nombreFamille, string dossierSortie)
        {
            List<ID_numero> note_liste_ID_numero = new List<ID_numero>();
            List<ID_numero> citation_liste_ID_numero = new List<ID_numero>();
            List<ID_numero> source_liste_ID_numero = new List<ID_numero>();
            List<ID_numero> temp_liste = new List<ID_numero>();
            temp_liste.Clear();
            List<ID_numero> repo_liste_ID_numero = new List<ID_numero>();
            GEDCOMClass.HEADER InfoGEDCOM = GEDCOMClass.AvoirInfoGEDCOM();
            GEDCOMClass.SUBMISSION_RECORD Info_SUBMISSION_RECORD = GEDCOMClass.AvoirInfoSUBMISSION_RECORD();
            List<ID_numero> liste_SUBMITTER = new List<ID_numero>();
            string nomFichier = @dossierSortie + "/index.html";
            string texte = "";
            string temp;
            texte += Haut_Page("", true);
            int numero_chercheur = 0;
            TextBox Tb_Status = Application.OpenForms["GH"].Controls["Tb_Status"] as TextBox;
            Tb_Status.Text = "Génération de la page couverture";
            Application.DoEvents();
            // GEDCOM ******************************************************************************************************************************
            {
                texte += Separation("large", 3);
                texte += "\t\t\t<table class=\"titre\">\n";
                texte += "\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t<td>\n";
                texte += "\t\t\t\t\t\tGEDCOM\n";
                texte += "\t\t\t\t\t</td>\n";
                texte += "\t\t\t\t</tr>\n";
                texte += "\t\t\t</table>\n";
                texte += Separation("mince", 3);
                texte += "\t\t\t<table class=\"tableau\">\n";
                // version
                if (InfoGEDCOM.N2_GEDC_VERS != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                    texte += "\t\t\t\t\t\tVersion" + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + InfoGEDCOM.N2_GEDC_VERS + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // Fichier original
                if (InfoGEDCOM.N1_FILE != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tFichier original" + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + InfoGEDCOM.N1_FILE + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // Fichier
                if (fichierGEDCOM != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tFichier" + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + fichierGEDCOM + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // date heure
                if (InfoGEDCOM.N1_DATE != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tCréer le\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + InfoGEDCOM.N1_DATE + " " + InfoGEDCOM.N2_DATE_TIME + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                if (InfoGEDCOM.N1_SUBM_liste_ID != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td colspan=2>\n";
                    texte += "\t\t\t\t\tVoir chercheur \n";
                    string texteTemp = "";
                    {
                        foreach (string ID in InfoGEDCOM.N1_SUBM_liste_ID)
                        {
                            if (liste_SUBMITTER.Count == 0)
                            {
                                numero_chercheur = 1;
                                ID_numero info = new ID_numero
                                {
                                    numero = numero_chercheur,
                                    ID = ID
                                };
                                liste_SUBMITTER.Add(info);
                                texteTemp += "<a class=\"chercheur\" href=\"#RefSubmitter" + numero_chercheur.ToString() + "\">" +
                                    numero_chercheur.ToString() + "</a>" + ", ";
                                numero_chercheur++;
                            }
                            else
                            {
                                bool existe = false;
                                foreach (ID_numero IDListe in liste_SUBMITTER)
                                {
                                    if (ID == IDListe.ID)
                                    {
                                        existe = true;
                                        numero_chercheur = IDListe.numero;
                                    }
                                }
                                if (existe)
                                {
                                    texteTemp += "<a class=\"chercheur\" href=\"#RefSubmitter" + numero_chercheur.ToString() +
                                        "\">" + numero_chercheur.ToString() + "</a>" + ", ";
                                }
                                else
                                {
                                    // trouver nouveau numero
                                    numero_chercheur = liste_SUBMITTER.Count + 1;
                                    ID_numero info = new ID_numero
                                    {
                                        numero = numero_chercheur,
                                        ID = ID
                                    };
                                    liste_SUBMITTER.Add(info);
                                    texteTemp += "<a class=\"chercheur\" href=\"#RefSubmitter" + numero_chercheur.ToString() +
                                        "\">" + numero_chercheur.ToString() + "</a>" + ", ";
                                }
                            }
                        }
                    }
                    texte += texteTemp.TrimEnd(' ', ',');
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                    texte += "\t\t\t</tr>\n";
                }



                //char set
                if (InfoGEDCOM.N1_CHAR != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tCaractère set" + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + InfoGEDCOM.N1_CHAR + "\n";
                    if (InfoGEDCOM.N2_CHAR_VERS != "") texte += "\t\t\t\t\t\tVersion " + InfoGEDCOM.N2_CHAR_VERS + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // language
                if (InfoGEDCOM.N1_LANG != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tLangue" + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + InfoGEDCOM.N1_LANG + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // place
                if (InfoGEDCOM.N2_PLAC_FORM != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tPlace" + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + InfoGEDCOM.N2_PLAC_FORM + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // copyright
                if (InfoGEDCOM.N1_COPR != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tCopyright\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + InfoGEDCOM.N1_COPR + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                if (nombreIndividu != "00")
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tNombre d'individu\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + nombreIndividu + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                if (nombreFamille != "00")
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tNombre de famille\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + nombreFamille + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // destination
                if (InfoGEDCOM.N1_DEST != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tRéception par\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + InfoGEDCOM.N1_DEST + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // date transmission
                if (InfoGEDCOM.N1_DATE != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tDate transmission\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    string date;
                    if (GH.Properties.Settings.Default.date_longue)
                    {
                        date = GEDCOMClass.ConvertirDateTexte(InfoGEDCOM.N1_DATE);
                    }
                    else
                    {
                        date = InfoGEDCOM.N1_DATE;
                    }
                    texte += "\t\t\t\t\t\t" + date + " à " + InfoGEDCOM.N2_DATE_TIME + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // note GEDCOM ****************************************************************************
                if (InfoGEDCOM.N1_NOTE_liste_ID != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td colspan=2>\n";
                    (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                        Avoir_note_lien(
                            InfoGEDCOM.N1_NOTE_liste_ID,
                            note_liste_ID_numero,
                            citation_liste_ID_numero,
                            source_liste_ID_numero,
                            repo_liste_ID_numero,
                            5);
                    texte += temp;
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                texte += "\t\t\t</table>\n";
            }
            // Information d'envoie ************************************************************************
            {
                if (InfoGEDCOM.N1_SUBN != null)
                {
                    texte += Separation("large", 3);
                    texte += Groupe("debut", 3);
                    texte += "\t\t\t<table class=\"titre\">\n";
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tInformation d'envoie\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                    texte += "\t\t\t</table>\n";

                    texte += Separation("mince", 3);
                    texte += "\t\t\t<table class=\"tableau\">\n";
                    if (Info_SUBMISSION_RECORD.N1_FAMF != null)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td class=\"indexCol1\" style=\"width:170px\">\n";
                        texte += "\t\t\t\t\t\tNom du fichier familial\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\t" + Info_SUBMISSION_RECORD.N1_FAMF + "\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                    if (Info_SUBMISSION_RECORD.N1_TEMP != null)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                        texte += "\t\t\t\t\t\tCode du temple\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\t" + Info_SUBMISSION_RECORD.N1_TEMP + "\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                    if (Info_SUBMISSION_RECORD.N1_ANCE != null)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                        texte += "\t\t\t\t\t\tGénération d'ancêtre\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\t" + Info_SUBMISSION_RECORD.N1_ANCE + "\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                    if (Info_SUBMISSION_RECORD.N1_DESC != null)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                        texte += "\t\t\t\t\t\tGénération de descendant\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\t" + Info_SUBMISSION_RECORD.N1_DESC + "\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                    if (Info_SUBMISSION_RECORD.N1_ORDI != null)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                        texte += "\t\t\t\t\t\tProcessus d'ordonnance\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\t" + Info_SUBMISSION_RECORD.N1_ORDI + "\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                    if (Info_SUBMISSION_RECORD.N1_RIN != null)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                        texte += "\t\t\t\t\t\tID d'enregistrement automatisé(RIN) \n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\t" + Info_SUBMISSION_RECORD.N1_RIN + "\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                    // sousmit par
                    if (Info_SUBMISSION_RECORD.N1_SUBM_liste_ID != null)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                        texte += "\t\t\t\t\t Soumit par le chercheur \n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        string texteTemp = "";

                        {
                            foreach (string ID in Info_SUBMISSION_RECORD.N1_SUBM_liste_ID)
                            {
                                if (liste_SUBMITTER.Count == 0)
                                {
                                    numero_chercheur = 1;
                                    ID_numero info = new ID_numero
                                    {
                                        numero = numero_chercheur,
                                        ID = ID
                                    };
                                    liste_SUBMITTER.Add(info);
                                    texteTemp += "<a class=\"chercheur\" href=\"#RefSubmitter" + numero_chercheur.ToString() +
                                        "\">" + numero_chercheur.ToString() + "</a>" + ", ";
                                    numero_chercheur++;
                                }
                                else
                                {
                                    bool existe = false;
                                    foreach (ID_numero IDListe in liste_SUBMITTER)
                                    {
                                        if (ID == IDListe.ID)
                                        {
                                            existe = true;
                                            numero_chercheur = IDListe.numero;
                                        }
                                    }
                                    if (existe)
                                    {
                                        texteTemp += "<a class=\"chercheur\" href=\"#RefSubmitter" + numero_chercheur.ToString() +
                                            "\">" + numero_chercheur.ToString() + "</a>" + ", ";
                                    }
                                    else
                                    {
                                        // trouver nouveau numero
                                        numero_chercheur = liste_SUBMITTER.Count + 1;
                                        ID_numero info = new ID_numero
                                        {
                                            numero = numero_chercheur,
                                            ID = ID
                                        };
                                        liste_SUBMITTER.Add(info);
                                        texteTemp += "<a class=\"chercheur\" href=\"#RefSubmitter" + numero_chercheur.ToString() + "\">" +
                                            numero_chercheur.ToString() + "</a>" + ", ";
                                    }
                                }
                            }
                        }
                        texte += texteTemp.TrimEnd(' ', ',');
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                        texte += "\t\t\t</tr>\n";
                    }
                    // note  du sumiteur *******************************************************************
                    if (InfoGEDCOM.N1_NOTE_liste_ID != null)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td colspan=2>\n";
                        (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                            Avoir_note_lien(
                                Info_SUBMISSION_RECORD.N1_NOTE_liste_ID,
                                note_liste_ID_numero,
                                citation_liste_ID_numero,
                                source_liste_ID_numero,
                                repo_liste_ID_numero,
                                5);
                        texte += temp;
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                    // si changement de date
                    GEDCOMClass.CHANGE_DATE N1_CHAN = Info_SUBMISSION_RECORD.N1_CHAN;
                    if (N1_CHAN != null && GH.Properties.Settings.Default.voir_date_changement)
                    {
                        if (N1_CHAN.N1_CHAN_DATE != null)
                        {
                            (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                                Date_Changement_Bloc(
                                    N1_CHAN,
                                    note_liste_ID_numero,
                                    citation_liste_ID_numero,
                                    source_liste_ID_numero,
                                    repo_liste_ID_numero,
                                    6,
                                    false);
                            texte += "\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t<td colspan=2>\n";
                            texte += temp;
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t</tr>\n";
                        }
                    }
                    texte += "\t\t\t</table>\n";
                    texte += Groupe("fin", 3);
                }
            }
            // Logiciel ****************************************************************************************************************************
            if (InfoGEDCOM.N1_SOUR != null)
            {
                texte += Separation("large", 3);
                texte += Groupe("debut", 3);
                // titre Logiciel
                texte += "\t\t\t<table class=\"titre\">\n";
                texte += "\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t<td>\n";
                texte += "\t\t\t\t\t\tLogiciel\n";
                texte += "\t\t\t\t\t</td>\n";
                texte += "\t\t\t\t</tr>\n";
                texte += "\t\t\t</table>\n";
                texte += Separation("mince", 3);
                texte += "\t\t\t<table class=\"tableau\">\n";
                // SOUR
                if (InfoGEDCOM.N1_SOUR != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                    texte += "\t\t\t\t\t\tID système\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + InfoGEDCOM.N1_SOUR + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // version
                if (InfoGEDCOM.N2_SOUR_VERS != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\tVersion\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + InfoGEDCOM.N2_SOUR_VERS + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }
                // NANE
                if (InfoGEDCOM.N2_SOUR_NAME != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                    texte += "\t\t\t\t\t\tGénérer par\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + InfoGEDCOM.N2_SOUR_NAME + "\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                }

                // corporation *************************************************************************************************************************
                if (InfoGEDCOM.N2_SOUR_CORP != null)
                {
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                    texte += "\t\t\t\t\t\t\n";
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t<td>\n";
                    texte += "\t\t\t\t\t\t" + InfoGEDCOM.N2_SOUR_CORP + "\n";
                    GEDCOMClass.ADDRESS_STRUCTURE adresseCORP = InfoGEDCOM.N3_SOUR_CORP_ADDR;
                    if (adresseCORP != null)
                    {
                        if (adresseCORP.N0_ADDR != null) texte += "\t\t\t\t\t\t<br/>" + adresseCORP.N0_ADDR + "\n";
                        if (adresseCORP.N1_ADR1 != null) texte += "\t\t\t\t\t\t<br/>" + adresseCORP.N1_ADR1 + "\n";
                        if (adresseCORP.N1_ADR1 != null) texte += "\t\t\t\t\t\t<br/>" + adresseCORP.N1_ADR2 + "\n";
                        if (adresseCORP.N1_ADR3 != null) texte += "\t\t\t\t\t\t<br/>" + adresseCORP.N1_ADR3 + "\n";
                        if (adresseCORP.N1_CITY != null) texte += "\t\t\t\t\t\t<br/>" + adresseCORP.N1_CITY + "\n";
                        if (adresseCORP.N1_STAE != null) texte += "\t\t\t\t\t\t<br/>" + adresseCORP.N1_STAE + "\n";
                        if (adresseCORP.N1_CTRY != null) texte += "\t\t\t\t\t\t<br/>" + adresseCORP.N1_CTRY + "\n";
                        if (adresseCORP.N1_POST != "") texte += "\t\t\t\t\t\t<br/>" + adresseCORP.N1_POST + "\n";
                    }
                    texte += "\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                    // corporation téléphone
                    if (InfoGEDCOM.N3_SOUR_CORP_PHON_liste != null)
                    {
                        bool premier = true;
                        foreach (string s in InfoGEDCOM.N3_SOUR_CORP_PHON_liste)
                        {
                            texte += "\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                            if (premier)
                            {
                                texte += "\t\t\t\t\t\tTéléphone\n";
                                premier = false;
                            }
                            else texte += "\t\t\t\t\t\t&nbsp;\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t<td>\n";
                            texte += "\t\t\t\t\t\t" + s + "\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t</tr>\n";
                        }
                    }
                    // corporation fax
                    if (InfoGEDCOM.N3_SOUR_CORP_FAX_liste != null)
                    {
                        bool premier = true;
                        foreach (string s in InfoGEDCOM.N3_SOUR_CORP_FAX_liste)
                        {
                            texte += "\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                            if (premier)
                            {
                                texte += "\t\t\t\t\t\tTélécopieur\n";
                                premier = false;
                            }
                            else texte += "\t\t\t\t\t\t&nbsp;\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t<td>\n";
                            texte += "\t\t\t\t\t\t" + s + "\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t</tr>\n";
                        }
                    }
                    // corporation courriel
                    if (InfoGEDCOM.N3_SOUR_CORP_EMAIL_liste != null)
                    {
                        bool premier = true;
                        foreach (string s in InfoGEDCOM.N3_SOUR_CORP_EMAIL_liste)
                        {
                            texte += "\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                            if (premier)
                            {
                                texte += "\t\t\t\t\t\tCourriel\n";
                                premier = false;
                            }
                            else texte += "\t\t\t\t\t\t&nbsp;\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t<td>\n";
                            texte += "\t\t\t\t\t\t" + s + "\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t</tr>\n";
                        }
                    }
                    // corporation WWW
                    if (InfoGEDCOM.N3_SOUR_CORP_WWW_liste != null)
                    {
                        bool premier = true;
                        foreach (string s in InfoGEDCOM.N3_SOUR_CORP_WWW_liste)
                        {
                            texte += "\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t<td class=\"indexCol1\">\n";
                            if (premier)
                            {
                                texte += "\t\t\t\t\t\tSite Web\n";
                                premier = false;
                            }
                            else texte += "\t\t\t\t\t\t&nbsp;\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t<td>\n";
                            texte += "\t\t\t\t\t\t" + s + "\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t</tr>\n";
                        }
                    }
                    // information 
                    if (InfoGEDCOM.N2_SOUR_DATA != null || InfoGEDCOM.N3_SOUR_DATA_DATE != null || InfoGEDCOM.N3_SOUR_DATA_CORP != null)
                    {
                        texte += "\t\t\t\t<tr>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        texte += "\t\t\t\t\t\tInformation\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t\t<td>\n";
                        if (InfoGEDCOM.N2_SOUR_DATA != null) texte += "\t\t\t\t\t\t" + InfoGEDCOM.N2_SOUR_DATA + "\n";
                        string date;
                        if (GH.Properties.Settings.Default.date_longue)
                        {
                            date = GEDCOMClass.ConvertirDateTexte(InfoGEDCOM.N3_SOUR_DATA_DATE);
                        }
                        else
                        {
                            date = InfoGEDCOM.N3_SOUR_DATA_DATE;
                        }
                        if (InfoGEDCOM.N3_SOUR_DATA_DATE != null) texte += "\t\t\t\t\t\t<br/>" + date + "\n";
                        if (InfoGEDCOM.N3_SOUR_DATA_CORP != null) texte += "\t\t\t\t\t\t<br/>" + InfoGEDCOM.N3_SOUR_DATA_CORP + "\n";
                        texte += "\t\t\t\t\t</td>\n";
                        texte += "\t\t\t\t</tr>\n";
                    }
                }
                texte += "\t\t\t</table>\n";
                texte += Groupe("fin", 3);
            }
            // ajouter Groupe chercheur
            if (InfoGEDCOM.N1_SUBM_liste_ID != null)
            { 
                foreach (string ID in InfoGEDCOM.N1_SUBM_liste_ID)
                {
                    if (liste_SUBMITTER.Count == 0)
                    {
                        numero_chercheur = 1;
                        ID_numero info = new ID_numero
                        {
                            numero = numero_chercheur,
                            ID = ID
                        };
                        liste_SUBMITTER.Add(info);
                        numero_chercheur++;
                    }
                    else
                    {
                        bool existe = false;
                        foreach (ID_numero IDListe in liste_SUBMITTER)
                        {
                            if (ID == IDListe.ID)
                            {
                                existe = true;
                                numero_chercheur = IDListe.numero;
                            }
                        }
                        if (!existe)
                        {
                            // trouver nouveau numero
                            numero_chercheur = liste_SUBMITTER.Count + 1;
                            ID_numero info = new ID_numero
                            {
                                numero = numero_chercheur,
                                ID = ID
                            };
                            liste_SUBMITTER.Add(info);
                        }
                    }
                }
            }
            (temp, _, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                Groupe_chercheur(
                liste_SUBMITTER,
                dossierSortie,
                "index",
                note_liste_ID_numero,
                citation_liste_ID_numero,
                source_liste_ID_numero,
                repo_liste_ID_numero,
                0);
            texte += temp;
            // ajouter Groupe note *************************************************************************
            if (note_liste_ID_numero.Count() > 0)
            {
                (temp, _, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) = 
                    Groupe_note(
                        "index",
                        note_liste_ID_numero,
                        citation_liste_ID_numero,
                        source_liste_ID_numero,
                        repo_liste_ID_numero,
                        3);
                texte += temp;
                temp_liste = note_liste_ID_numero;
                temp_liste.Clear();
                temp_liste = citation_liste_ID_numero;
                temp_liste.Clear();
                temp_liste = source_liste_ID_numero;
                temp_liste.Clear();
                temp_liste = repo_liste_ID_numero;
                temp_liste.Clear();
            }
            texte += Bas_Page();
            try
            {
                System.IO.File.WriteAllText(nomFichier, texte);
            }
            catch (Exception msg)
            {
                MessageBox.Show("Ne peut pas écrire le fichier" + nomFichier + ".\r\n\r\n" + msg.Message, "Erreur HI3639 Problème ?",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning);
            }
        }
        public void Index_famille_conjoint(string dossierSortie)
        {
            ListView lvChoixFamille = Application.OpenForms["GH"].Controls["lvChoixFamille"] as ListView;
            string origine = "../";
            string nomFichier = @dossierSortie + "/familles/" + "indexConjoint.html";
            if (File.Exists(nomFichier))
            {
                File.Delete(nomFichier);
            }
            string texte = "";
            texte += Haut_Page(origine, true);
            texte += Separation("large", 3);
            texte += AvoirFamilleConjointIndex();
            texte += "\t\t\t<h1> \n";
            texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun//familleConjoint.svg\" alt=\"\" />\n";
            texte += "\t\t\t\tIndex des famille\n";
            texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun//male.svg\" alt=\"\" />\n";
            texte += "\t\t\t\t</h1>\n";

            texte += Bas_Page();
            try
            {
                System.IO.File.WriteAllText(nomFichier, texte);
            }
            catch (Exception msg)
            {
                MessageBox.Show("Ne peut pas écrire le fichier" + nomFichier + ".\r\n\r\n" + msg.Message, "Erreur HIF3670 Problème ?",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning);
            }
            // créé les pages
            bool[] alphabette = new bool[27];
            for (int f = 0; f < 27; f++)
            {
                alphabette[f] = false;
            }
            // détermine si lettre est utiliser
            for (int f = 0; f < lvChoixFamille.Items.Count; f++)
            {
                if (lvChoixFamille.Items[f].SubItems[1].Text != "")
                {
                    char l = lvChoixFamille.Items[f].SubItems[1].Text[0];
                    int v = (int)l - 65;
                    if (v > -1 && v < 26)
                    {
                        alphabette[v] = true;
                    }
                    else
                    {
                        alphabette[26] = true;
                    }
                }
            }
            for (int f = 0; f < 27; f++)
            {
                if (alphabette[f])
                {
                    texte = "";
                    texte += Haut_Page("..//", true);
                    texte += Separation("large", 3);
                    texte += AvoirFamilleConjointIndex();
                    texte += "\t\t\t<h1>\n";
                    if (f == 26)
                    {
                        texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun/familleConjoint.svg\" alt=\"\" />\n";
                        texte += "\t\t\t\tFamilles diver\n";
                    }
                    else
                    {
                        texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun/familleConjoint.svg\" alt=\"\" />\n";
                        texte += "\t\t\t\tFamilles " + (char)(f + 65) + "\n";
                    }
                    texte += "\t\t\t</h1>";
                    texte += "\t\t\t<table class=\"atl\" style=\"border:1px;\">";
                    texte += "\t\t\t\t<tr>";
                    texte += "\t\t\t\t\t<td class=\"liste\">Voir&nbsp;&nbsp;&nbsp;&nbsp;</td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Conjoint<img style =\"height:32px;vertical-align:middle\" src=\"" + "../commun/male.svg\" alt=\"\" /></td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Conjointe<img style =\"height:32px;vertical-align:middle\" src=\"" + "..//commun/femelle.svg\" alt=\"\" /></td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Mariage</td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">ID</td>\n";
                    texte += "\t\t\t\t</tr>";
                    for (int ff = 0; ff < lvChoixFamille.Items.Count; ff++)
                    {
                        char l;
                        if (lvChoixFamille.Items[ff].SubItems[1].Text == "")
                        {
                            l = '#';
                        }
                        else
                        {
                            l = lvChoixFamille.Items[ff].SubItems[1].Text[0];
                        }
                        if ((char)(f + 65) == l)
                        {
                            texte += "\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">\n";
                            texte += "\t\t\t\t\t\t<a class=\"ficheFamille\"  href=\"" + lvChoixFamille.Items[ff].SubItems[0].Text + ".html\">\n";
                            texte += "\t\t\t\t\t\t</a>\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixFamille.Items[ff].SubItems[1].Text + "</td >\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixFamille.Items[ff].SubItems[2].Text + "</td >\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixFamille.Items[ff].SubItems[3].Text + "</td>\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixFamille.Items[ff].SubItems[0].Text + "</td>\n";
                            texte += "\t\t\t\t</tr>";
                        }
                        else if (f == 26 && !("ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(l)))
                        {
                            texte += "\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">\n";
                            texte += "\t\t\t\t\t\t<a class=\"ficheIndividu\"  href=\"" + lvChoixFamille.Items[ff].SubItems[0].Text + ".html\">\n";
                            texte += "\t\t\t\t\t\t</a>\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixFamille.Items[ff].SubItems[0].Text + "</td >\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixFamille.Items[ff].SubItems[1].Text + "</td >\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixFamille.Items[ff].SubItems[1].Text + "</td>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixFamille.Items[ff].SubItems[2].Text + "</td>\n";
                            texte += "\t\t\t\t</tr>";
                        }
                    }
                    texte += "\t\t\t</table>";
                    texte += Bas_Page();
                    try
                    {
                        if (f == 26)
                        {
                            System.IO.File.WriteAllText(@dossierSortie + "/familles/mDiver.html", texte);
                            Message_HTML("Génération index conjoint  ", "Diver");
                            Application.DoEvents();
                        }
                        else
                        {
                            Message_HTML("Génération index conjoint  ", "lettre" + (char)(f + 65));
                            Application.DoEvents();
                            System.IO.File.WriteAllText(@dossierSortie + "/familles/m" + (char)(f + 65) + ".html", texte);
                        }
                    }
                    catch (Exception msg)
                    {
                        MessageBox.Show("Ne peut pas écrire le fichier" + dossierSortie + ".\r\n\r\n" + msg.Message, "Erreur HIF3782 Problème ?",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Warning);
                        Message_HTML();

                    }
                }
            }
            Message_HTML();
        }
        public void Index_famille_conjointe(string dossierSortie)
        {
            string origine = "../";
            string nomFichier = @dossierSortie + "//familles//" + "indexConjointe.html";
            ListView lvChoixFamille = Application.OpenForms["GH"].Controls["lvChoixFamille"] as ListView;
            if (File.Exists(nomFichier))
            {
                File.Delete(nomFichier);
            }
            string texte = "";
            texte += Haut_Page(origine, true);
            texte += Separation("large", 3);
            texte += AvoirFamilleConjointeIndex();
            texte += "\t\t\t<h1>\n";
            texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun/familleConjointe.svg\" alt=\"\" />\n";
            texte += "\t\t\t\tIndex des famille\n";
            texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun/femelle.svg\" alt=\"\" />\n";
            texte += "\t\t\t</h1>\n";

            texte += Bas_Page();
            try
            {
                System.IO.File.WriteAllText(nomFichier, texte);
            }
            catch (Exception msg)
            {
                MessageBox.Show("Ne peut pas écrire le fichier" + nomFichier + ".\r\n\r\n" + msg.Message, "Erreur HIF3818 Problème ?",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning);
            }
            // créé les pages
            bool[] alphabette = new bool[27];
            for (int f = 0; f < 27; f++)
            {
                alphabette[f] = false;
            }
            // détermine si lettre est utiliser
            for (int f = 0; f < lvChoixFamille.Items.Count; f++)
            {
                if (lvChoixFamille.Items[f].SubItems[2].Text != "")
                {
                    char l = lvChoixFamille.Items[f].SubItems[2].Text[0];
                    int v = (int)l - 65;
                    if (v > -1 && v < 26)
                    {
                        alphabette[v] = true;
                    }
                    else
                    {
                        alphabette[26] = true;
                    }
                }
            }
            for (int f = 0; f < 27; f++)
            {
                if (alphabette[f])
                {
                    
                    
                    texte = "";
                    texte += Haut_Page("../", true);
                    texte += Separation("large", 3);
                    texte += AvoirFamilleConjointeIndex();
                    texte += "\t\t\t<h1>\n";
                    if (f == 26)
                    {
                        texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun/familleConjointe.svg\" alt=\"\" />\n";
                        texte += "\t\t\t\tFamilles diver\n";
                    }
                    else
                    {
                        texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun/familleConjointe.svg\" alt=\"\" />\n";
                        texte += "\t\t\t\tFamilles " + (char)(f + 65) + "\n";
                    }
                    texte += "\t\t\t</h1>\n";
                    texte += "\t\t\t<table class=\"atl\" style=\"border:1px;\">\n";
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Voir&nbsp;&nbsp;&nbsp;&nbsp;</td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Conjointe<img style =\"height:32px;vertical-align:middle\" src=\"" + "../commun/femelle.svg\" alt=\"\" /></td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Conjoint<img style =\"height:32px;vertical-align:middle\" src=\"" + "../commun/male.svg\" alt=\"\" /></td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Mariage</td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">ID</td>\n";
                    texte += "\t\t\t\t</tr>\n";
                    for (int ff = 0; ff < lvChoixFamille.Items.Count; ff++)
                    {
                        char l;
                        if (lvChoixFamille.Items[ff].SubItems[2].Text == "")
                        {
                            l = '#';
                        }
                        else
                        {
                            l = lvChoixFamille.Items[ff].SubItems[2].Text[0];
                        }
                        if ((char)(f + 65) == l)
                        {
                            texte += "\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">\n";
                            texte += "\t\t\t\t\t\t<a class=\"ficheFamille\"  href=\"" + lvChoixFamille.Items[ff].SubItems[0].Text + ".html\"></a>\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixFamille.Items[ff].SubItems[1].Text + "</td>\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixFamille.Items[ff].SubItems[2].Text + "</td>\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixFamille.Items[ff].SubItems[3].Text + "</td>\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixFamille.Items[ff].SubItems[0].Text + "</td>\n";
                            texte += "\t\t\t\t</tr>\n";
                        }
                        else if (f == 26 && !("ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(l)))
                        {
                            texte += "\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">\n";
                            texte += "\t\t\t\t\t\t<a class=\"ficheIndividu\"  href=\"" + lvChoixFamille.Items[ff].SubItems[0].Text + ".html\">\n";
                            texte += "\t\t\t\t\t\t</a>\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixFamille.Items[ff].SubItems[2].Text + "</td >\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixFamille.Items[ff].SubItems[3].Text + "</td >\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixFamille.Items[ff].SubItems[2].Text + "</td>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixFamille.Items[ff].SubItems[3].Text + "</td>\n";
                            texte += "\t\t\t\t</tr>\n";
                        }
                    }
                    texte += "\t\t\t</table>\n";
                    texte += Bas_Page();
                    try
                    {
                        if (f == 26)
                        {
                            Message_HTML("Génération index conjointe  ", "Divert");
                            Application.DoEvents();
                            System.IO.File.WriteAllText(@dossierSortie + "/familles/fDiver.html", texte);
                            if (GH.GH.annuler) return;
                        }
                        else
                        {
                            Message_HTML("Génération index conjointe  ", "lettre" + (char)(f + 65));
                            Application.DoEvents();
                            System.IO.File.WriteAllText(@dossierSortie + "/familles/f" + (char)(f + 65) + ".html", texte);
                            if (GH.GH.annuler) return;
                        }
                    }
                    catch (Exception msg)
                    {
                        MessageBox.Show("Ne peut pas écrire le fichier" + dossierSortie + ".\r\n\r\n" + msg.Message, "Erreur HIF3931 Problème ?",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Warning);
                        Message_HTML();
                    }
                }
            }
            Message_HTML();
        }
        public void Index_individu(string dossierSortie)
        {
            string nomFichier = @dossierSortie + "/individus/" + "index.html";
            ListView lvChoixIndividu = Application.OpenForms["GH"].Controls["lvChoixIndividu"] as ListView;
            if (File.Exists(nomFichier))
            {
                File.Delete(nomFichier);
            }
            string texte = "";
            texte += Haut_Page("..//", true);
            texte += Separation("large", 3);
            string texte_index = Individu_Index_Bouton();
            texte += Individu_Index_Bouton();
            texte += "\t\t\t<h1>\n";
            texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun/neutre.svg\" alt=\"\" />\n";
            texte += "\t\t\t\tIndex des individus\n";
            texte += "\t\t\t</h1>\n";
            texte += Bas_Page();
            try
            {
                System.IO.File.WriteAllText(nomFichier, texte);
            }
            catch (Exception msg)
            {
                MessageBox.Show("Ne peut pas écrire le fichier" + nomFichier + ".\r\n\r\n" + msg.Message, "Erreur HI3964 Problème ?",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Warning);
            }
            // créé les pages
            bool[] alphabette = new bool[27];
            for (int f = 0; f < 27; f++)
            {
                alphabette[f] = false;
            }
            // détermine si lettre est utiliser
            for (int f = 0; f < lvChoixIndividu.Items.Count; f++)
            {
                if (lvChoixIndividu.Items[f].SubItems[1].Text != "")
                {
                    if (lvChoixIndividu.Items[f].SubItems[1].Text[0].ToString() != "")
                    {
                        char l = lvChoixIndividu.Items[f].SubItems[1].Text[0];

                        int v = (int)l - 65;
                        if (v > -1 && v < 26)
                        {
                            alphabette[v] = true;
                        }
                        else
                        {
                            alphabette[26] = true;
                        }
                    }
                    else
                    {
                        alphabette[26] = true;
                    }
                }
                else
                {
                    alphabette[26] = true;
                }
            }
            for (int f = 0; f < 27; f++)
            {
                if (alphabette[f])
                {
                    texte = "";
                    texte += Haut_Page("../", true);
                    texte += Separation("large", 3);
                    texte += texte_index;
                    texte += "\t\t\t<h1>\n";
                    if (f == 26)
                    {
                        texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun/neutre.svg\" alt=\"\" />\n";
                        texte += "\t\t\t\tIndividus diver" + "\n";
                    }
                    else
                    {
                        texte += "\t\t\t\t<img style =\"height:128px;vertical-align:middle\" src=\"" + "../commun/neutre.svg\" alt=\"\" />\n";
                        texte += "\t\t\t\tIndividus  " + (char)(f + 65) + "\n";
                    }
                    texte += "\t\t\t</h1>\n";
                    texte += "\t\t\t<table class=\"atl\" style=\"border:1px;\">\n";
                    texte += "\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Voir&nbsp;&nbsp;&nbsp;&nbsp;</td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Nom</td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">ID</td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Naissance</td>\n";
                    texte += "\t\t\t\t\t<td class=\"liste\">Décès</td>\n";
                    texte += "\t\t\t\t</tr>";
                    for (int ff = 0; ff < lvChoixIndividu.Items.Count; ff++)
                    {
                        char l;
                        if (lvChoixIndividu.Items[ff].SubItems[1].Text == "")
                        {
                            l = '#';
                        }
                        else
                        {
                            l = lvChoixIndividu.Items[ff].SubItems[1].Text[0];
                        }
                        if ((char)(f + 65) == l)
                        {
                            texte += "\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">\n";
                            texte += "\t\t\t\t\t\t<a class=\"ficheIndividu\"  href=\"" + lvChoixIndividu.Items[ff].SubItems[0].Text + ".html\">\n";
                            texte += "\t\t\t\t\t\t</a>\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t<td>\n";
                            texte += "\t\t\t\t\t\t" + lvChoixIndividu.Items[ff].SubItems[1].Text + "\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixIndividu.Items[ff].SubItems[0].Text + "</td>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixIndividu.Items[ff].SubItems[2].Text + "</td>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixIndividu.Items[ff].SubItems[4].Text + "</td>\n";
                            texte += "\t\t\t\t</tr>";
                        }
                        else if (f == 26 && !("ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(l)))
                        {
                            texte += "\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">\n";
                            texte += "\t\t\t\t\t\t<a class=\"ficheIndividu\"  href=\"" + lvChoixIndividu.Items[ff].SubItems[2].Text + ".html\">\n";
                            texte += "\t\t\t\t\t\t</a>\n";
                            texte += "\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixIndividu.Items[ff].SubItems[1].Text + "</td >\n";
                            texte += "\t\t\t\t\t<td>" + lvChoixIndividu.Items[ff].SubItems[0].Text + "</td >\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixIndividu.Items[ff].SubItems[2].Text + "</td>\n";
                            texte += "\t\t\t\t\t<td class=\"liste\">" + lvChoixIndividu.Items[ff].SubItems[3].Text + "</td>\n";
                            texte += "\t\t\t\t</tr>";
                        }
                    }
                    texte += "\t\t\t</table>";
                    texte += Bas_Page();
                    try
                    {
                        if (f == 26)
                        {
                            Message_HTML("Génération index individu  ", "Diver");
                            Application.DoEvents();
                            System.IO.File.WriteAllText(@dossierSortie + "/individus/Diver.html", texte);
                            if (GH.GH.annuler) return;
                        }
                        else
                        {
                            Message_HTML("Génération index individu  ", "lettre" + (char)(f + 65));
                            Application.DoEvents();
                            System.IO.File.WriteAllText(@dossierSortie + "/individus/" + (char)(f + 65) + ".html", texte);
                            if (GH.GH.annuler) return;
                        }
                    }
                    catch (Exception msg)
                    {
                        MessageBox.Show("Ne peut pas écrire le fichier" + dossierSortie + ".\r\n\r\n" + msg.Message, "Erreur HII4090 Problème ?",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Warning);
                        Message_HTML();
                    }
                }
            }
            Message_HTML();
        }
        public string Individu(string IDIndividu, bool menu, string dossierSortie)
        {
            List<ID_numero> liste_SUBMITTER_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_note_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_citation_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_source_ID_numero = new List<ID_numero>();
            List<ID_numero> liste_repo_ID_numero = new List<ID_numero>();
            List<ID_numero> temp_liste = new List<ID_numero>();

            (liste_note_ID_numero, liste_citation_ID_numero, liste_source_ID_numero, liste_repo_ID_numero) =
                Avoir_liste_reference
                    (
                        "INDIVIDU",
                        IDIndividu,
                        liste_note_ID_numero,
                        citation_liste_ID_numero,
                        source_liste_ID_numero,
                        repo_liste_ID_numero
                    );
            temp_liste.Clear();
            TextBox Tb_Status = Application.OpenForms["GH"].Controls["Tb_Status"] as TextBox;
            Tb_Status.Text = "Génération de la fiche individu ID " + IDIndividu;
            Application.DoEvents();
            string sousDossier = "individus";
            if (IDIndividu == null) return null;
            
            bool groupe_conjoint = false;
            bool groupe_parent = false;
            bool groupe_media = false;
            bool groupe_evenement;
            bool groupe_attribut;
            bool groupe_chercheur;
            bool groupe_ordonnance;
            bool groupe_citation = false;
            bool groupe_source = false;
            bool groupe_note = false;
            bool groupe_depot;
            int numeroCarte = 0;
            string IDCourantIndividu = IDIndividu;
            // Récupérer les informations de l'individu
            GEDCOMClass.INDIVIDUAL_RECORD infoIndividu;
            (_, infoIndividu) = GEDCOMClass.Avoir_info_individu(IDIndividu);
            string nom = GEDCOMClass.AvoirPremierNomIndividu(IDIndividu);
            List<GEDCOMClass.PERSONAL_NAME_STRUCTURE> listeInfoNom;
            bool OkLIN;
            (OkLIN, listeInfoNom) = GEDCOMClass.AvoirListeNom(IDIndividu);
            string fichierSex;
            string genre = infoIndividu.N1_SEX;
            string genreTxt = null;
            if (genre == "M")
            {
                genreTxt = "masculin";
            };
            if (genreTxt == "F")
            {
                genre = "féminin";
            };
            // avoir le portrait
            string fichierPortrait;
            string IDPortrait = null;
            GEDCOMClass.MULTIMEDIA_RECORD portraitInfo;
            if (infoIndividu.N1_OBJE_liste.Count > 0)
            {
                IDPortrait = infoIndividu.N1_OBJE_liste[0];
                portraitInfo = GEDCOMClass.AvoirMedia(IDPortrait);
                fichierPortrait = CopierObjet(portraitInfo.N1_FILE, "individus", dossierSortie);
                if (fichierPortrait == null)
                {
                    fichierPortrait = "../commun/media_manquant.svg";
                }
                else
                {
                    fichierPortrait = "medias/" + fichierPortrait;
                }
            }
            else
            {
                if (genre == "M")
                {
                    fichierPortrait = "../commun/masculin.png";
                }
                else if (genre == "F")
                {
                    fichierPortrait = "../commun/feminin.png";
                }
                else
                {
                    fichierPortrait = "../commun/neutre.png";
                }
            }
            if (genre == "M")
            {
                fichierSex = "../commun/male.svg";

            }
            else if (genre == "F")
            {
                fichierSex = "../commun/femelle.svg";
            }
            else
            {
                fichierSex = "../commun/neutre.svg";
            }
            GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE NaissanceIndividu;
            (_, NaissanceIndividu) = GEDCOMClass.AvoirEvenementNaissance(infoIndividu.N1_EVENT_Liste);
            GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE DecesIndividu;
            (_, DecesIndividu) = GEDCOMClass.AvoirEvenementDeces(infoIndividu.N1_EVENT_Liste);
            string dateNaissance = NaissanceIndividu.N2_DATE;
            string dateDeces = DecesIndividu.N2_DATE;
            string age = null;
            if (dateNaissance != null && dateDeces != null)
            {
                age = Avoir_age(dateNaissance, dateDeces);
            }
            string nomFichierIndividu = dossierSortie + "\\individus\\" + IDIndividu + ".html";
            if (!menu) nomFichierIndividu = dossierSortie + "\\individus\\page.html";
            if (File.Exists(nomFichierIndividu))
            {
                File.Delete(nomFichierIndividu);
            }
            string texte = null;
            texte += Haut_Page("../", menu);
            texte += Separation("large", 3);
            // restriction
            if (infoIndividu.N1_RESN != null)
            {
                texte += "\t\t\t\t<div class=\"blink3\">\n";
                texte += "\t\t\t\t\tRestriction\n";
                texte += "\t\t\t\t\t" + infoIndividu.N1_RESN + "\n";
                texte += "\t\t\t\t</div>\n";
            }
            texte += "\t\t\t\t<table class=\"titre\">\n";
            texte += "\t\t\t\t\t<tr>\n";
            texte += "\t\t\t\t\t\t<td>\n";
            texte += "\t\t\t\t\t\t\t" + nom + "\n";
            string temp;
            (temp, liste_note_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) = 
                Avoir_citation_source_lien(
                    infoIndividu.N1_SOUR_citation_liste_ID,
                    liste_note_ID_numero,
                    citation_liste_ID_numero,
                    source_liste_ID_numero,
                    repo_liste_ID_numero,
                    6);
            texte += temp;
            // affiche si adopter
            if (infoIndividu.Adopter != null)
            {
                texte += "\t\t\t\t\t\t\t<div style=\"font-size:small\"><strong>Adopter</strong></div>" + "\n";
            }
            if (GH.Properties.Settings.Default.VoirID == true)
            {
                texte += "\t\t\t\t\t\t<div style=\"font-size:small\">[" + IDIndividu + "]</div>" + "\n";
            }
            texte += "\t\t\t\t\t\t</td>\n";
            texte += "\t\t\t\t\t</tr>\n";
            texte += "\t\t\t\t</table>\n";
            texte += "\t\t\t\t<div style=\"border: 2px solid #000;border-top:0px solid #000;padding:10px;min-height:300px;\">\n";
            if (GH.Properties.Settings.Default.photo_principal)
            {
                texte += "\t\t\t\t\t<img class=\"portrait\" src=\"" + fichierPortrait + "\" alt=\"\" />\n";
            }
            // adopter
            if (infoIndividu.Adopter != null)
            {
                texte += "\t\t\t\t\t<div>\n";
                texte += "\t\t\t\t\t\t<span class=\"detail_col1\">&nbsp;</span>\n";
                texte += "\t\t\t\t\t\t<span class=\"detail_col2\" style=\"font-size:150%;\"><strong>Adopter" +
                    "</strong></span>\n";
                texte += "\t\t\t\t\t</div>\n";
            }
            // liste des noms **********************************************************************************************************************
            if (OkLIN)
            {
                foreach (GEDCOM.GEDCOMClass.PERSONAL_NAME_STRUCTURE infoNom in listeInfoNom)
                {
                    string nom1 = null;
                    string nom2 = null;
                    if (infoNom.N0_NAME != null)
                        nom1 = infoNom.N0_NAME;
                    if (infoNom.N1_SURN != null || infoNom.N1_GIVN != null)
                    {
                        string titre = infoNom.N1_NPFX;
                        if (titre != null) titre += " ";
                        string prefix = infoNom.N1_SPFX;
                        if (prefix != null) prefix += " ";
                        string suffix = infoNom.N1_NSFX;
                        if (suffix != null) suffix = ", " + suffix;
                        nom2 = titre + prefix + infoNom.N1_SURN + ", " + infoNom.N1_GIVN + suffix;
                    }
                    
                    texte += "\t\t\t\t\t<div>\n";
                    texte += "\t\t\t\t\t\t<span class=\"detail_col1\">\n";
                    texte += "                         " + infoNom.N1_TYPE + "\n";
                    texte += "\t\t\t\t\t\t</span>\n";
                    if (nom1 != null && nom2 == null)
                    {
                        texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                        texte += "\t\t\t\t\t\t\t<strong>" + nom1 + "</strong>\n";
                        texte += "\t\t\t\t\t\t</span>\n";
                    }
                    if (nom1 != null && nom2 != null)
                    {
                        texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                        texte += "\t\t\t\t\t\t<strong>" + nom1 + "</strong><br />\n";
                        texte += "\t\t\t\t\t\t" + nom2 + "\n";
                        texte += "\t\t\t\t\t\t</span>\n";
                    }
                    if (nom1 == null && nom2 != null)
                    {
                        texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                        texte += "\t\t\t\t\t\t\t<strong>" + nom2 + "</strong>\n";
                        texte += "\t\t\t\t\t\t</span>\n";
                    }
                    texte += "\t\t\t\t\t</div>\n";
                    // surnom
                    if (infoNom.N1_NICK != null)
                    {
                        texte += "\t\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t\t<span class=\"detail_col1\">\n";
                        texte += "\t\t\t\t\t\t\t&nbsp;\n";
                        texte += "\t\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                        texte += "\t\t\t\t\t\t\tSurnom " + infoNom.N1_NICK + "\n";
                        texte += "\t\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t\t</div>\n";
                    }
                    if (infoNom.N1_NICK != null)
                    {
                        texte += "\t\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t\t<span class=\"detail_col1\">\n";
                        texte += "\t\t\t\t\t\t\t&nbsp;\n";
                        texte += "\t\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                        texte += "\t\t\t\t\t\t\tSurnom " + infoNom.N1_NICK + "\n";
                        texte += "\t\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t\t</div>\n";
                    }
                    if(infoNom.N1_SOUR_citation_liste_ID != null)
                    {
                        texte += "\t\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t\t<span class=\"detail_col1\">\n";
                        texte += "\t\t\t\t\t\t\t&nbsp;\n";
                        texte += "\t\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                        (temp, liste_note_ID_numero, liste_citation_ID_numero, liste_source_ID_numero, liste_repo_ID_numero) = 
                            Avoir_citation_source_lien(
                                infoNom.N1_SOUR_citation_liste_ID,
                                liste_note_ID_numero,
                                liste_citation_ID_numero,
                                liste_source_ID_numero,
                                repo_liste_ID_numero,
                                6);
                        texte += temp;
                        texte += "\t\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t\t</div>\n";
                    }
                    if (infoNom.N1_NOTE_liste_ID != null)
                    {
                        texte += "\t\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t\t<span class=\"detail_col1\">\n";
                        texte += "\t\t\t\t\t\t\t&nbsp;\n";
                        texte += "\t\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                        (temp, note_liste_ID_numero, citation_liste_ID_numero, liste_source_ID_numero, repo_liste_ID_numero) =
                            Avoir_note_lien(
                                infoNom.N1_NOTE_liste_ID,
                                note_liste_ID_numero,
                                citation_liste_ID_numero,
                                liste_source_ID_numero,
                                repo_liste_ID_numero,
                                6);
                        texte += temp;
                        texte += "\t\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t\t</div>\n";
                    }
                    // FONE
                    string nom3 = null;
                    string nom4 = null;
                    string prefixFONE = infoNom.N2_FONE_SPFX;
                    if (prefixFONE != null) prefixFONE += " ";
                    string suffixFONE = infoNom.N2_FONE_NSFX;
                    if (suffixFONE != null) suffixFONE = ", " + suffixFONE;
                    string surnomFONE = infoNom.N2_FONE_NICK;
                    string titreFONE = infoNom.N2_FONE_NPFX;
                    if (titreFONE != null) titreFONE += " ";
                    if (infoNom.N2_FONE_SURN != null || infoNom.N2_FONE_GIVN != null)
                    {
                        nom4 = titreFONE + prefixFONE + infoNom.N2_FONE_SURN + ", " + infoNom.N2_FONE_GIVN + suffixFONE;
                    }
                    nom3 = infoNom.N1_FONE;
                    if (nom3 != null || nom4 != null)
                    {
                        texte += "\t\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t\t<span class=\"detail_col1 texteR\" >\n";
                        texte += "\t\t\t\t\t\t\tNom phonétiser&nbsp;&nbsp;\n";
                        texte += "\t\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                        texte += "\t\t\t\t\t\tType " + infoNom.N2_FONE_TYPE;
                        texte += "\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t\t</div>\n";
                        texte += "\t\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t\t<span class=\"detail_col1 texteR\" >\n";
                        texte += "\t\t\t\t\t\t\t&nbsp;\n";
                        texte += "\t\t\t\t\t\t</span>\n";
                        if (nom3 != null && nom4 == null)
                        {
                            texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                            texte += "\t\t\t\t\t\t\t<strong>" + nom3 + "</strong>\n";
                            texte += "\t\t\t\t\t\t</span>\n";
                        }
                        if (nom3 != null && nom4 != null)
                        {
                            texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                            texte += "\t\t\t\t\t\t<strong>" + nom3 + "</strong><br />\n";
                            texte += "\t\t\t\t\t\t" + nom4 + "\n";
                            texte += "\t\t\t\t\t\t</span>\n";
                        }
                        if (nom3 == null && nom4 != null)
                        {
                            texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                            texte += "\t\t\t\t\t\t\t<strong>" + nom4 + "</strong>\n";
                            texte += "\t\t\t\t\t\t</span>\n";
                        }
                        texte += "\t\t\t\t\t</div>\n";
                        // surnom FONE
                        if (infoNom.N2_FONE_NICK != null)
                        {
                            texte += "\t\t\t\t\t<div>\n";
                            texte += "\t\t\t\t\t\t<span class=\"detail_col1\">\n";
                            texte += "\t\t\t\t\t\t\t&nbsp;\n";
                            texte += "\t\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                            texte += "\t\t\t\t\t\t\tSurnom " + infoNom.N2_FONE_NICK + "\n";
                            texte += "\t\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t\t</div>\n";
                        }
                        // SOUR FONE
                        if (infoNom.N2_FONE_SOUR_citation_liste_ID != null)
                        {
                            texte += "\t\t\t\t\t<div>\n";
                            texte += "\t\t\t\t\t\t<span class=\"detail_col1\">\n";
                            texte += "\t\t\t\t\t\t\t&nbsp;\n";
                            texte += "\t\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                            (temp, note_liste_ID_numero, citation_liste_ID_numero, liste_source_ID_numero, repo_liste_ID_numero) = 
                                    Avoir_citation_source_lien(
                                    infoNom.N2_FONE_SOUR_citation_liste_ID,
                                    note_liste_ID_numero,
                                    citation_liste_ID_numero,
                                    liste_source_ID_numero,
                                    repo_liste_ID_numero,
                                7);
                            texte += temp;
                            //texte += Avoir_source_citation_lien(infoNom.N2_FONE_SOUR_citation_liste_ID, 7);
                            texte += "\t\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t\t</div>\n";
                        }
                        // note FONE
                        if (infoNom.N2_FONE_NOTE_ID_liste != null)
                        {
                            texte += "\t\t\t\t\t<div>\n";
                            texte += "\t\t\t\t\t\t<span class=\"detail_col1\">\n";
                            texte += "\t\t\t\t\t\t\t&nbsp;\n";
                            texte += "\t\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                            (temp, note_liste_ID_numero, citation_liste_ID_numero, liste_source_ID_numero, repo_liste_ID_numero) =
                                Avoir_note_lien(
                                    infoNom.N2_FONE_NOTE_ID_liste,
                                    note_liste_ID_numero,
                                    citation_liste_ID_numero,
                                    liste_source_ID_numero,
                                    repo_liste_ID_numero,
                                    7);
                            texte += temp;
                            texte += "\t\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t\t</div>\n";
                        }
                    }
                    // ROMN
                    string nom5 = null;
                    string nom6 = null;
                    string prefixROMN = infoNom.N2_ROMN_SPFX;
                    if (prefixROMN != null) prefixROMN += " ";
                    string suffixROMN = infoNom.N2_ROMN_NSFX;
                    if (suffixROMN != null) suffixROMN = ", " + suffixROMN;
                    string surnomROMN = infoNom.N2_ROMN_NICK;
                    string titreROMN = infoNom.N2_ROMN_NPFX;
                    if (titreROMN != null) titreROMN += " ";
                    if (infoNom.N2_ROMN_SURN != null || infoNom.N2_ROMN_GIVN != null)
                    {
                        nom4 = titreROMN + prefixROMN + infoNom.N2_ROMN_SURN + ", " + infoNom.N2_ROMN_GIVN + suffixROMN;
                    }
                    nom5 = infoNom.N1_ROMN;
                    if (nom5 != null || nom6 != null)
                    {
                        texte += "\t\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t\t<span class=\"detail_col1 texteR\" >\n";
                        texte += "\t\t\t\t\t\t\tNom romanisation&nbsp;&nbsp;\n";
                        texte += "\t\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                        texte += "\t\t\t\t\t\tType " + infoNom.N2_ROMN_TYPE;
                        texte += "\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t\t</div>\n";
                        texte += "\t\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t\t<span class=\"detail_col1 texteR\" >\n";
                        texte += "\t\t\t\t\t\t\t&nbsp;\n";
                        texte += "\t\t\t\t\t\t</span>\n";
                        if (nom5 != null && nom6 == null)
                        {
                            texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                            texte += "\t\t\t\t\t\t\t<strong>" + nom3 + "</strong>\n";
                            texte += "\t\t\t\t\t\t</span>\n";
                        }
                        if (nom5 != null && nom6 != null)
                        {
                            texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                            texte += "\t\t\t\t\t\t<strong>" + nom5 + "</strong><br />\n";
                            texte += "\t\t\t\t\t\t" + nom6 + "\n";
                            texte += "\t\t\t\t\t\t</span>\n";
                        }
                        if (nom5 == null && nom6 != null)
                        {
                            texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                            texte += "\t\t\t\t\t\t\t<strong>" + nom6 + "</strong>\n";
                            texte += "\t\t\t\t\t\t</span>\n";
                        }
                        if (infoNom.N2_ROMN_TYPE != null)
                        {
                            texte += "\t\t\t\t<div>\n";
                            texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                            texte += "\t\t\t\t\t\t&nbsp;\n";
                            texte += "\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                            texte += "\t\t\t\t\t\tType " + infoNom.N2_ROMN_TYPE;
                            texte += "\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t</div>\n";
                        }
                        // SOUR ROMN
                        if (infoNom.N2_ROMN_SOUR_citation_liste_ID != null)
                        {
                            texte += "\t\t\t\t\t<div>\n";
                            texte += "\t\t\t\t\t\t<span class=\"detail_col1\">\n";
                            texte += "\t\t\t\t\t\t\t&nbsp;\n";
                            texte += "\t\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                            (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) = 
                                Avoir_citation_source_lien(
                                    infoNom.N2_ROMN_SOUR_citation_liste_ID,
                                    note_liste_ID_numero,
                                    citation_liste_ID_numero,
                                    source_liste_ID_numero,
                                    repo_liste_ID_numero,
                                    7);
                            texte += temp;
                            //texte += Avoir_source_citation_lien(infoNom.N2_ROMN_SOUR_citation_liste_ID, 7);
                            texte += "\t\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t\t</div>\n";
                        }
                        // NOTE ROMN
                        if (infoNom.N2_ROMN_NOTE_ID_liste != null)
                        {
                            texte += "\t\t\t\t<div>\n";
                            texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                            texte += "\t\t\t\t\t\t&nbsp;\n";
                            texte += "\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                            (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                                Avoir_note_lien(
                                    infoNom.N2_ROMN_NOTE_ID_liste,
                                    note_liste_ID_numero, 
                                    citation_liste_ID_numero,
                                    source_liste_ID_numero,
                                    repo_liste_ID_numero,
                                    6);
                            texte += temp;
                            texte += "\t\t\t\t\t</span>\n";
                            texte += "\t\t\t\t</div>\n";
                        }
                    }
                }
                texte += Separation("moyen", 4);
            }
            // si allia
            if (infoIndividu.N1_ALIA_liste_ID != null)
            {
                foreach (string ID in infoIndividu.N1_ALIA_liste_ID)
                {
                    (bool Ok_nom_Alia, GEDCOMClass.INDIVIDUAL_RECORD alia_individu) = GEDCOMClass.Avoir_info_individu(ID);
                    string nom_ALIA = null;
                    if (Ok_nom_Alia)
                    {
                        if (alia_individu.N1_NAME_liste[0].N1_SURN != null ||
                            alia_individu.N1_NAME_liste[0].N1_GIVN != null
                            )
                        {
                            nom_ALIA = AssemblerPatronymePrenom(
                                    alia_individu.N1_NAME_liste[0].N1_SURN,
                                    alia_individu.N1_NAME_liste[0].N1_GIVN);
                        }
                        else
                        {
                            nom_ALIA = alia_individu.N1_NAME_liste[0].N0_NAME;
                        }
                    }
                    string txtID = null;
                    if (GH.Properties.Settings.Default.VoirID == true)
                    {
                        txtID = " [" + ID + "]";
                    }

                    texte += "\t\t\t\t\t<div>\n";
                    texte += "\t\t\t\t\t\t<span class=\"detail_col1\">\n";
                    texte += "\t\t\t\t\t\t\tMême personne?\n";
                    texte += "\t\t\t\t\t\t</span>\n";
                    texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                    if (menu)
                    {
                        texte += "\t\t\t\t\t\t\t<a class=\"ficheIndividu\"  href=\"" + ID + ".html\"></a>\n";
                    }
                    else
                    {
                        texte += "\t\t\t\t\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                    }
                    texte += "\t\t\t\t\t\t\t" + nom_ALIA + txtID + "\n";
                    texte += "\t\t\t\t\t\t</span>\n";
                    texte += "\t\t\t\t\t</div>\n";
                }
            }
            // Genre
            if (infoIndividu.N1_SEX != null)
            {
                texte += Separation("moyen", 5);
                texte += "\t\t\t\t\t<div>\n";
                texte += "\t\t\t\t\t\t<span class=\"detail_col1\">\n";
                texte += "\t\t\t\t\t\t\tGenre\n";
                texte += "\t\t\t\t\t\t</span>\n";
                texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                texte += "\t\t\t\t\t\t\t" + genre + " " +
                    "<img style =\"height:16px;vertical-align:middle\" src=\"" + fichierSex +
                    "\" alt=\"\" />\n";
                texte += "\t\t\t\t\t\t</span>\n";
                texte += "\t\t\t\t\t</div>\n";
            }
            // Âge
            if (age != null)
            {
                texte += Separation("moyen", 5);
                texte += "\t\t\t\t\t<div>\n";
                texte += "\t\t\t\t\t\t<span class=\"detail_col1\">\n";
                texte += "\t\t\t\t\t\t\tÂge au décès\n";
                texte += "\t\t\t\t\t\t</span>\n";
                texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                texte += "\t\t\t\t\t\t\t" + age + "\n";
                texte += "\t\t\t\t\t\t</span>\n";
                texte += "\t\t\t\t\t</div>\n";
            }
            // Nombre d'enfant
            GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE info = GEDCOMClass.Avoir_attribute_nombre_enfant(infoIndividu.N1_Attribute_liste);
            if (info.N1_EVEN_texte != null) // NCHI {CHILDREN_COUNT}
            {
                texte += Separation("moyen", 5);
                texte += "\t\t\t\t\t<div>\n";
                texte += "\t\t\t\t\t\t<span class=\"detail_col1\">\n";
                texte += "\t\t\t\t\t\t\tNombre d'enfant\n";
                texte += "\t\t\t\t\t\t</span>\n";
                texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                texte += "\t\t\t\t\t\t\t" + info.N1_EVEN_texte + "\n";
                texte += "\t\t\t\t\t\t</span>\n";
                texte += "\t\t\t\t\t</div>\n";
            }
            // Nombre de mariage
            GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE evenement = GEDCOMClass.Avoir_attribute_nombre_mariage(infoIndividu.N1_Attribute_liste);
            if (evenement.N1_EVEN_texte != null) // NMR {MARRIAGE_COUNT}
            {
                texte += Separation("moyen", 5);
                texte += "\t\t\t\t\t<div>\n";
                texte += "\t\t\t\t\t\t<span class=\"detail_col1\">\n";
                texte += "\t\t\t\t\t\t\tNombre de mariage\n";
                texte += "\t\t\t\t\t\t</span>\n";
                texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                texte += "\t\t\t\t\t\t\t" + evenement.N1_EVEN_texte + "\n";
                texte += "\t\t\t\t\t\t</span>\n";
                texte += "\t\t\t\t\t</div>\n";
            }
            // si association
            if (infoIndividu.N1_ASSO_liste != null)
            {
                foreach (GEDCOMClass.ASSOCIATION_STRUCTURE info_ASSO in infoIndividu.N1_ASSO_liste)
                {
                    texte += Separation("moyen", 5);
                    texte += "\t\t\t\t\t<div>\n";
                    texte += "\t\t\t\t\t\t<span class=\"detail_col1\">\n";
                    texte += "\t\t\t\t\t\t\tAssociation\n";
                    texte += "\t\t\t\t\t\t</span>\n";
                    texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                    texte += "\t\t\t\t\t\t\t" + GEDCOMClass.AvoirPremierNomIndividu(info_ASSO.N0_ASSO) + " Relation " + 
                        info_ASSO.N1_RELA + "\n";
                    // source association
                    List<string> listeLien = new List<string>();
                    (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) = 
                        Avoir_citation_source_lien(
                            info_ASSO.N1_SOUR_citation_liste_ID,
                            note_liste_ID_numero,
                            citation_liste_ID_numero,
                            source_liste_ID_numero,
                            repo_liste_ID_numero,
                            6);
                    texte += temp;
                    texte += SuprimerVirgule(listeLien);
                    texte += "\t\t\t\t\t\t</span>\n";
                    texte += "\t\t\t\t\t</div>\n";
                    // association note
                    if (info_ASSO.N1_NOTE_liste_ID != null)
                    {
                        texte += "\t\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t\t<span class=\"detail_col1\">\n";
                        texte += "\t\t\t\t\t\t\t&nbsp;\n";
                        texte += "\t\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                        (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                            Avoir_note_lien(
                                info_ASSO.N1_NOTE_liste_ID, 
                                note_liste_ID_numero, 
                                citation_liste_ID_numero,
                                source_liste_ID_numero,
                                repo_liste_ID_numero,
                                6);
                        texte += temp;
                        texte += "\t\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t\t</div>\n";
                    }
                }
            }
            // si N1__ANCES_CLE_FIXE
            if (infoIndividu.N1__ANCES_CLE_FIXE != null)
            {
                texte += Separation("moyen", 5);
                texte += "\t\t\t\t\t<div>\n";
                texte += "\t\t\t\t\t\t\tClé fixe (Ancestrologie) \n";
                texte += "\t\t\t\t\t\t\t" + infoIndividu.N1__ANCES_CLE_FIXE + "\n";
                texte += "\t\t\t\t\t</div>\n";
            }
            // si RIN ******************************************************************************************************************************
            if (infoIndividu.N1_RIN != null)
            {
                texte += Separation("moyen", 5);
                texte += "\t\t\t\t\t<div>\n";
                texte += "\t\t\t\t\t\t\tID d'enregistrement automatisé(RIN) \n";
                texte += "\t\t\t\t\t\t\t" + infoIndividu.N1_RIN + "\n";
                texte += "\t\t\t\t\t</div>\n";
            }
            // si REFN ******************************************************************************************************************************
            if (infoIndividu.N1_REFN_liste != null)
            {
                foreach (GEDCOMClass.USER_REFERENCE_NUMBER info_REFN in infoIndividu.N1_REFN_liste)
                {
                    texte += Separation("moyen", 5);
                    texte += "\t\t\t\t\t<div>\n";
                    texte += "\t\t\t\t\t\t\tNuméro de fichier d'enregistrement permanent(REFN) \n";
                    texte += "\t\t\t\t\t\t\t" + info_REFN.N0_REFN;
                    if (info_REFN.N1_TYPE != null)
                    {
                        texte += " Type " + info_REFN.N1_TYPE + "\n";
                    }
                    else
                    {
                        texte += "\n";
                    }
                    texte += "\t\t\t\t\t</div>\n";
                }
            }
            int numeroSUBMITTER = 0;
            // si ANCI *****************************************************************************************************
            if (infoIndividu.N1_ANCI_liste_ID.Count > 0)
            {
                texte += Separation("moyen", 5);
                texte += "\t\t\t\t\t<div>\n";
                texte += "\t\t\t\t\t\tIl y a une recherche en cours pour identifier des ascendants supplémentaires de cet individu par le(s) chercheur(s) numéro(s) \n";
                string texteTemp = null;
                foreach (string ID in infoIndividu.N1_ANCI_liste_ID)
                {
                    if (liste_SUBMITTER_ID_numero.Count == 0)
                    {
                        numeroSUBMITTER = 1;
                        ID_numero info_ANCI = new ID_numero
                        {
                            numero = numeroSUBMITTER,
                            ID = ID
                        };
                        liste_SUBMITTER_ID_numero.Add(info_ANCI);
                        texteTemp += "<a class=\"chercheur\" href=\"#RefSubmitter" + numeroSUBMITTER.ToString() + "\">" + numeroSUBMITTER.ToString() + "</a>" + ", ";
                        numeroSUBMITTER++;
                    }
                    else
                    {
                        bool existe = false;
                        foreach (ID_numero IDListe in liste_SUBMITTER_ID_numero)
                        {
                            if (ID == IDListe.ID)
                            {
                                existe = true;
                                numeroSUBMITTER = IDListe.numero;
                            }
                        }
                        if (existe)
                        {
                            texteTemp += "<a class=\"chercheur\" href=\"#RefSubmitter" + numeroSUBMITTER.ToString() + "\">" + numeroSUBMITTER.ToString() + "</a>" + ", ";
                        }
                        else
                        {
                            // trouver nouveau numero
                            numeroSUBMITTER = liste_SUBMITTER_ID_numero.Count + 1;
                            ID_numero info_ANCI = new ID_numero
                            {
                                numero = numeroSUBMITTER,
                                ID = ID
                            };
                            liste_SUBMITTER_ID_numero.Add(info_ANCI);
                            texteTemp += "<a class=\"chercheur\" href=\"#RefSubmitter" + numeroSUBMITTER.ToString() + "\">" + numeroSUBMITTER.ToString() + "</a>" + ", ";
                        }
                    }
                }
                texte += texteTemp.TrimEnd(' ', ',') + ".";
                texte += "\t\t\t\t\t\t" + " Voir le groupe chercheur.";
                texte += "\t\t\t\t\t</div>\n";
            }
            // si DESI *****************************************************************************************************
            if (infoIndividu.N1_DESI_liste_ID.Count > 0)
            {
                texte += Separation("moyen", 5);
                texte += "\t\t\t\t\t<div>\n";
                texte += "\t\t\t\t\t\tIl y a une recherche en cours pour identifier des descendants supplémentaires de cet individu par le(s) chercheur(s) numéro(s) \n";
                string texteTemp = null;
                foreach (string ID in infoIndividu.N1_DESI_liste_ID)
                {
                    if (liste_SUBMITTER_ID_numero.Count == 0)
                    {
                        numeroSUBMITTER = 1;
                        ID_numero info_DESI = new ID_numero
                        {
                            numero = numeroSUBMITTER,
                            ID = ID
                        };
                        liste_SUBMITTER_ID_numero.Add(info_DESI);
                        texteTemp += "<a class=\"chercheur\" href=\"#RefSubmitter" + numeroSUBMITTER.ToString() + "\">" + numeroSUBMITTER.ToString() + "</a>" + ", ";
                        numeroSUBMITTER++;
                    }
                    else
                    {
                        bool existe = false;
                        foreach (ID_numero IDListe in liste_SUBMITTER_ID_numero)
                        {
                            if (ID == IDListe.ID)
                            {
                                existe = true;
                                numeroSUBMITTER = IDListe.numero;
                            }
                        }
                        if (existe)
                        {
                            texteTemp += "<a class=\"chercheur\" href=\"#RefSubmitter" + numeroSUBMITTER.ToString() + "\">" + numeroSUBMITTER.ToString() + "</a>" + ", ";
                        }
                        else
                        {
                            // trouver nouveau numero
                            numeroSUBMITTER = liste_SUBMITTER_ID_numero.Count + 1;
                            ID_numero info_DESI = new ID_numero
                            {
                                numero = numeroSUBMITTER,
                                ID = ID
                            };
                            liste_SUBMITTER_ID_numero.Add(info_DESI);
                            texteTemp += "<a class=\"chercheur\" href=\"#RefSubmitter" + numeroSUBMITTER.ToString() + "\">" + numeroSUBMITTER.ToString() + "</a>" + ", ";
                        }
                    }
                }
                texte += texteTemp.TrimEnd(' ', ',') + ".";
                texte += "\t\t\t\t\t\t" + " Voir le groupe chercheur.";
                texte += "\t\t\t\t\t</div>\n";
            }
            // si RFN *****************************************************************************************************
            if (infoIndividu.N1_RFN != null)
            {
                texte += Separation("moyen", 5);
                texte += "\t\t\t\t\t<div>\n";
                texte += "\t\t\t\t\t\tNuméro d'enregistrement permanent(RFN) \n";
                texte += "\t\t\t\t\t\t" + infoIndividu.N1_RFN + "\n";
                texte += "\t\t\t\t\t</div>\n";
            }
            // si AFN *****************************************************************************************************
            if (infoIndividu.N1_AFN != null)
            {
                texte += Separation("moyen", 5);
                texte += "\t\t\t\t\t<div>\n";
                texte += "\t\t\t\t\t\tNuméro de fichier ancestral(AFN) \n";
                texte += "\t\t\t\t\t\t" + infoIndividu.N1_AFN + "\n";
                texte += "\t\t\t\t\t</div>\n";
            }
            // si WWW
            if (infoIndividu.N1_WWW_liste != null)
            {
                foreach (string www in infoIndividu.N1_WWW_liste)
                {
                    texte += "\t\t\t\t<div>\n";
                    texte += "\t\t\t\t\t\t<span class=\"detail_col1\">\n";
                    texte += "\t\t\t\t\t\tPage Web\n";
                    texte += "\t\t\t\t\t</span>\n";
                    texte += "\t\t\t\t\t\t<span class=\"detail_col2\">\n";
                    texte += "\t\t\t\t\t\t" + www + "\n";
                    texte += "\t\t\t\t\t</span>\n";
                    texte += "\t\t\t\t</div>\n";
                }
            }
            // note ********************************************************************************************************
            if (infoIndividu.N1_NOTE_liste_ID != null)
            {
                texte += Separation("moyen", 5);
                //string note_texte = "\t\t\t\t<div>\n";
                texte += "\t\t\t\t\t<div>\n";
                (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                    Avoir_note_lien(infoIndividu.N1_NOTE_liste_ID,
                    note_liste_ID_numero,
                    citation_liste_ID_numero,
                    source_liste_ID_numero,
                    repo_liste_ID_numero,
                    5);
                texte += temp;
                texte += "\t\t\t\t\t</div>\n";
            }
            texte += "\t\t\t\t</div>\n";
            // chercheur
            if (infoIndividu.N1_SUBM_liste_ID.Count > 0 && GH.Properties.Settings.Default.voir_chercheur)
            {
                texte += Separation("mince", 5);
                texte += "\t\t\t\t\t<div>\n";
                texte += "\t\t\t\t\t\t" + " Voir chercheur ";
                string texteTemp = null;
                foreach (string ID in infoIndividu.N1_SUBM_liste_ID)
                {
                    if (liste_SUBMITTER_ID_numero.Count == 0)
                    {
                        numeroSUBMITTER = 1;
                        ID_numero info_ANCI = new ID_numero
                        {
                            numero = numeroSUBMITTER,
                            ID = ID
                        };
                        liste_SUBMITTER_ID_numero.Add(info_ANCI);
                        texteTemp += "<a class=\"chercheur\" href=\"#RefSubmitter" + numeroSUBMITTER.ToString() + "\">" + numeroSUBMITTER.ToString() + "</a>" + ", ";
                        numeroSUBMITTER++;
                    }
                    else
                    {
                        bool existe = false;
                        foreach (ID_numero IDListe in liste_SUBMITTER_ID_numero)
                        {
                            if (ID == IDListe.ID)
                            {
                                existe = true;
                                numeroSUBMITTER = IDListe.numero;
                            }
                        }
                        if (existe)
                        {
                            texteTemp += "<a class=\"chercheur\" href=\"#RefSubmitter" + numeroSUBMITTER.ToString() + "\">" + numeroSUBMITTER.ToString() + "</a>" + ", ";
                        }
                        else
                        {
                            // trouver nouveau numero
                            numeroSUBMITTER = liste_SUBMITTER_ID_numero.Count + 1;
                            ID_numero info_ANCI = new ID_numero
                            {
                                numero = numeroSUBMITTER,
                                ID = ID
                            };
                            liste_SUBMITTER_ID_numero.Add(info_ANCI);
                            texteTemp += "<a class=\"chercheur\" href=\"#RefSubmitter" + numeroSUBMITTER.ToString() + "\">" + numeroSUBMITTER.ToString() + "</a>" + ", ";
                        }
                    }
                }
                texte += texteTemp.TrimEnd(' ', ',') + ".";
                texte += "\t\t\t\t\t</div>\n";
            }
            // Date changement
            (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                    Date_Changement_Bloc(
                        infoIndividu.N1_CHAN,
                        note_liste_ID_numero,
                        citation_liste_ID_numero,
                        source_liste_ID_numero,
                        repo_liste_ID_numero,
                        4,
                        false);
            texte += temp;
            texte += "\t\t\t\t</div>\n";
            // @conjoint *****************************************************************************************************************************
            if (infoIndividu.N1_FAMS_liste_Conjoint.Count > 0)
            {
                groupe_conjoint = true;
                texte += Separation("large", 5);
                texte += "<a id=\"groupe_conjoint\"></a>\n";
                texte += Groupe("debut", 5);
                texte += "\t\t\t\t\t<table class=\"titre\">\n";
                texte += "\t\t\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t\t\t<td>\n";
                texte += "\t\t\t\t\t\t\t\tConjoint\n";
                texte += "\t\t\t\t\t\t\t</td>\n";
                texte += "\t\t\t\t\t\t</tr>\n";
                texte += "\t\t\t\t\t</table>\n";
                texte += Separation("mince", 5);
                texte += "\t\t\t\t<table class=\"tableau\">\n";
                texte += "\t\t\t\t\t<thead>\n";
                texte += "\t\t\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t\t\t<th class=\"cellule2LMB\" style=\"width:85px\">&nbsp;</th>\n";
                texte += "\t\t\t\t\t\t\t<th class=\"cellule2LMB\">Nom</th>\n";
                texte += "\t\t\t\t\t\t\t<th class=\"cellule2LMB date\">Naissance</th>\n";
                texte += "\t\t\t\t\t\t\t<th class=\"cellule2LMB date\">Décès</th>\n";
                texte += "\t\t\t\t\t\t</tr>\n";
                texte += "\t\t\t\t\t</thead>\n";
                foreach (GEDCOMClass.SPOUSE_TO_FAMILY_LINK infoLienConjoint in infoIndividu.N1_FAMS_liste_Conjoint)
                {
                    string IDFamilleConjoint = infoLienConjoint.N0_ID;
                    string txtIDFamille;
                    if (GH.Properties.Settings.Default.VoirID == true)
                    {
                        txtIDFamille = " [" + IDFamilleConjoint + "]";
                    }
                    else
                    {
                        txtIDFamille = null;
                    }
                    GEDCOMClass.FAM_RECORD infoFamilleConjoint = GEDCOMClass.Avoir_info_famille(IDFamilleConjoint);

                    string IDConjoint;
                    if (infoFamilleConjoint.N1_HUSB == IDIndividu)
                    {
                        IDConjoint = infoFamilleConjoint.N1_WIFE;

                    }
                    else
                    {
                        IDConjoint = infoFamilleConjoint.N1_HUSB;
                    }
                    GEDCOMClass.INDIVIDUAL_RECORD infoConjoint;
                    
                    (_, infoConjoint) = GEDCOMClass.Avoir_info_individu(IDConjoint);
                    string nomConjoint = GEDCOMClass.AvoirPremierNomIndividu(IDConjoint);

                    GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE NaissanceConjoint;
                    (_, NaissanceConjoint) = GEDCOMClass.AvoirEvenementNaissance(infoConjoint.N1_EVENT_Liste);
                    GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE DecesConjoint;
                    (_, DecesConjoint) = GEDCOMClass.AvoirEvenementDeces(infoConjoint.N1_EVENT_Liste);
                    texte += "\t\t\t\t\t<tr>\n";
                    texte += "\t\t\t\t\t\t<td class=\"cellule2LMF\">\n";
                    if (txtIDFamille != null)
                    {
                        if (menu)
                        {
                            texte += "\t\t\t\t\t\t\t<a class=\"ficheFamille\"  href=../familles/" +
                                infoLienConjoint.N0_ID + ".html></a>\n";
                        }
                        else
                        {
                            texte += "\t\t\t\t\t\t\t<a class=\"ficheFamilleGris\"></a>\n";
                        }
                    }
                    else
                    {
                        texte += "\t\t\t\t\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                    }
                    texte += "\t\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t\t<td class=\"cellule2LMF\">\n";
                    texte += "\t\t\t\t\t\t\t" + nomConjoint + "\n";

                    if (GH.Properties.Settings.Default.VoirID == true)
                    {
                        texte += "\t\t\t\t\t\t<span style=\"font-size:small\">[" +
                            IDConjoint + "]</span>" + "\n";
                    }
                    if (infoLienConjoint.N1_NOTE_liste_ID.Count > 0)
                    {
                        (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                        Avoir_note_lien(
                            infoLienConjoint.N1_NOTE_liste_ID,
                            note_liste_ID_numero,
                            citation_liste_ID_numero,
                            source_liste_ID_numero,
                            repo_liste_ID_numero,
                            7);
                        texte += "\t\t\t\t\t\t\t<br />\n" + temp;
                    }
                    texte += "\t\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t\t<td class=\"cellule2LMF\">\n";
                    string date = null;
                    if (GH.Properties.Settings.Default.date_longue)
                    {
                        date = GEDCOMClass.ConvertirDateTexte(NaissanceConjoint.N2_DATE);
                    }
                    else
                    {
                        date = NaissanceConjoint.N2_DATE;
                    }
                    texte += "\t\t\t\t\t\t\t" + date + "\n";
                    texte += "\t\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t\t<td class=\"cellule2LMF\">\n";
                    if (GH.Properties.Settings.Default.date_longue)
                    {
                        date = GEDCOMClass.ConvertirDateTexte(DecesConjoint.N2_DATE);
                    }
                    else
                    {
                        date = DecesConjoint.N2_DATE;
                    }
                    texte += "\t\t\t\t\t\t\t" + date + "\n";
                    texte += "\t\t\t\t\t\t</td>\n";
                    texte += "\t\t\t\t\t</tr>\n";
                }
                texte += "\t\t\t\t</table>\n";
                texte += Groupe("fin", 3);
            }
            // @Parent Groupe ******************************************************************************************************************************
            GEDCOMClass.CHILD_TO_FAMILY_LINK infoFamille = GEDCOMClass.AvoirInfoFamilleEnfant(IDIndividu);
            if (
                infoFamille.N0_FAMC != null ||
                infoFamille.N1_PEDI != null ||
                infoFamille.N1_STAT != null ||
                infoFamille.N1_NOTE_liste_ID != null
                )
            {
                groupe_parent = true;
                // père
                string pereID = GEDCOMClass.Avoir_famille_conjoint_ID(infoFamille.N0_FAMC);
                GEDCOMClass.INDIVIDUAL_RECORD infoPere;
                (_, infoPere) = GEDCOMClass.Avoir_info_individu(pereID);
                string nomPere = Avoir_premier_nom_individu(infoPere);
                GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE NaissancePere;
                (_, NaissancePere) = GEDCOMClass.AvoirEvenementNaissance(infoPere.N1_EVENT_Liste);
                GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE DecesPere;
                (_, DecesPere) = GEDCOMClass.AvoirEvenementDeces(infoPere.N1_EVENT_Liste);
                // mère                
                string mereID = GEDCOMClass.Avoir_famille_conjointe_ID(infoFamille.N0_FAMC);
                GEDCOMClass.INDIVIDUAL_RECORD infoMere;
                (_, infoMere) = GEDCOMClass.Avoir_info_individu(mereID);
                string nomMere = Avoir_premier_nom_individu(infoMere);
                GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE NaissanceMere;
                (_, NaissanceMere) = GEDCOMClass.AvoirEvenementNaissance(infoMere.N1_EVENT_Liste);
                GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE DecesMere;
                (_, DecesMere) = GEDCOMClass.AvoirEvenementDeces(infoMere.N1_EVENT_Liste);
                // afficher ou pas le ID
                string txtIDPere = null;
                string txtIDMere = null;
                if (GH.Properties.Settings.Default.VoirID == true)
                {
                    if (pereID != null && nomPere != null)
                    {
                        txtIDPere = " [" + pereID + "]";
                    }
                    if (mereID != null && nomMere != null)
                    {
                        txtIDMere = " [" + mereID + "]";
                    }
                }
                texte += Separation("mince", 5);
                texte += "<a id=\"groupe_parent\"></a>\n";
                texte += Groupe("debut", 3);
                texte += "\t\t\t\t<table class=\"titre\">\n";
                texte += "\t\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t\t<td>\n";
                texte += "\t\t\t\t\t\tParent\n";
                if (GH.Properties.Settings.Default.VoirID == true)
                {
                    texte += "\t\t\t\t\t\t<div style=\"font-size:small\">[" + infoFamille.N0_FAMC + "]</div>" + "\n";
                }
                texte += "\t\t\t\t\t</td>\n";
                texte += "\t\t\t\t</tr>\n";
                texte += "\t\t\t\t</table>\n";
                texte += Separation("mince", 5);
                texte += "\t\t\t\t<table class=\"tableau\">\n";
                texte += "\t\t\t\t\t<thead>\n";
                texte += "\t\t\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t\t\t<th class=\"cellule2LMB\" style=\"width:85px\">\n";
                if (infoFamille.N0_FAMC != null)
                {
                    if (menu)
                    {
                        texte += "\t\t\t\t\t\t\t\t<a class=\"ficheFamille\"  href=\"../familles/" + infoFamille.N0_FAMC + ".html\"></a>\n";
                    }
                    else
                    {
                        texte += "\t\t\t\t\t\t\t\t<a class=\"ficheFamilleGris\"></a>\n";
                    }
                }
                else
                {
                    texte += "\t\t\t\t\t\t\t\t<a class=\"ficheFamilleGris\"></a>\n";
                }
                texte += "\t\t\t\t\t\t\t</th>\n";
                texte += "\t\t\t\t\t\t\t<th class=\"cellule2LMB\">Nom</th>\n";
                texte += "\t\t\t\t\t\t\t<th class=\"date cellule2LMB\">Naissance</th>\n";
                texte += "\t\t\t\t\t\t\t<th class=\"date cellule2LMB\">Décès</th>\n";
                texte += "\t\t\t\t\t\t</tr>\n";
                texte += "\t\t\t\t\t</thead>\n";
                texte += "\t\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t\t<td class=\"cellule2LMF\" style=\"width:85px\">\n";
                if (pereID != null)
                {
                    if (menu)
                    {
                        texte += "\t\t\t\t\t\t\t<a class=\"ficheIndividu\"  href=\"" + pereID + ".html\"></a>\n";
                    }
                    else
                    {
                        texte += "\t\t\t\t\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                    }
                }
                else
                {
                    texte += "\t\t\t\t\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                }
                texte += "\t\t\t\t\t\t\tPère\n";
                texte += "\t\t\t\t\t\t</td>\n";
                texte += "\t\t\t\t\t\t<td class=\"cellule2LMF\">" + nomPere + txtIDPere + "</td>\n";
                string date;
                if (GH.Properties.Settings.Default.date_longue)
                {
                    date = GEDCOMClass.ConvertirDateTexte(NaissancePere.N2_DATE);
                }
                else
                {
                    date = NaissancePere.N2_DATE;
                }
                texte += "\t\t\t\t\t\t<td class=\"date cellule2LMF\">" + date + "</td>\n";
                if (GH.Properties.Settings.Default.date_longue)
                {
                    date = GEDCOMClass.ConvertirDateTexte(DecesPere.N2_DATE);
                }
                else
                {
                    date = DecesPere.N2_DATE;
                }
                texte += "\t\t\t\t\t\t<td class=\"date cellule2LMF\">" + date + "</td>\n";
                texte += "\t\t\t\t\t</tr>\n";
                texte += "\t\t\t\t\t<tr>\n";
                texte += "\t\t\t\t\t\t<td class=\"cellule2LMf\" style=\"width:85px\">\n";
                if (mereID != null)
                {
                    if (menu)
                    {
                        texte += "\t\t\t\t\t\t\t<a class=\"ficheIndividu\"  href=\"" +
                            mereID + ".html\"></a>\n";
                    }
                    else
                    {
                        texte += "\t\t\t\t\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                    }
                }
                else
                {
                    texte += "\t\t\t\t\t\t\t<a class=\"ficheIndividuGris\"></a>\n";
                }
                texte += "\t\t\t\t\t\t\tMère\n";
                texte += "\t\t\t\t\t\t</td>\n";
                texte += "\t\t\t\t\t\t<td class=\"cellule2LMF\">" + nomMere + txtIDMere + "</td>\n";
                if (GH.Properties.Settings.Default.date_longue)
                {
                    date = GEDCOMClass.ConvertirDateTexte(NaissanceMere.N2_DATE);
                }
                else
                {
                    date = NaissanceMere.N2_DATE;
                }
                texte += "\t\t\t\t\t\t<td class=\"date cellule2LMF\">" + date + "</td>\n";
                if (GH.Properties.Settings.Default.date_longue)
                {
                    date = GEDCOMClass.ConvertirDateTexte(DecesMere.N2_DATE);
                }
                else
                {
                    date = DecesMere.N2_DATE;
                }
                texte += "\t\t\t\t\t\t<td class=\"date cellule2LMF\">" + date + "</td>\n";
                texte += "\t\t\t\t\t</tr>\n";
                texte += "\t\t\t\t</table>\n";
                if (infoFamille.N1_PEDI != null || infoFamille.N1_STAT != null || infoFamille.N1_NOTE_liste_ID != null)
                {
                    texte += "\t\t\t<div style=\"border:2px solid #000;border-top:0px solid #000;padding:10px;\">\n";
                    if (infoFamille.N1_PEDI != null)
                    {
                        texte += Separation("mince", 5);
                        texte += "\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                        texte += "\t\t\t\t\t\tPédigrée\n";
                        texte += "\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                        texte += "\t\t\t\t\t\t" + infoFamille.N1_PEDI + "\n";
                        texte += "\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t</div>\n";
                    }
                    if (infoFamille.N1_STAT != null)
                    {
                        texte += Separation("mince", 5);
                        texte += "\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col1\">\n";
                        texte += "\t\t\t\t\t\tStatus\n";
                        texte += "\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                        texte += "\t\t\t\t\t\t" + infoFamille.N1_STAT + "\n";
                        texte += "\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t</div>\n";
                    }
                    if (infoFamille.N1_NOTE_liste_ID != null)
                    {
                        texte += Separation("mince", 5);
                        texte += "\t\t\t\t<div>\n";
                        texte += "\t\t\t\t\t<span class=\"detail_col2\">\n";
                        (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                            Avoir_note_lien(
                            infoFamille.N1_NOTE_liste_ID,
                            note_liste_ID_numero,
                            citation_liste_ID_numero,
                            source_liste_ID_numero,
                            repo_liste_ID_numero,
                            6);
                        texte += temp;
                        texte += "\t\t\t\t\t</span>\n";
                        texte += "\t\t\t\t</div>\n";
                    }
                    texte += "\t\t\t</div>\n";
                }
                texte += Groupe("fin", 3);
            }
            //@média Groupe ********************************************************************************************************************************
            {
                if (GH.Properties.Settings.Default.voir_media)
                {
                    if (infoIndividu.N1_OBJE_liste != null)
                    {
                        groupe_media = true;
                        int totalMedia = infoIndividu.N1_OBJE_liste.Count;
                        int nombreMedia = 0;
                        foreach (string IDMedia in infoIndividu.N1_OBJE_liste)
                        {
                            if (IDMedia != IDPortrait)
                            {
                                nombreMedia++;
                            }
                        }
                        if (nombreMedia > 0)
                        {
                            texte += Separation("large", 3);
                            texte += "<a id=\"groupe_media\"></a>\n";
                            texte += Groupe("debut", 3);
                            texte += "\t\t\t\t<table class=\"titre\">\n";
                            texte += "\t\t\t\t\t<tr>\n";
                            texte += "\t\t\t\t\t\t<td>\n";
                            if (nombreMedia == 1)
                                texte += "\t\t\t\t\t\t\tMédia\n";
                            else
                                texte += "\t\t\t\t\t\t\tMédias\n";
                            texte += "\t\t\t\t\t\t</td>\n";
                            texte += "\t\t\t\t\t</tr>\n";
                            texte += "\t\t\t\t</table>\n";
                            texte += Separation("mince", 4);
                            texte += "\t\t\t\t<table class=\"tableauMedia\">\n";
                            texte += "\t\t\t\t\t<tr>\n";
                            int cRanger = 0;
                            foreach (string IDMedia in infoIndividu.N1_OBJE_liste)
                            {
                                GEDCOMClass.MULTIMEDIA_RECORD mediaInfo = GEDCOMClass.AvoirMedia(IDMedia);
                                if (mediaInfo != null)
                                {
                                    texte += "\t\t\t\t\t\t<td class=\"tableauMedia\">\n";
                                    (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                                        Objet(
                                            mediaInfo,
                                            sousDossier,
                                            dossierSortie,
                                            note_liste_ID_numero,
                                            citation_liste_ID_numero,
                                            source_liste_ID_numero,
                                            repo_liste_ID_numero,
                                            6);
                                    texte += temp;
                                    texte += "\t\t\t\t\t\t</td>\n";
                                }
                                cRanger++;

                                if (cRanger % 3 == 0 && cRanger != totalMedia)
                                {

                                    texte += "\t\t\t\t\t</tr>\n";
                                    texte += "\t\t\t\t\t<tr>\n";
                                }
                            }
                            if (cRanger % 3 == 1)
                            {
                                texte += "\t\t\t\t\t\t<td class=\"tableauMedia\">\n";
                                texte += "\t\t\t\t\t\t</td>\n";
                                texte += "\t\t\t\t\t\t<td class=\"tableauMedia\">\n";
                                texte += "\t\t\t\t\t\t</td>\n";
                            }
                            if (cRanger % 3 == 2)
                            {
                                texte += "\t\t\t\t\t\t<td class=\"tableauMedia\">\n";
                                texte += "\t\t\t\t\t\t</td>\n";
                            }
                            texte += "\t\t\t\t\t</tr>\n";
                            texte += "\t\t\t\t</table>\n";
                            texte += Groupe("fin", 3);
                        }
                    }
                }
            }
            //@ordonnance ****************************************************************************************************************************
            (temp, numeroCarte, groupe_ordonnance, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                Groupe_ordonnance(
                    infoIndividu.N1_LDS_liste, 
                    dateNaissance, 
                    numeroCarte,
                    note_liste_ID_numero,
                    citation_liste_ID_numero,
                    source_liste_ID_numero,
                    repo_liste_ID_numero,
                    3);
            texte += temp;
            //@Événement ****************************************************************************************************************************
            (temp, numeroCarte, groupe_evenement, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                Groupe_evenement(
                    infoIndividu.N1_EVENT_Liste, 
                    dateNaissance,
                    "",
                    "",
                    numeroCarte,
                    sousDossier,
                    dossierSortie,
                    note_liste_ID_numero,
                    citation_liste_ID_numero,
                    source_liste_ID_numero,
                    repo_liste_ID_numero,
                    3);
            texte += temp;
            //@Attribut *************************************************************************************
            (temp, _, groupe_attribut, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                Groupe_attribut(
                    infoIndividu.N1_Attribute_liste,
                    numeroCarte,
                    sousDossier,
                    dossierSortie,
                    note_liste_ID_numero,
                    citation_liste_ID_numero,
                    source_liste_ID_numero,
                    repo_liste_ID_numero,
                    0);
            texte += temp;
            //@chercheur Groupe
            (temp, groupe_chercheur, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                Groupe_chercheur(
                liste_SUBMITTER_ID_numero, 
                dossierSortie, 
                "individus", 
                note_liste_ID_numero,
                citation_liste_ID_numero,
                source_liste_ID_numero,
                repo_liste_ID_numero,
                3);
            texte += temp;
            // @citation ************************************************************************************************************************
            if (citation_liste_ID_numero.Count > 0)
            {
                // Générer le texte HTML
                (temp, groupe_citation, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                        Groupe_citation(
                        dossierSortie,
                        "individus",
                        note_liste_ID_numero,
                        citation_liste_ID_numero,
                        source_liste_ID_numero,
                        repo_liste_ID_numero,
                        3);
                texte += temp;
            }
            // @source ************************************************************************************************************************
            if (source_liste_ID_numero.Count() > 0)
            {
                // Générer le texte HTML
                (
                    temp, 
                    groupe_source,
                    note_liste_ID_numero,
                    citation_liste_ID_numero,
                    source_liste_ID_numero,
                    repo_liste_ID_numero
                ) = 
                    Groupe_source(
                        dossierSortie,
                        "individus",
                        note_liste_ID_numero,
                        citation_liste_ID_numero,
                        source_liste_ID_numero,
                        repo_liste_ID_numero,
                        3);
                texte += temp;
                
            }
            // @Depot Groupe **********************************************************************************************
            (temp, groupe_depot, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) = 
                Groupe_depot(
                    //liste_REPOSITORY_RECORDPlus,
                    liste_note_ID_numero, 
                    citation_liste_ID_numero, 
                    source_liste_ID_numero,
                    repo_liste_ID_numero,
                    3);
            texte += temp;
            // @note Groupe **********************************************************************************************
            if (liste_note_ID_numero.Count() > 0)
            {
                (temp, groupe_note, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) = 
                    Groupe_note(
                        "individu",
                        note_liste_ID_numero,
                        citation_liste_ID_numero,
                        source_liste_ID_numero,
                        repo_liste_ID_numero,
                        3);
                texte += temp;
                temp_liste = note_liste_ID_numero;
                temp_liste.Clear();
                temp_liste = citation_liste_ID_numero;
                temp_liste.Clear();
                temp_liste = source_liste_ID_numero;
                temp_liste.Clear();
                temp_liste = repo_liste_ID_numero;
                temp_liste.Clear();
            }
            // menu hamburger
            texte += "\t\t\t\t<div class=\"hamburger\">\n";
            texte += "\t\t\t\t\t<button class=\"boutonHamburger\"></button>\n";
            texte += "\t\t\t\t\t<div class=\"hamburger-contenu\">\n";
            texte += "\t\t\t\t\t\t<a href=\"#groupe_nom\">Nom</a>\n";
            if (groupe_conjoint) texte += "\t\t\t\t\t\t<a href=\"#groupe_conjoint\">Conjoint</a>\n";
            if (groupe_parent) texte += "\t\t\t\t\t\t<a href=\"#groupe_parent\">Parent</a>\n";
            if (groupe_media) texte += "\t\t\t\t\t\t<a href=\"#groupe_media\">Média</a>\n";
            if (groupe_ordonnance) texte += "\t\t\t\t\t\t<a href=\"#groupe_ordonnance\">Ordonnance</a>\n";
            if (groupe_evenement) texte += "\t\t\t\t\t\t<a href=\"#groupe_evenement\">Événement</a>\n";
            if (groupe_attribut) texte += "\t\t\t\t\t\t<a href=\"#groupe_attribut\">Attribut</a>\n";
            if (groupe_chercheur) texte += "\t\t\t\t\t\t<a href=\"#groupe_chercheur\">Chercheur</a>\n";
            if (groupe_citation) texte += "\t\t\t\t\t\t<a href=\"#groupe_citation\">Citation</a>\n";
            if (groupe_source) texte += "\t\t\t\t\t\t<a href=\"#groupe_source\">Source</a>\n";
            if (groupe_depot) texte += "\t\t\t\t\t\t<a href=\"#groupe_depot\">Dépôt</a>\n";
            if (groupe_note) texte += "\t\t\t\t\t\t<a href=\"#groupe_note\">Note</a>\n";
            texte += "\t\t\t\t\t</div>\n";
            texte += "\t\t\t\t</div>\n";
            // bas de page
            texte += Bas_Page();
            try
            {
                System.IO.File.WriteAllText(nomFichierIndividu, texte);
            }
            catch (Exception msg)
            {
                MessageErreur("Ne peut pas écrire le fichier" + nomFichierIndividu + ".\r\n\r\n" + msg.Message);
            }
            return IDCourantIndividu;
        }
        public string Individu_Index_Bouton()
        {
            string texte = null;
            bool[] alphabette = new bool[27];
            ListView lvChoixIndividu = Application.OpenForms["GH"].Controls["lvChoixIndividu"] as ListView;
            for (int f = 0; f < 27; f++)
            {
                alphabette[f] = false;
            }
            // détermine si lettre est utiliser
            for (int f = 0; f < lvChoixIndividu.Items.Count; f++)
            {
                int v;
                if (lvChoixIndividu.Items[f].SubItems[1].Text != "")
                {
                    if (lvChoixIndividu.Items[f].SubItems[1].Text[0].ToString() != "")
                    {
                        char l = lvChoixIndividu.Items[f].SubItems[1].Text[0];
                        v = (int)l - 65;
                        if (v > -1 && v < 26)
                        {
                            alphabette[v] = true;
                        }
                        else
                        {
                            v = 26;
                            alphabette[v] = true;
                        }
                    }
                    else
                    {
                        v = 26;
                        alphabette[v] = true;
                    }
                }
                else
                {
                    v = 26;
                    alphabette[v] = true;
                }
            }
            texte += "\t\t\t<div style=\"width:860px;border-radius:10px;background-color:#6464FF;margin-left:auto;margin-right:auto;padding:5px;\">\n";
            for (int f = 0; f < 26; f++)
            {
                if (alphabette[f])
                {
                    texte += "\t\t\t\t<a class=\"index\" href=\"" + (char)(f + 65) + ".html\" >\n";
                    texte += "\t\t\t\t\t" + (char)(f + 65) + "\n";
                    texte += "\t\t\t\t</a>\n";
                }
                else
                {
                    texte += "\t\t\t\t<div class=\" index\">\n";
                    texte += "\t\t\t\t\t" + (char)(f + 65) + "\n";
                    texte += "\t\t\t\t</div>\n";
                }
            }
            if (alphabette[26])
            {
                texte += "\t\t\t\t<a class=\"index\" href=\"diver.html\" >\n";
                texte += "\t\t\t\t&\n";
                texte += "\t\t\t\t</a>\n";
            }
            else
            {
                texte += "\t\t\t\t<div class=\" index\">\n";
                texte += "\t\t\t\t\t&\n";
                texte += "\t\t\t\t</div>\n";
            }
            texte += "\t\t\t</div>\n";
            return texte;
        }
        private static void MessageErreur(string message, [CallerLineNumber] int lineNumber = 0)
        {
            MessageBox.Show(message, "Erreur H" + lineNumber);
        }
        private (string, bool, List<ID_numero>, List<ID_numero>, List<ID_numero>, List<ID_numero>) Groupe_citation(
            string dossierSortie,
            string sousDossier,
            List<ID_numero> note_liste_ID_numero,
            List<ID_numero> citation_liste_ID_numero,
            List<ID_numero> source_liste_ID_numero,
            List<ID_numero> repo_liste_ID_numero,
            int tab = 0)
        {
            try
            {

                if (citation_liste_ID_numero == null)
                    return ("", false, note_liste_ID_numero, citation_liste_ID_numero,
                        source_liste_ID_numero, repo_liste_ID_numero);
                string espace = Tabulation(tab);
                string texte = Separation("large", tab);
                string temp;
                texte += "<a id=\"groupe_citation\"></a>\n";
                texte += Groupe("debut", tab);
                texte += espace + "\t<table class=\"titre\">\n";
                texte += espace + "\t\t<tr>\n";
                texte += espace + "\t\t\t<td>\n";
                texte += espace + "\t\t\t\t<img  style=\"vertical-align:middle;heigh:64px\" src=\"..\\commun\\citation.svg\" />\n";
                if (citation_liste_ID_numero.Count == 1)
                    texte += espace + "\t\t\t\t Citation\n";
                else
                    texte += espace + "\t\t\t\t Citations\n";
                texte += espace + "\t\t\t</td>\n";
                texte += espace + "\t\t</tr>\n";
                texte += espace + "\t</table>\n";
                foreach (ID_numero item_citation in citation_liste_ID_numero)
                {
                    // avoir l'info de source citation
                    GEDCOMClass.SOURCE_CITATION info_citation = GEDCOMClass.Avoir_info_citation(item_citation.ID);
                    texte += espace + "\t<span style=\"display: table-cell;\"><a id=\"citation-" +
                        item_citation.numero.ToString() + "\"></a></span>\n";
                    texte += Separation("mince", tab + 1);

                    texte += espace + "\t<table class=\"tableau\">\n";
                    if (info_citation.N0_Titre != null)
                    {
                        texte += espace + "\t\t<tr>\n";
                        texte += espace + "\t\t\t<td style=\"background-color:#BBB;border:2px solid #000;margin-left:5px;vertical-align:top;\">\n";// ne pas enlever Border.
                        texte += espace + "\t\t\t\t<span class=\"citation\">" + item_citation.numero.ToString() + " </span>\n";
                        texte += espace + "\t\t\t\t<span style=\"display:table-cell;text-align:top; padding-left:5px\">" + info_citation.N0_Titre + "</span>\n";
                        texte += espace + "\t\t\t</td>\n";
                        texte += espace + "\t\t</tr>\n";
                        
                    }
                    if (info_citation.N0_ID_source != null || info_citation.N1_PAGE != null)
                    {
                        GEDCOMClass.SOURCE_RECORD info_source = new GEDCOMClass.SOURCE_RECORD();
                        if (info_citation.N0_ID_source != null)
                        {
                            info_source = GEDCOMClass.Avoir_info_SOURCE(info_citation.N0_ID_source);
                        }

                        texte += espace + "\t\t<tr>\n";
                        texte += espace + "\t\t\t<td style=\"background-color:#BBB;border:2px solid #000;margin-left:5px;vertical-align:top;\">\n";// ne pas enlever Border.
                        if (info_source != null)
                        {
                            int numero = Avoir_numero_de_la_source(info_source.N0_ID, source_liste_ID_numero);

                            texte += espace + "\t\t\t\t<span class=\"citation\">" + item_citation.numero.ToString() + " </span>\n";
                            texte += espace + "\t\t\t\t<span style=\"display:table-cell;text-align:top; padding-left:5px\">Voir la source <a class=\"source\" href=\"#source-" + numero.ToString() + "\">" + numero.ToString() + "</a>";
                        }
                        if (info_citation.N1_PAGE != null)
                            texte += espace + "\t\t\t\tpage " + info_citation.N1_PAGE;
                        texte += espace + "\t\t\t\t</span>\n";
                        texte += espace + "\t\t\t</td>\n";
                        texte += espace + "\t\t</tr>\n";
                    }
                    // media
                    if (GH.Properties.Settings.Default.voir_media)
                    {
                        if (info_citation.N1_OBJE_ID_liste.Count > 0)
                        {
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td>\n";
                            texte += espace + "\t\t\t\t<table class=\"tableauMedia retrait_point5in\" style=\"border:2px;width:480px\">\n";
                            texte += espace + "\t\t\t\t\t<tr>\n";
                            int cRanger = 0;
                            int totalOBJE = info_citation.N1_OBJE_ID_liste.Count;
                            foreach (string media in info_citation.N1_OBJE_ID_liste)
                            {
                                GEDCOMClass.MULTIMEDIA_RECORD OBJE = GEDCOMClass.AvoirMedia(media);
                                texte += espace + "\t\t\t\t\t\t<td class=\"tableauMedia\" style=\"width:50%\">\n";
                                (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                                Objet(
                                    OBJE,
                                    sousDossier,
                                    dossierSortie,
                                    note_liste_ID_numero,
                                    citation_liste_ID_numero,
                                    source_liste_ID_numero,
                                    repo_liste_ID_numero,
                                    tab + 6);
                                texte += temp;
                                texte += espace + "\t\t\t\t\t\t</td>\n";
                                cRanger++;
                                if (cRanger % 2 == 0 && cRanger != totalOBJE)
                                {
                                    texte += espace + "\t\t\t\t\t</tr>\n";
                                    texte += espace + "\t\t\t\t\t<tr>\n";
                                }
                            }
                            if (cRanger % 2 == 1)
                            {
                                texte += espace + "\t\t\t\t\t\t<td class=\"tableauMedia\" style=\"border:0px;width:50%\">\n";
                                texte += "\t\t\t\t\t\t</td>\n";
                            }
                            texte += espace + "\t\t\t\t\t</tr>\n";
                            texte += espace + "\t\t\t\t</table>\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                    }
                    // citation
                    if (info_citation.N1_EVEN != null)
                    {
                        texte += espace + "\t\t<tr>\n";
                        texte += espace + "\t\t\t<td>\n";
                        texte += espace + "\t\t\t\t<div style=\"border:0px solid #00F;padding-left:.5in;\">Événement: " +
                            GEDCOMClass.Convertir_EVENT_titre(info_citation.N1_EVEN) + "\n";
                        texte += espace + "\t\t\t\t</div>\n";
                        if (info_citation.N2_EVEN_ROLE != null)
                        {
                            texte += espace + "\t\t\t\t<div style=\"border:0px solid #00F;padding-left:.5in;\">Role dans l'événement: " +
                                GEDCOMClass.Convertir_Sujet(info_citation.N2_EVEN_ROLE) + "\n";
                            texte += espace + "\t\t\t\t</div>\n";
                        }
                        texte += Separation("mince", tab + 4);
                        texte += espace + "\t\t\t</td>\n";
                        texte += espace + "\t\t</tr>\n";
                    }
                    // DATA
                    if (info_citation.N2_DATA_DATE != "" && info_citation.N2_DATA_DATE != null)
                    {
                        texte += espace + "\t\t<tr>\n";
                        texte += espace + "\t\t\t<td>\n";
                        texte += espace + "\t\t\t\t<div style=\"border:0px solid #00F;padding-left:.5in;\">Data: ";
                        if (GH.Properties.Settings.Default.date_longue)
                        {
                            texte += GEDCOMClass.ConvertirDateTexte(info_citation.N2_DATA_DATE);
                        }
                        else
                        {
                            texte += info_citation.N2_DATA_DATE;
                        }
                        texte += espace + "\t\t\t\t</div>\n";
                        if (info_citation.N2_DATA_TEXT != "" && info_citation.N2_DATA_TEXT != null)
                        {
                            texte += espace + "\t\t\t\t<div style=\"border:0px solid #00F;padding-left:1in;\">Texte: " +
                                info_citation.N2_DATA_TEXT + "</div>\n";
                        }
                        texte += Separation("mince", tab + 4);
                        texte += espace + "\t\t\t</td>\n";
                        texte += espace + "\t\t</tr>\n";
                    }
                    // QUAY
                    if (info_citation.N1_QUAY != null)
                    {
                        string temp2 = "";
                        if (info_citation.N1_QUAY == "0")
                        {
                            temp2 = "Preuve non fiable ou données estimées.";
                        }
                        else if (info_citation.N1_QUAY == "1")
                        {
                            temp2 = "Fiabilité douteuse des preuves (entretiens, recensement, généalogies orales ou potentiel de biais par exemple, une autobiographie).";
                        }
                        else if (info_citation.N1_QUAY == "2")
                        {
                            temp2 = "Preuve secondaire, données officiellement enregistrées quelque temps après l'évènement.";
                        }
                        else if (info_citation.N1_QUAY == "3")
                        {
                            temp2 = "Preuve directe et principale utilisée, ou par dominance de la preuve.";
                        }
                        texte += espace + "\t\t<tr>\n";
                        texte += espace + "\t\t\t<td>\n";
                        texte += espace + "\t\t\t\t<div style=\"border:0px solid #00F;padding-left:.5in;\">Crédibilité: " + temp2 + "</div>\n";
                        texte += Separation("mince", tab + 4);
                        texte += espace + "\t\t\t</td>\n";
                        texte += espace + "\t\t</tr>\n";
                    }
                    // note
                    if (info_citation.N1_NOTE_liste_ID != null)
                    {
                        if (info_citation.N1_NOTE_liste_ID.Count > 0)
                        {
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td style=\"border:0px solid #0e0;text-align:left;padding-left:.5in\">\n";
                            
                            (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                                Avoir_note_lien(
                                    info_citation.N1_NOTE_liste_ID,
                                    note_liste_ID_numero,
                                    citation_liste_ID_numero,
                                    source_liste_ID_numero,
                                    repo_liste_ID_numero,
                                    tab + 4);
                            texte += temp;
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                    }
                    texte += espace + "\t</table>\n";
                }
                texte += Groupe("fin", tab);
                return (texte, true, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
            }
            catch (Exception msg)
            {
                AutoClosingMessageBox.Show("Une erreur c'est produit dans le programme.\n\r" +
                    "L'erreur a été enregisté dans le fichier " + GH.Properties.Settings.Default.DossierHTML + "\\erreur.txt\r\n" +
                    "La section Citation sera manquante pour cet infdividu.",
                    "Erreur",
                    15000);
                GEDCOMClass.Erreur_log(GEDCOMClass.Avoir_erreur_ligne(msg) + " " + msg.Message);
                return (null, true, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
            }
        }
        private (string, bool, List<ID_numero>, List<ID_numero>, List<ID_numero>, List<ID_numero>) Groupe_note(
            string sousDossier,
            List<ID_numero> note_liste_ID_numero,
            List<ID_numero> citation_liste_ID_numero,
            List<ID_numero> source_liste_ID_numero,
            List<ID_numero> repo_liste_ID_numero,
            int tab = 0)
        {
            if (note_liste_ID_numero == null) 
                return ("", false, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
            string temp;
            string espace = Tabulation(tab);
            string texte = Separation("large", tab);
            texte += "<a id=\"groupe_note\"></a>";
            texte += Groupe("debut", tab);
            texte += espace + "\t<table class=\"titre\">\n";
            texte += espace + "\t\t<tr>\n";
            texte += espace + "\t\t\t<td>\n";
            if (sousDossier == "index")
                texte += espace + "\t\t\t\t<img  style=\"vertical-align:middle;heigh:64px\" src=\"commun\\note.svg\" />\n";
            else
                texte += espace + "\t\t\t\t<img  style=\"vertical-align:middle;heigh:64px\" src=\"..\\commun\\note.svg\" />\n";
            if (note_liste_ID_numero.Count == 1) 
                texte += espace + "\t\t\t\tNote\n"; 
            else 
                texte += espace + "\t\t\t\tNotes\n";
            texte += espace + "\t\t\t</td>\n";
            texte += espace + "\t\t</tr>\n";
            texte += espace + "\t</table>\n";
            texte += Separation("mince", tab + 1);
            foreach (ID_numero note_ID_numero in note_liste_ID_numero)
            {
                GEDCOMClass.NOTE_RECORD info_note = GEDCOMClass.Avoir_Info_Note(note_ID_numero.ID);
                texte += espace + "\t<table class=\"tableau\">\n";
                GEDCOM.GEDCOMClass.SOURCE_RECORD infoSourceDetail = GEDCOMClass.AvoirInfoSource(info_note.N0_ID);
                {
                    texte += espace + "\t\t<tr>\n";
                    texte += espace + "\t\t\t<td style=\"background-color:#BBB;border:2px solid #000;margin-left:5px;vertical-align:top;\">\n";// ne pas enlever Border.
                    texte += espace + "\t\t\t\t<span style=\"display: table-cell;\"><a id=\"note-" + note_ID_numero.numero.ToString() + "\"></a></span>\n";
                    texte += espace + "\t\t\t\t<span class=\"note\">" + note_ID_numero.numero.ToString() + "</span>\n";
                    texte += espace + "\t\t\t\t<span style=\"display:table-cell;text-align:top; padding-left:5px\">\n";
                    texte += espace + "\t\t\t\t\t" + info_note.N0_NOTE_Texte + "\n";
                    if (GH.Properties.Settings.Default.deboguer) texte += "\t\t\t\t {" + info_note.N0_ID + "}\n";
                    texte += espace + "\t\t\t\t</span>\n";
                    (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) = 
                        Avoir_citation_source_lien(
                            info_note.N1_SOUR_citation_liste_ID,
                            note_liste_ID_numero, 
                            citation_liste_ID_numero, 
                            source_liste_ID_numero,
                            repo_liste_ID_numero,
                            tab + 4);
                    texte += temp;
                    texte += Separation("mince", tab + 4);
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t</tr>\n";
                }
                if (info_note.N1_REFN_liste != null)
                {
                    foreach (GEDCOMClass.USER_REFERENCE_NUMBER info in info_note.N1_REFN_liste)
                    {
                        texte += espace + "\t\t<tr>\n";
                        texte += espace + "\t\t\t<td>\n";
                        texte += espace + "\t\t\t\tRéférence " + info.N0_REFN + " type " + info.N1_TYPE + "\n";
                        texte += espace + "\t\t\t</td>\n";
                        texte += espace + "\t\t</tr>\n";
                    }
                }
                if (info_note.N1_RIN != null)
                {
                    texte += espace + "\t\t<tr>\n";
                    texte += espace + "\t\t\t<td>\n";
                    texte += espace + "\t\t\t\tID d'enregistrement automatisé(RIN) " + info_note.N1_RIN + "\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t</tr>\n";
                }
                if (info_note.N1_CHAN.N1_CHAN_DATE != null && GH.Properties.Settings.Default.voir_date_changement)
                {
                    texte += espace + "\t\t<tr>\n";
                    texte += espace + "\t\t\t<td>\n";
                    (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                    Date_Changement_Bloc(
                        info_note.N1_CHAN,
                        note_liste_ID_numero,
                        citation_liste_ID_numero,
                        source_liste_ID_numero,
                        repo_liste_ID_numero,
                        tab + 3,
                        false);
                    texte += temp;
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t</tr>\n";
                }
                texte += espace + "\t</table>\n";
                texte += Separation("mince", tab + 1);
            }
            texte += Groupe("fin", tab);
            return (texte, true, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
        }
        private (string, bool, List<ID_numero>, List<ID_numero>, List<ID_numero>, List<ID_numero>) Groupe_source(
            string dossierSortie,
            string sousDossier,
            List<ID_numero> note_liste_ID_numero,
            List<ID_numero> citation_liste_ID_numero,
            List<ID_numero> source_liste_ID_numero,
            List<ID_numero> repo_liste_ID_numero,
            int tab = 0)
        {
            if (source_liste_ID_numero == null) 
                return ("", false, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
            string espace = Tabulation(tab);
            string texte = Separation("large", tab);
            string temp;
            texte += "<a id=\"groupe_source\"></a>\n";
            texte += Groupe("debut", tab);
            texte += espace + "\t<table class=\"titre\">\n";
            texte += espace + "\t\t<tr>\n";
            texte += espace + "\t\t\t<td>\n";
            texte += espace + "\t\t\t\t<img  style=\"vertical-align:middle;heigh:64px\" src=\"..\\commun\\livre.svg\" />\n";
            if (source_liste_ID_numero.Count == 1)
                texte += espace + "\t\t\t\t Source\n";
            else
                texte += espace + "\t\t\t\t Sources\n";
            texte += espace + "\t\t\t</td>\n";
            texte += espace + "\t\t</tr>\n";
            texte += espace + "\t</table>\n";
            int compteurMedia = 0;
            //bool Ok_Titre;
            foreach (ID_numero item_source in source_liste_ID_numero)
            {
                compteurMedia++;
                GEDCOM.GEDCOMClass.SOURCE_RECORD info_source = GEDCOMClass.Avoir_info_SOURCE(item_source.ID);
                {
                    texte += espace + "\t\t\t\t<span style=\"display: table-cell;\"><a id=\"source-" + item_source.numero.ToString() + "\"></a></span>\n";
                    texte += Separation("mince", tab + 1);
                    texte += espace + "\t<table class=\"tableau\">\n";
                    texte += espace + "\t\t<tr>\n";
                    texte += espace + "\t\t\t<td colspan=2 style=\"background-color:#BBB;border:2px solid #000;margin-left:5px;vertical-align:top;\">\n";// ne pas enlever Border.
                    texte += espace + "\t\t\t\t<span class=\"source\">" + item_source.numero.ToString() + "</span> \n";
                    texte += espace + "\t\t\t\t<span style=\"display:table-cell;text-align:top; padding-left:5px\">\n";
                    texte += espace + "\t\t\t\t\t" + info_source.N1_TITL + "\n";
                    if (GH.Properties.Settings.Default.deboguer)
                        texte += espace + "\t\t\t\t\t" + " {" + info_source.N0_ID + "}\n";
                    texte += espace + "\t\t\t\t</span>\n";
                    texte += espace + "\t\t\t</td>\n";
                    texte += espace + "\t\t</tr>\n";
                    {
                        // média source

                        if (info_source.N1_OBJE_liste_ID.Count > 0 && GH.Properties.Settings.Default.voir_media)
                        {
                            int totalOBJE = info_source.N1_OBJE_liste_ID.Count;
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td colspan=2>\n";
                            texte += "\t\t\t\t<table class=\"tableauMedia retrait_point25in\" style=\"border:0px;width:480px\">\n";
                            texte += "\t\t\t\t\t<tr>\n";
                            int cRanger = 0;
                            foreach (string media in info_source.N1_OBJE_liste_ID)
                            {
                                GEDCOMClass.MULTIMEDIA_RECORD OBJE = GEDCOMClass.AvoirMedia(media);
                                texte += "\t\t\t\t\t\t<td class=\"tableauMedia\" style=\"width:50%\">\n";
                                string textMedia = "";
                                (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                                    Objet(
                                        OBJE,
                                        sousDossier,
                                        dossierSortie,
                                        note_liste_ID_numero,
                                        citation_liste_ID_numero,
                                        source_liste_ID_numero,
                                        repo_liste_ID_numero,
                                        tab + 6);
                                texte += temp;
                                //textMedia = Objet(false, OBJE, sousDossier, dossierSortie, tab + 6);
                                if (textMedia != "") texte += textMedia;
                                texte += "\t\t\t\t\t\t</td>\n";
                                cRanger++;
                                if (cRanger % 2 == 0 && cRanger != totalOBJE)
                                {
                                    texte += "\t\t\t\t\t</tr>\n";
                                    texte += "\t\t\t\t\t<tr>\n";
                                }
                            }
                            if (cRanger % 2 == 1)
                            {
                                texte += "\t\t\t\t\t\t<td class=\"tableauMedia\" style=\"border:0px;width:50%\">\n";
                                texte += "\t\t\t\t\t\t</td>\n";
                            }
                            texte += "\t\t\t\t\t</tr>\n";
                            texte += "\t\t\t\t</table>\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // pour événemnt
                        if (info_source.N2_DATA_EVEN != null)
                        {
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td style=\"width:150px\">\n";
                            texte += espace + "\t\t\t\tÉvénement:\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t\t<td>\n";
                            texte += espace + "\t\t\t\t" + info_source.N2_DATA_EVEN + ".\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // Date période
                        if (info_source.N3_DATA_DATE != null)
                        {
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td style=\"width:150px\">\n";
                            texte += espace + "\t\t\t\tDate période: \n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t\t<td>\n";
                            texte += espace + "\t\t\t\t" + info_source.N3_DATA_DATE + "\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // Lieu de juridiction
                        if (info_source.N3_DATA_PLAC != null)
                        {
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td style=\"width:150px\">\n";
                            texte += espace + "\t\t\t\tLieu de juridiction: \n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t\t<td>\n";
                            texte += espace + "\t\t\t\t" + info_source.N3_DATA_PLAC + ".\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // Agence
                        if (info_source.N2_DATA_AGNC != null)
                        {
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td style=\"width:150px\">\n";
                            texte += espace + "\t\t\t\tAgence:\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t\t<td>\n";
                            texte += espace + "\t\t\t\t" + info_source.N2_DATA_AGNC + "\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // AUTH
                        if (info_source.N1_AUTH != null)
                        {
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td style=\"width:150px\">\n";
                            texte += espace + "\t\t\t\tOriginateur:\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t\t<td>\n";
                            texte += espace + "\t\t\t\t" + info_source.N1_AUTH + "\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // ABBR
                        if (info_source.N1_ABBR != null)
                        {
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td style=\"width:150px\">\n";
                            texte += espace + "\t\t\t\tSaissi des données:\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t\t<td>\n";
                            texte += espace + "\t\t\t\t" + info_source.N1_ABBR + "\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // PUBL
                        if (info_source.N1_PUBL != null)
                        {
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td style=\"width:150px\">\n";
                            texte += espace + "\t\t\t\tFait sur la publication:\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t\t<td>\n";
                            texte += espace + "\t\t\t\t" + info_source.N1_PUBL + "\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // TEXT
                        if (info_source.N1_PUBL != null)
                        {
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td style=\"width:150px\">\n";
                            texte += espace + "\t\t\t\tTexte de la source:\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t\t<td>\n";
                            texte += espace + "\t\t\t\t" + info_source.N1_TEXT + "\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // REFN
                        if (info_source.N1_REFN_liste != null)
                        {
                            foreach (GEDCOMClass.USER_REFERENCE_NUMBER info in info_source.N1_REFN_liste)
                            {
                                texte += espace + "\t\t<tr>\n";
                                texte += espace + "\t\t\t<td colspan=2 style=\"border:0px solid #0e0;text-align:left\">\n";
                                texte += "\t\t\t\t\t\tNuméro de fichier d'enregistrement permanent(REFN): " + info.N0_REFN + " Type " + info.N1_TYPE + "\n";
                                texte += espace + "\t\t\t</td>\n";
                                texte += espace + "\t\t</tr>\n";
                            }
                        }
                        // RIN
                        if (info_source.N1_RIN != null)
                        {
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td style=\"width:150px\">\n";
                            texte += espace + "\t\t\t\t\tRIN:\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t\t<td>\n";
                            texte += espace + "\t\t\t\t" + info_source.N1_RIN + "\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // REPO
                        if (info_source.N1_REPO_info != null)
                        {
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td style=\"width:150px\">\n";
                            texte += espace + "\t\t\t\t\tDépôt:\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t\t<td>\n";
                            (temp, repo_liste_ID_numero) = Avoir_repo_lien(info_source.N1_REPO_info.N0_ID, repo_liste_ID_numero, tab + 4);
                            texte += espace + "\t\t\t\t" + temp + "\n";
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                            // CALN
                            if (info_source.N1_REPO_info.N1_CALN != null)
                            {
                                texte += espace + "\t\t<tr>\n";
                                texte += espace + "\t\t\t<td style=\"width:150px\">\n";
                                texte += espace + "\t\t\t\t\t&nbsp;\n";
                                texte += espace + "\t\t\t</td>\n";
                                texte += espace + "\t\t\t<td>\n";
                                texte += espace + "Référence du fond (CALN): " + info_source.N1_REPO_info.N1_CALN;
                                texte += espace + "\t\t\t</td>\n";
                                texte += espace + "\t\t</tr>\n";
                            }
                            if (info_source.N1_REPO_info.N2_CALN_MEDI != null)
                            {
                                texte += espace + "\t\t<tr>\n";
                                texte += espace + "\t\t\t<td style=\"width:150px\">\n";
                                texte += espace + "\t\t\t\t\t&nbsp;\n";
                                texte += espace + "\t\t\t</td>\n";
                                texte += espace + "\t\t\t<td>\n";
                                texte += espace + "Support du média: " + info_source.N1_REPO_info.N2_CALN_MEDI;
                                texte += espace + "\t\t\t</td>\n";
                                texte += espace + "\t\t</tr>\n";
                            }
                            // note du dépôt
                            if (info_source.N1_REPO_info.N1_NOTE_liste_ID.Count > 0)
                            {
                                texte += espace + "\t\t<tr>\n";
                                texte += espace + "\t\t\t<td style=\"width:150px\">\n";
                                texte += espace + "\t\t\t\t\t&nbsp;\n";
                                texte += espace + "\t\t\t</td>\n";
                                texte += espace + "\t\t\t<td>\n";
                                (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                                    Avoir_note_lien(
                                        info_source.N1_REPO_info.N1_NOTE_liste_ID,
                                        note_liste_ID_numero,
                                        citation_liste_ID_numero,
                                        source_liste_ID_numero,
                                        repo_liste_ID_numero,
                                        4);
                                texte += espace + temp;
                                texte += espace + "\t\t\t</td>\n";
                                texte += espace + "\t\t</tr>\n";
                            }
                        }
                        // NOTE
                        if (info_source.N1_NOTE_liste_ID.Count > 0)
                        {
                            texte += espace + "\t\t<tr>\n";
                            texte += espace + "\t\t\t<td colspan=2 style=\"border:0px solid #0e0;text-align:left;\">\n";
                            (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                                Avoir_note_lien(
                                    info_source.N1_NOTE_liste_ID,
                                    note_liste_ID_numero,
                                    citation_liste_ID_numero,
                                    source_liste_ID_numero,
                                    repo_liste_ID_numero,
                                    4);
                            texte += temp;
                            texte += espace + "\t\t\t</td>\n";
                            texte += espace + "\t\t</tr>\n";
                        }
                        // date de changement
                        if (GH.Properties.Settings.Default.voir_date_changement)
                        {
                            if (info_source.N1_CHAN != null)
                            {
                                if (info_source.N1_CHAN.N1_CHAN_DATE != null)
                                {
                                    texte += espace + "\t<tr>\n";
                                    texte += espace + "\t\t<td colspan=2 style=\"vertical-align:bottom\">\n";
                                    (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                                        Date_Changement_Bloc(
                                            info_source.N1_CHAN,
                                            note_liste_ID_numero,
                                            citation_liste_ID_numero,
                                            source_liste_ID_numero,
                                            repo_liste_ID_numero,
                                            tab + 3,
                                            true);
                                    texte += temp;
                                    texte += espace + "\t\t</td>\n";
                                    texte += espace + "\t</tr>\n";
                                }
                            }
                        }
                    }
                    texte += espace + "\t</table>\n";
                }
            }
            texte += Groupe("fin", tab);
            return (texte, true, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
        }
        public void Message_HTML(string ligne1 = "", string ligne2 = "", string ligne3 = "")
        {
            Label Lb_HTML_1 = Application.OpenForms["GH"].Controls["Lb_HTML_1"] as Label;
            Label Lb_HTML_2 = Application.OpenForms["GH"].Controls["Lb_HTML_2"] as Label;
            Label Lb_HTML_3 = Application.OpenForms["GH"].Controls["Lb_HTML_3"] as Label;
            Label Lb_HTML_4 = Application.OpenForms["GH"].Controls["Lb_HTML_4"] as Label;
            if (ligne1 == "" && ligne2 == "" && ligne3 == "")
            {
                Lb_HTML_1.Visible = false;
                Lb_HTML_2.Visible = false;
                Lb_HTML_3.Visible = false;
                Lb_HTML_4.Visible = false;
            }
            else
            {
                Lb_HTML_1.Visible = true;
                Lb_HTML_2.Visible = true;
                Lb_HTML_3.Visible = true;
                Lb_HTML_4.Visible = true;
                Lb_HTML_1.Text = ligne1;
                Lb_HTML_2.Text = ligne2;
                Lb_HTML_3.Text = ligne3;
                Random rnd = new Random();
                string ligne4 = ""; 
                for (int f = 0; f < 7; f++)
                {
                    if (rnd.Next(0, 2) == 0) ligne4 += "▀"; else ligne4 += "▄";
                }
                Lb_HTML_4.Text = ligne4.Substring(0, 5);
            }
            Application.DoEvents();
        }
        private static (string, List<ID_numero>, List<ID_numero>, List<ID_numero>, List<ID_numero>) Objet
            (
            GEDCOMClass.MULTIMEDIA_RECORD infoMedia, 
            string sousDossier,
            string dossierSortie,
            List<ID_numero> note_liste_ID_numero,
            List<ID_numero> citation_liste_ID_numero,
            List<ID_numero> source_liste_ID_numero,
            List<ID_numero> repo_liste_ID_numero,
            int tab)
        {
            string texte = "";
            string temp;
            string espace = Tabulation(tab);
            if (infoMedia == null)
            {
                texte += espace + "<table style=\"height:fit-content;width:100%;border:0px solid #00e;\">\n";
                texte += espace + "\t<tr>\n";
                texte += espace + "\t\t<td style=\"border:0px solid #0e0;height:150px\">\n";
                texte += espace + "\t\t\t<div class=\"media_manquant\">\n";
                texte += espace + "\t\t\t\t<div style=\"font-size:50px\">?</div>\n";
                texte += espace + "\t\t\t\tN'a pas trouver le fichier\n";
                texte += espace + "\t\t\t</div>\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
                texte += espace + "</table>\n";
                return (texte, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
            }
            texte += espace + "<table style=\"height:fit-content;width:100%;border:0px solid #00e;\">\n";
            texte += espace + "\t<tr>\n";
            texte += espace + "\t\t<td style=\"border:0px solid #0e0;height:150px\">\n";
            string fichierMedia = CopierObjet(infoMedia.N1_FILE, sousDossier, dossierSortie);
            string extention = null;
            if (fichierMedia != null) extention = Path.GetExtension(fichierMedia.ToLower());
            if (extention != null) // trouver fichier
            {
                // image
                if (extention == ".jpg" ||
                    extention == ".jpeg" ||
                    extention == ".gif" ||
                    extention == ".bmp" ||
                    extention == ".svg" ||
                    extention == ".png"
                )
                {
                    if (fichierMedia != "" && sousDossier != "commun" && sousDossier != "index")
                    {
                        texte += espace + "\t\t\t\t<img class=\"mini\" src=\"" + "medias/" + fichierMedia + "\" alt=\"\" />\n";
                    }
                    else if ((sousDossier == "commun" || sousDossier == "index") && fichierMedia != "")
                    {
                        texte += espace + "\t\t\t\t<img class=\"mini\" src=\"" + "commun/" + fichierMedia + "\" alt=\"\" />\n";
                    }
                }
                // Image tif, pic et pcx doivent être télécharger.
                else if (
                        extention == ".tif" ||
                        extention == ".tiff" ||
                        extention == ".pic" ||
                        extention == ".pcx" ||
                        extention == ".pic" || // Macintosh
                        extention == ".pict" || // Macintosh
                        extention == ".pct" || // Macintosh
                        extention == ".mac" || // Macintosh
                        extention == ".aif" || // Macintosh
                        extention == ".psd" || // photoshop
                        extention == ".tga"
                        )
                {
                    //if (extention == ".pic" || extention == ".pict" || extention == ".pct" || extention == ".aif") extention = ".mac";
                    if (fichierMedia != "" && sousDossier != "commun" && sousDossier != "index")
                    {
                        texte += espace + "\t\t\t<a href=\"medias/" + fichierMedia + "\" target=\"_BLANK\">\n";
                        texte += espace + "\t\t\t\t<img style=\"height:150px\" src=\"../commun/" + infoMedia.N2_FILE_FORM.ToLower() + ".svg\" alt=\"\" />\n";
                        texte += espace + "\t\t\t</a>\n";
                    }
                    else if ((sousDossier == "commun" || sousDossier == "index") && fichierMedia != "")
                    {
                        texte += espace + "\t\t\t<a href=\"commun/" + fichierMedia + "\" >\n";
                        texte += espace + "\t\t\t\t<img style=\"height:150px\" src=\"../commun/" + infoMedia.N2_FILE_FORM.ToLower() + ".svg\" alt=\"\" />\n";
                        texte += espace + "\t\t\t</a>\n";
                    }
                }
                // video audio
                else if (
                    extention == ".mov" ||
                    extention == ".aac" ||
                    extention == ".avi" ||
                    extention == ".flac" ||
                    extention == ".mkv" ||
                    extention == ".wav" ||
                    extention == ".mpg" ||
                    extention == ".mp3"
                    )
                {
                    texte += espace + "\t\t\t<a href=\"medias/" + fichierMedia + "\" target=\"_BLANK\">\n";
                    texte += espace + "\t\t\t\t<img style=\"height:150px\" src=\"../commun/media.svg\" alt=\"\" />\n";
                    texte += espace + "\t\t\t</a>\n";
                }
                // pdf
                else if (extention == ".pdf")
                {
                    if (fichierMedia != null && sousDossier != "commun" && sousDossier != "index")
                    {
                        texte += espace + "\t\t\t<a class=\"media\" href=\"" + "medias/" + fichierMedia + "\" target=\"_BLANK\">\n";
                        texte += espace + "\t\t\t\t<img style=\"height:150px\" src=\"" + "../commun/pdf.svg\" alt=\"\" />\n";
                        texte += espace + "\t\t\t</a>\n";
                    }
                    else if ((sousDossier == "commun" || sousDossier == "index") && fichierMedia != "")
                    {
                        texte += espace + "\t\t\t<a href=\"commun/" + fichierMedia + "\" >\n";
                        texte += espace + "\t\t\t\t<img style=\"height:150px\" src=\"../commun/pdf.svg\" alt=\"\" />\n";
                        texte += espace + "\t\t\t</a>\n";
                    }
                }
                // link 
                else if (infoMedia.N2_FILE_FORM.ToLower() == "url")
                {
                    texte += espace + "\t\t\t<a class=\"media\" href=\"" + infoMedia.N1_FILE + "\" target=\"_BLANK\">\n";
                    texte += espace + "\t\t\t\t<img style=\"height:150px\" src=\"" + "../commun/www.svg\" alt=\"\" />\n";
                    texte += espace + "\t\t\t</a>\n";
                }
                // tout les autres avec extention
                else
                {
                    texte += espace + "\t\t\t<a class=\"media\" href=\"" + "medias/" + fichierMedia + "\" target=\"_BLANK\">\n";
                    texte += espace + "\t\t\t\t<img style=\"height:150px\" src=\"" + "../commun/doc.svg\" alt=\"\" />\n";
                    texte += espace + "\t\t\t</a>\n";
                }
            }
            else // pas trouver de fichier
            {
                texte += espace + "\t\t\t\t<div class=\"media_manquant\">\n";
                texte += espace + "\t\t\t\t\t<div style=\"font-size:50px\">?</div>\n";
                texte += espace + "\t\t\t\t\tN'a pas trouver le fichier\n";
                texte += espace + "\t\t\t\t</div>\n";
            }
            texte += espace + "\t\t</td>\n";
            texte += espace + "\t</tr>\n";
            // ID
            if (GH.Properties.Settings.Default.deboguer)
            {
                texte += espace + "\t<tr>\n";
                texte += espace + "\t\t<td style=\"border:0px solid #0e0;text-align:center\">\n";
                texte += espace + "\t\t\t\t{" + infoMedia.N0_ID + "}\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            if (infoMedia.N3_FILE_FORM_TYPE != null )
            {
                texte += espace + "\t<tr>\n";
                texte += espace + "\t\t<td style=\"border:0px solid #0e0;text-align:center\">\n";
                texte += espace + "\t\t\t\t" + infoMedia.N3_FILE_FORM_TYPE + "\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            // titre
            if (infoMedia.N2_FILE_TITL != null)
            {
                texte += espace + "\t<tr>\n";
                texte += espace + "\t\t<td style=\"border:0px solid #0e0;text-align:left\">\n";
                texte += espace + "\t\t\t\t" + infoMedia.N2_FILE_TITL + "\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            // REFM
            if (infoMedia.N1_REFN_liste != null)
            {
                foreach (GEDCOMClass.USER_REFERENCE_NUMBER info_REFN in infoMedia.N1_REFN_liste)
                {
                    texte += espace + "\t<tr>\n";
                    texte += espace + "\t\t<td colspan=2 style=\"border:0px solid #0e0;text-align:left\">\n";
                    texte += espace + "\t\t\tREFN: " + info_REFN.N0_REFN + " Type: " + info_REFN.N1_TYPE + "\n";
                    texte += espace + "\t\t</td>\n";
                    texte += espace + "\t</tr>\n";
                }
            }
            // RIN
            if (infoMedia.N1_RIN != null)
            {
                texte += espace + "\t<tr>\n";
                texte += espace + "\t\t<td style=\"border:0px solid #0e0;text-align:left\">\n";
                texte += espace + "\t\t\t\tRIN: " + infoMedia.N1_RIN + "\n";
                texte += espace + "\t\t</td>\n";
                texte += espace + "\t</tr>\n";
            }
            // note
            texte += espace + "\t<tr>\n";
            texte += espace + "\t\t<td style=\"border:0px solid #0e0;text-align:left\">\n";
            (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                Avoir_note_lien(
                    infoMedia.N1_NOTE_liste_ID,
                    note_liste_ID_numero,
                    citation_liste_ID_numero,
                    source_liste_ID_numero,
                    repo_liste_ID_numero,
                    tab + 3);
            texte += temp;
            texte += espace + "\t\t</td>\n";
            texte += espace + "\t</tr>\n";

            // référence
            if (infoMedia.N1_SOUR_citation_liste_ID != null && GH.Properties.Settings.Default.voir_reference)
            {
                (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) = 
                    Avoir_citation_source_lien(
                        infoMedia.N1_SOUR_citation_liste_ID,
                        note_liste_ID_numero,
                        citation_liste_ID_numero,
                        source_liste_ID_numero,
                        repo_liste_ID_numero,
                        6);
                texte += temp;
            }
            // si a une date de changement
            GEDCOMClass.CHANGE_DATE N1_CHAN = infoMedia.N1_CHAN;
            if (GH.Properties.Settings.Default.voir_date_changement)
            {
                if (N1_CHAN != null)
                {
                    if (N1_CHAN.N1_CHAN_DATE != null)
                    {
                        texte += espace + "\t<tr>\n";
                        texte += espace + "\t\t<td style=\"vertical-align:bottom\">\n";
                        (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                            Date_Changement_Bloc(
                                N1_CHAN,
                                note_liste_ID_numero,
                                citation_liste_ID_numero,
                                source_liste_ID_numero,
                                repo_liste_ID_numero,
                                tab + 3,
                                true);
                        texte += temp; 
                        texte += espace + "\t\t</td>\n";
                        texte += espace + "\t</tr>\n";
                    }
                }
            }
            texte += espace + "</table>\n";
            return (texte, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
        }
        private (string, bool, List<ID_numero>, List<ID_numero>, List<ID_numero>, List<ID_numero>) Groupe_chercheur(
            List<ID_numero> liste, 
            string DossierSortie,
            string sousDossier,
            List<ID_numero> note_liste_ID_numero,
            List<ID_numero> citation_liste_ID_numero,
            List<ID_numero> source_liste_ID_numero,
            List<ID_numero> repo_liste_ID_numero,
            int tab)
        {
            if(GH.Properties.Settings.Default.voir_chercheur == false || liste.Count == 0)
                return ("", false, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
            string espace = Tabulation(tab);
            string texte = "";
            string temp;
            texte += Separation("large", tab);
            texte += "<a id=\"groupe_chercheur\"></a>";
            texte += Groupe("debut", tab);
            texte += espace + "<table class=\"titre\">\n";
            texte += espace + "\t<tr>\n";
            texte += espace + "\t\t<td>\n";
            if (sousDossier == "index")
                texte += espace + "\t\t\t\t<img  style=\"vertical-align:middle;heigh:64px\" src=\"commun\\tete.svg\" />\n";
            else
                texte += espace + "\t\t\t\t<img  style=\"vertical-align:middle;heigh:64px\" src=\"..\\commun\\tete.svg\" />\n";
            if (liste.Count == 1)
                texte += espace + "\t\t\t\tChercheur\n";
            else
                texte += espace + "\t\t\t\tChercheurs\n";
            texte += espace + "\t\t</td>\n";
            texte += espace + "\t</tr>\n";
            texte += espace + "</table>\n";
            (temp, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero) =
                Avoir_chercheur(
                    liste,
                    DossierSortie,
                    sousDossier,
                    note_liste_ID_numero,
                    citation_liste_ID_numero, 
                    source_liste_ID_numero,
                    repo_liste_ID_numero,
                    tab);
            texte += temp;
            return (texte, true, note_liste_ID_numero, citation_liste_ID_numero, source_liste_ID_numero, repo_liste_ID_numero);
        }
        private static string Separation(string px = "large", int tab = 0)
        {
            string espace = Tabulation(tab);
            if (px == "large")
            {
                string texte = espace + "<div style =\"height:15px;\"><!--sépatration large -->\n";
                texte += espace + "</div>\n";
                return texte;
            }
            if (px == "moyen")
            {
                string texte = espace + "<div style =\"height:10px;\"><!--sépatration moyen -->\n";
                texte += espace + "</div>\n";
                return texte;
            }
            if (px == "mince")
            {
                string texte = espace + "<div style =\"height:05px;\"><!--sépatration mince -->\n";
                texte += espace + "</div>\n";
                return texte;
            }
            return "<!--sépatration ? -->\n";
        }
        private string SuprimerVirgule(List<string> listeLien)
        {
            int c = 1;
            string texte = "";
            foreach (string info in listeLien)
            {
                if (c == listeLien.Count)
                {
                    texte += info + "\n ";
                }
                else
                {
                    texte += info + ", \n";
                }
                c++;
            }
            return texte;
        }
        private static string Tabulation(int tab = 0)
        {
            return string.Concat(Enumerable.Repeat("    ", tab));

        }
        public class Liste_par_date : IComparer<GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE>
        {
            public int Compare(GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE x, GEDCOMClass.EVENT_ATTRIBUTE_STRUCTURE y)
            {
                return x.N2_DATE.CompareTo(y.N2_DATE);
            }
        }
    }

}
