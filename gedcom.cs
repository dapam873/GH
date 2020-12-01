
/* 
Cette class est basé sur la documentation de

----------------------------------------------------------------------------------------------------------------------------
THE GEDCOM STANDARD
Release 5.5.1

Prepared by the
Family History Department
The Church of
Jesus Christ
of Latter-day Saints
15 November 2019

Suggestions and Correspondence:
Email: gedcom@ChurchOfJesusChrist.org
Telephone: 801-240-0770
Mail:   Family History Department
        Solution Provider Coordinator
        15 South Temple Street
        Salt Lake City, UT 84150 USA

Copyright © 1987, 1989, 1992, 1993, 1995, 1999, 2019 by The Church of Jesus Christ of Latter-day Saints. This document may be
copied for purposes of review or programming of genealogical software, provided this notice is included. All other rights reserved.

Les commentaires non référencé de cette classe font référence a ce document, ficher 5.5.1_LDS_2019-11-15.pdf. 
Les numéros de page seront indiqué par 551-p.xxx
----------------------------------------------------------------------------------------------------------------------------


The GEDCOM 5.5.5 Specification with Annotations
    Editor Tamura Jones
    
    Technical Reviewers
    Bob Coret               creator of Genealogy Online and Open Archives
    Diedrich                Hesmer creator of Our Family Book and GEDCOM Service Programs
    Andrew Hoyle            creator of Chronoplex My Family Tree and the Chronoplex GEDCOM Validator
    Kari Kujansuu           software developer for The Genealogy Society of Finland's Isotammi.net
    Louis Kessler           creator of Behold, GEDCOM File Finder, and Double Match Triangulator
    Stanley Mitchell        creator of ezGED Viewer
    Nigel Munro Parker      creator of the GED-inline GEDCOM validator

    First Release 2 Oct 2019
    2019-10-05 edit: minor typo & link fixes.
    
    Copyright © 2013 - 2019 Tamura Jones.
    All rights reserved.
    
    The latest versions of the GEDCOM specification are available for download on www.gedcom.org.

    The FamilySearch GEDCOM 5.5 specification contains the following copyright notice:
    /--------------------------------------------------------------------------------------\
    | Copyright © 1987, 1989, 1992, 1993, 1995 by The Church of Jesus Christ of Latter-day |
    | Saints. This document may be copied for purposes of review or programming of         |
    | genealogical software, provided this notice is included. All other rights reserved.  |
    \--------------------------------------------------------------------------------------/

Les réferences aux pages sont pour 5.5.1_LDS_2019-11-15 si non mentionner.

*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using TextBox = System.Windows.Forms.TextBox;

namespace GEDCOM
{
    public static class GEDCOMClass
    {
        static readonly TextBox Tb_Status = Application.OpenForms["GH"].Controls["Tb_Status"] as TextBox;
        static readonly System.Windows.Forms.Label lb_animation = Application.OpenForms["GH"].Controls["lb_animation"] as System.Windows.Forms.Label;
        public static int ligne = 0;
        private static int conteur_citation = 0;
        private static bool LogInfoGEDCOM = false;
        private static bool LogErreur = false;
        private static bool TagInfoGEDCOM = false;
        public static bool debug = false;
        private static readonly Random hazard = new Random();
        public static List<string> dataGEDCOM = new List<string>();
        public class ADDRESS_STRUCTURE
        {
            // 5.5_LDS_1996-01-02.pdf   p.29
            // 5.5.1_LDS_2019-11-15.pdf p.31
            // 5.5.5_Annotations_TJ.pdf p.64
                                                                //ADDRESS_STRUCTURE:=
            public string N0_ADDR;                              //  n ADDR <ADDRESS_LINE>               {1:1}
                                                                //                                          V5.5    p.37
                                                                //                                          V5.5.1  p.41
                                                                //                                          V5.5.5 p.97
                                                                //      +1 CONT <ADDRESS_LINE>          {0:3}
                                                                //                                          V5.5    p.37
                                                                //                                          V5.5.1  p.41
            public string N1_ADR1;                              //      +1 ADR1 <ADDRESS_LINE1>         {0:1}
                                                                //                                          V5.5    p.37
                                                                //                                          V5.5.1  p.41
                                                                //                                          v5.5.5  p.75
            public string N1_ADR2;                              //      +1 ADR2 <ADDRESS_LINE2>         {0:1}
                                                                //                                          V5.5    p.37
                                                                //                                          V5.5.1  p.41
                                                                //                                          v5.5.5  p.75
            public string N1_ADR3;                              //      +1 ADR3 <ADDRESS_LINE3>         {0:1}
                                                                //                                          V5.5    p.37
                                                                //                                          V5.5.1  p.41
                                                                //                                          v5.5.5  p.76
            public string N1_CITY;                              //      +1 CITY <ADDRESS_CITY>          {0:1}
                                                                //                                          V5.5    p.37
                                                                //                                          V5.5.1  p.41
                                                                //                                          v5.5.5  p.76
            public string N1_STAE;                              //      +1 STAE <ADDRESS_STATE>         {0:1}
                                                                //                                          V5.5    p.37
                                                                //                                          V5.5.1  p.42
                                                                //                                          v5.5.5  p.76
            public string N1_POST;                              //      +1 POST <ADDRESS_POSTAL_CODE>   {0:1}
                                                                //                                          V5.5    p.37
                                                                //                                          V5.5.1  p.41
                                                                //                                          v5.5.5  p.76
            public string N1_CTRY;                              //      +1 CTRY <ADDRESS_COUNTRY>       {0:1}
                                                                //                                          V5.5    p.37
                                                                //                                          V5.5.1  p.41
                                                                //                                          v5.5.5  p.76
                                                                // déplacer à                   //  n PHON <PHONE_NUMBER>
                                                                // EVENT_ATTRIBUTE_STRUCTURE    //  n EMAIL <ADDRESS_EMAIL>
                                                                // GEDCOM_HEADER                //  n FAX <ADDRESS_FAX>
                                                                // SUBMITTER_RECORD             //  n WWW <ADDRESS_WEB_PAGE>
                                                                // REPOSITORY_RECORD
            public List<string> N1_NOTE_liste_ID;               // GRAMPS
        }
        public class ASSOCIATION_STRUCTURE
        {
            // 5.5_LDS_1996-01-02.pdf   p.29
            // 5.5.1_LDS_2019-11-15.pdf p.31
            // 5.5.5_Annotations_TJ.pdf p.65
                                                            //  ASSOCIATION_STRUCTURE:=
            public string N0_ASSO;                          //      n ASSO @<XREF:INDI>@                    {1:1}
                                                            //                                                  V5.5    p.29
                                                            //                                                  V5.5.1  p.25
                                                            //                                                  V5.5.5  p.108

            public string N1_TYPE;                          //          +1 TYPE <RECORD_TYPE>
                                                            //                                                  V5.5    p.51
            public string N1_RELA;                          //          +1 RELA <RELATION_IS_DESCRIPTOR>    {1:1}
                                                            //                                                  V5.5    p.52
                                                            //                                                  V5.5.1  p.60
                                                            //                                                  V5.5.5  p.104

            public List<string> N1_SOUR_citation_liste_ID;  //          +1 <<SOURCE_CITATION>>              {0:M}
                                                            //                                                  V5.5    p.34
                                                            //                                                  V5.5.1  p.39
                                                            //                                                  V5.5.5  p.73
            
            public List<string> N1_SOUR_source_liste_ID;    //          +1 généré par l'appication
            public List<string> N1_NOTE_liste_ID;           //          +1 <<NOTE_STRUCTURE>>               {0:M}
                                                            //                                                  V5.5    p.33
                                                            //                                                  V5.5.1  p.37
                                                            //                                                  v5.5.5  p.71
        }
        public class CHANGE_DATE
        {
            // 5.5_LDS_1996-01-02.pdf   p.29
            // 5.5.1_LDS_2019-11-15.pdf p.31
            // 5.5.5_Annotations_TJ.pdf p.66
                                                            //CHANGE_DATE:=
                                                            //  n CHAN                                      {1:1}
                                                            //                                                  V5.5
                                                            //                                                  V5.5.1
                                                            //                                                  V5.5.5
            public string N1_CHAN_DATE;                     //      +1 DATE <DATE_EXACT>                    {1:1}
                                                            //                                                  V5.5    p.39
                                                            //                                                  V5.5.1  p.44
                                                            //                                                  V5.5.5  p.83
            public string N2_CHAN_DATE_TIME;                //          +2 TIME <TIME_VALUE>                {0:1}
                                                            //                                                  V5.5    p.55
                                                            //                                                  V5.5.1  p.63
                                                            //                                                  V5.5.5  p.107
            public List<string> N1_CHAN_NOTE_ID_liste;      //          +1 <<NOTE_STRUCTURE>>               {0:M}
                                                            //                                                  V5.5    p.33
                                                            //                                                  V5.5.1  p.37
                                                            //                                                  V5.5.5  p.71
        }
        public class CHILD_TO_FAMILY_LINK
        {
            // 5.5_LDS_1996-01-02.pdf   p.29
            // 5.5.1_LDS_2019-11-15.pdf p.31
            // 5.5.5_Annotations_TJ.pdf p.67
                                                            //  CHILD_TO_FAMILY_LINK:=
            public string N0_FAMC;                          //      n FAMC @<XREF:FAM>@                     {1:1}
                                                            //                                                  V5.5
                                                            //                                                  V5.5.1  p.24
                                                            //                                                  V5.5.5  P.108
            public string N1_PEDI;                          //          +1 PEDI <PEDIGREE_LINKAGE_TYPE>     {0:1}
                                                            //                                                  V5.5    p.50
                                                            //                                                  V5.5.1  p.57
                                                            //                                                  V5.5.5  p.99
            public string N1_STAT;                          //          +1 STAT <CHILD_LINKAGE_STATUS>      {0:1}
                                                            //                                                  V5.5.1  p.44
            public List<string> N1_NOTE_liste_ID;           //          +1 <<NOTE_STRUCTURE>>               {0:M}
                                                            //                                                  V5.5    p.33
                                                            //                                              V5.5.1  p.37
                                                            //                                              v5.5.5  p.71
        }
        public class EVENT_ATTRIBUTE_STRUCTURE
        {
            // mixe


            //      INDIVIDUAL_EVENT_DETAIL
            //                      5.5.1_LDS_2019-11-15 p.34
            //                      5.5.5_Annotations_TJ p.69
            //      INDIVIDUAL_EVENT_STRUCTURE
            //                      5.5.1_LDS_2019-11-15 p.34
            //                      5.5.5_Annotations_TJ p.69
            //      FAMILY_EVENT_DETAIL
            //                      5.5.1_LDS_2019-11-15 p.32
            //                      5.5.5_Annotations_TJ p.67
            //      EVENT_DETAIL    
            //                      5.5_LDS_1996-01-02.pdf   p.29
            //                      5.5.1_LDS_2019-11-15.pdf p.32
            //                      5.5.5_Annotations_TJ.pdf p.67
            public string N1_EVEN;                          //  n                                           {1:1}
                                                            //                                                  V5.5
                                                            //                                                  V5.5.1
                                                            //                                                  V5.5.5
            public string N1_EVEN_texte;                    // récupaire le texte avec balise.

                                                            // EVENT_DETAIL:=
            public string N2_TYPE;                          //  n TYPE <EVENT_OR_FACT_CLASSIFICATION>       {0:1}
                                                            //                                                  V5.5    p.43
                                                            //                                                  V5.5.1  p.49
                                                            //                                                  V5.5.5  p.91

            public string N2_DATE;                          //  n DATE <DATE_VALUE>                         {0:1}
                                                            //                                                  V5.5    p.42 p.41
                                                            //                                                  V5.5.1  p.47 p.46
                                                            //                                                  V5.5.5  p.87
            public string N3_DATE_TIME;                     //  n TIME                                        Heridis
            public PLACE_STRUCTURE N2_PLAC;                 //  n <<PLACE_STRUCTURE>>                       {0:1}
                                                            //                                                  V5.5    p.34
                                                            //                                                  V5.5.1  p.38
                                                            //                                                  V5.5.5  p.72
            public ADDRESS_STRUCTURE N2_ADDR;               //  n <<ADDRESS_STRUCTURE>>                     {0:1}
                                                            //                                                  V5.5    p.29
                                                            //                                                  V5.5.1  p.31
                                                            //                                                  V5.5.5  p.64
            public List<string> N2_PHON_liste;              //  n PHON <PHONE_NUMBER>                       {0:3}
                                                            //                                                  V5.5    p.50
                                                            //                                                  V5.5.1  p.57
                                                            //                                                  V5.5.5  p.99
            public List<string> N2_EMAIL_liste;             //  n EMAIL <ADDRESS_EMAIL>                     {0:3}
                                                            //                                                  V5.5.1  p.41
                                                            //                                                  V5.5.5  p.75
            public List<string> N2_FAX_liste;               //  n FAX <ADDRESS_FAX>                         {0:3}
                                                            //                                                  V5.5.1  p.41
                                                            //                                                  V5.5.5  p.75
            public List<string> N2_WWW_liste;               //  n WWW <ADDRESS_WEB_PAGE>                    {0:3}
                                                            //                                                  V5.5.1  p.42
                                                            //                                                  V5.5.5  p.76
            public string N2_AGNC;                          //  n AGNC <RESPONSIBLE_AGENCY>                 {0:1}
                                                            //                                                  V5.5    p.34
                                                            //                                                  V5.5.1  p.60
                                                            //                                                  V5.5.5  p.104
            public string N2_RELI;                          //  n RELI <RELIGIOUS_AFFILIATION>              {0:1}
                                                            //                                                  V5.5.1  p.60
                                                            //                                                  V5.5.5  p.104
            public string N2_CAUS;                          //  n CAUS <CAUSE_OF_EVENT>                     {0:1} 
                                                            //                                                  V5.5    p.38
                                                            //                                                  V5.5.1  p.78
                                                            //                                                  V5.5.5  p.72
            public string N2_RESN;                          //  n RESN <RESTRICTION_NOTICE>                 {0:1}
                                                            //                                                  V5.5.1  p.60
            public List<string> N2_NOTE_liste_ID;           //  n <<NOTE_STRUCTURE>>                        {0:M}
                                                            //                                                  V5.5    p.33
                                                            //                                                  V5.5.1  p.37
                                                            //                                                  V5.5.5  p.71
            public List<string> N2_SOUR_citation_liste_ID;  //  n <<SOURCE_CITATION>>                       {0:M}
                                                            //                                                  V5.5    p.34
                                                            //                                                  V5.5.1  p.39
                                                            //                                                  V5.5.5  p.73
            public List<string> N2_SOUR_source_liste_ID;
            public List<string> N2_OBJE_liste_ID;           //  n <<MULTIMEDIA_LINK>>                       {0:M}
                                                            //                                                  V5.5    p.33 p.26
                                                            //                                                  V5.5.1  p.37 p.26
                                                            //                                                  V5.5.5  p.71
                                                            // FAMILY_EVENT_DETAIL
                                                            //  n HUSB {0:1}
            public string N3_HUSB_AGE;                      //      +1 AGE <AGE_AT_EVENT>                   {1:1}
                                                            //                                                  V5.5    p.37
                                                            //                                                  V5.5.1  p.42
                                                            //                                                  V5.5.5  p.77
            public string N3_WIFE_AGE;                      //      +1 AGE <AGE_AT_EVENT>                   {1:1}
                                                            //                                                  V5.5    p.37
                                                            //                                                  V5.5.1  p.42
                                                            //                                                  v5.5.5  p.77
                                                            // INDIVIDUAL_EVENT_DETAIL:=
            public string N2_AGE;                           //  n AGE <AGE_AT_EVENT>
                                                            //                                                  V5.5    p.37
                                                            //                                                  V5.5.1  p.42
                                                            //                                                  V5.5.5  p.77
                                                            // INDIVIDUAL_EVENT_STRUCTURE:=
            public string N2_FAMC;                          //      +1 FAMC @<XREF:FAM>@                    {0:1}
                                                            //                                                  V5.5    p.55
                                                            //                                                  V5.5.1  p.24
                                                            //                                                  V5.5.5  p.108

            public string N2_FAMC_ADOP;                     //         +2 ADOP<ADOPTED_BY_WHICH_PARENT>     {0:1}
                                                            //                                                  V5.5    p.37
                                                            //                                                  V5.5.1  p.42
                                                            //                                                  V5.5.5  p.76

            public string titre;
            public string description;

            public string N2__ANCES_ORDRE;                  //  n Ancestrologie
            public string N2__ANCES_XINSEE;                 //  n Ancestrologie
            public string N2__FNA;                          //  n Heridis Etat des recherches d'un événement
        }
        public class FAM_RECORD
        {
            // 5.5_LDS_1996-01-02.pdf   p29
            // 5.5.1_LDS_2019-11-15.pdf p.24
            // 5.5.5_Annotations_TJ.pdf p.58 FAM_GROUP_RECORD
            //                                                          FAM_RECORD:=
            public string N0_ID;                                        //    n @<XREF:FAM>@ FAM                {1:1}
                                                                        //                                          V5.5
                                                                        //                                          V5.5.1
                                                                        //                                          v5.5.5
            public string N1_RESN;                                      //    +1 RESN <RESTRICTION_NOTICE>      {0:1) 
                                                                        //                                          V5.5.1  p.60
            public List<EVENT_ATTRIBUTE_STRUCTURE> N1_EVENT_Liste;      //    +1 <<FAMILY_EVENT_STRUCTURE>>     {0:M}
                                                                        //                                          V5.5    p.30
                                                                        //                                          V5.5.1  p.32
                                                                        //                                          v5.5.5  p.67
            public string N1_HUSB;                                      //    +1 HUSB @<XREF:INDI>@             {0:1}
                                                                        //                                          V5.5    p.55
                                                                        //                                          V5.5.1  p.25
                                                                        //                                          v5.5.5  p.108
            public string N1_WIFE;                                      //    +1 WIFE @<XREF:INDI>@             {0:1} p.25
                                                                        //                                          V5.5    p.55
                                                                        //                                          V5.5.1  p.25
                                                                        //                                          v5.5.5  p.108
            public List<string> N1_CHIL_liste_ID;                       //    +1 CHIL @<XREF:INDI>@             {0:M}
                                                                        //                                          V5.5    p.55
                                                                        //                                          V5.5.1  p.25
                                                                        //                                          v5.5.5  p.108
            public string N1_NCHI;                                      //    +1 NCHI <COUNT_OF_CHILDREN>       {0:1}
                                                                        //                                          V5.5    p.39
                                                                        //                                          V5.5.1  p.44
                                                                        //                                          v5.5.5  p.79
            public List<string> N1_SUBM_liste_ID;                       //    +1 SUBM @<XREF:SUBM>@             {0:M}
                                                                        //                                          V5.5    p.55
                                                                        //                                          V5.5.1  p.28
                                                                        //                                          v5.5.5  p.79
            public LDS_SPOUSE_SEALING N1_SLGS;                          //    +1 <<LDS_SPOUSE_SEALING>>         {0:M}
                                                                        //                                          V5.5    p.33
                                                                        //                                          V5.5.1  p.36
            public List<USER_REFERENCE_NUMBER> N1_REFN_liste;           //    +1 REFN <USER_REFERENCE_NUMBER>   {0:M}
                                                                        //                                          V5.5    p.55
                                                                        //                                          V5.5.1  p.63
                                                                        //                                          v5.5.5  p.107
                                                                        //      +2 TYPE <USER_REFERENCE_TYPE>   {0:1}
                                                                        //                                          V5.5    p.55
                                                                        //                                          V5.5.1  p.63
                                                                        //                                          v5.5.5  p.107
            public string N1_RIN;                                       //    +1 RIN <AUTOMATED_RECORD_ID>      {0:1}
                                                                        //                                          V5.5    p.38
                                                                        //                                          V5.5.1  p.63
                                                                        //                                          v5.5.5  p.78
            public CHANGE_DATE N1_CHAN;                                 //    +1 <<CHANGE_DATE>>                {0:1}
                                                                        //                                          V5.5    p.29
                                                                        //                                          V5.5.1  p.31
                                                                        //                                          v5.5.5  p.66
            public List<string> N1_NOTE_liste_ID;                       //    +1 <<NOTE_STRUCTURE>>             {0:M}
                                                                        //                                          V5.5    p.33
                                                                        //                                          V5.5.1  p.37
                                                                        //                                          v5.5.5  p.71
            public List<string> N1_SOUR_citation_liste_ID;              //    +1 <<SOURCE_CITATION>>            {0:M}
                                                                        //                                          V5.5    p.34
                                                                        //                                          V5.5.1  p.39
                                                                        //                                          v5.5.5  p.73
            public List<string> N1_SOUR_source_liste_ID;
            public List<string> N1_OBJE_liste;                          //    +1 <<MULTIMEDIA_LINK>>            {0:M}
                                                                        //                                          V5.5    p.33 p.26
                                                                        //                                          V5.5.1  p.37 p.26
                                                                        //                                          v5.5.5  p.71

            public List<EVENT_ATTRIBUTE_STRUCTURE> N1_ATTRIBUTE_liste;  //    pour GRAMPS
            public string N1_TYPU;                                      //    +1 Type d'union                                      Ancestrologie
            public string N1__UST;                                      //    +1 Type d'union                                      Heridis 
        }
        public class INDIVIDUAL_RECORD
        {
            // 5.5_LDS_1996-01-02.pdf   p25
            // 5.5.1_LDS_2019-11-15.pdf p.25 
            // 5.5.5_Annotations_TJ.pdf p.61
                                                                        //  INDIVIDUAL_RECORD
            public string N0_ID;                                        //      n @XREF:INDI@ INDI                          {1:1}
                                                                        //                                                      V5.5
                                                                        //                                                      V5.5.1
                                                                        //                                                      V5.5.5
            public string N1_RESN;                                      //          +1 RESN <RESTRICTION_NOTICE>            {0:1}
                                                                        //                                                      V5.5    p.52
                                                                        //                                                      V5.5.1  p.60
            public List<PERSONAL_NAME_STRUCTURE> N1_NAME_liste;         //          +1 <<PERSONAL_NAME_STRUCTURE>>          {0:M}
                                                                        //                                                      V5.5    p.34
                                                                        //                                                      V5.5.1  p.38
                                                                        //                                                      V5.5.5  p.72
            public string N1_SEX;                                       //          +1 SEX <SEX_VALUE>                      {0:1}
                                                                        //                                                      V5.5    p.53
                                                                        //                                                      V5.5.1  p.61
                                                                        //                                                      V5.5.5  p.105
            public List<EVENT_ATTRIBUTE_STRUCTURE> N1_EVENT_Liste;      //          +1 <<INDIVIDUAL_EVENT_STRUCTURE>>       {0:M}
                                                                        //                                                      V5.5    p.31
                                                                        //                                                      V5.5.1  p.34
                                                                        //                                                      V5.5.5  p.69
            public List<EVENT_ATTRIBUTE_STRUCTURE> N1_Attribute_liste;  //          +1 <<INDIVIDUAL_ATTRIBUTE_STRUCTURE>>   {0:M}
                                                                        //                                                      V5.5    p.30
                                                                        //                                                      V5.5.1  p.33
                                                                        //                                                      V5.5.5  p.67
            public List<LDS_INDIVIDUAL_ORDINANCE> N1_LDS_liste;         //          +1 <<LDS_INDIVIDUAL_ORDINANCE>>         {0:M}
                                                                        //                                                      V5.5    p.32
                                                                        //                                                      V5.5.1  p.35
            public CHILD_TO_FAMILY_LINK N1_FAMC;                        //          +1 <<CHILD_TO_FAMILY_LINK>>             {0:M}
                                                                        //                                                      V5.5    p.32
                                                                        //                                                      V5.5.1  p.31
                                                                        //                                                      V5.5.5  p.67
            public List<SPOUSE_TO_FAMILY_LINK> N1_FAMS_liste_Conjoint;  //          +1 <<SPOUSE_TO_FAMILY_LINK>>            {0:M}
                                                                        //                                                      V5.5    p.35
                                                                        //                                                      V5.5.1  p.40
                                                                        //                                                      V5.5.5  p.75
            public List<string> N1_SUBM_liste_ID;                       //          +1 SUBM @<XREF:SUBM>@                   {0:M}
                                                                        //                                                      V5.5    p.55
                                                                        //                                                      V5.5.1  p.28
            public List<ASSOCIATION_STRUCTURE> N1_ASSO_liste;           //          +1 <<ASSOCIATION_STRUCTURE>>            {0:M}
                                                                        //                                                      V5.5    p.29
                                                                        //                                                      V5.5.1  p.31
                                                                        //                                                      V5.5.5  p.65
            public List<string> N1_ALIA_liste_ID;                       //          +1 ALIA @<XREF:INDI>@                   {0:M}
                                                                        //                                                      V5.5    p.55
                                                                        //                                                      V5.5.1  p.25
            public List<string> N1_ANCI_liste_ID;                       //          +1 ANCI @<XREF:SUBM>@                   {0:M}
                                                                        //                                                      V5.5    p.55
                                                                        //                                                      V5.5.1  p.28
            public List<string> N1_DESI_liste_ID;                       //          +1 DESI @<XREF:SUBM>@                   {0:M}
                                                                        //                                                      V5.5    p.55
                                                                        //                                                      V5.5.1  p.28
            public string N1_RFN;                                       //          +1 RFN <PERMANENT_RECORD_FILE_NUMBER>   {0:1}
                                                                        //                                                      V5.5    p.50
                                                                        //                                                      V5.5.1  p.57
            public string N1_AFN;                                       //          +1 AFN <ANCESTRAL_FILE_NUMBER>          {0:1}
                                                                        //                                                      V5.5    p.38
                                                                        //                                                      V5.5.1  p.42
            public List<USER_REFERENCE_NUMBER> N1_REFN_liste;           //          +1 REFN <USER_REFERENCE_NUMBER>         {0:M}
                                                                        //                                                      V5.5    p.55
                                                                        //                                                      V5.5.1  p.63 p.64
                                                                        //                                                      V5.5.5  p.107
                                                                        //              +2 TYPE <USER_REFERENCE_TYPE>       {0:1}
                                                                        //                      inclue dans la ligne précédente
                                                                        //                                                      V5.5    p.55
                                                                        //                                                      V5.5.1  p.64
                                                                        //                                                      V5.5.5  p.107

            public string N1_RIN;                                       //          +1 RIN <AUTOMATED_RECORD_ID>            {0:1}
                                                                        //                                                      V5.5    p.38
                                                                        //                                                      V5.5.1  p.43
                                                                        //                                                      V5.5.5  p.78
            public CHANGE_DATE N1_CHAN;                                 //          +1 <<CHANGE_DATE>>                      {0:1}
                                                                        //                                                      V5.5    p.29
                                                                        //                                                      V5.5.1  p.31
                                                                        //                                                      v5.5.5  p.66
            public List<string> N1_NOTE_liste_ID;                       //          +1 <<NOTE_STRUCTURE>>                   {0:M}
                                                                        //                                                      V5.5    p.33
                                                                        //                                                      V5.5.1  p.37
                                                                        //                                                      v5.5.5  p.71
            public List<string> N1_SOUR_citation_liste_ID;              //          +1 <<SOURCE_CITATION>>                  {0:M}
                                                                        //                                                      V5.5    p.34
                                                                        //                                                      V5.5.1  p.39
                                                                        //                                                      v5.5.5  p.73
            public List<string> N1_SOUR_source_liste_ID;
            public List<string> N1_OBJE_liste;                          //          +1 <<MULTIMEDIA_LINK>>                  {0:M}
                                                                        //                                                      V5.5    p.33 p.26
                                                                        //                                                      V5.5.1  p.37 p.26
                                                                        //                                                      v5.5.5  p.71
                                                                        // extra
            public string Adopter;
            public string nom_section_1;
            public string nom_section_2;
            public string nom_section_3;
            public string Titre;
            public string PhotoID;

            public string N1__ANCES_CLE_FIXE;                           // +1 _ANCES_CLE_FIXE dans Ancestrologie
            public string N1_FILA;                                      // +1 FILA Filiation dans Ancestrologie
            public List<string> N1_WWW_liste;                           // +1 WWW dans GRAMPS
            public string N1_SIGN;                                      // Heridis
            public string N1__FIL;                                      // Filiation de l'individu Heridis
            public string N1__CLS;                                      // Individu sans postérité. Heridis
        }
        public struct HEADER
        {
            // HEADER           5.5_LDS_1996-01-02.pdf p.23
            // HEADER           5.5.1_LDS_1999-10-02.pdf p.23
            // GEDCOM_HEADER    5.5.5_Annotations_TJ p.67

                                                            //      n HEAD                                              {1:1}
                                                            //                                                              V5.5    p.23
                                                            //                                                              V5.5.1  p.23
                                                            //                                                              V5.5.5  p.56

            public string N1_SOUR;                          //      +1 SOUR <APPROVED_SYSTEM_ID>                        {1:1} 
                                                            //                                                              V5.5    p.38
                                                            //                                                              V5.5.1  p.42
                                                            //                                                              V5.5.5  p.57
            public string N2_SOUR_VERS;                     //          +2 VERS <VERSION_NUMBER>                        {0:1}
                                                            //                                                              V5.5    p.55
                                                            //                                                              V5.5.1  p.64
                                                            //                                                              V5.5.5  p.106
            public string N2_SOUR_NAME;                     //          +2 NAME <NAME_OF_PRODUCT>                       {0:1}
                                                            //                                                              V5.5    p.48
                                                            //                                                              V5.5.1  p.54
                                                            //                                                              V5.5.5  p.96
            public string N2_SOUR_CORP;                     //          +2 CORP <NAME_OF_BUSINESS>                      {0:1}
                                                            //                                                              V5.5    p.48
                                                            //                                                              V5.5.1  p.54
                                                            //                                                              V5.5.5  p.96
            public ADDRESS_STRUCTURE N3_SOUR_CORP_ADDR;     //              +3 <<ADDRESS_STRUCTURE>>                    {0:1}
                                                            //                                                              V5.5    p.29
                                                            //                                                              V5.5.1  p.31
                                                            //                                                              V5.5.5  p.64
            public List<string> N3_SOUR_CORP_PHON_liste;    //              +3 PHON <PHONE_NUMBER>                      {0:3}
                                                            //                                                              V5.5    p.50
                                                            //                                                              V5.5.1  p.57
                                                            //                                                              V5.5.5  p.75
            public List<string> N3_SOUR_CORP_EMAIL_liste;   //              +3 EMAIL <ADDRESS_EMAIL>                    {0:3} p.41
                                                            //                                                              V5.5.1  p.41
                                                            //                                                              V5.5.5  p.75
            public List<string> N3_SOUR_CORP_FAX_liste;     //              +3 FAX <ADDRESS_FAX>                        {0:3}
                                                            //                                                              V5.5.1  p.41
                                                            //                                                              V5.5.5  p.75
            public List<string> N3_SOUR_CORP_WWW_liste;     //              +3 WWW <ADDRESS_WEB_PAGE>                   {0:3}
                                                            //                                                              V5.5.1  p.42
                                                            //                                                              V5.5.5  p.76
            public string N2_SOUR_DATA;                     //          +2 DATA <NAME_OF_SOURCE_DATA>                   {0:1}
                                                            //                                                              V5.5    p.48
                                                            //                                                              V5.5.1  p.54
                                                            //                                                              V5.5.5  p.
            public string N3_SOUR_DATA_DATE;                //              +3 DATE <PUBLICATION_DATE>                  {0:1}
                                                            //                                                              V5.5    p.51
                                                            //                                                              V5.5.1  p.59
                                                            //                                                              V5.5.5  p.103
            public string N3_SOUR_DATA_CORP;                //              +3 COPR <COPYRIGHT_SOURCE_DATA>             {0:1}
                                                            //                                                              V5.5    p.39
                                                            //                                                              V5.5.1  p.44
                                                            //                                                              V5.5.5  p.79
                                                            //                  +4 [CONT|CONC]<COPYRIGHT_SOURCE_DATA>   {0:M}
                                                            //                                                              V5.5.1  p.44
            public string N1_DEST;                          //      +1 DEST <RECEIVING_SYSTEM_NAME>                     {0:1}
                                                            //                                                              V5.5    p.51
                                                            //                                                              V5.5.1  p.59
                                                            //                                                              V5.5.5  p.103
            public string N1_DATE;                          //      +1 DATE <TRANSMISSION_DATE>                         {0:1}
                                                            //                                                              V5.5    p.55
                                                            //                                                              V5.5.1  p.63
                                                            //                                                              V5.5.5  p.93
            public string N2_DATE_TIME;                     //          +2 TIME <TIME_VALUE>                            {0:1}
                                                            //                                                              V5.5    p.55
                                                            //                                                              V5.5.1  p.63
                                                            //                                                              V5.5.5  p.107
            public List<string> N1_SUBM_liste_ID;           //      +1 SUBM @<XREF:SUBM>@                               {1:1}
                                                            //                                                              V5.5    p.55
                                                            //                                                              V5.5.1  p.28
                                                            //                                                              V5.5.5  p.108
            public string N1_SUBN;                          //      +1 SUBN @<XREF:SUBN>@                               {0:1}
                                                            //                                                              V5.5    p.55
                                                            //                                                              V5.5.1  p.28
            public string N1_FILE;                          //      +1 FILE <FILE_NAME>                                 {0:1}
                                                            //                                                              V5.5    p.23
                                                            //                                                              V5.5.1  p.50
                                                            //                                                              V5.5.5  p.93
            public string N1_COPR;                          //      +1 COPR <COPYRIGHT_GEDCOM_FILE>                     {0:1}
                                                            //                                                              V5.5    p.39
                                                            //                                                              V5.5.1  p.55
                                                            //                                                              V5.5.5  p.79
                                                            //      +1 GEDC                                             {1:1}      
            public string N2_GEDC_VERS;                     //          +2 VERS <VERSION_NUMBER>                        {1:1}
                                                            //                                                              V5.5    p.55
                                                            //                                                              V5.5.1  p.64
                                                            //                                                              V5.5.5  p.
            public string N2_GEDC_FORM;                     //          +2 FORM <GEDCOM_FORM>                           {1:1}
                                                            //                                                              V5.5    p.44
                                                            //                                                              V5.5.1  p.50
                                                            //                                                              V5.5.5  p.
            public string N3_GEDC_FORM_VERS;                //              +3 VERS <GEDCOM_VERSION_NUMBER>             {1:1}
                                                            //                                                              V5.5.5  p.49
            public string N1_CHAR;                          //      +1 CHAR <CHARACTER_SET>                             {1:1} p.44 V5.5.1
                                                            //                                                              V5.5    p.39
                                                            //                                                              V5.5.1  p.44
                                                            //                                                              V5.5.5  p.47
            public string N2_CHAR_VERS;                     //          +2 VERS <VERSION_NUMBER>                        {0:1} p.64 V5.5.1
                                                            //                                                              V5.5    p.55
                                                            //                                                              V5.5.1  p.64
            public string N1_LANG;                          //      +1 LANG <LANGUAGE_OF_TEXT>                          {0:1}
                                                            //                                                              V5.5    p.45
                                                            //                                                              V5.5.1  p.51
                                                            //                                                              V5.5.5  p.94
            public string N1_PLAC;                          //      +1 PLAC                                             {0:1}      V5.
                                                            //                                                              V5.5
                                                            //                                                              V5.5.1
            public string N2_PLAC_FORM;                     //          +2 FORM <PLACE_HIERARCHY>                       {1:1} p.58 V5.5.1
                                                            //                                                              V5.5    p.51
                                                            //                                                              V5.5.1  p.58
            public string N1_NOTE;                          //      +1 NOTE <GEDCOM_CONTENT_DESCRIPTION>                {0:1} p.50 V5.5.1 V5.5.5
                                                            //                                                              V5.5    p.44
                                                            //                                                              V5.5.1  p.50
                                                            //                                                              V5.5.5  p.93
                                                            //                  +2 [CONT|CONC]<COPYRIGHT_SOURCE_DATA>   {0:M}
                                                            //                                                              V5.5
                                                            //                                                              V5.5.1


            //public string N1_DATA;
            //public string N2_DATA_DATE;
            public string Nom_fichier_disque;               // nom du fichier réel sur le disque
            public string N1__GUID;                         // identificateur global unique Herisis
        }
        public class LDS_INDIVIDUAL_ORDINANCE
        {
            // 5.5_LDS_1996-01-02.pdf   p.32
            // 5.5.1_LDS_1999-10-02.pdf p.36
                                                                        // LDS_INDIVIDUAL_ORDINANCE
            public string N0_Type;                                      //  n [ BAPL | CONL | ENDL | SLGC]      {1:1}      V5.5.1
            public string N1_DATE;                                      //      +1 DATE <DATE_LDS_ORD>          {0:1}
                                                                        //                                          V5.5    p.41
                                                                        //                                          V5.5.1  p.46
            public string N1_TEMP;                                      //      +1 TEMP <TEMPLE_CODE>           {0:1}
                                                                        //                                          V5.5    p.54
                                                                        //                                          V5.5.1  p.63
            public string N1_PLAC;                                      //      +1 PLAC <PLACE_LIVING_ORDINANCE>{0:1}1
                                                                        //                                          V5.5    p.41
                                                                        //                                          V5.5.1  p.58
            public string N1_STAT;                                      //      +1 STAT <LDS_BAPTISM_DATE_STATUS>{0:1}
                                                                        //                                          V5.5    p.45
                                                                        //                                          V5.5.1  p.37 p.26

            public string N2_STAT_DATE;                                 //          +2 DATE <CHANGE_DATE>       {1:1}
                                                                        //                                          V5.5.1  p.41
            public List<string> N1_NOTE_liste_ID;                       //      +1 <<NOTE_STRUCTURE>>           {0:M}
                                                                        //                                          V5.5    p.33
                                                                        //                                          V5.5.1  p.51
            public List<string> N1_SOUR_citation_liste_ID;              //      +1 <<SOURCE_CITATION>>          {0:M}
                                                                        //                                          V5.5    p.34
                                                                        //                                          V5.5.1  p.39
            public List<string> N1_SOUR_source_liste_ID;
            public string N1_FAMC;                                      //      +1 FAMC @<XREF:FAM>@            {1:1}
                                                                        //                                          V5.5    p.55
                                                                        //                                          V5.5.1  p.24
        }
        public class LDS_SPOUSE_SEALING
        {
            // 5.5.1_LDS_1999-10-02.pdf p.36
            // 5.5_LDS_1996-01-02.pdf   p.33
                                                                        // LDS_SPOUSE_SEALING:=
                                                                        //      n SLGS {1:1}
            public string N1_DATE;                                      //         +1 DATE <DATE_LDS_ORD>                       {0:1}
                                                                        //                                                          V5.5    p.41
                                                                        //                                                          V5.5.1  p.46
            public string N1_TEMP;                                      //         +1 TEMP <TEMPLE_CODE>                        {0:1}
                                                                        //                                                          V5.5    p.54
                                                                        //                                                          V5.5.1  p.63
            public string N1_PLAC;                                      //         +1 PLAC <PLACE_LIVING_ORDINANCE>             {0:1} p.58 V5.5.1
                                                                        //                                                          V5.5    p.41
                                                                        //                                                          V5.5.1  p.58
            public string N1_STAT;                                      //         +1 STAT <LDS_SPOUSE_SEALING_DATE_STATUS>     {0:1}
                                                                        //                                                          V5.5    p.46
                                                                        //                                                          V5.5.1  p.52
            public string N2_STAT_DATE;                                 //             +2 DATE <CHANGE_DATE>                    {1:1}
                                                                        //                                                          V5.5.1  p.44
            public List<string> N1_NOTE_liste_ID;                       //         +1 <<NOTE_STRUCTURE>>                        {0:M}
                                                                        //                                                          V5.5    p.33
                                                                        //                                                          V5.5.1  p.37
            public List<string> N1_SOUR_citation_liste_ID;              //         +1 <<SOURCE_CITATION>>                       {0:M}
                                                                        //                                                          V5.5    p.34
                                                                        //                                                          V5.5.1  p.39
            public List<string> N1_SOUR_source_liste_ID;
        }
        public class MULTIMEDIA_RECORD 
        {
            // 5.5_LDS_1996-01-02.pdf   p33
            // 5.5.1_LDS_1999-10-02.pdf p.26
            // 5.5.5_Annotations_TJ.pdf p.62
                                                                        //  MULTIMEDIA_LINK:= V5.5
                                                                        //  MULTIMEDIA_RECORD:= V5.5.1 V 5.5.5
            public string N0_ID;                                        //      n @XREF:OBJE@ OBJE                          {1:1}
                                                                        //                                                      V5.5    p.55
                                                                        //                                                      V5.5.1
                                                                        //                                                      V5.5.5
            public string N1_FORM;                                      //          +1 +1 FORM <MULTIMEDIA_FORMAT>          {1:1}
                                                                        //                                                      V5.5    p.48
            public string N1_TITL;                                      //          +1 FORM <MULTIMEDIA_FORMAT>             {0:1}
                                                                        //                                                      v5.5 p.43
            public string N1_FILE;                                      //          +1 FILE <MULTIMEDIA_FILE_REFN>          {1:M} p.54
                                                                        //                                                      V5.5    p.47
                                                                        //                                                      V5.5.1  p.54
                                                                        //                                                      V5.5.5  p.95
            public string N2_FILE_FORM;                                 //              +2 FORM <MULTIMEDIA_FORMAT>         {1:1}
                                                                        //                                                      V5.5.1  p.54
                                                                        //                                                      V5.5.5  p.95
            public string N3_FILE_FORM_TYPE;                            //                  +3 TYPE <SOURCE_MEDIA_TYPE>     {0:1}
                                                                        //                                                      V5.5.1  p.62
                                                                        //                                                      V5.5.5  p.106
            public string N2_FILE_TITL;                                 //              +2 TITL <DESCRIPTIVE_TITLE>         {0:1}
                                                                        //                                                      V5.5.1  p.48
                                                                        //                                                      V5.5.5  p.89
            public string N1_BLOB;                                      //          +1 BINARY_OBJECT                        {1:1}
                                                                        //                                                      V5.5
                                                                        //              +2 CONT <ENCODED_MULTIMEDIA_LINE>   {1:M} 
                                                                        //                                                      V5.5 p.43
            public string N1_OBJE;                                      //          +1 @<XREF:OBJE>@ chain to continued object  V5.5 

            public List<USER_REFERENCE_NUMBER> N1_REFN_liste;           //          +1 REFN <USER_REFERENCE_NUMBER>         {0:M}
                                                                        //                                                      V5.5.1  p.63
                                                                        //                                                      V5.5.5  p.107
                                                                        //              +2 TYPE <USER_REFERENCE_TYPE>       {0:1}
                                                                        //                                                      V5.5    p.55
                                                                        //                                                      V5.5.1  p.64
                                                                        //                                                      v5.5.5  p.106
            public string N1_RIN;                                       //          +1 RIN <AUTOMATED_RECORD_ID>            {0:1}
                                                                        //                                                      V5.5.1  p.43
                                                                        //                                                      V5.5.5  p.78
            public List<string> N1_NOTE_liste_ID;                       //          +1 <<NOTE_STRUCTURE>>                   {0:M}
                                                                        //                                                      V5.5    p.33
                                                                        //                                                      V5.5.1  p.37
                                                                        //                                                      V5.5.5  p.71
            public List<string> N1_SOUR_citation_liste_ID;              //          +1 <<SOURCE_CITATION>>                  {0:M}
                                                                        //                                                      V5.5.1  p.39
                                                                        //                                                      V5.5.5  p.73
            public List<string> N1_SOUR_source_liste_ID;
            public CHANGE_DATE N1_CHAN;                                 //          +1 <<CHANGE_DATE>>                      {0:1}
                                                                        //                                                      V5.5.1  p.31
                                                                        //                                                      V5.5.5  p.66
           
            public string N1__DATE;                                     //          +1                                          Herisis
        }
        public class NOTE_RECORD
        {
            // 5.5_LDS_1996-01-02.pdf   p26
            // 5.5.1_LDS_1999-10-02.pdf p.27
            // 5.5.5_Annotations_TJ     p.71
                                                                        // NOTE_RECORD:=
            public string N0_ID;                                        //      n @<XREF:NOTE>@ NOTE <SUBMITTER_TEXT>       {1:1} 
                                                                        //                                                      V5.5   p.54
                                                                        //                                                      V5.5.1 p.63
                                                                        //                                                      V5.5.5 p.108
                                                                        //          +1 [CONC|CONT] <SUBMITTER_TEXT>         {0:M}
                                                                        //                                                      V5.5
                                                                        //                                                      V5.5.1 p.63
                                                                        // le texte est placé dans N0_NOTE_Texte

            public List<USER_REFERENCE_NUMBER> N1_REFN_liste;           //          +1 REFN <USER_REFERENCE_NUMBER>         {0:M}
                                                                        //                                                      V5.5   p.55
                                                                        //                                                      V5.5.1 p.63 p.64
                                                                        //                                                      V5.5.5 p.107
                                                                        // inclue avec le précédent
                                                                        //              +2 TYPE <USER_REFERENCE_TYPE>       {0:1} 
                                                                        //                                                      V5.5   p.54
                                                                        //                                                      V5.5.1 p.64
                                                                        //                                                      V5.5.5 p.107
            public string N1_RIN;                                       //          +1 RIN <AUTOMATED_RECORD_ID>            {0:1}
                                                                        //                                                      V5.5    p.38 
                                                                        //                                                      V5.5.1  p.43 
                                                                        //                                                      V5.5.5  p.78
            public List<string> N1_SOUR_citation_liste_ID;              //          +1 <<SOURCE_CITATION>>                  {0:M} 
                                                                        //                                                      V5.5    p.34
                                                                        //                                                      V5.5.1  p.39
                                                                        //                                                      V5.5.5  p.73
                                                                        // V5.5.1 p.39
                                                                        // V5.5.5 p.73
            public List<string> N1_SOUR_source_liste_ID;
            public CHANGE_DATE N1_CHAN;                                 //          +1 <<CHANGE_DATE>>                      {0:1} 
                                                                        //                                                      V5.5    p.29
                                                                        //                                                      V5.5.1  p.31
                                                                        //                                                      V5.5.5  p.66

            public int numero;
            public string N0_NOTE_Texte;
        }
        public class PERSONAL_NAME_PIECES
        {
            // 5.5_LDS_1996-01-02.pdf   p.37
            // 5.5.1_LDS_2019-11-15.pdf p.37
            // 5.5.5_Annotations_TJ.pdf p.71
            public string Nn_NPFX;                                      //      n NPFX <NAME_PIECE_PREFIX>  {0:1}
                                                                        //                                      V5.5   p.48
                                                                        //                                      V5.5.1 p.55
                                                                        //                                      V5.5.5 p.97
            public string Nn_GIVN;                                      //      n GIVN <NAME_PIECE_GIVEN>   {0:1}
                                                                        //                                      V5.5   p.49
                                                                        //                                      V5.5.1 p.55
                                                                        //                                      V5.5.5 p.97
            public string Nn_NICK;                                      //      n NICK <NAME_PIECE_NICKNAME> {0:1}
                                                                        //                                      V5.5   p.49
                                                                        //                                      V5.5.1 p.55
                                                                        //                                      V5.5.5 p.97
            public string Nn_SPFX;                                      //      n SPFX <NAME_PIECE_SURNAME_PREFIX {0:1}
                                                                        //                                      V5.5   p.49
                                                                        //                                      V5.5.1 p.56
                                                                        //                                      V5.5.5 p.98
            public string Nn_SURN;                                      //      n SURN <NAME_PIECE_SURNAME> {0:1}
                                                                        //                                      V5.5   p.49
                                                                        //                                      V5.5.1 p.55
                                                                        //                                      V5.5.5 p.98
            public string Nn_NSFX;                                      //      n NSFX <NAME_PIECE_SUFFIX>  {0:1}
                                                                        //                                      V5.5   p.49
                                                                        //                                      V5.5.1 p.55
                                                                        //                                      V5.5.5 p.98
            public List<string> Nn_NOTE_liste_ID;                       //      n <<NOTE_STRUCTURE>>        {0:M}
                                                                        //                                      V5.5   p.33
                                                                        //                                      V5.5.1 p.37
                                                                        //                                      V5.5.5 p.71
            public List<string> Nn_SOUR_citation_liste_ID;              //      n <<SOURCE_CITATION>>       {0:M}
            public List<string> Nn_SOUR_source_liste_ID;
                                                                        //                                      V5.5   p.44
                                                                        //                                      V5.5.1 p.39
                                                                        //                                      V5.5.5 p.73
        }
        public class PERSONAL_NAME_STRUCTURE
        {
            // 5.5_LDS_1996-01-02.pdf   p.34
            // 5.5.1_LDS_2019-11-15.pdf p.38
            // 5.5.5_Annotations_TJ.pdf p.72
                                                                        //  PERSONAL_NAME_STRUCTURE:=
            public string N0_NAME;                                      //      n NAME <NAME_PERSONAL>      {1:1}
                                                                        //                                      V5.5   p.48
                                                                        //                                      V5.5.1 p.54
                                                                        //                                      V5.5.5 p.96

            public string N1_TYPE;                                      //          +1 TYPE <NAME_TYPE>     {0:1}
                                                                                                            // V5.5.1 p.56
                                                                                                            // V5.5.5 p.98
            public PERSONAL_NAME_PIECES N1_PERSONAL_NAME_PIECES;        //  PERSONAL_NAME_PIECES:=
                                                                        //                                  // V5.5.1 p.37
                                                                        //                                  // V5.5.5 p.71

            public string N1_FONE;                                      //          +1 FONE <NAME_PHONETIC_VARIATION>{0:M} 
                                                                        //                                  V5.5.1 p.55
                                                                        //                                  V5.5.5 p.97

            public string N2_FONE_TYPE;                                 //              +2 TYPE <PHONETIC_TYPE>{1:1} 
                                                                        //                                  V5.5.1 p.57
                                                                        //                                  V5.5.5 p.72
                                                                        //  PERSONAL_NAME_PIECES:=
                                                                        //                                  V5.5.1 p.37
                                                                        //                                  V5.5.5 p.71
            public PERSONAL_NAME_PIECES N1_FONE_name_pieces;            //  PERSONAL_NAME_PIECES:=
                                                                        //                                  V5.5.1 p.37
                                                                        //                                  V5.5.5 p.71
           
            public string N1_ROMN;                                      //          +1 ROMN <NAME_ROMANIZED_VARIATION>{0:M} 
                                                                        //                                  V5.5.1 p.56
                                                                        //                                  V5.5.5 p.98
            public string N2_ROMN_TYPE;                                 //              +2 TYPE <ROMANIZED_TYPE>{1:1} 
                                                                        //                                  V5.5.1 p.61
                                                                        //                                  V5.5.5 p.98
            public PERSONAL_NAME_PIECES N1_ROMN_name_pieces;            //  PERSONAL_NAME_PIECES:=
                                                                        //                                  V5.5.1 p.37
                                                                        //                                  V5.5.5 p.105
            // extra
            public List<string> N1_ALIA_liste;                          //              +1 ALIA  alia dans BROSKEEP

        }
        public class PLACE_STRUCTURE
        {
            // 5.5_LDS_1996-01-02.pdf   p.37
            // 5.5.1_LDS_2019-11-15.pdf p.38
            // 5.5.5_Annotations_TJ.pdf p.72
                                                                // PLACE_STRUCTURE:=
            public string N0_PLAC;                              //  n PLAC <PLACE_NAME>                     {1:1}
                                                                //                                              V5.5 p.51
                                                                //                                              V5.5.1 p.58
                                                                //                                              V5.5.5 p.100
            public string N1_FORM;                              //      +1 FORM <PLACE_HIERARCHY>           {0:1}
                                                                //                                              V5.5.1 p.51
                                                                //                                              V5.5.1 p.58
                                                                //                                              V5.5.5 p.100
            public string N1_FONE;                              //      +1 FONE <PLACE_PHONETIC_VARIATION>  {0:M}
                                                                //                                          V5.5.1 p.59
                                                                //                                          V5.5.51 p.101
            public string N2_FONE_TYPE;                         //          +2 TYPE <PHONETIC_TYPE>         {1:1} p.57
                                                                //                                          V5.5.1 p.57
                                                                //                                          V5.5.51 p.100
            public string N1_ROMN;                              //      +1 ROMN <PLACE_ROMANIZED_VARIATION> {0:M} p.59
                                                                //                                          V5.5.1 p.59
                                                                //                                          V5.5.51 p.101
            public string N2_ROMN_TYPE;                         //          +2 TYPE <ROMANIZED_TYPE>        {1:1} p.61
                                                                //                                          V5.5.1 p.61
                                                                //                                          V5.5.5 p.105
                                                                //      +1 MAP                              {0:1}
            public string N2_MAP_LATI;                          //          +2 LATI <PLACE_LATITUDE>        {1:1}
                                                                //                                          V5.5.1 p.58
                                                                //                                          V5.5.5 p.100
            public string N2_MAP_LONG;                          //          +2 LONG <PLACE_LONGITUDE>       {1:1}
                                                                //                                          V5.5.1 p.58
                                                                //                                          V5.5.5 p.100
            public List<string> N1_NOTE_liste_ID;               //      +1 <<NOTE_STRUCTURE>>               {0:M} p.37
                                                                //                                              V5.5   p.33
                                                                //                                              V5.5.1 p.37
                                                                //                                              V5.5.5 p.71

            public List<string> N1_SOUR_citation_liste_ID;      //                                              V5.5   p.34
                                                                // pour GEDitCOM
            public List<string> N1_SOUR_source_liste_ID;        // pour GEDitCOM
        }
        public class REPOSITORY_RECORD 
        {
            // 5.5_LDS_1996-01-02.pdf   p26
            // 5.5.1_LDS_2019-11-15.pdf p.27
            // 5.5.5_Annotations_TJ.pdf p.63
                                                                        // REPOSITORY_RECORD:=
            public string N0_ID;                                        //  n @<XREF:REPO>@ REPO {1:1}
                                                                        //                                          V5.5
                                                                        //                                          V5.5.1
                                                                        //                                          V5.5.5

            public string N1_NAME;                                      //      +1 NAME <NAME_OF_REPOSITORY>    {1:1}
                                                                        //                                          V5.5   p.48
                                                                        //                                          V5.5.1 p.54
                                                                        //                                          V5.5.5 p.96
            public ADDRESS_STRUCTURE N1_ADDR;                           //      +1 <<ADDRESS_STRUCTURE>>            {0:1}
                                                                        //                                          V5.5   p.29
                                                                        //                                          V5.5.1 p.31
                                                                        //                                          V5.5.5 p.64
            public List<string> N1_PHON_liste;                          //      n PHON <PHONE_NUMBER>               {0:3}
                                                                        //                                          V5.5   p.50
                                                                        //                                          V5.5.1 p.57
                                                                        //                                          V5.5.5 p.99
            public List<string> N1_EMAIL_liste;                         //      n EMAIL <ADDRESS_EMAIL>             {0:3}
                                                                        //                                          V5.5.1 p.41
                                                                        //                                          V5.5.5 p.75
            public List<string> N1_FAX_liste;                           //      n FAX <ADDRESS_FAX>                 {0:3}
                                                                        //                                          V5.5.1 p.41
                                                                        //                                          V5.5.5 p.75
            public List<string> N1_WWW_liste;                           //      n WWW <ADDRESS_WEB_PAGE>            {0:3}
                                                                        //                                          V5.5.1 p.42
                                                                        //                                          V5.5.5 p.76
            public List<string> N1_NOTE_liste_ID;                       //      +1 <<NOTE_STRUCTURE>>               {0:M}
                                                                        //                                          V5.5   p.33
                                                                        //                                          V5.5.1 p.37
                                                                        //                                          V5.5.5 p.71
            public List<USER_REFERENCE_NUMBER> N1_REFN_liste;           //      +1 REFN <USER_REFERENCE_NUMBER>     {0:M}
                                                                        //                                          V5.5   p.55
                                                                        //                                          V5.5.1 p.63
                                                                        //                                          V5.5.5 p.107
                                                                        //      +2 TYPE <USER_REFERENCE_TYPE>       {0:1}
                                                                        //                                          V5.5   p.55
                                                                        //                                          V5.5.1 p.64
                                                                        //                                          V5.5.5 p.107
            public string N1_RIN;                                       //      +1 RIN <AUTOMATED_RECORD_ID>        {0:1}
                                                                        //                                          V5.5   p.38
                                                                        //                                          V5.5.1 p.43
                                                                        //                                          V5.5.5 p.78
            public CHANGE_DATE N1_CHAN;                                 //      +1 <<CHANGE_DATE>>                  {0:1}
                                                                        //                                          V5.5   p.29
                                                                        //                                          V5.5.1 p.54
                                                                        //                                          V5.5.5 p.66
        }
        public class SOURCE_CITATION
        {
            // 5.5_LDS_1996-01-02.pdf   p34
            // 5.5.1_LDS_2019-11-15.pdf p.39
            // 5.5.5_Annotations_TJ.pdf p.73
            //  SOURCE_CITATION:=
            //      [      /* pointer to source record (preferred)*/
            public string N0_ID_source;                         //      n SOUR @<XREF:SOUR>@                {1:1}
                                                                //                                              V5.5   p.55
                                                                //                                              V5.5.1 p.27
                                                                //                                              V5.5.5 p.108
            public string N1_PAGE;                              //          +1 PAGE<WHERE_WITHIN_SOURCE>    {0:1}
                                                                //                                              V5.5   p.55
                                                                //                                              V5.5.1 p.64
                                                                //                                              V5.5.5 p.107
            public string N1_EVEN;                              //          +1 EVEN<EVENT_TYPE_CITED_FROM>  {0:1}
                                                                //                                              V5.5   p.43
                                                                //                                              V5.5.1 p.49
                                                                //                                              V5.5.5 p.92
            public string N2_EVEN_ROLE;                         //              +2 ROLE<ROLE_IN_EVENT>      {0:1}
                                                                //                                              V5.5   p.53
                                                                //                                              V5.5.1 p.61
                                                                //                                              V5.5.5 p.104
                                                                //          +1 DATA                         {0:1}
            public string N2_DATA_DATE;                         //              +2 DATE<ENTRY_RECORDING_DATE> {0:1}
                                                                //                                              V5.5   p.43
                                                                //                                              V5.5.1 p.48
                                                                //                                              V5.5.5 p.91
            public string N2_DATA_TEXT;                         //              +2 TEXT<TEXT_FROM_SOURCE>   {0:M}
                                                                //                                              V5.5   p.54
                                                                //                                              V5.5.1 p.63
                                                                //                                              V5.5.5 p.106
                                                                //                  +3 [CONC|CONT] <TEXT_FROM_SOURCE> {0:M}
                                                                //                                              V5.5
                                                                //                                              V5.5.1
            public List<string> N1_OBJE_ID_liste;               //          +1 <<MULTIMEDIA_LINK>>          {0:M}
                                                                //                                              V5.5    p.33 p.26
                                                                //                                              V5.5.1  p.37 p.26
                                                                //                                              v5.5.5  p.71
            public List<string> N1_NOTE_liste_ID;               //          +1 <<NOTE_STRUCTURE>>           {0:M}
                                                                //                                              V5.5   p.33
                                                                //                                              V5.5.1 p.37
                                                                //                                              V5.5.5 p.71
            public string N1_QUAY;                              //          +1 QUAY<CERTAINTY_ASSESSMENT>   {0:1}
                                                                //                                              V5.5   p.38
                                                                //                                              V5.5.1 p.43
                                                                //                                              V5.5.5 p.78
                                                                //      |           /* Systems not using source records */
            public string N0_Titre;                             //      n SOUR <SOURCE_DESCRIPTION>         {1:1}
                                                                //                                              V5.5   p.53
                                                                //                                              V5.5.1 p.61
                                                                //          +1 [CONC|CONT] <SOURCE_DESCRIPTION>{0:M}
            public string N1_TEXT;                              //          +1 TEXT<TEXT_FROM_SOURCE>       {0:M}
                                                                //                                              V5.5   p.54
                                                                //                                              V5.5.1 p.63
                                                                //              +2 [CONC|CONT] <TEXT_FROM_SOURCE> {0:M}
                                                                //                                              V5.5
                                                                //                                              V5.5.1
                                                                //      ]
            public string N0_ID_citation;                       // pour identifier tous les citations avec et sans ID de source

            //                                                              +1 _QUAL                        // Herisis
            public string N2__QUAL__SOUR;                       //              +2 _SOUR Qualité de la source  // Herisis
            public string N2__QUAL__INFO;                       //              +2 _INFO Qualité de l'information // Heridis
            public string N2__QUAL__EVID;                       //              +2 _EVID Qualité de la preuve  // Heridis
        }
        public class SOURCE_RECORD 
        {
            // 5.5_LDS_1996-01-02.pdf   p26
            // 5.5.1_LDS_2019-11-15.pdf p.27
            // 5.5.5_Annotations_TJ.pdf p.63
                                                                        //  SOURCE_RECORD:=
            public int N0_numero;                                       //      n numero assigné par application
            public string N0_ID;                                        //      n @<XREF:SOUR>@ SOUR                {1:1}
                                                                        //                                              V5.5
                                                                        //                                              V5.5.1
                                                                        //                                              V5.5.5
                                                                        //          +1 DATA                         {0:1}
                                                                        //                                              V5.5
                                                                        //                                              V5.5.1
                                                                        //                                              V5.5.5
            public string N2_DATA_EVEN;                                 //              +2 EVEN <EVENTS_RECORDED>   {0:M}
                                                                        //                                              V5.5   p.44
                                                                        //                                              V5.5.1 p.50
                                                                        //                                              V5.5.5 p.92
            public string N3_DATA_DATE;                                 //                  +3 DATE <DATE_PERIOD>   {0:1}
                                                                        //                                              V5.5   p.41
                                                                        //                                              V5.5.1 p.46
                                                                        //                                              V5.5.5 p.85
            public string N3_DATA_PLAC;                                 //                  +3 PLAC <SOURCE_JURISDICTION_PLACE>{0:1}
                                                                        //                                              V5.5   p.54
                                                                        //                                              V5.5.1 p62
                                                                        //                                              V5.5.5 p.106
            public string N2_DATA_AGNC;                                 //              +2 AGNC <RESPONSIBLE_AGENCY>{0:1}
                                                                        //                                              V5.5   p.54
                                                                        //                                              V5.5.1 p.60
                                                                        //                                              V5.5.5 p.104
            public List<string> N2_DATA_NOTE_liste_ID;                  //              +2 <<NOTE_STRUCTURE>>       {0:M}
                                                                        //                                              V5.5   p.33
                                                                        //                                              V5.5.1 p.37
                                                                        //                                              V5.5.5 p.71
            public string N1_AUTH;                                      //          +1 AUTH <SOURCE_ORIGINATOR>     {0:1}
                                                                        //                                              V5.5   p.54
                                                                        //                                              V5.5.1 p.62
                                                                        //                                              V5.5.5 p.106
                                                                        //              +2 [CONC|CONT] <SOURCE_ORIGINATOR>{0:M}
                                                                        //                                              V5.5   p.54
                                                                        //                                          V5.5.1 p.62
            public string N1_TITL;                                      //          +1 TITL <SOURCE_DESCRIPTIVE_TITLE>{0:1}
                                                                        //                                              V5.5   p.53
                                                                        //                                              V5.5.1 p.62
                                                                        //                                              V5.5.5 p.105
                                                                        //              +2 [CONC|CONT] <SOURCE_DESCRIPTIVE_TITLE>{0:M}
                                                                        //                                              V5.5   p.53
                                                                        //                                              V5.5.1
            public string N1_ABBR;                                      //          +1 ABBR <SOURCE_FILED_BY_ENTRY> {0:1}
                                                                        //                                              V5.5   p.53
                                                                        //                                              V5.5.1 p.62
                                                                        //                                              V5.5.5 p.106
            public string N1_PUBL;                                      //          +1 PUBL <SOURCE_PUBLICATION_FACTS>{0:1}
                                                                        //                                              V5.5   p.54
                                                                        //                                              V5.5.1 p.62
                                                                        //                                              V5.5.5 p.106
                                                                        //              +2 [CONC|CONT] <SOURCE_PUBLICATION_FACTS>{0:M}
                                                                        //                                              V5.5   p.54
                                                                        //                                              V5.5.1 p.62
            public string N1_TEXT;                                      //          +1 TEXT <TEXT_FROM_SOURCE>      {0:1}
                                                                        //                                              V5.5   p.54
                                                                        //                                              V5.5.1 p.63
                                                                        //                                              V5.5.5 p.106
                                                                        //              +2 [CONC|CONT] <TEXT_FROM_SOURCE>{0:M}
                                                                        //                                              V5.5   p.54
                                                                        //                                              V5.5.1 p.63
            public SOURCE_REPOSITORY_CITATION N1_REPO_info;             //          +1 <<SOURCE_REPOSITORY_CITATION>>{0:M}
                                                                        //                                              V5.5   p.35
                                                                        //                                              V5.5.1 p.40
                                                                        //                                              V5.5.5 p.74
            public List<USER_REFERENCE_NUMBER> N1_REFN_liste;           //          +1 REFN <USER_REFERENCE_NUMBER> {0:M}
                                                                        //                                              V5.5   p.55
                                                                        //                                              V5.5.1 p.63 p.64
                                                                        //                                              V5.5.5 p.107
                                                                        //              +2 TYPE <USER_REFERENCE_TYPE>{0:1}
                                                                        //                                              V5.5   p.55
                                                                        //                                              V5.5.1 p.64
                                                                        //                                              V5.5.5 p.107
            public string N1_RIN;                                       //          +1 RIN <AUTOMATED_RECORD_ID>    {0:1} p.43
                                                                        //                                              V5.5   p.38
                                                                        //                                              V5.5.1 p.43
                                                                        //                                              V5.5.5 p.78
            public CHANGE_DATE N1_CHAN;                                 //          +1 <<CHANGE_DATE>>              {0:1}
                                                                        //                                              V5.5   p.29
                                                                        //                                              V5.5.1 p.31
                                                                        //                                              V5.5.5 p.66
            public List<string> N1_NOTE_liste_ID;                       //          +1 <<NOTE_STRUCTURE>>           {0:M}
                                                                        //                                              V5.5   p.33
                                                                        //                                              V5.5.1 p.37
                                                                        //                                              V5.5.5 p.71
            public List<string> N1_OBJE_liste_ID;                       //          +1 <<MULTIMEDIA_LINK>>          {0:M}
                                                                        //                                              V5.5   p.33 p.26
                                                                        //                                              V5.5.1 p.37 p.26
                                                                        //                                              v5.5.5 p.71
            public string N1_EVEN;                                      //          +1 valeur I, F                      ancestrologie
            public string N1_QUAY;                                      //          +1                                  Heridis
            public string N1_TYPE;                                      //          +1                                  Heridis
            public string N1_DATE;                                      //          +1                                  Heridis
        }
        public class SOURCE_REPOSITORY_CITATION
        {
            // 5.5_LDS_1996-01-02.pdf   p34
            // 5.5.1_LDS_2019-11-15.pdf p.40
            // 5.5.5_Annotations_TJ.pdf p.74
                                                                // SOURCE_REPOSITORY_CITATION:=
            public string N0_ID;                                //  n REPO [ @XREF:REPO@ | <NULL>]          {1:1}
                                                                //                                              V5.5   p.55
                                                                //                                              V5.5.1 p.27
                                                                //                                              V5.5.5 p.108
            public List<string> N1_NOTE_liste_ID;               //      +1 <<NOTE_STRUCTURE>>               {0:M}
                                                                //                                              V5.5   p.33
                                                                //                                              V5.5.1 p.37
            public string N1_CALN;                              //      +1 CALN <SOURCE_CALL_NUMBER>        {0:M}
                                                                //                                              V5.5   p.53
                                                                //                                              V5.5.1 p.61
                                                                //                                              V5.5.5 p.105
            public string N2_CALN_MEDI;                         //          +2 MEDI <SOURCE_MEDIA_TYPE>     {0:1}
                                                                //                                              V5.5   p.54
                                                                //                                              V5.5.1 p.62
                                                                //                                              V5.5.5 p.106
                                                                //          [ audio | book | card | electronic | 
                                                                //              fiche | film | magazine |
                                                                //              manuscript | map | newspaper | 
                                                                //              photo | tombstone | video ]
        }
        public class SPOUSE_TO_FAMILY_LINK 
        {
            // 5.5_LDS_1996-01-02.pdf   p35
            // 5.5.1_LDS_2019-11-15.pdf p.40
            // 5.5.5_Annotations_TJ.pdf p.75
                                                                //  SPOUSE_TO_FAMILY_LINK:=
            public string N0_ID;                                //      n FAMS @<XREF:FAM>@                 {1:1}
                                                                //                                              V5.5   p.55
                                                                //                                              V5.5.1 p.24
                                                                //                                              V5.5.5 p.75
            public List<string> N1_NOTE_liste_ID;               //      +1 <<NOTE_STRUCTURE>>               {0:M} 
                                                                //                                              V5.5   p.33
                                                                //                                              V5.5.1 p.37
                                                                //                                              V5.5.5 p.71
        }
        public class SUBMISSION_RECORD
        {
            // 5.5_LDS_1996-01-02.pdf   p27
            // 5.5.1_LDS_2019-11-15.pdf p.28
            // 5.5.5_Annotations_TJ.pdf pas utiliser
                                                                //  SUBMISSION_RECORD:=
            public string N0_ID;                                //  n @XREF:SUBN@ SUBN                          {1:1}
                                                                //                                                  V5.5
                                                                //                                                  V5.5.1
            public List<string> N1_SUBM_liste_ID;               //      +1 SUBM @XREF:SUBM@                     {0:1} 
                                                                //                                                  V5.5 p.55
                                                                //                                                  V5.5.1 p.28
            public string N1_FAMF;                              //      +1 FAMF <NAME_OF_FAMILY_FILE>           {0:1}
                                                                //                                                  V5.5 p.48
                                                                //                                                  V5.5.1 p.54
            public string N1_TEMP;                              //      +1 TEMP <TEMPLE_CODE>                   {0:1}
                                                                //                                                  V5.5 p.54
                                                                //                                                  V5.5.1 p.63
            public string N1_ANCE;                              //      +1 ANCE <GENERATIONS_OF_ANCESTORS>      {0:1} 
                                                                //                                                  V5.5 p.44
                                                                //                                                  V5.5.1
            public string N1_DESC;                              //      +1 DESC <GENERATIONS_OF_DESCENDANTS>    {0:1}
                                                                //                                                  V5.5 p.44
                                                                //                                                  V5.5.1 p.50
            public string N1_ORDI;                              //      +1 ORDI <ORDINANCE_PROCESS_FLAG>        {0:1}
                                                                //                                                  V5.5 p.50
                                                                //                                                  V5.5.1 p.57
            public string N1_RIN;                               //      +1 RIN <AUTOMATED_RECORD_ID>            {0:1}
                                                                //                                                  V5.5 p.38
                                                                //                                                  V5.5.1 p.43
            public List<string> N1_NOTE_liste_ID;               //      +1 <<NOTE_STRUCTURE>>                   {0:M}
                                                                //                                                  V5.5.1  p.37
                                                                //                                                  v5.5.5  p.71
                                                                //                                              V5.5.1 p.37
            public CHANGE_DATE N1_CHAN;                         //      +1 <<CHANGE_DATE>>                      {0:1}
                                                                //                                                  V5.5.1  p.31
                                                                //                                                  V5.5.5  p.66
        }
        public class SUBMITTER_RECORD
        {
            // 5.5_LDS_1996-01-02.pdf   p27
            // 5.5.1_LDS_2019-11-15.pdf p.28
            // 5.5.5_Annotations_TJ.pdf p.63
                                                                //  SUBMITTER_RECORD:=
            public string N0_ID;                                //      n @<XREF:SUBM>@ SUBM                    {1:1}
                                                                //                                                  V5.5
                                                                //                                                  V5.5.1
                                                                //                                                  V5.5.5
            public string N1_NAME;                              //      +1 NAME <SUBMITTER_NAME>                {1:1}
                                                                //                                                  V5.5 p.54
                                                                //                                                  V5.5.1 p.63
                                                                //                                                  V5.5.5 p.106
            public ADDRESS_STRUCTURE N1_ADDR;                   //      +1 <<ADDRESS_STRUCTURE>>                {0:1}
                                                                //                                                  V5.5    p.29
                                                                //                                                  V5.5.1  p.31
                                                                //                                                  V5.5.5  p.64
            public List<string> N1_PHON_liste;                  //      n PHON <PHONE_NUMBER>                   {0:3}
                                                                //                                                  V5.5    p.50
                                                                //                                                  V5.5.1  p.57
                                                                //                                                  V5.5.5  p.99
            public List<string> N1_EMAIL_liste;                 //      n EMAIL <ADDRESS_EMAIL>                 {0:3}
                                                                //                                                  V5.5.1 p.41
                                                                //                                                  V5.5.5  p.75
            public List<string> N1_FAX_liste;                   //      n FAX <ADDRESS_FAX>                     {0:3}
                                                                //                                                  V5.5.1 p.41
                                                                //                                                  V5.5.5  p.75
            public List<string> N1_WWW_liste;                   //      n WWW <ADDRESS_WEB_PAGE>                {0:3}
                                                                //                                                  V5.5.1 p.42
                                                                //                                                  V5.5.5  p.76
            public List<string> N1_OBJE_ID_liste;               //      +1 <<MULTIMEDIA_LINK>>                  {0:M}
                                                                //                                                  V5.5    p.33 p.26
                                                                //                                                  V5.5.1  p.37 p.26
                                                                //                                                  v5.5.5  p.71
            public string N1_LANG;                              //      +1 LANG <LANGUAGE_PREFERENCE>           {0:3}
                                                                //                                                  V5.5    p.45
                                                                //                                                  V5.5.1 p.51
            public string N1_RFN;                               //      +1 RFN <SUBMITTER_REGISTERED_RFN>       {0:1}
                                                                //                                                  V5.5    p.54
                                                                //                                                  V5.5.1  p.63
            public string N1_RIN;                               //      +1 RIN <AUTOMATED_RECORD_ID>            {0:1}
                                                                //                                                  V5.5    p.38
                                                                //                                                  V5.5.1  p.43
            public List<string> N1_NOTE_liste_ID;               //      +1 <<NOTE_STRUCTURE>>                   {0:M}
                                                                //                                          V5.5    p.33
                                                                //                                          V5.5.1  p.37
                                                                //                                          v5.5.5  p.71
                                                                //                                              V5.5.1 p.37
            public CHANGE_DATE N1_CHAN;                         //      +1 <<CHANGE_DATE>>                      {0:1}
                                                                //                                          V5.5    p.29
                                                                //                                          V5.5.1  p.31
                                                                //                                          v5.5.5  p.66
        }
        public class USER_REFERENCE_NUMBER
        {
            // 5.5_LDS_1996-01-02.pdf   
            // 5.5.1_LDS_2019-11-15.pdf p.63
            // 5.5.5_Annotations_TJ.pdf p.107
            public string N0_REFN;
                                                                //                                          V5.5. p.55
                                                                //                                          V5.5.1 p.63
                                                                //                                          V5.5.5 p.107
            public string N1_TYPE;
                                                                //                                          V5.5.1 p.55
                                                                //                                          V5.5.1 p.64
                                                                //                                          V5.5.5 p.107
        }
        public class ItemSourceIDTexte
        {
            public int Reference { get; set; }
            public string ID { get; set; }
            public string Note { get; set; }
            public override bool Equals(object obj)
            {
                if (obj == null) return false;

                if (!(obj is ItemSourceIDTexte objAsPart)) return false;
                else return Equals(objAsPart);
            }
            public override int GetHashCode()
            {
                return Reference;
            }
            public bool Equals(ItemSourceIDTexte other)
            {
                if (other == null) return false;
                return (this.ID.Equals(other.ID));
            }
        }
        public static string erreur = "";
        // RECORD GEDCOM
        public static HEADER Info_HEADER = new HEADER(); // 551-p23
        private static List<FAM_RECORD> liste_FAM_RECORD = new List<FAM_RECORD>();  // 551-p24
        private static List<INDIVIDUAL_RECORD> liste_INDIVIDUAL_RECORD = new List<INDIVIDUAL_RECORD>(); // 551-p25
        private static List<MULTIMEDIA_RECORD> liste_MULTIMEDIA_RECORD = new List<MULTIMEDIA_RECORD>(); // 551-p26
        private static List<NOTE_RECORD> listeInfoNote = new List<NOTE_RECORD>(); // 551-p27
        private static List<SOURCE_RECORD> liste_SOURCE_RECORD = new List<SOURCE_RECORD>(); // 551-p27
        private static List<SUBMITTER_RECORD> liste_SUBMITTER_RECORD = new List<SUBMITTER_RECORD>(); // 551-p28
        public static readonly List<SOURCE_CITATION> liste_SOURCE_CITATION = new List<SOURCE_CITATION>();
        private static readonly SUBMISSION_RECORD info_SUBMISSION_RECORD = new SUBMISSION_RECORD();
        private static List<REPOSITORY_RECORD> liste_REPOSITORY_RECORD = new List<REPOSITORY_RECORD>(); // 551-p27
        public static void Animation(bool actif)
        {
            if (!actif)
            {
                lb_animation.Visible = false;
            }
            else
            {
                lb_animation.Visible = true;
                Random rnd = new Random();
                string ligne = "";
                for (int f = 0; f < 7; f++)
                {
                    if (rnd.Next(0, 2) == 0) ligne += "▀"; else ligne += "▄";
                }
                lb_animation.Text = ligne.Substring(0, 5);
            }
            Application.DoEvents();
        }
        public static NOTE_RECORD Avoir_Info_Note(string N0_ID)
        {
            foreach (NOTE_RECORD info in listeInfoNote)
            {
                if (N0_ID == info.N0_ID)
                    return info;
            }
            return null;
        }
        public static REPOSITORY_RECORD Avoir_Info_Repo(string ID)
        {
            foreach (REPOSITORY_RECORD info in liste_REPOSITORY_RECORD)
            {
                if (ID == info.N0_ID)
                {
                    return info;
                }
            }
            return null;
        }
        public static SOURCE_CITATION Avoir_info_citation(string ID)
        {
            foreach (SOURCE_CITATION info in liste_SOURCE_CITATION)
            {
                if (ID == info.N0_ID_citation)
                {
                    return info;
                }
            }
            return null;
        }
        public static SOURCE_RECORD Avoir_info_SOURCE(string source)
        {
            foreach (SOURCE_RECORD info in liste_SOURCE_RECORD)
            {
                if (source == info.N0_ID)
                    return info;
            }
            return null;
        }
        private static void Avoir_code_erreur([CallerLineNumber] int sourceLineNumber = 0)
        {
            erreur = "G" + sourceLineNumber;
        }
        private static string Avoir_type_media(string TYPE)
        {
            if (TYPE.ToLower() == "audio") TYPE = "Audio";
            if (TYPE.ToLower() == "book") TYPE = "Livre";
            if (TYPE.ToLower() == "card") TYPE = "Fiche";
            if (TYPE.ToLower() == "electronic") TYPE = "Numérique";
            if (TYPE.ToLower() == "fiche") TYPE = "Fiche";
            if (TYPE.ToLower() == "film") TYPE = "Film";
            if (TYPE.ToLower() == "magazine") TYPE = "Magazine";
            if (TYPE.ToLower() == "manuscript") TYPE = "Manuscrit";
            if (TYPE.ToLower() == "map") TYPE = "Carte géographique";
            if (TYPE.ToLower() == "photo") TYPE = "Photographie";
            if (TYPE.ToLower() == "newspaper") TYPE = "Journal";
            if (TYPE.ToLower() == "tombstone") TYPE = "Pierre tombale";
            if (TYPE.ToLower() == "video") TYPE = "Vidéo";
            if (TYPE.ToLower() == "other") TYPE = "Autre";
            return TYPE;
        }
        private static (LDS_INDIVIDUAL_ORDINANCE, int) Extraire_LDS_INDIVIDUAL_ORDINANCE(int ligne)
        {
            LDS_INDIVIDUAL_ORDINANCE info = new LDS_INDIVIDUAL_ORDINANCE
            {
                N0_Type = null,
                N1_DATE = null,
                N1_TEMP = null,
                N1_PLAC = null,
                N1_FAMC = null,
                N1_STAT = null,
                N2_STAT_DATE = null,
                N1_NOTE_liste_ID = new List<string>(),
                N1_SOUR_citation_liste_ID = new List<string>(),
                N1_SOUR_source_liste_ID = new List<string>()
            };
            int niveau_I = Extraire_niveau(ligne);
            string niveau_S = niveau_I.ToString();
            info.N0_Type = Extraire_balise(dataGEDCOM[ligne]);
            if (info.N0_Type.ToUpper() == niveau_S + " BAPL") info.N0_Type = "Baptême célébré à l'âge de huit ans ou plus par l'Église SDJ";
            if (info.N0_Type.ToUpper() == niveau_S + " CONL") info.N0_Type = "Devient membre de l'Église SDJ.";
            if (info.N0_Type.ToUpper() == niveau_S + " ENDL") info.N0_Type = "Dotation a été accomplie par la prêtrise autorité dans un temple SDJ.";
            if (info.N0_Type.ToUpper() == niveau_S + " SLGC") info.N0_Type = "Scellement d'un enfant à ses parents lors d'une cérémonie au temple SDJ.";
            ligne++;
            while (Extraire_niveau(ligne) > niveau_I)
            {
                if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " DATE")          // +1 DATE
                {
                    info.N1_DATE = ConvertirDateGEDCOM(Extraire_ligne(dataGEDCOM[ligne], 4));
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " TEMP")     // +1 TEMP
                {
                    info.N1_TEMP = Extraire_ligne(dataGEDCOM[ligne], 4);
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " FAMC")     // +1 FAMC
                {
                    info.N1_FAMC = Extraire_ID(dataGEDCOM[ligne]);
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " PLAC")     // +1 PLAC
                {
                    info.N1_PLAC = Extraire_ligne(dataGEDCOM[ligne], 4);
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " STAT")     // +1 STAT
                {
                    info.N1_STAT = Extraire_ligne(dataGEDCOM[ligne], 4);
                    ligne++;
                    if (info.N1_STAT.ToUpper() == "CHILD") info.N1_STAT = "Décédé avant l'âge de huit ans, le baptême n'est pas requis.";//BAPL CONL ENDL
                    if (info.N1_STAT.ToUpper() == "COMPLETED") info.N1_STAT = "Terminer mais la date n'est pas connue.";//BAPL CONL ENDL
                    if (info.N1_STAT.ToUpper() == "EXCLUDED") info.N1_STAT = "Le patron a exclu que cette ordonnance soit effacée dans cette soumission.";//BAPL CONL ENDL SLGC
                    if (info.N1_STAT.ToUpper() == "PRE-1970") info.N1_STAT = "L'ordonnance est probablement terminée, une autre ordonnance pour cette personne a été convertie à partir des registres du temple des travaux achevés avant 1970, donc cette ordonnance est supposé complet jusqu'à la conversion de tous les enregistrements."; //BAPL CONL ENDL SLGC
                    if (info.N1_STAT.ToUpper() == "STILLBORN") info.N1_STAT = "Mort-né, baptême non requis.";//BAPL CONL ENDL SLGC
                    if (info.N1_STAT.ToUpper() == "SUBMITTED") info.N1_STAT = "Une ordonnance avait déjà été soumise."; // ENDL SLGC
                    if (info.N1_STAT.ToUpper() == "UNCLEARED") info.N1_STAT = "Data for clearing ordinance request was insufficient.";//BAPL CONL ENDLSLGC
                    if (info.N1_STAT.ToUpper() == "INFANT") info.N1_STAT = "Décédé avant moins d'un an, baptême ou dotation non requis."; // ENDL
                    if (info.N1_STAT.ToUpper() == "BIC") info.N1_STAT = "Né dans l'alliance recevant la bénédiction de l'enfant au scellement des parents.";// SLGC
                    while (Extraire_niveau(ligne) > niveau_I + 1)
                    {
                        if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 2).ToString() + " DATE")  // +2 STAT_DATE
                        {
                            info.N2_STAT_DATE = ConvertirDateGEDCOM(Extraire_ligne(dataGEDCOM[ligne], 4));
                            ligne++;
                        }
                        else
                        {
                            EcrireBalise(ligne);
                            ligne++;
                        }
                    }
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " NOTE")     // +1 NOTE
                {
                    string IDNote;
                    (IDNote, ligne) = Extraire_NOTE_RECORD(ligne);
                    info.N1_NOTE_liste_ID.Add(IDNote);
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " SOUR")     // +1 SOUR
                {
                    string citation;
                    string source;
                    (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                    if (citation != null) info.N1_SOUR_citation_liste_ID.Add(citation);
                    if (source != null) info.N1_SOUR_source_liste_ID.Add(source);
                }
                else
                {
                    EcrireBalise(ligne);
                    ligne++;
                }
            }
            return (info, ligne);
        }
        private static (LDS_SPOUSE_SEALING, int) Extraire_LDS_SPOUSE_SEALING(int ligne)
        {
            LDS_SPOUSE_SEALING info = new LDS_SPOUSE_SEALING
            {
                N1_DATE = null,
                N1_TEMP = null,
                N1_PLAC = null,
                N1_STAT = null,
                N2_STAT_DATE = null,
                N1_NOTE_liste_ID = new List<string>(),
                N1_SOUR_citation_liste_ID = new List<string>(),
                N1_SOUR_source_liste_ID = new List<string>()
            };
            int niveau_I = Extraire_niveau(ligne);
            ligne++;
            while (Extraire_niveau(ligne) > niveau_I)
            {
                if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " DATE")          // +1 DATE
                {
                    info.N1_DATE = ConvertirDateGEDCOM(Extraire_ligne(dataGEDCOM[ligne], 4));
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " TEMP")     // +1 TEMP
                {
                    info.N1_TEMP = Extraire_ligne(dataGEDCOM[ligne], 4);
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " PLAC")     // +1 PLAC
                {
                    info.N1_PLAC = Extraire_ligne(dataGEDCOM[ligne], 4);
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " STAT")     // +1 STAT
                {
                    info.N1_STAT = Extraire_ligne(dataGEDCOM[ligne], 4);
                    ligne++;
                    if (info.N1_STAT == "CANCELED") info.N1_STAT = "Annuler et cosidérer invalide";
                    if (info.N1_STAT == "COMPLETED") info.N1_STAT = "Terminé mais la date n'est pas connue.";
                    if (info.N1_STAT == "EXCLUDED") info.N1_STAT = "Le patron a exclu que cette ordonnance soit effacée dans cette soumission.";
                    if (info.N1_STAT == "DNS") info.N1_STAT = "Cette ordonnance n'est pas autorisée.";
                    if (info.N1_STAT == "DNS/CAN") info.N1_STAT = "Cette ordonnance n'est pas autorisée, le scellement précédent a été annulé.";
                    if (info.N1_STAT == "PRE-1970") info.N1_STAT = "L'ordonnance est probablement terminée, une autre ordonnance pour cette personne a été convertie à partir des registres du temple des travaux achevés avant 1970, donc cette ordonnance est supposé complet jusqu'à la conversion de tous les enregistrements.";
                    if (info.N1_STAT == "SUBMITTED") info.N1_STAT = "Une ordonnance avait déjà été soumise.";
                    if (info.N1_STAT == "UNCLEARED") info.N1_STAT = "Data for clearing ordinance request was insufficient.";
                    while (Extraire_niveau(ligne) > niveau_I + 1)
                    {
                        if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 2).ToString() + " DATE")  // +2 STAT_DATE
                        {
                            info.N2_STAT_DATE = ConvertirDateGEDCOM(Extraire_ligne(dataGEDCOM[ligne], 4));
                            ligne++;
                        }
                        else
                        {
                            EcrireBalise(ligne);
                            ligne++;
                        }
                    }
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " NOTE")     // +1 NOTE
                {
                    string IDNote;
                    (IDNote, ligne) = Extraire_NOTE_RECORD(ligne);
                    info.N1_NOTE_liste_ID.Add(IDNote);
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " SOUR")     // +1 SOUR
                {
                    string citation;
                    string source;
                    (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                    if (citation != null) info.N1_SOUR_citation_liste_ID.Add(citation);
                    if (source != null) info.N1_SOUR_source_liste_ID.Add(source);
                }
                else
                {
                    EcrireBalise(ligne);
                    ligne++;
                }
            }
            return (info, ligne);
        }
        private static (string, int) Extraire_MULTIMEDIA_RECORD(int ligne)
        {
            string N0_ID;
            string N2_FILE = null;
            string N2_FORM = null;
            string N3_FORM_TYPE = null;
            string N2_TITL = null;
            List<USER_REFERENCE_NUMBER> N1_REFN_liste = null;// new List<USER_REFERENCE_NUMBER>();
            string N1_RIN = null;
            List<string> N1_NOTE_liste_ID = new List<string>();
            List<string> N1_SOUR_citation_liste_ID = new List<string>();
            CHANGE_DATE N1_CHAN = new CHANGE_DATE();
            bool ajouter = false;
            N0_ID = Extraire_ID(dataGEDCOM[ligne]);
            if (N0_ID == null) N0_ID = "O-" + DateTime.Now.ToString("HHmmssffffff" + hazard.Next(999).ToString());
            // niveau 1
            {
                int niveau_I = Extraire_niveau(ligne);
                ligne++;
                while (Extraire_niveau(ligne) > niveau_I) // > +0
                {
                    ajouter = true;
                    if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " FILE")      // +1 FILLE
                    {
                        if (dataGEDCOM[ligne].Length > 7)
                        {
                            N2_FILE = dataGEDCOM[ligne].Substring(7, dataGEDCOM[ligne].Length - 7);
                        }
                        ligne++;
                        while (Extraire_niveau(ligne) > niveau_I + 1) // > +1
                        {
                            if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 2).ToString() + " FORM")// +2 FORM
                            {
                                N2_FORM = dataGEDCOM[ligne].Substring(7, dataGEDCOM[ligne].Length - 7);
                                ligne++;
                                while (Extraire_niveau(ligne) > niveau_I + 2)
                                {
                                    if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 3).ToString() + " TYPE")// +3 TYPE
                                    {
                                        if (dataGEDCOM[ligne].Length > 7)
                                        {
                                            N3_FORM_TYPE = dataGEDCOM[ligne].Substring(7, dataGEDCOM[ligne].Length - 7);
                                        }
                                        ligne++;
                                    }
                                    else
                                    {
                                        EcrireBalise(ligne);
                                        ligne++;
                                    }
                                }
                            }
                            else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 2).ToString() + " TITL")// +2 TITL
                            {
                                if (dataGEDCOM[ligne].Length > 7)
                                {
                                    N2_TITL = dataGEDCOM[ligne].Substring(7, dataGEDCOM[ligne].Length - 7);
                                    ligne++;
                                }
                            }
                            else
                            {
                                EcrireBalise(ligne);
                                ligne++;
                            }
                        }
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " FORM") // +1 FORM
                    {
                        if (dataGEDCOM[ligne].Length > 7)
                        {
                            N2_FORM = dataGEDCOM[ligne].Substring(7, dataGEDCOM[ligne].Length - 7);
                            ligne++;
                        }
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " TITL") // +1 TITL
                    {
                        if (dataGEDCOM[ligne].Length > 7)
                        {
                            N2_TITL = dataGEDCOM[ligne].Substring(7, dataGEDCOM[ligne].Length - 7);
                            ligne++;
                        }
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 REFN")                            // 1 REFN
                    {
                        USER_REFERENCE_NUMBER N1_REFN;
                        (N1_REFN, ligne) = Extraire_USER_REFERENCE_NUMBER(ligne);
                        N1_REFN_liste.Add(N1_REFN);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " NOTE") // +1 NOTE
                    {
                        string IDNote;
                        (IDNote, ligne) = Extraire_NOTE_RECORD(ligne);
                        N1_NOTE_liste_ID.Add(IDNote);
                    }
                    else
                    {
                        EcrireBalise(ligne);
                        ligne++;
                    }
                }
            }
            if (ajouter)
            {
                liste_MULTIMEDIA_RECORD.Add(new MULTIMEDIA_RECORD()
                {
                    N0_ID = N0_ID,
                    N1_FILE = N2_FILE,
                    N2_FILE_FORM = N2_FORM,
                    N2_FILE_TITL = N2_TITL,
                    N3_FILE_FORM_TYPE = N3_FORM_TYPE,
                    N1_REFN_liste = N1_REFN_liste,
                    N1_RIN = N1_RIN,
                    N1_NOTE_liste_ID = N1_NOTE_liste_ID,
                    N1_SOUR_citation_liste_ID = N1_SOUR_citation_liste_ID,
                    N1_CHAN = N1_CHAN
                });
            }
            return (N0_ID, ligne);
        }
        private static void Extraire_MULTIMEDIA_RECORD_N0(int ligne)
        {
            string N0_ID;
            string N1_FORM = null; // v5.5
            string N1_TITL = null; // v5.5
            string N1_FILE = null;
            string N2_FILE_FORM = null;
            string N3_FILE_FORM_TYPE = null;
            string N2_FILE_TITL = null;
            List<USER_REFERENCE_NUMBER> N1_REFN_liste = new List<USER_REFERENCE_NUMBER>();
            string N1_RIN = null;
            List<string> N1_NOTE_liste_ID = new List<string>();
            string N1_BLOB = null; // v5.5
            string N1_OBJE = null; // v5.5
            string N1__DATE = null;// Herisis 

            List<string> N1_SOUR_citation_liste_ID = new List<string>();
            List<string> N1_SOUR_source_liste_ID = new List<string>();
            CHANGE_DATE N1_CHAN = new CHANGE_DATE();
            N0_ID = Extraire_ID(dataGEDCOM[ligne]);
            Tb_Status.Text = "Décodage du data. Lecture des médias  ID " + N0_ID;
            Animation(true);
            ligne++;
            Avoir_code_erreur();
            try
            {
                Avoir_code_erreur();
                while (Extraire_niveau(ligne) > 0)
                {
                    Avoir_code_erreur();
                    Application.DoEvents();
                    if (Extraire_balise(dataGEDCOM[ligne]) == "1 FORM")                                 // 1 FORM v5.5
                    {
                        Avoir_code_erreur();
                        N1_FORM = Extraire_ligne(dataGEDCOM[ligne], 4);
                        ligne++;
                    }
                    if (Extraire_balise(dataGEDCOM[ligne]) == "1 TITL")                                 // 1 TITL v5.5
                    {
                        Avoir_code_erreur();
                        N1_TITL = Extraire_ligne(dataGEDCOM[ligne], 4);
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 FILE")                            // 1 FILLE
                    {
                        Avoir_code_erreur();
                        N1_FILE = Extraire_ligne(dataGEDCOM[ligne], 4);
                        ligne++;
                        while (Extraire_niveau(ligne) > 1)
                        {
                            Avoir_code_erreur();
                            if (Extraire_balise(dataGEDCOM[ligne]) == "2 FORM")                         // 2 FORM
                            {
                                Avoir_code_erreur();
                                N2_FILE_FORM = Extraire_ligne(dataGEDCOM[ligne], 4);
                                ligne++;
                                while (Extraire_niveau(ligne) > 2)
                                {
                                    Avoir_code_erreur();
                                    if (Extraire_balise(dataGEDCOM[ligne]) == "3 TYPE")                 // 3 FORM TYPE
                                    {
                                        Avoir_code_erreur();
                                        N3_FILE_FORM_TYPE = Extraire_ligne(dataGEDCOM[ligne], 4);
                                        N3_FILE_FORM_TYPE = Avoir_type_media(N3_FILE_FORM_TYPE);
                                        ligne++;
                                    }
                                    else
                                    {
                                        Avoir_code_erreur();
                                        EcrireBalise(ligne);
                                        ligne++;
                                    }
                                }
                            }
                            else if (Extraire_balise(dataGEDCOM[ligne]) == "2 TITL")                    // 2 TITL
                            {
                                Avoir_code_erreur();
                                N2_FILE_TITL = Extraire_ligne(dataGEDCOM[ligne], 4);
                                ligne++;
                                while (Extraire_niveau(ligne) > 2)
                                {
                                    Avoir_code_erreur();
                                    if (Extraire_balise(dataGEDCOM[ligne]) == "3 CONC")                 // 2 CONC
                                    {
                                        Avoir_code_erreur();
                                        N2_FILE_TITL += " " + Extraire_ligne(dataGEDCOM[ligne], 4);
                                        ligne++;
                                    }
                                    else if (Extraire_balise(dataGEDCOM[ligne]) == "3 CONT")            // 2 CONT
                                    {
                                        Avoir_code_erreur();
                                        N2_FILE_TITL += "<br/>" + Extraire_ligne(dataGEDCOM[ligne], 4);
                                        ligne++;
                                    }
                                    else
                                    {
                                        Avoir_code_erreur();
                                        EcrireBalise(ligne);
                                        ligne++;
                                    }
                                }
                            }
                            else
                            {
                                Avoir_code_erreur();
                                EcrireBalise(ligne);
                                ligne++;
                            }
                        }
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 REFN")                            // 1 REFN
                    {
                        Avoir_code_erreur();
                        USER_REFERENCE_NUMBER N1_REFN;
                        (N1_REFN, ligne) = Extraire_USER_REFERENCE_NUMBER(ligne);
                        N1_REFN_liste.Add(N1_REFN);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 RIN")                             // 1 RIN
                    {
                        Avoir_code_erreur();
                        N1_RIN = Extraire_ligne(dataGEDCOM[ligne], 3);
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 NOTE")                            // 1 NOTE
                    {
                        Avoir_code_erreur();
                        string IDNote;
                        (IDNote, ligne) = Extraire_NOTE_RECORD(ligne);
                        N1_NOTE_liste_ID.Add(IDNote);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 BLOB")                            // 1 BLOB v5.5
                    {
                        Avoir_code_erreur();
                        N1_BLOB = "blob";
                        N1_FILE = "blob.svg";
                        N2_FILE_FORM = "svg";
                        ligne++;
                        while (Extraire_niveau(ligne) > 1)
                        {
                            Avoir_code_erreur();
                            Avoir_code_erreur();
                            if (Extraire_balise(dataGEDCOM[ligne]) == "2 CONT")                             // 2 CONT
                            {
                                Avoir_code_erreur();
                                ligne++;
                            }
                            else
                            {
                                Avoir_code_erreur();
                                EcrireBalise(ligne);
                                ligne++;
                            }
                        }
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 OBJE")                                // 1 OBJE v5.5
                    {
                        Avoir_code_erreur();
                        ligne++;
                        while (Extraire_niveau(ligne) > 1)
                        {
                            Avoir_code_erreur();
                            EcrireBalise(ligne);
                            ligne++;
                        }
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 SOUR")                                // 1 SOUR
                    {
                        Avoir_code_erreur();
                        string citation;
                        string source;
                        (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                        if (citation != null) N1_SOUR_citation_liste_ID.Add(citation);
                        if (source != null) N1_SOUR_source_liste_ID.Add(source);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 CHAN")                                // 1 CHAN changer date
                    {
                        Avoir_code_erreur();
                        (N1_CHAN, ligne) = Extraire_CHANGE_DATE(ligne);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 _DATE")                           // _DATE Heridis
                    {
                        Avoir_code_erreur();
                        N1__DATE = ConvertirDateGEDCOM(Extraire_ligne(dataGEDCOM[ligne], 5));
                        ligne++;
                    }

                    else
                    {
                        Avoir_code_erreur();
                        EcrireBalise(ligne);
                        ligne++;

                    }
                }
            }
            catch (Exception msg)
            {
                string message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                Voir_message(message, msg.Message, erreur);
            }
            if (!GH.GH.annuler)
            {
                liste_MULTIMEDIA_RECORD.Add(new MULTIMEDIA_RECORD()
                {
                    N0_ID = N0_ID,
                    N1_FORM = N1_FORM, //v5.5
                    N1_TITL = N1_TITL, // v5.5
                    N1_FILE = N1_FILE,
                    N2_FILE_FORM = N2_FILE_FORM,
                    N3_FILE_FORM_TYPE = N3_FILE_FORM_TYPE,
                    N2_FILE_TITL = N2_FILE_TITL,
                    N1_REFN_liste = N1_REFN_liste,
                    N1_RIN = N1_RIN,
                    N1_NOTE_liste_ID = N1_NOTE_liste_ID,
                    N1_BLOB = N1_BLOB, // v5.5
                    N1_OBJE = N1_OBJE, // v5.5
                    N1_SOUR_citation_liste_ID = N1_SOUR_citation_liste_ID,
                    N1_SOUR_source_liste_ID = N1_SOUR_source_liste_ID,
                    N1_CHAN = N1_CHAN,
                    N1__DATE = N1__DATE // Herisis
                });
            }
        }
        private static (string, int) Extraire_SOURCE_RECORD(int ligne)
        {
           
            bool ajouter = false;
            string N0_ID;
            string N2_DATA_EVEN = null;
            string N3_DATA_EVEN_DATE = null;
            string N3_DATA_EVEN_PLAC = null;
            string N2_DATA_AGNC = null;
            List<string> N2_DATA_NOTE_liste_ID = new List<string>();
            string N1_AUTH = null;
            string N1_TITL = null;
            string N1_ABBR = null;
            string N1_PUBL = null;
            string N1_TEXT = null;
            string N1_QUAY = null; //Heridis
            string N1_TYPE = null; // Heridis
            string N1_DATE = null; // Herisis
            SOURCE_REPOSITORY_CITATION N1_REPO_info = null;
            List<USER_REFERENCE_NUMBER> N1_REFN_liste = new List<USER_REFERENCE_NUMBER>();
            string N1_RIN = null;
            CHANGE_DATE N1_CHAN = new CHANGE_DATE
            {
                N1_CHAN_DATE = null,
                N2_CHAN_DATE_TIME = null
            };
            List<string> N1_NOTE_liste_ID = new List<string>();
            List<string> N1_OBJE_liste_ID = new List<string>();
            string N1_EVEN = null; // acestrologie valeur I, F
            N0_ID = Extraire_ID(dataGEDCOM[ligne]);
            Tb_Status.Text = "Décodage du data. Lecture des Sources  ID " + N0_ID;
            Animation(true);
            string niveau = Extraire_niveau(ligne).ToString();
            int niveau_I = Extraire_niveau(ligne);
            ligne++;
            Avoir_code_erreur();
            // niveau 0
            if (niveau == "0")
            {
                Avoir_code_erreur();
                while (Extraire_niveau(ligne) > 0)
                {
                    Avoir_code_erreur();
                    try
                    {
                        Avoir_code_erreur();
                        Application.DoEvents();
                        ajouter = true;
                        string baliseN1 = Extraire_balise(dataGEDCOM[ligne]);
                        if (baliseN1 == niveau_I + 1 + " DATA")                                         // +1 DATA
                        {
                            Avoir_code_erreur();
                            ligne++;
                            while (Extraire_niveau(ligne) > 1)
                            {
                                string baliseN2 = Extraire_balise(dataGEDCOM[ligne]);
                                Avoir_code_erreur();
                                if (baliseN2 == (niveau_I + 2).ToString() + " EVEN")                    // +2 DATA_EVEN  BIRT, DEAT, MARR.
                                {
                                    Avoir_code_erreur();
                                    Extraire_balise(dataGEDCOM[ligne]);
                                    N2_DATA_EVEN = Extraire_EVENT_liste(Extraire_ligne(dataGEDCOM[ligne], 4));
                                    ligne++;
                                    while (Extraire_niveau(ligne) > 2)
                                    {
                                        string baliseN3 = Extraire_balise(dataGEDCOM[ligne]);
                                        Avoir_code_erreur();
                                        if (baliseN3 == (niveau_I + 3).ToString() + " DATE")                // +3 DATE
                                        {
                                            Avoir_code_erreur();
                                            N3_DATA_EVEN_DATE = ConvertirDateGEDCOM(Extraire_ligne(dataGEDCOM[ligne], 4));
                                            ligne++;
                                        }
                                        else if (baliseN3 == (niveau_I + 3).ToString() + " PLAC")            // +3 PLAC
                                        {
                                            Avoir_code_erreur();
                                            N3_DATA_EVEN_PLAC = Extraire_ligne(dataGEDCOM[ligne], 4);
                                            ligne++;
                                        }
                                        else
                                        {
                                            Avoir_code_erreur();
                                            EcrireBalise(ligne);
                                            ligne++;
                                        }
                                    }
                                }
                                else if (baliseN2 == "2 AGNC")                    // 2 AGNC
                                {
                                    Avoir_code_erreur();
                                    N2_DATA_AGNC = Extraire_ligne(dataGEDCOM[ligne], 4);
                                    ligne++;
                                }
                                else if (baliseN2 == "2 NOTE")                    // 2 NOTE
                                {
                                    Avoir_code_erreur();
                                    string IDNote;
                                    (IDNote, ligne) = Extraire_NOTE_RECORD(ligne);
                                    N2_DATA_NOTE_liste_ID.Add(IDNote);
                                }
                                else
                                {
                                    Avoir_code_erreur();
                                    EcrireBalise(ligne);
                                    ligne++;
                                }
                            }
                        }
                        else if (baliseN1 == "1 DATE")                 // 1 DATE Herisis
                        {
                            Avoir_code_erreur();
                            N1_DATE = ConvertirDateGEDCOM(Extraire_ligne(dataGEDCOM[ligne], 4));
                            ligne++;
                        }
                        else if (baliseN1 == "1 TITL")                                                      // 1 TITL
                        {
                            Avoir_code_erreur();
                            N1_TITL = Extraire_ligne(dataGEDCOM[ligne], 4);
                            ligne++;
                            while (Extraire_niveau(ligne) > 1)
                            {
                                string baliseN2 = Extraire_balise(dataGEDCOM[ligne]);
                                Avoir_code_erreur();
                                if (baliseN2 == "2 CONC")                         // 2 CONC
                                {
                                    Avoir_code_erreur();
                                    N1_TITL += Extraire_ligne(dataGEDCOM[ligne], 4);
                                    ligne++;
                                }
                                else if (baliseN2 == "2 CONT")                    // 2 CONT
                                {
                                    Avoir_code_erreur();
                                    N1_TITL += "<br/>" + Extraire_ligne(dataGEDCOM[ligne], 4);
                                    ligne++;
                                }
                                else
                                {
                                    Avoir_code_erreur();
                                    EcrireBalise(ligne);
                                    ligne++;
                                }
                            }
                        }
                        else if (baliseN1 == "1 AUTH")                                                      // 1 AUTH
                        {
                            Avoir_code_erreur();
                            N1_AUTH = Extraire_ligne(dataGEDCOM[ligne], 4);
                            ligne++;
                            while (Extraire_niveau(ligne) > 1)
                            {
                                string baliseN2 = Extraire_balise(dataGEDCOM[ligne]);
                                Avoir_code_erreur();
                                if (baliseN2 == "2 CONT")                         // 2 CONT
                                {
                                    Avoir_code_erreur();
                                    N1_AUTH += "<br/>" + Extraire_ligne(dataGEDCOM[ligne], 4);
                                    ligne++;
                                }
                                else if (baliseN2 == "2 CONC")                    // 2 CONC
                                {
                                    Avoir_code_erreur();
                                    N1_AUTH += " " + Extraire_ligne(dataGEDCOM[ligne], 4);
                                    ligne++;
                                }
                                else
                                {
                                    Avoir_code_erreur();
                                    EcrireBalise(ligne);
                                    ligne++;
                                }
                            }

                        }
                        else if (baliseN1 == "1 PUBL")                                                      // 1 PUBL
                        {
                            Avoir_code_erreur();
                            N1_PUBL = Extraire_ligne(dataGEDCOM[ligne], 4);
                            ligne++;
                            while (Extraire_niveau(ligne) > 1)
                            {
                                string baliseN2 = Extraire_balise(dataGEDCOM[ligne]);
                                Avoir_code_erreur();
                                if (baliseN2 == "2 CONC")                         // 2 CONC
                                {
                                    Avoir_code_erreur();
                                    N1_PUBL += " " + Extraire_ligne(dataGEDCOM[ligne], 4);
                                    ligne++;
                                }
                                else if (baliseN2 == "2 CONT")                    // 2 CONT
                                {
                                    Avoir_code_erreur();
                                    if (dataGEDCOM[ligne].Length > 7)
                                    {
                                        Avoir_code_erreur();
                                        N1_PUBL += "<br />" + dataGEDCOM[ligne].Substring(7);
                                    }
                                    else N1_PUBL += "<br />\n";
                                    ligne++;
                                }
                                else
                                {
                                    Avoir_code_erreur();
                                    EcrireBalise(ligne);
                                    ligne++;
                                }
                            }
                        }
                        else if (baliseN1 == "1 TEXT" && dataGEDCOM[ligne].Length > 7)                      // 1 TEXT
                        {
                            Avoir_code_erreur();
                            (N1_TEXT, ligne) = Extraire_TEXT(ligne);
                        }
                        else if (baliseN1 == "1 REFN")                            // 1 REFN
                        {
                            Avoir_code_erreur();
                            USER_REFERENCE_NUMBER N1_REFN;
                            (N1_REFN, ligne) = Extraire_USER_REFERENCE_NUMBER(ligne);
                            N1_REFN_liste.Add(N1_REFN);
                        }
                        else if (baliseN1 == "1 ABBR")                                                      // 1 ABBR
                        {
                            Avoir_code_erreur();
                            N1_ABBR = Extraire_ligne(dataGEDCOM[ligne], 4);
                            ligne++;
                        }
                        else if (baliseN1 == "1 OBJE")                                                      // 1 OBJE
                        {
                            Avoir_code_erreur();
                            string temp;
                            (temp, ligne) = Extraire_MULTIMEDIA_RECORD(ligne);
                            N1_OBJE_liste_ID.Add(temp);
                        }
                        else if (baliseN1 == "1 REPO")                                                      // 1 REPO
                        {
                            Avoir_code_erreur();
                            (N1_REPO_info, ligne) = Extraire_SOURCE_REPOSITORY_CITATION(ligne);
                        }
                        else if (baliseN1 == "1 NOTE")                                                      // 1 NOTE
                        {
                            Avoir_code_erreur();
                            string IDNote;
                            (IDNote, ligne) = Extraire_NOTE_RECORD(ligne);
                            N1_NOTE_liste_ID.Add(IDNote);
                        }
                        else if (baliseN1 == "1 CHAN")                                                      // 1 CHAN changer date
                        {
                            Avoir_code_erreur();
                            (N1_CHAN, ligne) = Extraire_CHANGE_DATE(ligne);
                        }
                        else if (baliseN1 == "1 RIN")                             // 1 RIN
                        {
                            Avoir_code_erreur();
                            N1_RIN = Extraire_ligne(dataGEDCOM[ligne], 3);
                            ligne++;
                        }
                        else if (baliseN1 == "1 EVEN")                            // 1 EVEN dans ancestrologie
                        {
                            Avoir_code_erreur();
                            N1_EVEN = Extraire_ligne(dataGEDCOM[ligne], 4);
                            ligne++;
                        }
                        else if (baliseN1 == "1 QUAY")                            // 1 QUAY Heridis
                        {
                            Avoir_code_erreur();
                            N1_QUAY = Extraire_ligne(dataGEDCOM[ligne], 4);
                            ligne++;
                        }
                        else if (baliseN1 == "1 TYPE")                            // 1 TYPE Heridis
                        {
                            Avoir_code_erreur();
                            N1_TYPE = Extraire_ligne(dataGEDCOM[ligne], 4);
                            switch (N1_TYPE.ToUpper())
                            {
                                case "ADMINISTRATIVE DOCUMENT": N1_TYPE = "Document administratif"; break;
                                case "DEED": N1_TYPE = "Acte"; break;
                                case "OTHER": N1_TYPE = "Autre"; break;
                            }
                            ligne++;
                        }
                        else
                        {
                            Avoir_code_erreur();
                            EcrireBalise(ligne);
                            ligne++;
                        }
                    }
                    catch (Exception msg)
                    {
                        string message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                        Voir_message(message, msg.Message, erreur);
                        if (GH.GH.annuler)
                        {
                            return (N0_ID, ligne);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Souce niveau inconnu", "Problème ? Souce niveau", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (ajouter && !GH.GH.annuler)
            {
                liste_SOURCE_RECORD.Add(new SOURCE_RECORD()
                {
                    N0_ID = N0_ID,
                    N2_DATA_EVEN = N2_DATA_EVEN,
                    N3_DATA_DATE = N3_DATA_EVEN_DATE,
                    N3_DATA_PLAC = N3_DATA_EVEN_PLAC,
                    N2_DATA_AGNC = N2_DATA_AGNC,
                    N2_DATA_NOTE_liste_ID = N2_DATA_NOTE_liste_ID,
                    N1_AUTH = N1_AUTH,
                    N1_TITL = N1_TITL,
                    N1_ABBR = N1_ABBR,
                    N1_PUBL = N1_PUBL,
                    N1_TEXT = N1_TEXT,
                    N1_REPO_info = N1_REPO_info,
                    N1_REFN_liste = N1_REFN_liste,
                    N1_RIN = N1_RIN,
                    N1_CHAN = N1_CHAN,
                    N1_NOTE_liste_ID = N1_NOTE_liste_ID,
                    N1_OBJE_liste_ID = N1_OBJE_liste_ID,
                    N1_EVEN = N1_EVEN,
                    N1_QUAY = N1_QUAY,
                    N1_TYPE = N1_TYPE,
                    N1_DATE = N1_DATE
                });
            }
            return (N0_ID, ligne);
        }
        private static (PLACE_STRUCTURE, int) Extraire_PLACE_STRUCTURE(int ligne)
        {
            PLACE_STRUCTURE info = new PLACE_STRUCTURE();
            int niveau_I = Extraire_niveau(ligne);
            info.N0_PLAC = null;
            info.N1_FORM = null;
            info.N1_FONE = null;
            info.N2_FONE_TYPE = null;
            info.N1_ROMN = null;
            info.N2_ROMN_TYPE = null;
            info.N2_MAP_LATI = null;
            info.N2_MAP_LONG = null;
            info.N1_NOTE_liste_ID = new List<string>();
            info.N1_SOUR_citation_liste_ID = new List<string>(); // pour GEDitCOM
            info.N1_SOUR_source_liste_ID = new List<string>(); // pour GEDitCOM
            if (dataGEDCOM[ligne].Length > 7)
            {
                info.N0_PLAC = Extraire_ligne(dataGEDCOM[ligne], 4);                                    // PLAC
            }
            ligne++;
            while (Extraire_niveau(ligne) > niveau_I)
            {
                if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " FORM")          // +1 FORM
                {
                    info.N1_FORM += Extraire_ligne(dataGEDCOM[ligne], 4);
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " FONE")     // +1 FONE
                {
                    info.N1_FONE += Extraire_ligne(dataGEDCOM[ligne], 4);
                    ligne++;
                    while (Extraire_niveau(ligne) > niveau_I + 1)
                    {
                        if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 2).ToString() + " TYPE")  // +2 FONE_TYPE+
                        {
                            info.N2_FONE_TYPE = Extraire_ligne(dataGEDCOM[ligne], 4);
                            ligne++;

                        }
                        else
                        {
                            EcrireBalise(ligne);
                            ligne++;
                        }
                    }
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " ROMN")     // +1 ROMN
                {
                    info.N1_ROMN = Extraire_ligne(dataGEDCOM[ligne], 4);
                    ligne++;
                    while (Extraire_niveau(ligne) > niveau_I + 1)
                    {
                        if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 2).ToString() + " TYPE")  // +2 RONN_TYPE
                        {
                            info.N2_ROMN_TYPE = Extraire_ligne(dataGEDCOM[ligne], 4);
                            ligne++;
                        }
                        else
                        {
                            EcrireBalise(ligne);
                            ligne++;
                        }
                    }
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " MAP")      // +1 MAP
                {
                    ligne++;
                    while (Extraire_niveau(ligne) > niveau_I + 1)
                    {
                        if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 2).ToString() + " LATI")  // +2 MAP_LATI
                        {
                            info.N2_MAP_LATI = Extraire_ligne(dataGEDCOM[ligne], 4);
                            info.N2_MAP_LATI = info.N2_MAP_LATI.Replace("N", "");
                            info.N2_MAP_LATI = info.N2_MAP_LATI.Replace("S", "-");
                            ligne++;
                        }
                        if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 2).ToString() + " LONG")  // +2 MAP_LONG
                        {
                            info.N2_MAP_LONG = Extraire_ligne(dataGEDCOM[ligne], 4);
                            info.N2_MAP_LONG = info.N2_MAP_LONG.Replace("E", "");
                            info.N2_MAP_LONG = info.N2_MAP_LONG.Replace("W", "-");
                            ligne++;
                        }
                        else
                        {
                            EcrireBalise(ligne);
                            ligne++;
                        }
                    }
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " NOTE")     // 1 NOTE
                {
                    string IDNote;
                    (IDNote, ligne) = Extraire_NOTE_RECORD(ligne);
                    info.N1_NOTE_liste_ID.Add(IDNote);
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " SOUR")     // 1 SOUR pour GEDitCOM
                {
                    string citation;
                    string source;
                    (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                    if (citation != null) info.N1_SOUR_citation_liste_ID.Add(citation);
                    if (source != null) info.N1_SOUR_source_liste_ID.Add(source);
                }
                else
                {
                    EcrireBalise(ligne);
                    ligne++;
                }
            }
            return (info, ligne);
        }
        private static (string, int) Extraire_REPOSITORY_RECORD(int ligne)
        {
            REPOSITORY_RECORD info = new REPOSITORY_RECORD
            {
                N0_ID = null,
                N1_NAME = null,
                N1_ADDR = null,
                N1_PHON_liste = null,
                N1_EMAIL_liste = null,
                N1_FAX_liste = null,
                N1_WWW_liste = null,
                N1_NOTE_liste_ID = null,
                N1_REFN_liste = null,
                N1_RIN = null,
                N1_CHAN = null
            };
            List<string> N1_PHON_liste = new List<string>();
            List<string> N1_EMAIL_liste = new List<string>();
            List<string> N1_FAX_liste = new List<string>();
            List<string> N1_WWW_liste = new List<string>();
            List<string> N1_NOTE_liste_ID = new List<string>();
            List<USER_REFERENCE_NUMBER> N1_REFN_liste = new List<USER_REFERENCE_NUMBER>();
            info.N0_ID = Extraire_ID(dataGEDCOM[ligne]);
            Tb_Status.Text = "Décodage du data. Lecture des dépôts  ID " + info.N0_ID;
            Animation(true);
            ligne++;
            Avoir_code_erreur();
            try
            {
                Avoir_code_erreur();
                while (Extraire_niveau(ligne) > 0)
                {
                    Avoir_code_erreur();
                    Application.DoEvents();
                    if (Extraire_balise(dataGEDCOM[ligne]) == "1 NAME")                                     // 1 NAME
                    {
                        Avoir_code_erreur();
                        info.N1_NAME = Extraire_ligne(dataGEDCOM[ligne], 4);
                        ligne++;
                        while (Extraire_niveau(ligne) > 1)
                        {
                            Avoir_code_erreur();
                            if (Extraire_balise(dataGEDCOM[ligne]) == "2 CONT")                             // 2 CONT
                            {
                                Avoir_code_erreur();
                                Avoir_code_erreur();
                                info.N1_NAME += "<br/>" + Extraire_ligne(dataGEDCOM[ligne], 4);
                                ligne++;
                            }
                            else if (Extraire_balise(dataGEDCOM[ligne]) == "2 CONC")                        // 1 CONC
                            {
                                Avoir_code_erreur();
                                info.N1_NAME += " " + Extraire_ligne(dataGEDCOM[ligne], 4);
                                ligne++;
                            }
                            else
                            {
                                Avoir_code_erreur();
                                EcrireBalise(ligne);
                                ligne++;
                            }
                        }
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 ADDR")                                // 1 ADDR
                    {
                        Avoir_code_erreur();
                        (info.N1_ADDR, ligne) = Extraire_ADDRESS_STRUCTURE(ligne);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 PHON")                                // 1 PHON
                    {
                        Avoir_code_erreur();
                        N1_PHON_liste.Add(Extraire_ligne(dataGEDCOM[ligne], 4));
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 EMAIL")                               // 1 EMAIL
                    {
                        Avoir_code_erreur();
                        N1_EMAIL_liste.Add(Extraire_ligne(dataGEDCOM[ligne], 5));
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 _EMAIL")                              // 1 _EMAIL
                    {
                        Avoir_code_erreur();
                        N1_EMAIL_liste.Add(Extraire_ligne(dataGEDCOM[ligne], 6));
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 FAX")                                 // 1 FAX
                    {
                        Avoir_code_erreur();
                        N1_FAX_liste.Add(Extraire_ligne(dataGEDCOM[ligne], 3));
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 WWW")                                 // 1 wwW
                    {
                        Avoir_code_erreur();
                        N1_WWW_liste.Add(Extraire_ligne(dataGEDCOM[ligne], 3));
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 _URL")                                // 1 URL
                    {
                        Avoir_code_erreur();
                        N1_WWW_liste.Add(Extraire_ligne(dataGEDCOM[ligne], 4));
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 NOTE")                                // 1 NOTE
                    {
                        Avoir_code_erreur();
                        string IDNote;
                        (IDNote, ligne) = Extraire_NOTE_RECORD(ligne);
                        N1_NOTE_liste_ID.Add(IDNote);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 REFN")                                // 1 REFN
                    {
                        Avoir_code_erreur();
                        USER_REFERENCE_NUMBER N1_REFN;
                        (N1_REFN, ligne) = Extraire_USER_REFERENCE_NUMBER(ligne);
                        N1_REFN_liste.Add(N1_REFN);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 RIN")                                 // 1 RIN
                    {
                        Avoir_code_erreur();
                        info.N1_RIN = Extraire_ligne(dataGEDCOM[ligne], 3);
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 CHAN")                                // 1 CHAN
                    {
                        Avoir_code_erreur();
                        (info.N1_CHAN, ligne) = Extraire_CHANGE_DATE(ligne);
                    }
                    else
                    {
                        Avoir_code_erreur();
                        EcrireBalise(ligne);
                        ligne++;
                    }
                }
            }
            catch (Exception msg)
            {
                string message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                Voir_message(message, msg.Message, erreur);
            }
            if (!GH.GH.annuler)
            {
                info.N1_PHON_liste = N1_PHON_liste;
                info.N1_EMAIL_liste = N1_EMAIL_liste;
                info.N1_FAX_liste = N1_FAX_liste;
                info.N1_WWW_liste = N1_WWW_liste;
                info.N1_NOTE_liste_ID = N1_NOTE_liste_ID;
                info.N1_REFN_liste = N1_REFN_liste;
                liste_REPOSITORY_RECORD.Add(info);
            }
            return (info.N0_ID, ligne);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ligne"></param>
        /// <returns> 
        ///     numero citation, 
        ///     source ID, 
        ///     ligne 
        /// </returns>
        private static (string, string, int) Extraire_SOURCE_CITATION(int ligne)
        {
            string N0_ID_citation;
            string N0_ID_source;
            string N0_Titre = null;
            string N1_TEXT = null;
            string N1_PAGE = null;
            string N1_EVEN = null;
            string N2_EVEN_ROLE = null;
            string N1_DATA = null;
            string N2_DATA_DATE = null;
            string N2_DATA_TEXT = null;
            List<string> listeObjetIDN3 = new List<string>();
            List<string> N1_NOTE_liste_ID = new List<string>();
            string N1_QUAY = null;
            string N2__QUAL__SOUR = null; // Herisis
            string N2__QUAL__INFO = null; // Heridis
            string N2__QUAL__EVID = null; // Heridis

        int niveau_I = Extraire_niveau(ligne);
            var itemSourceOld = new SOURCE_CITATION();
            N0_ID_source = Extraire_ID(dataGEDCOM[ligne].Substring(7, dataGEDCOM[ligne].Length - 7));
            if (N0_ID_source == null) N0_Titre = Extraire_texte_ligne1(dataGEDCOM[ligne]);
            ligne++;
            while (Extraire_niveau(ligne) > niveau_I)
            {
                if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " CONT")          // +1 CONT
                {
                    N0_Titre += "<br/>" + dataGEDCOM[ligne].Substring(7, dataGEDCOM[ligne].Length - 7);
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " CONC")     // +1 CONC
                {
                    N0_Titre += " " + dataGEDCOM[ligne].Substring(7, dataGEDCOM[ligne].Length - 7);
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " TEXT")     // +1 TEXT
                {
                    (N1_TEXT, ligne) = Extraire_TEXT(ligne);
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " PAGE")     // +1 PAGE
                {
                    N1_PAGE = Extraire_ligne(dataGEDCOM[ligne], 4);
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " OBJE")     // +1 OBJE
                {
                    string temp;
                    (temp, ligne) = Extraire_MULTIMEDIA_RECORD(ligne);
                    listeObjetIDN3.Add(temp);
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " DATA")     // +1 DATA
                {
                    ligne++;
                    while (Extraire_niveau(ligne) > niveau_I + 1)
                    {
                        if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 2).ToString() + " DATE")  // +2 DATE
                        {
                            N2_DATA_DATE = ConvertirDateGEDCOM(dataGEDCOM[ligne].Substring(7, dataGEDCOM[ligne].Length - 7));
                            ligne++;
                        }
                        else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 2).ToString() +
                                " TEXT")                                                                // +2 TEXT
                        {
                            N2_DATA_TEXT = dataGEDCOM[ligne].Substring(7, dataGEDCOM[ligne].Length - 7);
                            ligne++;
                            while (Extraire_niveau(ligne) > niveau_I + 2)
                            {
                                if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 3).ToString() + 
                                    " CONT" && dataGEDCOM[ligne].Length > 7)                            // +3 CONT
                                {
                                    N2_DATA_TEXT += "<br />" + dataGEDCOM[ligne].Substring(7, dataGEDCOM[ligne].Length - 7);
                                    ligne++;
                                }
                                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 3).ToString() +
                                    " CONC" && dataGEDCOM[ligne].Length > 7)                            // +3 CONT
                                {
                                    N2_DATA_TEXT += dataGEDCOM[ligne].Substring(7, dataGEDCOM[ligne].Length - 7);
                                    ligne++;
                                }
                                else
                                {
                                    ligne++;
                                    EcrireBalise(ligne);
                                }
                            }
                        }
                        else
                        {
                            EcrireBalise(ligne);
                            ligne++;
                        }
                    }
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " NOTE")     // +1 NOTE
                {
                    string IDNote;
                    (IDNote, ligne) = Extraire_NOTE_RECORD(ligne);
                    N1_NOTE_liste_ID.Add(IDNote);
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " EVEN")     // +1 EVEN
                {
                    if (dataGEDCOM[ligne].Length > 7)
                    {
                        N1_EVEN = dataGEDCOM[ligne].Substring(7, dataGEDCOM[ligne].Length - 7);
                    }
                    ligne++;
                    while (Extraire_niveau(ligne) > niveau_I + 1)
                    {
                        if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 2).ToString() + " ROLE")  // +2 ROLE
                        {
                            N2_EVEN_ROLE = ConvertirDateGEDCOM(dataGEDCOM[ligne].Substring(7, dataGEDCOM[ligne].Length - 7));
                            ligne++;
                        }
                        else
                        {

                            EcrireBalise(ligne);
                            ligne++;
                        }
                    }
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " QUAY")     // +1 QUAY
                {
                    if (dataGEDCOM[ligne].Length > 7)
                    {
                        N1_QUAY = dataGEDCOM[ligne].Substring(7, dataGEDCOM[ligne].Length - 7);
                    }
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " _QUAL")     // +1 _QUAL Heridis
                {
                    ligne++;
                    while (Extraire_niveau(ligne) > niveau_I + 1)
                    {
                        if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 2).ToString() + " _SOUR")  // +2 _QUAL__SOUR Heridis
                        {
                            N2__QUAL__SOUR = Extraire_ligne(dataGEDCOM[ligne], 5);
                            ligne++;
                        }
                        if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 2).ToString() + " _INFO")  // +2 _QUAL__INFO Heridis
                        {
                            N2__QUAL__INFO = Extraire_ligne(dataGEDCOM[ligne], 5);
                            ligne++;
                        }
                        if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 2).ToString() + " _EVID")  // +2 _QUAL__EVED Heridis
                        {
                            N2__QUAL__EVID = Extraire_ligne(dataGEDCOM[ligne], 5);
                            ligne++;
                        }
                        else
                        {
                            EcrireBalise(ligne);
                            ligne++;
                        }
                    }
                }
                else
                {
                    EcrireBalise(ligne);
                    ligne++;
                }
            }
            // verifie si la ciation à de l'information ou seulement le ID de la source
            if (
                N0_Titre == null &&
                N1_TEXT == null &&
                N1_PAGE == null &&
                N1_EVEN == null &&
                N2_EVEN_ROLE == null &&
                N1_DATA == null &&
                N2_DATA_DATE == null &&
                N2_DATA_TEXT == null &&
                N1_QUAY == null &&
                N2__QUAL__SOUR == null &&
                N2__QUAL__INFO == null &&
                N2__QUAL__EVID == null &&
                N1_NOTE_liste_ID == null &&
                listeObjetIDN3 == null
                )
            {
                return (null, N0_ID_source, ligne);
            }
            else
            {
                // numero de la citation
                conteur_citation++;
                N0_ID_citation = "C" + conteur_citation.ToString();
                itemSourceOld.N0_ID_citation = N0_ID_citation;
                itemSourceOld.N0_ID_source = N0_ID_source;
                itemSourceOld.N0_Titre = N0_Titre;
                itemSourceOld.N1_TEXT = N1_TEXT;
                itemSourceOld.N1_PAGE = N1_PAGE;
                itemSourceOld.N1_EVEN = N1_EVEN;
                itemSourceOld.N2_EVEN_ROLE = N2_EVEN_ROLE;
                itemSourceOld.N2_DATA_DATE = N2_DATA_DATE;
                itemSourceOld.N2_DATA_TEXT = N2_DATA_TEXT;
                itemSourceOld.N1_QUAY = N1_QUAY;
                itemSourceOld.N2__QUAL__SOUR = N2__QUAL__SOUR;
                itemSourceOld.N2__QUAL__INFO = N2__QUAL__INFO;
                itemSourceOld.N2__QUAL__EVID = N2__QUAL__EVID;
                itemSourceOld.N1_NOTE_liste_ID = N1_NOTE_liste_ID;
                itemSourceOld.N1_OBJE_ID_liste = listeObjetIDN3;
                liste_SOURCE_CITATION.Add(itemSourceOld);
                return (N0_ID_citation, N0_ID_source, ligne);
            }
        }
        public static bool Extraire_GEDCOM()
        {
            Tb_Status.Text = "Décodage du data.";
            Application.DoEvents();
            liste_SUBMITTER_RECORD = new List<SUBMITTER_RECORD>();
            listeInfoNote = new List<NOTE_RECORD>();
            liste_INDIVIDUAL_RECORD = new List<INDIVIDUAL_RECORD>();
            liste_FAM_RECORD = new List<FAM_RECORD>();
            liste_SOURCE_RECORD = new List<SOURCE_RECORD>();
            liste_MULTIMEDIA_RECORD = new List<MULTIMEDIA_RECORD>();
            liste_REPOSITORY_RECORD = new List<REPOSITORY_RECORD>();
            liste_REPOSITORY_RECORD = new List<REPOSITORY_RECORD>();
            long dataGEDCOMCount = dataGEDCOM.Count;
                SiBaliseZero("0 @SUBMISSION@ SUBN");                                                    // 0 SUBM
                ligne = 0;
                Tb_Status.Text = "Décodage du data. Lecture des chercheurs";
                Application.DoEvents();
                do
                {
                    if (SiBaliseZero(dataGEDCOM[ligne]) == "SUBM")
                    {
                        if (GH.GH.annuler) return (false);
                        ligne = Extraire_SUBMITTER_RECORD(ligne);
                    }
                    else
                    {
                        ligne++;
                    }
                } while (ligne < dataGEDCOMCount - 1);
                if (GH.GH.annuler) return (false);
                // 0 HEAD
                Tb_Status.Text = "Décodage du data. Lecture de l'entête";
                Application.DoEvents();
                ligne = 0;
                do
                {
                    if (SiBaliseZero(dataGEDCOM[ligne]) == "HEAD")                                          // 0 HEAD
                    {
                        if (GH.GH.annuler) return (false);
                        Extraire_HEADER(ligne);
                        break;
                    }
                    else
                    {
                        ligne++;
                    }
                } while (ligne < dataGEDCOM.Count - 1);
                if (GH.GH.annuler) return (false);
                //                                                                                          0 NOTE
                ligne = 0;
                Tb_Status.Text = "Décodage du data. Lecture des notes";
                do
                {
                if (SiBaliseZero(dataGEDCOM[ligne]) == "NOTE")
                    {
                        if (GH.GH.annuler) return (false);
                        (_, ligne) = Extraire_NOTE_RECORD(ligne);
                    }
                    else
                    {
                        ligne++;
                    }
                } while (ligne < dataGEDCOM.Count - 1);
                if (GH.GH.annuler) return (false);
                //                                                                                          0 SUBN
                ligne = 0;
                Tb_Status.Text = "Décodage du data. Lecture du soumissionnaire";
                do
                {
                if (GH.GH.annuler) return (false);
                    if (SiBaliseZero(dataGEDCOM[ligne]) == "SUBN") Extraire_SUBMISSION_RECORD(ligne);
                    ligne++;
                } while (ligne < dataGEDCOM.Count - 1);
                if (GH.GH.annuler) return (false);
                //                                                                                          0 INDI
                ligne = 0;
                Tb_Status.Text = "Décodage du data. Lecture des individus";
                do
                {
                    if (SiBaliseZero(dataGEDCOM[ligne]) == "INDI")
                    {
                        if (GH.GH.annuler) return (false);
                        Extraire_INDIVIDUAL_RECORD(ligne);
                    }
                ligne++;
                } while (ligne < dataGEDCOM.Count - 1);
                if (GH.GH.annuler) return (false);
                //                                                                                          0 FAM
                ligne = 0;
                Tb_Status.Text = "Décodage du data. Lecture des familles";
                do
                {

                    if (GH.GH.annuler) return (false);
                    if (SiBaliseZero(dataGEDCOM[ligne]) == "FAM") Extraire_FAM_RECORD(ligne);
                    ligne++;
                } while (ligne < dataGEDCOM.Count - 1);
                if (GH.GH.annuler) return (false);
                //                                                                                          0 SOUR
                ligne = 0;
                Application.DoEvents();
                Tb_Status.Text = "Décodage du data. Lecture des citations";
                do
                {
                if (GH.GH.annuler) return (false);
                    if (SiBaliseZero(dataGEDCOM[ligne]) == "SOUR") (_, _) = Extraire_SOURCE_RECORD(ligne);
                    ligne++;
                } while (ligne < dataGEDCOM.Count - 1);
                if (GH.GH.annuler) return (false);
                // 0 obje
                ligne = 0;
                Tb_Status.Text = "Décodage du data. Lecture des médias";
                do
                {
                    if (GH.GH.annuler) return (false);
                    if (SiBaliseZero(dataGEDCOM[ligne]) == "OBJE") Extraire_MULTIMEDIA_RECORD_N0(ligne);
                    ligne++;
                } while (ligne < dataGEDCOM.Count - 1);
                if (GH.GH.annuler) return (false);
                //                                                                                          0 REPO
                ligne = 0;
                Tb_Status.Text = "Décodage du data. Lecture des dépôt";
                do
                {
                    if (GH.GH.annuler) return (false);
                    if (SiBaliseZero(dataGEDCOM[ligne]) == "REPO") Extraire_REPOSITORY_RECORD(ligne);
                    ligne++;
                    
                } while (ligne < dataGEDCOM.Count - 1);
                if (GH.GH.annuler) return (false);
                Animation(false);
            dataGEDCOM = new List<string>();
            //Z X C V("Analyse du data complèter"); // ne pas effacer cette lignes
            return true;
        }
        private static void EcrireBalise
            (
            int numeroLigne,
            //string infoLigne,
            [CallerLineNumber] int ligneCode = 0,
            [CallerMemberName] string fonction = null
            )
        {
            if (!GH.Properties.Settings.Default.balise || GH.Properties.Settings.Default.DossierHTML == "") return;
            System.Windows.Forms.Button Btn_balise = Application.OpenForms["GH"].Controls["Btn_balise"] as 
                System.Windows.Forms.Button;
            string fichier = GH.Properties.Settings.Default.DossierHTML + "\\balise.txt";
            try
            {
                using (StreamWriter ligne = File.AppendText(fichier))
                {
                    if (!TagInfoGEDCOM && Info_HEADER.N2_SOUR_NAME != "")
                    {

                        Btn_balise.Visible = true;
                        ligne.WriteLine(DateTime.Now);
                        ligne.WriteLine("*** Balise *************************************************************");
                        ligne.WriteLine("Nom: " + Info_HEADER.N2_SOUR_NAME);
                        ligne.WriteLine("Version: " + Info_HEADER.N2_SOUR_VERS);
                        ligne.WriteLine("Date: " + Info_HEADER.N1_DATE + " " + Info_HEADER.N2_DATE_TIME);
                        ligne.WriteLine("Copyright: " + Info_HEADER.N1_COPR);
                        ligne.WriteLine("Version: " + Info_HEADER.N2_GEDC_VERS);
                        ligne.WriteLine("Code charactère: " + Info_HEADER.N1_CHAR);
                        ligne.WriteLine("Langue: " + Info_HEADER.N1_LANG);
                        ligne.WriteLine("Fichier sur le disque: " + Info_HEADER.Nom_fichier_disque);
                        ligne.WriteLine("***********************************************************************");
                        TagInfoGEDCOM = true;
                    }
                    //string s = String.Format("{0,5} {1,-30:G} {2,5} ►{3}◄ ", ligneCode, fonction, numeroLigne, infoLigne);
                    string s = String.Format("{0,5} {1,5} {2,-35:G} ►{3}◄ ", ligneCode, numeroLigne + 1, fonction,  dataGEDCOM[numeroLigne]);
                    ligne.WriteLine(s);
                }
            }
            catch (Exception msg)
            {
                DialogResult reponse;
                reponse = MessageBox.Show("Ne peut écrire le fichier balise.txt.\r\n\r\n" +
                    msg.Message +
                    "\r\n\r\n",
                    "Erreur " + erreur + " problème ?",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                if (reponse == System.Windows.Forms.DialogResult.Cancel)
                    GH.GH.annuler = true;
            }
        }

        
        public static string Extraire_balise(string ligne)
        {
            ligne = ligne.ToUpper();
            if (ligne.Length > 2)
            {
                string[] s = ligne.Split(' ');
                return s[0] + " " + s[1];
            }
            return "";
        }
        private static string Extraire_ID(string s)
        {
            int p1 = s.IndexOf("@") + 2;
            if (p1 > 9) return ""; // si ID n'est pas au début retour ""
            int p2 = s.IndexOf("@", s.IndexOf('@') + 1);
            if ((p1 >= 0) && (p2 >= 0))
            {
                return s.Substring(p1 - 1, p2 - p1 + 1);
            }
            else
            {
                return null;
            }
        }
        private static string Extraire_texte_balise(int nombre, string ligne)
        {
            if (ligne.Length > 3 + nombre)
            {
                return ligne.Substring(3 + nombre);
            }
            return null;
        }
        private static string Extraire_ligne(string ligne, int nombre_charactere_balise)
        {
            string l = null;
            int nombre = nombre_charactere_balise + 3;
            if (ligne.Length > nombre)
            {
                l = ligne.Substring(nombre, ligne.Length - nombre);
            }
            return l;
        }
        private static string Extraire_NAME(string s)
        {
            if (s == null) return null;
            if (s == "") return null;
            int p1 = s.IndexOf("/");
            int p2 = s.IndexOf("/", p1 + 1);
            string un;
            string deux;
            string trois;
            // aucune barre oblique
            if (p1 < 0 && p2 < 0)
            {
                un = s;
                return un.Trim();
            }
            // une barre oblique
            if (p1 > -1 && p2 == -1)
            {
                un = s.Substring(0, p1).Trim();
                deux = s.Substring(p1 + 1).Trim();
                return deux + " " + un;
            }
            // deux barrre oblique la première position 0
            else if (p1 == 0 && p2 > 0)
            {
                un = s.Substring(p1 + 1, p2 - p1 - 1).Trim();
                deux = s.Substring(p2 + 1).Trim();
                return deux + " " + un;
            }
            // deux barre oblique 
            else if (p1 > 0 && p2 > 0)
            {
                un = s.Substring(0, p1).Trim();
                deux = s.Substring(p1 + 1, p2 - p1 - 1).Trim();
                trois = s.Substring(p2 + 1).Trim();
                return deux + " " + un + " " + trois;
            }
            return s;
        }
        private static void Extraire_HEADER(int ligne)
        {
            Info_HEADER.N1_SOUR = null;
            Info_HEADER.N2_SOUR_VERS = null;
            Info_HEADER.N2_SOUR_NAME = null;
            Info_HEADER.N2_SOUR_CORP = null;
            Info_HEADER.N3_SOUR_CORP_ADDR = null;
            List<string> N3_SOUR_CORP_PHON_liste = new List<string>();
            List<string> N3_SOUR_CORP_EMAIL_liste = new List<string>();
            List<string> N3_SOUR_CORP_FAX_liste = new List<string>();
            List<string> N3_SOUR_CORP_WWW_liste = new List<string>();
            Info_HEADER.N2_SOUR_DATA = null;
            Info_HEADER.N3_SOUR_DATA_DATE = null;
            Info_HEADER.N3_SOUR_DATA_CORP = null;
            Info_HEADER.N1_DEST = null;
            Info_HEADER.N1_DATE = null;
            Info_HEADER.N2_DATE_TIME = null;
            Info_HEADER.N1_SUBM_liste_ID = null;
            Info_HEADER.N1_SUBN = null;
            Info_HEADER.N1_FILE = null;
            Info_HEADER.N1_COPR = null;
            Info_HEADER.N2_GEDC_VERS = null;
            Info_HEADER.N2_GEDC_FORM = null;
            Info_HEADER.N3_GEDC_FORM_VERS = null;
            Info_HEADER.N1_CHAR = null;
            Info_HEADER.N2_CHAR_VERS = null;
            Info_HEADER.N1_LANG = null;
            Info_HEADER.N1_PLAC = null;
            Info_HEADER.N2_PLAC_FORM = null;
            Info_HEADER.N1_NOTE = null;
            Info_HEADER.N1__GUID = null;
            List<string> N1_SUBM_liste_ID = new List<string>();
            ligne++;
            Avoir_code_erreur();
            try
            {
                Avoir_code_erreur();
                while (Extraire_niveau(ligne) > 0)
                {
                    Avoir_code_erreur();
                    string balise_1 = Extraire_balise(dataGEDCOM[ligne]);
                    if (balise_1 == "1 DEST")                                     // 1 DEST
                    {
                        Avoir_code_erreur();
                        Info_HEADER.N1_DEST = Extraire_ligne(dataGEDCOM[ligne], 4);
                        ligne++;
                    }
                    else if (balise_1 == "1 SOUR")                                // 1 SOUR
                    {
                        Avoir_code_erreur();
                        string balise_1_texte = Extraire_ligne(dataGEDCOM[ligne], 4);
                        Info_HEADER.N1_SOUR = balise_1_texte;
                        ligne++;
                        while (Extraire_niveau(ligne) > 1)
                        {
                            Avoir_code_erreur();
                            string balise_2 = Extraire_balise(dataGEDCOM[ligne]);
                            if (balise_2 == "2 VERS")                             // 2 VERS
                            {
                                Avoir_code_erreur();
                                Info_HEADER.N2_SOUR_VERS = Extraire_ligne(dataGEDCOM[ligne], 4);
                                ligne++;
                            }
                            else if (balise_2 == "2 NAME")                        // 2 NAME
                            {
                                Avoir_code_erreur();
                                Info_HEADER.N2_SOUR_NAME = Extraire_ligne(dataGEDCOM[ligne], 4);
                                ligne++;
                            }
                            else if (balise_2 == "2 CORP")                        // 2 CORP
                            {
                                Avoir_code_erreur();
                                Info_HEADER.N2_SOUR_CORP = Extraire_ligne(dataGEDCOM[ligne], 4);
                                ligne++;
                                while (Extraire_niveau(ligne) > 2)
                                {
                                    string balise_3 = Extraire_balise(dataGEDCOM[ligne]);
                                    Avoir_code_erreur();
                                    if (balise_3 == "3 ADDR")                     // 3 ADDR
                                    {
                                        Avoir_code_erreur();
                                        (Info_HEADER.N3_SOUR_CORP_ADDR, ligne) = Extraire_ADDRESS_STRUCTURE(ligne);
                                    }
                                    else if (balise_3 == "3 PHON")                // 2 PHON
                                    {
                                        Avoir_code_erreur();
                                        N3_SOUR_CORP_PHON_liste.Add(Extraire_ligne(dataGEDCOM[ligne], 4));
                                        ligne++;
                                    }
                                    else if (balise_3 == "3 FAX")                 // 3 FAX
                                    {
                                        Avoir_code_erreur();
                                        N3_SOUR_CORP_FAX_liste.Add(Extraire_ligne(dataGEDCOM[ligne], 3));
                                        ligne++;
                                    }
                                    else if (balise_3 == "3 EMAIL")               // 3 EMAIL
                                    {
                                        Avoir_code_erreur();
                                        N3_SOUR_CORP_EMAIL_liste.Add(Extraire_ligne(dataGEDCOM[ligne], 5));
                                        ligne++;
                                    }
                                    else if (balise_3 == "3 _EMAIL")              // 3 _EMAIL
                                    {
                                        Avoir_code_erreur();
                                        N3_SOUR_CORP_EMAIL_liste.Add(Extraire_ligne(dataGEDCOM[ligne], 6));
                                        ligne++;
                                    }
                                    else if (balise_3 == "3 WWW")                 // 3 WWW
                                    {
                                        Avoir_code_erreur();
                                        N3_SOUR_CORP_WWW_liste.Add(Extraire_ligne(dataGEDCOM[ligne], 3));
                                        ligne++;
                                    }
                                    else if (balise_3 == "3 _WWW")                // 3 _WWW
                                    {
                                        Avoir_code_erreur();
                                        N3_SOUR_CORP_WWW_liste.Add(Extraire_ligne(dataGEDCOM[ligne], 4));
                                        ligne++;
                                    }
                                    else
                                    {
                                        Avoir_code_erreur();
                                        EcrireBalise(ligne);
                                        ligne++;
                                    }
                                }
                            }
                            else if (balise_2 == "2 DATA")                        // 2 DATA
                            {
                                Avoir_code_erreur();
                                Info_HEADER.N2_SOUR_DATA = Extraire_ligne(dataGEDCOM[ligne], 4);
                                ligne++;
                                while (Extraire_niveau(ligne) > 2)
                                {
                                    Avoir_code_erreur();
                                    string balise_3 = Extraire_balise(dataGEDCOM[ligne]);
                                    if (balise_3 == "3 DATE")                     // 3 DATA DATE
                                    {
                                        Avoir_code_erreur();
                                        Info_HEADER.N3_SOUR_DATA_DATE = ConvertirDateGEDCOM(dataGEDCOM[ligne].Substring(7));
                                        ligne++;
                                    }
                                    else if (balise_3 == "3 COPR")                // 3 DATA COPR
                                    {
                                        Avoir_code_erreur();
                                        Info_HEADER.N3_SOUR_DATA_CORP = Extraire_ligne(dataGEDCOM[ligne], 4);
                                        ligne++;
                                        while (Extraire_niveau(ligne) > 3)
                                        {
                                            Avoir_code_erreur();
                                            string balise_4 = Extraire_balise(dataGEDCOM[ligne]);
                                            if (balise_4 == "4 CONT")             // 4 CONT
                                            {
                                                Avoir_code_erreur();
                                                if (dataGEDCOM[ligne].Length > 7)
                                                {
                                                    Info_HEADER.N3_SOUR_DATA_CORP += "<br />" + Extraire_ligne(dataGEDCOM[ligne], 4);
                                                }
                                                else Info_HEADER.N3_SOUR_DATA_CORP += "<br />\n";
                                                ligne++;
                                            }
                                            else if (balise_4 == "4 CONC")        // 4 CONC
                                            {
                                                Avoir_code_erreur();
                                                Info_HEADER.N3_SOUR_DATA_CORP += " " + Extraire_ligne(dataGEDCOM[ligne], 4);
                                                ligne++;
                                            }
                                            else
                                            {
                                                Avoir_code_erreur();
                                                EcrireBalise(ligne);
                                                ligne++;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        Avoir_code_erreur();
                                        EcrireBalise(ligne);
                                        ligne++;
                                    }
                                }
                            }
                            else
                            {
                                Avoir_code_erreur();
                                EcrireBalise(ligne);
                                ligne++;
                            }
                        }
                    }
                    else if (balise_1 == "1 DATE")                                // 1 DATE
                    {
                        Avoir_code_erreur();
                        Info_HEADER.N1_DATE = ConvertirDateGEDCOM(dataGEDCOM[ligne].Substring(7));
                        ligne++;
                        while (Extraire_niveau(ligne) > 1)
                        {
                            Avoir_code_erreur();
                            if (Extraire_balise(dataGEDCOM[ligne]) == "2 TIME")                             // 2 TIME
                            {
                                Avoir_code_erreur();
                                if (dataGEDCOM[ligne].Length > 7)
                                {
                                    Avoir_code_erreur();
                                    Info_HEADER.N2_DATE_TIME = dataGEDCOM[ligne].Substring(7);
                                }
                                ligne++;
                            }
                            else
                            {
                                Avoir_code_erreur();
                                EcrireBalise(ligne);
                                ligne++;
                            }
                        }
                    }
                    else if (balise_1 == "1 SUBM")                                // 1 SUBM
                    {
                        Avoir_code_erreur();
                        N1_SUBM_liste_ID.Add(Extraire_ID(dataGEDCOM[ligne]));
                        ligne++;
                    }
                    else if (balise_1 == "1 FILE")                                // 1 FILE
                    {
                        Avoir_code_erreur();
                        if (dataGEDCOM[ligne].Length > 7)
                        {
                            Avoir_code_erreur();
                            Info_HEADER.N1_FILE = dataGEDCOM[ligne].Substring(7, dataGEDCOM[ligne].Length - 7);
                            ligne++;
                        }
                        else
                        {
                            Avoir_code_erreur();
                            EcrireBalise(ligne);
                            ligne++;
                        }
                    }
                    else if (balise_1 == "1 SUBN")                                // 1 SUBN
                    {
                        Avoir_code_erreur();
                        Info_HEADER.N1_SUBN = Extraire_ligne(dataGEDCOM[ligne], 4);
                        ligne++;
                    }
                    else if (balise_1 == "1 _HME")                                // 1 _HME
                    {
                        Avoir_code_erreur();
                        ligne++;
                        while (Extraire_niveau(ligne) > 1)
                        {
                            Avoir_code_erreur();
                        }
                    }
                    else if (balise_1 == "1 GEDC")                                // 1 GEDC
                    {
                        Avoir_code_erreur();
                        ligne++;
                        while (Extraire_niveau(ligne) > 1)
                        {
                            Avoir_code_erreur();
                            if (Extraire_balise(dataGEDCOM[ligne]) == "2 VERS")                             // 2 GEDC VERS
                            {
                                Avoir_code_erreur();
                                Info_HEADER.N2_GEDC_VERS = Extraire_ligne(dataGEDCOM[ligne], 4);
                                ligne++;
                            }
                            else if (Extraire_balise(dataGEDCOM[ligne]) == "2 FORM")                         // 2 GEDC FORM
                            {
                                Avoir_code_erreur();
                                Info_HEADER.N2_GEDC_FORM = Extraire_ligne(dataGEDCOM[ligne], 4);
                                ligne++;
                                while (Extraire_niveau(ligne) > 2)
                                {
                                    Avoir_code_erreur();
                                    if (Extraire_balise(dataGEDCOM[ligne]) == "3 VERS")                      // 3 GEDC FORM VERS
                                    {
                                        Avoir_code_erreur();
                                        Info_HEADER.N3_GEDC_FORM_VERS = Extraire_ligne(dataGEDCOM[ligne], 4);
                                        ligne++;
                                    }
                                    else
                                    {
                                        Avoir_code_erreur();
                                        EcrireBalise(ligne);
                                        ligne++;
                                    }
                                }
                            }
                            else
                            {
                                Avoir_code_erreur();
                                EcrireBalise(ligne);
                                ligne++;
                            }
                        }
                    }
                    else if (balise_1 == "1 CHAR")                                // 1 CHAR
                    {
                        Avoir_code_erreur();
                        Info_HEADER.N1_CHAR = Extraire_ligne(dataGEDCOM[ligne], 4);;
                        ligne++;
                        while (Extraire_niveau(ligne) > 1)
                        {
                            Avoir_code_erreur();
                            if (Extraire_balise(dataGEDCOM[ligne]) == "2 VERS")                             // 2 VERS
                            {
                                Avoir_code_erreur();
                                Info_HEADER.N2_CHAR_VERS = Extraire_ligne(dataGEDCOM[ligne], 4);
                                ligne++;
                            }
                            else
                            {
                                Avoir_code_erreur();
                                EcrireBalise(ligne);
                                ligne++;
                            }
                        }
                    }
                    else if (balise_1 == "1 PLAC")                                // 1 PLAC
                    {
                        Avoir_code_erreur();
                        Info_HEADER.N1_PLAC = Extraire_ligne(dataGEDCOM[ligne], 4);
                        ligne++;
                        while (Extraire_niveau(ligne) > 1)
                        {
                            Avoir_code_erreur();
                            if (Extraire_balise(dataGEDCOM[ligne]) == "2 FORM")                             // 2 PLAC FORM
                            {
                                Avoir_code_erreur();
                                Info_HEADER.N2_PLAC_FORM = dataGEDCOM[ligne].Substring(7,
                                    dataGEDCOM[ligne].Length - 7);
                                ligne++;
                            }
                            else
                            {
                                Avoir_code_erreur();
                                EcrireBalise(ligne);
                                ligne++;
                            }

                        }
                    }
                    else if (balise_1 == "1 LANG")                                // 1 LANG
                    {
                        Avoir_code_erreur();
                        if (dataGEDCOM[ligne].Length > 7)
                        {
                            Avoir_code_erreur();
                            Info_HEADER.N1_LANG = dataGEDCOM[ligne].Substring(7, dataGEDCOM[ligne].Length - 7);
                            if (Info_HEADER.N1_LANG.ToLower() == "english") Info_HEADER.N1_LANG = "Anglais";
                            if (Info_HEADER.N1_LANG.ToLower() == "french") Info_HEADER.N1_LANG = "Français";
                        }
                        ligne++;
                    }
                    else if (balise_1 == "1 COPR")                                                      // 1 COPR
                    {
                        Avoir_code_erreur();
                        Info_HEADER.N1_COPR = Extraire_ligne(dataGEDCOM[ligne], 4);
                        ligne++;
                    }
                    else if (balise_1 == "1 NOTE")                                // 1 NOTE
                    {
                        Avoir_code_erreur();
                        Info_HEADER.N1_NOTE = Extraire_texte_ligne1(dataGEDCOM[ligne]);
                        ligne++;
                        while (Extraire_niveau(ligne) > 1)
                        {
                            if (Extraire_balise(dataGEDCOM[ligne]) == "1 CONT")                            // +1 CONT
                            {
                                Info_HEADER.N1_NOTE += "<br />" + Extraire_ligne(dataGEDCOM[ligne], 4);
                                ligne++;
                            }
                            else if (Extraire_balise(dataGEDCOM[ligne]) == "1 CONC")                        // +1 CONC
                            {
                                Info_HEADER.N1_NOTE += " " + Extraire_ligne(dataGEDCOM[ligne], 4);
                                ligne++;
                            }
                            else
                            {
                                EcrireBalise(ligne);
                                ligne++;
                            }
                        }
                    }
                    else if (balise_1 == "1 _GUID")                                                    // 1 _GUID
                    {
                        Avoir_code_erreur();
                        Info_HEADER.N1__GUID = Extraire_ligne(dataGEDCOM[ligne], 5);
                        ligne++;
                    }
                    else
                    {
                        Avoir_code_erreur();
                        EcrireBalise(ligne);
                        ligne++;
                    }
                }
            }
            catch (Exception msg)
            {
                string message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                Voir_message(message, msg.Message, erreur);
            }
            if (!GH.GH.annuler)
            {
                Info_HEADER.N3_SOUR_CORP_PHON_liste = N3_SOUR_CORP_PHON_liste;
                Info_HEADER.N3_SOUR_CORP_FAX_liste = N3_SOUR_CORP_FAX_liste;
                Info_HEADER.N3_SOUR_CORP_EMAIL_liste = N3_SOUR_CORP_EMAIL_liste;
                Info_HEADER.N3_SOUR_CORP_WWW_liste = N3_SOUR_CORP_WWW_liste;
                Info_HEADER.N1_SUBM_liste_ID = N1_SUBM_liste_ID;
            }
        }
        public static void Erreur_log(
            string message = "")
            //[CallerFilePath] string code = "",
            //[CallerLineNumber] int ligneCode = 0,
            //[CallerMemberName] string fonction = null)
        {
            if (!GH.Properties.Settings.Default.deboguer || GH.Properties.Settings.Default.DossierHTML == "") return;
            System.Windows.Forms.Button Btn_erreur = Application.OpenForms["GH"].Controls["Btn_erreur"] as System.Windows.Forms.Button;
            //code = Path.GetFileName(code);
            //code = code[0].ToString().ToUpper();
            string fichier = GH.Properties.Settings.Default.DossierHTML + "\\erreur.txt";
            try
            {
                using (StreamWriter ligne = File.AppendText(fichier))
                {
                    if (!LogErreur)
                    {
                        Btn_erreur.Visible = true;
                        ligne.WriteLine("*** Erreur ************************************************************");
                        ligne.WriteLine("");
                        ligne.WriteLine("Liste des erreurs dans le code GH");
                        ligne.WriteLine("");
                        ligne.WriteLine("SVP contactez le développeur en copiant l'adresse suivante dans votre. ");
                        ligne.WriteLine("navigateur https://pambrun.net/communication/form.php?PAGE=GH.exe");
                        ligne.WriteLine("L'information aidera a corrigé le problème.");
                        ligne.WriteLine("**********************************************************************");
                        ligne.WriteLine("Version de GH " + Application.ProductVersion);
                        ligne.WriteLine("Installer dans le dossier " + Application.ExecutablePath);
                        ligne.WriteLine("**********************************************************************");
                        ligne.WriteLine("Information sur le fichier GEDCOM");
                        ligne.WriteLine("Nom: " + Info_HEADER.N2_SOUR_NAME);
                        ligne.WriteLine("Version: " + Info_HEADER.N2_SOUR_VERS);
                        ligne.WriteLine("Date: " + Info_HEADER.N1_DATE + " " + Info_HEADER.N2_DATE_TIME);
                        ligne.WriteLine("Copyright: " + Info_HEADER.N1_COPR);
                        ligne.WriteLine("Version: " + Info_HEADER.N2_GEDC_VERS);
                        ligne.WriteLine("Code charactère: " + Info_HEADER.N1_CHAR);
                        ligne.WriteLine("Langue: " + Info_HEADER.N1_LANG);
                        ligne.WriteLine("Fichier sur le disque: " + Info_HEADER.Nom_fichier_disque);
                        ligne.WriteLine("**********************************************************************");
                        LogErreur = true;
                    }
                    ligne.WriteLine(/*code + " " + ligneCode + ">" + fonction + "-> " + "►" + */message /*+ "◄"*/);
                }
            }
            catch (Exception msg)
            {
                {
                    MessageBox.Show("Ne peut pas écrire l'information sur le disque." + msg.Message, "Problème",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                }
            }
        }
            private static (ADDRESS_STRUCTURE, int) Extraire_ADDRESS_STRUCTURE(int ligne)
        {
            ADDRESS_STRUCTURE info = new ADDRESS_STRUCTURE
            {
                N0_ADDR = null,
                N1_ADR1 = null,
                N1_ADR2 = null,
                N1_ADR3 = null,
                N1_CITY = null,
                N1_STAE = null,
                N1_POST = null,
                N1_CTRY = null,
                N1_NOTE_liste_ID = null
            };
            List<string> N1_NOTE_liste_ID = new List<string>();
            int niveau_I = Extraire_niveau(ligne);
            info.N0_ADDR = Extraire_ligne(dataGEDCOM[ligne], 4);
            ligne++;
            while (Extraire_niveau(ligne) > niveau_I)
            {
                if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " CONT")          // +1 CONT;
                {
                    info.N0_ADDR += "<br />" + Extraire_ligne(dataGEDCOM[ligne], 4);
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " ADR1")     // +1 ADR1
                {
                    if (dataGEDCOM[ligne].Length > 7)
                    {
                        info.N1_ADR1 = Extraire_ligne(dataGEDCOM[ligne], 4);
                    }
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " ADR2")     // +1 ADR2
                {
                    if (dataGEDCOM[ligne].Length > 7)
                    {
                        info.N1_ADR2 = Extraire_ligne(dataGEDCOM[ligne], 4);
                    }
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " ADR3")     // +1 ADR3
                {
                    if (dataGEDCOM[ligne].Length > 7)
                    {
                        info.N1_ADR3 = Extraire_ligne(dataGEDCOM[ligne], 4);
                    }
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " CITY")     // +1 CITY
                {
                    if (dataGEDCOM[ligne].Length > 7)
                    {
                        info.N1_CITY = Extraire_ligne(dataGEDCOM[ligne], 4);
                    }
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " STAE")     // +1 STAE
                {
                    if (dataGEDCOM[ligne].Length > 7)
                    {
                        info.N1_STAE = Extraire_ligne(dataGEDCOM[ligne], 4);
                    }
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " CTRY")     // +1 CTRY
                {
                    if (dataGEDCOM[ligne].Length > 7)
                    {
                        info.N1_CTRY = Extraire_ligne(dataGEDCOM[ligne], 4);
                    }
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " POST")     // +1 POST
                {
                    if (dataGEDCOM[ligne].Length > 7)
                    {
                        info.N1_POST = Extraire_ligne(dataGEDCOM[ligne], 4); ;
                    }
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " NOTE")     // +1 NOTE pour GRAMPS
                {
                    string NoteID;
                    (NoteID, ligne) = Extraire_NOTE_RECORD(ligne);
                    N1_NOTE_liste_ID.Add(NoteID);
                }
                else
                {
                    EcrireBalise(ligne);
                    ligne++;
                }
            }
            info.N1_NOTE_liste_ID = N1_NOTE_liste_ID;
            return (info, ligne);
        }
        private static (ASSOCIATION_STRUCTURE, int) Extraire_ASSOCIATION_STRUCTURE(int ligne)
        {
            ASSOCIATION_STRUCTURE info = new ASSOCIATION_STRUCTURE();
            string N0_ASSO;
            string N1_TYPE = null;
            string N1_RELA = null;
            List<string> N1_SOUR_citation_liste_ID = new List<string>();
            List<string> N1_SOUR_source_liste_ID = new List<string>();
            List<string> N1_NOTE_liste_ID = new List<string>();
            int niveau_I = Extraire_niveau(ligne);
            N0_ASSO = Extraire_ID(dataGEDCOM[ligne]);                                                   // +0 ASSO
            ligne++;
            while (Extraire_niveau(ligne) > niveau_I)
            {
                string baliseN1 = Extraire_balise(dataGEDCOM[ligne]);
                if (baliseN1 == (niveau_I + 1).ToString() + " RELA")          // +1 RELA
                {
                    N1_RELA = Extraire_ligne(dataGEDCOM[ligne], 4);
                    ligne++;
                }
                else if (baliseN1 == (niveau_I + 1).ToString() + " TYPE")
                {
                    N1_TYPE = Extraire_ligne(dataGEDCOM[ligne], 4);
                    switch (N1_TYPE.ToUpper())
                    {
                        case "FAM": N1_TYPE = "Famille"; break;
                        case "INDI": N1_TYPE = "Individu"; break;
                    }
                    ligne++;
                }
                else if (baliseN1 == (niveau_I + 1).ToString() + " SOUR")     // +1 SOUR
                {
                    string citation;
                    string source;
                    (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                    if (citation != null) N1_SOUR_citation_liste_ID.Add(citation);
                    if (source != null) N1_SOUR_source_liste_ID.Add(source);
                }
                else if (baliseN1 == (niveau_I + 1).ToString() + " NOTE")     // +1 NOTE
                {
                    string NoteID;
                    (NoteID, ligne) = Extraire_NOTE_RECORD(ligne);
                    N1_NOTE_liste_ID.Add(NoteID);
                }
                else
                {
                    EcrireBalise(ligne);
                    ligne++;
                }
            }
            info.N0_ASSO = N0_ASSO;
            info.N1_TYPE = N1_TYPE;
            info.N1_RELA = N1_RELA;
            info.N1_SOUR_citation_liste_ID = N1_SOUR_citation_liste_ID;
            info.N1_SOUR_source_liste_ID = N1_SOUR_source_liste_ID;
            info.N1_NOTE_liste_ID = N1_NOTE_liste_ID;
            return (info, ligne);
        }
        private static (CHANGE_DATE, int) Extraire_CHANGE_DATE(int ligne)
        {
            CHANGE_DATE info = new CHANGE_DATE();
            string N1_CHAN_DATE = null;
            string N2_CHAN_DATE_TIME = null;
            List<string> N1_NOTE_liste_ID = new List<string>();
            int niveau_I = Extraire_niveau(ligne);
            ligne++;
            while (Extraire_niveau(ligne) > niveau_I)
            {
                if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " DATE")          // +1 DATE
                {
                    N1_CHAN_DATE = ConvertirDateGEDCOM(dataGEDCOM[ligne].Substring(7, dataGEDCOM[ligne].Length - 7));
                    ligne++;
                    while (Extraire_niveau(ligne) > niveau_I + 1) // > +1
                    {
                        if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 2).ToString() + " TIME")  // +2 TIME
                        {
                            N2_CHAN_DATE_TIME = dataGEDCOM[ligne].Substring(7, dataGEDCOM[ligne].Length - 7);
                            ligne++;
                        }
                        else
                        {
                            EcrireBalise(ligne);
                            ligne++;
                        }
                    }
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " NOTE")     // +1 NOTE
                {
                    string IDNote;
                    (IDNote, ligne) = Extraire_NOTE_RECORD(ligne);
                    N1_NOTE_liste_ID.Add(IDNote);
                }
                else
                {
                    EcrireBalise(ligne);
                    ligne++;
                }
            }
            info.N1_CHAN_DATE = N1_CHAN_DATE;
            info.N2_CHAN_DATE_TIME = N2_CHAN_DATE_TIME;
            info.N1_CHAN_NOTE_ID_liste = N1_NOTE_liste_ID;
            return (info, ligne);
        }
        private static (CHILD_TO_FAMILY_LINK, int) Extraire_CHILD_TO_FAMILY_LINK(int ligne)
        {
            int niveau_I = Extraire_niveau(ligne);
            List<string> N1_NOTE_liste_ID = new List<string>();
            CHILD_TO_FAMILY_LINK info = new CHILD_TO_FAMILY_LINK
            {
                N0_FAMC = Extraire_ID(dataGEDCOM[ligne])
            };
            ligne++;
            while (Extraire_niveau(ligne) > niveau_I)
            {
                if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " PEDI")          // +1 PEDI
                {
                    info.N1_PEDI = Extraire_ligne(dataGEDCOM[ligne], 4);
                    if (info.N1_PEDI.ToLower() == "adopted") info.N1_PEDI = "Indique les parents adoptifs.";
                    if (info.N1_PEDI.ToLower() == "birth") info.N1_PEDI = "Indique les parents biologiques";
                    if (info.N1_PEDI.ToLower() == "foster") info.N1_PEDI = "Indique que l'enfant faisait partie d'une famille d'accueil ou d'une famille tutrice.";
                    if (info.N1_PEDI.ToLower() == "sealing") info.N1_PEDI = "indique que l'enfant a été scellé à des parents autres que les parents biologiques.";
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " STAT")     // +1 STAT
                {
                    info.N1_STAT = Extraire_ligne(dataGEDCOM[ligne], 4);
                    if (info.N1_STAT.ToLower() == "challenged") info.N1_STAT = "Lier cet enfant à cette famille est suspect, mais le lien n’a été ni prouvé ni réfuté.";
                    if (info.N1_STAT.ToLower() == "disproven") info.N1_STAT = "Certains prétendent que cet enfant appartient à cette famille, mais le lien a été réfutée.";
                    if (info.N1_STAT.ToLower() == "proven") info.N1_STAT = "Certains prétendent que cet enfant n'appartient pas à cette famille, mais le lien a été prouvé.";
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " NOTE")     // +1 NOTE
                {
                    string IDNote;
                    (IDNote, ligne) = Extraire_NOTE_RECORD(ligne);
                    N1_NOTE_liste_ID.Add(IDNote);
                }
                else
                {
                    EcrireBalise(ligne);
                    ligne++;
                }
            }
            info.N1_NOTE_liste_ID = N1_NOTE_liste_ID;
            return (info, ligne);
        }
        private static (EVENT_ATTRIBUTE_STRUCTURE, int) Extraire_EVENT_ATTRIBUTE_STRUCTURE(int ligne)
        {
            EVENT_ATTRIBUTE_STRUCTURE info = new EVENT_ATTRIBUTE_STRUCTURE
            {
                N2_TYPE = null,
                N2_DATE = "", // important pour classer par date;
                N3_DATE_TIME = null, // Herisis
                N2_PLAC = null,
                N2_ADDR = null,
                N2_AGNC = null,
                N2_RELI = null,
                N2_CAUS = null,
                N2_RESN = null,
                N1_EVEN = null,
                N1_EVEN_texte = null,
                N3_HUSB_AGE = null,
                N3_WIFE_AGE = null,
                N2_AGE = null,
                N2_FAMC = null,
                N2_FAMC_ADOP = null,
                titre = null,
                description = null,
                N2__ANCES_ORDRE = null, // Ancestrologie
                N2__ANCES_XINSEE = null,  // Ancestrologie
                N2__FNA = null, // Heredis
            };
            List<string> N2_PHON_liste = new List<string>();
            List<string> N2_EMAIL_liste = new List<string>();
            List<string> N2_FAX_liste = new List<string>();
            List<string> N2_WWW_liste = new List<string>();
            List<string> N2_NOTE_liste_ID = new List<string>();
            List<string> N2_SOUR_citation_liste_ID = new List<string>();
            List<string> N2_SOUR_source_liste_ID = new List<string>();
            List<string> N2_OBJE_liste_ID = new List<string>();

            string[] s = dataGEDCOM[ligne].Split(' ');
            info.N1_EVEN = s[1];
            info.N1_EVEN = info.N1_EVEN.ToUpper();
            info.titre = Convertir_EVENT_titre(info.N1_EVEN, Extraire_ligne(dataGEDCOM[ligne], 4));
            if (info.N1_EVEN == "ADOP")
            {
                info.N1_EVEN_texte = null;
            }
            if (dataGEDCOM[ligne].Length > 6)
            {
                if (dataGEDCOM[ligne].Length > 2 + info.N1_EVEN.Length)
                    info.N1_EVEN_texte = dataGEDCOM[ligne].Substring(3 + info.N1_EVEN.Length);
            }
            string extraEVEN = "";
            
            ligne++;
            Avoir_code_erreur();
            while (Extraire_niveau(ligne) > 1)
            {
                Avoir_code_erreur();
                if (Extraire_balise(dataGEDCOM[ligne]) == "2 CONC")                                     // 2 CONC
                {
                    Avoir_code_erreur();
                    if (dataGEDCOM[ligne].Length > 7)
                        extraEVEN += " " + Extraire_ligne(dataGEDCOM[ligne], 4);
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == "2 CONT")                                // 2 CONT
                {
                    Avoir_code_erreur();
                    if (dataGEDCOM[ligne].Length > 7)
                    {
                        extraEVEN += "<br />" + dataGEDCOM[ligne].Substring(7);
                    }
                    else extraEVEN += "<br />\n";
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == "2 TYPE")                                // 2 TYPE 
                {
                    Avoir_code_erreur();
                    info.N2_TYPE = Extraire_ligne(dataGEDCOM[ligne], 4);
                    switch(info.N2_TYPE)
                    {
                        case "unknown": info.N2_TYPE = "inconnue"; break; 
                        case "marriage": info.N2_TYPE = "marriage"; break; 
                        case "not married": info.N2_TYPE = "pas marié"; break; 
                        case "civil": info.N2_TYPE = "mariage civil"; break;
                        case "religious": info.N2_TYPE =" mariage religieux"; break;
                        case "common law": info.N2_TYPE = "mariage de fait en union libre"; break;
                        case "partnership": info.N2_TYPE = "partenariat"; break;
                        case "registered partnership": info.N2_TYPE = "partenariat enregistré"; break;
                        case "living together": info.N2_TYPE = "living together"; break;
                        case "living apart together": info.N2_TYPE = "vivre séparément ensemble"; break;
                    }
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == "2 DATE")                                // 2 DATE
                {
                    Avoir_code_erreur();
                    string d = Extraire_ligne(dataGEDCOM[ligne], 4);
                    string dd = ConvertirDateGEDCOM(d);
                    info.N2_DATE = dd;
                    ligne++;
                    while (Extraire_niveau(ligne) > 2)
                    {
                        Avoir_code_erreur();
                        if (Extraire_balise(dataGEDCOM[ligne]) == "3 TIME")                             // 3 TIME Heresis
                        {
                            Avoir_code_erreur();
                            info.N3_DATE_TIME = Extraire_ligne(dataGEDCOM[ligne], 4);
                            ligne++;
                        }
                        else
                        {
                            EcrireBalise(ligne);
                            ligne++;
                        }
                    }
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == "2 PLAC")                                // 2 PLAC
                {
                    Avoir_code_erreur();
                    (info.N2_PLAC, ligne) = Extraire_PLACE_STRUCTURE(ligne);
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == "2 ADDR")                                // 2 ADDR
                {
                    Avoir_code_erreur();
                    (info.N2_ADDR, ligne) = Extraire_ADDRESS_STRUCTURE(ligne);
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == "2 PHON")                                // 2 PHON
                {
                    Avoir_code_erreur();
                    N2_PHON_liste.Add(Extraire_ligne(dataGEDCOM[ligne], 4));
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == "2 FAX")                                 // 2 FAX
                {
                    Avoir_code_erreur();
                    N2_FAX_liste.Add(Extraire_ligne(dataGEDCOM[ligne], 3));
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == "2 EMAIL")                               // 2 EMAIL
                {
                    Avoir_code_erreur();
                    N2_EMAIL_liste.Add(Extraire_ligne(dataGEDCOM[ligne], 5));
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == "2 WWW")                                 // 2 WWW
                {
                    Avoir_code_erreur();
                    N2_WWW_liste.Add(Extraire_ligne(dataGEDCOM[ligne], 3));
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == "2 AGNC")                                // 2 AGNC 
                {
                    Avoir_code_erreur();
                    info.N2_AGNC = Extraire_ligne(dataGEDCOM[ligne], 4);
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == "2 RELI")                                // 2 RELI 
                {
                    Avoir_code_erreur();
                    info.N2_RELI = Extraire_ligne(dataGEDCOM[ligne], 4);
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == "2 CAUS")                                // 2 CAUS 
                {
                    Avoir_code_erreur();
                    info.N2_CAUS = Extraire_ligne(dataGEDCOM[ligne], 4);
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == "2 RESN")                                // 2 RESN 
                {
                    info.N2_RESN = Extraire_ligne(dataGEDCOM[ligne], 4);
                    if (info.N2_RESN.ToLower() == "confidential") info.N2_RESN = "confidentiel";
                    if (info.N2_RESN.ToLower() == "locked") info.N2_RESN = "verrouillé";
                    if (info.N2_RESN.ToLower() == "privacy") info.N2_RESN = "privé";
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == "2 NOTE")                                // 2 NOTE
                {
                    Avoir_code_erreur();
                    string IDNote;
                    (IDNote, ligne) = Extraire_NOTE_RECORD(ligne);
                    N2_NOTE_liste_ID.Add(IDNote);
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == "2 OBJE")                                // 2 OBJE
                {
                    Avoir_code_erreur();
                    string ID_OBJE;
                    (ID_OBJE, ligne) = Extraire_MULTIMEDIA_RECORD(ligne);
                    N2_OBJE_liste_ID.Add(ID_OBJE);
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == "2 SOUR")                                // 2 SOUR
                {
                    Avoir_code_erreur();
                    string citation;
                    string source;
                    (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                    if (citation != null) N2_SOUR_citation_liste_ID.Add(citation);
                    if (source != null) N2_SOUR_source_liste_ID.Add(source);
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == "2 AGE")                                 // 2 AGE
                {
                    Avoir_code_erreur();
                    info.N2_AGE = Extraire_ligne(dataGEDCOM[ligne], 3);
                    info.N2_AGE = Convert_Age_GEDCOM(info.N2_AGE);
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == "2 FAMC")                                // 2 FAMC
                {
                    Avoir_code_erreur();
                    info.N2_FAMC = Extraire_ID(dataGEDCOM[ligne]);
                    ligne++;
                    while (Extraire_niveau(ligne) > 2)
                    {
                        Avoir_code_erreur();
                        if (Extraire_balise(dataGEDCOM[ligne]) == "3 ADOP")                             // 3 ADOP
                        {
                            Avoir_code_erreur();
                            info.N2_FAMC_ADOP = Extraire_ligne(dataGEDCOM[ligne], 4);
                            ligne++;
                        }
                        else
                        {
                            Avoir_code_erreur();
                            EcrireBalise(ligne);
                            ligne++;
                        }
                    }
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == "2 HUSB")                                // 2 HUSB
                {
                    Avoir_code_erreur();
                    ligne++;
                    while (Extraire_niveau(ligne) > 2)
                    {
                        Avoir_code_erreur();
                        if (Extraire_balise(dataGEDCOM[ligne]) == "3 AGE")                              // 3  HUSB AGE
                        {
                            Avoir_code_erreur();
                            info.N3_HUSB_AGE = Extraire_ligne(dataGEDCOM[ligne], 3);
                            info.N3_HUSB_AGE = Convert_Age_GEDCOM(info.N3_HUSB_AGE);
                            ligne++;
                        }
                        else
                        {
                            EcrireBalise(ligne);
                            ligne++;
                        }
                    }
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == "2 WIFE")                                // 2 WIFE
                {
                    Avoir_code_erreur();
                    ligne++;
                    while (Extraire_niveau(ligne) > 2)
                    {
                        Avoir_code_erreur();
                        if (Extraire_balise(dataGEDCOM[ligne]) == "3 AGE")                              // 3 WIFE AGE
                        {
                            Avoir_code_erreur();
                            info.N3_WIFE_AGE = Extraire_ligne(dataGEDCOM[ligne], 3);
                            info.N3_WIFE_AGE = Convert_Age_GEDCOM(info.N3_WIFE_AGE);
                            ligne++;
                        }
                        else
                        {
                            EcrireBalise(ligne);
                            ligne++;
                        }
                    }
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == "2 _ANCES_ORDRE")                        // 2 ANCES_ORDRE
                {
                    Avoir_code_erreur();
                    info.N2__ANCES_ORDRE = Extraire_ligne(dataGEDCOM[ligne], 12);
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == "2 _ANCES_XINSEE")                       // 2 ANCES_XINSEE
                {
                    Avoir_code_erreur();
                    info.N2__ANCES_XINSEE = Extraire_ligne(dataGEDCOM[ligne], 13);
                    ligne++;
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == "2 _FNA")                                // 1 SIGN pour Heresis
                {
                    Avoir_code_erreur();
                    info.N2__FNA = Extraire_ligne(dataGEDCOM[ligne], 4);
                    if (info.N2__FNA != null)
                    {
                        Avoir_code_erreur();
                        switch (info.N2__FNA.ToUpper())
                        {
                            case "YES": info.N2__FNA = "Oui"; break;
                            case "NO": info.N2__FNA = "Non"; break;
                        }
                    }
                    ligne++;
                    while (Extraire_niveau(ligne) > 2)
                    {
                        EcrireBalise(ligne);
                        ligne++;
                    }
                }
               

                else
                {
                    EcrireBalise(ligne);
                    ligne++;
                }
            }
            Avoir_code_erreur();
            if (extraEVEN != "") info.titre += extraEVEN;
            info.N2_PHON_liste = N2_PHON_liste;
            info.N2_FAX_liste = N2_FAX_liste;
            info.N2_EMAIL_liste = N2_EMAIL_liste;
            info.N2_WWW_liste = N2_WWW_liste;
            info.N2_NOTE_liste_ID = N2_NOTE_liste_ID;
            info.N2_OBJE_liste_ID = N2_OBJE_liste_ID;
            info.N2_SOUR_citation_liste_ID = N2_SOUR_citation_liste_ID;
            info.N2_SOUR_source_liste_ID = N2_SOUR_source_liste_ID;
            return (info, ligne);
        }
        private static string Extraire_EVENT_liste( string liste)
        {
            if (liste == null) return null;
            char[] charactere = new char[] { ',' };
            string[] item_split = liste.Split(charactere);
            int nombre_item = item_split.Length;
            for (int f = 0; f < nombre_item; f++)
            {
                item_split[f] = Convertir_EVENT_titre(item_split[f].Trim());
            }
            string grouper = null;
            for (int f = 0; f < nombre_item; f++)
            {
                grouper += item_split[f] + ", ";
            }
            grouper = grouper.TrimEnd(' ', ',');
            return grouper;
        }
        private static (string, int) Extraire_FAM_RECORD(int ligne)
        {
            if (GH.GH.annuler) return (null, 0);
            string N0_ID;
            string N1_RESN = null;
            List<EVENT_ATTRIBUTE_STRUCTURE> N1_Event_liste = new List<EVENT_ATTRIBUTE_STRUCTURE>();
            List<EVENT_ATTRIBUTE_STRUCTURE> N1_ATTRIBUTE_liste = new List<EVENT_ATTRIBUTE_STRUCTURE>();
            

            string N1_HUSB = null;
            string N1_WIFE = null;
            List<string> N1_CHIL_liste_ID = new List<string>();
            string N1_NCHI = null;
            List<string> N1_SUBM_liste_ID = new List<string>();
            LDS_SPOUSE_SEALING N1_SLGS = new LDS_SPOUSE_SEALING();
            List<USER_REFERENCE_NUMBER> N1_REFN_liste = new List<USER_REFERENCE_NUMBER>();
            string N1_RIN = null;
            CHANGE_DATE N1_CHAN = new CHANGE_DATE();
            List<string> N1_NOTE_liste_ID = new List<string>();
            List<string> N1_SOUR_citation_liste_ID = new List<string>();
            List<string> N1_SOUR_source_liste_ID = new List<string>();
            List<string> N1_OBJE_liste = new List<string>();
            string N1_TYPU = null; // Ancestrologie
            string N1__UST = null; // Heridis
            N0_ID = Extraire_ID(dataGEDCOM[ligne]);
            Tb_Status.Text = "Décodage du data. Lecture des familles ID  " + N0_ID;
            Animation(true);
            ligne++;
            // extraction
            Avoir_code_erreur();
            try
            {
                Avoir_code_erreur();
                while (Extraire_niveau(ligne) > 0)
                {
                    Avoir_code_erreur();
                    Application.DoEvents();
                    string balise = Extraire_balise(dataGEDCOM[ligne]);
                    // Si événement
                    if (SiBaliseEvenementFamille(balise))
                    {

                        Avoir_code_erreur();
                        EVENT_ATTRIBUTE_STRUCTURE Event;
                        (Event, ligne) = Extraire_EVENT_ATTRIBUTE_STRUCTURE(ligne);
                        N1_Event_liste.Add(Event);
                    }
                    // Si attribut
                    else if (SiBaliseAttributeFamille(balise))                                              // attribut pour GRAMPS
                    {
                        Avoir_code_erreur();
                        EVENT_ATTRIBUTE_STRUCTURE Attribute;
                        (Attribute, ligne) = Extraire_EVENT_ATTRIBUTE_STRUCTURE(ligne);
                        N1_ATTRIBUTE_liste.Add(Attribute);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 RESN")                                     //1 RESN
                    {
                        Avoir_code_erreur();
                        N1_RESN = Extraire_ligne(dataGEDCOM[ligne], 4);
                        if (N1_RESN.ToLower() == "confidential") N1_RESN = "confidentiel";
                        if (N1_RESN.ToLower() == "locked") N1_RESN = "verrouillé";
                        if (N1_RESN.ToLower() == "privacy") N1_RESN = "privé";
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 TYPU")                                // 1 TYPU pour Ancestrologie
                    {
                        Avoir_code_erreur();
                        N1_TYPU = Extraire_ligne(dataGEDCOM[ligne], 4);
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 _UST")                                // 1 _UST Heridis
                    {
                        Avoir_code_erreur();
                        N1__UST = Extraire_ligne(dataGEDCOM[ligne], 4);
                        if (N1__UST != null)
                        {
                            switch (N1__UST.ToUpper())
                            {
                                case "MARRIED": N1__UST = "Marié"; break;
                                case "SEPARATED": N1__UST = "Séparé"; break;
                                case "EXTRACONJUGAL_RELATION": N1__UST = "Relation extraconjugale"; break;
                                case "COHABITATION": N1__UST = "Cohabitation"; break;
                            }
                        }
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 HUSB")                                // 1 HUSB
                    {
                        Avoir_code_erreur();
                        N1_HUSB = Extraire_ID(dataGEDCOM[ligne]);
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 WIFE")                                // 1 WIFE
                    {
                        Avoir_code_erreur();
                        N1_WIFE = Extraire_ID(dataGEDCOM[ligne]);
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 CHIL")                                // 1 CHIL
                    {
                        Avoir_code_erreur();
                        if (dataGEDCOM[ligne].Length > 7)
                        {
                            Avoir_code_erreur();
                            string IDEnfant = Extraire_ID(dataGEDCOM[ligne]);
                            N1_CHIL_liste_ID.Add(IDEnfant);
                        }
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 NCHI")                                // 1 NCHI
                    {
                        Avoir_code_erreur();
                        N1_NCHI = Extraire_ligne(dataGEDCOM[ligne], 4);
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 SUBM")                                // 1 SUBM
                    {
                        Avoir_code_erreur();
                        if (dataGEDCOM[ligne].Length > 7)
                        {
                            Avoir_code_erreur();
                            N1_SUBM_liste_ID.Add(Extraire_ID(dataGEDCOM[ligne]));
                            ligne++;
                        }
                        else
                        {
                            Avoir_code_erreur();
                            EcrireBalise(ligne);
                            ligne++;
                        }
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 SLGS")                                // 1 SLGS
                    {
                        Avoir_code_erreur();
                        (N1_SLGS, ligne) = Extraire_LDS_SPOUSE_SEALING(ligne);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 REFN")                                // 1 REFN
                    {
                        Avoir_code_erreur();
                        USER_REFERENCE_NUMBER N1_REFN;
                        (N1_REFN, ligne) = Extraire_USER_REFERENCE_NUMBER(ligne);
                        N1_REFN_liste.Add(N1_REFN);

                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 RIN")                                 // 1 RIN
                    {
                        Avoir_code_erreur();
                        N1_RIN = Extraire_ligne(dataGEDCOM[ligne], 3);
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 CHAN")                                // 1 CHAN
                    {
                        Avoir_code_erreur();
                        (N1_CHAN, ligne) = Extraire_CHANGE_DATE(ligne);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 NOTE")                                // 1 NOTE
                    {
                        Avoir_code_erreur();
                        string IDNote;
                        (IDNote, ligne) = Extraire_NOTE_RECORD(ligne);
                        N1_NOTE_liste_ID.Add(IDNote);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 OBJE")                                // 1 OBJE Famille
                    {
                        Avoir_code_erreur();
                        string ID_OBJE;
                        (ID_OBJE, ligne) = Extraire_MULTIMEDIA_RECORD(ligne);
                        N1_OBJE_liste.Add(ID_OBJE);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 SOUR")                                // 1 SOUR
                    {
                        Avoir_code_erreur();
                        string citation;
                        string source;
                        (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                        if (citation != null) N1_SOUR_citation_liste_ID.Add(citation);
                        if (source != null) N1_SOUR_source_liste_ID.Add(source);
                    }
                    else
                    {
                        Avoir_code_erreur();
                        EcrireBalise(ligne);
                        ligne++;
                    }
                }
            }
            catch (Exception msg)
            {

                string message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                Voir_message(message, msg.Message, erreur);
            }
            if (!GH.GH.annuler)
            {
                liste_FAM_RECORD.Add(new FAM_RECORD()
                {
                    N0_ID = N0_ID,
                    N1_EVENT_Liste = N1_Event_liste,
                    N1_ATTRIBUTE_liste = N1_ATTRIBUTE_liste, // pour GRAMPS
                    N1_RESN = N1_RESN,
                    N1_HUSB = N1_HUSB,
                    N1_WIFE = N1_WIFE,
                    N1_NCHI = N1_NCHI,
                    N1_CHIL_liste_ID = N1_CHIL_liste_ID,
                    N1_SUBM_liste_ID = N1_SUBM_liste_ID,
                    N1_SLGS = N1_SLGS,
                    N1_REFN_liste = N1_REFN_liste,
                    N1_RIN = N1_RIN,
                    N1_OBJE_liste = N1_OBJE_liste,
                    N1_NOTE_liste_ID = N1_NOTE_liste_ID,
                    N1_SOUR_citation_liste_ID = N1_SOUR_citation_liste_ID,
                    N1_SOUR_source_liste_ID = N1_SOUR_source_liste_ID,
                    N1_CHAN = N1_CHAN,
                    N1_TYPU = N1_TYPU,
                    N1__UST = N1__UST
                });
            }
            return (N0_ID, ligne);
        }
        private static void Extraire_INDIVIDUAL_RECORD(int ligne)
        {
            string N0_ID;
            string N1_RESN = null;
            List<PERSONAL_NAME_STRUCTURE> N1_NAME_liste = new List<PERSONAL_NAME_STRUCTURE>();
            string N1_SEX = null;
            List<EVENT_ATTRIBUTE_STRUCTURE> N1_Event_liste = new List<EVENT_ATTRIBUTE_STRUCTURE>();
            List<EVENT_ATTRIBUTE_STRUCTURE> N1_ATTRIBUTE_liste = new List<EVENT_ATTRIBUTE_STRUCTURE>();
            LDS_INDIVIDUAL_ORDINANCE N1_LDS;
            CHILD_TO_FAMILY_LINK N1_FAMC = new CHILD_TO_FAMILY_LINK();
            List<SPOUSE_TO_FAMILY_LINK> N1_FAMS_liste_Conjoint = new List<SPOUSE_TO_FAMILY_LINK>();
            List<string> N1_SUBM_liste_ID = new List<string>();
            List<ASSOCIATION_STRUCTURE> N1_ASSO_liste = new List<ASSOCIATION_STRUCTURE>();
            List<string> N1_ALIA_liste_ID = new List<string>();
            List<string> N1_ANCI_liste_ID = new List<string>();
            List<string> N1_DESI_liste_ID = new List<string>();
            string N1_RFN = null;
            string N1_AFN = null;
            List<USER_REFERENCE_NUMBER> N1_REFN_liste = new List<USER_REFERENCE_NUMBER>();
            string N1_RIN = null;
            CHANGE_DATE N1_CHAN = new CHANGE_DATE();
            List<string> N1_NOTE_liste_ID = new List<string>();
            List<string> N1_SOUR_citation_liste_ID = new List<string>();
            List<string> N1_SOUR_source_liste_ID = new List<string>();
            List<string> N1_OBJE_liste = new List<string>();
            string adopter = null;
            string nom_section_1 = null;
            string nom_section_2 = null;
            string nom_section_3 = null;
            string titre = null;
            string photoID = null;
            string N1__ANCES_CLE_FIXE = null; // Ancestrologie
            string N1_FILA = null; // Ancestrologie
            string N1_SIGN = null; // Heridis
            string N1__FIL = null; // heridis
            string N1__CLS = null; // Heridis
            List<string> N1_WWW_liste = new List<string>(); // GRAMPS
            List<LDS_INDIVIDUAL_ORDINANCE> N1_LDS_liste = new List<LDS_INDIVIDUAL_ORDINANCE>();
            int cn = 0;
            if (dataGEDCOM[ligne].Length == 5)
            {
                dataGEDCOM[ligne] = dataGEDCOM[ligne];
            }
            N0_ID = Extraire_ID(dataGEDCOM[ligne]);
            Tb_Status.Text = "Décodage du data. Lecture des individus ID  " + N0_ID;
            Animation(true);
            ligne++;
            Avoir_code_erreur();
            try
            {
                while (Extraire_niveau(ligne) > 0 && !GH.GH.annuler)
                {
                    Avoir_code_erreur();
                    Application.DoEvents();
                    string baliseN1 = Extraire_balise(dataGEDCOM[ligne].ToUpper());
                    if (baliseN1 == "1 RESN")                                                               //1 RESN
                    {
                        Avoir_code_erreur();
                        N1_RESN = Extraire_ligne(dataGEDCOM[ligne], 4);
                        if (N1_RESN.ToLower() == "confidential") N1_RESN = "confidentiel";
                        if (N1_RESN.ToLower() == "locked") N1_RESN = "verrouillé";
                        if (N1_RESN.ToLower() == "privacy") N1_RESN = "privé";
                        ligne++;
                    }
                    // 1 NAME **************************************************************************************************
                    else if (baliseN1 == "1 NAME")                                                      // 1 NAME
                    {
                        Avoir_code_erreur();
                        PERSONAL_NAME_PIECES N1_PERSONAL_NAME_PIECES = new PERSONAL_NAME_PIECES
                        {
                            Nn_NPFX = null,
                            Nn_GIVN = null,
                            Nn_NICK = null,
                            Nn_SPFX = null,
                            Nn_SURN = null,
                            Nn_NSFX = null,
                            Nn_SOUR_citation_liste_ID = new List<string>(),
                            Nn_SOUR_source_liste_ID = new List<string>(),
                            Nn_NOTE_liste_ID = new List<string>()
                        };
                        PERSONAL_NAME_PIECES N1_FONE_name_pieces = new PERSONAL_NAME_PIECES
                        {
                            Nn_NPFX = null,
                            Nn_GIVN = null,
                            Nn_NICK = null,
                            Nn_SPFX = null,
                            Nn_SURN = null,
                            Nn_NSFX = null,
                            Nn_SOUR_citation_liste_ID = new List<string>(),
                            Nn_SOUR_source_liste_ID = new List<string>(),
                            Nn_NOTE_liste_ID = new List<string>()
                        };
                        PERSONAL_NAME_PIECES N1_ROMN_name_pieces = new PERSONAL_NAME_PIECES
                        {
                            Nn_NPFX = null,
                            Nn_GIVN = null,
                            Nn_NICK = null,
                            Nn_SPFX = null,
                            Nn_SURN = null,
                            Nn_NSFX = null,
                            Nn_SOUR_citation_liste_ID = new List<string>(),
                            Nn_SOUR_source_liste_ID = new List<string>(),
                            Nn_NOTE_liste_ID = new List<string>()
                        };
                        PERSONAL_NAME_STRUCTURE itemNom = new PERSONAL_NAME_STRUCTURE()
                        {
                            N0_NAME = null,
                            N1_TYPE = "Nom",
                            N1_FONE = null,
                            N2_FONE_TYPE = null,
                            N1_ROMN = null,
                            N2_ROMN_TYPE = null,
                            N1_ALIA_liste = null // BROSKEEP
                        };

                        List<string> alia_liste = new List<string>();
                        itemNom.N0_NAME = Extraire_ligne(dataGEDCOM[ligne], 4);
                        itemNom.N0_NAME = Extraire_NAME(itemNom.N0_NAME);
                        cn++;
                        ligne++;
                        Avoir_code_erreur();
                        while (Extraire_niveau(ligne) > 1)
                        {
                            string balise = Extraire_balise(dataGEDCOM[ligne]);
                            if (balise == "2 TYPE")                                                     // 2 Type
                            {
                                Avoir_code_erreur();
                                itemNom.N1_TYPE = Extraire_ligne(dataGEDCOM[ligne], 4);
                                if (itemNom.N1_TYPE.ToLower() == "aka") itemNom.N1_TYPE = "Alias";
                                if (itemNom.N1_TYPE.ToLower() == "birth") itemNom.N1_TYPE = "A la naissance";
                                if (itemNom.N1_TYPE.ToLower() == "immigrant") itemNom.N1_TYPE = "À immigrant";
                                if (itemNom.N1_TYPE.ToLower() == "maiden") itemNom.N1_TYPE = "Avant le premier mariage";
                                if (itemNom.N1_TYPE.ToLower() == "married") itemNom.N1_TYPE = "Au mariage";
                                if (itemNom.N1_TYPE.ToLower() == "user_defined") itemNom.N1_TYPE = "Par l'utilisateur";
                                ligne++;
                            }
                            else if (balise == "2 NPFX")                                                // 2 NPFX
                            {
                                Avoir_code_erreur();
                                N1_PERSONAL_NAME_PIECES.Nn_NPFX = Extraire_ligne(dataGEDCOM[ligne], 4);
                                ligne++;
                            }
                            else if (balise == "2 GIVN")                                                // 2 GIVN
                            {
                                Avoir_code_erreur();
                                N1_PERSONAL_NAME_PIECES.Nn_GIVN = Extraire_ligne(dataGEDCOM[ligne], 4);
                                ligne++;
                            }
                            else if (balise == "2 NICK")                                                // 2 NICK
                            {
                                Avoir_code_erreur();
                                N1_PERSONAL_NAME_PIECES.Nn_NICK = Extraire_ligne(dataGEDCOM[ligne], 4);
                                ligne++;
                            }
                            else if (balise == "2 SPFX")                                                // 2 SPFX
                            {
                                Avoir_code_erreur();
                                N1_PERSONAL_NAME_PIECES.Nn_SPFX = Extraire_ligne(dataGEDCOM[ligne], 4);
                                ligne++;
                            }
                            else if (balise == "2 SURN")                                                // 2 SURN
                            {
                                Avoir_code_erreur();
                                N1_PERSONAL_NAME_PIECES.Nn_SURN = Extraire_ligne(dataGEDCOM[ligne], 4);
                                ligne++;
                            }
                            else if (balise == "2 NSFX")                                                // 2 NSFX
                            {
                                Avoir_code_erreur();
                                N1_PERSONAL_NAME_PIECES.Nn_NSFX = Extraire_ligne(dataGEDCOM[ligne], 4);
                                ligne++;
                            }
                            else if (balise == "2 NOTE")                                                // 2 NOTE
                            {
                                Avoir_code_erreur();
                                string IDNote;
                                (IDNote, ligne) = Extraire_NOTE_RECORD(ligne);
                                N1_PERSONAL_NAME_PIECES.Nn_NOTE_liste_ID.Add(IDNote);
                            }
                            else if (balise == "2 SOUR")                                                // 2 SOUR
                            {
                                Avoir_code_erreur();
                                string citation;
                                string source;
                                (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                                if (citation != null) N1_PERSONAL_NAME_PIECES.Nn_SOUR_citation_liste_ID.Add(citation);
                                if (source != null) N1_PERSONAL_NAME_PIECES.Nn_SOUR_source_liste_ID.Add(source);
                            }
                            else if (balise == "2 FONE")                                                // 2 FONE
                            {
                                Avoir_code_erreur();
                                itemNom.N1_FONE = Extraire_ligne(dataGEDCOM[ligne], 4);
                                itemNom.N1_FONE = Extraire_NAME(itemNom.N1_FONE);
                                ligne++;
                                balise = Extraire_balise(dataGEDCOM[ligne]);
                                while (Extraire_niveau(ligne) > 2)
                                {
                                    Avoir_code_erreur();
                                    if (balise == "3 TYPE")                                             // FONE 3 TYPE
                                    {
                                        Avoir_code_erreur();
                                        itemNom.N2_FONE_TYPE = Extraire_ligne(dataGEDCOM[ligne], 4);
                                        ligne++;
                                        balise = Extraire_balise(dataGEDCOM[ligne]);
                                    }
                                    else if (balise == "3 NPFX")                                        // FONE NPFX
                                    {
                                        Avoir_code_erreur();
                                        N1_FONE_name_pieces.Nn_NPFX = Extraire_ligne(dataGEDCOM[ligne], 4);
                                        ligne++;
                                        balise = Extraire_balise(dataGEDCOM[ligne]);
                                    }
                                    else if (balise == "3 GIVN")                                        // FONE GIVN
                                    {
                                        Avoir_code_erreur();
                                        N1_FONE_name_pieces.Nn_GIVN = Extraire_ligne(dataGEDCOM[ligne], 4);
                                        ligne++;
                                        balise = Extraire_balise(dataGEDCOM[ligne]);
                                    }
                                    else if (balise == "3 NICK")                                        // FONE NICK
                                    {
                                        Avoir_code_erreur();
                                        N1_FONE_name_pieces.Nn_NICK = Extraire_ligne(dataGEDCOM[ligne], 4);
                                        ligne++;
                                        balise = Extraire_balise(dataGEDCOM[ligne]);
                                    }
                                    else if (balise == "3 SPFX")                                        // FONE SPFX
                                    {
                                        Avoir_code_erreur();
                                        N1_FONE_name_pieces.Nn_SPFX = Extraire_ligne(dataGEDCOM[ligne], 4);
                                        ligne++;
                                        balise = Extraire_balise(dataGEDCOM[ligne]);
                                    }
                                    else if (balise == "3 SURN")                                        // FONE 3 SURN
                                    {
                                        Avoir_code_erreur();
                                        N1_FONE_name_pieces.Nn_SURN = Extraire_ligne(dataGEDCOM[ligne], 4);
                                        ligne++;
                                        balise = Extraire_balise(dataGEDCOM[ligne]);
                                    }
                                    else if (balise == "3 NSFX")                                        // FONE  3 NSFX
                                    {
                                        Avoir_code_erreur();
                                        N1_FONE_name_pieces.Nn_NSFX = Extraire_ligne(dataGEDCOM[ligne], 4);
                                        ligne++;
                                        balise = Extraire_balise(dataGEDCOM[ligne]);
                                    }
                                    else if (balise == "3 NOTE")                                        // FONE 3 NOTE
                                    {
                                        Avoir_code_erreur();
                                        string IDNote;
                                        (IDNote, ligne) = Extraire_NOTE_RECORD(ligne);
                                        N1_FONE_name_pieces.Nn_NOTE_liste_ID.Add(IDNote);
                                        balise = Extraire_balise(dataGEDCOM[ligne]);
                                    }
                                    else if (balise == "3 SOUR")                                        // FONE 3 SOUR
                                    {
                                        Avoir_code_erreur();
                                        string citation;
                                        string source;
                                        (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                                        if (citation != null) N1_FONE_name_pieces.Nn_SOUR_citation_liste_ID.Add(citation);
                                        if (source != null) N1_FONE_name_pieces.Nn_SOUR_source_liste_ID.Add(source);
                                        balise = Extraire_balise(dataGEDCOM[ligne]);
                                    }
                                }
                            }
                            else if (balise == "2 ROMN")                                                // 2 ROMN
                            {
                                Avoir_code_erreur();
                                itemNom.N1_ROMN = Extraire_ligne(dataGEDCOM[ligne], 4);
                                itemNom.N1_ROMN = Extraire_NAME(itemNom.N1_ROMN);
                                ligne++;
                                balise = Extraire_balise(dataGEDCOM[ligne]);
                                while (Extraire_niveau(ligne) > 2)
                                {
                                    Avoir_code_erreur();
                                    if (balise == "3 TYPE")                                             // ROMN 3 TYPE
                                    {
                                        itemNom.N2_ROMN_TYPE = Extraire_ligne(dataGEDCOM[ligne], 4);
                                        ligne++;
                                        balise = Extraire_balise(dataGEDCOM[ligne]);
                                    }
                                    else if (balise == "3 NPFX")                                        // ROMN 3 NPFX
                                    {
                                        Avoir_code_erreur();
                                        N1_ROMN_name_pieces.Nn_NPFX = Extraire_ligne(dataGEDCOM[ligne], 4);
                                        ligne++;
                                        balise = Extraire_balise(dataGEDCOM[ligne]);
                                    }
                                    else if (balise == "3 GIVN")                                        // ROMN 3 GIVN
                                    {
                                        Avoir_code_erreur();
                                        N1_ROMN_name_pieces.Nn_GIVN = Extraire_ligne(dataGEDCOM[ligne], 4);
                                        ligne++;
                                        balise = Extraire_balise(dataGEDCOM[ligne]);
                                    }
                                    else if (balise == "3 NICK")                                        // ROMN 3 NICK
                                    {
                                        Avoir_code_erreur();
                                        N1_ROMN_name_pieces.Nn_NICK = Extraire_ligne(dataGEDCOM[ligne], 4);
                                        ligne++;
                                        balise = Extraire_balise(dataGEDCOM[ligne]);
                                    }
                                    else if (balise == "3 SPFX")                                        // ROMN SPFX
                                    {
                                        Avoir_code_erreur();
                                        N1_ROMN_name_pieces.Nn_SPFX = Extraire_ligne(dataGEDCOM[ligne], 4);
                                        ligne++;
                                        balise = Extraire_balise(dataGEDCOM[ligne]);
                                    }
                                    else if (balise == "3 SURN")                                        // ROMN SURN
                                    {
                                        Avoir_code_erreur();
                                        N1_ROMN_name_pieces.Nn_SURN = Extraire_ligne(dataGEDCOM[ligne], 4);
                                        ligne++;
                                        balise = Extraire_balise(dataGEDCOM[ligne]);
                                    }
                                    else if (balise == "3 NSFX")                                        // ROMN NSFX
                                    {
                                        Avoir_code_erreur();
                                        N1_ROMN_name_pieces.Nn_NSFX = Extraire_ligne(dataGEDCOM[ligne], 4);
                                        ligne++;
                                        balise = Extraire_balise(dataGEDCOM[ligne]);
                                    }
                                    else if (balise == "3 NOTE")                                        // ROMN 3 NOTE
                                    {
                                        Avoir_code_erreur();
                                        string IDNote;
                                        (IDNote, ligne) = Extraire_NOTE_RECORD(ligne);
                                        N1_ROMN_name_pieces.Nn_NOTE_liste_ID.Add(IDNote);
                                        balise = Extraire_balise(dataGEDCOM[ligne]);
                                    }
                                    else if (balise == "3 SOUR")                                        // ROMN SOUR
                                    {
                                        Avoir_code_erreur();
                                        string citation;
                                        string source;
                                        (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                                        if (citation != null) N1_ROMN_name_pieces.Nn_SOUR_citation_liste_ID.Add(citation);
                                        if (source != null) itemNom.N1_ROMN_name_pieces.Nn_SOUR_source_liste_ID.Add(source);
                                    }
                                }
                                itemNom.N1_PERSONAL_NAME_PIECES = N1_PERSONAL_NAME_PIECES;
                                itemNom.N1_FONE_name_pieces = N1_FONE_name_pieces;
                                itemNom.N1_ROMN_name_pieces = N1_ROMN_name_pieces;
                            }
                            else if (balise == "2 ALIA")                                                // 2 ALIA BROSKEEP
                            {
                                Avoir_code_erreur();
                                string alia = Extraire_ligne(dataGEDCOM[ligne], 4);
                                alia = Extraire_NAME(alia);
                                alia_liste.Add(alia);
                                ligne++;
                            }
                            else
                            {
                                Avoir_code_erreur();
                                EcrireBalise(ligne);
                                ligne++;
                            }
                        }
                        N1_NAME_liste.Add(itemNom);
                        itemNom.N1_ALIA_liste = alia_liste;
                    }
                    // Fin 1 NAME **********************************************************************************************
                    // Si événement
                    else if (SiBaliseEvenementIndividu(baliseN1))                                       // événement
                    {
                        Avoir_code_erreur();
                        EVENT_ATTRIBUTE_STRUCTURE Event;
                        (Event, ligne) = Extraire_EVENT_ATTRIBUTE_STRUCTURE(ligne);
                        N1_Event_liste.Add(Event);
                    }
                    // Si attribut
                    else if (SiBaliseAttributeIndividu(baliseN1))                                       // attribut
                    {
                        Avoir_code_erreur();
                        EVENT_ATTRIBUTE_STRUCTURE Attribute;
                        (Attribute, ligne) = Extraire_EVENT_ATTRIBUTE_STRUCTURE(ligne);
                        N1_ATTRIBUTE_liste.Add(Attribute);
                    }
                    // Si ordinance
                    else if (SiBaliseOrdinanceIndividu(baliseN1))                                       // ordinance
                    {
                        Avoir_code_erreur();
                        (N1_LDS, ligne) = Extraire_LDS_INDIVIDUAL_ORDINANCE(ligne);
                        N1_LDS_liste.Add(N1_LDS);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 FILA")                            // 1 FILA dans Ancestrologie
                    {
                        Avoir_code_erreur();
                        N1_FILA = Extraire_ligne(dataGEDCOM[ligne], 4);
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 SEX")                             // 1 SEX
                    {
                        Avoir_code_erreur();
                        N1_SEX = Extraire_ligne(dataGEDCOM[ligne], 3);
                        switch (N1_SEX.ToUpper())
                        {
                            case "M": N1_SEX = "Masculin"; break;
                            case "F": N1_SEX = "Féminin"; break;
                            case "X": N1_SEX = "Intersexe"; break;
                            case "U": N1_SEX = "Inconnue"; break;
                            case "N": N1_SEX = "Pas enregistré"; break;
                        }
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 SUBM")                            // 1 SUBM
                    {
                        Avoir_code_erreur();
                        if (dataGEDCOM[ligne].Length > 7)
                        {
                            N1_SUBM_liste_ID.Add(Extraire_ID(dataGEDCOM[ligne]));
                            ligne++;
                        }
                        else
                        {
                            EcrireBalise(ligne);
                            ligne++;
                        }
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 ASSO")                            // 1 ASSO
                    {
                        Avoir_code_erreur();
                        ASSOCIATION_STRUCTURE info;
                        (info, ligne) = Extraire_ASSOCIATION_STRUCTURE(ligne);
                        N1_ASSO_liste.Add(info);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 ALIA")                            // 1 ALIA
                    {
                        Avoir_code_erreur();
                        N1_ALIA_liste_ID.Add(Extraire_ID(dataGEDCOM[ligne]));
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 ANCI")                            // 1 ANCI
                    {
                        Avoir_code_erreur();
                        N1_ANCI_liste_ID.Add(Extraire_ID(dataGEDCOM[ligne]));
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 DESI")                            // 1 DESI
                    {
                        Avoir_code_erreur();
                        N1_DESI_liste_ID.Add(Extraire_ID(dataGEDCOM[ligne]));
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 RFN")                             // 1 RFN
                    {
                        Avoir_code_erreur();
                        N1_RFN = Extraire_ligne(dataGEDCOM[ligne], 3);
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 AFN")                             // 1 AFN
                    {
                        Avoir_code_erreur();
                        N1_AFN = Extraire_ligne(dataGEDCOM[ligne], 3);
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 FAMS")                            // 1 FAMS conjoint/spouse
                    {
                        Avoir_code_erreur();
                        SPOUSE_TO_FAMILY_LINK info;// = new SPOUSE_TO_FAMILY_LINK();
                        (info, ligne) = Extraire_SPOUSE_TO_FAMILY_LINK(ligne);
                        N1_FAMS_liste_Conjoint.Add(info);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 REFN")                            // 1 REFN
                    {
                        Avoir_code_erreur();
                        USER_REFERENCE_NUMBER N1_REFN;
                        (N1_REFN, ligne) = Extraire_USER_REFERENCE_NUMBER(ligne);
                        N1_REFN_liste.Add(N1_REFN);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 FAMC")                            // 1 FAMC enfant de la famille
                    {
                        Avoir_code_erreur();
                        (N1_FAMC, ligne) = Extraire_CHILD_TO_FAMILY_LINK(ligne);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 SOUR")                            // 1 SOUR
                    {
                        Avoir_code_erreur();
                        string citation;
                        string source;
                        (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                        if (citation != null) N1_SOUR_citation_liste_ID.Add(citation);
                        if (source != null) N1_SOUR_source_liste_ID.Add(source);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 OBJE")                            // 1 OBJE individu
                    {
                        Avoir_code_erreur();
                        string ID_OBJE;
                        (ID_OBJE, ligne) = Extraire_MULTIMEDIA_RECORD(ligne);
                        N1_OBJE_liste.Add(ID_OBJE);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 NOTE")                            // 1 NOTE
                    {
                        Avoir_code_erreur();
                        string IDNote;
                        (IDNote, ligne) = Extraire_NOTE_RECORD(ligne);
                        N1_NOTE_liste_ID.Add(IDNote);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 CHAN")                            // 1 CHAN
                    {
                        Avoir_code_erreur();
                        (N1_CHAN, ligne) = Extraire_CHANGE_DATE(ligne);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 RIN")
                    {
                        Avoir_code_erreur();
                        N1_RIN = Extraire_ligne(dataGEDCOM[ligne], 3);
                        ligne++;
                        while (Extraire_niveau(ligne) > 1)
                        {
                            EcrireBalise(ligne);
                            ligne++;
                        }
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 _ANCES_CLE_FIXE")                 // 1 _ANCES_CLE_FIXE
                    {
                        Avoir_code_erreur();
                        N1__ANCES_CLE_FIXE = Extraire_ligne(dataGEDCOM[ligne], 15);
                        ligne++;
                        while (Extraire_niveau(ligne) > 1)
                        {
                            EcrireBalise(ligne);
                            ligne++;
                        }
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 WWW")                             // 1 WWW pour GRAMPS
                    {
                        Avoir_code_erreur();
                        string WWW;
                        WWW = Extraire_ligne(dataGEDCOM[ligne], 3);
                        N1_WWW_liste.Add(WWW);
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 SIGN")                             // 1 SIGN pour Heresis
                    {
                        Avoir_code_erreur();
                        N1_SIGN = Extraire_ligne(dataGEDCOM[ligne], 4);
                        if (N1_SIGN != null)
                        {
                            switch (N1_SIGN.ToUpper())
                            {
                                case "YES": N1_SIGN = "Oui"; break;
                                case "NO": N1_SIGN = "Non"; break;
                            }
                        }
                        ligne++;
                        while (Extraire_niveau(ligne) > 1)
                        {
                            EcrireBalise(ligne);
                            ligne++;
                        }
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 _FIL")                             // 1 _FIL pour Heresis
                    {
                        Avoir_code_erreur();
                        N1__FIL = Extraire_ligne(dataGEDCOM[ligne], 4);
                        if (N1__FIL != null)
                        {
                            switch (N1__FIL.ToUpper())
                            {
                                case "LEGITIMATE_CHILD": N1__FIL = "Enfant légitime"; break;
                                case "ADULTEROUS_CHILD": N1__FIL = "Enfant adultérin"; break;
                                case "RECOGNIZED_CHILD": N1__FIL = "Enfant reconnu"; break;
                                case "NATURAL_CHILD": N1__FIL = "Enfant naturel"; break;
                                case "CHILD_FOUND": N1__FIL = "Enfant trouvé"; break;
                                case "ADOPTED_CHILD": N1__FIL = "Enfant adopté"; break;
                                case "STILLBORN_CHILD": N1__FIL = "Mort - Né"; break;
                                case "RELATIONSHIP_UNKNOW": N1__FIL = "Non Connue"; break;
                            }
                        }
                        ligne++;
                        while (Extraire_niveau(ligne) > 1)
                        {
                            EcrireBalise(ligne);
                            ligne++;
                        }
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 _CLS")                                // 1 CLS pour Heresis
                    {
                        N1__CLS = Extraire_ligne(dataGEDCOM[ligne], 4);
                        if (N1__CLS != null)
                        {
                            switch (N1__CLS.ToUpper())
                            {
                                case "YES": N1__CLS = "Oui"; break;
                                case "NO": N1__CLS = "Non"; break;
                            }
                        }
                        ligne++;
                        while (Extraire_niveau(ligne) > 2)
                        {
                            EcrireBalise(ligne);
                            ligne++;
                        }
                    }
                    else
                    {
                        Avoir_code_erreur();
                        EcrireBalise(ligne);
                        ligne++;
                    }

                }
            }
            catch (Exception msg)
            {
                string message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                Voir_message(message, msg.Message, erreur);
            }
            if (!GH.GH.annuler)
            {
                
                liste_INDIVIDUAL_RECORD.Add(new INDIVIDUAL_RECORD()
                {
                    N0_ID = N0_ID,
                    N1_RESN = N1_RESN,
                    N1__ANCES_CLE_FIXE = N1__ANCES_CLE_FIXE,
                    Titre = titre,
                    nom_section_1 = nom_section_1,
                    nom_section_2 = nom_section_2,
                    nom_section_3 = nom_section_3,
                    N1_NAME_liste = N1_NAME_liste,
                    N1_OBJE_liste = N1_OBJE_liste,
                    PhotoID = photoID,
                    N1_SEX = N1_SEX,
                    N1_EVENT_Liste = N1_Event_liste,
                    N1_Attribute_liste = N1_ATTRIBUTE_liste,
                    N1_LDS_liste = N1_LDS_liste,
                    N1_FILA = N1_FILA, // Ancestrologie
                    N1_SUBM_liste_ID = N1_SUBM_liste_ID,
                    N1_ASSO_liste = N1_ASSO_liste,
                    N1_ALIA_liste_ID = N1_ALIA_liste_ID,
                    N1_ANCI_liste_ID = N1_ANCI_liste_ID,
                    N1_DESI_liste_ID = N1_DESI_liste_ID,
                    N1_RFN = N1_RFN,
                    N1_AFN = N1_AFN,
                    N1_FAMS_liste_Conjoint = N1_FAMS_liste_Conjoint,
                    N1_REFN_liste = N1_REFN_liste,
                    N1_FAMC = N1_FAMC,
                    Adopter = adopter, // ADOP,
                    N1_CHAN = N1_CHAN,
                    N1_NOTE_liste_ID = N1_NOTE_liste_ID,
                    N1_SOUR_citation_liste_ID = N1_SOUR_citation_liste_ID,
                    N1_SOUR_source_liste_ID = N1_SOUR_source_liste_ID,
                    N1_RIN = N1_RIN,
                    N1_WWW_liste = N1_WWW_liste, // GRAMPS
                    N1_SIGN = N1_SIGN, // Heridis
                    N1__FIL = N1__FIL, // Heridis
                    N1__CLS = N1__CLS // Heridis
                });
            }
        }
        private static int Extraire_niveau(int ligne)
        {
            char[] espace = { ' ' };
            string[] section = dataGEDCOM[ligne].Split(espace);
            return Int32.Parse(section[0]);
        }
        private static (string, int) Extraire_NOTE_RECORD(int ligne)
        {
            string note;
            string N0_ID;
            List<USER_REFERENCE_NUMBER> N1_REFN_liste = new List<USER_REFERENCE_NUMBER>();
            string N1_RIN = null;
            List<string> N1_SOUR_citation_liste_ID = new List<string>();
            List<string> N1_SOUR_source_liste_ID = new List<string>();
            CHANGE_DATE N1_CHAN = new CHANGE_DATE();
            bool ID_Creer = false;
            N0_ID = Extraire_ID(dataGEDCOM[ligne]);
            note = Extraire_texte_ligne1(dataGEDCOM[ligne]);
            if (N0_ID == null)
            {
                N0_ID = "N-" + DateTime.Now.ToString("HHmmssffffff") + hazard.Next(999).ToString();
                ID_Creer = true;
            }
            Tb_Status.Text = "Décodage du data. Lecture des notes ID " + N0_ID;
            Animation(true);
            int niveau_I = Extraire_niveau(ligne);
            ligne++;
            while (Extraire_niveau(ligne) > niveau_I)
            {
                Avoir_code_erreur();
                try
                {
                    Avoir_code_erreur();
                    Application.DoEvents();
                    if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " CONT")              // +1 CONT
                    {
                        Avoir_code_erreur();
                        note += "<br />" + Extraire_ligne(dataGEDCOM[ligne], 4);
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " CONC")         // +1 CONC
                    {
                        Avoir_code_erreur();
                        note += " " + Extraire_ligne(dataGEDCOM[ligne], 4);
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " REFN")         // +1 REFN
                    {
                        Avoir_code_erreur();
                        USER_REFERENCE_NUMBER N1_REFN;
                        (N1_REFN, ligne) = Extraire_USER_REFERENCE_NUMBER(ligne);
                        N1_REFN_liste.Add(N1_REFN);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " RIN")          // +1 RIN
                    {
                        Avoir_code_erreur();
                        N1_RIN = Extraire_ligne(dataGEDCOM[ligne], 3);
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " SOUR")         // +1 SOUR
                    {
                        Avoir_code_erreur();
                        string citation;
                        string source;
                        (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                        if (citation != null) N1_SOUR_citation_liste_ID.Add(citation);
                        if (source != null) N1_SOUR_source_liste_ID.Add(source);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " CHAN")         // +1 CHAN
                    {
                        Avoir_code_erreur();
                        (N1_CHAN, ligne) = Extraire_CHANGE_DATE(ligne);
                    }
                    else
                    {
                        Avoir_code_erreur();
                        EcrireBalise(ligne);
                        ligne++;
                    }
                }
                catch (Exception msg)
                {
                    string message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                    Voir_message(message, msg.Message, erreur);
                    if (GH.GH.annuler) return (N0_ID, ligne);
                }
            }
            if (!GH.GH.annuler)
            {
                if (niveau_I > 0)
                {
                    if (ID_Creer)
                    {
                        listeInfoNote.Add(new NOTE_RECORD()
                        {
                            N0_ID = N0_ID,
                            N0_NOTE_Texte = note,
                            N1_RIN = N1_RIN,
                            N1_REFN_liste = N1_REFN_liste,
                            N1_SOUR_citation_liste_ID = N1_SOUR_citation_liste_ID,
                            N1_SOUR_source_liste_ID = N1_SOUR_source_liste_ID,
                            N1_CHAN = N1_CHAN
                        });
                        return (N0_ID, ligne);
                    }
                    else
                    {
                        // NOTE_RECORD exixte déjà ajouter nouvelle source
                        if (N1_SOUR_citation_liste_ID.Count > 0)
                        {
                            int index = 0;
                            foreach (NOTE_RECORD infoNote in listeInfoNote)
                            {
                                if (infoNote.N0_ID == N0_ID)
                                {
                                    foreach (string s in infoNote.N1_SOUR_citation_liste_ID)
                                    {
                                        N1_SOUR_citation_liste_ID.Add(s);
                                    }
                                    listeInfoNote.RemoveAt(index);
                                    listeInfoNote.Add(new NOTE_RECORD()
                                    {
                                        N0_ID = N0_ID,
                                        N0_NOTE_Texte = infoNote.N0_NOTE_Texte,
                                        N1_RIN = infoNote.N1_RIN,
                                        N1_REFN_liste = infoNote.N1_REFN_liste,
                                        N1_SOUR_citation_liste_ID = N1_SOUR_citation_liste_ID,
                                        N1_CHAN = N1_CHAN
                                    });
                                    return (N0_ID, ligne);
                                }
                                index++;
                            }
                        }
                        return (N0_ID, ligne);
                    }
                }
                listeInfoNote.Add(new NOTE_RECORD()
                {
                    N0_ID = N0_ID,
                    N0_NOTE_Texte = note,
                    N1_RIN = N1_RIN,
                    N1_REFN_liste = N1_REFN_liste,
                    N1_SOUR_citation_liste_ID = N1_SOUR_citation_liste_ID,
                    N1_SOUR_source_liste_ID = N1_SOUR_source_liste_ID,
                    N1_CHAN = N1_CHAN
                });
            }
            return ("", ligne);
        }
        private static (SOURCE_REPOSITORY_CITATION, int) Extraire_SOURCE_REPOSITORY_CITATION(int ligne)
        {
            SOURCE_REPOSITORY_CITATION info = new SOURCE_REPOSITORY_CITATION
            {
                N0_ID = null,
                N1_CALN = null,
                N2_CALN_MEDI = null
            };
            List<string> N1_NOTE_liste_ID = new List<string>();
            int niveau_I = Extraire_niveau(ligne);
            info.N0_ID = Extraire_ID(dataGEDCOM[ligne]);
            ligne++;
            while (Extraire_niveau(ligne) > niveau_I)
            {
                if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " NOTE")          // +1 NOTE
                {
                    string IDNote;
                    (IDNote, ligne) = Extraire_NOTE_RECORD(ligne);
                    N1_NOTE_liste_ID.Add(IDNote);
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " CALN")     // +1 CALN
                {
                    info.N1_CALN = Extraire_texte_balise(4, dataGEDCOM[ligne]);
                    ligne++;
                    while (Extraire_niveau(ligne) > niveau_I + 1)
                    {
                        if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 2).ToString() + " MEDI")  // +2 MEDI
                        {
                            info.N2_CALN_MEDI = Extraire_texte_balise(4, dataGEDCOM[ligne]);
                            info.N2_CALN_MEDI = Avoir_type_media(info.N2_CALN_MEDI);
                            ligne++;
                        }
                        else
                        {
                            EcrireBalise(ligne);
                            ligne++;
                        }
                    }
                }
                else
                {
                    EcrireBalise(ligne);
                    ligne++;
                }
            }
            info.N1_NOTE_liste_ID = N1_NOTE_liste_ID;
            return (info, ligne);
        }
        private static (SPOUSE_TO_FAMILY_LINK, int) Extraire_SPOUSE_TO_FAMILY_LINK(int ligne)
        {
            SPOUSE_TO_FAMILY_LINK info = new SPOUSE_TO_FAMILY_LINK();
            int niveau_I = Extraire_niveau(ligne);
            List<string> N1_NOTE_liste_ID = new List<string>();
            info.N0_ID = Extraire_ID(dataGEDCOM[ligne]);
            ligne++;
            while (Extraire_niveau(ligne) > niveau_I)
            {
                if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " NOTE")          // +1 NOTE
                {
                    string IDNote;
                    (IDNote, ligne) = Extraire_NOTE_RECORD(ligne);
                    N1_NOTE_liste_ID.Add(IDNote);
                }
                else
                {
                    EcrireBalise(ligne);
                    ligne++;
                }
            }
            info.N1_NOTE_liste_ID = N1_NOTE_liste_ID;
            return (info, ligne);
        }
        private static int Extraire_SUBMISSION_RECORD(int ligne)
        {

            info_SUBMISSION_RECORD.N0_ID = null;
            info_SUBMISSION_RECORD.N1_SUBM_liste_ID = new List<string>();
            info_SUBMISSION_RECORD.N1_FAMF = null;
            info_SUBMISSION_RECORD.N1_TEMP = null;
            info_SUBMISSION_RECORD.N1_ANCE = null;
            info_SUBMISSION_RECORD.N1_DESC = null;
            info_SUBMISSION_RECORD.N1_ORDI = null;
            info_SUBMISSION_RECORD.N1_RIN = null;
            info_SUBMISSION_RECORD.N1_NOTE_liste_ID = null;
            info_SUBMISSION_RECORD.N1_CHAN = new CHANGE_DATE();
            info_SUBMISSION_RECORD.N0_ID = Extraire_ID(dataGEDCOM[ligne]);
            List<string> listeNoteID = new List<string>();
            Tb_Status.Text = "Décodage du data. Lecture du soumissionaire";
            Animation(true);
            ligne++;
            
            while (Extraire_niveau(ligne) > 0)
            {
                Avoir_code_erreur();
                try
                {
                    Avoir_code_erreur();
                    if (Extraire_balise(dataGEDCOM[ligne]) == "1 SUBM")                                 // 1 SUBM
                    {
                        Avoir_code_erreur();
                        info_SUBMISSION_RECORD.N1_SUBM_liste_ID.Add(Extraire_ID(dataGEDCOM[ligne]));
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 FAMF")                            // 1 FAMF
                    {
                        Avoir_code_erreur();
                        info_SUBMISSION_RECORD.N1_FAMF = Extraire_texte_balise(4, dataGEDCOM[ligne]);
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 TEMP")                            // 1 TEMP
                    {
                        Avoir_code_erreur();
                        info_SUBMISSION_RECORD.N1_TEMP = Extraire_texte_balise(4, dataGEDCOM[ligne]);
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 ANCE")                            // 1 ANCE
                    {
                        Avoir_code_erreur();
                        info_SUBMISSION_RECORD.N1_ANCE = Extraire_texte_balise(4, dataGEDCOM[ligne]);
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 DESC")                            // 1 DESC
                    {
                        Avoir_code_erreur();
                        info_SUBMISSION_RECORD.N1_DESC = Extraire_texte_balise(4, dataGEDCOM[ligne]);
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 ORDI")                            // 1 ORDI
                    {
                        Avoir_code_erreur();
                        info_SUBMISSION_RECORD.N1_ORDI = Extraire_texte_balise(4, dataGEDCOM[ligne]);
                        if (info_SUBMISSION_RECORD.N1_ORDI.ToLower() == "yes") info_SUBMISSION_RECORD.N1_ORDI = "Oui";
                        if (info_SUBMISSION_RECORD.N1_ORDI.ToLower() == "no") info_SUBMISSION_RECORD.N1_ORDI = "Non";
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 RIN")                             // 1 RIN
                    {
                        Avoir_code_erreur();
                        info_SUBMISSION_RECORD.N1_RIN = Extraire_texte_balise(3, dataGEDCOM[ligne]);
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 NOTE")                            // 1 NOTE
                    {
                        Avoir_code_erreur();
                        string IDNote;
                        (IDNote, ligne) = Extraire_NOTE_RECORD(ligne);
                        listeNoteID.Add(IDNote);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 CHAN")                            // 1 CHAN
                    {
                        Avoir_code_erreur();
                        (info_SUBMISSION_RECORD.N1_CHAN, ligne) = Extraire_CHANGE_DATE(ligne);
                    }
                    else
                    {
                        Avoir_code_erreur();
                        EcrireBalise(ligne);
                        ligne++;
                    }
                }
                catch (Exception msg)
                {
                    string message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                    Voir_message(message, msg.Message, erreur);
                }
            }
            info_SUBMISSION_RECORD.N1_NOTE_liste_ID = listeNoteID;
            return (ligne);
        }
        private static int Extraire_SUBMITTER_RECORD(int ligne)
        {
            string N0_ID;
            string N1_NAME = null;
            ADDRESS_STRUCTURE N1_ADDR = new ADDRESS_STRUCTURE();
            
            List<string> N1_PHON_liste = new List<string>();
            List<string> N1_FAX_liste = new List<string>();
            List<string> N1_EMAIL_liste = new List<string>();
            List<string> N1_WWW_liste = new List<string>();
            List<string> N1_OBJE_ID_liste = new List<string>();
            string N1_LANG = null;
            string N1_RIN = null;
            string N1_RFN = null;
            List<string> N1_NOTE_liste_ID = new List<string>();
            CHANGE_DATE N1_CHAN = new CHANGE_DATE
            {
                N1_CHAN_DATE = null,
                N2_CHAN_DATE_TIME = null
            };
            N0_ID = Extraire_ID(dataGEDCOM[ligne]);
            Tb_Status.Text = "Décodage du data. Lecture des chercheurs ID  " + N0_ID;
            ligne++;
            Avoir_code_erreur();
            try
            {
                Avoir_code_erreur();
                while (Extraire_niveau(ligne) > 0)
                {
                    Avoir_code_erreur();
                    Application.DoEvents();
                    if (Extraire_balise(dataGEDCOM[ligne]) == "1 NAME")                                 // 1 NAME
                    {
                        Avoir_code_erreur();
                        N1_NAME = Extraire_ligne(dataGEDCOM[ligne], 4);
                        N1_NAME = Extraire_NAME(N1_NAME);
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 ADDR")                            // 1 ADDR
                    {
                        Avoir_code_erreur();
                        (N1_ADDR, ligne) = Extraire_ADDRESS_STRUCTURE(ligne);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 PHON")                            // 2 PHON
                    {
                        Avoir_code_erreur();
                        N1_PHON_liste.Add(Extraire_ligne(dataGEDCOM[ligne], 4));
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 FAX")                             // 2 FAX
                    {
                        Avoir_code_erreur();
                        N1_FAX_liste.Add(Extraire_ligne(dataGEDCOM[ligne], 3));
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 EMAIL")                           // 2 EMAIL
                    {
                        Avoir_code_erreur();
                        N1_EMAIL_liste.Add(Extraire_ligne(dataGEDCOM[ligne], 5));
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 _EMAIL")                          // 2 _EMAIL
                    {
                        Avoir_code_erreur();
                        N1_EMAIL_liste.Add(Extraire_ligne(dataGEDCOM[ligne], 6));
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 WWW")                             // 2 WWW
                    {
                        Avoir_code_erreur();
                        N1_WWW_liste.Add(Extraire_ligne(dataGEDCOM[ligne], 3));
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 _WWW")                            // 2 WWW
                    {
                        Avoir_code_erreur();
                        N1_WWW_liste.Add(Extraire_ligne(dataGEDCOM[ligne], 4));
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 _URL")                            // 2 _URL 
                    {
                        Avoir_code_erreur();
                        N1_WWW_liste.Add(Extraire_ligne(dataGEDCOM[ligne], 4));
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 LANG")                            // 1 LANG
                    {
                        Avoir_code_erreur();
                        N1_LANG += Extraire_ligne(dataGEDCOM[ligne], 4);
                        if (N1_LANG.ToLower() == "english") N1_LANG = "Anglais";
                        if (N1_LANG.ToLower() == "french") N1_LANG = "Français";
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 NOTE")                            // 1 NOTE
                    {
                        Avoir_code_erreur();
                        string IDNote;
                        (IDNote, ligne) = Extraire_NOTE_RECORD(ligne);
                        N1_NOTE_liste_ID.Add(IDNote);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 OBJE")                            // 1 OBJE
                    {
                        Avoir_code_erreur();
                        string IDObje;
                        (IDObje, ligne) = Extraire_MULTIMEDIA_RECORD(ligne);
                        N1_OBJE_ID_liste.Add(IDObje);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 CHAN")                            // 1 CHAN
                    {
                        Avoir_code_erreur();
                        (N1_CHAN, ligne) = Extraire_CHANGE_DATE(ligne);
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 RIN")                             // 1 RIN
                    {
                        Avoir_code_erreur();
                        N1_RIN += Extraire_ligne(dataGEDCOM[ligne], 3);
                        ligne++;
                    }
                    else if (Extraire_balise(dataGEDCOM[ligne]) == "1 RFN")                             // 1 RFN
                    {
                        Avoir_code_erreur();
                        N1_RFN += Extraire_ligne(dataGEDCOM[ligne], 3);
                        ligne++;
                    }
                    else
                    {
                        Avoir_code_erreur();
                        EcrireBalise(ligne);
                        ligne++;
                    }
                }
            }
            catch (Exception msg)
            {
                string message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                Voir_message(message, msg.Message, erreur);
            }
            if (!GH.GH.annuler)
            {
                liste_SUBMITTER_RECORD.Add(new SUBMITTER_RECORD()
                {
                    N0_ID = N0_ID,
                    N1_NAME = N1_NAME,
                    N1_ADDR = N1_ADDR,
                    N1_PHON_liste = N1_PHON_liste,
                    N1_FAX_liste = N1_FAX_liste,
                    N1_EMAIL_liste = N1_EMAIL_liste,
                    N1_WWW_liste = N1_WWW_liste,
                    N1_LANG = N1_LANG,
                    N1_OBJE_ID_liste = N1_OBJE_ID_liste,
                    N1_RIN = N1_RIN,
                    N1_RFN = N1_RFN,
                    N1_NOTE_liste_ID = N1_NOTE_liste_ID,
                    N1_CHAN = N1_CHAN
                });
            }
            return (ligne);
        }
        private static (string, int) Extraire_TEXT(int ligne)
        {
            int niveau_I = Extraire_niveau(ligne);
            string TEXT;

            TEXT = dataGEDCOM[ligne].Substring(7, dataGEDCOM[ligne].Length - 7);
            ligne++;
            while (Extraire_niveau(ligne) > niveau_I)
            {
                if (Extraire_balise(dataGEDCOM[ligne]) == (niveau_I + 1).ToString() + " CONT")          // +1 CONT
                {
                    if (dataGEDCOM[ligne].Length > 7)
                    {
                        TEXT += "<br />" + dataGEDCOM[ligne].Substring(7, dataGEDCOM[ligne].Length - 7);
                        ligne++;
                    }
                    else
                    {
                        TEXT += "<br />";
                        ligne++;
                    }
                }
                else if (Extraire_balise(dataGEDCOM[ligne]) ==                                          // +1 CONT
                    (niveau_I + 1).ToString() + " CONC" && dataGEDCOM[ligne].Length > 7)
                {
                    TEXT += dataGEDCOM[ligne].Substring(7, dataGEDCOM[ligne].Length - 7);
                    ligne++;
                }
                else
                {
                    EcrireBalise(ligne);
                    ligne++;
                }
            }
            return (TEXT, ligne);
        }
        private static (USER_REFERENCE_NUMBER, int) Extraire_USER_REFERENCE_NUMBER(int ligne)
        {
            int niveau_I = Extraire_niveau(ligne);
            USER_REFERENCE_NUMBER info = new USER_REFERENCE_NUMBER
            {
                N1_TYPE = "",
                N0_REFN = Extraire_ligne(dataGEDCOM[ligne], 4)
            };
            info.N0_REFN = Extraire_ligne(dataGEDCOM[ligne], 4);
            ligne++;
            while (Extraire_niveau(ligne) > niveau_I)
            {
                if (Extraire_balise(dataGEDCOM[ligne]) ==                                               // +1 TYPE
                    (niveau_I + 1).ToString() + " TYPE" && dataGEDCOM[ligne].Length > 7)
                {
                    info.N1_TYPE += Extraire_ligne(dataGEDCOM[ligne], 4);
                    ligne++;
                }
                else
                {
                    EcrireBalise(ligne);
                    ligne++;
                }
            }
            return (info, ligne);
        }
        private static string Extraire_texte_ligne1(string s)
        {
            int p1 = 0;
            if (s.Contains("NOTE")) p1 = s.IndexOf("NOTE");
            if (s.Contains("SOUR")) p1 = s.IndexOf("SOUR");
            if (s.Length > p1 + 5)
            {
                return s.Substring(p1 + 5);
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fichier"></param>
        /// <returns>nom du fichier original ou convertie en UTF8</returns>
        public static string LireEnteteGEDCOM(string fichier)
        {
            Avoir_code_erreur();
            try
            {
                Avoir_code_erreur();
                ligne = 0;
                Info_HEADER.N1_CHAR = null;
                List<string> entete = new List<string>();
                Info_HEADER.N2_GEDC_FORM = "";
                Tb_Status.Text = "CCCVerification de la validité du fichier " + Path.GetFileName(fichier) + " et de la page de code";
                Application.DoEvents();
                System.IO.StreamReader fichierCodage = new System.IO.StreamReader(@fichier);
                entete.Add(fichierCodage.ReadLine());
                Avoir_code_erreur();
                if (entete[0].Substring(0, 6) != "0 HEAD")
                {
                    MessageBox.Show("Le fichier ne semble pas être un fichier au format GEDCOM.\r\n\r\n" + "\r\n\r\n", "GEDCOM ?",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                    return null;
                }
                string text;
                Avoir_code_erreur();
                do
                {
                    text = fichierCodage.ReadLine();
                    entete.Add(text);
                } while (text[0].ToString() != "0");
                entete.Add("0 TRLR");
                Avoir_code_erreur();
                do
                {
                    Avoir_code_erreur();
                    if (Extraire_balise(entete[ligne]) == "0 HEAD")                                     // 0 HEAD
                    {
                        Avoir_code_erreur();
                        ligne++;
                        while ((int)Char.GetNumericValue(entete[ligne][0]) > 0)
                        {
                            Avoir_code_erreur();
                            if (Extraire_balise(entete[ligne]) == "1 CHAR")                             // 1 CHAR
                            {
                                Avoir_code_erreur();
                                Info_HEADER.N1_CHAR = Extraire_ligne(entete[ligne], 4);
                                ligne++;

                                while ((int)Char.GetNumericValue(entete[ligne][0]) > 1)
                                {
                                    Avoir_code_erreur();
                                    ligne++;
                                }
                            }
                            else if (Extraire_balise(entete[ligne]) == "1 GEDC")                        // 1 GEDC
                            {
                                Avoir_code_erreur();
                                ligne++;
                                while ((int)Char.GetNumericValue(entete[ligne][0]) > 1)
                                {
                                    Avoir_code_erreur();
                                    if (Extraire_balise(entete[ligne]) == "2 FORM")                     // 2 FORM
                                    {
                                        Avoir_code_erreur();
                                        Info_HEADER.N2_GEDC_FORM = Extraire_ligne(entete[ligne], 4);
                                        ligne++;
                                    }
                                    else
                                    {
                                        Avoir_code_erreur();
                                        ligne++;
                                    }
                                }
                            }
                            else
                            {
                                Avoir_code_erreur();
                                ligne++;
                            }
                        }
                    }
                    ligne++;
                    Avoir_code_erreur();
                }
                while (!entete[ligne].Contains("0 TRLR"));
                fichierCodage.Close();
                Avoir_code_erreur();
                if (Info_HEADER.N1_CHAR == "IBMPC")
                {
                    MessageBox.Show("Le fichier GEDCOM utilise le jeu de caractères IBMPC.\r\n\r\n" +
                        "Le jeu de caractères IBMPC n'est pas autorisé. Ce jeu de " +
                        "caractères ne peut pas être interprété correctement sans " +
                        "savoir quelle page code l'expéditeur utilisait, selon " +
                        "la norme GEDCOM.\r\n\r\n" +
                        "GH va lire le fichier. Certains caractères " + 
                        "seront invalides, en particulier les accents.\r\n",
                        "Jeu de caractères IBMPC",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                // si en en codage ANSI ou ANSI convertir à UFT8
                if (
                    Info_HEADER.N1_CHAR == "ANSI" ||
                    Info_HEADER.N1_CHAR == "ASCII" ||
                    Info_HEADER.N1_CHAR == "ANSEL"
                   )
                {
                    Avoir_code_erreur();
                    Tb_Status.Text = "DDDConvertion du fichier " + Path.GetFileName(fichier) + "de code de page " +
                        Info_HEADER.N1_CHAR + " en UTF8.";
                    Application.DoEvents();
                    string utf8String = "";
                    if (
                        Info_HEADER.N1_CHAR == "ANSI" ||
                        Info_HEADER.N1_CHAR == "ASCII" ||
                        Info_HEADER.N1_CHAR == "ANSEL"
                       )
                    {
                        Avoir_code_erreur();
                        byte[] ansiBytes = File.ReadAllBytes(fichier);
                        utf8String = Encoding.Default.GetString(ansiBytes);
                    }
                    Avoir_code_erreur();
                    string sansExtention = Path.GetFileNameWithoutExtension(fichier);
                    fichier = Path.GetTempPath() + "UTF8-" + sansExtention + ".gedCopie";
                    File.WriteAllText(fichier, utf8String);
                }
                Tb_Status.Text = "";
                Application.DoEvents();
                Avoir_code_erreur();
                return fichier;
            }
            catch (Exception msg)
            {
                string message;
                if (ligne > 0)
                {
                    message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                    Voir_message(message, msg.Message, erreur);
                    return null;
                }
                message = "Erreur dans la lecture du fichier GEDCOM.";
                Voir_message(message, msg.Message, erreur);
                return null;
            }
        }
        public static bool LireGEDCOM(string fichier)
        {
            Avoir_code_erreur();
            try
            {
                Avoir_code_erreur();
                dataGEDCOM = new List<string>();
                dataGEDCOM.Clear();
                string text;
                Tb_Status.Text = "Lecture du fichier " + Path.GetFileName(fichier);
                Application.DoEvents();
                long position = 0;
                System.IO.StreamReader file = new System.IO.StreamReader(@fichier);
                Avoir_code_erreur();
                ligne = 0;
                text = file.ReadLine();
                dataGEDCOM.Add(text);
                Avoir_code_erreur();
                Info_HEADER.Nom_fichier_disque = fichier;
                while ((text = file.ReadLine()) != null)
                {
                    Avoir_code_erreur();
                    position += text.Length;
                    if (Info_HEADER.N1_CHAR.ToLower() == "ansel") text = Convertir_ANSEL(text);
                    if (text == "") text = "9 ESPACE";
                    dataGEDCOM.Add(text);
                    if (text.Contains("0 TRLR") || file.EndOfStream)
                    {
                        file.Close();
                        break;
                    }
                    ligne++;
                }
                file.Close();
                Avoir_code_erreur();
                // effacer fichier  si extention .gedCopie
                if (Path.GetExtension(fichier) == ".gedCopie")
                {
                    if (File.Exists(@fichier))
                    {
                        File.Delete(@fichier);
                    }
                }
                Avoir_code_erreur();
                return true;
            }
            catch (Exception msg)
            {
                string message;
                if (ligne > 0)
                {
                    message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                    Voir_message(message, msg.Message, erreur);
                    return false;
                }
                message = "Erreur dans la lecture du fichier GEDCOM.";
                Voir_message(message, msg.Message, erreur);
                return false;
            }
        }
        public static string AvoirIDAdoption(string ID)
        {
            foreach (INDIVIDUAL_RECORD info in liste_INDIVIDUAL_RECORD)
            {
                if (info.N0_ID == ID)
                {
                    return info.Adopter;
                }
            }
            return "";
        }
        public static HEADER AvoirInfoGEDCOM()
        {
            return Info_HEADER;
        }
        public static SUBMITTER_RECORD Avoir_info_chercheur(string ID)
        {
            foreach (SUBMITTER_RECORD info in liste_SUBMITTER_RECORD)
            {
                if (info.N0_ID == ID)
                {
                    return info;
                }
            }
            return null;
        }
        public static SUBMISSION_RECORD AvoirInfoSUBMISSION_RECORD()
        {
            return info_SUBMISSION_RECORD;
        }
        public static List<string> AvoirListIDEnfant(string ID)
        {
            foreach (FAM_RECORD info in liste_FAM_RECORD)
            {
                if (info.N0_ID == ID)
                {
                    return info.N1_CHIL_liste_ID;
                }
            }
            return null;
        }
        public static string Avoir_erreur_ligne(Exception msg)
        {
            int i = msg.StackTrace.LastIndexOf(" ");
            if (i > -1)
            {
                string s = msg.StackTrace.Substring(i + 1);
                if (int.TryParse(s, out int ligne))
                    return ligne.ToString();
            }
            return null;
        }
        public static string Avoir_famille_conjoint_ID(string ID)
        {
            foreach (FAM_RECORD info in liste_FAM_RECORD)
            {
                if (info.N0_ID == ID)
                {
                    return info.N1_HUSB;
                }
            }
            return null;
        }
        public static string Avoir_famille_conjointe_ID(string ID)
        {
            foreach (FAM_RECORD info in liste_FAM_RECORD)
            {
                if (info.N0_ID == ID)
                {
                    return info.N1_WIFE;
                }
            }
            return null;
        }
        public static EVENT_ATTRIBUTE_STRUCTURE Avoir_attribute_nombre_enfant(List<EVENT_ATTRIBUTE_STRUCTURE> liste)
        {
            foreach (EVENT_ATTRIBUTE_STRUCTURE info in liste)
            {
                if (info.N1_EVEN == "NCHI")
                    return info;
            }
            EVENT_ATTRIBUTE_STRUCTURE r = new EVENT_ATTRIBUTE_STRUCTURE();
            return r;
        }
        public static EVENT_ATTRIBUTE_STRUCTURE Avoir_attribute_nombre_mariage(List<EVENT_ATTRIBUTE_STRUCTURE> liste)
        {
            foreach (EVENT_ATTRIBUTE_STRUCTURE info in liste)
            {
                if (info.N1_EVEN == "NMR")
                    return info;
            }
            EVENT_ATTRIBUTE_STRUCTURE r = new EVENT_ATTRIBUTE_STRUCTURE();
            return r;
        }
        public static (bool, EVENT_ATTRIBUTE_STRUCTURE) AvoirEvenementDeces(List<EVENT_ATTRIBUTE_STRUCTURE> liste)
        {
            EVENT_ATTRIBUTE_STRUCTURE retourNull = new EVENT_ATTRIBUTE_STRUCTURE();
            if (liste == null) return (false, retourNull);
            foreach (EVENT_ATTRIBUTE_STRUCTURE info in liste)
            {
                if (info.N1_EVEN == "DEAT")
                    return (true, info);
            }

            return (false, retourNull);
        }
        public static EVENT_ATTRIBUTE_STRUCTURE AvoirEvenementMariage(List<EVENT_ATTRIBUTE_STRUCTURE> liste)
        {
            EVENT_ATTRIBUTE_STRUCTURE r = new EVENT_ATTRIBUTE_STRUCTURE();
            if (liste == null) return r;
            foreach (EVENT_ATTRIBUTE_STRUCTURE info in liste)
            {
                if (info.N1_EVEN == "MARR")
                {
                    return info;
                }
            }
            return r;
        }
        public static (bool, EVENT_ATTRIBUTE_STRUCTURE) AvoirEvenementNaissance(List<EVENT_ATTRIBUTE_STRUCTURE> liste)
        {
            
            EVENT_ATTRIBUTE_STRUCTURE retourNull = new EVENT_ATTRIBUTE_STRUCTURE();
            if (liste == null ) return (false, retourNull);
            foreach (EVENT_ATTRIBUTE_STRUCTURE info in liste)
            {
                if (info.N1_EVEN == "BIRT")
                return (true, info);
            }
            return (false, retourNull);
        }
        public static CHILD_TO_FAMILY_LINK AvoirInfoFamilleEnfant(string ID)
        {
            foreach (INDIVIDUAL_RECORD info in liste_INDIVIDUAL_RECORD)
            {
                if (info.N0_ID == ID)
                {
                    return info.N1_FAMC;
                }
            }
            return null;
        }
        public static List<string> AvoirListeIDFamille()
        {
            List<string> ListeID = new List<string>();
            foreach (FAM_RECORD info in liste_FAM_RECORD)
            {
                ListeID.Add(info.N0_ID);
            }
            return ListeID;
        }
        public static (bool, INDIVIDUAL_RECORD) Avoir_info_individu(string ID)
        {
            foreach (INDIVIDUAL_RECORD info in liste_INDIVIDUAL_RECORD)
            {
                if (info.N0_ID == ID)
                {
                    return (true, info);
                }
            }
            INDIVIDUAL_RECORD retour = new INDIVIDUAL_RECORD();
            return (false, retour);
        }
        public static SOURCE_RECORD AvoirInfoSource(string ID)
        {
            foreach (SOURCE_RECORD info in liste_SOURCE_RECORD)
            {
                if (info.N0_ID == ID)
                {
                    return info;
                }
            }
            return null;
        }
        public static List<string> AvoirListeIDIndividu()
        {

            List<string> ListeID = new List<string>();
            ListeID.Clear();
            foreach (INDIVIDUAL_RECORD info in liste_INDIVIDUAL_RECORD)
            {
                ListeID.Add(info.N0_ID);
            }
            return ListeID;
        }
        public static FAM_RECORD Avoir_info_famille(string ID)
        {
            foreach (FAM_RECORD info in liste_FAM_RECORD)
            {
                if (info.N0_ID == ID)
                {
                    return info;
                }
            }
            return null;
        }
        public static (bool, List<PERSONAL_NAME_STRUCTURE>) AvoirListeNom(string ID)
        {
            foreach (INDIVIDUAL_RECORD info in liste_INDIVIDUAL_RECORD)
            {
                if (info.N0_ID == ID)
                {
                    if (info.N1_NAME_liste == null)
                    {
                        return (false, null);
                    }
                    else if (info.N1_NAME_liste.Count == 0)
                    {
                        return (false, null);
                    }
                    else
                    {
                        return (true, info.N1_NAME_liste);
                    }
                }
            }
            return (false, null);
        }
        public static MULTIMEDIA_RECORD Avoir_info_media(string ID)
        {
            foreach (MULTIMEDIA_RECORD info in liste_MULTIMEDIA_RECORD)
            {
                if (info.N0_ID == ID)
                {
                    return info;
                }
            }
            return null;
        }
        public static string AvoirPrenomPatronymeIndividu(string ID)
        {
            if (ID == "") return null;
            if (ID == " ") return null;
            if (ID == null) return null;
            bool Ok;
            List<PERSONAL_NAME_STRUCTURE> info;
            (Ok, info) = AvoirListeNom(ID);
            if (!Ok) return "";
            string patronyme = info[0].N1_PERSONAL_NAME_PIECES.Nn_SURN;
            string prenom = info[0].N1_PERSONAL_NAME_PIECES.Nn_GIVN;
            if (prenom == "" && patronyme == "") return null;
            if (prenom == null && patronyme == null) return null;
            if (prenom == "") prenom = "?";
            if (prenom == null) prenom = "?";
            if (patronyme == "") patronyme = "?";
            if (patronyme == null) patronyme = "?";
            return prenom + " " + patronyme;
        }
        public static string AvoirPremierNomIndividu(string ID)
        {
            if (ID == "") return null;
            if (ID == null) return null;
            bool Ok;
            List<PERSONAL_NAME_STRUCTURE> info;
            (Ok, info) = AvoirListeNom(ID);
            if (!Ok) return null;
            string patronyme = null;
            string prenom = null;
            if (info[0].N1_PERSONAL_NAME_PIECES != null)
            {
                patronyme = info[0].N1_PERSONAL_NAME_PIECES.Nn_SURN;
                prenom = info[0].N1_PERSONAL_NAME_PIECES.Nn_GIVN;
            }
            if (prenom == null && patronyme == null) return info[0].N0_NAME;
            if (prenom == null) prenom = "?";
            if (patronyme == null) patronyme = "?";
            return patronyme + ", " + prenom;
        }
        public static string Convert_Age_GEDCOM(string age)
        {
            if (age.ToUpper() == "CHILD") return "enfant";
            if (age.ToUpper() == "INFANT") return "bébé";
            if (age.ToUpper() == "STILLBORN") return "mort né";
            char[] espace = { ' ' };
            string[] section = age.Split(espace);
            int nombreSection = section.Length;
            for (int f = 0; f < nombreSection; f++)
            {
                if (section[f].ToLower().Contains(">"))
                    section[f] = "après ";
                if (section[f].ToLower().Contains("<"))
                    section[f] = "avant ";
                if (section[f].ToLower().Contains("y"))
                {
                    section[f] = section[f].Remove(section[f].Length - 1, 1);
                    if (int.Parse(section[f]) == 1)
                    {
                        section[f] += " an";
                    }
                    else
                    {
                        section[f] += " ans";
                    }
                }
                if (section[f].ToLower().Contains("m"))
                {
                    section[f] = section[f].Remove(section[f].Length - 1, 1);
                    section[f] += " mois";
                }
                if (section[f].ToLower().Contains("d"))
                {
                    section[f] = section[f].Remove(section[f].Length - 1, 1);
                    if (int.Parse(section[f]) == 1)
                    {
                        section[f] += " jour";
                    }
                    else
                    {
                        section[f] += " jours";
                    }
                }
            }
            if (nombreSection == 1) age = section[0];
            if (nombreSection == 2) age = section[0] + " " + section[1];
            if (nombreSection == 3) age = section[0] + " " + section[1] + " " + section[2];
            if (nombreSection == 4) age = section[0] + " " + section[1] + " " + section[2] + " " + section[3];
            if (age.All(char.IsDigit))
            {
                if (int.Parse(section[0]) == 1) age += " an";
                else age += " ans";
            }
            return age;
        }
        public static string Convertir_ANSEL(string texte)
        {
            if (texte == "") return "";
            texte = texte.Replace("Comments", "cOmMeNtS");
            // E0 accent à                      OK

            texte = texte.Replace("àA", "À");
            texte = texte.Replace("àE", "È");
            texte = texte.Replace("àI", "Ì");
            texte = texte.Replace("àO", "Ò");
            texte = texte.Replace("àU", "Ù");
            texte = texte.Replace("àY", "Y");

            texte = texte.Replace("àa", "à");
            texte = texte.Replace("àe", "è");
            texte = texte.Replace("ài", "ì");
            texte = texte.Replace("ào", "ò");
            texte = texte.Replace("àu", "ù");
            texte = texte.Replace("ày", "y");


            //E1 accent á                       OK
            texte = texte.Replace("áA", "Á");
            texte = texte.Replace("áE", "É");
            texte = texte.Replace("áI", "Í");
            texte = texte.Replace("áO", "Ó");
            texte = texte.Replace("áU", "Ú");
            texte = texte.Replace("áY", "Ý");

            texte = texte.Replace("áa", "á");
            texte = texte.Replace("áe", "é");
            texte = texte.Replace("ái", "í");
            texte = texte.Replace("áo", "ó");
            texte = texte.Replace("áu", "ú");
            texte = texte.Replace("áy", "ý");

            //E2 accent â                       OK
            texte = texte.Replace("âA", "Â");
            texte = texte.Replace("âE", "Ê");
            texte = texte.Replace("âI", "Î");
            texte = texte.Replace("âO", "Ô");
            texte = texte.Replace("âU", "Û");
            texte = texte.Replace("âY", "Y");

            texte = texte.Replace("âa", "â");
            texte = texte.Replace("âe", "ê");
            texte = texte.Replace("âi", "î");
            texte = texte.Replace("âo", "ô");
            texte = texte.Replace("âu", "û");
            texte = texte.Replace("ây", "Y");

            //E3 accent ã
            texte = texte.Replace("ãA", "Ã");
            texte = texte.Replace("ãE", "E");
            texte = texte.Replace("ãI", "I");
            texte = texte.Replace("ãO", "Õ");
            texte = texte.Replace("ãU", "U");
            texte = texte.Replace("ãY", "Y");
            texte = texte.Replace("ãN", "Ñ");

            texte = texte.Replace("ãa", "ã");
            texte = texte.Replace("ãe", "e");
            texte = texte.Replace("ãi", "i");
            texte = texte.Replace("ão", "õ");
            texte = texte.Replace("ãu", "u");
            texte = texte.Replace("ãy", "Y");
            texte = texte.Replace("ãn", "ñ");

            //E4 accent ä                       OK
            texte = texte.Replace("äA", "Ä");
            texte = texte.Replace("äE", "Ë");
            texte = texte.Replace("äI", "Ï");
            texte = texte.Replace("äO", "Ö");
            texte = texte.Replace("äU", "Ü");
            texte = texte.Replace("äY", "Ÿ");

            texte = texte.Replace("äa", "ä");
            texte = texte.Replace("äe", "ë");
            texte = texte.Replace("äi", "ï");
            texte = texte.Replace("äo", "ö");
            texte = texte.Replace("äu", "ü");
            texte = texte.Replace("äy", "ÿ");


            //E5 accent å
            texte = texte.Replace("åA", "Å");
            texte = texte.Replace("åE", "E");
            texte = texte.Replace("åI", "I");
            texte = texte.Replace("åO", "O");
            texte = texte.Replace("åU", "U");
            texte = texte.Replace("åY", "Y");

            texte = texte.Replace("åa", "å");
            texte = texte.Replace("åe", "e");
            texte = texte.Replace("åi", "i");
            texte = texte.Replace("åo", "o");
            texte = texte.Replace("åu", "u");
            texte = texte.Replace("åy", "y");


            //E6 accent æ
            texte = texte.Replace("æA", "Æ");
            //texte = texte.Replace("æE", "");
            //texte = texte.Replace("æI", "");
            texte = texte.Replace("æA", "OE");
            //texte = texte.Replace("æU", "");
            //texte = texte.Replace("æY", "");

            texte = texte.Replace("æa", "æ");
            //texte = texte.Replace("æe", "");
            //texte = texte.Replace("æi", "");
            texte = texte.Replace("æo", "oe");
            //texte = texte.Replace("æu", "");
            //texte = texte.Replace("æy", "");

            //E7 accent ç
            texte = texte.Replace("çC", "Ç");
            /*texte = texte.Replace("A", "");
            texte = texte.Replace("E", "");
            texte = texte.Replace("I", "");
            texte = texte.Replace("O", "");
            texte = texte.Replace("U", "");
            texte = texte.Replace("Y", "");*/

            texte = texte.Replace("çc", "ç");
            /*texte = texte.Replace("a", "");
            texte = texte.Replace("e", "");
            texte = texte.Replace("i", "");
            texte = texte.Replace("o", "");
            texte = texte.Replace("u", "");
            texte = texte.Replace("y", "");*/


            //E8 accent è
            texte = texte.Replace("èA", "À");
            texte = texte.Replace("èE", "È");
            texte = texte.Replace("èI", "Ì");
            texte = texte.Replace("èO", "Ò");
            texte = texte.Replace("èU", "Ù");
            texte = texte.Replace("èY", "Y");

            texte = texte.Replace("èa", "à");
            texte = texte.Replace("èe", "è");
            texte = texte.Replace("èi", "ì");
            texte = texte.Replace("èo", "ò");
            texte = texte.Replace("èu", "ù");
            texte = texte.Replace("èy", "y");

            //E9 accent é  
            texte = texte.Replace("éA", "Á");
            texte = texte.Replace("éE", "É");
            texte = texte.Replace("éI", "Í");
            texte = texte.Replace("éO", "Ó");
            texte = texte.Replace("éU", "Ú");
            texte = texte.Replace("éY", "Ý");

            texte = texte.Replace("éa", "á");
            texte = texte.Replace("ée", "é");
            texte = texte.Replace("éi", "í");
            texte = texte.Replace("éo", "ó");
            texte = texte.Replace("éu", "ú");
            texte = texte.Replace("éy", "ý");

            //EA accent ê
            texte = texte.Replace("êA", "Â");
            texte = texte.Replace("êE", "Ê");
            texte = texte.Replace("êI", "Î");
            texte = texte.Replace("êO", "Ô");
            texte = texte.Replace("êU", "Û");
            texte = texte.Replace("êY", "Y");

            texte = texte.Replace("êa", "â");
            texte = texte.Replace("êe", "ê");
            texte = texte.Replace("êi", "î");
            texte = texte.Replace("êo", "ô");
            texte = texte.Replace("êu", "û");
            texte = texte.Replace("êy", "y");

            //EB accent ë
            texte = texte.Replace("ëA", "Ä");
            texte = texte.Replace("ëE", "Ë");
            texte = texte.Replace("ëI", "Ï");
            texte = texte.Replace("ëO", "Ö");
            texte = texte.Replace("ëU", "Ü");
            texte = texte.Replace("ëY", "Y");

            texte = texte.Replace("ëa", "ä");
            texte = texte.Replace("ëe", "ë");
            texte = texte.Replace("ëi", "ï");
            texte = texte.Replace("ëo", "ö");
            texte = texte.Replace("ëu", "ü");
            texte = texte.Replace("ëy", "ÿ");

            //EC accent ì
            texte = texte.Replace("ìA", "À");
            texte = texte.Replace("ìE", "È");
            texte = texte.Replace("ìI", "Ì");
            texte = texte.Replace("ìO", "Ò");
            texte = texte.Replace("ìU", "Ù");
            texte = texte.Replace("ìY", "Y");

            texte = texte.Replace("ìa", "à");
            texte = texte.Replace("ìe", "è");
            texte = texte.Replace("ìi", "ì");
            texte = texte.Replace("ìo", "ò");
            texte = texte.Replace("ìu", "ù");
            texte = texte.Replace("ìy", "y");

            //ED accent í
            texte = texte.Replace("íA", "Á");
            texte = texte.Replace("íE", "É");
            texte = texte.Replace("íI", "Í");
            texte = texte.Replace("íO", "Ó");
            texte = texte.Replace("íU", "Ú");
            texte = texte.Replace("íY", "Ý");

            texte = texte.Replace("ía", "á");
            texte = texte.Replace("íe", "é");
            texte = texte.Replace("íi", "í");
            texte = texte.Replace("ío", "ó");
            texte = texte.Replace("íu", "ú");
            texte = texte.Replace("íy", "ý");

            //EE accent ï
            texte = texte.Replace("ïA", "Ä");
            texte = texte.Replace("ïE", "Ë");
            texte = texte.Replace("ïI", "Ï");
            texte = texte.Replace("ïO", "Ö");
            texte = texte.Replace("ïU", "Ü");
            texte = texte.Replace("ïY", "Y");

            texte = texte.Replace("ïa", "ä");
            texte = texte.Replace("ïe", "ë");
            texte = texte.Replace("ïi", "ï");
            texte = texte.Replace("ïo", "ö");
            texte = texte.Replace("ïu", "ü");
            texte = texte.Replace("ïy", "ÿ");

            if (Info_HEADER.N2_CHAR_VERS == "ANSI Z39.47-1985")
            {
                texte = texte.Replace("Ã", "©");
            }
            return texte;
        }
        public static string ConvertirDateTexte(string date)
        {
            string mois;
            // si date  = "" ou null retourner ""
            if (date == "" || date == null)
            {
                return "";
            }
            // vérifie si alphabet dans la date, si oui retourne sans changement
            int c = date.Length;
            int w = 0;
            while (w < c)
            {
                if ((date[w] >= 'a' && date[w] <= 'z') || (date[w] >= 'A' && date[w] <= 'Z'))
                {
                    return date;
                }
                w++;
            }
            char[] s = { '-' };
            string[] d = date.Split(s);
            int l = d.Length;
            if (l == 1)
            {
                return date;
            }
            if (l == 2)
            {
                mois = ConvertirMoisEnLong(d[1]);
                return mois + " " + d[0];
            }
            if (l == 3)
            {
                mois = ConvertirMoisEnLong(d[1]);
                if (d[2] == "01") d[2] = "1<sup>er</sup>";
                return d[2] + " " + mois + " " + d[0];
            }
            return "";
        }
        private static string ConvertirDateGEDCOM(string date)
        {
            if (date == "" || date == null)
            {
                return "";
            }
            date = date.ToUpper().Trim();
            char[] s = { ' ' };
            char zero = '0';
            string[] d = date.Split(s);
            int l = d.Length; // nombre item dans la date ex. CAL 31 DEC 1997 l=4
            if (l == 1)
            {
                return date; // année seulement
            }
            /*
                ABT  ABT. = About, meaning the date is not exact.
                CAL = Calculated mathematically, for example, from an event date and age.
                EST = Estimated based on an algorithm using some other event date.

                FROM = Indicates the beginning of a happening or state.
                TO = Indicates the ending of a happening or state

                AFT AFT.= Event happened after the given date.
                BEF BEF. = Event happened before the given date.
                BET = Event happened sometime between date 1 AND date 2

                The date modifiers ABT, BEF & AFT are three-letter abbreviations. Several old
                versions of Family Tree Maker use the illegal four-letter abbreviations ABT., BEF.
                & AFT.
             */
            if (l == 2)
            {
                // 0        1       2       3       4       5       6       7  
                // ABT      1852
                if (d[0] == "ABT" || d[0] == "ABT.")
                {
                    return "Autour " + d[1];
                }
                // AFT      1852
                if (d[0] == "AFT" || d[0] == "AFT.")
                {
                    return "Autour " + d[1];
                }
                // BEF      1852
                if (d[0] == "BEF" || d[0] == "BEF.")
                {
                    return "Avant " + d[1];
                }
                // EST      1852
                if (d[0] == "EST" || d[0] == "EST.")
                {
                    return "Estimé " + d[1];
                }
                // CAL      1852
                if (d[0] == "CAL" || d[0] == "CAL.")
                {
                    return "Calculé " + d[1];
                }
                if (d[0] == "TO")
                {
                    return "Jusqu'au " + d[1];
                }
                // DEC      1852    
                return d[1] + "-" + ConvertirMoisEnChiffre(d[0]);
            }
            if (l == 3)
            {
                // 0        1       2       3       4       5       6       7  
                // ABT      DEC     1852
                if (d[0] == "ABT" || d[0] == "ABT.")
                {
                    return "Autour " + d[2] + "-" + ConvertirMoisEnChiffre(d[1]);
                }
                // AFT      DEC     1852
                if (d[0] == "AFT" || d[0] == "AFT.")
                {
                    return "Après " + d[2] + ConvertirMoisEnChiffre(d[1]);
                }
                //ABT DEC 1852
                if (d[1] == "ABT" || d[1] == "ABT.")
                {
                    return "Autour + " + d[2] + "-" + ConvertirMoisEnChiffre(d[1]);
                }
                // BEF      DEC     1852
                if (d[1] == "BEF" || d[1] == "BEF.")
                {
                    return "Avant + " + d[2] + "-" + ConvertirMoisEnChiffre(d[1]);
                }
                // CAL      DEC     1852
                if (d[0] == "CAL" || d[0] == "CAL")
                {
                    return "Calculé + " + d[2] + "-" + ConvertirMoisEnChiffre(d[1]);
                }
                // EST      DEC     1852
                if (d[0] == "EST" || d[0] == "EST.")
                {
                    return "Estimé " + d[2] + "-" + ConvertirMoisEnChiffre(d[1]);
                }
                if (d[0] == "TO")
                {
                    return "Jusqu'au " + d[2] + "-" + d[1];
                }
                // 24       DEC     1852 
                return d[2] + "-" + ConvertirMoisEnChiffre(d[1]) + "-" + d[0].PadLeft(2, zero);
            }
            if (l == 4)
            {
                // 0        1       2       3       4       5       6       7  
                // ABT      24      DEC     1852
                if (d[0] == "ABT" || d[0] == "ABT.")
                {
                    return "Autour " + d[3] + "-" + ConvertirMoisEnChiffre(d[2]) + "-" + d[1].PadLeft(2, zero);
                }
                // BEF      24       DEC    1852
                if (d[0] == "BEF" || d[0] == "BEF.")
                {
                    return "Avant " + d[3] + "-" + ConvertirMoisEnChiffre(d[2]) + "-" + d[1].PadLeft(2, zero);
                }
                // AFT      24      DEC     1852
                if (d[0] == "AFT" || d[0] == "AFT")
                {
                    return "Après " + d[3] + "-" + ConvertirMoisEnChiffre(d[2]) + "-" + d[1].PadLeft(2, zero);
                }
                // BEF      24      DEC     1852
                if (d[0] == "BEF" || d[0] == "BEF.")
                {
                    return "Avant " + d[3] + "-" + ConvertirMoisEnChiffre(d[2]) + "-" + d[1].PadLeft(2, zero);
                }
                // BET     1852    AND     1853
                if ((d[0] == "BET" || d[0] == "BET.") && d[2] == "AND")
                {
                    return "Entre " + d[1] + " et " + d[3];
                }
                // FROM     1852    TO      1853
                if (d[0] == "FROM" && d[2] == "TO")
                {
                    return "Du " + d[1] + " au " + d[3];
                }
                // CAL      24      DEC     1852
                if (d[0] == "CAL" || d[0] == "CAL.")
                {
                    return "Calculé " + d[3] + "-" + ConvertirMoisEnChiffre(d[2]) + "-" + d[1].PadLeft(2, zero);
                }
                // EST      24      DEC     1852
                if (d[0] == "EST" || d[0] == "EST.")
                {
                    return "Estimé " + d[3] + "-" + ConvertirMoisEnChiffre(d[2]) + "-" + d[1].PadLeft(2, zero);
                }
                // TO       24      DEC     1852
                if (d[0] == "TO")
                {
                    return "Jusqu'au " + d[3] + "-" + ConvertirMoisEnChiffre(d[2]) + "-" + d[1].PadLeft(2, zero);
                }
            }
            if (l == 5)
            {
                // 0        1       2       3       4       5       6       7  
                // BET     1852    AND     DEC     1853
                if ((d[0] == "BET" || d[0] == "BET.") && d[2] == "AND")
                {

                    return "Entre " + d[1] + " et " + d[4] + "-" + ConvertirMoisEnChiffre(d[3]);
                }
                // BET     DEC     1852    AND     1853 
                if ((d[0] == "BET" || d[0] == "BET.") && d[3] == "AND")
                {
                    return "Entre " + d[2] + "-" + ConvertirMoisEnChiffre(d[1]) + " et " + d[4];
                }
                // FROM     1852    TO      DEC     1853
                if (d[0] == "FROM" && d[3] == "TO")
                {
                    return "Du " + d[1] + " au " + d[4] + "-" + ConvertirMoisEnChiffre(d[3]);
                }
                // FROM     DEC     1852    TO      1853
                if (d[0] == "FROM" && d[3] == "TO")
                {
                    return "Du " + d[2] + "-" + ConvertirMoisEnChiffre(d[2]) + " au " + d[4];
                }
            }
            if (l == 6)
            {
                // 0        1       2       3       4       5       6       7  
                // BET     1852    AND     24      DEC     1853
                if ((d[0] == "BET" || d[0] == "BET.") && d[2] == "AND")
                {
                    return "Entre " + d[1] + " et " + d[5] + "-" + ConvertirMoisEnChiffre(d[4]) + "-" + d[3].PadLeft(2, zero);
                }
                // BET     DEC     1852    AND     DEC     1853
                if ((d[0] == "BET" || d[0] == "BET.") && d[3] == "AND")
                {
                    return "Entre " + d[2] + "-" + ConvertirMoisEnChiffre(d[1]) + " et " + d[5] + "-" + ConvertirMoisEnChiffre(d[4]);
                }
                // BET      24     DEC     1852    AND     1853
                if ((d[0] == "BET" || d[0] == "BET.") && d[4] == "AND")
                {
                    return "Entre " + d[3] + "-" + ConvertirMoisEnChiffre(d[2]) + "-" + d[1].PadLeft(2, zero) + " et " + d[5];
                }
                // FROM     24      DEC     1852    TO      1853
                if (d[0] == "FROM" && d[4] == "TO")
                {
                    return "Du " + d[3] + "-" + ConvertirMoisEnChiffre(d[2]) + "-" + d[1].PadLeft(2, zero) + " au " + d[5];
                }
                // FROM     DEC     1852    TO      DEC     1853
                if (d[0] == "FROM" && d[4] == "TO")
                {
                    return "Du " + d[2] + "-" + ConvertirMoisEnChiffre(d[1]) + " au " + d[5] + "-" + ConvertirMoisEnChiffre(d[3]);
                }
                // FROM     24      DEC     1852    TO      1853    
                if (d[0] == "FROM" && d[4] == "TO")
                {
                    return "Du " + d[3] + "-" + ConvertirMoisEnChiffre(d[2]) + "-" + d[1].PadLeft(2, zero) + " au " + d[5];
                }
            }
            if (l == 7)
            {
                // 0        1       2       3       4       5       6       7   
                // BET      DEC     1852    AND     24      DEC     1853
                if ((d[0] == "BET" || d[0] == "BET.") && d[3] == "AND")
                {
                    return "Entre " + d[2] + "-" + ConvertirMoisEnChiffre(d[1]) + " et " + d[6] + ConvertirMoisEnChiffre(d[5]) + "-" + d[4].PadLeft(2, zero);
                }
                // BET      24     DEC     1852    AND      DEC     1853
                if ((d[0] == "BET" || d[0] == "BET.") && d[3] == "AND")
                {
                    return "Entre " + d[3] + ConvertirMoisEnChiffre(d[2]) + "-" + d[1].PadLeft(2, zero) + " et " + d[6] + "-" + ConvertirMoisEnChiffre(d[5]);
                }
                // FROM     DEC     1852    TO      DEC     1853
                if (d[0] == "FROM" && d[4] == "TO")
                {
                    return "Du " + d[2] + "-" + ConvertirMoisEnChiffre(d[1]) + " au " + d[5] + "-" + ConvertirMoisEnChiffre(d[4]);
                }
                // FROM     24      DEC     1852    TO      DEC     1853    
                if (d[0] == "FROM" && d[4] == "TO")
                {
                    return "Du " + d[3] + "-" + ConvertirMoisEnChiffre(d[2]) + "-" + d[1].PadLeft(2, zero) + " au " + d[6] + "-" + ConvertirMoisEnChiffre(d[5]);
                }
                // FROM     DEC     1852    TO      24      DEC     1853    
                if (d[0] == "FROM" && d[4] == "TO")
                {
                    return "Du " + d[2] + "-" + ConvertirMoisEnChiffre(d[1]) + " au " + d[6] + "-" + ConvertirMoisEnChiffre(d[5]) + "-" + d[4].PadLeft(2, zero);
                }
            }
            if (l == 8)
            {
                // 0        1       2       3       4       5       6       7   
                // BET      24     DEC     1852    AND     24      DEC     1853
                if ((d[0] == "BET" || d[0] == "BET.") && d[4] == "AND")
                {
                    return "Entre " + d[3] + "-" + ConvertirMoisEnChiffre(d[2]) + "-" + d[1].PadLeft(2, zero) + " et " + d[7] + "-" + ConvertirMoisEnChiffre(d[6]) + "-" + d[5].PadLeft(2, zero);
                }
                // FROM     24      DEC     1852    TO      24      DEC     1853    
                if (d[0] == "FROM" && d[4] == "TO")
                {
                    return "Du " + d[3] + "-" + ConvertirMoisEnChiffre(d[2]) + "-" + d[1].PadLeft(2, zero) + " au " + d[7] + "-" + ConvertirMoisEnChiffre(d[6]) + "-" + d[5].PadLeft(2, zero);
                }
            }
            return date;
        }
        public static string Convertir_EVENT_titre(string action, string ligne = null)
        {
            string texte;
            if (action == "ADOP") texte = "Adoption";
            else if (action == "ANUL") texte = "Annulation de mariage";
            else if (action == "BIRT") texte = "Naissance";
            else if (action == "BAPM") texte = "Baptême";
            else if (action == "BARM") texte = "Bar Mitzvah";
            else if (action == "BASM") texte = "Bat Mitsva";
            else if (action == "BLES") texte = "Bénédiction";
            else if (action == "CAST") texte = "Caste " + ligne;
            else if (action == "CENS") texte = "Recensement";
            else if (action == "CHR") texte = "Baptême christianisme";
            else if (action == "CHRA") texte = "Baptême adulte";
            else if (action == "CONF") texte = "Confirmation";
            else if (action == "CONL") texte = "Confirmation LDS";
            else if (action == "CREM") texte = "Crémation";
            else if (action == "DIVF") texte = "Demande de divorce par épouse";
            else if (action == "DSCR") texte = "Description physique";
            else if (action == "EDUC") texte = "Éducation " + ligne;
            else if (action == "EMIG") texte = "Départ de son pays avec l'intention de résider ailleurs";
            else if (action == "ENDL") texte = "cérémonie religieuse LDS";
            else if (action == "DEAT") texte = "Décès";
            else if (action == "BURI") texte = "Inhumation";
            else if (action == "DIV") texte = "Divorce";
            else if (action == "ENGA") texte = "Fiançailles";
            else if (action == "FCOM") texte = "Première communiun";
            else if (action == "GRAD") texte = "Graduation";
            else if (action == "INDO") texte = "Numero d'identification " + ligne;
            else if (action == "IMMI") texte = "Immigration";
            else if (action == "MARC") texte = "contrat de mariage";
            else if (action == "MARR") texte = "Mariage";
            else if (action == "MARS") texte = "Contrat de mariage";
            else if (action == "MARB") texte = "Publication des bans";
            else if (action == "MARL") texte = "Licence de mariage";
            else if (action == "NATI") texte = "Patrimoine national " + ligne;
            else if (action == "NATU") texte = "Naturalization";
            else if (action == "NCHI") texte = "Nombre d'enfant " + ligne;
            else if (action == "NMR") texte = "Nombre de mariage " + ligne;
            else if (action == "OCCU") texte = "Profession " + ligne;
            else if (action == "ORDN") texte = "Ordination religieuse";
            else if (action == "PROB") texte = "Homologation d'un testament";
            else if (action == "PROP") texte = "Propriété " + ligne;
            else if (action == "RELI") texte = "Religion " + ligne;
            else if (action == "RESI") texte = "Résidence";
            else if (action == "RETI") texte = "Retraite";
            else if (action == "SSN") texte = "Numéro sécurité sociale " + ligne;
            else if (action == "TITL") texte = "Titre " + ligne;
            else if (action == "SLGC") texte = "Scellemt à ses parents LDS";
            else if (action == "WILL") texte = "Testament";
            else if (action == "_ELEC") texte = "Élection";
            else if (action == "_MILT") texte = "Service militaire"; // GRAMPS
            else if (action == "_MDCL") texte = "Information médicale";
            else if (action == "BAPL") texte = "Baptême église LDS";
            else if (action == "EVEN") texte = ligne;
            else if (action == "FACT") texte = ligne;
            else texte = action;
            return texte;
        }
        private static string ConvertirMoisEnChiffre(string mois)
        {

            string m = mois.ToUpper();
            if (m == "JAN")
            {
                m = "01";
            }
            if (m == "FEB")
            {
                m = "02";
            }
            if (m == "MAR")
            {
                m = "03";
            }
            if (m == "APR")
            {
                m = "04";
            }
            if (m == "MAY")
            {
                m = "05";
            }
            if (m == "JUN")
            {
                m = "06";
            }
            if (m == "JUL")
            {
                m = "07";
            }
            if (m == "AUG")
            {
                m = "08";
            }
            if (m == "SEP")
            {
                m = "09";
            }
            if (m == "OCT")
            {
                m = "10";
            }
            if (m == "NOV")
            {
                m = "11";
            }
            if (m == "DEC")
            {
                m = "12";
            }
            return m;
        }
        private static string ConvertirMoisEnLong(string mois)
        {
            string m = mois.ToUpper();
            if (m == "01")
            {
                m = "janvier";
            }
            if (m == "02")
            {
                m = "février";
            }
            if (m == "03")
            {
                m = "mars";
            }
            if (m == "04")
            {
                m = "avril";
            }
            if (m == "05")
            {
                m = "mai";
            }
            if (m == "06")
            {
                m = "juin";
            }
            if (m == "07")
            {
                m = "Juillet";
            }
            if (m == "08")
            {
                m = "août";
            }
            if (m == "09")
            {
                m = "septembre";
            }
            if (m == "10")
            {
                m = "octobre";
            }
            if (m == "11")
            {
                m = "novembre";
            }
            if (m == "12")
            {
                m = "décembre";
            }
            return m;
        }
        public static string Convertir_Sujet(string sujet)
        {
            string texte;
            if (sujet.ToUpper() == "CHIL") texte = "Enfant";
            else if (sujet.ToUpper() == "HUSB") texte = "Conjoint";
            else if (sujet.ToUpper() == "WIFE") texte = "Conjointe";
            else if (sujet.ToUpper() == "MOTH") texte = "Mère";
            else if (sujet.ToUpper() == "FATH") texte = "Père";
            else if (sujet.ToUpper() == "SPOU") texte = "Conjoint(e)";
            else texte = sujet;
            return texte;
        }
        public static bool SiBaliseAttributeFamille(string balise)
        {
             if (balise == "1 FACT") return true; // trouver dans GRAMPS
            return false;
        }
        public static bool SiBaliseAttributeIndividu(string balise)
        {
            // p.33

            if (balise == "1 CAST") return true;
            if (balise == "1 DSCR") return true;
            if (balise == "1 EDUC") return true;
            if (balise == "1 IDNO") return true;
            if (balise == "1 NATI") return true;
            if (balise == "1 NCHI") return true;
            if (balise == "1 NMR") return true;
            if (balise == "1 OCCU") return true;
            if (balise == "1 PROP") return true;
            if (balise == "1 RELI") return true;
            if (balise == "1 RESI") return true;
            if (balise == "1 SSN") return true;
            if (balise == "1 TITL") return true;
            if (balise == "1 FACT") return true;
            return false;
        }
        public static bool SiBaliseEvenementFamille(string balise)
        {
            // p.32
            if (balise == "1 ANUL" || balise == "1 CENS" || balise == "1 DIV" || balise == "1 DIVF") return true;
            if (balise == "1 ENGA" || balise == "1 MARB" || balise == "1 MARC") return true;
            if (balise == "1 MARR") return true;
            if (balise == "1 MARL" || balise == "1 MARS") return true;
            if (balise == "1 RESI") return true;
            if (balise == "1 EVEN") return true;

            // ancestrologie
            if (balise == "_ANCES_ORDRE") return true;
            if (balise == "_ANCES_XINSEE") return true;
            // GRAMPS
            return false;
        }
        public static bool SiBaliseEvenementIndividu(string balise)
        {
            // p.34
            balise = balise.ToUpper();
            if (balise == "1 BIRT" || balise == "1 CHR") return true;
            if (balise == "1 DEAT") return true;
            if (balise == "1 BURI" || balise == "1 CREM") return true;
            if (balise == "1 ADOP") return true;
            if (balise == "1 BAPM" || balise == "1 BARM" || balise == "1 BASM" || balise == "1 BLES") return true;
            if (balise == "1 CHRA" || balise == "1 CONF" || balise == "1 FCOM" || balise == "1 ORDN") return true;
            if (balise == "1 BAPM" || balise == "1 BARM" || balise == "1 BASM" || balise == "1 BLES") return true;
            if (balise == "1 NATU" || balise == "1 EMIG" || balise == "1 IMMI") return true;
            if (balise == "1 CENS" || balise == "1 PROB" || balise == "1 WILL") return true;
            if (balise == "1 GRAD" || balise == "1 RETI") return true;
            if (balise == "1 EVEN") return true;
            if (balise == "1 _MDCL") return true; // oem
            if (balise == "1 _MILT") return true; // oem
            
            if (balise == "1 _ELEC") return true; // GRAMPS
            return false;
        }
        public static bool SiBaliseOrdinanceIndividu(string balise)
        {
            if (balise == "1 BAPL") return true; //allowed in GEDCOM 5.5.1
            if (balise == "1 CONL") return true; //allowed in GEDCOM 5.5.1
            if (balise == "1 ENDL") return true; //allowed in GEDCOM 5.5.1
            if (balise == "1 SLGC") return true; //allowed in GEDCOM 5.5.1
            return false;
        }
        public static string SiBaliseZero(string s)
        {
            if (s == "") return "";
            if (s[0] != '0') return "";
            if (s.Substring(2) == "HEAD") return "HEAD";
            int p1 = s.IndexOf("@");
            int p2 = s.IndexOf("@", s.IndexOf('@') + 1);
            if (p2 == -1) return "";
            if (s.Length < p2 + 3) return "";
            if ((p1 < 2 && p2 < 3) || (p1 > p2))
            {
                return "";
            }
            if (s.Substring(p2 + 2, 3) == "FAM") return "FAM";
            s = s.Substring(p2 + 2, 4);
            return s;
        }
        public static void Voir_message(string message, string raison, string erreur)
        {
            DialogResult reponse;
            string message_log = String.Format("Erreur {0,-7 } {1}\r\n{2,-15 }{3}", erreur, message, "",raison );
            reponse = MessageBox.Show("Erreur " + erreur + ". " + message + "\r\n" + raison +
                "\r\n\r\n" +
                "L'erreur a été enregisté dans le fichier " + GH.Properties.Settings.Default.DossierHTML + "\\erreur.txt\r\n",
                "Erreur " + erreur + " problème ?",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning); ;
            GEDCOMClass.Erreur_log(message_log);
            if (reponse == System.Windows.Forms.DialogResult.Cancel)
                GH.GH.annuler = true;
        }
        public static void ZXCV(string message = "", [CallerFilePath] string code = "", [CallerLineNumber] int ligneCode = 0, [CallerMemberName] string fonction = null)
        {
            if (!GH.Properties.Settings.Default.deboguer || GH.Properties.Settings.Default.DossierHTML == "") return;
            System.Windows.Forms.Button Btn_deboguer = Application.OpenForms["GH"].Controls["Btn_deboguer"] as System.Windows.Forms.Button;
            code = Path.GetFileName(code);
            code = code[0].ToString().ToUpper();
            string fichier = GH.Properties.Settings.Default.DossierHTML + "\\deboguer.txt";
            try
            {
                using (StreamWriter ligne = File.AppendText(fichier))
                {
                    if (!LogInfoGEDCOM && Info_HEADER.N2_SOUR_NAME != "")
                    {
                        Btn_deboguer.Visible = true;
                        ligne.WriteLine(DateTime.Now);
                        ligne.WriteLine("*** Deboguer **********************************************************");
                        ligne.WriteLine("Nom: " + Info_HEADER.N2_SOUR_NAME);
                        ligne.WriteLine("Version: " + Info_HEADER.N2_SOUR_VERS);
                        ligne.WriteLine("Date: " + Info_HEADER.N1_DATE + " " + Info_HEADER.N2_DATE_TIME);
                        ligne.WriteLine("Copyright: " + Info_HEADER.N1_COPR);
                        ligne.WriteLine("Version: " + Info_HEADER.N2_GEDC_VERS);
                        ligne.WriteLine("Code charactère: " + Info_HEADER.N1_CHAR);
                        ligne.WriteLine("Langue: " + Info_HEADER.N1_LANG);
                        ligne.WriteLine("Fichier sur le disque: " + Info_HEADER.Nom_fichier_disque);
                        ligne.WriteLine("**********************************************************************");
                        LogInfoGEDCOM = true;
                    }
                    string s = String.Format("{0} {1,5} {2,-20:G} ►{3}◄", code, ligneCode, fonction, message);
                    ligne.WriteLine(s);
                }
            }
            catch (Exception msg)
            {
                {
                    MessageBox.Show("Debub Actif dans GEDCOM.\r\n\r\n" + code + " " + ligneCode + " " + 
                        fonction + "-> " + message + "\r\n\r\n" + msg.Message, erreur + " problème ?",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                }
            }
        }
    }
}
