
/* 
R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name +" code " + callerLineNumber + "</b> GEDCOM="+ ligne + "&nbsp;&nbsp;niveau=" + niveau);
, [CallerLineNumber] int callerLineNumber = 0

  
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

Pour connaitre la méthode qui a appelé une metode


*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using Routine;

namespace GEDCOM
{
    public static class GEDCOMClass
    {
        private static int numero_ID = 0;
        public static int ligne = 0;
        private static int conteur_MULTIMEDIA_LINK = 0;
        private static readonly Random hazard = new Random();
        public static List<string> dataGEDCOM = new List<string>();
 
        public class ADDRESS_STRUCTURE
        {
            // GEDCOM53.pdf             p.19
            // GEDCOM54.pdf             p.27
            // 5.5_LDS_1996-01-02.pdf   p.29
            // 5.5.1_LDS_2019-11-15.pdf p.31
            // 5.5.5_Annotations_TJ.pdf p.64
            //                                                      ADDRESS_STRUCTURE:=
            public string N0_ADDR;                              //  n ADDR <ADDRESS_LINE>               {1:1}
                                                                //                                          V5.4    p.27
                                                                //                                          V5.5    p.37
                                                                //                                          V5.5.1  p.41
                                                                //                                          V5.5.5 p.97
                                                                //      +1 CONT <ADDRESS_LINE>          {0:3}
                                                                //                                          V5.3 
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
                                                                //                                          V5.4    p.34
                                                                //                                          V5.5    p.37
                                                                //                                          V5.5.1  p.41
                                                                //                                          v5.5.5  p.76
            public List<string> N1_PHON_liste;                  //      +1 PHON <PHONE_NUMBER>          {0:3}
            public List<string> N1_FAX_liste;
            public List<string> N1_EMAIL_liste;
            public List<string> N1_WWW_liste;

            public List<string> N1_NOTE_STRUCTURE_liste_ID;     // GRAMPS
        }
        public class ASSOCIATION_STRUCTURE
        {
            // 5.3 5.4 pas de structure comme tel.
            // 5.5_LDS_1996-01-02.pdf   p.29
            // 5.5.1_LDS_2019-11-15.pdf p.31
            // 5.5.5_Annotations_TJ.pdf p.65
            //  ASSOCIATION_STRUCTURE:=
            public string N0_ASSO;                          //      n ASSO @<XREF:INDI>@                  {1:1}
                                                            //      n ASSO @XREF:ANY@                     {0:M} V5.3
                                                            //      n ASSO @<XREF:INDI>@                  {0:M} V5.4
                                                            //                                                  V5.5    p.29
                                                            //                                                  V5.5.1  p.25
                                                            //                                                  V5.5.5  p.108

            public string N1_TYPE;                          //          +1 TYPE <RECORD_TYPE>
                                                            //          +1 TYPE<ASSOCIATION_DESCRIPTOR>         {0:1} V5.3
                                                            //                                                  V5.3    p.26
                                                            //                                                  V5.5    p.51
            public string N1_RELA;                          //          +1 RELA <RELATION_IS_DESCRIPTOR>    {1:1}
                                                            //                                                  V5.4    p.45
                                                            //                                                  V5.5    p.52
                                                            //                                                  V5.5.1  p.60
                                                            //                                                  V5.5.5  p.104

            public List<string> N1_SOUR_citation_liste_ID;  //          +1 <<SOURCE_CITATION>>              {0:M}
                                                            //                                                  V5.3    p.23
                                                            //                                                  V5.4    p.31
                                                            //                                                  V5.5    p.34
                                                            //                                                  V5.5.1  p.39
                                                            //                                                  V5.5.5  p.73

            public List<string> N1_SOUR_source_liste_ID;    //          +1 généré par l'appication
            public List<string> N1_NOTE_STRUCTURE_liste_ID; //          +1 <<NOTE_STRUCTURE>>               {0:M}
                                                            //                                                  V5.3    p.21
                                                            //                                                  V5.4    p.30
                                                            //                                                  V5.5    p.33
                                                            //                                                  V5.5.1  p.37
                                                            //                                                  v5.5.5  p.71
        }

        public class CHANGE_DATE
        {
            // GEDCOM53.pdf             p.19
            // GEDCOM54.pdf             p.27
            // 5.5_LDS_1996-01-02.pdf   p.29
            // 5.5.1_LDS_2019-11-15.pdf p.31
            // 5.5.5_Annotations_TJ.pdf p.66
            //                                                  CHANGE_DATE:=
            //                                                  n CHAN                                      {1:1}
            //                                                                                                  V5.5
            //                                                                                                  V5.5.1
            //                                                                                                  V5.5.5
            public string N1_CHAN_DATE;                     //      +1 DATE <DATE_EXACT>                    {1:1}
                                                            //                                                  V5.3
                                                            //                                                  V5.4    p.35
                                                            //                                                  V5.5    p.39
                                                            //                                                  V5.5.1  p.44
                                                            //                                                  V5.5.5  p.83
            public string N2_CHAN_DATE_TIME;                //          +2 TIME <TIME_VALUE>                {0:1}
                                                            //                                                  V5.3
                                                            //                                                  V5.4    p.47
                                                            //                                                  V5.5    p.55
                                                            //                                                  V5.5.1  p.63
                                                            //                                                  V5.5.5  p.107
            public List<string> N1_CHAN_NOTE_STRUCTURE_ID_liste;//          +1 <<NOTE_STRUCTURE>>           {0:M}
                                                                //                                                  V5.3    p.21
                                                                //                                                  V5.4    p.30
                                                                //                                                  V5.5    p.33
                                                                //                                                  V5.5.1  p.37
                                                                //                                                  V5.5.5  p.71
        }
        public class CHILD_TO_FAMILY_LINK
        {
            // GEDCOM54.pdf             p.27
            // 5.5_LDS_1996-01-02.pdf   p.29
            // 5.5.1_LDS_2019-11-15.pdf p.31
            // 5.5.5_Annotations_TJ.pdf p.67
            //  CHILD_TO_FAMILY_LINK:=
            public string N0_FAMC;                          //      n FAMC @<XREF:FAM>@                     {1:1}
                                                            //                                                  V5.4
                                                            //                                                  V5.5
                                                            //                                                  V5.5.1  p.24
                                                            //                                                  V5.5.5  P.108
            public bool N1_adop;                            // True = adopter false = pas adopter
            public string N2_ADOP_TYPE;                     //              +2 <CHILD_FAMILY_EVENT_DESCRIPTOR>{0:1}
                                                            //                                                  V5.3    p.19
            public string N2_ADOP_AGE;                      //              +2 AGE <AGE_VALUE>              {0:1}
                                                            //                                                  V5.3    p.26
            public string N2_ADOP_DATE;                     //  n DATE <DATE_VALUE>                         {0:1}
                                                            //                                                  V5.3    p.28
            public PLACE_STRUCTURE N2_ADOP_PLAC;            //  n <<PLACE_STRUCTURE>>                       {0:1}
                                                            //                                                  V5.3    p.21
            public List<string> N2_ADOP_SOUR_citation_liste_ID;//n <<SOURCE_CITATION>>                      {0:M}
                                                               //                                                  V5.3    p.23
            public List<string> N2_ADOP_NOTE_STRUCTURE_liste_ID;//      +1 <<NOTE_STRUCTURE>>               {0:M}
                                                                //                                                  V5.3    p.21
            public List<string> N2_ADOP_SOUR_source_liste_ID;
            public bool N1_slgc;                            // true = Ordinance false = pas ordinace
            public string N2_SLGC_TYPE;                     //          +1 TYPE <LDS_CHILD_SEALING_DESCRIPTOR>{0:1}
                                                            //                                                  V5.3    p.31
            public string N2_SLGC_DATE;                     //  n DATE <DATE_VALUE>                         {0:1}
                                                            //                                                  V5.3    p.28
            public string N2_SLGC_TEMP;                     //  n DATE <DATE_VALUE>                         {5:5}
                                                            //                                                  V5.3    p.39
            public List<EVEN_ATTRIBUTE_STRUCTURE> N2_FANC_CHILD_FAMILY_EVEN_V53; //+2 <<CHILD_FAMILY_EVEN>>  {0:M}
                                                                                 //                                                  V5.3    p.19
            public string N1_PEDI;                          //          +1 PEDI <PEDIGREE_LINKAGE_TYPE>     {0:1}
                                                            //                                                  V5.5    p.50
                                                            //                                                  V5.5.1  p.57
                                                            //                                                  V5.5.5  p.99
            public string N1_STAT;                          //          +1 STAT <CHILD_LINKAGE_STATUS>      {0:1}
                                                            //                                                  V5.5.1  p.44
            public List<string> N1_NOTE_STRUCTURE_liste_ID; //          +1 <<NOTE_STRUCTURE>>               {0:M}
                                                            //                                                  V5.3    p.21
                                                            //                                                  V5.4    p.30
                                                            //                                                  V5.5    p.33
                                                            //                                                  V5.5.1  p.37
                                                            //                                                  v5.5.5  p.71
        }
        public class EVEN_RECORD_53 // V5.3 SEULEMENT
        {
            // GEDCOM53.pdf             p.18

            public string N0_ID;//n @XREF:EVEN@ EVEN
            public CHANGE_DATE N1_CHAN;                             //      +1 <<CHANGE_DATE>> {0:M}
            public string N1_EVEN;                                  //      +1 <EVENT_TAG> {1:1}
            public string N2_EVEN_TYPE;                             //          +2 TYPE <EVENT_DESCRIPTOR> {0:1}
            public string N2_EVEN_DATE;                             //          +2 DATE <DATE_VALUE> {0:1}
            public string DATE_trier;                               // date au format YYYYMMDD
            public string N2_EVEN_SITE;                             //
            public PLACE_STRUCTURE N2_EVEN_PLAC;                    //          +2 <<PLACE_STRUCTURE>> {0:1}
            public string N2_EVEN_PERI;                             //          +2 PERI <TIME_PERIOD> {0:M}
            public string N2_EVEN_RELI;                             //          +2 RELI <RELIGIOUS_AFFILIATION> {0:1}
            public List<string> N2_EVEN_AUDIO_liste;                //          +2 AUDIO <ESCAPE_TO_AUXILLARY_PROCESSING>        {0:1} p.21
            public List<string> N2_EVEN_PHOTO_liste;                //          +2 PHOTO <ESCAPE_TO_AUXILLARY_PROCESSING>        {0:1} p.21
            public List<string> N2_EVEN_VIDEO_liste;                //          +2 VIDEO <ESCAPE_TO_AUXILLARY_PROCESSING>        {0:1} p.21
            public List<string> MULTIMEDIA_LINK_liste_ID;

            public List<TEXT_STRUCTURE> N2_EVEN_TEXT_liste;         //          +2 <<TEXT_STRUCTURE>> {0:1}
            public List<string> N2_EVEN_SOUR_citation_liste_ID;     //          +2 <<SOURCE_CITATION>>                       {0:M}
                                                                    //                                                  V5.3    p.23
            public List<string> N2_EVEN_SOUR_source_liste_ID;
            public List<string> N2_EVEN_NOTE_STRUCTURE_liste_ID;    //  n <<NOTE_STRUCTURE>>                        {0:M}
                                                                    //                                                  V5.5.5  p.71
            public string N2_EVEN_ROLE;                             //          +2 <ROLE_TAG> {0:M}
            public string N3_EVEN_ROLE_TYPE;                        //              +3 TYPE <ROLE_DESCRIPTOR> {0:1}
            public INDIVIDUAL_53 N3_EVEN_ROLE_INDIVIDUAL;           //              +3 <<INDIVIDUAL>> {0:1}
            public List<ASSOCIATION_STRUCTURE> N3_EVEN_ROLE_ASSO_liste;//              +3 ASSO @XREF:INDI@ {0:M}
            //public string N4_EVEN_ROLE_ASSO_TYPE;                 //                  +4 TYPE <ASSOCIATION_DESCRIPTOR> {1:1}
            public string N3_EVEN_ROLE_RELATIONSHIP_tag;           //              +3 <RELATIONSHIP_ROLE_TAG> [NULL | @XREF:INDI@ ] {0:M}
            public string N3_EVEN_ROLE_RELATIONSHIP_ID;             //              separer le tag et ID de la ligne précédente
            public string N4_EVEN_ROLE_RELATIONSHIP_TYPE;           //                  +4 TYPE <ROLE_DESCRIPTOR> {0:1}
            public INDIVIDUAL_53 N4_EVEN_ROLE_RELATIONSHIP_INDIVIDUAL;//                  +4 <<INDIVIDUAL>> {0:1}
            
            public List<Ligne_perdue> ligne_perdue_liste;
        }

        public class EVEN_ATTRIBUTE_STRUCTURE
        {
            // mixe
            //      INDIVIDUAL_DETAIL
            //                      5.5.1_LDS_2019-11-15 p.34
            //                      5.5.5_Annotations_TJ p.69
            //      INDIVIDUAL_EVENT_STRUCTURE
            //                      GEDCOM53.pdf         p.20
            //                      GEDCOM54.pdf         p.28
            //                      5.5.1_LDS_2019-11-15 p.34
            //                      5.5.5_Annotations_TJ p.69
            //      INDIVIDUAL_ATTRIBUTE_STRUCTURE
            //                      GEDCOM54.pdf         p.28
            //      FAMILY_EVENT_DETAIL
            //                      GEDCOM54.pdf         p.27
            //                      5.5.1_LDS_2019-11-15 p.32
            //                      5.5.5_Annotations_TJ p.67
            //      EVENT_DETAIL    
            //                      GEDCOM54.pdf         p.27
            //                      5.5_LDS_1996-01-02.pdf   p.29
            //                      5.5.1_LDS_2019-11-15.pdf p.32
            //                      5.5.5_Annotations_TJ.pdf p.67
            public string N1_EVEN;                          //  n                                           {1:1}
                                                            //                                                  V5.3
                                                            //                                                  V5.4
                                                            //                                                  V5.5
                                                            //                                                  V5.5.1
                                                            //                                                  V5.5.5
            public string N1_EVEN_texte;                    // récupaire le texte avec balise.

            // EVEN_DETAIL:=
            public List<string> N2_TYPE_liste;              //  n TYPE <EVENT_OR_FACT_CLASSIFICATION>       {0:1}
                                                            //                                                  V5.3    p.30
                                                            //                                                  V5.3    p.38
                                                            //                                                  V5.5    p.43
                                                            //                                                  V5.5.1  p.49
                                                            //                                                  V5.5.5  p.91

            public string N2_DATE;                          //  n DATE <DATE_VALUE>                         {0:1}
                                                            //                                                  V5.3    p.28
                                                            //                                                  V5.5    p.42 p.41
                                                            //                                                  V5.5.1  p.47 p.46
                                                            //                                                  V5.5.5  p.87
            public string DATE_trier;                       // date au format YYYYMMDD
            public string N2_TEMP;                          //      +1 TEMP <TEMPLE_VALUE>                  {0:1}
                                                            //                                                  V5.3
            public string N3_DATE_TIME;                     //  n TIME                                        Heridis
            public PLACE_STRUCTURE N2_PLAC;                 //  n <<PLACE_STRUCTURE>>                       {0:1}
                                                            //                                                  V5.3    p.21
                                                            //                                                  V5.5    p.34
                                                            //                                                  V5.5.1  p.38
                                                            //                                                  V5.5.5  p.72
            public string N2_SITE;                          //  n SITE <SITE_NAME>                          {0:1}
                                                            //                                                  V5.3    p.37
            public List<ADDRESS_STRUCTURE> N2_ADDR_liste;   //  n <<ADDRESS_STRUCTURE>>                     {0:1}
                                                            //                                                  V5.4    p.27
                                                            //                                                  V5.5    p.29
                                                            //                                                  V5.5.1  p.31
                                                            //                                                  V5.5.5  p.64
            public List<string> N2_PHON_liste;              //  n PHON <PHONE_NUMBER>                       {0:3}
                                                            //                                                  V5.4    p.43
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
            public string N2_QUAY;                          //  n QUAY <QUALITY_OF_DATA>                    {0:1}
                                                            //                                                  V5.3    p.19
            public string N2_AGNC;                          //  n AGNC <RESPONSIBLE_AGENCY>                 {0:1}
                                                            //                                          V5.4    p.45
                                                            //                                                  V5.5    p.34
                                                            //                                                  V5.5.1  p.60
                                                            //                                                  V5.5.5  p.104
            public string N2_RELI;                          //  n RELI <RELIGIOUS_AFFILIATION>              {0:1}
                                                            //                                                  V5.3    p.36
                                                            //                                                  V5.5.1  p.60
                                                            //                                                  V5.5.5  p.104
            public string N2_CAUS;                          //  n CAUS <CAUSE_OF_EVENT>                     {0:1} 
                                                            //                          CAUSE_OF_DEATH:=        v5.3    p.27
                                                            //                                                  V5.4    p.35
                                                            //                                                  V5.5    p.38
                                                            //                                                  V5.5.1  p.78
                                                            //                                                  V5.5.5  p.72
            public string N2_RESN;                          //  n RESN <RESTRICTION_NOTICE>                 {0:1}
                                                            //                                                  V5.5.1  p.60
            public List<TEXT_STRUCTURE> N2_TEXT_liste;      //  +2 <<TEXT_STRUCTURE>>                       {0:1}
                                                            //                                                  p.5.3   p.24
            public List<string> N2_NOTE_STRUCTURE_liste_ID;           //  n <<NOTE_STRUCTURE>>                        {0:M}
                                                                      //                                                  V5.3    p.21
                                                                      //                                                  V5.4    p.30
                                                                      //                                                  V5.5    p.33
                                                                      //                                                  V5.5.1  p.37
                                                                      //                                                  V5.5.5  p.71
            public List<string> N2_SOUR_citation_liste_ID;  //  n <<SOURCE_CITATION>>                       {0:M}
                                                            //                                                  V5.3    p.23
                                                            //                                                  V5.4    p.31
                                                            //                                                  V5.5    p.34
                                                            //                                                  V5.5.1  p.39
                                                            //                                                  V5.5.5  p.73
            public List<string> N2_SOUR_source_liste_ID;
            public List<string> MULTIMEDIA_LINK_liste_ID;

            // FAMILY_EVEN_DETAIL
            //  n HUSB {0:1}
            public string N3_HUSB_AGE;                      //      +1 AGE <AGE_AT_EVENT>                   {1:1}
                                                            //                                                  V5.4    p.34
                                                            //                                                  V5.5    p.37
                                                            //                                                  V5.5.1  p.42
                                                            //                                                  V5.5.5  p.77
            public string N3_WIFE_AGE;                      //      +1 AGE <AGE_AT_EVENT>                   {1:1}
                                                            //                                                  V5.4    p.34
                                                            //                                                  V5.5    p.37
                                                            //                                                  V5.5.1  p.42
                                                            //                                                  v5.5.5  p.77
                                                            // INDIVIDUAL_EVENT_DETAIL:=
            public string N2_AGE;                           //  n AGE <AGE_AT_EVENT>
                                                            //                                                  V5.3    p.26
                                                            //                                                  V5.4    p.34
                                                            //                                                  V5.5    p.37
                                                            //                                                  V5.5.1  p.42
                                                            //                                                  V5.5.5  p.77
            public string N2_MSTAT;                         //  n1 MSTAT <MARITAL_STATUS>                   {0:1}
                                                            //                                                  V5.3    p.32
                                                            // INDIVIDUAL_EVENT_STRUCTURE:=
            public string N2_FAMC;                          //      +1 FAMC @<XREF:FAM>@                    {0:1}
                                                            //                                                  V5.4
                                                            //                                                  V5.5    p.55
                                                            //                                                  V5.5.1  p.24
                                                            //                                                  V5.5.5  p.108

            public string N2_FAMC_ADOP;                     //         +2 ADOP<ADOPTED_BY_WHICH_PARENT>     {0:1}
                                                            //                                                  V5.4    p.34
                                                            //                                                  V5.5    p.37
                                                            //                                                  V5.5.1  p.42
                                                            //                                                  V5.5.5  p.76


            public CHANGE_DATE N2_CHAN;                     //          +1 <<CHANGE_DATE>>                  {0:M}
                                                            //                                                  V5.3    p.19


            //public string titre;
            public string description;

            public string N2__ANCES_ORDRE;                  //  n Ancestrologie
            public string N2__ANCES_XINSEE;                 //  n Ancestrologie
            public string N2__FNA;                          //  n Heridis Etat des recherches d'un événement
            public List<Ligne_perdue> ligne_perdue_liste;
        }
        public class EVEN_STRUCTURE_53
        {
            // GEDCOM53.pdf             p.20
            public string N0_EVEN;                                      //  n <EVENT_TAG> {1:1}	
            public string N1_TYPE;                                      //      +1 TYPE<EVENT_DESCRIPTOR> {0:M}
            public string N1_DATE;                                      //      +1 DATE<DATE_VALUE> {0:1}
            public PLACE_STRUCTURE N1_PLAC;                             //      +1 <<PLACE_STRUCTURE>> {0:1}
            public string N2_PLAC_CEME;                                 //          +2 << BURIAL_STRUCTURE >> { 0:1}
                                                                        //                                      p.19
            public string N3_PLAC_CEME_PLOT;                            //              +1 PLOT <BURIAL_PLOT_ID> {0:1}
                                                                        //                                      p.19
            public string N1_AGE;                                       //      +1 AGE<AGE_VALUE> { 0:1}
            public string N1_MSTAT;                                     //      +1 MSTAT<MARITAL_STATUS> { 0:1}
            public string N1_CAUS;                                      //      +1 CAUS <CAUSE_OF_DEATH> {0:1}
            public string N1_RELI;                                      //      +1 RELI<RELIGIOUS_AFFILIATION> { 0:1}
            public string N1_AGNC;                                      //      +1 AGNC<GOVERNMENT_AGENCY> { 0:1}
            public List<TEXT_STRUCTURE> N1_TEXT_liste;                  //+1 << TEXT_STRUCTURE >> { 0:1}
            public List<string> N1_SOUR_citation_liste_ID;              //    +1 <<SOURCE_CITATION>>            {0:M}
            public List<string> N1_SOUR_source_liste_ID;
            public List<string> N1_NOTE_STRUCTURE_liste_ID;             //    +1 <<NOTE_STRUCTURE>>             {0:M}
            public CHANGE_DATE N1_CHAN;                                 //    +1 <<CHANGE_DATE>>                {0:1}
            public List<Ligne_perdue> ligne_perdue_liste;
        }
        public class FAM_RECORD
        {
            // GEDCOM54.pdf             p.16
            // GEDCOM54.pdf             p.22
            // 5.5_LDS_1996-01-02.pdf   p29
            // 5.5.1_LDS_2019-11-15.pdf p.24
            // 5.5.5_Annotations_TJ.pdf p.58 FAM_GROUP_RECORD
            //                                                          FAM_RECORD:=
            //                                                          FAMILY_RECORD:= V5.3
            public string N0_ID;                                        //    n @<XREF:FAM>@ FAM                {1:1}
                                                                        //                                          V5.3
                                                                        //                                          V5.4
                                                                        //                                          V5.5
                                                                        //                                          V5.5.1
                                                                        //                                          v5.5.5
            public string N1_RESN;                                      //    +1 RESN <RESTRICTION_NOTICE>      {0:1) 
                                                                        //                                          V5.5.1  p.60
            public List<EVEN_ATTRIBUTE_STRUCTURE> N1_EVEN_Liste;        //    +1 <<FAMILY_EVENT_STRUCTURE>>     {0:M}
                                                                        //                                          V5.4    p.27
                                                                        //                                          V5.5    p.30
                                                                        //                                          V5.5.1  p.32
                                                                        //                                          v5.5.5  p.67
            public string N1_HUSB;                                      //    +1 HUSB @<XREF:INDI>@             {0:1}
                                                                        //                                          V5.3
                                                                        //                                          V5.4    p.48
                                                                        //                                          V5.5    p.55
                                                                        //                                          V5.5.1  p.25
                                                                        //                                          v5.5.5  p.108
            public string N1_WIFE;                                      //    +1 WIFE @<XREF:INDI>@             {0:1}
                                                                        //                                          V5.3
                                                                        //                                          V5.4    p.48
                                                                        //                                          V5.5    p.55
                                                                        //                                          V5.5.1  p.25
                                                                        //                                          v5.5.5  p.108
            public List<string> N1_CHIL_liste_ID;                       //    +1 CHIL @<XREF:INDI>@             {0:M}
                                                                        //                                          V5.3
                                                                        //                                          V5.4    p.48
                                                                        //                                          V5.5    p.55
                                                                        //                                          V5.5.1  p.25
                                                                        //                                          v5.5.5  p.108
            public string N1_NCHI;                                      //    +1 NCHI <COUNT_OF_CHILDREN>       {0:1}

            //                                          V5.3    p.27
            //                                          V5.4    p.35
            //                                          V5.5    p.39
            //                                          V5.5.1  p.44
            //                                          v5.5.5  p.79
            public List<string> N1_SUBM_liste_ID;                       //    +1 SUBM @<XREF:SUBM>@             {0:M}
                                                                        //                                          V5.4
                                                                        //                                          V5.5    p.55
                                                                        //                                          V5.5.1  p.28
                                                                        //                                          v5.5.5  p.79
            public LDS_SPOUSE_SEALING N1_SLGS;                          //    +1 <<LDS_SPOUSE_SEALING>>         {0:M}
                                                                        //                                          V5.3    p.21
                                                                        //                                          V5.4    p.30
                                                                        //                                          V5.5    p.33
                                                                        //                                          V5.5.1  p.36
            public List<USER_REFERENCE_NUMBER> N1_REFN_liste;           //    +1 REFN <USER_REFERENCE_NUMBER>   {0:M}
                                                                        //                                          V5.3
                                                                        //                                          V5.4    p.47
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
                                                                        //                                          V5.3    p.19
                                                                        //                                          V5.4    p.27
                                                                        //                                          V5.5    p.29
                                                                        //                                          V5.5.1  p.31
                                                                        //                                          v5.5.5  p.66
            public List<string> N1_NOTE_STRUCTURE_liste_ID;                       //    +1 <<NOTE_STRUCTURE>>             {0:M}
                                                                                  //                                          V5.3    p.21
                                                                                  //                                          V5.4    p.30
                                                                                  //                                          V5.5    p.33
                                                                                  //                                          V5.5.1  p.37
                                                                                  //                                          v5.5.5  p.71
            public List<string> N1_SOUR_citation_liste_ID;              //    +1 <<SOURCE_CITATION>>            {0:M}
                                                                        //                                          V5.3    p.23
                                                                        //                                          V5.4    p.31
                                                                        //                                          V5.5    p.34
                                                                        //                                          V5.5.1  p.39
                                                                        //                                          v5.5.5  p.73
            public List<string> N1_SOUR_source_liste_ID;
            public List<string> MULTIMEDIA_LINK_liste_ID;
            public List<ASSOCIATION_STRUCTURE> N1_ASSO_liste;           //    +1 <<ASSOCIATION_STRUCTURE>>       {0:M}
                                                                        //                                           V5.3    p.48
            public List<EVEN_ATTRIBUTE_STRUCTURE> N1_ATTRIBUTE_liste;  //    pour GRAMPS
            public string N1_TYPU;                                      //    +1 Type d'union                                      Ancestrologie
            public string N1__UST;                                      //    +1 Type d'union                                      Heridis 
            public List<Ligne_perdue> ligne_perdue_liste;
        }
        public class INDIVIDUAL_53// 5.3 seulement
        {
            // GEDCOM53.pdf             p.20
            public List<NAME_STRUCTURE_53> N0_NAME_liste;                       // n <<NAME_STRUCTURE>> {1:M}
            public string N0_TITL;                                              // n TITL <INDI_TITLE> {0:M} p.31
            public string N0_SEX;                                               // n SEX <SEX_VALUE> {0:1}  M = Male  F = Female
            public List<EVEN_STRUCTURE_53> N0_EVEN_liste;                       // n <<EVENT_STRUCTURE>> {0:M}
            public List<ADDRESS_STRUCTURE> N0_ADDR_liste;                       // n <<ADDRESS_STRUCTURE>> {0:M}
            public string N0_RELI;                                              // n RELI <RELIGIOUS_AFFILIATION> {0:M} p,36
            public string N0_NAMR;                                              // n NAMR <RELIGIOUS_NAME> {0:M}
            public string N1_NAMR_RELI;                                         //      +1 RELI <RELIGIOUS_AFFILIATION> {0:1} p.36
            public string N0_EDUC;                                              // n EDUC <SCHOLASTIC_ACHIEVEMENT> {0:M}
            public string N0_OCCU;                                              // n OCCU <OCCUPATION> {0:M}
            public string N0_SSN;                                               // n SSN <SOCIAL_SECURITY_NUMBER> {0:M}
            public string N0_IDNO;                                              // n IDNO <NATIONAL_ID_NUMBER> {0:M}
            public string N1_IDNO_TYPE;                                         //      +1 TYPE <TYPE_OF> {1:1}
            public string N0_PROP;                                              // n PROP <POSSESSIONS> {0:M} p.35
            public string N0_DSCR;                                              // n DSCR <PHYSICAL_DESCRIPTION> {0:M} p.35
                                                                                // +1 CONT <PHYSICAL_DESCRIPTION> {0:M}
            public string N0_SIGN;                                              // n SIGN <SIGNATURE_INFO> {0:M} p.37
            public string N0_NMR;                                               // n NMR <COUNT_OF_MARRIAGES> {0:M}
            public string N0_NCHI;                                              // n NCHI <COUNT_OF_CHILDREN> {0:M}
            public string N0_NATI;                                              // n NATI <NATIONALITY> {0:M}
            public string N0_CAST;                                              // n CAST <CASTE_NAME> {0:M}
            public List<Ligne_perdue> ligne_perdue_liste;
        }
        public class INDIVIDUAL_RECORD
        {
            // GEDCOM53.pdf             p.17  plus INDIVIDUAL:= p.20
            // GEDCOM54.pdf             p.22
            // 5.5_LDS_1996-01-02.pdf   p25
            // 5.5.1_LDS_2019-11-15.pdf p.25 
            // 5.5.5_Annotations_TJ.pdf p.61
            //  INDIVIDUAL_RECORD
            public string N0_ID;                                        //      n @XREF:INDI@ INDI                          {1:1}
                                                                        //                                                      V5.3
                                                                        //                                                      V5.4
                                                                        //                                                      V5.5
                                                                        //                                                      V5.5.1
                                                                        //                                                      V5.5.5
            public string N1_RESN;                                      //          +1 RESN <RESTRICTION_NOTICE>            {0:1}
                                                                        //                                                      V5.4    p.45
                                                                        //                                                      V5.5    p.52
                                                                        //                                                      V5.5.1  p.60
            public List<PERSONAL_NAME_STRUCTURE> N1_NAME_liste;         //          +1 <<PERSONAL_NAME_STRUCTURE>>          {0:M}
                                                                        //                                                      V5.3    p.21
                                                                        //                                                      V5.4    p.31
                                                                        //                                                      V5.5    p.34
                                                                        //                                                      V5.5.1  p.38
                                                                        //                                                      V5.5.5  p.72
                                                                        //          +1 <INDI_TITLE>                         {0:M}
                                                                        //                                                      V5.3    p.31
                                                                        //  est pris en charge par attribut 
            public string N1_SEX;                                       //          +1 SEX <SEX_VALUE>                      {0:1}
                                                                        //                                                      V5.3    p.37
                                                                        //                                                      V5.4    p.45
                                                                        //                                                      V5.5    p.53
                                                                        //                                                      V5.5.1  p.61
                                                                        //                                                      V5.5.5  p.105
            public List<EVEN_ATTRIBUTE_STRUCTURE> N1_EVEN_Liste;        //          +1 <<INDIVIDUAL_EVENT_STRUCTURE>>       {0:M}

            //                                                      V5.4    p.28
            //                                                      V5.5    p.31
            //                                                      V5.5.1  p.34
            //                                                      V5.5.5  p.69
            public List<EVEN_ATTRIBUTE_STRUCTURE> N1_Attribute_liste;  //          +1 <<INDIVIDUAL_ATTRIBUTE_STRUCTURE>>   {0:M}
                                                                       //                                                      V5.4    p.28
                                                                       //                                                      V5.5    p.30
                                                                       //                                                      V5.5.1  p.33
                                                                       //                                                      V5.5.5  p.67
            public List<LDS_INDIVIDUAL_ORDINANCE> N1_LDS_liste;         //          +1 <<LDS_INDIVIDUAL_ORDINANCE>>         {0:M}
                                                                        //                                                      V5.4    p.30
                                                                        //                                                      V5.5    p.32
                                                                        //                                                      V5.5.1  p.35
            public string N1_SITE;                                      //  n SITE <SITE_NAME>                              {0:1}
                                                                        //                                                      V5.3    p.37
            public List<ADDRESS_STRUCTURE> N1_ADDR_liste;               //          +1 <<ADDRESS_STRUCTURE>>                {0:M}
                                                                        //                                                      V5.3    p.19
            public List<string> N1_PHON_liste;                          //  n PHON <PHONE_NUMBER> {0:3}
            public List<string> N1_EMAIL_liste;                         //  n EMAIL <ADDRESS_EMAIL> {0:3}
            public List<string> N1_FAX_liste;                           //  n FAX <ADDRESS_FAX> {0:3}
            public List<string> N1_WWW_liste;                           //  n WWW <ADDRESS_WEB_PAGE> {0:3}

            public string N1_RELI;                                      //          +1 RELI <RELIGIOUS_AFFILIATION>        {0:M}
                                                                        //                                                      V5.3    p.36
            public string N1_NAMR;                                      //          +1 NAMR <RELIGIOUS_NAME>                {0:M}
                                                                        //                                                      V5.3    p.36
            public string N2_NAMR_RELI;                                 //          +1 RELI <RELIGIOUS_AFFILIATION>         {0:1}
                                                                        //                                                      V5.3    p.36
                                                                        // EDUC                                                     //          +1 EDUC <SCHOLASTIC_ACHIEVEMENT>       {0:M}
                                                                        //                                                      V5.3    p.37
                                                                        // prit en charge par Attibut
                                                                        // OCCC                                                     //          +1 OCCU <OCCUPATION>                    {0:M}
                                                                        //                                                      V5.3    p.33
                                                                        // prit en charge par Attibut
                                                                        // SSN                                                                  +1 SSN <SOCIAL_SECURITY_NUMBER>         {0:M}
                                                                        //                                                      V5.3    p.37
                                                                        // prit en charge par Attibut
                                                                        // IDNO                                                                 +1 IDNO <NATIONAL_ID_NUMBER>            {0:M}
                                                                        //                                                      V5.3    p.33
                                                                        // prit en charge par Attibut
                                                                        // TYPE                                                                 +1 TYPE<TYPE_OF>                        {1:1}
                                                                        //                                                      V5.3    p.40
                                                                        // prit en charge par Attibut
                                                                        // PROP                                                                 +1 PROP <POSSESSIONS>                   {0:M}
                                                                        //                                                      V5.3    p.35
                                                                        // DSCR                                                                 1 DSCR <PHYSICAL_DESCRIPTION>           {0:M}
                                                                        //                                                      V5.3    p.35
                                                                        // prit en charge par Attibut
                                                                        // CONT                                                                 +1 CONT <PHYSICAL_DESCRIPTION>          {0:M}
                                                                        // prit en charge par Attibut
            public string N1_SIGN;                                      //          +1 SIGN <SIGNATURE_INFO>                {0:M} 
                                                                        //                                                      V5.3    p.37
                                                                        // NMR                                                      //          +1 NMR <COUNT_OF_MARRIAGES>             {0:M}
                                                                        //                                                      V5.3    p.27
                                                                        // prit en charge par Attibut
                                                                        //MCHI                                                                  + NCHI <COUNT_OF_CHILDREN>              {0:M}
                                                                        //                                                      V5.3    p.27
                                                                        // prit en charge par Attibut
                                                                        // NATI                                                                 +1 NATI <NATIONALITY>                   {0:M}
                                                                        //                                                      V5.3    p.33
                                                                        // prit en charge par Attibut
                                                                        // CAST                                                                 +1 CAST <CASTE_NAME>                    {0:M}
                                                                        //                                                      V5.3    p.27
                                                                        // prit en charge par Attibut

            public CHILD_TO_FAMILY_LINK N1_FAMC;                        //          +1 <<CHILD_TO_FAMILY_LINK>>             {0:M}
                                                                        //                                                      V5.3
                                                                        //                                                      V5.4    p.27
                                                                        //                                                      V5.5    p.32
                                                                        //                                                      V5.5.1  p.31
                                                                        //                                                      V5.5.5  p.67

            public List<SPOUSE_TO_FAMILY_LINK> N1_FAMS_liste_Conjoint;  //          +1 <<SPOUSE_TO_FAMILY_LINK>>            {0:M}
                                                                        //                                                      V5.3
                                                                        //                                                      V5.4    p.32
                                                                        //                                                      V5.5    p.35
                                                                        //                                                      V5.5.1  p.40
                                                                        //                                                      V5.5.5  p.75
            public List<string> N1_SUBM_liste_ID;                       //          +1 SUBM @<XREF:SUBM>@                   {0:M}
                                                                        //                                                      V5.4
                                                                        //                                                      V5.5    p.55
                                                                        //                                                      V5.5.1  p.28
            public List<ASSOCIATION_STRUCTURE> N1_ASSO_liste;           //          +1 <<ASSOCIATION_STRUCTURE>>            {0:M}
                                                                        //                                                      V5.3
                                                                        //                                                      V5.4    p.48
                                                                        //                                                      V5.5    p.29
                                                                        //                                                      V5.5.1  p.31
                                                                        //                                                      V5.5.5  p.65
            public List<string> N1_ALIA_liste_ID;                       //          +1 ALIA @<XREF:INDI>@                   {0:M}
                                                                        //                                                      V5.3
                                                                        //                                                      V5.4    p.48
                                                                        //                                                      V5.5    p.55
                                                                        //                                                      V5.5.1  p.25
            public List<string> N1_ANCI_liste_ID;                       //          +1 ANCI @<XREF:SUBM>@                   {0:M}
                                                                        //                                                      V5.3
                                                                        //                                                      V5.4
                                                                        //                                                      V5.5    p.55
                                                                        //                                                      V5.5.1  p.28
            public List<string> N1_DESI_liste_ID;                       //          +1 DESI @<XREF:SUBM>@                   {0:M}
                                                                        //                                                      V5.3
                                                                        //                                                      V5.4
                                                                        //                                                      V5.5    p.55
                                                                        //                                                      V5.5.1  p.28
            public string N1_RFN;                                       //          +1 RFN <PERMANENT_RECORD_FILE_NUMBER>   {0:1}
                                                                        //                                                      V5.3    p.34
                                                                        //                                                      V5.4    p.43
                                                                        //                                                      V5.5    p.50
                                                                        //                                                      V5.5.1  p.57
            public string N1_AFN;                                       //          +1 AFN <ANCESTRAL_FILE_NUMBER>          {0:1}
                                                                        //                                                      V5.3    p.26
                                                                        //                                                      V5.4    p.34
                                                                        //                                                      V5.5    p.38
                                                                        //                                                      V5.5.1  p.42
            public List<USER_REFERENCE_NUMBER> N1_REFN_liste;           //          +1 REFN <USER_REFERENCE_NUMBER>         {0:M}
                                                                        //                                                      V5.3
                                                                        //                                                      V5.4    p.47
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
                                                                        //                                                      V5.3    p.19
                                                                        //                                                      V5.4    p.27
                                                                        //                                                      V5.5    p.29
                                                                        //                                                      V5.5.1  p.31

            //                                                      v5.5.5  p.66
            public List<string> N1_NOTE_STRUCTURE_liste_ID;                       //          +1 <<NOTE_STRUCTURE>>                   {0:M}
                                                                                  //                                                      V5.3    p.21
                                                                                  //                                                      V5.4    p.30
                                                                                  //                                                      V5.5    p.33
                                                                                  //                                                      V5.5.1  p.37
                                                                                  //                                                      v5.5.5  p.71
            public List<string> N1_SOUR_citation_liste_ID;              //          +1 <<SOURCE_CITATION>>                  {0:M}
                                                                        //                                                      V5.3    p.23
                                                                        //                                                      V5.4    p.31
                                                                        //                                                      V5.5    p.34
                                                                        //                                                      V5.5.1  p.39
                                                                        //                                                      v5.5.5  p.73
            public List<string> N1_SOUR_source_liste_ID;
            public List<string> MULTIMEDIA_LINK_liste_ID;
            public string Adopter;
            public string nom_section_1;
            public string nom_section_2;
            public string nom_section_3;
            public string Titre;
            public string PhotoID;

            public string N1__ANCES_CLE_FIXE;                           // +1 _ANCES_CLE_FIXE dans Ancestrologie
            public string N1_FILA;                                      // +1 FILA Filiation dans Ancestrologie
            public string N1__FIL;                                      // Filiation de l'individu Heridis
            public string N1__CLS;                                      // Individu sans postérité. Heridis
            public List<Ligne_perdue> ligne_perdue_liste;
        }
        public struct HEADER
        {
            // HEADER           GEDCOM53.pdf                p.16
            // HEADER           GEDCOM54.pdf                p.21
            // HEADER           5.5_LDS_1996-01-02.pdf      p.23
            // HEADER           5.5.1_LDS_1999-10-02.pdf    p.23
            // GEDCOM_HEADER    5.5.5_Annotations_TJ        p.67

            //                                                      n HEAD                                              {1:1}
            //                                                                                                              V5.3    p39
            //                                                                                                              V5.4    p.34
            //                                                                                                              V5.5    p.23
            //                                                                                                              V5.5.1  p.23
            //                                                                                                              V5.5.5  p.56

            public string N1_SOUR;                          //      +1 SOUR <APPROVED_SYSTEM_ID>                        {1:1}
                                                            //                                                              V5.3    p.39
                                                            //                                                              V5.4    p.34
                                                            //                                                              V5.5    p.38
                                                            //                                                              V5.5.1  p.42
                                                            //                                                              V5.5.5  p.57
            public string N2_SOUR_VERS;                     //          +2 VERS <VERSION_NUMBER>                        {0:1}
                                                            //                                                              V5.3    p.40
                                                            //                                                              V5.4    p.47
                                                            //                                                              V5.5    p.55
                                                            //                                                              V5.5.1  p.64
                                                            //                                                              V5.5.5  p.106
            public string N2_SOUR_NAME;                     //          +2 NAME <NAME_OF_PRODUCT>                       {0:1}
                                                            //                                                              V5.3    p.35
                                                            //                                                              V5.4    p.41
                                                            //                                                              V5.5    p.48
                                                            //                                                              V5.5.1  p.54
                                                            //                                                              V5.5.5  p.96
            public string N2_SOUR_CORP;                     //          +2 CORP <NAME_OF_BUSINESS>                      {0:1}
                                                            //                                                              V5.3    p.27
                                                            //                                                              V5.4    p.41
                                                            //                                                              V5.5    p.48
                                                            //                                                              V5.5.1  p.54
                                                            //                                                              V5.5.5  p.96
            public string N3_SOUR_CORP_SITE;                //              3 SITE <SITE_NAME>                          {0:1}
                                                            //                                                              V5.3    p.37
            public List<ADDRESS_STRUCTURE> N3_SOUR_CORP_ADDR_liste;//              +3 <<ADDRESS_STRUCTURE>>             {0:1}
                                                                   //                                                              V5.3    p.19
                                                                   //                                                              V5.4    p.27
                                                                   //                                                              V5.5    p.29
                                                                   //                                                              V5.5.1  p.31
                                                                   //                                                              V5.5.5  p.64
            public List<string> N3_SOUR_CORP_PHON_liste;    //              +3 PHON <PHONE_NUMBER>                      {0:3}
                                                            //                                                              V5.3    p.34
                                                            //                                                              V5.4    p.43
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
                                                            //                                                              V5.3    p.32
                                                            //                                                              V5.4    p.41
                                                            //                                                              V5.5    p.48
                                                            //                                                              V5.5.1  p.54
                                                            //                                                              V5.5.5  p.
            public string N3_SOUR_DATA_DATE;                //              +3 DATE <PUBLICATION_DATE>                  {0:1}
                                                            //                                                              V5.3    p.35
                                                            //                                                              V5.4    p.44
                                                            //                                                              V5.5    p.51
                                                            //                                                              V5.5.1  p.59
                                                            //                                                              V5.5.5  p.103
            public string N3_SOUR_DATA_CORP;                //              +3 COPR <COPYRIGHT_SOURCE_DATA>             {0:1}
                                                            //                                                              V5.4    p.35
                                                            //                                                              V5.5    p.39
                                                            //                                                              V5.5.1  p.44
                                                            //                                                              V5.5.5  p.79
            public string N1_DEST;                          //      +1 DEST <RECEIVING_SYSTEM_NAME>                     {0:1}
                                                            //                                                              V5.3    p.39
                                                            //                                                              V5.4    p.44
                                                            //                                                              V5.5    p.51
                                                            //                                                              V5.5.1  p.59
                                                            //                                                              V5.5.5  p.103
            public string N1_DATE;                          //      +1 DATE <TRANSMISSION_DATE>                         {0:1}
                                                            //                                                              V5.3    p.40
                                                            //                                                              V5.4    p.44
                                                            //                                                              V5.5    p.55
                                                            //                                                              V5.5.1  p.63
                                                            //                                                              V5.5.5  p.93
            public string N2_DATE_TIME;                     //          +2 TIME <TIME_VALUE>                            {0:1}
                                                            //                                                              V5.3    p.39
                                                            //                                                              V5.4    p.47
                                                            //                                                              V5.5    p.55
                                                            //                                                              V5.5.1  p.63
                                                            //                                                              V5.5.5  p.107
            public List<string> N1_SUBM_liste_ID;           //      +1 SUBM @<XREF:SUBM>@                               {1:1}
                                                            //                                                              V5.3    p.41
                                                            //                                                              V5.4    p.24
                                                            //                                                              V5.5    p.55
                                                            //                                                              V5.5.1  p.28
                                                            //                                                              V5.5.5  p.108
            public string N1_SUBN;                          //      +1 SUBN @<XREF:SUBN>@                               {0:1}
                                                            //                                                              V5.4    p.48
                                                            //                                                              V5.5    p.55
                                                            //                                                              V5.5.1  p.28
            public string N1_FILE;                          //      +1 FILE <FILE_NAME>                                 {0:1}
                                                            //                                                              V5.3    p.30
                                                            //                                                              V5.4    p.39
                                                            //                                                              V5.5    p.23
                                                            //                                                              V5.5.1  p.50
                                                            //                                                              V5.5.5  p.93
                                                            //      +1 SCHEMA
            public string N1_tag_shema;                     //          +2 <<USER_TAG_SCHEMA>>                          {1:M}
                                                            //                                                              V5.3    p.30

            public string N1_COPR;                          //      +1 COPR <COPYRIGHT_GEDCOM_FILE>                     {0:1}
                                                            //                                                              V5.3    p.27
                                                            //                                                              V5.4    p.35
                                                            //                                                              V5.5    p.39
                                                            //                                                              V5.5.1  p.55
                                                            //                                                              V5.5.5  p.79
                                                            //      +1 SHEMA
                                                            //                                                              V5.3    p.58
                                                            // A context pattern definition that specifies the meaning and the valid 
                                                            // context(s) of a user defined tag. See the SCHEMA_STRUCTURE substructure
                                                            // definition.
                                                            //      +1 GEDC                                             {1:1}      
            public string N2_GEDC_VERS;                     //          +2 VERS <VERSION_NUMBER>                        {1:1}
                                                            //                                                              V5.3    p.40
                                                            //                                                              V5.4    p.47
                                                            //                                                              V5.5    p.55
                                                            //                                                              V5.5.1  p.64
                                                            //                                                              V5.5.5  p.
            public string N2_GEDC_FORM;                     //          +2 FORM <GEDCOM_FORM>                           {1:1}
                                                            //                                                              V5.3    p.30
                                                            //                                                              V5.4    p.39
                                                            //                                                              V5.5    p.44
                                                            //                                                              V5.5.1  p.50
                                                            //                                                              V5.5.5  p.
            public string N3_GEDC_FORM_VERS;                //              +3 VERS <GEDCOM_VERSION_NUMBER>             {1:1}
                                                            //                                                              V5.5.5  p.49
            public string N1_CHAR;                          //      +1 CHAR <CHARACTER_SET>                             {1:1}
                                                            //                                                              V5.3    p.27
                                                            //                                                              V5.4    p.35
                                                            //                                                              V5.5    p.39
                                                            //                                                              V5.5.1  p.44
                                                            //                                                              V5.5.5  p.47
            public string N2_CHAR_VERS;                     //          +2 VERS <VERSION_NUMBER>                        {0:1}
                                                            //                                                              V5.3    p.40
                                                            //                                                              V5.4    p.47
                                                            //                                                              V5.5    p.55
                                                            //                                                              V5.5.1  p.64
            public string N1_LANG;                          //      +1 LANG <LANGUAGE_OF_TEXT>                          {0:1}
                                                            //                                                              V5.3
                                                            //                                                              V5.4    p.39
                                                            //                                                              V5.5    p.45
                                                            //                                                              V5.5.1  p.51
                                                            //                                                              V5.5.5  p.94
            public string N2_PLAC_FORM;                     //      +1 FORM <PLACE_HIERARCHY>                           {1:1}
                                                            //                                                              V5.3
                                                            //                                                              V5.4    p.44
                                                            //                                                              V5.5    p.51
                                                            //                                                              V5.5.1  p.58
            public List<string> N1_NOTE_STRUCTURE_liste_ID; //      +1 NOTE <GEDCOM_CONTENT_DESCRIPTION>                {0:1}
                                                            //                                                              V5.4    p.39
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
            public List<Ligne_perdue> ligne_perdue_liste;
        }
        public class LDS_INDIVIDUAL_ORDINANCE
        {
            // GEDCOM53.pdf             p.21 LDS_INDI_ORDINANCE_EVENT:=
            // GEDCOM54.pdf             p.30
            // 5.5_LDS_1996-01-02.pdf   p.32
            // 5.5.1_LDS_1999-10-02.pdf p.36
            // LDS_INDIVIDUAL_ORDINANCE
            public string N0_EVEN;                                      //  n [ BAPL | CONL | ENDL | SLGC]      {1:1}      V5.5.1
                                                                        //  n [ BAPL | CONL | WAC | ENDL ]      {1:1}      V5.3
            public string N1_TYPE;                                      //      +1 TYPE <LDS_INDI_ORD_DESCRIPTOR>{0:1}
            public string N1_DATE;                                      //      +1 DATE <DATE_LDS_ORD>          {0:1}
                                                                        //                                          V5.3    p.28
                                                                        //                                          V5.4    p.36
                                                                        //                                          V5.5    p.41
                                                                        //                                          V5.5.1  p.46
            public string DATE_trier;                                   // date au format YYYYMMDD
            public string N1_TEMP;                                      //      +1 TEMP <TEMPLE_CODE>           {0:1}
                                                                        //                                          V5.3    p.39
                                                                        //                                          V5.4    p.47
                                                                        //                                          V5.5    p.54
                                                                        //                                          V5.5.1  p.63
            public string N1_PLAC;                                      //      +1 PLAC <PLACE_LIVING_ORDINANCE>{0:1}
                                                                        //                                          V5.4    p.44
                                                                        //                                          V5.5    p.41
                                                                        //                                          V5.5.1  p.58
            public string N1_STAT;                                      //      +1 STAT <LDS_BAPTISM_DATE_STATUS>{0:1}
                                                                        //                                          V5.4    p.39
                                                                        //                                          V5.5    p.45
                                                                        //                                          V5.5.1  p.37 p.26

            public string N2_STAT_DATE;                                 //          +2 DATE <CHANGE_DATE>       {1:1}
                                                                        //                                          V5.5.1  p.41
            public List<string> N1_NOTE_STRUCTURE_liste_ID;             //      +1 <<NOTE_STRUCTURE>>           {0:M}
                                                                        //                                          V5.3    p.21
                                                                        //                                          V5.4    p.30
                                                                        //                                          V5.5    p.33
                                                                        //                                          V5.5.1  p.51
            public List<string> N1_SOUR_citation_liste_ID;              //      +1 <<SOURCE_CITATION>>          {0:M}
                                                                        //                                          V5.3    p.23
                                                                        //                                          V5.4    p.31
                                                                        //                                          V5.5    p.34
                                                                        //                                          V5.5.1  p.39
            public List<string> N1_SOUR_source_liste_ID;
            public string N1_FAMC;                                      //      +1 FAMC @<XREF:FAM>@            {1:1}
                                                                        //                                          V5.4
                                                                        //                                          V5.5    p.55
                                                                        //                                          V5.5.1  p.24

            public List<Ligne_perdue> ligne_perdue_liste;
        }
        public class LDS_SPOUSE_SEALING
        {
            // GEDCOM54.pdf             p.21 LDS_FAM_ORDINANCE_EVENT:=
            // GEDCOM54.pdf             p.30
            // 5.5.1_LDS_1999-10-02.pdf p.36
            // 5.5_LDS_1996-01-02.pdf   p.33
            // LDS_SPOUSE_SEALING:=
            //      n SLGS {1:1}
            public string N1_TYPE;                                      //         +1 <LDS_FAM_ORD_DESCRIPTOR>                  {0:1}
                                                                        //                                                          V5.3    p.31
                                                                        //                                                          V5.3    p.28
            public string N1_DATE;                                      //         +1 DATE <DATE_LDS_ORD>                       {0:1}
                                                                        //                                                          V5.3    p.28
                                                                        //                                                          V5.4    p.36
                                                                        //                                                          V5.5    p.41
                                                                        //                                                          V5.5.1  p.46
            public string N1_TEMP;                                      //         +1 TEMP <TEMPLE_CODE>                        {0:1}
                                                                        //                                                          V5.3    p.39
                                                                        //                                                          V5.4    p.47
                                                                        //                                                          V5.5    p.54
                                                                        //                                                          V5.5.1  p.63
            public string N1_PLAC;                                      //         +1 PLAC <PLACE_LIVING_ORDINANCE>             {0:1}
                                                                        //                                                          V5.4    p.44
                                                                        //                                                          V5.5    p.41
                                                                        //                                                          V5.5.1  p.58
            public string N1_STAT;                                      //         +1 STAT <LDS_SPOUSE_SEALING_DATE_STATUS>     {0:1}
                                                                        //                                                          V5.4
                                                                        //                                                          V5.5    p.46
                                                                        //                                                          V5.5.1  p.52
            public string N2_STAT_DATE;                                 //             +2 DATE <CHANGE_DATE>                    {1:1}
                                                                        //                                                          V5.5.1  p.44
            public List<string> N1_NOTE_STRUCTURE_liste_ID;             //         +1 <<NOTE_STRUCTURE>>                        {0:M}
                                                                        //                                                          V5.3    p.21
                                                                        //                                                          V5.4    p.30
                                                                        //                                                          V5.5    p.33
                                                                        //                                                          V5.5.1  p.37
            public List<string> N1_SOUR_citation_liste_ID;              //         +1 <<SOURCE_CITATION>>                       {0:M}
                                                                        //                                                          V5.3    p.23
                                                                        //                                                          V5.4    p.31
                                                                        //                                                          V5.5    p.34
                                                                        //                                                          V5.5.1  p.39
            public List<string> N1_SOUR_source_liste_ID;
        }
        public class Ligne_perdue
        {
            public int ligne;
            public string texte;
        }
        public class MULTIMEDIA_LINK // V5.4 et plus
        {
            // GEDCOM54.pdf             p.30
            // 5.5_LDS_1996-01-02.pdf   p.33
            // 5.5.1_LDS_1999-10-02.pdf p.37
            // 5.5.5_Annotations_TJ.pdf p.71
            public string ID_RECORD;                                 //      n @XREF:OBJE@ OBJE                          {1:1}
                                                                        //                                                      V5.4
                                                                        //                                                      V5.5
                                                                        //                                                      V5.5.1
                                                                        //                                                      V5.5.5
                                                                        // ou 
                                                                        //      n OBJE                                      {1:1}
            public string ID_LINK;                                   // générer par GH
            public bool ID_seul;                                        // vrai si contient seulement ID_record
            public string FORM;                                      //      +1 FORM<MULTIMEDIA_FORMAT>                  {1:1}
                                                                        //                                                      V5.4
                                                                        //                                                      V5.5
                                                                        //                                                      V5.5.1
                                                                        //              [bmp | gif | jpeg | ole | pcx | tiff | wav]
            public string TITL;                                      //          +1 TITL<DESCRIPTIVE_TITLE>              {0:1}
                                                                        //                                                      V5.4
                                                                        //                                                      V5.5
                                                                        //                                                      V5.5.1
            public string FILE;                                      //          +1 FILE<MULTIMEDIA_FILE_REFERENCE>      {1:1}
                                                                        //                                                      V5.4
                                                                        //                                                      V5.5
            //public string FORM;                                 //              +2 FORM<MULTIMEDIA_FORMAT>          {1:1}
                                                                        //                                                      V5.5.1
                                                                        //                  [bmp | gif | jpg | ole | pcx | tif | wav]
            public string MEDI;                            //                      +3 MEDI<SOURCE_MEDIA_TYPE>  {0:1}
                                                                        //                              [audio | book | card |
                                                                        //                              electronic | fiche | film |
                                                                        //                              magazine |manuscript | map |
                                                                        //                              newspaper | photo | tombstone |
                                                                        //                              video]
                                                                        //                                                      V5.5.1
            public List<string> NOTE_STRUCTURE_liste_ID;             //          +1 <<NOTE_STRUCTURE>>                   {0:M}
                                                                        //                                                      V5.4
                                                                        //                                                      V5.5
            public List<string> SOUR_citation_liste_ID;              //          +1 <<SOURCE_CITATION>>                  {0:M}
                                                                        //                                                      V5.5.1  p.39
        }
        public class MULTIMEDIA_RECORD
        {
            // GEDCOM54.pdf             p.23
            // 5.5_LDS_1996-01-02.pdf   p33
            // 5.5.1_LDS_1999-10-02.pdf p.26
            // 5.5.5_Annotations_TJ.pdf p.62
            //                                                              MULTIMEDIA_LINK:= V5.3 V5.4
            //                                                              MULTIMEDIA_LINK:= V5.3 V5.5
            //                                                              MULTIMEDIA_RECORD:= V5.5.1 V 5.5.5
            public string ID_RECORD;                                        //      n @XREF:OBJE@ OBJE                          {1:1}
                                                                        //                                                      V5.4
                                                                        //                                                      V5.5    p.55
                                                                        //                                                      V5.5.1
                                                                        //                                                      V5.5.5
            public string FORM;                                      //          +1 FORM <MULTIMEDIA_FORMAT>             {1:1}
                                                                        //                                                      V5.4    p.41
                                                                        //                                                      V5.5    p.48
            public string TITL;                                      //          +1 TITL <DESCRIPTIVE_TITLE>             {0:1}
                                                                        //                                                      V5.4 p.41
                                                                        //                                                      V5.5 p.43
            public string FILE;                                      //          +1 FILE <MULTIMEDIA_FILE_REFN>          {1:M}
                                                                        //                                                      V5.4 p.41
                                                                        //                                                      V5.5    p.47
                                                                        //                                                      V5.5.1  p.54
                                                                        //                                                      V5.5.5  p.95
            //public string FORM;                                 //              +2 FORM <MULTIMEDIA_FORMAT>         {1:1}
                                                                        //                                                      V5.5.1  p.54
                                                                        //                                                      V5.5.5  p.95
            public string FORM_TYPE;                            //                  +3 TYPE <SOURCE_MEDIA_TYPE>     {0:1}
                                                                        //                                                      V5.5.1  p.62
                                                                        //                                                      V5.5.5  p.106
            //public string TITL;                                 //              +2 TITL <DESCRIPTIVE_TITLE>         {0:1}
                                                                        //                                                      V5.5.1  p.48
                                                                        //                                                      V5.5.5  p.89
            public string BLOB;                                      //          +1 BINARY_OBJECT                        {1:1}
                                                                        //                                                      V5.4
                                                                        //                                                      V5.5
                                                                        //              +2 CONT <ENCODED_MULTIMEDIA_LINE>   {1:M} 
                                                                        // public string N1_EOBJ;                                      //          +1 EOBJ  END_OBJECT}                    {1:1}
                                                                        //                                                      V5.4    p.62
                                                                        // public string N1_OBJE;                                      //          +1 @<XREF:OBJE>@ chain to continued object  V5.5 

            public List<USER_REFERENCE_NUMBER> REFN_liste;           //          +1 REFN <USER_REFERENCE_NUMBER>         {0:M}
                                                                        //                                                      V5.5.1  p.63
                                                                        //                                                      V5.5.5  p.107
                                                                        //              +2 TYPE <USER_REFERENCE_TYPE>       {0:1}
                                                                        //                                                      V5.5    p.55
                                                                        //                                                      V5.5.1  p.64
                                                                        //                                                      v5.5.5  p.106
            public string RIN;                                       //          +1 RIN <AUTOMATED_RECORD_ID>            {0:1}
                                                                        //                                                      V5.5.1  p.43
                                                                        //                                                      V5.5.5  p.78
            public List<string> NOTE_STRUCTURE_liste_ID;             //          +1 <<NOTE_STRUCTURE>>                   {0:M}
                                                                        //                                                      V5.3    p.21
                                                                        //                                                      V5.4    p.30
                                                                        //                                                      V5.5    p.33
                                                                        //                                                      V5.5.1  p.37
                                                                        //                                                      V5.5.5  p.71
            public List<string> SOUR_citation_liste_ID;              //          +1 <<SOURCE_CITATION>>                  {0:M}
                                                                        //                                                      V5.3    p.23
                                                                        //                                                      V5.4    p.31
                                                                        //                                                      V5.5.1  p.39
                                                                        //                                                      V5.5.5  p.73
            public List<string> SOUR_source_liste_ID;
            public CHANGE_DATE CHAN;                                 //          +1 <<CHANGE_DATE>>                      {0:1}
                                                                        //                                                      V5.3    p.19
                                                                        //                                                      V5.4    p.27
                                                                        //                                                      V5.5.1  p.31
                                                                        //                                                      V5.5.5  p.66

            public string Herisis_DATE;                                 //          +1                                          Herisis

            public List<Ligne_perdue> ligne_perdue_liste;
        }
        public class NAME_STRUCTURE_53 // V5.3 seulement
        {
            // GEDCOM53.pdf             p.21
            public string N0_NAME;                                      //      n NAME <NAME_PERSONAL>      {1:1}
                                                                        //                                      V5.3
            public string N1_TYPE;                                      // +1 TYPE <NAME_TYPE_DESCRIPTOR>
                                                                        //                                      V5.3
            public List<string> N1_SOUR_citation_liste_ID;              // +1 <<SOUR_STRUCTURE>> {0:1}
                                                                        //                                      V5.3
            public List<string> N1_SOUR_source_liste_ID;
            public List<string> N1_NOTE_STRUCTURE_liste_ID;             // +1 <<NOTE_STRUCTURE>> {0:1}
                                                                        //                                      V5.3
        }
        public class NOTE_STRUCTURE
        {
            // GEDCOM53.pdf             p.21
            // GEDCOM54.pdf             p.31
            // 5.5_LDS_1996-01-02.pdf   p.33
            // 5.5.1_LDS_1999-10-02.pdf p.37
            // 5.5.5_Annotations_TJ     p.71
            //                                                              NOTE_STRUCTURE:=
            public string N0_ID_STRUCTURE;                           // généré par GH
            public string N0_ID_RECORD;                              // n [ @XREF:NOTE@ | NULL ] NOTE [ <SUBMITTER_TEXT>{1:1}
                                                                     //                                                      V5.3
                                                                     //                                                      V5.4
                                                                     //                                                      V5.5
                                                                     //                                                      V5.5.1
                                                                     //                                                      V5.5.5
                                                                     //          +1 [ CONC | CONT ] <SUBMITTERS_TEXT> {0:M}  V5.3
                                                                     //                                                      V5.4
                                                                     //                                                      V5.5
                                                                     //          +1 [ CONC | CONT ] <SUBMITTERS_TEXT> {0:M}  V5.5.1
            public string N0_texte;                                     // texte venant du ID
            public List<string> N1_NOTE_STRUCTURE_liste_ID;             //      n <<NOTE_STRUCTURE>>        {0:M}
                                                                        //                                                      V5.3    p.21
                                                                        //                                                      V5.4
                                                                        //                                                      V5.5
                                                                        //                                                      V5.5
                                                                        //                                                      V5.5.1

            public List<string> N1_SOUR_citation_liste_ID;              //          +1 <<SOURCE_CITATION>>                  {0:M} 
                                                                        //                                                  V5.5        p.34
            public List<string> N1_SOUR_source_liste_ID;
        }
        public class NOTE_RECORD
        {
            // GEDCOM53.pdf             p.18
            // GEDCOM54.pdf             p.23
            // 5.5_LDS_1996-01-02.pdf   p26
            // 5.5.1_LDS_1999-10-02.pdf p.27
            // 5.5.5_Annotations_TJ     p.71
            //                                                              NOTE_RECORD:=
            public string N0_ID;                                        //      n @<XREF:NOTE>@ NOTE <SUBMITTER_TEXT>       {1:1} 
                                                                        //                                                      V5.3
                                                                        //                                                      V5.4
                                                                        //                                                      V5.5   p.54
                                                                        //                                                      V5.5.1 p.63
                                                                        //                                                      V5.5.5 p.108
            public string N0_texte;                                     // texte du niveau 0
                                                                        //          +1 [CONC|CONT] <SUBMITTER_TEXT>         {0:M}
                                                                        //                                                      V5.3
                                                                        //                                                      V5.4
                                                                        //                                                      V5.5
                                                                        //                                                      V5.5.1 p.63

            public List<USER_REFERENCE_NUMBER> N1_REFN_liste;           //          +1 REFN <USER_REFERENCE_NUMBER>         {0:M}
                                                                        //                                                      V5.4
                                                                        //                                                      V5.5   p.55
                                                                        //                                                      V5.5.1 p.63 p.64
                                                                        //                                                      V5.5.5 p.107
                                                                        // inclue avec le précédent
                                                                        //              +2 TYPE <USER_REFERENCE_TYPE>       {0:1} 
                                                                        //                                                      V5.5   p.54
                                                                        //                                                      V5.5.1 p.64
                                                                        //                                                      V5.5.5 p.107
                                                                        //                                                      V5.5    p.55 
            public string N1_RIN;                                       //          +1 RIN <AUTOMATED_RECORD_ID>            {0:1}
                                                                        //                                                      V5.4
                                                                        //                                                      V5.5    p.38 
                                                                        //                                                      V5.5.1  p.43 
                                                                        //                                                      V5.5.5  p.78
            public List<string> N1_SOUR_citation_liste_ID;              //          +1 <<SOURCE_CITATION>>                  {0:M} 
                                                                        //                                                      V5.5    p.34
                                                                        //                                                      V5.5.1  p.39
                                                                        //                                                      V5.5.5  p.73
            public List<string> N1_SOUR_source_liste_ID;
            public List<CHANGE_DATE> N1_CHAN_liste;                     //          +1 <<CHANGE_DATE>>                      {0:1}
                                                                        //                                                      V5.3    p.19
                                                                        //                                                      V5.4    p.27
                                                                        //                                                      V5.5    p.29
                                                                        //                                                      V5.5.1  p.31
                                                                        //                                                      V5.5.5  p.66
            public List<string> N1_NOTE_STRUCTURE_liste_ID;             //      n <<NOTE_STRUCTURE>>        {0:M}
                                                                        //                                                      V5.3    p.21
            public int numero;
            public List<Ligne_perdue> ligne_perdue_liste;
        }
        public class PERSONAL_NAME_PIECES
        {
            // GEDCOM54.pdf             p.31
            // 5.5_LDS_1996-01-02.pdf   p.37
            // 5.5.1_LDS_2019-11-15.pdf p.37
            // 5.5.5_Annotations_TJ.pdf p.71
            public string Nn_NPFX;                                      //      n NPFX <NAME_PIECE_PREFIX>  {0:1}
                                                                        //                                      V5.4   p.42
                                                                        //                                      V5.5   p.48
                                                                        //                                      V5.5.1 p.55
                                                                        //                                      V5.5.5 p.97
            public string Nn_GIVN;                                      //      n GIVN <NAME_PIECE_GIVEN>   {0:1}
                                                                        //                                      V5.4   p.42
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
                                                                        //                                      V5.4   p.42
                                                                        //                                      V5.5   p.49
                                                                        //                                      V5.5.1 p.55
                                                                        //                                      V5.5.5 p.98
            public string Nn_NSFX;                                      //      n NSFX <NAME_PIECE_SUFFIX>  {0:1}
                                                                        //                                      V5.4   p.42
                                                                        //                                      V5.5   p.49
                                                                        //                                      V5.5.1 p.55
                                                                        //                                      V5.5.5 p.98
            public List<string> Nn_NOTE_STRUCTURE_liste_ID;             //      n <<NOTE_STRUCTURE>>        {0:M}
                                                                        //                                      V5.3    p.21
                                                                        //                                      V5.4    p.30
                                                                        //                                      V5.5   p.33
                                                                        //                                      V5.5.1 p.37
                                                                        //                                      V5.5.5 p.71
            public List<string> Nn_SOUR_citation_liste_ID;              //      n <<SOURCE_CITATION>>       {0:M}
                                                                        //                                      V5.3    p.23
                                                                        //                                      V5.4    p.31
                                                                        //                                      V5.5   p.44
                                                                        //                                      V5.5.1 p.39
                                                                        //                                      V5.5.5 p.73
            public List<string> Nn_SOUR_source_liste_ID;
        }
        public class PERSONAL_NAME_STRUCTURE
        {
            // GEDCOM54.pdf             p.31
            // 5.5_LDS_1996-01-02.pdf   p.34
            // 5.5.1_LDS_2019-11-15.pdf p.38
            // 5.5.5_Annotations_TJ.pdf p.72
            //                                                              PERSONAL_NAME_STRUCTURE:=
            public string N0_NAME;                                      //      n NAME <NAME_PERSONAL>      {1:1}
                                                                        //                                      V5.3    p.34
                                                                        //                                      V5.4
                                                                        //                                      V5.5   p.48
                                                                        //                                      V5.5.1 p.54
                                                                        //                                      V5.5.5 p.96

            public string N1_TYPE;                                      //          +1 TYPE <NAME_TYPE>     {0:1}
                                                                        //                                      V5.3
                                                                        //                                      V5.5.1 p.56
                                                                        //                                      V5.5.5 p.98
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
            // GEDCOM53.pdf             p.21
            // GEDCOM54.pdf             p.31
            // 5.5_LDS_1996-01-02.pdf   p.37
            // 5.5.1_LDS_2019-11-15.pdf p.38
            // 5.5.5_Annotations_TJ.pdf p.72
            // PLACE_STRUCTURE:=
            public string N0_PLAC;                              //  n PLAC <PLACE_NAME>                     {1:1}
                                                                //                                PLACE_VALUE:= V5.3    p.35
                                                                //                                              V5.4    p.44
                                                                //                                              V5.5    p.51
                                                                //                                              V5.5.1  p.58
                                                                //                                              V5.5.5  p.100
            public string N1_CEME;                              //      +1 CEME <CEMETERY_NAME>             {0:1}
                                                                //                                              V5.3    p.27
            public string N2_CEME_PLOT;                         //      +2 PLOT<BURIAL_PLOT_ID>             {0:1}
                                                                //                                              V5.3    p.
            public string N1_FORM;                              //      +1 FORM <PLACE_HIERARCHY>           {0:1}
                                                                //                                              V5.3
                                                                //                                              V5.4    p.44
                                                                //                                              V5.5.1  p.51
                                                                //                                              V5.5.1  p.58
                                                                //                                              V5.5.5  p.100
            public string N1_SITE;                              //      +1 SITE <SITE_NAME>                 {0:1}
                                                                //                                              V5.3    p.37
            public List<ADDRESS_STRUCTURE> N1_ADDR_liste;       //      +1 <<ADDRESS_STRUCTURE>>            {0:1}
                                                                //                                              V5.3    p.19
            public List<string> N1_PHON_liste;                          //      n PHON <PHONE_NUMBER>           {0:3}
                                                                        //                                          V5.4    p.43
                                                                        //                                          V5.5    p.50
                                                                        //                                          V5.5.1  p.57
                                                                        //                                          V5.5.5  p.99
            public List<string> N1_EMAIL_liste;                         //      n EMAIL <ADDRESS_EMAIL>             {0:3}
                                                                        //                                          V5.5.1 p.41
                                                                        //                                          V5.5.5 p.75
            public List<string> N1_FAX_liste;                           //      n FAX <ADDRESS_FAX>             {0:3}
                                                                        //                                          V5.5.1 p.41
                                                                        //                                          V5.5.5 p.75
            public List<string> N1_WWW_liste;                           //      n WWW <ADDRESS_WEB_PAGE>        {0:3}
                                                                        //                                          V5.5.1 p.42
                                                                        //                                          V5.5.5 p.76
            public string N1_FONE;                              //      +1 FONE <PLACE_PHONETIC_VARIATION>  {0:M}
                                                                //                                              V5.5.1  p.59
                                                                //                                              V5.5.51 p.101
            public string N2_FONE_TYPE;                         //          +2 TYPE <PHONETIC_TYPE>         {1:1} p.57
                                                                //                                              V5.5.1  p.57
                                                                //                                              V5.5.51 p.100

            public string N1_ROMN;                              //      +1 ROMN <PLACE_ROMANIZED_VARIATION> {0:M} p.59
                                                                //                                              V5.5.1  p.59
                                                                //                                              V5.5.51 p.101
            public string N2_ROMN_TYPE;                         //          +2 TYPE <ROMANIZED_TYPE>        {1:1} p.61
                                                                //                                              V5.5.1  p.61
                                                                //                                              V5.5.5  p.105
                                                                //      +1 MAP                              {0:1}
            public string N2_MAP_LATI;                          //          +2 LATI <PLACE_LATITUDE>        {1:1}
                                                                //                                              V5.5.1  p.58
                                                                //                                              V5.5.5  p.100
            public string N2_MAP_LONG;                          //          +2 LONG <PLACE_LONGITUDE>       {1:1}
                                                                //                                              V5.5.1  p.58
                                                                //                                              V5.5.5  p.100
            public List<string> N1_NOTE_STRUCTURE_liste_ID;     //      +1 <<NOTE_STRUCTURE>>               {0:M}
                                                                //                                              V5.3    p.21
                                                                //                                              V5.4    p.30
                                                                //                                              V5.5    p.33
                                                                //                                              V5.5.1  p.37
                                                                //                                              V5.5.5  p.71
            public List<string> N1_SOUR_citation_liste_ID;      //                                          {0:M}
                                                                //                                              V5.4    p.31
                                                                //                                              V5.5    p.34
                                                                // pour GEDitCOM
            public List<string> N1_SOUR_source_liste_ID;        // pour GEDitCOM
        }
        public class REPOSITORY_RECORD
        {
            // GEDCOM53.pdf             p.21 REPOSITORY_STRUCTURE:=
            // GEDCOM54.pdf             p.24
            // 5.5_LDS_1996-01-02.pdf   p26
            // 5.5.1_LDS_2019-11-15.pdf p.27
            // 5.5.5_Annotations_TJ.pdf p.63
            //                                                              REPOSITORY_RECORD:=
            public string N0_ID;                                        //  n @<XREF:REPO>@ REPO {1:1}
                                                                        //                                          V5.3
                                                                        //                                          V5.4
                                                                        //                                          V5.5
                                                                        //                                          V5.5.1
                                                                        //                                          V5.5.5

            public string N1_NAME;                                      //      +1 NAME <NAME_OF_REPOSITORY>    {1:1}
                                                                        //                                          V5.3
                                                                        //                                          V5.5    p.41
                                                                        //                                          V5.5    p.48
                                                                        //                                          V5.5.1  p.54
                                                                        //                                          V5.5.5  p.96
            public string N1_CNTC;                                      //      +1 CNTC <NAME_OF_CONTACT_PERSON> {0:1}
                                                                        //                                          V5.3
            public string N1_SITE;                                      //  n SITE <SITE_NAME>                  {0:1}
                                                                        //                                          V5.3    p.37
            public List<ADDRESS_STRUCTURE> N1_ADDR_liste;               //      +1 <<ADDRESS_STRUCTURE>>        {0:1}
                                                                        //                                          V5.3    p.19
                                                                        //                                          V5.4    p.27
                                                                        //                                          V5.5    p.29
                                                                        //                                          V5.5.1  p.31
                                                                        //                                          V5.5.5  p.64
            public List<string> N1_PHON_liste;                          //      n PHON <PHONE_NUMBER>           {0:3}
                                                                        //                                          V5.4    p.43
                                                                        //                                          V5.5    p.50
                                                                        //                                          V5.5.1  p.57
                                                                        //                                          V5.5.5  p.99
            public List<string> N1_EMAIL_liste;                         //      n EMAIL <ADDRESS_EMAIL>             {0:3}
                                                                        //                                          V5.5.1 p.41
                                                                        //                                          V5.5.5 p.75
            public List<string> N1_FAX_liste;                           //      n FAX <ADDRESS_FAX>             {0:3}
                                                                        //                                          V5.5.1 p.41
                                                                        //                                          V5.5.5 p.75
            public List<string> N1_WWW_liste;                           //      n WWW <ADDRESS_WEB_PAGE>        {0:3}
                                                                        //                                          V5.5.1 p.42
                                                                        //                                          V5.5.5 p.76
            public string N1_MEDI;                                      //      +1 MEDI<MEDIA_TYPE>             {0:1}
                                                                        //                                          V5.3    p.18
                                                                        //[ audio | book | card | electronic | fiche | film | magazine | manuscript | map | newspaper |
                                                                        //photo | tombstone | video ]
            public string N1_CALN;                                      //      +1 CALN <SOURCE_CALL_NUMBER>    {0:1}
                                                                        //                                          V5.3    p.37
            public string N2_CALN_ITEM;                                 //          +2 ITEM <FILM_ITEM_IDENTIFICATION> {0:1}
                                                                        //                                          V5.3    p.30
            public string N2_CALN_SHEE;                                 //          +2 SHEE <SHEET_NUMBER>      {0:1}
                                                                        //                                          V5.3
            public string N2_CALN_PAGE;                                 //          +2 PAGE <PAGE_NUMBER>       {0:1}
                                                                        //                                          V5.3
            public List<string> N1_NOTE_STRUCTURE_liste_ID;             //      +1 <<NOTE_STRUCTURE>>           {0:M}
                                                                        //                                          V5.3    p.21
                                                                        //                                          V5.4    p.30
                                                                        //                                          V5.5   p.33
                                                                        //                                          V5.5.1 p.37
                                                                        //                                          V5.5.5 p.71
            public List<USER_REFERENCE_NUMBER> N1_REFN_liste;           //      +1 REFN <USER_REFERENCE_NUMBER> {0:M}
                                                                        //                                          V5.5   p.55
                                                                        //                                          V5.5.1 p.63
                                                                        //                                          V5.5.5 p.107
                                                                        //      +2 TYPE <USER_REFERENCE_TYPE>   {0:1}
                                                                        //                                          V5.5   p.55
                                                                        //                                          V5.5.1 p.64
                                                                        //                                          V5.5.5 p.107
            public string N1_RIN;                                       //      +1 RIN <AUTOMATED_RECORD_ID>    {0:1}
                                                                        //                                          V5.5   p.38
                                                                        //                                          V5.5.1 p.43
                                                                        //                                          V5.5.5 p.78
            public List<CHANGE_DATE> N1_CHAN_liste;                     //      +1 <<CHANGE_DATE>>              {0:1}
                                                                        //                                          V5.3    p.19
                                                                        //                                          V5.4    p.27
                                                                        //                                          V5.5   p.29
                                                                        //                                          V5.5.1 p.54
                                                                        //                                          V5.5.5 p.66
            public List<Ligne_perdue> ligne_perdue_liste;
        }
        public class CENS_record
        {
            // utiliser dans SOUR V5.3
            public string N1_DATE;                                      //  <CENSUS_DATE> {0:1}u
            public string N1_LINE;                                      //  <LINE_NUMBER> {0:1}u
            public string N1_DWEL;                                      //  <DWELLING_NUMBER> {0:1}u
            public string N1_FAMN;                                      //  <FAMILY_NUMBER>  { 0:1}u
            public List<string> N1_NOTE_STRUCTURE_liste_ID;             //  << NOTE_STRUCTURE >>{0:1}
        }
        public class IMMI_record
        {
            public string N1_NAME;                                      //  <NAME_OF_VESSEL> {0:1}
            public string N3_PORT_ARVL_DATE;                            //  <ARRIVAL_DATE> {0:1}
            public string N3_PORT_ARVL_PLAC;                            //  <ARRIVAL_PLACE> {0:1}
            public string N3_PORT_DPRT_DATE;                            //  <DEPARTURE_DATE> {0:1}
            public string N3_PORT_DPRT_PLAC;                            //  <DEPARTURE_PLACE> {0:1}
            public List<TEXT_STRUCTURE> N1_TEXT_liste;                  //  <<TEXT_STRUCTURE>> {0:1}
            public List<string> N1_NOTE_STRUCTURE_liste_ID;             //  <<NOTE_STRUCTURE>> {0:1}
        }
        public class ORIG_record
        {
            // utiliser dans SOUR V5.3
            public string N1_NAME;                                      //  NAME<ORIGINATOR_NAME> {0:1} up
            public string N1_TYPE;                                      //  TYPE <ORIGINATOR_TYPE> {1:1}up
            public List<string> N1_NOTE_STRUCTURE_liste_ID;             //  << NOTE_STRUCTURE >>{0:1}
        }
        public class PUBL_record
        {
            public string N1_PUBL;                                      // texte 
            public string N1_TYPE;                                      // <PUBLICATION_TYPE> {1:1}up
            public string N1_NAME;                                      //<NAME_OF_PUBLICATION> {0:1}p
            public string N1_PUBR;                                      //<PUBLISHER_NAME> {0:1}p
            public string N1_SITE;
            public List<ADDRESS_STRUCTURE> N1_ADDR_liste;
            public string N1_DATE;                                      //<PUBLICATION_DATE> {0:1}up
            public string N1_EDTN;                                      //<PUBLICATION_EDITION> {0:1}p
            public string N1_SERS;                                      //<SERIES_VOLUME_DESCRIPTION> {0:1}p
            public string N1_ISSU;                                      //<PERIODICAL_ISSUE_NUMBER> {0:1}p
            public string N1_LCCN;                                      //<LIBRARY_CONGRESS_CALL_NUMBER> {0:1}
            public List<string> N1_PHON_liste;
            public List<string> N1_FAX_liste;
            public List<string> N1_EMAIL_liste;
            public List<string> N1_WWW_liste;
        }
        public class SOURCE_CITATION
        {
            // GEDCOM53.pdf             p.23    SOURCE_STRUCTURE:=
            // GEDCOM54.pdf             p.31
            // 5.5_LDS_1996-01-02.pdf   p.34
            // 5.5.1_LDS_2019-11-15.pdf p.39
            // 5.5.5_Annotations_TJ.pdf p.73
            //                                                      SOURCE_CITATION:=
            // Le ID est placé dans N1_SOUR_liste_ID            //      n SOUR @<XREF:SOUR>@                {1:1}
            //      n SOUR <SOURCE_DESCRIPTION>         {1:1}
            //                                              V5.3
            //                                              V5.5   p.46
            //                                              V5.5   p.55
            //                                              V5.5.1 p.27
            //                                              V5.5.5 p.108
            public string N0_ID_SOUR;                           // ID de la source
            public bool source_ID_seulement;                    // indique que SOURCE_CITATION contient seulement ID de la source
            public string N0_texte;                             // texte du niveau 0

            public string N1_CLAS;                              //          +1 SOURCE_CLASSIFICATION_CODE   {1:1}up
                                                                //                                              V5.3
            public string N1_PAGE;                              //          +1 PAGE<WHERE_WITHIN_SOURCE>    {0:1}

            //                                              V5.3   p.34
            //                                              V5.4   p.48
            //                                              V5.5   p.55
            //                                              V5.5.1 p.64
            //                                              V5.5.5 p.107
            public string N1_DATE;                              //          +1 DATE <ENTRY_RECORDED_DATE>   {0:1}u
                                                                //                                              V5.3   p.34
            public string N1_EVEN;                              //          +1 EVEN<EVENT_TYPE_CITED_FROM>  {0:1}
                                                                //                                              V5.3   p.29
                                                                //                                              V5.4   p.38
                                                                //                                              V5.5   p.43
                                                                //                                              V5.5.1 p.49
                                                                //                                              V5.5.5 p.92
            public string N2_EVEN_ROLE;                         //              +2 ROLE<ROLE_IN_EVENT>      {0:1}
                                                                //                                              V5.4   p.38
                                                                //                                              V5.5   p.53
                                                                //                                              V5.5.1 p.61
                                                                //                                              V5.5.5 p.104
            public string N1_PERI;                              //          +1 PERI <TIME_PERIOD_COVERED>       {0:M}up
                                                                //          +1 DATA                         {0:1}
                                                                //                                              V5.3   p.39
            public List<string> N1_TITL_liste;                  //          +1 TITL <DESCRIPTIVE_TITLE>    {0:1}up
                                                                //                                              V5.3   p.29
            public List<string> N1_SOUR_liste_ID;               //          +1 SOUR [@XREF:SOUR@|@XREF:EVEN]    {0:M}up
                                                                //                                              V5.3
            public List<string> N1_SOUR_liste_EVEN;             //          +1 SOUR [@XREF:SOUR@|@XREF:EVEN]    {0:M}up
                                                                //                                              V5.3
            public string N2_DATA_DATE;                         //              +2 DATE<ENTRY_RECORDING_DATE> {0:1}
                                                                //                                              V5.3
                                                                //                                              V5.4   p.37
                                                                //                                              V5.5   p.43
                                                                //                                              V5.5.1 p.48
                                                                //                                              V5.5.5 p.91
            public List<TEXT_STRUCTURE> N2_DATA_TEXT_liste;     //              +2 TEXT<TEXT_FROM_SOURCE>   {0:M}
                                                                //                                              V5.4   p.47
                                                                //                                              V5.5   p.54
                                                                //                                              V5.5.1 p.63
                                                                //                                              V5.5.5 p.106
                                                                //                  +3 [CONC|CONT] <TEXT_FROM_SOURCE> {0:M}
                                                                //                                              V5.5
                                                                //                                              V5.5.1
            public List<CENS_record> N1_CENS_liste;             //      +1 CENS                                 V5.3
            public List<ORIG_record> N1_ORIG_liste;             //      +1 ORIG                                 V5.3
            public PUBL_record N1_PUBL_record;                  //      +1 PUB                                  V5.3
            public List<SOURCE_REPOSITORY_CITATION> N1_REPO_liste; //      +1 <<REPOSITORY_STRUCTURE>>         {0:1}up
                                                                   //                                              V5.3    p.18
            public IMMI_record N1_IMMI_record;                  //      +1 IMMI                                 V5.3
            public List<string> MULTIMEDIA_LINK_liste_ID;
            public List<string> N1_NOTE_STRUCTURE_liste_ID;     //          +1 <<NOTE_STRUCTURE>>           {0:M}
                                                                //                                              V5.3    p.21
                                                                //                                              V5.4    p.30
                                                                //                                              V5.5   p.33
                                                                //                                              V5.5.1 p.37
                                                                //                                              V5.5.5 p.71
            public string N1_STAT;                              //          +1 STAT <SEARCH_STATUS>         {0:1}
                                                                //                                              V5.3    p.37
            public string N2_STAT_DATE;                         //              +2 DATE <SEARCH_STATUS_DATE>{0:1}
                                                                //                                              V5.3
            public List<string> N1_REFS_liste_ID;               //          +1 REFS @XREF:SOUR@/*REFERENCED SOURCE*/{0:1}
                                                                //                                              V5.3
            public List<string> N1_REFS_liste_EVEN;             //          +1 SOUR [@XREF:SOUR@|@XREF:EVEN]    {0:M}up
                                                                //                                              V5.3
            public string N1_FIDE;                              //          +1 FIDE <SOURCE_FIDELITY_CODE>  {0:1}
                                                                //                                              V5.3    p.38
            public string N1_QUAY;                              //          +1 QUAY<CERTAINTY_ASSESSMENT>   {0:1}
                                                                //                                              V5.3    p.19
                                                                //                                              V5.4    p.35
                                                                //                                              V5.5    p.38
                                                                //                                              V5.5.1  p.43
                                                                //                                              V5.5.5  p.78
            public string N0_Titre;                             //      n SOUR <SOURCE_DESCRIPTION>         {1:1}
                                                                //                                              V5.5   p.53
                                                                //                                              V5.5.1 p.61
                                                                //          +1 [CONC|CONT] <SOURCE_DESCRIPTION>{0:M}
            public List<TEXT_STRUCTURE> N1_TEXT_liste;          //          +1 TEXT<TEXT_FROM_SOURCE>       {0:M}
                                                                //                                              V5.5   p.54
                                                                //                                              V5.5.1 p.63
                                                                //              +2 [CONC|CONT] <TEXT_FROM_SOURCE> {0:M}
                                                                //                                              V5.5
                                                                //                                              V5.5.1

            //                                              V5.3    p.18
            public string N0_ID_citation;                       // pour identifier tous les citations avec et sans ID de source

            //                                                              +1 _QUAL                        // Herisis
            public string N2__QUAL__SOUR;                       //              +2 _SOUR Qualité de la source  // Herisis
            public string N2__QUAL__INFO;                       //              +2 _INFO Qualité de l'information // Heridis
            public string N2__QUAL__EVID;                       //              +2 _EVID Qualité de la preuve  // Heridis
            public List<Ligne_perdue> ligne_perdue_liste;
        }
        public class SOURCE_RECORD
        {
            // GEDCOM54.pdf             p.24
            // 5.5_LDS_1996-01-02.pdf   p26
            // 5.5.1_LDS_2019-11-15.pdf p.27
            // 5.5.5_Annotations_TJ.pdf p.63
            //                                                              SOURCE_RECORD:=
            public int N0_numero;                                       //      n numero assigné par application
            public string N0_ID;                                        //      n @<XREF:SOUR>@ SOUR                {1:1}
                                                                        //                                              V5.4
                                                                        //                                              V5.5
                                                                        //                                              V5.5.1
                                                                        //                                              V5.5.5
                                                                        //          +1 DATA                         {0:1}
                                                                        //                                              V5.4
                                                                        //                                              V5.5
                                                                        //                                              V5.5.1
                                                                        //                                              V5.5.5
            public string N0_texte;                                     //      texte du niveau 0
            public string N1_CLAS;                                      //          +1 CLAS <SOURCE_CLASSIFICATION_CODE {1:1}
                                                                        //                                              V5.3  
                                                                        //          [ BOOK | CENSUS | CHURCH | COURT | HISTORY |
                                                                        //            INTERVIEW | JOURNAL | LAND | LETTER | 
                                                                        //            MILITARY | NEWSPAPER | PERIODICAL |
                                                                        //            PERSONAL | RECITED | TRADITION | VITAL |
                                                                        //            OTHER!<SOURCE_CLASS_DESCRIPTOR> ]
            public string N1_PERI;                                      //          +1 PERI <TIME_PERIOD_COVERED>
                                                                        //                                              V5.3
            public List<string> N1_SOUR_EVEN_liste_ID;                  //          +1 SOUR [@XREF:SOUR@|@XREF:EVEN]
                                                                        //                                              V5.3
            public string N1_PAGE;                                      //          +1 PAGE<WHERE_WITHIN_SOURCE>
            public List<CENS_record> N1_CENS_liste;                     //          +1 CENS                             V5.3
            public List<ORIG_record> N1_ORIG_liste;                     //          +1 ORIG                             V5.3
            public PUBL_record N1_PUBL_record;                          //          +1 PUB                              V5.3
            public IMMI_record N1_IMMI_record;                          //          +1 IMMI                             V5.3
            public string N2_DATA_EVEN;                                 //              +2 EVEN <EVENTS_RECORDED>   {0:M}
                                                                        //                                          V5.4   p.38
                                                                        //                                          V5.5   p.44
                                                                        //                                          V5.5.1 p.50
                                                                        //                                          V5.5.5 p.92
            public string N3_DATA_EVEN_DATE;                            //                  +3 DATE <DATE_PERIOD>   {0:1}
                                                                        //                                          V5.4   p.38
                                                                        //                                          V5.5   p.41
                                                                        //                                          V5.5.1 p.46
                                                                        //                                          V5.5.5 p.85
            public string N3_DATA_EVEN_PLAC;                            //                  +3 PLAC <SOURCE_JURISDICTION_PLACE>{0:1}
                                                                        //                                         5.4   p.46
                                                                        //                                         5.5   p.54
                                                                        //                                         V5.5.5 p.106
            public string N2_DATA_AGNC;                                 //              +2 AGNC <RESPONSIBLE_AGENCY>{0:1}
                                                                        //                                         V5.4   p.45
                                                                        //                                         V5.5   p.54
                                                                        //                                         V5.5.1 p.60
                                                                        //                                         V5.5.5 p.104
            public List<string> N2_DATA_NOTE_STRUCTURE_liste_ID;        //              +2 <<NOTE_STRUCTURE>>       {0:M}
                                                                        //                                         V5.3    p.21
                                                                        //                                         V5.4    p.30
                                                                        //                                         V5.5    p.33
                                                                        //                                         V5.5.1  p.37
                                                                        //                                         V5.5.5  p.71
            public string N1_AUTH;                                      //          +1 AUTH <SOURCE_ORIGINATOR>     {0:1}
                                                                        //                                         V5.4   p.46
                                                                        //                                         V5.5   p.54
                                                                        //                                         V5.5.1 p.62
                                                                        //                                         V5.5.5 p.106
                                                                        //              +2 [CONC|CONT] <SOURCE_ORIGINATOR>{0:M}
                                                                        //                                         V5.5    p.54
                                                                        //                                         V5.5.1 p.62
            public string N1_CPLR;                                      //                                         V5.3 p.53
            public string N1_EDTR;                                      //                                         V5.3 p.53

            public List<string> N1_TITL_liste;                          //          +1 TITL [<DESCRIPTIVE_TITLE> | @XREF:SOUR@]     {0:1} up    V5.3    p.29
                                                                        //          +1 TITL <SOURCE_DESCRIPTIVE_TITLE> {0:1}
                                                                        //                                         V5.4    p.46
                                                                        //          +1 TITL <SOURCE_DESCRIPTIVE_TITLE> {0:1}
                                                                        //                                         V5.5    p.53
                                                                        //          +1 TITL <SOURCE_DESCRIPTIVE_TITLE>              {0:1}       V5.5.1  p.62
                                                                        //          +1 TITL <SOURCE_DESCRIPTIVE_TITLE>                          V5.5.5  p.105
                                                                        //              +2 [CONT|CONC] <SOURCE_DESCRIPTIVE_TITLE>   {0:M}       V5.5    p.53
                                                                        //              +1 TITL <SOURCE_DESCRIPTIVE_TITLE>          {0:1}       5.5.1   p.62

            public string N1_ABBR;                                      //          +1 ABBR <SOURCE_FILED_BY_ENTRY> {0:1}
                                                                        //                                              V5.4    p.46
                                                                        //                                              V5.5    p.53
                                                                        //                                              V5.5.1  p.62
                                                                        //                                              V5.5.5  p.106
            public string N1_PUBL;                                      //          +1 PUBL <SOURCE_PUBLICATION_FACTS>{0:1}
                                                                        //                                              V5.4    p.46
                                                                        //                                              V5.5    p.54
                                                                        //                                              V5.5.1  p.62
                                                                        //                                              V5.5.5  p.106
                                                                        //              +2 [CONC|CONT] <SOURCE_PUBLICATION_FACTS>{0:M}
                                                                        //                                              V5.5   p.54
                                                                        //                                              V5.5.1 p.62
            public List<TEXT_STRUCTURE> N1_TEXT_liste;                        //          +1 TEXT <TEXT_FROM_SOURCE>      {0:1}
                                                                              //                                              V5.4    p.47
                                                                              //                                              V5.5    p.54
                                                                              //                                              V5.5.1  p.63
                                                                              //                                              V5.5.5  p.106
                                                                              //              +2 [CONC|CONT] <TEXT_FROM_SOURCE>{0:M}
                                                                              //                                              V5.5   p.54
                                                                              //                                              V5.5.1 p.63
            public List<SOURCE_REPOSITORY_CITATION> N1_REPO_liste;      //          +1 <<SOURCE_REPOSITORY_CITATION>>{0:M}
                                                                        //                                              V5.4    p.32
                                                                        //                                              V5.5    p.35
                                                                        //                                              V5.5.1  p.40
                                                                        //                                              V5.5.5  p.74
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
                                                                        //                                          V5.3    p.19
                                                                        //                                              V5.4   p.27
                                                                        //                                              V5.5   p.29
                                                                        //                                              V5.5.1 p.31
                                                                        //                                              V5.5.5 p.66
            public List<string> N1_NOTE_STRUCTURE_liste_ID;             //          +1 <<NOTE_STRUCTURE>>           {0:M}
                                                                        //                                              V5.3    p.21
                                                                        //                                              V5.4    p.30
                                                                        //                                              V5.5   p.33
                                                                        //                                              V5.5.1 p.37
                                                                        //                                              V5.5.5 p.71
            public string N1_STAT;                                      //          +1 STAT <SEARCH_STATUS>         {0:1}
                                                                        //                                              V5.3    p.37
            public string N2_STAT_DATE;                                 //              +2 DATE <SEARCH_STATUS_DATE>{0:1}
                                                                        //                                              V5.3    p.37
            public List<string> N1_REFS_liste_ID;                       //          +1 REFS @XREF:SOUR@/*REFERENCED SOURCE*/{0:1}
            public List<string> N1_REFS_liste_EVEN;                     //          +1 SOUR [@XREF:SOUR@|@XREF:EVEN]    {0:M}up
                                                                        //                                              V5.3
            public string N1_FIDE;                                      //          +1 FIDE <SOURCE_FIDELITY_CODE>  {0:1}
                                                                        //                                              V5.3    p.38
            public string N1_QUAY;                                      //          +1 QUAY <QUALITY_OF_DATA>       {0:1}
                                                                        //                                              V5.3    p.36
            //public List<string> N1_OBJE_LINK_liste_ID;                  //          +1 <<MULTIMEDIA_LINK>>          {0:M}
                                                                        //                                              V5.3    p.21
                                                                        //                                              V5.4    p.30
                                                                        //                                              V5.5   p.33 p.26
                                                                        //                                              V5.5.1 p.37 p.26
                                                                        //                                              v5.5.5 p.71
            /* media pour 5.3 */
            //public List<string> N1_AUDIO_liste;                         //  n AUDIO <ESCAPE_TO_AUXILLARY_PROCESSING>        {0:1} p.21
            //public List<string> N1_PHOTO_liste;                         //  n PHOTO <ESCAPE_TO_AUXILLARY_PROCESSING>        {0:1} p.21
            //public List<string> N1_VIDEO_liste;                         //  n VIDEO <ESCAPE_TO_AUXILLARY_PROCESSING>        {0:1} p.21
            public List<string> MULTIMEDIA_LINK_liste_ID;
            public string N1_EVEN;                                      //          +1 valeur I, F                      ancestrologie

            public string N1_TYPE;                                      //          +1                                  Heridis
            public string N1_DATE;                                      //          +1                                  Heridis
            public List<Ligne_perdue> ligne_perdue_liste;
        }
        public class SOURCE_REPOSITORY_CITATION
        {
            // GEDCOM53.pdf             p.21    REPOSITORY_STRUCTURE:=
            // GEDCOM54.pdf             p.32
            // 5.5_LDS_1996-01-02.pdf   p34
            // 5.5.1_LDS_2019-11-15.pdf p.40
            // 5.5.5_Annotations_TJ.pdf p.74
            //                                                  SOURCE_REPOSITORY_CITATION:=
            public string N0_ID;                                //  n REPO [ @XREF:REPO@ | <NULL>]          {1:1}
                                                                //                                              V5.3
                                                                //                                              V5.4
                                                                //                                              V5.5   p.55
                                                                //                                              V5.5.1 p.27
                                                                //                                              V5.5.5 p.108
            public string N1_NAME;                              //      +1 NAME <NAME_OF_REPOSITORY>        {0:1}
                                                                //                                              V5.3
            public string N1_CNTC;                              //      +1 CNTC<NAME_OF_CONTACT_PERSON>     {0:1}
                                                                //                                              V5.3
            public string N1_SITE;                              //
                                                                //                                              V5.3
            public List<ADDRESS_STRUCTURE> N1_ADDR_liste;       //
                                                                //      +1 <<ADDRESS_STRUCTURE>>            {0:1}
                                                                //                                              V5.3
            public List<string> N1_PHON_liste;                  //
                                                                //                                              V5.3
            public List<string> N1_EMAIL_liste;                 //
                                                                //                                              V5.3
            public List<string> N1_FAX_liste;                   //
                                                                //                                              V5.3
            public List<string> N1_WWW_liste;                   //
                                                                //                                              V5.3
            public string N1_MEDI;                              //      +1 MEDI <MEDIA_TYPE>                {0:1} 
                                                                //          [ AUDIO | BOOK | CARD | ELECTRONIC | FICHE |
                                                                //              FILM | 	MAGAZINE | MANUSCRIPT | MAP | 
                                                                //              NEWSPAPER | PHOTO | TOMBSTONE | VIDEO ]
            public List<string> N1_NOTE_STRUCTURE_liste_ID;     //      +1 <<NOTE_STRUCTURE>>               {0:M}
                                                                //                                              V5.3    p.21
                                                                //                                              V5.4    p.30
                                                                //                                              V5.5    p.33
                                                                //                                              V5.5.1  p.37
            public string N1_CALN;                              //      +1 CALN <SOURCE_CALL_NUMBER>        {0:M}
                                                                //                                              V5.3
                                                                //                                              V5.4    p.43
                                                                //                                              V5.5    p.53
                                                                //                                              V5.5.1  p.61
                                                                //                                              V5.5.5  p.105
            public string N2_CALN_ITEM;                         //          +2 ITEM <FILM_ITEM_IDENTIFICATION>{0:1}
                                                                //                                              V5.3
            public string N2_CALN_SHEE;                         //          +2 SHEE <SHEET_NUMBER>          {0:1}
                                                                //                                              V5.3
            public string N2_CALN_PAGE;                         //          +2 PAGE <PAGE_NUMBER>           {0:1}
                                                                //                                              V5.3
            public string N2_CALN_MEDI;                         //          +2 MEDI <SOURCE_MEDIA_TYPE>     {0:1}
                                                                //                                              V5.4    p.46
                                                                //                                              V5.5    p.54
                                                                //                                              V5.5.1  p.62
                                                                //                                              V5.5.5  p.106
                                                                //          [ audio | book | card | electronic | 
                                                                //              fiche | film | magazine |
                                                                //              manuscript | map | newspaper | 
                                                                //              photo | tombstone | video ]
            public string N1_REFN;                              //      +1 REFN <MANUAL_FILING_IDENTIFICATION> {0:1}
                                                                //                                              V5.3
        }
        public class SPOUSE_TO_FAMILY_LINK
        {
            // GEDCOM54.pdf             p.32
            // 5.5_LDS_1996-01-02.pdf   p35
            // 5.5.1_LDS_2019-11-15.pdf p.40
            // 5.5.5_Annotations_TJ.pdf p.75
            //                                                      SPOUSE_TO_FAMILY_LINK:=
            public string N0_ID;                                //      n FAMS @<XREF:FAM>@                 {1:1}
                                                                //                                              V5.4
                                                                //                                              V5.5   p.55
                                                                //                                              V5.5.1 p.24
                                                                //                                              V5.5.5 p.75
            public List<string> N1_NOTE_STRUCTURE_liste_ID;     //      +1 <<NOTE_STRUCTURE>>               {0:M} 
                                                                //                                              V5.3    p.21
                                                                //                                              V5.4    p.30
                                                                //                                              V5.5   p.33
                                                                //                                              V5.5.1 p.37
                                                                //                                              V5.5.5 p.71
        }
        public class SUBMISSION_RECORD
        {
            // GEDCOM54.pdf             p.24
            // 5.5_LDS_1996-01-02.pdf   p27
            // 5.5.1_LDS_2019-11-15.pdf p.28
            // 5.5.5_Annotations_TJ.pdf pas utiliser
            //                                                      SUBMISSION_RECORD:=
            public string N0_ID;                                //  n @XREF:SUBN@ SUBN                          {1:1}
                                                                //                                                  V5.4
                                                                //                                                  V5.5
                                                                //                                                  V5.5.1
            public List<string> N1_SUBM_liste_ID;               //      +1 SUBM @XREF:SUBM@                     {0:1}
                                                                //                                                  V5.4
                                                                //                                                  V5.5    p.55
                                                                //                                                  V5.5.1  p.28
            public string N1_FAMF;                              //      +1 FAMF <NAME_OF_FAMILY_FILE>           {0:1}
                                                                //                                                  V5.4    p.41
                                                                //                                                  V5.5    p.48
                                                                //                                                  V5.5.1  p.54
            public string N1_TEMP;                              //      +1 TEMP <TEMPLE_CODE>                   {0:1}
                                                                //                                                  V5.4    p.47
                                                                //                                                  V5.5    p.54
                                                                //                                                  V5.5.1  p.63
            public string N1_ANCE;                              //      +1 ANCE <GENERATIONS_OF_ANCESTORS>      {0:1} 
                                                                //                                                  V5.4    p.39
                                                                //                                                  V5.5    p.44
                                                                //                                                  V5.5.1
            public string N1_DESC;                              //      +1 DESC <GENERATIONS_OF_DESCENDANTS>    {0:1}
                                                                //                                                  V5.4    p.39
                                                                //                                                  V5.5    p.44
                                                                //                                                  V5.5.1  p.50
            public string N1_ORDI;                              //      +1 ORDI <ORDINANCE_PROCESS_FLAG>        {0:1}
                                                                //                                                  V5.4    p.43
                                                                //                                                  V5.5    p.50
                                                                //                                                  V5.5.1  p.57
            public string N1_RIN;                               //      +1 RIN <AUTOMATED_RECORD_ID>            {0:1}
                                                                //                                                  V5.5    p.38
                                                                //                                                  V5.5.1  p.43
            public List<string> N1_NOTE_STRUCTURE_liste_ID;     //      +1 <<NOTE_STRUCTURE>>                   {0:M}
                                                                //                                                  V5.3    p.21
                                                                //                                                  V5.4    p.30
                                                                //                                                  V5.5.1  p.37
                                                                //                                                  v5.5.5  p.71
                                                                //                                                  V5.5.1  p.37
            public CHANGE_DATE N1_CHAN;                         //      +1 <<CHANGE_DATE>>                      {0:1}
                                                                //                                                  V5.3    p.19
                                                                //                                                  V5.4    p.27
                                                                //                                                  V5.5.1  p.31
                                                                //                                                  V5.5.5  p.66
            public List<Ligne_perdue> ligne_perdue_liste;
        }
        public class SUBMITTER_RECORD
        {
            // GEDCOM54.pdf             p.24
            // 5.5_LDS_1996-01-02.pdf   p27
            // 5.5.1_LDS_2019-11-15.pdf p.28
            // 5.5.5_Annotations_TJ.pdf p.63
            //                                                       SUBMITTER_RECORD:=
            public string N0_ID;                                //      n @<XREF:SUBM>@ SUBM                    {1:1}
                                                                //                                                  V5.4
                                                                //                                                  V5.5
                                                                //                                                  V5.5.1
                                                                //                                                  V5.5.5
            public List<PERSONAL_NAME_STRUCTURE> N1_NAME_liste; //          +1 <<PERSONAL_NAME_STRUCTURE>>          {0:M}
                                                                //                                                  V5.4    p.31
                                                                //                                                  V5.5    p.54
                                                                //                                                  V5.5.1  p.63
                                                                //                                                  V5.5.5  p.106
            public string N1_SITE;                              //      +1 SITE <SITE_NAME>                     {0:1}
                                                                //                                                  V5.3    p.37
            public List<ADDRESS_STRUCTURE> N1_ADDR_liste;       //      +1 <<ADDRESS_STRUCTURE>>                {0:1}
                                                                //                                                  V5.4    p.27
                                                                //                                                  V5.5    p.29
                                                                //                                                  V5.5.1  p.31
                                                                //                                                  V5.5.5  p.64
            public List<string> N1_PHON_liste;                  //      n PHON <PHONE_NUMBER>                   {0:3}
                                                                //                                                  V5.4    p.43
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

            public List<string> MULTIMEDIA_LINK_liste_ID;
            public string N1_LANG;                              //      +1 LANG <LANGUAGE_PREFERENCE>           {0:3}
                                                                //                                                  V5.4    p.39
                                                                //                                                  V5.5    p.45
                                                                //                                                  V5.5.1 p.51
            public string N1_RFN;                               //      +1 RFN <SUBMITTER_REGISTERED_RFN>       {0:1}
                                                                //                                                  V5.4    p.47
                                                                //                                                  V5.5    p.54
                                                                //                                                  V5.5.1  p.63
            public string N1_RIN;                               //      +1 RIN <AUTOMATED_RECORD_ID>            {0:1}
                                                                //                                                  V5.5    p.38
                                                                //                                                  V5.5.1  p.43
            public List<string> N1_NOTE_STRUCTURE_liste_ID;     //      +1 <<NOTE_STRUCTURE>>                   {0:M}
                                                                //                                                  V5.3    p.21
                                                                //                                                  V5.4    p.30
                                                                //                                                  V5.5    p.33
                                                                //                                                  V5.5.1  p.37
                                                                //                                                  V5.5.5  p.71
                                                                //                                                  V5.5.1  p.37
            public CHANGE_DATE N1_CHAN;                         //      +1 <<CHANGE_DATE>>                      {0:1}
                                                                //                                                  V5.3    p.19
                                                                //                                                  V5.4    p.27
                                                                //                                                  V5.5    p.29
                                                                //                                                  V5.5.1  p.31
                                                                //                                                  V5.5.5  p.66
            public List<Ligne_perdue> ligne_perdue_liste;
        }
        public class TEXT_STRUCTURE
        {
            // version 5.+ utilise balise TEXT CONT CONC
            // version 5.3 utilise balise TEXT CONT CONC NOTE
            // GEDCOM53.pdf             p.24                    TEXT_STRUCTURE:=
            public string N0_TEXT;                              //  n TEXT <SOURCE_TEXT>                            {1:1}
                                                                //                                                  V5.3    p.39
                                                                //      +1 [ CONT | CONC ] <SOURCE_TEXT>            {1:M}
            public List<string> N1_NOTE_STRUCTURE_liste_ID;     //      +1 <<NOTE_STRUCTURE>>                       {0:M}
                                                                //                                                  V5.3    p.21
        }
        public class USER_REFERENCE_NUMBER
        {
            // GEDCOM54.pdf             p.47
            // 5.5_LDS_1996-01-02.pdf   
            // 5.5.1_LDS_2019-11-15.pdf p.63
            // 5.5.5_Annotations_TJ.pdf p.107
            public string N0_REFN;
            //                                          V5.4. p.47
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
        // RECORD GEDCOM
        public static HEADER info_HEADER = new HEADER(); // 551-p23
        private static List<FAM_RECORD> liste_FAM_RECORD = new List<FAM_RECORD>();  // 551-p24
        private static List<INDIVIDUAL_RECORD> liste_INDIVIDUAL_RECORD = new List<INDIVIDUAL_RECORD>(); // 551-p25
        public static List<EVEN_RECORD_53> liste_EVEN_RECORD_53 = new List<EVEN_RECORD_53>();
        private static readonly List<MULTIMEDIA_LINK> liste_MULTIMEDIA_LINK = new List<MULTIMEDIA_LINK>();
        private static List<MULTIMEDIA_RECORD> liste_MULTIMEDIA_RECORD = new List<MULTIMEDIA_RECORD>(); // 551-p26
        public static List<NOTE_STRUCTURE> liste_NOTE_STRUCTURE = new List<NOTE_STRUCTURE>();
        public static List<NOTE_RECORD> liste_NOTE_RECORD = new List<NOTE_RECORD>();
        private static List<SOURCE_RECORD> liste_SOURCE_RECORD = new List<SOURCE_RECORD>(); // 551-p27
        private static List<SUBMITTER_RECORD> liste_SUBMITTER_RECORD = new List<SUBMITTER_RECORD>(); // 551-p28
        public static readonly List<SOURCE_CITATION> liste_SOURCE_CITATION = new List<SOURCE_CITATION>();
        public static readonly List<SOURCE_CITATION> liste_SOURCE_CITATION_REFS = new List<SOURCE_CITATION>();
        private static readonly SUBMISSION_RECORD info_SUBMISSION_RECORD = new SUBMISSION_RECORD();
        private static List<REPOSITORY_RECORD> liste_REPOSITORY_RECORD = new List<REPOSITORY_RECORD>(); // 551-p27

        public static NOTE_RECORD Avoir_Info_Note(string N0_ID)
        {
            foreach (NOTE_RECORD info in liste_NOTE_RECORD)
            {
                if (N0_ID == info.N0_ID)
                    return info;
            }
            return null;
        }
        public static NOTE_STRUCTURE Avoir_Info_NOTE_STRUCTURE(string N0_ID)
        {
            foreach (NOTE_STRUCTURE info in liste_NOTE_STRUCTURE)
            {
                if (N0_ID == info.N0_ID_STRUCTURE)
                    return info;
            }
            return null;
        }

        public static int Avoir_index_repo(string N0_ID)
        {
            int index = 0;
            foreach (REPOSITORY_RECORD info in liste_REPOSITORY_RECORD)
            {
                if (N0_ID == info.N0_ID)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }
        public static MULTIMEDIA_LINK Avoir_info_MULTIMEDIA_LINK(string ID)
        {
            foreach (MULTIMEDIA_LINK info in liste_MULTIMEDIA_LINK)
            {
                if (ID == info.ID_LINK)
                {
                    return info;
                }
            }
            return null;
        }
        public static MULTIMEDIA_RECORD Avoir_info_MULTIMEDIA_RECORD(
            string ID
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            foreach (MULTIMEDIA_RECORD info in liste_MULTIMEDIA_RECORD)
            {
                if (ID == info.ID_RECORD)
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

        private static void Regler_code_erreur([CallerLineNumber] int sourceLineNumber = 0)
        {
            GH.GH.erreur = "GE" + sourceLineNumber;
        }
 
        public static (INDIVIDUAL_53, int, bool, List<Ligne_perdue>) Extraire_INDIVIDUAL_53(
            INDIVIDUAL_53 info_INDIVIDUAL_53,
            int ligne,
            int niveau,
            List<Ligne_perdue> ligne_perdue_liste
            //,[CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name +" code " + callerLineNumber + "</b><br> GEDCOM ligne="+ ligne + "<br>niveau=" + niveau);
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            bool trouver = false;
            string balise_0 = Avoir_2_balise(dataGEDCOM[ligne]);
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            if (IsNullOrEmpty(info_INDIVIDUAL_53.N0_EVEN_liste))

            {
                info_INDIVIDUAL_53.N0_EVEN_liste = new List<EVEN_STRUCTURE_53>();
            }
            string niveau_0_s = niveau.ToString();
            if (balise_0 == niveau.ToString() + " NAME")
            {
                trouver = true;
                NAME_STRUCTURE_53 info_nom;
                (info_nom, ligne, ligne_perdue_liste) = Extraire_NAME_STRUCTURE_53(ligne, niveau, ligne_perdue_liste);
                if (IsNullOrEmpty(info_INDIVIDUAL_53.N0_NAME_liste))
                {
                    List<NAME_STRUCTURE_53> temp2 = new List<NAME_STRUCTURE_53>
                    {
                        info_nom
                    };
                    info_INDIVIDUAL_53.N0_NAME_liste = temp2;
                }
                else
                    info_INDIVIDUAL_53.N0_NAME_liste.Add(info_nom);
            }
            else if (balise_0 == niveau.ToString() + " TITL")
            {
                trouver = true;
                (info_INDIVIDUAL_53.N0_TITL, ligne) = Extraire_texte_niveau_plus(ligne);
            }
            else if (balise_0 == niveau.ToString() + " SEX")
            {
                trouver = true;
                (info_INDIVIDUAL_53.N0_SEX, ligne) = Extraire_texte_niveau_plus(ligne);
                info_INDIVIDUAL_53.N0_SEX = info_INDIVIDUAL_53.N0_SEX.ToUpper();
            }
            else if (balise_0 == niveau.ToString() + " ADOP" ||
                balise_0 == niveau.ToString() + " BIRT" ||
                balise_0 == niveau.ToString() + " BAPM" ||
                balise_0 == niveau.ToString() + " BARM" ||
                balise_0 == niveau.ToString() + " BASM" ||
                balise_0 == niveau.ToString() + " BLES" ||
                balise_0 == niveau.ToString() + " BURI" ||
                balise_0 == niveau.ToString() + " CENS" ||
                balise_0 == niveau.ToString() + " CHR" ||
                balise_0 == niveau.ToString() + " CHRA" ||
                balise_0 == niveau.ToString() + " CONF" ||
                balise_0 == niveau.ToString() + " DEAT" ||
                balise_0 == niveau.ToString() + " EVEN" ||
                balise_0 == niveau.ToString() + " EMIG" ||
                balise_0 == niveau.ToString() + " GRAD" ||
                balise_0 == niveau.ToString() + " IMMI" ||
                balise_0 == niveau.ToString() + " MARR" ||
                balise_0 == niveau.ToString() + " NATU" ||
                balise_0 == niveau.ToString() + " ORDN" ||
                balise_0 == niveau.ToString() + " RETI" ||
                balise_0 == niveau.ToString() + " MARB" ||
                balise_0 == niveau.ToString() + " MARC" ||
                balise_0 == niveau.ToString() + " MARL" ||
                balise_0 == niveau.ToString() + " MARS" ||
                balise_0 == niveau.ToString() + " ENGA" ||
                balise_0 == niveau.ToString() + " ANUL" ||
                balise_0 == niveau.ToString() + " DIV" ||
                balise_0 == niveau.ToString() + " DIVF"
                )
            {
                trouver = true;
                EVEN_STRUCTURE_53 temp;
                (temp, ligne) = Extraire_EVEN_STRUCTURE_53(ligne, niveau);
                info_INDIVIDUAL_53.N0_EVEN_liste.Add(temp);
            }
            else if (balise_0 == niveau.ToString() + " ADDR")
            {
                trouver = true;
                ADDRESS_STRUCTURE N1_ADDR;
                (N1_ADDR, ligne, ligne_perdue_liste) = Extraire_ADDRESS_STRUCTURE(ligne, ligne_perdue_liste);
                if (IsNullOrEmpty(info_INDIVIDUAL_53.N0_ADDR_liste))
                {
                    List<ADDRESS_STRUCTURE> temp3 = new List<ADDRESS_STRUCTURE>
                        {
                            N1_ADDR
                        };
                    info_INDIVIDUAL_53.N0_ADDR_liste = temp3;
                }
                else
                    info_INDIVIDUAL_53.N0_ADDR_liste.Add(N1_ADDR);
            }
            else if (balise_0 == niveau.ToString() + " RELI")
            {
                trouver = true;
                (info_INDIVIDUAL_53.N0_RELI, ligne) = Extraire_texte_niveau_plus(ligne);
            }
            else if (balise_0 == niveau.ToString() + " NAMR")
            {
                trouver = true;
                (info_INDIVIDUAL_53.N0_NAMR, ligne) = Extraire_texte_niveau_plus(ligne);
                while (Extraire_niveau(ligne) > niveau)
                {
                    string balise_1 = Avoir_2_balise(dataGEDCOM[ligne]);
                    int balise_1_ligne = ligne;
                    if (balise_1 == (niveau + 1).ToString() + " RELI")
                    {
                        (info_INDIVIDUAL_53.N1_NAMR_RELI, ligne) = Extraire_texte_niveau_plus(ligne);
                    }
                    else
                    {
                        // pour la page HTML
                        Ligne_perdue aa = new Ligne_perdue
                        {
                            ligne = ligne,
                            texte = dataGEDCOM[ligne]
                        };
                        ligne_perdue_liste.Add(aa);
                        // rapport Balise
                        method = MethodBase.GetCurrentMethod().Name;
                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                        Ligne_perdue a0 = new Ligne_perdue
                        {
                            ligne = balise_0_ligne,
                            texte = dataGEDCOM[balise_0_ligne]
                        };
                        groupe_ligne.Add(a0);
                        Ligne_perdue a1 = new Ligne_perdue
                        {
                            ligne = balise_1_ligne,
                            texte = dataGEDCOM[balise_1_ligne]
                        };
                        groupe_ligne.Add(a1);
                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                        groupe_ligne.Clear();
                    }

                    switch (balise_1)
                    {
                        case "2 RELI":                                                            // RELI
                            (info_INDIVIDUAL_53.N1_NAMR_RELI, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        default:
                            // pour la page HTML
                            Ligne_perdue aa = new Ligne_perdue
                            {
                                ligne = ligne,
                                texte = dataGEDCOM[ligne]
                            };
                            ligne_perdue_liste.Add(aa);
                            // rapport Balise
                            method = MethodBase.GetCurrentMethod().Name;
                            lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                            Ligne_perdue a0 = new Ligne_perdue
                            {
                                ligne = balise_0_ligne,
                                texte = dataGEDCOM[balise_0_ligne]
                            };
                            groupe_ligne.Add(a0);
                            Ligne_perdue a1 = new Ligne_perdue
                            {
                                ligne = balise_1_ligne,
                                texte = dataGEDCOM[balise_1_ligne]
                            };
                            groupe_ligne.Add(a1);
                            ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                            groupe_ligne.Clear();
                            break;
                    }
                }
            }
            else if (balise_0 == niveau.ToString() + " EDUC")
            {
                trouver = true;
                (info_INDIVIDUAL_53.N0_EDUC, ligne) = Extraire_texte_niveau_plus(ligne);
            }
            else if (balise_0 == niveau.ToString() + " SSN")
            {
                trouver = true;
                (info_INDIVIDUAL_53.N0_SSN, ligne) = Extraire_texte_niveau_plus(ligne);
            }
            else if (balise_0 == niveau.ToString() + " IDNO")
            {
                trouver = true;
                (info_INDIVIDUAL_53.N0_IDNO, ligne) = Extraire_texte_niveau_plus(ligne);
                while (Extraire_niveau(ligne) > niveau)
                {
                    string balise_1 = Avoir_2_balise(dataGEDCOM[ligne]);
                    int balise_1_ligne = ligne;
                    if (balise_1 == (niveau + 1).ToString() + " TYPE")
                    {
                        (info_INDIVIDUAL_53.N1_IDNO_TYPE, ligne) = Extraire_texte_niveau_plus(ligne);
                    }
                    else
                    {
                        // pour la page HTML
                        Ligne_perdue bb = new Ligne_perdue
                        {
                            ligne = ligne,
                            texte = dataGEDCOM[ligne]
                        };
                        ligne_perdue_liste.Add(bb);
                        // rapport Balise
                        method = MethodBase.GetCurrentMethod().Name;
                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                        Ligne_perdue b0 = new Ligne_perdue
                        {
                            ligne = balise_0_ligne,
                            texte = dataGEDCOM[balise_0_ligne]
                        };
                        groupe_ligne.Add(b0);
                        Ligne_perdue b1 = new Ligne_perdue
                        {
                            ligne = balise_1_ligne,
                            texte = dataGEDCOM[balise_1_ligne]
                        };
                        groupe_ligne.Add(b1);
                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                        groupe_ligne.Clear();
                    }


                    switch (balise_1)
                    {
                        case "2 TYPE":                                                                // TYPE
                            (info_INDIVIDUAL_53.N1_IDNO_TYPE, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        default:
                            // pour la page HTML
                            Ligne_perdue bb = new Ligne_perdue
                            {
                                ligne = ligne,
                                texte = dataGEDCOM[ligne]
                            };
                            ligne_perdue_liste.Add(bb);
                            // rapport Balise
                            method = MethodBase.GetCurrentMethod().Name;
                            lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                            Ligne_perdue b0 = new Ligne_perdue
                            {
                                ligne = balise_0_ligne,
                                texte = dataGEDCOM[balise_0_ligne]
                            };
                            groupe_ligne.Add(b0);
                            Ligne_perdue b1 = new Ligne_perdue
                            {
                                ligne = balise_1_ligne,
                                texte = dataGEDCOM[balise_1_ligne]
                            };
                            groupe_ligne.Add(b1);
                            ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                            groupe_ligne.Clear();
                            break;
                    }
                }
            }
            else if (balise_0 == niveau.ToString() + " PROP")
            {
                trouver = true;
                (info_INDIVIDUAL_53.N0_PROP, ligne) = Extraire_texte_niveau_plus(ligne);
            }
            else if (balise_0 == niveau.ToString() + " DSCR")
            {
                trouver = true;
                (info_INDIVIDUAL_53.N0_DSCR, ligne) = Extraire_texte_niveau_plus(ligne);
            }
            else if (balise_0 == niveau.ToString() + " SIGN")
            {
                trouver = true;
                (info_INDIVIDUAL_53.N0_SIGN, ligne) = Extraire_texte_niveau_plus(ligne);
            }
            else if (balise_0 == niveau.ToString() + " NMR")
            {
                trouver = true;
                (info_INDIVIDUAL_53.N0_NMR, ligne) = Extraire_texte_niveau_plus(ligne);
            }
            else if (balise_0 == niveau.ToString() + " NCHI")
            {
                trouver = true;
                (info_INDIVIDUAL_53.N0_NCHI, ligne) = Extraire_texte_niveau_plus(ligne);
            }
            else if (balise_0 == niveau.ToString() + " NATI")
            {
                trouver = true;
                (info_INDIVIDUAL_53.N0_NATI, ligne) = Extraire_texte_niveau_plus(ligne);
            }
            else if (balise_0 == niveau.ToString() + " NATI")
            {
                trouver = true;
                (info_INDIVIDUAL_53.N0_CAST, ligne) = Extraire_texte_niveau_plus(ligne);
            }
            else
            {
                // pour la page HTML
                Ligne_perdue bb = new Ligne_perdue
                {
                    ligne = ligne,
                    texte = dataGEDCOM[ligne]
                };
                ligne_perdue_liste.Add(bb);
                // rapport Balise
                method = MethodBase.GetCurrentMethod().Name;
                lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                Ligne_perdue b0 = new Ligne_perdue
                {
                    ligne = balise_0_ligne,
                    texte = dataGEDCOM[balise_0_ligne]
                };
                groupe_ligne.Add(b0);
                ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                groupe_ligne.Clear();
            }
                ////////    

            info_INDIVIDUAL_53.ligne_perdue_liste = ligne_perdue_liste;
            return (info_INDIVIDUAL_53, ligne, trouver, ligne_perdue_liste);
        }

        private static (LDS_INDIVIDUAL_ORDINANCE, int) Extraire_LDS_INDIVIDUAL_ORDINANCE_even(
            int ligne)
        {
            List<Ligne_perdue> ligne_perdue_liste = new List<Ligne_perdue>();
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            LDS_INDIVIDUAL_ORDINANCE info = new LDS_INDIVIDUAL_ORDINANCE
            {
                N0_EVEN = null,
                N1_TYPE = null,
                N1_DATE = null,
                DATE_trier = "99999999",
                N1_TEMP = null,
                N1_PLAC = null,
                N1_FAMC = null,
                N1_STAT = null,
                N2_STAT_DATE = null,
                N1_NOTE_STRUCTURE_liste_ID = new List<string>(),
                N1_SOUR_citation_liste_ID = new List<string>(),
                N1_SOUR_source_liste_ID = new List<string>(),
            };
            int niveau = Extraire_niveau(ligne);
            info.N0_EVEN = Avoir_balise(dataGEDCOM[ligne]);
            //string balise_0 = info.N0_EVEN;
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            ligne++;
            while (Extraire_niveau(ligne) > niveau)
            {
                string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                int balise_1_ligne = ligne;
                switch (balise_1)
                {
                    case "TYPE":                                                                        // TYPE
                        (info.N1_TYPE, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "DATE":                                                                        // DATE
                        (info.N1_DATE, ligne) = Extraire_texte_niveau_plus(ligne);
                        info.DATE_trier = Convertir_date_trier(info.N1_DATE);
                        break;
                    case "TEMP":                                                                        // TEMP
                        (info.N1_TEMP, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "FAMC":                                                                        // FAMC
                        info.N1_FAMC = Extraire_ID(dataGEDCOM[ligne]);
                        ligne++;
                        break;
                    case "PLAC":                                                                        // PLAC
                        (info.N1_PLAC, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "STAT":                                                                        // STAT
                        (info.N1_STAT, ligne) = Extraire_texte_niveau_plus(ligne);
                        while (Extraire_niveau(ligne) > niveau + 1)
                        {
                            string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                            int balise_2_ligne = ligne;
                            switch (balise_2)
                            {
                                case "DATE":                                                            // STAT_DATE
                                    (info.N2_STAT_DATE, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                default:
                                    // pour la page HTML
                                    Ligne_perdue aa = new Ligne_perdue()
                                    {
                                        ligne = balise_0_ligne,
                                        texte = dataGEDCOM[ligne]
                                    };
                                    ligne_perdue_liste.Add(aa);
                                    // rapport Balise
                                    method = MethodBase.GetCurrentMethod().Name;
                                    lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                    Ligne_perdue a0 = new Ligne_perdue
                                    {
                                        ligne = balise_0_ligne,
                                        texte = dataGEDCOM[balise_0_ligne]
                                    };
                                    groupe_ligne.Add(a0);
                                    Ligne_perdue a1 = new Ligne_perdue()
                                    {
                                        ligne = balise_1_ligne,
                                        texte = dataGEDCOM[balise_1_ligne]
                                    };
                                    groupe_ligne.Add(a1);
                                    Ligne_perdue a2 = new Ligne_perdue()
                                    {
                                        ligne = balise_2_ligne,
                                        texte = dataGEDCOM[balise_2_ligne]
                                    };
                                    groupe_ligne.Add(a2);
                                    ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                    groupe_ligne.Clear();
                                    break;
                            }
                        }
                        break;
                    case "NOTE":                                                                        // NOTE
                        string IDNote;
                        (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                        info.N1_NOTE_STRUCTURE_liste_ID.Add(IDNote);
                        break;
                    case "SOUR":                                                                        // SOUR
                        string citation;
                        string source;
                        (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                        if (IsNotNullOrEmpty(citation)) info.N1_SOUR_citation_liste_ID.Add(citation);
                        if (IsNotNullOrEmpty(source) && IsNullOrEmpty(citation)) info.N1_SOUR_source_liste_ID.Add(source);
                        break;
                    default:
                        // pour la page HTML
                        Ligne_perdue cc = new Ligne_perdue()
                        {
                            ligne = ligne,
                            texte = dataGEDCOM[ligne]
                        };
                        ligne_perdue_liste.Add(cc);
                        // rapport Balise
                        method = MethodBase.GetCurrentMethod().Name;
                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                        Ligne_perdue c0 = new Ligne_perdue()
                        {
                            ligne = balise_0_ligne,
                            texte = dataGEDCOM[balise_0_ligne]
                        };
                        groupe_ligne.Add(c0);
                        Ligne_perdue c1 = new Ligne_perdue()
                        {
                            ligne = balise_1_ligne,
                            texte = dataGEDCOM[balise_1_ligne]
                        };
                        groupe_ligne.Add(c1);
                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                        groupe_ligne.Clear();
                        break;
                }
                info.ligne_perdue_liste = ligne_perdue_liste;
            }
            return (info, ligne);
        }

        private static (LDS_SPOUSE_SEALING, int, List<Ligne_perdue>) Extraire_LDS_SPOUSE_SEALING(
            int ligne,
            List<Ligne_perdue> ligne_perdue_liste
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b> GEDCOM=");
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            LDS_SPOUSE_SEALING info = new LDS_SPOUSE_SEALING
            {
                N1_DATE = null,
                N1_TEMP = null,
                N1_PLAC = null,
                N1_STAT = null,
                N2_STAT_DATE = null,
                N1_NOTE_STRUCTURE_liste_ID = new List<string>(),
                N1_SOUR_citation_liste_ID = new List<string>(),
                N1_SOUR_source_liste_ID = new List<string>()
            };
            int niveau_I = Extraire_niveau(ligne);
            //string balise_0 = Avoir_balise(dataGEDCOM[ligne]);
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            ligne++;
            while (Extraire_niveau(ligne) > niveau_I)
            {
                string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                int balise_1_ligne = ligne;
                switch (balise_1)
                {
                    case "TYPE":                                                                        // TYPE
                        (info.N1_TYPE, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "DATE":                                                                        // DATE
                        (info.N1_DATE, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "TEMP":                                                                        // TEMP
                        (info.N1_TEMP, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "PLAC":                                                                        // PLAC
                        (info.N1_PLAC, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "STAT":                                                                        // TYPE V5.3
                        (info.N1_STAT, ligne) = Extraire_texte_niveau_plus(ligne);
                        while (Extraire_niveau(ligne) > niveau_I + 1)
                        {
                            string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                            int balise_2_ligne = ligne;
                            switch (balise_2)
                            {
                                case "DATE":                                                            // STAT_DATE
                                    (info.N2_STAT_DATE, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                default:
                                    // pour la page HTML
                                    Ligne_perdue aa = new Ligne_perdue()
                                    {
                                        ligne = ligne,
                                        texte = dataGEDCOM[ligne]
                                    };
                                    ligne_perdue_liste.Add(aa);
                                    // rapport Balise
                                    method = MethodBase.GetCurrentMethod().Name;
                                    lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                    Ligne_perdue a0 = new Ligne_perdue()
                                    {
                                        ligne = balise_0_ligne,
                                        texte = dataGEDCOM[balise_0_ligne]
                                    };
                                    groupe_ligne.Add(a0);
                                    Ligne_perdue a1 = new Ligne_perdue()
                                    {
                                        ligne = balise_1_ligne,
                                        texte = dataGEDCOM[balise_1_ligne]
                                    };
                                    groupe_ligne.Add(a1);
                                    Ligne_perdue a2 = new Ligne_perdue()
                                    {
                                        ligne = balise_2_ligne,
                                        texte = dataGEDCOM[balise_2_ligne]
                                    };
                                    groupe_ligne.Add(a2);
                                    ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                    groupe_ligne.Clear();
                                    break;
                            }
                        }
                        break;
                    case "NOTE":                                                                        // NOTE
                        string IDNote;
                        (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                        info.N1_NOTE_STRUCTURE_liste_ID.Add(IDNote);
                        break;
                    case "SOUR":                                                                        // SOUR
                        string citation;
                        string source;
                        (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                        if (IsNotNullOrEmpty(citation))
                        {
                            info.N1_SOUR_citation_liste_ID.Add(citation);
                        }
                        if (IsNotNullOrEmpty(source) && IsNullOrEmpty(citation))
                        {
                            info.N1_SOUR_source_liste_ID.Add(source);
                        }
                        break;
                    default:
                        // pour la page HTML
                        Ligne_perdue bb = new Ligne_perdue()
                        {
                            ligne = ligne,
                            texte = dataGEDCOM[ligne]
                        };
                        ligne_perdue_liste.Add(bb);
                        // rapport Balise
                        method = MethodBase.GetCurrentMethod().Name;
                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                        Ligne_perdue b0 = new Ligne_perdue()
                        {
                            ligne = balise_0_ligne,
                            texte = dataGEDCOM[balise_0_ligne]
                        };
                        groupe_ligne.Add(b0);
                        Ligne_perdue b1 = new Ligne_perdue()
                        {
                            ligne = balise_1_ligne,
                            texte = dataGEDCOM[balise_1_ligne]
                        };
                        groupe_ligne.Add(b1);
                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                        groupe_ligne.Clear();
                        break;
                }
            }
            return (info, ligne, ligne_perdue_liste);
        }

        private static (
            string, // ID
            int, // ligne
            List<Ligne_perdue>
            ) Extraire_MULTIMEDIA_LINK(
            int ligne,
            List<Ligne_perdue> ligne_perdue_liste
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b> GEDCOM=" + ligne );

            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            //string balise_0 = Avoir_balise(dataGEDCOM[ligne]);
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            MULTIMEDIA_LINK info_LINK = new MULTIMEDIA_LINK
            {
                ID_RECORD = Extraire_ID(dataGEDCOM[ligne])
            };
            info_LINK.ID_RECORD = Extraire_ID(dataGEDCOM[ligne]);
            info_LINK.NOTE_STRUCTURE_liste_ID = new List<string>();
            info_LINK.ID_LINK = "IA" + String.Format("{0:-00-00-00-00}", ++numero_ID);
            // niveau 1
            int niveau = Extraire_niveau(ligne);
            ligne++;
            info_LINK.ID_seul = true;
            
            while (Extraire_niveau(ligne) == niveau + 1)
            {
                string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                int balise_1_ligne = ligne;
                switch (balise_1)
                {
                    case "FILE":                                                                        // FILLE
                        (info_LINK.FILE, ligne) = Extraire_texte_niveau_plus(ligne);
                        info_LINK.ID_seul = false;
                        while (Extraire_niveau(ligne) > niveau + 1)
                        {
                            string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                            int balise_2_ligne = ligne;
                            switch (balise_2)
                            {
                                case "FORM":                                                            // FORM
                                    (info_LINK.FORM, ligne) = Extraire_texte_niveau_plus(ligne);
                                    while (Extraire_niveau(ligne) > niveau + 2)
                                    {
                                        string balise_3 = Avoir_balise(dataGEDCOM[ligne]);
                                        int balise_3_ligne = ligne;
                                        switch (balise_3)
                                        {
                                            case "MEDI":                                // MEDI V5.5.1
                                                                                        //  MEDI V5.5.1 même chose que TYPE V5.5.5
                                                (info_LINK.MEDI, ligne) = Extraire_texte_niveau_plus(ligne);
                                                break;
                                            default:
                                                // pour la page HTML
                                                Ligne_perdue aa = new Ligne_perdue()
                                                {
                                                    ligne = ligne,
                                                    texte = dataGEDCOM[ligne]
                                                };
                                                ligne_perdue_liste.Add(aa);
                                                // rapport Balise
                                                method = MethodBase.GetCurrentMethod().Name;
                                                lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                                Ligne_perdue a0 = new Ligne_perdue()
                                                {
                                                    ligne = balise_0_ligne,
                                                    texte = dataGEDCOM[balise_0_ligne]
                                                };
                                                groupe_ligne.Add(a0);
                                                Ligne_perdue a1 = new Ligne_perdue()
                                                {
                                                    ligne = balise_1_ligne,
                                                    texte = dataGEDCOM[balise_1_ligne]
                                                };
                                                groupe_ligne.Add(a1);
                                                Ligne_perdue a2 = new Ligne_perdue()
                                                {
                                                    ligne = balise_2_ligne,
                                                    texte = dataGEDCOM[balise_2_ligne]
                                                };
                                                groupe_ligne.Add(a2);
                                                Ligne_perdue a3 = new Ligne_perdue()
                                                {
                                                    ligne = balise_3_ligne,
                                                    texte = dataGEDCOM[balise_3_ligne]
                                                };
                                                groupe_ligne.Add(a3);
                                                ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                                groupe_ligne.Clear();
                                                break;
                                        }
                                    }
                                    break;
                                default:
                                    // pour la page HTML
                                    Ligne_perdue bb = new Ligne_perdue()
                                    {
                                        ligne = ligne,
                                        texte = dataGEDCOM[ligne]
                                    };
                                    ligne_perdue_liste.Add(bb);
                                    // rapport Balise
                                    method = MethodBase.GetCurrentMethod().Name;
                                    lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                    Ligne_perdue b0 = new Ligne_perdue()
                                    {
                                        ligne = balise_0_ligne,
                                        texte = dataGEDCOM[balise_0_ligne]
                                    };
                                    groupe_ligne.Add(b0);
                                    Ligne_perdue b1 = new Ligne_perdue()
                                    {
                                        ligne = balise_1_ligne,
                                        texte = dataGEDCOM[balise_1_ligne]
                                    };
                                    groupe_ligne.Add(b1);
                                    Ligne_perdue b2 = new Ligne_perdue()
                                    {
                                        ligne = balise_2_ligne,
                                        texte = dataGEDCOM[balise_2_ligne]
                                    };
                                    groupe_ligne.Add(b2);
                                    ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                    groupe_ligne.Clear();
                                    break;
                            }
                        }
                        break;
                    case "FORM":                                                                        // FORM
                        info_LINK.ID_seul = false;
                        (info_LINK.FORM, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "TITL":                                                                        // TITL
                        info_LINK.ID_seul = false;
                        (info_LINK.TITL, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "NOTE":                                                                        // NOTE
                        info_LINK.ID_seul = false;
                        string IDNote;
                        (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                        info_LINK.NOTE_STRUCTURE_liste_ID.Add(IDNote);
                        break;
                    default:
                        // pour la page HTML
                        Ligne_perdue cc = new Ligne_perdue()
                        {
                            ligne = ligne,
                            texte = dataGEDCOM[ligne]
                        };
                        ligne_perdue_liste.Add(cc);
                        // rapport Balise
                        method = MethodBase.GetCurrentMethod().Name;
                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                        Ligne_perdue c0 = new Ligne_perdue()
                        {
                            ligne = balise_0_ligne,
                            texte = dataGEDCOM[balise_0_ligne]
                        };
                        groupe_ligne.Add(c0);
                        Ligne_perdue c1 = new Ligne_perdue()
                        {
                            ligne = balise_1_ligne,
                            texte = dataGEDCOM[balise_1_ligne]
                        };
                        groupe_ligne.Add(c1);
                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                        groupe_ligne.Clear();
                        break;
                }
            }
            liste_MULTIMEDIA_LINK.Add(info_LINK);
            //R..Z("retour L=" + info_LINK.ID_LINK + " R=" + info_LINK.ID_RECORD);
            return (info_LINK.ID_LINK, ligne, ligne_perdue_liste);
        }

        private static int Extraire_OBJE_record(
            int ligne  // MULTIMEDIA_RECORD
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b> GEDCOM=" + ligne);
            List<Ligne_perdue> ligne_perdue_liste = new List<Ligne_perdue>();
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            //string balise_0 = Avoir_balise(dataGEDCOM[ligne]);
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            string N0_ID;
            string N1_FORM = null; // v5.5
            string N1_TITL; // v5.5
            string N1_FILE = null;
            string N2_FILE_FORM;
            string N3_FILE_FORM_TYPE = null;
            string N2_FILE_TITL = null;
            List<USER_REFERENCE_NUMBER> N1_REFN_liste = new List<USER_REFERENCE_NUMBER>();
            string N1_RIN = null;
            List<string> N1_NOTE_STRUCTURE_liste_ID = new List<string>();
            string N1_BLOB = null; // v5.5
            string N1__DATE = null;// Herisis 

            List<string> N1_SOUR_citation_liste_ID = new List<string>();
            List<string> N1_SOUR_source_liste_ID = new List<string>();
            CHANGE_DATE N1_CHAN = null;
            N0_ID = Extraire_ID(dataGEDCOM[ligne]);
            ligne++;
            try
            {
                while (Extraire_niveau(ligne) > 0)
                {
                    string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                    int balise_1_ligne = ligne;
                    switch (balise_1)
                    {
                        case "FORM":                                                                    // FORM v5.
                            (N1_FORM, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "TITL":                                                                    // TITL v5.5
                            (N1_TITL, ligne) = Extraire_texte_niveau_plus(ligne);
                            break; ;
                        case "FILE":                                                                    //FILLE
                            (N1_FILE, ligne) = Extraire_texte_niveau_plus(ligne);
                            while (Extraire_niveau(ligne) > 1)
                            {
                                string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                                int balise_2_ligne = ligne;
                                switch (balise_2)
                                {
                                    case "FORM":                                                            // FORM
                                        (N2_FILE_FORM, ligne) = Extraire_texte_niveau_plus(ligne);
                                        while (Extraire_niveau(ligne) > 2)
                                        {
                                            string balise_3 = Avoir_balise(dataGEDCOM[ligne]);
                                            switch (balise_3)
                                            {
                                                case "TYPE":                                            // 3 FORM TYPE V5.5.5
                                                    (N3_FILE_FORM_TYPE, ligne) = Extraire_texte_niveau_plus(ligne);
                                                    break;
                                                default:
                                                    // pour la page HTML
                                                    Ligne_perdue aa = new Ligne_perdue()
                                                    {
                                                        ligne = ligne,
                                                        texte = dataGEDCOM[ligne]
                                                    };
                                                    ligne_perdue_liste.Add(aa);
                                                    // rapport Balise
                                                    method = MethodBase.GetCurrentMethod().Name;
                                                    lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                                    Ligne_perdue a0 = new Ligne_perdue()
                                                    {
                                                        ligne = balise_0_ligne,
                                                        texte = dataGEDCOM[balise_0_ligne]
                                                    };
                                                    groupe_ligne.Add(a0);
                                                    Ligne_perdue a1 = new Ligne_perdue()
                                                    {
                                                        ligne = balise_1_ligne,
                                                        texte = dataGEDCOM[balise_1_ligne]
                                                    };
                                                    groupe_ligne.Add(a1);
                                                    Ligne_perdue a2 = new Ligne_perdue()
                                                    {
                                                        ligne = balise_2_ligne,
                                                        texte = dataGEDCOM[balise_2_ligne]
                                                    };
                                                    groupe_ligne.Add(a2);
                                                    ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                                    groupe_ligne.Clear();
                                                    break;
                                            }
                                        }
                                        break;
                                    case "TITL":                                                        // TITL
                                        (N2_FILE_TITL, ligne) = Extraire_texte_niveau_plus(ligne);
                                        break;
                                    default:
                                        // pour la page HTML
                                        Ligne_perdue cc = new Ligne_perdue()
                                        {
                                            ligne = ligne,
                                            texte = dataGEDCOM[ligne]
                                        };
                                        ligne_perdue_liste.Add(cc);
                                        // rapport Balise
                                        method = MethodBase.GetCurrentMethod().Name;
                                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                        Ligne_perdue c0 = new Ligne_perdue()
                                        {
                                            ligne = balise_0_ligne,
                                            texte = dataGEDCOM[balise_0_ligne]
                                        };
                                        groupe_ligne.Add(c0);
                                        Ligne_perdue c1 = new Ligne_perdue()
                                        {
                                            ligne = balise_1_ligne,
                                            texte = dataGEDCOM[balise_1_ligne]
                                        };
                                        groupe_ligne.Add(c1);
                                        Ligne_perdue c2 = new Ligne_perdue()
                                        {
                                            ligne = balise_2_ligne,
                                            texte = dataGEDCOM[balise_2_ligne]
                                        };
                                        groupe_ligne.Add(c2);
                                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                        groupe_ligne.Clear();
                                        break;
                                }
                            }
                            break;
                        case "REFN":                                                                    // REFN
                            USER_REFERENCE_NUMBER N1_REFN;
                            (N1_REFN, ligne, ligne_perdue_liste) = Extraire_USER_REFERENCE_NUMBER(ligne, ligne_perdue_liste);
                            N1_REFN_liste.Add(N1_REFN);
                            break;
                        case "RIN":                                                                     // RIN
                            (N1_RIN, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "NOTE":                                                                    // NOTE
                            string IDNote;
                            (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                            N1_NOTE_STRUCTURE_liste_ID.Add(IDNote);
                            break;
                        case "BLOB":                                                                    // BLOB V5.4 V5.5
                            N1_BLOB = "blob";
                            N1_FILE = "blob.blob";
                            N1_FORM = "blob";
                            N2_FILE_FORM = "blob";
                            ligne++;
                            while (Extraire_niveau(ligne) > 1)
                            {
                                string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                                switch (balise_2)
                                {
                                    case "CONT":                                                            // CONT
                                        ligne++;
                                        break;
                                    default:
                                        // pour la page HTML
                                        Ligne_perdue dd = new Ligne_perdue()
                                        {
                                            ligne = ligne,
                                            texte = dataGEDCOM[ligne]
                                        };
                                        ligne_perdue_liste.Add(dd);
                                        // rapport Balise
                                        method = MethodBase.GetCurrentMethod().Name;
                                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                        Ligne_perdue d0 = new Ligne_perdue()
                                        {
                                            ligne = balise_0_ligne,
                                            texte = dataGEDCOM[balise_0_ligne]
                                        };
                                        groupe_ligne.Add(d0);
                                        Ligne_perdue d1 = new Ligne_perdue()
                                        {
                                            ligne = balise_1_ligne,
                                            texte = dataGEDCOM[balise_1_ligne]
                                        };
                                        groupe_ligne.Add(d1);
                                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                        groupe_ligne.Clear();
                                        break;
                                }
                            }
                            break;
                        case "EOBJ":                                                                    // EOBJ V5.4
                            ligne++;
                            break;
                        case "OBJE":                                                                    // OBJE v5.5
                            ligne++;
                            while (Extraire_niveau(ligne) > 1)
                            {
                                // pour la page HTML
                                Ligne_perdue ee = new Ligne_perdue()
                                {
                                    ligne = ligne,
                                    texte = dataGEDCOM[ligne]
                                };
                                ligne_perdue_liste.Add(ee);
                                // rapport Balise
                                method = MethodBase.GetCurrentMethod().Name;
                                lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                Ligne_perdue e0 = new Ligne_perdue()
                                {
                                    ligne = balise_0_ligne,
                                    texte = dataGEDCOM[balise_0_ligne]
                                };
                                groupe_ligne.Add(e0);
                                Ligne_perdue e1 = new Ligne_perdue()
                                {
                                    ligne = balise_1_ligne,
                                    texte = dataGEDCOM[balise_1_ligne]
                                };
                                groupe_ligne.Add(e1);
                                ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                groupe_ligne.Clear();
                            }
                            break;
                        case "SOUR":                                                                     // SOUR
                            string citation;
                            string source;
                            (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                            if (IsNotNullOrEmpty(citation)) N1_SOUR_citation_liste_ID.Add(citation);
                            if (IsNotNullOrEmpty(source) && IsNullOrEmpty(citation))
                                N1_SOUR_source_liste_ID.Add(source);
                            break;
                        case "CHAN":                                                                    // CHAN
                            (N1_CHAN, ligne, _) = Extraire_CHANGE_DATE(ligne, null);
                            break;
                        case "DATE":                                                                    // _DATE Heridis
                            (N1__DATE, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        default:
                            // pour la page HTML
                            Ligne_perdue ff = new Ligne_perdue()
                            {
                                ligne = ligne,
                                texte = dataGEDCOM[ligne]
                            };
                            ligne_perdue_liste.Add(ff);
                            // rapport Balise
                            method = MethodBase.GetCurrentMethod().Name;
                            lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                            Ligne_perdue f0 = new Ligne_perdue()
                            {
                                ligne = balise_0_ligne,
                                texte = dataGEDCOM[balise_0_ligne]
                            };
                            groupe_ligne.Add(f0);
                            Ligne_perdue f1 = new Ligne_perdue()
                            {
                                ligne = balise_1_ligne,
                                texte = dataGEDCOM[balise_1_ligne]
                            };
                            groupe_ligne.Add(f1);
                            ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                            groupe_ligne.Clear();
                            break;
                    }
                }
            }
            catch (Exception msg)
            {
                GC.Collect();
                string message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                Voir_message(message, msg.Message, GH.GH.erreur);
            }
            if (!GH.GH.annuler)
            {
                liste_MULTIMEDIA_RECORD.Add(new MULTIMEDIA_RECORD()
                {
                    ID_RECORD = N0_ID,
                    FORM = N1_FORM,
                    //N1_TITL = N1_TITL,
                    FILE = N1_FILE,
                    //FORM = N2_FILE_FORM,
                    FORM_TYPE = N3_FILE_FORM_TYPE,
                    TITL = N2_FILE_TITL,
                    REFN_liste = N1_REFN_liste,
                    RIN = N1_RIN,
                    NOTE_STRUCTURE_liste_ID = N1_NOTE_STRUCTURE_liste_ID,
                    BLOB = N1_BLOB,
                    //N1_OBJE = N1_OBJE,
                    SOUR_citation_liste_ID = N1_SOUR_citation_liste_ID,
                    SOUR_source_liste_ID = N1_SOUR_source_liste_ID,
                    CHAN = N1_CHAN,
                    Herisis_DATE = N1__DATE, // Herisis
                    ligne_perdue_liste = ligne_perdue_liste
    });
            }
            return ligne;
        }

        private static int Extraire_SOURCE_RECORD(
            int ligne
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            string N0_ID;
            string N0_texte;
            (_, N0_ID, _, N0_texte, ligne) = Extraire_info_niveau_0(ligne);
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + "<br>code " + callerLineNumber + "</b>" + "<br>ligne=" + ligne + "<br>N0_ID=" + N0_ID + "<br> N0_texte=" + N0_texte);
            Regler_code_erreur();
            List<Ligne_perdue> ligne_perdue_liste = new List<Ligne_perdue>();
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            
            string N1_CLAS = null;
            string N1_PERI = null;
            List<string> N1_SOUR_EVEN_liste_ID = new List<string>();
            string N1_PAGE = null;
            List<CENS_record> N1_CENS_liste = new List<CENS_record>(); //V5.3
            List<ORIG_record> N1_ORIG_liste = new List<ORIG_record>(); //V5.3
            PUBL_record N1_PUBL_record = null; //V5.3
            IMMI_record N1_IMMI_record = null; //V5.3
            string N1_STAT = null; //V5.3
            string N2_STAT_DATE = null; //V5.3
            List<string> N1_REFS_liste_EVEN = new List<string>();
            List<string> N1_REFS_liste_ID = new List<string>(); //V5.3
            string N1_FIDE = null;  //V5.3
            string N2_DATA_EVEN = null;
            string N3_DATA_EVEN_DATE = null;
            string N3_DATA_EVEN_PLAC = null;
            string N2_DATA_AGNC = null;
            List<string> N2_DATA_NOTE_STRUCTURE_liste_ID = new List<string>();
            string N1_AUTH = null;
            string N1_CPLR = null;
            string N1_EDTR = null;
            List<string> N1_TITL_liste = new List<string>();
            string N1_ABBR = null;

            List<TEXT_STRUCTURE> N1_TEXT_liste = new List<TEXT_STRUCTURE>();
            string N1_QUAY = null; // V5.3
            string N1_TYPE = null; // Heridis
            string N1_DATE = null; // Herisis
            List<SOURCE_REPOSITORY_CITATION> N1_REPO_liste = new List<SOURCE_REPOSITORY_CITATION>();
            List<USER_REFERENCE_NUMBER> N1_REFN_liste = new List<USER_REFERENCE_NUMBER>();
            string N1_RIN = null;
            CHANGE_DATE N1_CHAN = new CHANGE_DATE
            {
                N1_CHAN_DATE = null,
                N2_CHAN_DATE_TIME = null
            };
            List<string> N1_NOTE_STRUCTURE_liste_ID = new List<string>();
            List<string> MULTIMEDIA_LINK_liste_ID = new List<string>();
            string N1_EVEN = null; // acestrologie valeur I, F
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;

            // niveau 0
            if (Extraire_niveau(ligne) > 0)
            {
                while (Extraire_niveau(ligne) > 0)
                {
                    try
                    {
                        string balise_1 =  Avoir_2_balise(dataGEDCOM[ligne]);
                        int balise_1_ligne = ligne;
                        switch (balise_1)
                        {
                            case "1 CLAS":                                                                // CLAS
                                (N1_CLAS, ligne) = Extraire_texte_niveau_plus(ligne);
                                break;
                            case "1 PERI":                                                                // PERI V5.3
                                (N1_PERI, ligne) = Extraire_texte_niveau_plus(ligne);
                                break;
                            case "1 SOUR":                                                                // SOUR V5.3
                                N1_SOUR_EVEN_liste_ID.Add(Extraire_ID(dataGEDCOM[ligne]));
                                ligne++;
                                break;
                            case "1 PAGE":                                                                // PAGE
                                (N1_PAGE, ligne) = Extraire_texte_niveau_plus(ligne);
                                break;
                            case "1 CENS":                                                                // CENS v5.3
                                CENS_record info_CENS = null;
                                (info_CENS, ligne, ligne_perdue_liste) = Extraire_CENS_record(ligne, ligne_perdue_liste);
                                N1_CENS_liste.Add(info_CENS);
                                break;
                            case "1 ORIG":                                                                // ORIG v5.3
                                ORIG_record info_ORIG = null;
                                (info_ORIG, ligne, ligne_perdue_liste) = Extraire_ORIG_record(ligne, ligne_perdue_liste);
                                N1_ORIG_liste.Add(info_ORIG);
                                break;
                            case "1 PUBL":                                                                // PUBL v5.3
                                (N1_PUBL_record, ligne, ligne_perdue_liste) = Extraire_PUBL_record(ligne, ligne_perdue_liste);
                                break;
                            case "1 IMMI":                                                                // IMMI V5.3
                            case "1 EMIG":
                                // La documentation de DRAFT Release 5.3 du 4 November 1993 à la page 24, il manque
                                // la balise pour la section immigration. Probable que la balise soit IMMI ou EMIG
                                // recherche sur le web à rien donner, à investiquer.
                                (N1_IMMI_record, ligne, ligne_perdue_liste) = Extraire_IMMI_record(ligne, ligne_perdue_liste);
                                break;
                            case "1 STAT":                                                                // STAT
                                (N1_STAT, ligne) = Extraire_texte_niveau_plus(ligne);
                                while (Extraire_niveau(ligne) > 1)
                                {
                                    string balise_2 = Avoir_2_balise(dataGEDCOM[ligne]);
                                    int balise_2_ligne = ligne;
                                    switch (balise_2)
                                    {
                                        case "2 DATE":                                                    // DATE
                                            (N2_STAT_DATE, ligne) = Extraire_texte_niveau_plus(ligne);
                                            break;
                                        default:
                                            // pour la page HTML
                                            Ligne_perdue aa = new Ligne_perdue()
                                            {
                                                ligne = ligne,
                                                texte = dataGEDCOM[ligne]
                                            };
                                            ligne_perdue_liste.Add(aa);
                                            // rapport Balise
                                            method = MethodBase.GetCurrentMethod().Name;
                                            lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                            Ligne_perdue a0 = new Ligne_perdue()
                                            {
                                                ligne = balise_0_ligne,
                                                texte = dataGEDCOM[balise_0_ligne]
                                            };
                                            groupe_ligne.Add(a0);
                                            Ligne_perdue a1 = new Ligne_perdue()
                                            {
                                                ligne = balise_1_ligne,
                                                texte = dataGEDCOM[balise_1_ligne]
                                            };
                                            groupe_ligne.Add(a1);
                                            Ligne_perdue a2 = new Ligne_perdue()
                                            {
                                                ligne = balise_2_ligne,
                                                texte = dataGEDCOM[balise_2_ligne]
                                            };
                                            groupe_ligne.Add(a2);
                                            ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                            groupe_ligne.Clear();
                                            break;
                                    }
                                }
                                break;
                            case "1 REFS":                                                                // REFS
                                string ID = Extraire_ID(dataGEDCOM[ligne]);
                                if (IsNotNullOrEmpty(ID))
                                {
                                    N1_REFS_liste_ID.Add(ID);
                                    ligne++;
                                }
                                else
                                {
                                    string refs;
                                    (refs, ligne) = Extraire_texte_niveau_plus(ligne);
                                    N1_REFS_liste_EVEN.Add(refs);
                                }
                                break;
                            case "1 FIDE":                                                                // FIDE
                                (N1_FIDE, ligne) = Extraire_texte_niveau_plus(ligne);
                                break;
                            case "1 DATA":                                                                // DATA
                                ligne++;
                                while (Extraire_niveau(ligne) > 1)
                                {
                                    string balise_2 = Avoir_2_balise(dataGEDCOM[ligne]);
                                    int balise_2_ligne = ligne;
                                    switch (balise_2)
                                    {
                                        case "2 EVEN":                                                    // DATA_EVEN  BIRT, DEAT, MARR.
                                            Avoir_balise_p1(dataGEDCOM[ligne]);
                                            string even;
                                            (even, ligne) = Extraire_texte_niveau_plus(ligne);
                                            N2_DATA_EVEN = Extraire_EVEN_liste(even);
                                            while (Extraire_niveau(ligne) > 2)
                                            {
                                                string balise_3 = Avoir_balise(dataGEDCOM[ligne]);
                                                switch (balise_3)
                                                {
                                                    case "DATE":                                           // DATE
                                                        (N3_DATA_EVEN_DATE, ligne) = Extraire_texte_niveau_plus(ligne);
                                                        break;
                                                    case "PLAC":                                            // PLAC
                                                        (N3_DATA_EVEN_PLAC, ligne) = Extraire_texte_niveau_plus(ligne);
                                                        break;
                                                    default:
                                                        // pour la page HTML
                                                        Ligne_perdue bb = new Ligne_perdue()
                                                        {
                                                            ligne = ligne,
                                                            texte = dataGEDCOM[ligne]
                                                        };
                                                        ligne_perdue_liste.Add(bb);
                                                        // rapport Balise
                                                        method = MethodBase.GetCurrentMethod().Name;
                                                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                                        Ligne_perdue b0 = new Ligne_perdue()
                                                        {
                                                            ligne = balise_0_ligne,
                                                            texte = dataGEDCOM[balise_0_ligne]
                                                        };
                                                        groupe_ligne.Add(b0);
                                                        Ligne_perdue b1 = new Ligne_perdue()
                                                        {
                                                            ligne = balise_1_ligne,
                                                            texte = dataGEDCOM[balise_1_ligne]
                                                        };
                                                        groupe_ligne.Add(b1);
                                                        Ligne_perdue b2 = new Ligne_perdue()
                                                        {
                                                            ligne = balise_2_ligne,
                                                            texte = dataGEDCOM[balise_2_ligne]
                                                        };
                                                        groupe_ligne.Add(b2);
                                                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                                        groupe_ligne.Clear();
                                                        break;
                                                }
                                            }
                                            break;
                                        case "2 AGNC":                                                    // AGNC
                                            (N2_DATA_AGNC, ligne) = Extraire_texte_niveau_plus(ligne);
                                            break;
                                        case "2 NOTE":                                                    // NOTE
                                            string temp2;
                                            (temp2, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                                            N2_DATA_NOTE_STRUCTURE_liste_ID.Add(temp2);
                                            break;
                                        default:
                                            // pour la page HTML
                                            Ligne_perdue cc = new Ligne_perdue()
                                            {
                                                ligne = ligne,
                                                texte = dataGEDCOM[ligne]
                                            };
                                            ligne_perdue_liste.Add(cc);
                                            // rapport Balise
                                            method = MethodBase.GetCurrentMethod().Name;
                                            lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                            Ligne_perdue c0 = new Ligne_perdue()
                                            {
                                                ligne = balise_0_ligne,
                                                texte = dataGEDCOM[balise_0_ligne]
                                            };
                                            groupe_ligne.Add(c0);
                                            Ligne_perdue c1 = new Ligne_perdue()
                                            {
                                                ligne = balise_1_ligne,
                                                texte = dataGEDCOM[balise_1_ligne]
                                            };
                                            groupe_ligne.Add(c1);
                                            Ligne_perdue c2 = new Ligne_perdue()
                                            {
                                                ligne = balise_2_ligne,
                                                texte = dataGEDCOM[balise_2_ligne]
                                            };
                                            groupe_ligne.Add(c2);
                                            ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                            groupe_ligne.Clear();
                                            break;
                                    }
                                }
                                break;
                            case "1 DATE":                                                                // DATE Herisis
                                (N1_DATE, ligne) = Extraire_texte_niveau_plus(ligne);
                                break;
                            case "1 TITL":                                                               // TITL
                                string temp3 = null;
                                (temp3, ligne) = Extraire_texte_niveau_plus(ligne);
                                N1_TITL_liste.Add(temp3);
                                break;
                            case "1 AUTH":                                                                // 1 AUTH
                                (N1_AUTH, ligne) = Extraire_texte_niveau_plus(ligne);
                                break;
                            case "1 TEXT":                                                               // TEXT
                                TEXT_STRUCTURE temp;
                                (temp, ligne, ligne_perdue_liste) = Extraire_TEXT_STRUCTURE(ligne, ligne_perdue_liste);
                                N1_TEXT_liste.Add(temp);
                                break;
                            case "1 REFN":                                                                // REFN
                                USER_REFERENCE_NUMBER N1_REFN;
                                (N1_REFN, ligne, ligne_perdue_liste) = Extraire_USER_REFERENCE_NUMBER(ligne, ligne_perdue_liste);
                                N1_REFN_liste.Add(N1_REFN);
                                break;
                            case "1 ABBR":                                                                // ABBR
                                (N1_ABBR, ligne) = Extraire_texte_niveau_plus(ligne);
                                break;
                            case "1 OBJE":                                                                // OBJE
                                string ID_OBJE;
                                (ID_OBJE, ligne, ligne_perdue_liste) = Extraire_MULTIMEDIA_LINK(ligne, ligne_perdue_liste);
                                MULTIMEDIA_LINK_liste_ID.Add(ID_OBJE);
                                break;
                            case "1 AUDIO":                                                               // AUDIO V5.3
                                string audio;
                                (audio, ligne) = Extraire_texte_niveau_plus(ligne);
                                MULTIMEDIA_LINK_liste_ID.Add(audio);
                                break;
                            case "1 PHOTO":                                                               // PHOTO V5.3
                                string photo;
                                (photo, ligne) = Extraire_texte_niveau_plus(ligne);
                                MULTIMEDIA_LINK_liste_ID.Add(photo);
                                break;
                            case "1 VIDEO":                                                               // VIDEO V5.3
                                string video;
                                (video, ligne) = Extraire_texte_niveau_plus(ligne);
                                MULTIMEDIA_LINK_liste_ID.Add(video);
                                break;
                            case "1 REPO":                                                                // REPO
                                SOURCE_REPOSITORY_CITATION info_REPO;
                                (info_REPO, ligne) = Extraire_SOURCE_REPOSITORY_CITATION(ligne, ligne_perdue_liste);
                                N1_REPO_liste.Add(info_REPO);
                                break;
                            case "1 NOTE":                                                                // NOTE
                                string IDNote;
                                (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                                N1_NOTE_STRUCTURE_liste_ID.Add(IDNote);
                                break;
                            case "1 CHAN":                                                                // CHAN
                                (N1_CHAN, ligne, ligne_perdue_liste) = Extraire_CHANGE_DATE(ligne, ligne_perdue_liste);
                                break;
                            case "1 RIN":                                                                 // RIN
                                (N1_RIN, ligne) = Extraire_texte_niveau_plus(ligne);
                                break;
                            case "1 EVEN":                                                                // 1 EVEN dans ancestrologie
                                (N1_EVEN, ligne) = Extraire_texte_niveau_plus(ligne);
                                break;
                            case "1 QUAY":                                                                // 1 QUAY Heridis
                                (N1_QUAY, ligne) = Extraire_texte_niveau_plus(ligne);
                                break;
                            case "1 CPLR":
                                (N1_CPLR, ligne) = Extraire_texte_niveau_plus(ligne);
                                break;
                            case "1 EDTR":
                                (N1_EDTR, ligne) = Extraire_texte_niveau_plus(ligne);
                                break;
                            case "1 TYPE":                                                                // 1 TYPE Heridis
                                (N1_TYPE, ligne) = Extraire_texte_niveau_plus(ligne);
                                switch (N1_TYPE.ToUpper())
                                {
                                    case "ADMINISTRATIVE DOCUMENT": N1_TYPE = "Document administratif"; break;
                                    case "DEED": N1_TYPE = "Acte"; break;
                                    case "OTHER": N1_TYPE = "Autre"; break;
                                }
                                break;
                            default:
                                // pour la page HTML
                                Ligne_perdue ff = new Ligne_perdue()
                                {
                                    ligne = ligne,
                                    texte = dataGEDCOM[ligne]
                                };
                                ligne_perdue_liste.Add(ff);
                                // rapport Balise
                                method = MethodBase.GetCurrentMethod().Name;
                                lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                Ligne_perdue f0 = new Ligne_perdue()
                                {
                                    ligne = balise_0_ligne,
                                    texte = dataGEDCOM[balise_0_ligne]
                                };
                                groupe_ligne.Add(f0);
                                Ligne_perdue f1 = new Ligne_perdue
                                {
                                    ligne = balise_1_ligne,
                                    texte = dataGEDCOM[balise_1_ligne]
                                };
                                groupe_ligne.Add(f1);
                                ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                groupe_ligne.Clear();
                                break;
                        }
                    }
                    catch (Exception msg)
                    {
                        GC.Collect();
                        string message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                        Voir_message(message, msg.Message, GH.GH.erreur);
                        if (GH.GH.annuler)
                        {
                            return ligne;
                        }
                    }
                }
            }
            liste_SOURCE_RECORD.Add(new SOURCE_RECORD()
            {

                N0_ID = N0_ID,
                N0_texte = N0_texte,
                N1_CLAS = N1_CLAS,
                N1_PERI = N1_PERI,

                N1_SOUR_EVEN_liste_ID = N1_SOUR_EVEN_liste_ID,
                N2_DATA_EVEN = N2_DATA_EVEN,
                N1_PAGE = N1_PAGE,
                N1_CENS_liste = N1_CENS_liste,
                N1_ORIG_liste = N1_ORIG_liste,
                N1_PUBL_record = N1_PUBL_record,
                N1_IMMI_record = N1_IMMI_record,
                N1_STAT = N1_STAT,
                N2_STAT_DATE = N2_STAT_DATE,
                N1_REFS_liste_EVEN = N1_REFS_liste_EVEN,
                N1_REFS_liste_ID = N1_REFS_liste_ID,
                N1_FIDE = N1_FIDE,
                N3_DATA_EVEN_DATE = N3_DATA_EVEN_DATE,
                N3_DATA_EVEN_PLAC = N3_DATA_EVEN_PLAC,
                N2_DATA_AGNC = N2_DATA_AGNC,
                N2_DATA_NOTE_STRUCTURE_liste_ID = N2_DATA_NOTE_STRUCTURE_liste_ID,
                N1_AUTH = N1_AUTH,
                N1_CPLR = N1_CPLR,
                N1_EDTR = N1_EDTR,
                N1_TITL_liste = N1_TITL_liste,
                N1_ABBR = N1_ABBR,
                N1_TEXT_liste = N1_TEXT_liste,
                N1_REPO_liste = N1_REPO_liste,
                N1_REFN_liste = N1_REFN_liste,
                N1_RIN = N1_RIN,
                N1_CHAN = N1_CHAN,
                N1_NOTE_STRUCTURE_liste_ID = N1_NOTE_STRUCTURE_liste_ID,
                MULTIMEDIA_LINK_liste_ID = MULTIMEDIA_LINK_liste_ID,
                N1_EVEN = N1_EVEN,
                N1_QUAY = N1_QUAY,
                N1_TYPE = N1_TYPE,
                N1_DATE = N1_DATE,
                ligne_perdue_liste = ligne_perdue_liste
            });
            //R..Z("Retour de Extraire_SOURCE_RECORD " + ligne);
            return ligne;
        }

        public static (PERSONAL_NAME_STRUCTURE, int, List<Ligne_perdue>) Extraire_PERSONAL_NAME_STRUCTURE(
            int ligne,
            List<Ligne_perdue> ligne_perdue_liste
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b> GEDCOM=" + ligne );
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            int niveau = Extraire_niveau(ligne);
            //string balise_0 = Avoir_balise(dataGEDCOM[ligne]);
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            GEDCOMClass.PERSONAL_NAME_PIECES N1_nom_name_pieces = new PERSONAL_NAME_PIECES();
            GEDCOMClass.PERSONAL_NAME_PIECES N1_FONE_name_pieces = new PERSONAL_NAME_PIECES();
            GEDCOMClass.PERSONAL_NAME_PIECES N1_ROMN_name_pieces = new PERSONAL_NAME_PIECES();
            GEDCOMClass.PERSONAL_NAME_STRUCTURE info_nom = new PERSONAL_NAME_STRUCTURE();
            List<string> Nn_NOTE_STRUCTURE_liste_ID = new List<string>();
            List<string> Nn_SOUR_citation_liste_ID = new List<string>();
            List<string> Nn_SOUR_source_liste_ID = new List<string>();
            List<string> Nn_FONE_NOTE_STRUCTURE_liste_ID = new List<string>();
            List<string> Nn_FONE_SOUR_citation_liste_ID = new List<string>();
            List<string> Nn_FONE_SOUR_source_liste_ID = new List<string>();
            List<string> Nn_ROMN_NOTE_STRUCTURE_liste_ID = new List<string>();
            List<string> Nn_ROMN_SOUR_citation_liste_ID = new List<string>();
            List<string> Nn_ROMN_SOUR_source_liste_ID = new List<string>();
            List<string> alia_liste = new List<string>();
            (info_nom.N0_NAME, ligne) = Extraire_texte_niveau_plus(ligne);
            info_nom.N0_NAME = Extraire_NAME(info_nom.N0_NAME);
            while (Extraire_niveau(ligne) > niveau)
            {
                string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                int balise_1_ligne = ligne;
                switch (balise_1)
                {
                    case "TYPE":                                                                        // Type
                        (info_nom.N1_TYPE, ligne) = Extraire_texte_niveau_plus(ligne);
                        if (info_nom.N1_TYPE.ToLower() == "aka") info_nom.N1_TYPE = "Alias";
                        if (info_nom.N1_TYPE.ToLower() == "birth") info_nom.N1_TYPE = "A la naissance";
                        if (info_nom.N1_TYPE.ToLower() == "immigrant") info_nom.N1_TYPE = "À immigrant";
                        if (info_nom.N1_TYPE.ToLower() == "maiden") info_nom.N1_TYPE = "Avant le premier mariage";
                        if (info_nom.N1_TYPE.ToLower() == "married") info_nom.N1_TYPE = "Au mariage";
                        if (info_nom.N1_TYPE.ToLower() == "user_defined") info_nom.N1_TYPE = "Par l'utilisateur";
                        break;
                    case "NPFX":                                                                        // NPFX
                        (N1_nom_name_pieces.Nn_NPFX, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "GIVN":                                                                        // GIVN
                        (N1_nom_name_pieces.Nn_GIVN, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "NICK":                                                                        // NICK
                        (N1_nom_name_pieces.Nn_NICK, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "SPFX":                                                                        // SPFX
                        (N1_nom_name_pieces.Nn_SPFX, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "SURN":                                                                        // SURN
                        (N1_nom_name_pieces.Nn_SURN, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "NSFX":                                                                        // NSFX
                        (N1_nom_name_pieces.Nn_NSFX, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "NOTE":                                                                        // NOTE
                        string IDNote;
                        (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                        Nn_NOTE_STRUCTURE_liste_ID.Add(IDNote);
                        break;
                    case "SOUR":                                                                        // SOUR
                        string ID_citation;
                        string ID_source;
                        (
                            ID_citation, // ID_citation
                            ID_source, // ID_source
                            ligne // ligne
                        ) = Extraire_SOURCE_CITATION(ligne);
                        if (IsNotNullOrEmpty(ID_citation)) Nn_SOUR_citation_liste_ID.Add(ID_citation);
                        if (IsNotNullOrEmpty(ID_source) && IsNullOrEmpty(ID_citation))
                        {
                            Nn_SOUR_source_liste_ID.Add(ID_source);
                        }
                        break;
                    case "FONE":                                                                        // FONE
                        (info_nom.N1_FONE, ligne) = Extraire_texte_niveau_plus(ligne);
                        info_nom.N1_FONE = Extraire_NAME(info_nom.N1_FONE);
                        while (Extraire_niveau(ligne) > niveau + 1)
                        {
                            string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                            int balise_2_ligne = ligne;
                            switch (balise_2)
                            {
                                case "TYPE":                                                            // FONE TYPE
                                    (info_nom.N2_FONE_TYPE, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                case "NPFX":                                                            // FONE NPFX
                                    (N1_FONE_name_pieces.Nn_NPFX, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                case "GIVN":                                                            // FONE GIVN
                                    (N1_FONE_name_pieces.Nn_GIVN, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                case "NICK":                                                            // FONE NICK
                                    (N1_FONE_name_pieces.Nn_NICK, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                case "SPFX":                                                            // FONE SPFX
                                    (N1_FONE_name_pieces.Nn_SPFX, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                case "SURN":                                                            // FONE SURN
                                    (N1_FONE_name_pieces.Nn_SURN, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                case "NSFX":                                                            // FONE NSFX
                                    (N1_FONE_name_pieces.Nn_NSFX, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                case "NOTE":                                                            // FONE NOTE
                                    string temp;
                                    (temp, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                                    Nn_FONE_NOTE_STRUCTURE_liste_ID.Add(temp);
                                    break;
                                case "SOUR":                                                            // FONE SOUR
                                    string citation;
                                    string source;
                                    (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                                    if (IsNotNullOrEmpty(citation)) Nn_FONE_SOUR_citation_liste_ID.Add(citation);
                                    if (IsNotNullOrEmpty(source) && IsNullOrEmpty(citation)) Nn_FONE_SOUR_source_liste_ID.Add(source);
                                    break;
                                default:
                                    // pour la page HTML
                                    Ligne_perdue aa = new Ligne_perdue()
                                    {
                                        ligne = ligne,
                                        texte = dataGEDCOM[ligne]
                                    };
                                    ligne_perdue_liste.Add(aa);
                                    // rapport Balise
                                    method = MethodBase.GetCurrentMethod().Name;
                                    lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                    Ligne_perdue a0 = new Ligne_perdue()
                                    {
                                        ligne = balise_0_ligne,
                                        texte = dataGEDCOM[balise_0_ligne]
                                    };
                                    groupe_ligne.Add(a0);
                                    Ligne_perdue a1 = new Ligne_perdue()
                                    {
                                        ligne = balise_1_ligne,
                                        texte = dataGEDCOM[balise_1_ligne]
                                    };
                                    groupe_ligne.Add(a1);
                                    Ligne_perdue a2 = new Ligne_perdue()
                                    {
                                        ligne = balise_2_ligne,
                                        texte = dataGEDCOM[balise_2_ligne]
                                    };
                                    groupe_ligne.Add(a2);
                                    ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                    groupe_ligne.Clear();
                                    break;
                            }
                        }
                        break;
                    case "ROMN":                                                                        // ROMN
                        (info_nom.N1_ROMN, ligne) = Extraire_texte_niveau_plus(ligne);
                        info_nom.N1_ROMN = Extraire_NAME(info_nom.N1_ROMN);
                        while (Extraire_niveau(ligne) > 2)
                        {
                            string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                            int balise_2_ligne = ligne;
                            switch (balise_2)
                            {
                                case "TYPE":                                                            // ROMN TYPE
                                    (info_nom.N2_ROMN_TYPE, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                case "NPFX":                                                            // ROMN NPFX
                                    (N1_ROMN_name_pieces.Nn_NPFX, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                case "GIVN":                                                            // ROMN GIVN
                                    (N1_ROMN_name_pieces.Nn_GIVN, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                case "NICK":                                                            // ROMN  NICK
                                    (N1_ROMN_name_pieces.Nn_NICK, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                case "SPFX":                                                            // ROMN SPFX
                                    (N1_ROMN_name_pieces.Nn_SPFX, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                case "SURN":                                                            // ROMN SURN
                                    (N1_ROMN_name_pieces.Nn_SURN, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                case "NSFX":                                                            // ROMN NSFX
                                    (N1_ROMN_name_pieces.Nn_NSFX, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                case "NOTE":                                                            // ROMN NOTE
                                    string temp2;
                                    (temp2, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                                    Nn_ROMN_NOTE_STRUCTURE_liste_ID.Add(temp2);
                                    break;
                                case "SOUR":                                                            // ROMN SOUR
                                    string citation;
                                    string source;
                                    (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                                    if (IsNotNullOrEmpty(citation)) Nn_ROMN_SOUR_citation_liste_ID.Add(citation);
                                    if (IsNotNullOrEmpty(source) && IsNullOrEmpty(citation)) Nn_ROMN_SOUR_source_liste_ID.Add(source);
                                    break;
                                default:
                                    // pour la page HTML
                                    Ligne_perdue bb = new Ligne_perdue()
                                    {
                                        ligne = ligne,
                                        texte = dataGEDCOM[ligne]
                                    };
                                    ligne_perdue_liste.Add(bb);
                                    // rapport Balise
                                    method = MethodBase.GetCurrentMethod().Name;
                                    lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                    Ligne_perdue b0 = new Ligne_perdue()
                                    {
                                        ligne = balise_0_ligne,
                                        texte = dataGEDCOM[balise_0_ligne]
                                    };
                                    groupe_ligne.Add(b0);
                                    Ligne_perdue b1 = new Ligne_perdue()
                                    {
                                        ligne = balise_1_ligne,
                                        texte = dataGEDCOM[balise_1_ligne]
                                    };
                                    groupe_ligne.Add(b1);
                                    Ligne_perdue b2 = new Ligne_perdue()
                                    {
                                        ligne = balise_2_ligne,
                                        texte = dataGEDCOM[balise_2_ligne]
                                    };
                                    groupe_ligne.Add(b2);
                                    ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                    groupe_ligne.Clear();
                                    break;
                            }
                        }
                        info_nom.N1_PERSONAL_NAME_PIECES = N1_nom_name_pieces;
                        info_nom.N1_FONE_name_pieces = N1_FONE_name_pieces;
                        info_nom.N1_ROMN_name_pieces = N1_ROMN_name_pieces;
                        break;
                    case "ALIA":                                                                        // ALIA BROSKEEP
                        string alia;
                        (alia, ligne) = Extraire_texte_niveau_plus(ligne);
                        alia = Extraire_NAME(alia);
                        alia_liste.Add(alia);
                        break;
                    default:
                        // pour la page HTML
                        Ligne_perdue cc = new Ligne_perdue()
                        {
                            ligne = ligne,
                            texte = dataGEDCOM[ligne]
                        };
                        ligne_perdue_liste.Add(cc);
                        // rapport Balise
                        method = MethodBase.GetCurrentMethod().Name;
                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                        Ligne_perdue c0 = new Ligne_perdue()
                        {
                            ligne = balise_0_ligne,
                            texte = dataGEDCOM[balise_0_ligne]
                        };
                        groupe_ligne.Add(c0);
                        Ligne_perdue c1 = new Ligne_perdue()
                        {
                            ligne = balise_1_ligne,
                            texte = dataGEDCOM[balise_1_ligne]
                        };
                        groupe_ligne.Add(c1);
                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                        groupe_ligne.Clear();
                        break;
                }
            }
            N1_nom_name_pieces.Nn_NOTE_STRUCTURE_liste_ID = Nn_NOTE_STRUCTURE_liste_ID;
            N1_nom_name_pieces.Nn_SOUR_citation_liste_ID = Nn_SOUR_citation_liste_ID;
            N1_nom_name_pieces.Nn_SOUR_source_liste_ID = Nn_SOUR_source_liste_ID;
            N1_FONE_name_pieces.Nn_NOTE_STRUCTURE_liste_ID = Nn_FONE_NOTE_STRUCTURE_liste_ID;
            N1_FONE_name_pieces.Nn_SOUR_citation_liste_ID = Nn_FONE_SOUR_citation_liste_ID;
            N1_FONE_name_pieces.Nn_SOUR_source_liste_ID = Nn_FONE_SOUR_source_liste_ID;
            N1_ROMN_name_pieces.Nn_NOTE_STRUCTURE_liste_ID = Nn_ROMN_NOTE_STRUCTURE_liste_ID;
            N1_ROMN_name_pieces.Nn_SOUR_citation_liste_ID = Nn_ROMN_SOUR_citation_liste_ID;
            N1_ROMN_name_pieces.Nn_SOUR_source_liste_ID = Nn_ROMN_SOUR_source_liste_ID;
            PERSONAL_NAME_STRUCTURE info_PERSONAL_NAME_STRUCTURE = new PERSONAL_NAME_STRUCTURE
            {
                N0_NAME = info_nom.N0_NAME,
                N1_TYPE = info_nom.N1_TYPE,
                N1_PERSONAL_NAME_PIECES = N1_nom_name_pieces,
                N1_FONE = info_nom.N1_FONE,
                N2_FONE_TYPE = info_nom.N2_FONE_TYPE,
                N1_FONE_name_pieces = N1_FONE_name_pieces,
                N1_ROMN = info_nom.N1_ROMN,
                N2_ROMN_TYPE = info_nom.N2_ROMN_TYPE,
                N1_ROMN_name_pieces = N1_ROMN_name_pieces,
                N1_ALIA_liste = alia_liste
            };
            return (info_PERSONAL_NAME_STRUCTURE, ligne, ligne_perdue_liste);
        }

        private static (PLACE_STRUCTURE, int, List<Ligne_perdue>) Extraire_PLACE_STRUCTURE(
            int ligne,
            List<Ligne_perdue> ligne_perdue_liste
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            int niveau = Extraire_niveau(ligne);
            //GEDCOMClass.ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b> GEDCOM=" + ligne + "&nbsp;&nbsp;niveau=" + niveau);
            PLACE_STRUCTURE info = new PLACE_STRUCTURE
            {
                N0_PLAC = null,
                N1_CEME = null,
                N2_CEME_PLOT = null,
                N1_FORM = null,
                N1_SITE = null,
                N1_FONE = null,
                N2_FONE_TYPE = null,
                N1_ROMN = null,
                N2_ROMN_TYPE = null,
                N2_MAP_LATI = null,
                N2_MAP_LONG = null,
                N1_NOTE_STRUCTURE_liste_ID = new List<string>(),
                N1_SOUR_citation_liste_ID = new List<string>(), // pour GEDitCOM
                N1_SOUR_source_liste_ID = new List<string>() // pour GEDitCOM
            };
            (info.N0_PLAC, ligne) = Extraire_texte_niveau_plus(ligne);
            List<ADDRESS_STRUCTURE> N1_ADDR_liste = new List<ADDRESS_STRUCTURE>();
            List<string> N1_NOTE_STRUCTURE_liste_ID = new List<string>();
            List<string> N1_PHON_liste = new List<string>();
            List<string> N1_EMAIL_liste = new List<string>();
            List<string> N1_FAX_liste = new List<string>();
            List<string> N1_WWW_liste = new List<string>();
            //string balise_0 = Avoir_balise(dataGEDCOM[ligne]);
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            string temp;
            while (Extraire_niveau(ligne) > niveau)
            {
                string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                int balise_1_ligne = ligne;
                switch (balise_1)
                {
                    case "CEME":                                                                        // CEME
                        (info.N1_CEME, ligne) = Extraire_texte_niveau_plus(ligne);
                        while (Extraire_niveau(ligne) > niveau + 1)
                        {
                            string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                            int balise_2_ligne = ligne;
                            switch (balise_2)
                            {
                                case "PLOT":                                                            // CEME_PLOT+
                                    (info.N2_CEME_PLOT, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                default:
                                    // pour la page HTML
                                    Ligne_perdue aa = new Ligne_perdue()
                                    {
                                        ligne = ligne,
                                        texte = dataGEDCOM[ligne]
                                    };
                                    ligne_perdue_liste.Add(aa);
                                    // rapport Balise
                                    method = MethodBase.GetCurrentMethod().Name;
                                    lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                    Ligne_perdue a0 = new Ligne_perdue()
                                    {
                                        ligne = balise_0_ligne,
                                        texte = dataGEDCOM[balise_0_ligne]
                                    };
                                    groupe_ligne.Add(a0);
                                    Ligne_perdue a1 = new Ligne_perdue()
                                    {
                                        ligne = balise_1_ligne,
                                        texte = dataGEDCOM[balise_1_ligne]
                                    };
                                    groupe_ligne.Add(a1);
                                    Ligne_perdue a2 = new Ligne_perdue()
                                    {
                                        ligne = balise_2_ligne,
                                        texte = dataGEDCOM[balise_2_ligne]
                                    };
                                    groupe_ligne.Add(a2);
                                    ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                    groupe_ligne.Clear();
                                    break;
                            }
                        }
                        break;
                    case "FORM":                                                                         // FORM
                        (info.N1_FORM, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "SITE":                                                                        // SITE V5.3
                        (info.N1_SITE, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "ADDR":                                                                        // ADDR
                        ADDRESS_STRUCTURE N1_ADDR;
                        (N1_ADDR, ligne, ligne_perdue_liste) = Extraire_ADDRESS_STRUCTURE(ligne, ligne_perdue_liste);
                        N1_ADDR_liste.Add(N1_ADDR);
                        break;
                    case "PHON":                                                                        // PHON
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        N1_PHON_liste.Add(temp);
                        break;
                    case "EMAIL":                                                                       // EMAIL
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        N1_EMAIL_liste.Add(temp);
                        break;
                    case "_EMAIL":                                                                      // _EMAIL
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        N1_EMAIL_liste.Add(temp);
                        break;
                    case "FAX":                                                                         // FAX
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        N1_FAX_liste.Add(temp);
                        break;
                    case "WWW":                                                                         // www
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        N1_WWW_liste.Add(temp);
                        break;
                    case "_WWW":                                                                        // _www
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        N1_WWW_liste.Add(temp);
                        break;
                    case "_URL":                                                                        // URL
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        N1_WWW_liste.Add(temp);
                        break;
                    case "FONE":                                                                        // FONE
                        (info.N1_FONE, ligne) = Extraire_texte_niveau_plus(ligne);
                        while (Extraire_niveau(ligne) > niveau + 1)
                        {
                            string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                            int balise_2_ligne = ligne;
                            switch (balise_2)
                            {
                                case "TYPE":                                                                // FONE_TYPE+
                                    (info.N2_FONE_TYPE, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                default:
                                    // pour la page HTML
                                    Ligne_perdue bb = new Ligne_perdue()
                                    {
                                        ligne = ligne,
                                        texte = dataGEDCOM[ligne]
                                    };
                                    ligne_perdue_liste.Add(bb);
                                    // rapport Balise
                                    method = MethodBase.GetCurrentMethod().Name;
                                    lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                    Ligne_perdue b0 = new Ligne_perdue()
                                    {
                                        ligne = balise_0_ligne,
                                        texte = dataGEDCOM[balise_0_ligne]
                                    };
                                    groupe_ligne.Add(b0);
                                    Ligne_perdue b1 = new Ligne_perdue
                                    {
                                        ligne = balise_1_ligne,
                                        texte = dataGEDCOM[balise_1_ligne]
                                    };
                                    groupe_ligne.Add(b1);
                                    Ligne_perdue b2 = new Ligne_perdue()
                                    {
                                        ligne = balise_2_ligne,
                                        texte = dataGEDCOM[balise_2_ligne]
                                    };
                                    groupe_ligne.Add(b2);
                                    ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                    groupe_ligne.Clear();
                                    break;
                            }
                        }
                        break;
                    case "ROMN":                                                                        // ROMN
                        (info.N1_ROMN, ligne) = Extraire_texte_niveau_plus(ligne);
                        while (Extraire_niveau(ligne) > niveau + 1)
                        {
                            string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                            int balise_2_ligne = ligne;
                            switch (balise_2)
                            {
                                case "TYPE":                                                                // RONN_TYPE
                                    (info.N2_ROMN_TYPE, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                default:
                                    // pour la page HTML
                                    Ligne_perdue cc = new Ligne_perdue()
                                    {
                                        ligne = ligne,
                                        texte = dataGEDCOM[ligne]
                                    };
                                    ligne_perdue_liste.Add(cc);
                                    // rapport Balise
                                    method = MethodBase.GetCurrentMethod().Name;
                                    lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                    Ligne_perdue c0 = new Ligne_perdue()
                                    {
                                        ligne = balise_0_ligne,
                                        texte = dataGEDCOM[balise_0_ligne]
                                    };
                                    groupe_ligne.Add(c0);
                                    Ligne_perdue c1 = new Ligne_perdue()
                                    {
                                        ligne = balise_1_ligne,
                                        texte = dataGEDCOM[balise_1_ligne]
                                    };
                                    groupe_ligne.Add(c1);
                                    Ligne_perdue c2 = new Ligne_perdue()
                                    {
                                        ligne = balise_2_ligne,
                                        texte = dataGEDCOM[balise_2_ligne]
                                    };
                                    groupe_ligne.Add(c2);
                                    ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                    groupe_ligne.Clear();
                                    break;
                            }
                        }
                        break;
                    case "MAP":                                                                         // MAP
                        ligne++;
                        while (Extraire_niveau(ligne) > niveau + 1)
                        {
                            string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                            int balise_2_ligne = ligne;
                            switch (balise_2)
                            {
                                case "LATI":                                                            // MAP_LATI
                                    (info.N2_MAP_LATI, ligne) = Extraire_texte_niveau_plus(ligne);
                                    info.N2_MAP_LATI = info.N2_MAP_LATI.Replace("N", "");
                                    info.N2_MAP_LATI = info.N2_MAP_LATI.Replace("S", "-");
                                    break;
                                case "LONG":                                                            // MAP_LONG
                                    (info.N2_MAP_LONG, ligne) = Extraire_texte_niveau_plus(ligne);
                                    info.N2_MAP_LONG = info.N2_MAP_LONG.Replace("E", "");
                                    info.N2_MAP_LONG = info.N2_MAP_LONG.Replace("W", "-");
                                    break;
                                default:
                                    // pour la page HTML
                                    Ligne_perdue dd = new Ligne_perdue()
                                    {
                                        ligne = ligne,
                                        texte = dataGEDCOM[ligne]
                                    };
                                    ligne_perdue_liste.Add(dd);
                                    // rapport Balise
                                    method = MethodBase.GetCurrentMethod().Name;
                                    lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                    Ligne_perdue d0 = new Ligne_perdue()
                                    {
                                        ligne = balise_0_ligne,
                                        texte = dataGEDCOM[balise_0_ligne]
                                    };
                                    groupe_ligne.Add(d0);
                                    Ligne_perdue d1 = new Ligne_perdue()
                                    {
                                        ligne = balise_1_ligne,
                                        texte = dataGEDCOM[balise_1_ligne]
                                    };
                                    groupe_ligne.Add(d1);
                                    Ligne_perdue d2 = new Ligne_perdue()
                                    {
                                        ligne = balise_2_ligne,
                                        texte = dataGEDCOM[balise_2_ligne]
                                    };
                                    groupe_ligne.Add(d2);
                                    ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                    groupe_ligne.Clear();
                                    break;
                            }
                        }
                        break;
                    case "NOTE":                                                                            // NOTE
                        string IDNote;
                        (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                        N1_NOTE_STRUCTURE_liste_ID.Add(IDNote);
                        break;
                    case "SOUR":                                                                            // SOUR
                        string citation;
                        string source;
                        (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                        if (IsNotNullOrEmpty(citation)) info.N1_SOUR_citation_liste_ID.Add(citation);
                        if (IsNotNullOrEmpty(source) && IsNullOrEmpty(citation)) info.N1_SOUR_source_liste_ID.Add(source);
                        break;
                    default:
                        // pour la page HTML
                        Ligne_perdue ee = new Ligne_perdue()
                        {
                            ligne = ligne,
                            texte = dataGEDCOM[ligne]
                        };
                        ligne_perdue_liste.Add(ee);
                        // rapport Balise
                        method = MethodBase.GetCurrentMethod().Name;
                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                        Ligne_perdue e0 = new Ligne_perdue()
                        {
                            ligne = balise_0_ligne,
                            texte = dataGEDCOM[balise_0_ligne]
                        };
                        groupe_ligne.Add(e0);
                        Ligne_perdue e1 = new Ligne_perdue()
                        {
                            ligne = balise_1_ligne,
                            texte = dataGEDCOM[balise_1_ligne]
                        };
                        groupe_ligne.Add(e1);
                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                        groupe_ligne.Clear();
                        break;
                }
            }
            info.N1_ADDR_liste = N1_ADDR_liste;
            info.N1_EMAIL_liste = N1_EMAIL_liste;
            info.N1_FAX_liste = N1_FAX_liste;
            info.N1_PHON_liste = N1_PHON_liste;
            info.N1_WWW_liste = N1_WWW_liste;
            info.N1_NOTE_STRUCTURE_liste_ID = N1_NOTE_STRUCTURE_liste_ID;
            return (info, ligne, ligne_perdue_liste);
        }

        private static int Extraire_REPOSITORY_RECORD(int ligne)
        {
            Regler_code_erreur();
            List<Ligne_perdue> ligne_perdue_liste = new List<Ligne_perdue>();
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            string N0_ID;
            string N1_NAME = null;
            string N1_CNTC = null;
            string N1_SITE = null;
            List<ADDRESS_STRUCTURE> N1_ADDR_liste = new List<ADDRESS_STRUCTURE>();
            List<string> N1_PHON_liste = new List<string>();
            List<string> N1_EMAIL_liste = new List<string>();
            List<string> N1_FAX_liste = new List<string>();
            List<string> N1_WWW_liste = new List<string>();
            string N1_MEDI = null;
            string N1_CALN = null;
            string N2_CALN_ITEM = null;
            string N2_CALN_SHEE = null;
            string N2_CALN_PAGE = null;
            List<string> N1_NOTE_STRUCTURE_liste_ID = new List<string>();
            List<USER_REFERENCE_NUMBER> N1_REFN_liste = new List<USER_REFERENCE_NUMBER>();
            string N1_RIN = null;
            List<CHANGE_DATE> N1_CHAN_liste = new List<CHANGE_DATE>();
            int index_repo = -1;
            int niveau;
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            (niveau, N0_ID, _, _, ligne) = Extraire_info_niveau_0(ligne);
            // si ID est null générer un ID;
            if (IsNullOrEmpty(N0_ID))
            {
                N0_ID = "R-" + DateTime.Now.ToString("HHmmssffffff") + hazard.Next(999).ToString();
            }
            else
            {// avoir index de la note existante
                index_repo = Avoir_index_repo(N0_ID);
            }
            try
            {
                while ((Extraire_niveau(ligne) > niveau))
                {
                    string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                    int balise_1_ligne = ligne;
                    switch (balise_1)
                    {
                        case "NAME":                                                                    // NAME
                            (N1_NAME, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "CNTC":                                                                    // CNTC V5.3
                            (N1_CNTC, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "SITE":                                                                    // SITE V5.3
                            (N1_SITE, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "ADDR":                                                                    // ADDR
                            ADDRESS_STRUCTURE N1_ADDR;
                            (N1_ADDR, ligne, ligne_perdue_liste) = Extraire_ADDRESS_STRUCTURE(ligne, ligne_perdue_liste);
                            N1_ADDR_liste.Add(N1_ADDR);
                            break;
                        case "PHON":                                                                     // PHON
                            string phon;
                            (phon, ligne) = Extraire_texte_niveau_plus(ligne);
                            N1_PHON_liste.Add(phon);
                            break;
                        case "EMAIL":                                                                   // EMAIL
                            string email;
                            (email, ligne) = Extraire_texte_niveau_plus(ligne);
                            N1_EMAIL_liste.Add(email);
                            break;
                        case "_EMAIL":                                                                  // _EMAIL
                            string email2;
                            (email2, ligne) = Extraire_texte_niveau_plus(ligne);
                            N1_EMAIL_liste.Add(email2);
                            break;
                        case "FAX":                                                                     // FAX
                            string fax;
                            (fax, ligne) = Extraire_texte_niveau_plus(ligne);
                            N1_FAX_liste.Add(fax);
                            break;
                        case "WWW":                                                                     // www
                            string www;
                            (www, ligne) = Extraire_texte_niveau_plus(ligne);
                            N1_WWW_liste.Add(www);
                            break;
                        case "_WWW":                                                                    // _www
                            string www2;
                            (www2, ligne) = Extraire_texte_niveau_plus(ligne);
                            N1_WWW_liste.Add(www2);
                            break;
                        case "_URL":                                                                    // URL
                            string url;
                            (url, ligne) = Extraire_texte_niveau_plus(ligne);
                            N1_WWW_liste.Add(url);
                            ligne++;
                            break;
                        case "MEDI":                                                                    // MEDI V5.3
                            (N1_MEDI, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "CALN":                                                                    // CALN V5.3
                            (N1_CALN, ligne) = Extraire_texte_niveau_plus(ligne);
                            while (Extraire_niveau(ligne) > niveau + 1)
                            {
                                string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                                int balise_2_ligne = ligne;
                                switch (balise_2)
                                {
                                    case "ITEM":                                                        // CALN ITEM V5.3
                                        (N2_CALN_ITEM, ligne) = Extraire_texte_niveau_plus(ligne);
                                        break;
                                    case "SHEE":                                                        // CALN SHEE V5.3
                                        (N2_CALN_SHEE, ligne) = Extraire_texte_niveau_plus(ligne);
                                        break;
                                    case "PAGE":                                                        // CALN PAGE V5.3
                                        (N2_CALN_PAGE, ligne) = Extraire_texte_niveau_plus(ligne);
                                        break;
                                    default:
                                        // pour la page HTML
                                        Ligne_perdue bb = new Ligne_perdue()
                                        {
                                            ligne = ligne,
                                            texte = dataGEDCOM[ligne]
                                        };
                                        ligne_perdue_liste.Add(bb);
                                        // rapport Balise
                                        method = MethodBase.GetCurrentMethod().Name;
                                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                        Ligne_perdue b0 = new Ligne_perdue()
                                        {
                                            ligne = balise_0_ligne,
                                            texte = dataGEDCOM[balise_0_ligne]
                                        };
                                        groupe_ligne.Add(b0);
                                        Ligne_perdue b1 = new Ligne_perdue()
                                        {
                                            ligne = balise_1_ligne,
                                            texte = dataGEDCOM[balise_1_ligne]
                                        };
                                        groupe_ligne.Add(b1);
                                        Ligne_perdue b2 = new Ligne_perdue()
                                        {
                                            ligne = balise_2_ligne,
                                            texte = dataGEDCOM[balise_2_ligne]
                                        };
                                        groupe_ligne.Add(b2);
                                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                        groupe_ligne.Clear();
                                        break;
                                }
                            }
                            break;
                        case "NOTE":                                                                        // NOTE
                            string IDNote;
                            (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                            N1_NOTE_STRUCTURE_liste_ID.Add(IDNote);
                            break;
                        case "REFN":                                                                        // REFN
                            USER_REFERENCE_NUMBER N1_REFN;
                            (N1_REFN, ligne, ligne_perdue_liste) = Extraire_USER_REFERENCE_NUMBER(ligne, ligne_perdue_liste);
                            N1_REFN_liste.Add(N1_REFN);
                            break;
                        case "RIN":                                                                         // RIN
                            (N1_RIN, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "CHAN":                                                                        // CHAN
                            CHANGE_DATE info_date;
                            (info_date, ligne, ligne_perdue_liste) = Extraire_CHANGE_DATE(ligne, ligne_perdue_liste);
                            N1_CHAN_liste.Add(info_date);
                            break;
                        default:
                            // pour la page HTML
                            Ligne_perdue cc = new Ligne_perdue()
                            {
                                ligne = ligne,
                                texte = dataGEDCOM[ligne]
                            };
                            ligne_perdue_liste.Add(cc);
                            // rapport Balise
                            method = MethodBase.GetCurrentMethod().Name;
                            lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                            Ligne_perdue c0 = new Ligne_perdue()
                            {
                                ligne = balise_0_ligne,
                                texte = dataGEDCOM[balise_0_ligne]
                            };
                            groupe_ligne.Add(c0);
                            Ligne_perdue c1 = new Ligne_perdue()
                            {
                                ligne = balise_1_ligne,
                                texte = dataGEDCOM[balise_1_ligne]
                            };
                            groupe_ligne.Add(c1);
                            ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                            groupe_ligne.Clear();
                            break;
                    }
                }
            }
            catch (Exception msg)
            {
                GC.Collect();
                string message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                Voir_message(message, msg.Message, GH.GH.erreur);
            }
            if (!GH.GH.annuler)
            {
                if (index_repo == -1) //nouvelle, on ajoute
                {
                    REPOSITORY_RECORD info_repo = new REPOSITORY_RECORD
                    {
                        ligne_perdue_liste = ligne_perdue_liste,
                        N0_ID = N0_ID,
                        N1_ADDR_liste = N1_ADDR_liste,
                        N1_NAME = N1_NAME,
                        N1_CALN = N1_CALN,
                        N2_CALN_ITEM = N2_CALN_ITEM,
                        N2_CALN_PAGE = N2_CALN_PAGE,
                        N2_CALN_SHEE = N2_CALN_SHEE,
                        N1_CHAN_liste = N1_CHAN_liste,
                        N1_CNTC = N1_CNTC,
                        N1_EMAIL_liste = N1_EMAIL_liste,
                        N1_FAX_liste = N1_FAX_liste,
                        N1_MEDI = N1_MEDI,
                        N1_NOTE_STRUCTURE_liste_ID = N1_NOTE_STRUCTURE_liste_ID,
                        N1_PHON_liste = N1_PHON_liste,
                        N1_REFN_liste = N1_REFN_liste,
                        N1_RIN = N1_RIN,
                        N1_SITE = N1_SITE,
                        N1_WWW_liste = N1_WWW_liste

                    };
                    liste_REPOSITORY_RECORD.Add(info_repo);
                }
                else // repo existe, on modifie.
                {
                    // ADDR
                    if (IsNullOrEmpty(liste_REPOSITORY_RECORD[index_repo].N1_ADDR_liste))
                    {
                        liste_REPOSITORY_RECORD[index_repo].N1_ADDR_liste = N1_ADDR_liste;
                    }
                    else
                    {
                        foreach (ADDRESS_STRUCTURE info_adresse in N1_ADDR_liste)
                        {
                            liste_REPOSITORY_RECORD[index_repo].N1_ADDR_liste.Add(info_adresse);
                        }
                    }
                    // name
                    if (IsNullOrEmpty(liste_REPOSITORY_RECORD[index_repo].N1_NAME))
                    {
                        liste_REPOSITORY_RECORD[index_repo].N1_NAME = N1_NAME;
                    }
                    else
                    {
                        liste_REPOSITORY_RECORD[index_repo].N1_NAME += System.Environment.NewLine + N1_NAME;
                    }
                    // CALN
                    if (IsNullOrEmpty(liste_REPOSITORY_RECORD[index_repo].N1_CALN))
                    {
                        liste_REPOSITORY_RECORD[index_repo].N1_CALN = N1_CALN;
                    }
                    else
                    {
                        liste_REPOSITORY_RECORD[index_repo].N1_CALN += System.Environment.NewLine + N1_CALN;
                    }
                    // CALN ITEM
                    if (IsNullOrEmpty(liste_REPOSITORY_RECORD[index_repo].N2_CALN_ITEM))
                    {
                        liste_REPOSITORY_RECORD[index_repo].N2_CALN_ITEM = N2_CALN_ITEM;
                    }
                    else
                    {
                        liste_REPOSITORY_RECORD[index_repo].N2_CALN_ITEM += System.Environment.NewLine + N2_CALN_ITEM;
                    }
                    // CALN SHEE
                    if (IsNullOrEmpty(liste_REPOSITORY_RECORD[index_repo].N2_CALN_SHEE))
                    {
                        liste_REPOSITORY_RECORD[index_repo].N2_CALN_SHEE = N2_CALN_SHEE;
                    }
                    else
                    {
                        liste_REPOSITORY_RECORD[index_repo].N2_CALN_SHEE += System.Environment.NewLine + N2_CALN_SHEE;
                    }
                    // CALN PAGE
                    if (IsNullOrEmpty(liste_REPOSITORY_RECORD[index_repo].N2_CALN_PAGE))
                    {
                        liste_REPOSITORY_RECORD[index_repo].N2_CALN_PAGE = N2_CALN_PAGE;
                    }
                    else
                    {
                        liste_REPOSITORY_RECORD[index_repo].N2_CALN_PAGE += System.Environment.NewLine + N2_CALN_PAGE;
                    }
                    // CHAN
                    if (IsNullOrEmpty(liste_REPOSITORY_RECORD[index_repo].N1_CHAN_liste))
                    {
                        liste_REPOSITORY_RECORD[index_repo].N1_CHAN_liste = N1_CHAN_liste;
                    }
                    else
                    {
                        foreach (CHANGE_DATE info_date in N1_CHAN_liste)
                        {
                            liste_REPOSITORY_RECORD[index_repo].N1_CHAN_liste.Add(info_date);
                        }
                    }
                    // CNTC
                    if (IsNullOrEmpty(liste_REPOSITORY_RECORD[index_repo].N1_CNTC))
                    {
                        liste_REPOSITORY_RECORD[index_repo].N1_CNTC = N1_CNTC;
                    }
                    else
                    {
                        liste_REPOSITORY_RECORD[index_repo].N1_CNTC += System.Environment.NewLine + N1_CNTC;
                    }
                    // EMAIL
                    if (IsNullOrEmpty(liste_REPOSITORY_RECORD[index_repo].N1_EMAIL_liste))
                    {
                        liste_REPOSITORY_RECORD[index_repo].N1_EMAIL_liste = N1_EMAIL_liste;
                    }
                    else
                    {
                        foreach (string info_email in N1_EMAIL_liste)
                        {
                            liste_REPOSITORY_RECORD[index_repo].N1_EMAIL_liste.Add(info_email);
                        }
                    }
                    // FAX
                    if (IsNullOrEmpty(liste_REPOSITORY_RECORD[index_repo].N1_FAX_liste))
                    {
                        liste_REPOSITORY_RECORD[index_repo].N1_FAX_liste = N1_FAX_liste;
                    }
                    else
                    {
                        foreach (string info_FAX in N1_FAX_liste)
                        {
                            liste_REPOSITORY_RECORD[index_repo].N1_FAX_liste.Add(info_FAX);
                        }
                    }
                    // PHON
                    if (IsNullOrEmpty(liste_REPOSITORY_RECORD[index_repo].N1_PHON_liste))
                    {
                        liste_REPOSITORY_RECORD[index_repo].N1_PHON_liste = N1_PHON_liste;
                    }
                    else
                    {
                        foreach (string info_PHON in N1_PHON_liste)
                        {
                            liste_REPOSITORY_RECORD[index_repo].N1_PHON_liste.Add(info_PHON);
                        }
                    }
                    // WWW
                    if (IsNullOrEmpty(liste_REPOSITORY_RECORD[index_repo].N1_WWW_liste))
                    {
                        liste_REPOSITORY_RECORD[index_repo].N1_WWW_liste = N1_WWW_liste;
                    }
                    else
                    {
                        foreach (string info_WWW in N1_WWW_liste)
                        {
                            liste_REPOSITORY_RECORD[index_repo].N1_WWW_liste.Add(info_WWW);
                        }
                    }
                    // MEDI
                    if (IsNullOrEmpty(liste_REPOSITORY_RECORD[index_repo].N1_MEDI))
                    {
                        liste_REPOSITORY_RECORD[index_repo].N1_MEDI = N1_MEDI;
                    }
                    else
                    {
                        liste_REPOSITORY_RECORD[index_repo].N1_MEDI += System.Environment.NewLine + N1_MEDI;
                    }
                    // NOTE
                    if (IsNullOrEmpty(liste_REPOSITORY_RECORD[index_repo].N1_NOTE_STRUCTURE_liste_ID))
                    {
                        liste_REPOSITORY_RECORD[index_repo].N1_NOTE_STRUCTURE_liste_ID = N1_NOTE_STRUCTURE_liste_ID;
                    }
                    else
                    {
                        foreach (string info_NOTE in N1_NOTE_STRUCTURE_liste_ID)
                        {
                            liste_REPOSITORY_RECORD[index_repo].N1_NOTE_STRUCTURE_liste_ID.Add(info_NOTE);
                        }
                    }
                    // REFN
                    if (IsNullOrEmpty(liste_REPOSITORY_RECORD[index_repo].N1_REFN_liste))
                    {
                        liste_REPOSITORY_RECORD[index_repo].N1_REFN_liste = N1_REFN_liste;
                    }
                    else
                    {
                        foreach (USER_REFERENCE_NUMBER info_REFN in N1_REFN_liste)
                        {
                            liste_REPOSITORY_RECORD[index_repo].N1_REFN_liste.Add(info_REFN);
                        }
                    }
                    // RIN
                    if (IsNullOrEmpty(liste_REPOSITORY_RECORD[index_repo].N1_RIN))
                    {
                        liste_REPOSITORY_RECORD[index_repo].N1_RIN = N1_RIN;
                    }
                    else
                    {
                        liste_REPOSITORY_RECORD[index_repo].N1_RIN += System.Environment.NewLine + N1_RIN;
                    }
                    // SITE
                    if (IsNullOrEmpty(liste_REPOSITORY_RECORD[index_repo].N1_SITE))
                    {
                        liste_REPOSITORY_RECORD[index_repo].N1_SITE = N1_SITE;
                    }
                    else
                    {
                        liste_REPOSITORY_RECORD[index_repo].N1_SITE += System.Environment.NewLine + N1_SITE;
                    }
                }
            }
            return ligne;
        }

        private static (CENS_record, int, List<Ligne_perdue>) Extraire_CENS_record(
            int ligne,
            List<Ligne_perdue> ligne_perdue_liste)
        {
            // utiliser avec SOUR de V5.3
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            CENS_record info_CENS = new CENS_record();
            List<string> N1_NOTE_STRUCTURE_liste_ID = new List<string>();
            int niveau = Extraire_niveau(ligne);
            //string balise_0 = Avoir_balise(dataGEDCOM[ligne]);
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            ligne++;
            while (Extraire_niveau(ligne) > niveau)
            {
                string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                int balise_1_ligne = ligne;
                switch (balise_1)
                {
                    case "DATE":                                                                        // DATE
                        (info_CENS.N1_DATE, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "LINE":                                                                        // LINE
                        (info_CENS.N1_LINE, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "DWEL":                                                                        // DWEL
                        (info_CENS.N1_DWEL, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "FAMN":                                                                        // FAMN
                        (info_CENS.N1_FAMN, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "NOTE":                                                                        // NOTE
                        string IDNote;
                        (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                        N1_NOTE_STRUCTURE_liste_ID.Add(IDNote);
                        break;
                    default:
                        // pour la page HTML
                        Ligne_perdue aa = new Ligne_perdue()
                        {
                            ligne = ligne,
                            texte = dataGEDCOM[ligne]
                        };
                        ligne_perdue_liste.Add(aa);
                        // rapport Balise
                        method = MethodBase.GetCurrentMethod().Name;
                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                        Ligne_perdue a0 = new Ligne_perdue()
                        {
                            ligne = balise_0_ligne,
                            texte = dataGEDCOM[balise_0_ligne]
                        };
                        groupe_ligne.Add(a0);
                        Ligne_perdue a1 = new Ligne_perdue()
                        {
                            ligne = balise_1_ligne,
                            texte = dataGEDCOM[balise_1_ligne]
                        };
                        groupe_ligne.Add(a1);
                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                        groupe_ligne.Clear();
                        break;
                }
            }
            info_CENS.N1_NOTE_STRUCTURE_liste_ID = N1_NOTE_STRUCTURE_liste_ID;
            return (info_CENS, ligne, ligne_perdue_liste);
        }
        public static (
            int,
            string,
            string,
            string,
            int
        ) Extraire_info_niveau_0( // modifier
            int ligne
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b> GEDCOM=" + ligne);
            /*
                format de la première ligne
            1               |niveau|balise|ID|              3
            2               |niveau|ID    |balise| texte|   4 et plus
                ou
            3               |niveau|balise|ID    | texte|   4 et plus
                ou
            4               |niveau|balise|texte |          3 et plus 
            cas position    |   0      1     2       3      nombre
            
             retourne (texte, ID, position_ID, ligne)
             */
            if (ligne < 1) return (-1, null, null, null, ligne);
            string balise_0 = Avoir_balise(dataGEDCOM[ligne]);
            string balise_1;
            string ID = null;
            int section_balise = 1;
            int section_ID = 0;
            string texte_ligne = dataGEDCOM[ligne];
            string texte_retour = null;// le texte à retouner
            texte_ligne = texte_ligne.TrimStart();
            texte_ligne = texte_ligne.TrimEnd();
            // retire les espace d'extra
            string texte2 = texte_ligne[0].ToString();
            for (int f = 1; f < texte_ligne.Count(); f++)
            {
                if (texte_ligne[f] == ' ' && texte_ligne[f - 1] == ' ')
                {
                    texte2 += "#A#A#A#";
                }
                else texte2 += texte_ligne[f].ToString();
            }

            texte2 = String.Join(" ", texte2.Split(new string[] { " " }, // retire les espaces d'extra
                StringSplitOptions.RemoveEmptyEntries));
            // séparer la ligne en section
            char[] espace = { ' ' };
            string[] section = texte2.Split(espace);
            //foreach (string a in section)
            //GEDCOMClass.ZXXCV("section " + a);
            // niveau de la ligne
            int niveau = Int32.Parse(section[0]);
            // nombre de section dans la ligne
            int nombre_section = section.Length;
            // trouve ID
            char at = '@';
            if (nombre_section > 2)
            {
                if (section[1][0] == at && section[1][section[1].Length - 1] == at)
                // section 1 est un ID
                {
                    //GEDCOMClass.ZXXCV("ID section 1");
                    ID = Retirer_marque(section[1]);
                    section_ID = 1;
                    section_balise = 2;
                    balise_0 = Retirer_marque(section[2]);
                }
                if (section[2][0] == at && section[2][section[2].Length - 1] == at)
                {
                    //GEDCOMClass.ZXXCV("ID section 2");

                    ID = Retirer_marque(section[2]);
                    section_ID = 2;
                    section_balise = 1;
                    balise_0 = Retirer_marque(section[1]);
                }
            }
            // retirer @ du ID
            if (section_ID == 0) balise_0 = section[1];
            if (IsNotNullOrEmpty(ID)) ID = ID.Trim(at);

            // récupérer le texte de la première ligne

            if (nombre_section == 3 && section_balise > 0 && section_ID > 0)
            {// cas 1 pas de texte
                //GEDCOMClass.ZXXCV("cas 1 pas de texte");
            }
            else if (nombre_section > 3 && section_balise > 0 && section_ID > 0)
            {// cas 2 et 3 avec balise ID texte
                texte_retour = Retirer_marque(texte2.Substring(section[0].Length + section[1].Length + section[2].Length + 3));
            }
            else if (nombre_section > 2 && section_balise == 1 && section_ID == 0)
            {// cas 4 avec balise texte
                texte_retour = Retirer_marque(texte2.Substring(section[0].Length + section[1].Length + 2));
            }
            ligne++;

            // extraire les lignes suivantes avec balise CONT CONC
            balise_1 = Avoir_balise_p1(dataGEDCOM[ligne]);
            while (balise_1 == (niveau + 1).ToString() + " CONT" || balise_1 == (niveau + 1).ToString() + " CONC")
            {
                balise_1 = Avoir_balise_p1(dataGEDCOM[ligne]);
                if (balise_1 == (niveau + 1).ToString() + " CONT")                                    // +1 CONT
                {
                    texte_retour += System.Environment.NewLine + Extraire_textuel(ligne);
                    ligne++;
                }
                else if (balise_1 == (niveau + 1).ToString() + " CONC")                               // +1 CONC
                {
                    texte_retour += " " + Extraire_textuel(ligne);
                    ligne++;
                }
            }
            //GEDCOMClass.ZXXCV("sortie texte_retour " + texte_retour);
            return (niveau, ID, balise_0, texte_retour, ligne);
        }

        private static (IMMI_record, int, List<Ligne_perdue>) Extraire_IMMI_record(
            int ligne,
            List<Ligne_perdue> ligne_perdue_liste)
        {
            // utiliser avec SOUR de V5.3
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            IMMI_record info_IMMI = new IMMI_record();
            List<string> N1_NOTE_STRUCTURE_liste_ID = new List<string>();
            List<TEXT_STRUCTURE> N1_TEXT_liste = new List<TEXT_STRUCTURE>();
            int niveau = Extraire_niveau(ligne);
            //string balise_0 = Avoir_balise(dataGEDCOM[ligne]);
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            ligne++;
            while (Extraire_niveau(ligne) > niveau)
            {
                string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                int balise_1_ligne = ligne;
                switch (balise_1)
                {
                    case "NAME":                                                                        // +1 NAME
                        (info_IMMI.N1_NAME, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "PORT":                                                                        // +1 PORT
                        {
                            ligne++;
                            while (Extraire_niveau(ligne) > niveau + 1)
                            {
                                string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                                int balise_2_ligne = ligne;
                                switch (balise_2)
                                {
                                    case "ARVL":                                                        // ARVL
                                        {
                                            ligne++;
                                            while (Extraire_niveau(ligne) > niveau + 2)
                                            {
                                                string balise_3 = Avoir_balise(dataGEDCOM[ligne]);
                                                int balise_3_ligne = ligne;
                                                switch (balise_3)
                                                {
                                                    case "DATE":                                        // DATE
                                                        (info_IMMI.N3_PORT_ARVL_DATE, ligne) = Extraire_texte_niveau_plus(ligne);
                                                        break;
                                                    case "PLAC":                                        // PLAC
                                                        (info_IMMI.N3_PORT_ARVL_PLAC, ligne) = Extraire_texte_niveau_plus(ligne);
                                                        break;
                                                    default:
                                                        // pour la page HTML
                                                        Ligne_perdue aa = new Ligne_perdue()
                                                        {
                                                            ligne = ligne,
                                                            texte = dataGEDCOM[ligne]
                                                        };
                                                        ligne_perdue_liste.Add(aa);
                                                        // rapport Balise
                                                        method = MethodBase.GetCurrentMethod().Name;
                                                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                                        Ligne_perdue a0 = new Ligne_perdue()
                                                        {
                                                            ligne = balise_0_ligne,
                                                            texte = dataGEDCOM[balise_0_ligne]
                                                        };
                                                        groupe_ligne.Add(a0);
                                                        Ligne_perdue a1 = new Ligne_perdue()
                                                        {
                                                            ligne = balise_1_ligne,
                                                            texte = dataGEDCOM[balise_1_ligne]
                                                        };
                                                        groupe_ligne.Add(a1);
                                                        Ligne_perdue a2 = new Ligne_perdue()
                                                        {
                                                            ligne = balise_2_ligne,
                                                            texte = dataGEDCOM[balise_2_ligne]
                                                        };
                                                        groupe_ligne.Add(a2);
                                                        Ligne_perdue a3 = new Ligne_perdue()
                                                        {
                                                            ligne = balise_3_ligne,
                                                            texte = dataGEDCOM[balise_3_ligne]
                                                        };
                                                        groupe_ligne.Add(a3);
                                                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                                        groupe_ligne.Clear();
                                                        break;
                                                }
                                            }
                                        }
                                        break;
                                    case "DPRT":                                                         // DPRT
                                        {
                                            ligne++;
                                            while (Extraire_niveau(ligne) > niveau + 2)
                                            {
                                                string balise_3 = Avoir_balise(dataGEDCOM[ligne]);
                                                int balise_3_ligne = ligne;
                                                switch (balise_3)
                                                {
                                                    case "DATE":                                        // DATE
                                                        (info_IMMI.N3_PORT_DPRT_DATE, ligne) = Extraire_texte_niveau_plus(ligne);
                                                        break;
                                                    case "PLAC":                                        // PLAC
                                                        (info_IMMI.N3_PORT_DPRT_PLAC, ligne) = Extraire_texte_niveau_plus(ligne);
                                                        break;
                                                    default:
                                                        // pour la page HTML
                                                        Ligne_perdue cc = new Ligne_perdue()
                                                        {
                                                            ligne = ligne,
                                                            texte = dataGEDCOM[ligne]
                                                        };
                                                        ligne_perdue_liste.Add(cc);
                                                        // rapport Balise
                                                        method = MethodBase.GetCurrentMethod().Name;
                                                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                                        Ligne_perdue c0 = new Ligne_perdue()
                                                        {
                                                            ligne = balise_0_ligne,
                                                            texte = dataGEDCOM[balise_0_ligne]
                                                        };
                                                        groupe_ligne.Add(c0);
                                                        Ligne_perdue c1 = new Ligne_perdue()
                                                        {
                                                            ligne = balise_1_ligne,
                                                            texte = dataGEDCOM[balise_1_ligne]
                                                        };
                                                        groupe_ligne.Add(c1);
                                                        Ligne_perdue c2 = new Ligne_perdue()
                                                        {
                                                            ligne = balise_2_ligne,
                                                            texte = dataGEDCOM[balise_2_ligne]
                                                        };
                                                        groupe_ligne.Add(c2);
                                                        Ligne_perdue c3 = new Ligne_perdue()
                                                        {
                                                            ligne = balise_3_ligne,
                                                            texte = dataGEDCOM[balise_3_ligne]
                                                        };
                                                        groupe_ligne.Add(c3);
                                                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                                        groupe_ligne.Clear();
                                                        break;
                                                }
                                            }
                                        }
                                        break;
                                    default:
                                        // pour la page HTML
                                        Ligne_perdue dd = new Ligne_perdue()
                                        {
                                            ligne = ligne,
                                            texte = dataGEDCOM[ligne]
                                        };
                                        ligne_perdue_liste.Add(dd);
                                        // rapport Balise
                                        method = MethodBase.GetCurrentMethod().Name;
                                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                        Ligne_perdue d0 = new Ligne_perdue()
                                        {
                                            ligne = balise_0_ligne,
                                            texte = dataGEDCOM[balise_0_ligne]
                                        };
                                        groupe_ligne.Add(d0);
                                        Ligne_perdue d1 = new Ligne_perdue()
                                        {
                                            ligne = balise_1_ligne,
                                            texte = dataGEDCOM[balise_1_ligne]
                                        };
                                        groupe_ligne.Add(d1);
                                        Ligne_perdue d2 = new Ligne_perdue()
                                        {
                                            ligne = balise_2_ligne,
                                            texte = dataGEDCOM[balise_2_ligne]
                                        };
                                        groupe_ligne.Add(d2);
                                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                        groupe_ligne.Clear();
                                        break;
                                }
                            }
                        }
                        break;
                    case "TEXT":                                                                        // TEXT
                        {
                            TEXT_STRUCTURE temp;
                            (temp, ligne, ligne_perdue_liste) = Extraire_TEXT_STRUCTURE(ligne, ligne_perdue_liste);
                            N1_TEXT_liste.Add(temp);
                        }
                        break;
                    case "NOTE":                                                                        // NOTE
                        {
                            string IDNote;
                            (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                            N1_NOTE_STRUCTURE_liste_ID.Add(IDNote);
                        }
                        break;
                    default:
                        // pour la page HTML
                        Ligne_perdue ee = new Ligne_perdue()
                        {
                            ligne = ligne,
                            texte = dataGEDCOM[ligne]
                        };
                        ligne_perdue_liste.Add(ee);
                        // rapport Balise
                        method = MethodBase.GetCurrentMethod().Name;
                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                        Ligne_perdue e0 = new Ligne_perdue()
                        {
                            ligne = balise_0_ligne,
                            texte = dataGEDCOM[balise_0_ligne]
                        };
                        groupe_ligne.Add(e0);
                        Ligne_perdue e1 = new Ligne_perdue()
                        {
                            ligne = balise_1_ligne,
                            texte = dataGEDCOM[balise_1_ligne]
                        };
                        groupe_ligne.Add(e1);
                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                        groupe_ligne.Clear();
                        break;
                }
            }
            info_IMMI.N1_NOTE_STRUCTURE_liste_ID = N1_NOTE_STRUCTURE_liste_ID;
            info_IMMI.N1_TEXT_liste = N1_TEXT_liste;
            return (info_IMMI, ligne, ligne_perdue_liste);
        }

        private static (ORIG_record, int, List<Ligne_perdue>) Extraire_ORIG_record(
            int ligne,
            List<Ligne_perdue> ligne_perdue_liste
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z(" LP De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b> GEDCOM=" + ligne);
            // utiliser avec SOUR de V5.3
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            ORIG_record info_ORIG = new ORIG_record();
            List<string> N1_NOTE_STRUCTURE_liste_ID = new List<string>();
            int niveau = Extraire_niveau(ligne);
            //string balise_0 = Avoir_balise(dataGEDCOM[ligne]);
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            ligne++;
            while (Extraire_niveau(ligne) > niveau)
            {
                string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                int balise_1_ligne = ligne;
                switch (balise_1)
                {
                    case "NAME":                                                                        // NAME
                        (info_ORIG.N1_NAME, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "TYPE":                                                                        // TYPE
                        (info_ORIG.N1_TYPE, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "NOTE":                                                                        // NOTE
                        string IDNote;
                        (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                        N1_NOTE_STRUCTURE_liste_ID.Add(IDNote);
                        break;
                    default:
                        // pour la page HTML
                        Ligne_perdue aa = new Ligne_perdue()
                        {
                            ligne = ligne,
                            texte = dataGEDCOM[ligne]
                        };
                        ligne_perdue_liste.Add(aa);
                        // rapport Balise
                        method = MethodBase.GetCurrentMethod().Name;
                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                        Ligne_perdue a0 = new Ligne_perdue()
                        {
                            ligne = balise_0_ligne,
                            texte = dataGEDCOM[balise_0_ligne]
                        };
                        groupe_ligne.Add(a0);
                        Ligne_perdue a1 = new Ligne_perdue()
                        {
                            ligne = balise_1_ligne,
                            texte = dataGEDCOM[balise_1_ligne]
                        };
                        groupe_ligne.Add(a1);
                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                        groupe_ligne.Clear();
                        break;
                }
            }
            info_ORIG.N1_NOTE_STRUCTURE_liste_ID = N1_NOTE_STRUCTURE_liste_ID;
            return (info_ORIG, ligne, ligne_perdue_liste);
        }

        private static (PUBL_record, int, List<Ligne_perdue>) Extraire_PUBL_record(
            int ligne,
            List<Ligne_perdue> ligne_perdue_liste)
        {
            // utiliser avec SOUR de V5.3
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            PUBL_record info_PUBL = new PUBL_record();
            List<ADDRESS_STRUCTURE> N1_ADDR_liste = new List<ADDRESS_STRUCTURE>();
            List<string> N1_PHON_liste = new List<string>();
            List<string> N1_FAX_liste = new List<string>();
            List<string> N1_EMAIL_liste = new List<string>();
            List<string> N1_WWW_liste = new List<string>();
            int niveau;
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            string temp;
            (niveau, _, _, info_PUBL.N1_PUBL, ligne) = Extraire_info_niveau_0(ligne);
            while (Extraire_niveau(ligne) > niveau)
            {
                string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                int balise_1_ligne = ligne;
                switch (balise_1)
                {
                    case "NAME":                                                                        // NAME
                        (info_PUBL.N1_NAME, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "TYPE":                                                                        // TYPE
                        (info_PUBL.N1_TYPE, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "PUBR":                                                                        // PUBR
                        (info_PUBL.N1_PUBR, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "SITE":                                                                        // SITE
                        (info_PUBL.N1_SITE, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "ADDR":                                                                        // ADDR
                        ADDRESS_STRUCTURE N1_ADDR;
                        (N1_ADDR, ligne, ligne_perdue_liste) = Extraire_ADDRESS_STRUCTURE(ligne, ligne_perdue_liste);
                        N1_ADDR_liste.Add(N1_ADDR);
                        break;
                    case "PHON":                                                                        // PHON
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        N1_PHON_liste.Add(temp);
                        break;
                    case "FAX":                                                                         // FAX
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        N1_FAX_liste.Add(temp);
                        break;
                    case "EMAIL":                                                                       // EMAIL
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        N1_EMAIL_liste.Add(temp);
                        break;
                    case "_EMAIL":                                                                      // _EMAIL
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        N1_EMAIL_liste.Add(temp);
                        break;
                    case "WWW":                                                                         // WWW
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        N1_WWW_liste.Add(temp);
                        break;
                    case "_WWW":                                                                        // _WWW
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        N1_WWW_liste.Add(temp);
                        break;
                    case "_URL":                                                                        // _URL
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        N1_WWW_liste.Add(temp);
                        break;
                    case "DATE":                                                                        // DATE
                        (info_PUBL.N1_DATE, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "EDTN":                                                                        // EDTN
                        (info_PUBL.N1_EDTN, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "SERS":                                                                        // SERS
                        (info_PUBL.N1_SERS, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "ISSU":                                                                        // ISSU
                        (info_PUBL.N1_ISSU, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "LCCN":                                                                        // LCCN
                        (info_PUBL.N1_LCCN, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    default:
                        // pour la page HTML
                        Ligne_perdue aa = new Ligne_perdue()
                        {
                            ligne = ligne,
                            texte = dataGEDCOM[ligne]
                        };
                        ligne_perdue_liste.Add(aa);
                        // rapport Balise
                        method = MethodBase.GetCurrentMethod().Name;
                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                        Ligne_perdue a0 = new Ligne_perdue()
                        {
                            ligne = balise_0_ligne,
                            texte = dataGEDCOM[balise_0_ligne]
                        };
                        groupe_ligne.Add(a0);
                        Ligne_perdue a1 = new Ligne_perdue()
                        {
                            ligne = balise_1_ligne,
                            texte = dataGEDCOM[balise_1_ligne]
                        };
                        groupe_ligne.Add(a1);
                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                        groupe_ligne.Clear();
                        break;
                }
            }
            info_PUBL.N1_ADDR_liste = N1_ADDR_liste;
            info_PUBL.N1_PHON_liste = N1_PHON_liste;
            info_PUBL.N1_PHON_liste = N1_PHON_liste;
            info_PUBL.N1_PHON_liste = N1_PHON_liste;
            info_PUBL.N1_PHON_liste = N1_PHON_liste;
            return (info_PUBL, ligne, ligne_perdue_liste);
        }

        private static (string, string, int) Extraire_SOURCE_CITATION(
            int ligne
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b> GEDCOM=" + ligne + "&nbsp;&nbsp;ligne=" + ligne);
            Regler_code_erreur();
            List<Ligne_perdue> ligne_perdue_liste = new List<Ligne_perdue>();
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            SOURCE_CITATION info = new SOURCE_CITATION();
            Regler_code_erreur();
            //string N0_ID_SOUR = null;
            List<TEXT_STRUCTURE> N2_DATA_TEXT_liste = new List<TEXT_STRUCTURE>();
            List<string> N1_SOUR_liste_ID = new List<string>();
            List<string> N1_TITL_liste = new List<string>();
            //List<string> N1_REPO_liste_ID = new List<string>();
            List<SOURCE_REPOSITORY_CITATION> N1_REPO_liste = new List<SOURCE_REPOSITORY_CITATION>();
            List<string> N1_NOTE_STRUCTURE_liste_ID = new List<string>();
            List<string> MULTIMEDIA_LINK_liste_ID = new List<string>();
            List<string> N1_SOUR_liste_EVEN = new List<string>();
            List<CENS_record> N1_CENS_liste = new List<CENS_record>();
            List<ORIG_record> N1_ORIG_liste = new List<ORIG_record>();
            List<TEXT_STRUCTURE> N1_TEXT_liste = new List<TEXT_STRUCTURE>();
            List<string> N1_REFS_liste_ID = new List<string>();
            List<string> N1_REFS_liste_EVEN = new List<string>();
            //string balise_0;
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            int niveau;
            bool source_ID_seulement = true;
            try
            {
                niveau = Extraire_niveau(ligne);
                info.N0_ID_SOUR = Extraire_ID(dataGEDCOM[ligne]);
                if (info.N0_ID_SOUR == null)
                    (info.N0_texte, ligne) = Extraire_texte_niveau_plus(ligne);
                else
                    ligne++;
                //(Null, N0_ID_SOUR, balise_0, info.N0_texte, ligne) = Extraire_info_niveau_0(ligne);
                Regler_code_erreur();
                if (IsNotNullOrEmpty(info.N0_texte)) source_ID_seulement = false;
                while (Extraire_niveau(ligne) > niveau)
                {
                    string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                    int balise_1_ligne = ligne;
                    switch (balise_1)
                    {
                        case "TEXT":                                                                    // TEXT
                            source_ID_seulement = false;
                            TEXT_STRUCTURE temp;
                            (temp, ligne, ligne_perdue_liste) = Extraire_TEXT_STRUCTURE(ligne, ligne_perdue_liste);
                            N1_TEXT_liste.Add(temp);
                            break;
                        case "CLAS":                                                                    // CLASS v5.3
                            source_ID_seulement = false;
                            (info.N1_CLAS, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "PERI":                                                                    // PERI V5.3
                            source_ID_seulement = false;
                            (info.N1_PERI, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "TITL":                                                                    // TITL V5.3
                            source_ID_seulement = false;
                            string temp2;
                            (temp2, ligne) = Extraire_texte_niveau_plus(ligne);
                            N1_TITL_liste.Add(temp2);
                            break;
                        case "SOUR":                                                                    // SOUR V5.3
                            source_ID_seulement = false;
                            string temp_texte;
                            string ID;
                            (_, ID, _, temp_texte, ligne) = Extraire_info_niveau_0(ligne);
                            if (IsNotNullOrEmpty(ID))
                            {
                                N1_SOUR_liste_ID.Add(ID);
                            }
                            else
                            {
                                N1_SOUR_liste_EVEN.Add(temp_texte);
                            }
                            break;
                        case "PAGE":                                                                    // PAGE
                            source_ID_seulement = false;
                            (info.N1_PAGE, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "DATE":                                                                    // DATE v5.3
                            source_ID_seulement = false;
                            (info.N1_DATE, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "CENS":                                                                    // CENS v5.3
                            source_ID_seulement = false;
                            CENS_record info_CEMS;
                            (info_CEMS, ligne, ligne_perdue_liste) = Extraire_CENS_record(ligne, ligne_perdue_liste);
                            N1_CENS_liste.Add(info_CEMS);
                            break;
                        case "ORIG":                                                                    // ORIG v5.3
                            source_ID_seulement = false;
                            ORIG_record info_ORIG;
                            (info_ORIG, ligne, ligne_perdue_liste) = Extraire_ORIG_record(ligne, ligne_perdue_liste);
                            N1_ORIG_liste.Add(info_ORIG);
                            break;
                        case "PUBL":                                                                    // PUBL v5.3
                            source_ID_seulement = false;
                            (info.N1_PUBL_record, ligne, ligne_perdue_liste) = Extraire_PUBL_record(ligne, ligne_perdue_liste);
                            break;
                        case "REPO":                                                                    // REPO
                            source_ID_seulement = false;
                            SOURCE_REPOSITORY_CITATION info_REPO;
                            (info_REPO, ligne) = Extraire_SOURCE_REPOSITORY_CITATION(ligne, ligne_perdue_liste);
                            N1_REPO_liste.Add(info_REPO);
                            break;
                        case "IMMI":                                                                    // IMMI V5.3
                            source_ID_seulement = false;
                            (info.N1_IMMI_record, ligne, ligne_perdue_liste) = Extraire_IMMI_record(ligne, ligne_perdue_liste);
                            break;
                        case "OBJE":                                                                    // OBJE
                            source_ID_seulement = false;
                            string temp3;
                            (temp3, ligne, ligne_perdue_liste) = Extraire_MULTIMEDIA_LINK(ligne, ligne_perdue_liste);
                            MULTIMEDIA_LINK_liste_ID.Add(temp3);
                            break;
                        case "AUDIO":                                                                   // AUDIO V5.3
                            source_ID_seulement = false;
                            string audio;
                            (audio, ligne) = Extraire_texte_niveau_plus(ligne);
                            MULTIMEDIA_LINK_liste_ID.Add(audio);
                            break;
                        case "PHOTO":                                                                   // PHOTO V5.3
                            source_ID_seulement = false;
                            string photo;
                            (photo, ligne) = Extraire_texte_niveau_plus(ligne);
                            MULTIMEDIA_LINK_liste_ID.Add(photo);
                            break;
                        case "VIDEO":                                                                   // VIDEO V5.3
                            source_ID_seulement = false;
                            string video;
                            (video, ligne) = Extraire_texte_niveau_plus(ligne);
                            MULTIMEDIA_LINK_liste_ID.Add(video);
                            break;
                        case "DATA":                                                                    // DATA
                            source_ID_seulement = false;
                            ligne++;
                            while (Extraire_niveau(ligne) > niveau + 1)
                            {
                                string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                                int balise_2_ligne = ligne;
                                switch (balise_2)
                                {
                                    case "DATE":                                                        // DATE
                                        (info.N2_DATA_DATE, ligne) = Extraire_texte_niveau_plus(ligne);
                                        break;
                                    case "TEXT":                                                        // TEXT
                                        TEXT_STRUCTURE temp4;
                                        (temp4, ligne, ligne_perdue_liste) = Extraire_TEXT_STRUCTURE(ligne, ligne_perdue_liste);
                                        N2_DATA_TEXT_liste.Add(temp4);
                                        break;
                                    default:
                                        // pour la page HTML
                                        Ligne_perdue aa = new Ligne_perdue()
                                        {
                                            ligne = ligne,
                                            texte = dataGEDCOM[ligne]
                                        };
                                        ligne_perdue_liste.Add(aa);
                                        // rapport Balise
                                        method = MethodBase.GetCurrentMethod().Name;
                                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                        Ligne_perdue a0 = new Ligne_perdue()
                                        {
                                            ligne = balise_0_ligne,
                                            texte = dataGEDCOM[balise_0_ligne]
                                        };
                                        groupe_ligne.Add(a0);
                                        Ligne_perdue a1 = new Ligne_perdue()
                                        {
                                            ligne = balise_1_ligne,
                                            texte = dataGEDCOM[balise_1_ligne]
                                        };
                                        groupe_ligne.Add(a1);
                                        Ligne_perdue a2 = new Ligne_perdue()
                                        {
                                            ligne = balise_2_ligne,
                                            texte = dataGEDCOM[balise_2_ligne]
                                        };
                                        groupe_ligne.Add(a2);
                                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                        groupe_ligne.Clear();
                                        break;
                                }
                            }
                            break;
                        case "NOTE":                                                                    // NOTE
                            source_ID_seulement = false;
                            string IDNote;
                            (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                            N1_NOTE_STRUCTURE_liste_ID.Add(IDNote);
                            break;
                        case "EVEN":                                                                    // EVEN
                            source_ID_seulement = false;
                            (info.N1_EVEN, ligne) = Extraire_texte_niveau_plus(ligne);
                            while (Extraire_niveau(ligne) > niveau + 1)
                            {
                                string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                                int balise_2_ligne = ligne;
                                switch (balise_2)
                                {
                                    case "ROLE":                                                            // +2 ROLE
                                        (info.N2_EVEN_ROLE, ligne) = Extraire_texte_niveau_plus(ligne);
                                        break;
                                    default:
                                        // pour la page HTML
                                        Ligne_perdue aa = new Ligne_perdue()
                                        {
                                            ligne = ligne,
                                            texte = dataGEDCOM[ligne]
                                        };
                                        ligne_perdue_liste.Add(aa);
                                        // rapport Balise
                                        method = MethodBase.GetCurrentMethod().Name;
                                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                        Ligne_perdue a0 = new Ligne_perdue()
                                        {
                                            ligne = balise_0_ligne,
                                            texte = dataGEDCOM[balise_0_ligne]
                                        };
                                        groupe_ligne.Add(a0);
                                        Ligne_perdue a1 = new Ligne_perdue()
                                        {
                                            ligne = balise_1_ligne,
                                            texte = dataGEDCOM[balise_1_ligne]
                                        };
                                        groupe_ligne.Add(a1);
                                        Ligne_perdue a2 = new Ligne_perdue()
                                        {
                                            ligne = balise_2_ligne,
                                            texte = dataGEDCOM[balise_2_ligne]
                                        };
                                        groupe_ligne.Add(a2);
                                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                        groupe_ligne.Clear();
                                        break;
                                }
                            }
                            break;
                        case "STAT":                                                                    // +1 STAT V5.3)
                            source_ID_seulement = false;
                            (info.N1_STAT, ligne) = Extraire_texte_niveau_plus(ligne);
                            while (Extraire_niveau(ligne) > niveau + 1)
                            {
                                string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                                int balise_2_ligne = ligne;
                                switch (balise_2)
                                {
                                    case "DATE":                                                        // +2 STAT DATE
                                        (info.N2_STAT_DATE, ligne) = Extraire_texte_niveau_plus(ligne);
                                        break;
                                    default:
                                        // pour la page HTML
                                        Ligne_perdue bb = new Ligne_perdue()
                                        {
                                            ligne = ligne,
                                            texte = dataGEDCOM[ligne]
                                        };
                                        ligne_perdue_liste.Add(bb);
                                        // rapport Balise
                                        method = MethodBase.GetCurrentMethod().Name;
                                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                        Ligne_perdue b0 = new Ligne_perdue()
                                        {
                                            ligne = balise_0_ligne,
                                            texte = dataGEDCOM[balise_0_ligne]
                                        };
                                        groupe_ligne.Add(b0);
                                        Ligne_perdue b1 = new Ligne_perdue()
                                        {
                                            ligne = balise_1_ligne,
                                            texte = dataGEDCOM[balise_1_ligne]
                                        };
                                        groupe_ligne.Add(b1);
                                        Ligne_perdue b2 = new Ligne_perdue()
                                        {
                                            ligne = balise_2_ligne,
                                            texte = dataGEDCOM[balise_2_ligne]
                                        };
                                        groupe_ligne.Add(b2);
                                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                        groupe_ligne.Clear();
                                        break;
                                }
                            }
                            break;
                        case "REFS":                                                                    // REFS V5.3
                            source_ID_seulement = false;
                            string ID1 = Extraire_ID(dataGEDCOM[ligne]);
                            if (IsNotNullOrEmpty(ID1))
                            {
                                N1_REFS_liste_ID.Add(ID1);
                                ligne++;
                            }
                            else
                            {
                                string refs;
                                (refs, ligne) = Extraire_texte_niveau_plus(ligne);
                                N1_REFS_liste_EVEN.Add(refs);
                            }
                            break;
                        case "FIDE":                                                                    // FIDE V5.3
                            source_ID_seulement = false;
                            (info.N1_FIDE, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "QUAY":                                                                    // QUAY
                            source_ID_seulement = false;
                            (info.N1_QUAY, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "_QUAL":                                                                   // _QUAL Heridis
                            source_ID_seulement = false;
                            ligne++;
                            while (Extraire_niveau(ligne) > niveau + 1)
                            {
                                string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                                int balise_2_ligne = ligne;
                                switch (balise_2)
                                {
                                    case "_SOUR":                                                       // _QUAL__SOUR Heridis
                                        (info.N2__QUAL__SOUR, ligne) = Extraire_texte_niveau_plus(ligne);
                                        break;
                                    case "_INFO":                                                       // _QUAL__INFO Heridis
                                        (info.N2__QUAL__INFO, ligne) = Extraire_texte_niveau_plus(ligne);
                                        break;
                                    case "_EVID":                                                       // _QUAL__EVED Heridis
                                        (info.N2__QUAL__EVID, ligne) = Extraire_texte_niveau_plus(ligne);
                                        break;
                                    default:
                                        // pour la page HTML
                                        Ligne_perdue cc = new Ligne_perdue()
                                        {
                                            ligne = ligne,
                                            texte = dataGEDCOM[ligne]
                                        };
                                        ligne_perdue_liste.Add(cc);
                                        // rapport Balise
                                        method = MethodBase.GetCurrentMethod().Name;
                                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                        Ligne_perdue c0 = new Ligne_perdue()
                                        {
                                            ligne = balise_0_ligne,
                                            texte = dataGEDCOM[balise_0_ligne]
                                        };
                                        groupe_ligne.Add(c0);
                                        Ligne_perdue c1 = new Ligne_perdue()
                                        {
                                            ligne = balise_1_ligne,
                                            texte = dataGEDCOM[balise_1_ligne]
                                        };
                                        groupe_ligne.Add(c1);
                                        Ligne_perdue c2 = new Ligne_perdue()
                                        {
                                            ligne = balise_2_ligne,
                                            texte = dataGEDCOM[balise_2_ligne]
                                        };
                                        groupe_ligne.Add(c2);
                                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                        groupe_ligne.Clear();
                                        break;
                                }
                            }
                            break;
                        default:
                            source_ID_seulement = false;
                            // pour la page HTML
                            Ligne_perdue dd = new Ligne_perdue()
                            {
                                ligne = ligne,
                                texte = dataGEDCOM[ligne]
                            };
                            ligne_perdue_liste.Add(dd);
                            // rapport Balise
                            method = MethodBase.GetCurrentMethod().Name;
                            lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                            Ligne_perdue d0 = new Ligne_perdue()
                            {
                                ligne = balise_0_ligne,
                                texte = dataGEDCOM[balise_0_ligne]
                            };
                            groupe_ligne.Add(d0);
                            Ligne_perdue d1 = new Ligne_perdue()
                            {
                                ligne = balise_1_ligne,
                                texte = dataGEDCOM[balise_1_ligne]
                            };
                            groupe_ligne.Add(d1);
                            ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                            groupe_ligne.Clear();
                            break;
                    }
                }
            }
            catch (Exception msg)
            {
                GC.Collect();
                string message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                Voir_message(message, msg.Message, GH.GH.erreur);
            }
            // numero de la citation
            if (source_ID_seulement)
            {
                return (null, info.N0_ID_SOUR, ligne);
            }
            info.ligne_perdue_liste = ligne_perdue_liste;
            info.N0_ID_citation = "IC" + String.Format("{0:-00-00-00-00}", ++numero_ID);
            info.source_ID_seulement = source_ID_seulement;
            info.MULTIMEDIA_LINK_liste_ID = MULTIMEDIA_LINK_liste_ID;
            info.N2_DATA_TEXT_liste = N2_DATA_TEXT_liste;
            info.N1_SOUR_liste_ID = N1_SOUR_liste_ID;
            info.N1_REFS_liste_ID = N1_REFS_liste_ID;
            info.N1_TITL_liste = N1_TITL_liste;
            info.N1_REPO_liste = N1_REPO_liste;
            info.N1_NOTE_STRUCTURE_liste_ID = N1_NOTE_STRUCTURE_liste_ID;
            info.N1_TEXT_liste = N1_TEXT_liste;
            info.N1_SOUR_liste_EVEN = N1_SOUR_liste_EVEN;
            info.N1_REFS_liste_EVEN = N1_REFS_liste_EVEN;
            info.N1_ORIG_liste = N1_ORIG_liste;
            info.N1_CENS_liste = N1_CENS_liste;
            liste_SOURCE_CITATION.Add(info);
            //R..Z("extraire citation ID source = " + info.N0_ID_citation + " ID souce=" + info.N0_ID_SOUR + " source seulement="+ info.source_ID_seulement.ToString());
            return (info.N0_ID_citation, info.N0_ID_SOUR, ligne);
        }

        public static bool Extraire_GEDCOM(
            //[CallerLineNumber] int callerLineNumber = 0
            )
        {
            //ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name);
            Regler_code_erreur();
            Stopwatch chrono;
            chrono = Stopwatch.StartNew();
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            int niveau;
            liste_SUBMITTER_RECORD = new List<SUBMITTER_RECORD>();
            liste_NOTE_RECORD = new List<NOTE_RECORD>();
            liste_INDIVIDUAL_RECORD = new List<INDIVIDUAL_RECORD>();
            liste_FAM_RECORD = new List<FAM_RECORD>();
            liste_SOURCE_RECORD = new List<SOURCE_RECORD>();
            liste_MULTIMEDIA_RECORD = new List<MULTIMEDIA_RECORD>();
            liste_REPOSITORY_RECORD = new List<REPOSITORY_RECORD>();
            liste_REPOSITORY_RECORD = new List<REPOSITORY_RECORD>();
            if (GH.GH.annuler) return (false);
            try
            {
                ligne = 1;
                while (dataGEDCOM[ligne].ToUpper() != "0 TRLR".Trim())
                {
                    if (ligne % 1000 == 0) Application.DoEvents();
                    niveau = Extraire_niveau(ligne);
                    string balise_0;
                    if (niveau == 0)
                    {
                        if (GH.GH.annuler) return false;
                        string temp = dataGEDCOM[ligne];
                        string texte = String.Join(" ", temp.ToUpper().Split(new string[] { " " }, // retire les espaces d'extra
                            StringSplitOptions.RemoveEmptyEntries));
                        // séparer la ligne en section
                        char[] espace = { ' ' };
                        string[] section = texte.Split(espace);
                        // niveau de la ligne
                        //int nombre_section = section.Length;
                        if (section.Length == 2) balise_0 = section[1];
                        else balise_0 = section[2];
                        int balise_0_ligne = ligne;
                        switch (balise_0)
                        {
                            case "HEAD":                                                                // HEAD
                                ligne = Extraire_HEADER(ligne); break;
                            case "EVEN":                                                                // EVEN
                                ligne = Extraire_EVEN_RECORD_53(ligne); break;
                            case "FAM":                                                                 // FAM
                                ligne = Extraire_FAM_RECORD(ligne);
                                break;
                            case "INDI":                                                                // INDI
                                ligne = Extraire_INDIVIDUAL_RECORD(ligne);
                                break;
                            case "NOTE":                                                                // NOTE
                                ligne = Extraire_NOTE_RECORD(ligne);
                                break;
                            case "OBJE":                                                                // OBJE
                                ligne = Extraire_OBJE_record(ligne);
                                break;
                            case "REPO":                                                                // REPO
                                ligne = Extraire_REPOSITORY_RECORD(ligne);
                                break;
                            case "SOUR":                                                                // SOUR
                                ligne = Extraire_SOURCE_RECORD(ligne);
                                break;
                            case "SUBN":                                                                //SUBN
                                ligne = Extraire_SUBMISSION_RECORD(ligne);
                                break;
                            case "SUBM":                                                                // SUBM
                                ligne = Extraire_SUBMITTER_RECORD(ligne);
                                break;
                            case "TRLR":                                                                // TRLT
                                return true;
                            default:
                                // rapport Balise
                                string method = MethodBase.GetCurrentMethod().Name;
                                int lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                Ligne_perdue a0 = new Ligne_perdue();
                                {
                                    a0.ligne = balise_0_ligne;
                                    a0.texte = dataGEDCOM[balise_0_ligne];
                                }
                                groupe_ligne.Add(a0);
                                ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                groupe_ligne.Clear();
                                break;
                        }
                    }
                }
            }
            catch (Exception msg)
            {
                GC.Collect();
                string message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                Voir_message(message, msg.Message, GH.GH.erreur);
                if (GH.GH.annuler) return false;
            }
            chrono.Stop();
            TimeSpan durer = TimeSpan.FromMilliseconds(chrono.ElapsedMilliseconds);
            GEDCOMClass.Ecrire_execution_temps("Direct ------------------- ", durer);
            return false;
        }

        public static void Ecrire_execution_temps(string nom, TimeSpan durer)
        {
            Regler_code_erreur();
            string texte = "";
            if (durer.Days > 0) texte += durer.Days + "j ";
            if (durer.Hours > 0) texte += durer.Hours + "h ";
            if (durer.Minutes > 0) texte += durer.Minutes + "m ";
            if (durer.Seconds > 0) texte += durer.Seconds + "s ";
            if (durer.Milliseconds > 0) texte += durer.Milliseconds + "ms ";
            string fichier = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\GH\timer.html";
            try
            {
                using (StreamWriter ligne = File.AppendText(fichier))
                {
                    string s = String.Format(
                        "<table>" +
                        "<tr>" +
                        "<td class=\"col0\">" +
                        "{0}" +
                        "</td>" +
                        "<td class=\"col1\">" +
                        "{1}" +
                        "</td>" +
                        "<td class=\"col2\">" +
                        "{2}" +
                        "</td>" +
                        "</tr>" +
                        "</table>",
                        DateTime.Now, nom, texte);
                    ligne.WriteLine(s);
                }
            }
            catch (Exception msg)
            {
                GC.Collect();
                MessageBox.Show(msg.Message, GH.GH.erreur + " Timer problème ?",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            return;
        }
        private static string Convertir_string_2d(string s)
        {
            if (IsNullOrEmpty(s)) return "00";

            if (!s.All(char.IsDigit)) return "00";
            return s.PadLeft(2, '0');
        }

        private static string Convertir_date_trier(string date)
        {
            Regler_code_erreur();
            string Qd = "99999999"; // question date string
            if (date == null) return Qd;
            if (date == "") return Qd;
            int an1 = 99999999;
            int an2 = 99999999;
            date = date.ToUpper();
            date = Retirer_espace_inutile(date);
            char[] s = { ' ' };
            string[] d = date.Split(s);
            int l = d.Length; // nombre item dans la date ex. CAL 31 DEC 1997 l=4
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
                versions of Family Tree Maker use the illegal four-letter abbreviations ABT., BEF. & AFT.

                INT Intepreted  Interprèter
                CA  Circa       Environ        Utiliser par  Gedcom Publisher
                SAY Say         Dit            Utiliser par  Gedcom Publisher
                OR  or          ou             Utiliser par  Gedcom Publisher
                */
            if (l == 1)
            {
                if (date.All(char.IsDigit)) an1 = int.Parse(date + "0101");
            }
            else if (l == 2)
            {
                // 0        1       2       3       4       5       6       7  
                // ABT      1852
                if (d[0] == "ABT" || d[0] == "ABT.")
                {
                    if (d[1].All(char.IsDigit)) an1 = int.Parse(d[1] + "0101");
                }
                // 0        1       2       3       4       5       6       7  
                // AFT      1852
                else if (d[0] == "AFT" || d[0] == "AFT.")
                {
                    if (d[1].All(char.IsDigit)) an1 = int.Parse(d[1] + "0101");
                }
                // 0        1       2       3       4       5       6       7  
                // BEF      1852
                else if (d[0] == "BEF" || d[0] == "BEF.")
                {
                    if (d[1].All(char.IsDigit)) an1 = int.Parse(d[1] + "0101");
                }
                // 0        1       2       3       4       5       6       7  
                // EST      1852
                else if (d[0] == "EST" || d[0] == "EST.")
                {
                    if (d[1].All(char.IsDigit)) an1 = int.Parse(d[1] + "0101");
                }
                // 0        1       2       3       4       5       6       7  
                // INT      1852
                else if (d[0] == "INT")
                {
                    if (d[1].All(char.IsDigit)) an1 = int.Parse(d[1] + "0101");
                }
                // 0        1       2       3       4       5       6       7
                // CA      1852
                else if (d[0] == "CA")
                {
                    if (d[1].All(char.IsDigit)) an1 = int.Parse(d[1] + "0101");
                }
                // 0        1       2       3       4       5       6       7
                // SAY      1852
                else if (d[0] == "SAY")
                {
                    if (d[1].All(char.IsDigit)) an1 = int.Parse(d[1] + "0101");
                }
                // 0        1       2       3       4       5       6       7
                // CAL      1852
                else if (d[0] == "CAL" || d[0] == "CAL.")
                {
                    if (d[1].All(char.IsDigit)) an1 = int.Parse(d[1] + "0101");
                }
                // 0        1       2       3       4       5       6       7
                // TO       1852
                else if (d[0] == "TO")
                {
                    if (d[1].All(char.IsDigit)) an1 = int.Parse(d[1] + "0101");
                }
                // 0        1       2       3       4       5       6       7
                // FROM 1852
                else if (d[0] == "FROM")
                {
                    if (d[1].All(char.IsDigit)) an1 = int.Parse(d[1] + "0101");
                }
                // DEC      1852
                // 0        1       2       3       4       5       6       7
                else
                {
                    if (d[1].All(char.IsDigit)) an1 = int.Parse(d[1] + Convertir_mois_chiffre(d[0]) + "01");
                }
            }
            else if (l == 3)
            {
                // 0        1       2       3       4       5       6       7  
                // ABT      DEC     1852
                if (d[0] == "ABT" || d[0] == "ABT.")
                {
                    if (d[2].All(char.IsDigit)) an1 = int.Parse(d[2] + Convertir_mois_chiffre(d[1]) + "01");
                }
                // 0        1       2       3       4       5       6       7
                // AFT      DEC     1852
                else if (d[0] == "AFT" || d[0] == "AFT.")
                {
                    if (d[2].All(char.IsDigit)) an1 = int.Parse(d[2] + Convertir_mois_chiffre(d[1]) + "01");
                }
                // 0        1       2       3       4       5       6       7
                // BEF      DEC     1852
                else if (d[1] == "BEF" || d[1] == "BEF.")
                {
                    if (d[2].All(char.IsDigit)) an1 = int.Parse(d[2] + Convertir_mois_chiffre(d[1]) + "01");
                }
                // 0        1       2       3       4       5       6       7
                // CAL      DEC     1852
                else if (d[0] == "CAL" || d[0] == "CAL")
                {
                    if (d[2].All(char.IsDigit)) an1 = int.Parse(d[2] + Convertir_mois_chiffre(d[1]) + "01");
                }
                // 0        1       2       3       4       5       6       7
                // EST      DEC     1852
                else if (d[0] == "EST" || d[0] == "EST.")
                {
                    if (d[2].All(char.IsDigit)) an1 = int.Parse(d[2] + Convertir_mois_chiffre(d[1]) + "01");
                }
                // 0        1       2       3       4       5       6       7
                // INT      DEC     1852
                else if (d[0] == "INT")
                {
                    if (d[2].All(char.IsDigit)) an1 = int.Parse(d[2] + Convertir_mois_chiffre(d[1]) + "01");
                }
                // 0        1       2       3       4       5       6       7
                // CA      DEC     1852
                else if (d[0] == "CA")
                {
                    if (d[2].All(char.IsDigit)) an1 = int.Parse(d[2] + Convertir_mois_chiffre(d[1]) + "01");
                }
                // 0        1       2       3       4       5       6       7
                // SAY      DEC     1852
                else if (d[0] == "SAY")
                {
                    if (d[2].All(char.IsDigit)) an1 = int.Parse(d[2] + Convertir_mois_chiffre(d[1]) + "01");
                }
                // 0        1       2       3       4       5       6       7
                // TO       DEC     1852
                else if (d[0] == "TO")
                {
                    if (d[2].All(char.IsDigit)) an1 = int.Parse(d[2] + Convertir_mois_chiffre(d[1]) + "01");
                }
                // 0        1       2       3       4       5       6       7
                // 1853     OR      1853
                else if (d[1] == "OR")
                {
                    if (d[0].All(char.IsDigit)) an1 = int.Parse(d[0] + "0101");
                    if (d[2].All(char.IsDigit)) an2 = int.Parse(d[2] + "0101");
                }
                // 0        1       2       3       4       5       6       7
                // 24       DEC     1852
                else
                {
                    if (d[2].All(char.IsDigit)) an1 = int.Parse(d[2] + Convertir_mois_chiffre(d[1]) + Convertir_string_2d(d[0]));
                }

            }
            else if (l == 4)
            {
                // 0        1       2       3       4       5       6       7  
                // ABT      24      DEC     1852
                if (d[0] == "ABT" || d[0] == "ABT.")
                {
                    if (d[3].All(char.IsDigit)) an1 = int.Parse(d[3] + Convertir_mois_chiffre(d[2]) + Convertir_string_2d(d[1]));
                }
                // 0        1       2       3       4       5       6       7
                // BEF      24       DEC    1852
                else if (d[0] == "BEF" || d[0] == "BEF.")
                {
                    if (d[3].All(char.IsDigit)) an1 = int.Parse(d[3] + Convertir_mois_chiffre(d[2]) + Convertir_string_2d(d[1]));
                }
                // 0        1       2       3       4       5       6       7
                // AFT      24      DEC     1852
                else if (d[0] == "AFT" || d[0] == "AFT")
                {
                    if (d[3].All(char.IsDigit)) an1 = int.Parse(d[3] + Convertir_mois_chiffre(d[2]) + Convertir_string_2d(d[1]));
                }
                // 0        1       2       3       4       5       6       7
                // BET     1852    AND     1853
                else if ((d[0] == "BET" || d[0] == "BET.") && d[2] == "AND")
                {
                    if (d[1].All(char.IsDigit)) an1 = int.Parse(d[1] + "0101");
                    if (d[3].All(char.IsDigit)) an2 = int.Parse(d[3] + "0101");

                }
                // 0        1       2       3       4       5       6       7
                // FROM     1852    TO      1853
                else if (d[0] == "FROM" && d[2] == "TO")
                {
                    if (d[1].All(char.IsDigit)) an1 = int.Parse(d[1] + "0101");
                    if (d[3].All(char.IsDigit)) an2 = int.Parse(d[3] + "0101");
                }
                // 0        1       2       3       4       5       6       7
                // FROM     01      JAN     1852
                else if (d[0] == "FROM")
                {
                    if (d[3].All(char.IsDigit)) an1 = int.Parse(d[3] + Convertir_mois_chiffre(d[2]) + Convertir_string_2d(d[1]));
                }
                // 0        1       2       3       4       5       6       7
                // CAL      24      DEC     1852
                else if (d[0] == "CAL" || d[0] == "CAL.")
                {
                    if (d[3].All(char.IsDigit)) an1 = int.Parse(d[3] + Convertir_mois_chiffre(d[2]) + Convertir_string_2d(d[1]));
                }
                // 0        1       2       3       4       5       6       7
                // EST      24      DEC     1852
                else if (d[0] == "EST" || d[0] == "EST.")
                {
                    if (d[3].All(char.IsDigit)) an1 = int.Parse(d[3] + Convertir_mois_chiffre(d[2]) + Convertir_string_2d(d[1]));
                }
                // 0        1       2       3       4       5       6       7
                // INT      24      DEC     1852
                else if (d[0] == "INT")
                {
                    if (d[3].All(char.IsDigit)) an1 = int.Parse(d[3] + Convertir_mois_chiffre(d[2]) + Convertir_string_2d(d[1]));
                }
                // 0        1       2       3       4       5       6       7
                // CA      24      DEC     1852
                else if (d[0] == "CA")
                {
                    if (d[3].All(char.IsDigit)) an1 = int.Parse(d[3] + Convertir_mois_chiffre(d[2]) + Convertir_string_2d(d[1]));
                }
                // 0        1       2       3       4       5       6       7
                // SAY      24      DEC     1852
                else if (d[0] == "SAY")
                {
                    if (d[3].All(char.IsDigit)) an1 = int.Parse(d[3] + Convertir_mois_chiffre(d[2]) + Convertir_string_2d(d[1]));
                }
                // 0        1       2       3       4       5       6       7
                // TO       24      DEC     1852
                else if (d[0] == "TO")
                {
                    if (d[3].All(char.IsDigit)) an1 = int.Parse(d[3] + Convertir_mois_chiffre(d[2]) + Convertir_string_2d(d[1]));
                }
                // 0        1       2       3       4       5       6       7
                // 1853     OR      JAN     1853
                else if (d[1] == "OR")
                {
                    if (d[0].All(char.IsDigit)) an1 = int.Parse(d[0] + "0101");
                    if (d[3].All(char.IsDigit)) an2 = int.Parse(d[3] + Convertir_mois_chiffre(d[2]) + "01");
                }
                // 0        1       2       3       4       5       6       7
                // JAN      1853    OR      1854
                else if (d[2] == "OR")
                {
                    if (d[1].All(char.IsDigit)) an1 = int.Parse(d[1] + Convertir_mois_chiffre(d[0]));
                    if (d[3].All(char.IsDigit)) an2 = int.Parse(d[3] + "0101");
                }
                else GEDCOMClass.DEBUG("Problème pour convertir la date " + date); // ### DEBUG ###
            }
            else if (l == 5)
            {
                // 0        1       2       3       4       5       6       7  
                // BET     1852    AND     DEC     1853
                if ((d[0] == "BET" || d[0] == "BET.") && d[2] == "AND")
                {
                    if (d[1].All(char.IsDigit)) an1 = int.Parse(d[1] + "0101");
                    if (d[4].All(char.IsDigit)) an2 = int.Parse(d[4] + Convertir_mois_chiffre(d[3]) + "01");
                }
                // 0        1       2       3       4       5       6       7
                // BET     DEC     1852    AND     1853 
                else if ((d[0] == "BET" || d[0] == "BET.") && d[3] == "AND")
                {
                    if (d[2].All(char.IsDigit)) an1 = int.Parse(d[2] + Convertir_mois_chiffre(d[1]) + "01");
                    if (d[4].All(char.IsDigit)) an2 = int.Parse(d[4] + "0101");
                }
                // 0        1       2       3       4       5       6       7
                // FROM     1852    TO      DEC     1853
                else if (d[0] == "FROM" && d[2] == "TO")
                {
                    if (d[1].All(char.IsDigit)) an1 = int.Parse(d[1] + "0101");
                    if (d[4].All(char.IsDigit)) an2 = int.Parse(d[4] + Convertir_mois_chiffre(d[3]) + "01");
                }
                // 0        1       2       3       4       5       6       7
                // FROM     DEC     1852    TO      1853
                else if (d[0] == "FROM" && d[3] == "TO")
                {
                    if (d[2].All(char.IsDigit)) an1 = int.Parse(d[2] + Convertir_mois_chiffre(d[1]) + "01");
                    if (d[4].All(char.IsDigit)) an2 = int.Parse(d[4] + "0101");
                }
                // 0        1       2       3       4       5       6       7
                // 1853     OR      1       JAN     1953 
                else if (d[1] == "OR")
                {
                    if (d[0].All(char.IsDigit)) an1 = int.Parse(d[0] + "0101");
                    if (d[4].All(char.IsDigit)) an2 = int.Parse(d[4] + Convertir_mois_chiffre(d[3]) + Convertir_string_2d(d[2]));
                }
                // 0        1       2       3       4       5       6       7
                // JAN      1853    OR      JAN     1854
                else if (d[2] == "OR")
                {
                    if (d[1].All(char.IsDigit)) an1 = int.Parse(d[1] + "0101");
                    if (d[4].All(char.IsDigit)) an2 = int.Parse(d[4] + Convertir_mois_chiffre(d[3]) + "01");
                }
                // 0        1       2       3       4       5       6       7
                // 1        JAN     1853    OR      1854    
                else if (d[3] == "OR")
                {
                    if (d[2].All(char.IsDigit)) an1 = int.Parse(d[2] + Convertir_mois_chiffre(d[1]) + Convertir_string_2d(d[0]));
                    if (d[4].All(char.IsDigit)) an2 = int.Parse(d[4] + "0101");
                }
                else GEDCOMClass.DEBUG("Problème pour convertir la date " + date); // ### DEBUG ###
            }
            else if (l == 6)
            {
                // 0        1       2       3       4       5       6       7  
                // BET     1852    AND     24      DEC     1853
                if ((d[0] == "BET" || d[0] == "BET.") && d[2] == "AND")
                {
                    if (d[1].All(char.IsDigit)) an1 = int.Parse(d[1] + "0101");
                    if (d[5].All(char.IsDigit)) an2 = int.Parse(d[5] + Convertir_mois_chiffre(d[4]) + Convertir_string_2d(d[3]));
                }
                // 0        1       2       3       4       5       6       7
                // BET     DEC     1852    AND     DEC     1853
                else if ((d[0] == "BET" || d[0] == "BET.") && d[3] == "AND")
                {
                    if (d[2].All(char.IsDigit)) an1 = int.Parse(d[2] + Convertir_mois_chiffre(d[1]) + "01");
                    if (d[5].All(char.IsDigit)) an2 = int.Parse(d[5] + Convertir_mois_chiffre(d[4]) + "01");
                }
                // 0        1       2       3       4       5       6       7  
                // BET      24     DEC     1852    AND     1853
                else if ((d[0] == "BET" || d[0] == "BET.") && d[4] == "AND")
                {
                    if (d[3].All(char.IsDigit)) an1 = int.Parse(d[3] + Convertir_mois_chiffre(d[2]) + Convertir_string_2d(d[1]));
                    if (d[5].All(char.IsDigit)) an2 = int.Parse(d[5] + "0101");
                }
                // 0        1       2       3       4       5       6       7
                // FROM     24      DEC     1852    TO      1853
                else if (d[0] == "FROM" && d[4] == "TO")
                {
                    if (d[3].All(char.IsDigit)) an1 = int.Parse(d[3] + Convertir_mois_chiffre(d[2]) + Convertir_string_2d(d[1]));
                    if (d[5].All(char.IsDigit)) an2 = int.Parse(d[5] + "0101");
                }
                // 0        1       2       3       4       5       6       7  
                // FROM     DEC     1852    TO      DEC     1853
                else if (d[0] == "FROM" && d[3] == "TO")
                {
                    if (d[2].All(char.IsDigit)) an1 = int.Parse(d[2] + Convertir_mois_chiffre(d[1]) + "01");
                    if (d[5].All(char.IsDigit)) an2 = int.Parse(d[5] + Convertir_mois_chiffre(d[4]) + "01");
                }
                // 0        1       2       3       4       5       6       7  
                // FROM     24      DEC     1852    TO      1853    
                else if (d[0] == "FROM" && d[4] == "TO")
                {
                    if (d[3].All(char.IsDigit)) an1 = int.Parse(d[1] + d[2] + Convertir_mois_chiffre(d[1]));
                    if (d[5].All(char.IsDigit)) an2 = int.Parse(d[5] + "0101");
                }
                // 0        1       2       3       4       5       6       7  
                // JAN      1853   OR       1       JAN     1853
                else if (d[0] == "FROM" && d[4] == "TO")
                {
                    if (d[1].All(char.IsDigit)) an1 = int.Parse(d[1] + d[0] + "0101");
                    if (d[5].All(char.IsDigit)) an2 = int.Parse(d[5] + Convertir_mois_chiffre(d[4]) + Convertir_string_2d(d[3]));
                }
                // 0        1       2       3       4       5       6       7
                // JAN      1853    OR      1       JAN     1854
                else if (d[2] == "OR")
                {
                    if (d[1].All(char.IsDigit)) an1 = int.Parse(d[1] + Convertir_mois_chiffre(d[0]) + "01");
                    if (d[5].All(char.IsDigit)) an2 = int.Parse(d[5] + Convertir_mois_chiffre(d[4]) + Convertir_string_2d(d[3]));
                }
                // 0        1       2       3       4       5       6       7
                // 1        JAN     1853    OR      JAN     1854
                else if (d[3] == "OR")
                {
                    if (d[2].All(char.IsDigit)) an1 = int.Parse(d[2] + Convertir_mois_chiffre(d[1]) + Convertir_string_2d(d[0]));
                    if (d[5].All(char.IsDigit)) an2 = int.Parse(d[5] + Convertir_mois_chiffre(d[4]) + "01");
                }
                else GEDCOMClass.DEBUG("Problème pour convertir la date " + date); // ### DEBUG ###
            }
            else if (l == 7)
            {
                // 0        1       2       3       4       5       6       7   
                // BET      DEC     1852    AND     24      DEC     1853
                if ((d[0] == "BET" || d[0] == "BET.") && d[3] == "AND")
                {
                    if (d[2].All(char.IsDigit)) an1 = int.Parse(d[2] + Convertir_mois_chiffre(d[1]) + "01");
                    if (d[6].All(char.IsDigit)) an2 = int.Parse(d[6] + Convertir_mois_chiffre(d[5]) + Convertir_string_2d(d[4]));
                }
                // 0        1       2       3       4       5       6       7
                // BET      24     DEC     1852    AND      DEC     1853
                else if ((d[0] == "BET" || d[0] == "BET.") && d[4] == "AND")
                {
                    if (d[3].All(char.IsDigit)) an1 = int.Parse(d[3] + Convertir_mois_chiffre(d[2]) + Convertir_string_2d(d[1]));
                    if (d[6].All(char.IsDigit)) an2 = int.Parse(d[6] + Convertir_mois_chiffre(d[5]) + "01");
                }
                // 0        1       2       3       4       5       6       7   
                // FROM     DEC     1852    TO      24      DEC     1853
                else if (d[0] == "FROM" && d[3] == "TO")
                {
                    if (d[2].All(char.IsDigit)) an1 = int.Parse(d[2] + Convertir_mois_chiffre(d[1]) + "01");
                    if (d[6].All(char.IsDigit)) an2 = int.Parse(d[6] + Convertir_mois_chiffre(d[5]) + Convertir_string_2d(d[4]));
                }
                // 0        1       2       3       4       5       6       7
                // FROM     24      DEC     1852    TO      DEC     1853    
                else if (d[0] == "FROM" && d[4] == "TO")
                {
                    if (d[3].All(char.IsDigit)) an1 = int.Parse(d[3] + Convertir_mois_chiffre(d[2]) + Convertir_string_2d(d[1]));
                    if (d[6].All(char.IsDigit)) an2 = int.Parse(d[6] + Convertir_mois_chiffre(d[5]) + "01");
                }
                // 0        1       2       3       4       5       6       7
                // FROM     DEC     1852    TO      24      DEC     1853    
                else if (d[0] == "FROM" && d[4] == "TO")
                {
                    if (d[2].All(char.IsDigit)) an1 = int.Parse(d[2] + Convertir_mois_chiffre(d[1]) + "01");
                    if (d[6].All(char.IsDigit)) an2 = int.Parse(d[6] + Convertir_mois_chiffre(d[5]) + Convertir_string_2d(d[4]));
                }
                // 0        1       2       3       4       5       6       7   
                // 31       DEC     1852    TO      24      DEC     1853    
                else if (d[3] == "TO")
                {
                    if (d[2].All(char.IsDigit)) an1 = int.Parse(d[2] + Convertir_mois_chiffre(d[1]) + Convertir_string_2d(d[0]));
                    if (d[6].All(char.IsDigit)) an2 = int.Parse(d[6] + Convertir_mois_chiffre(d[5]) + Convertir_string_2d(d[4]));
                }
                // 0        1       2       3       4       5       6       7
                // 1        JAN     1853    OR      1       JAN     1854
                else if (d[3] == "OR")
                {
                    if (d[2].All(char.IsDigit)) an1 = int.Parse(d[2] + Convertir_mois_chiffre(d[1]) + Convertir_string_2d(d[0]));
                    if (d[6].All(char.IsDigit)) an2 = int.Parse(d[6] + Convertir_mois_chiffre(d[5]) + Convertir_string_2d(d[4]));
                }
                else GEDCOMClass.DEBUG("Problème pour convertir la date " + date); // ### DEBUG ###
            }
            else if (l == 8)
            {
                // 0        1       2       3       4       5       6       7   
                // BET      24     DEC     1852    AND     24      DEC     1853
                if ((d[0] == "BET" || d[0] == "BET.") && d[4] == "AND")
                {
                    if (d[3].All(char.IsDigit)) an1 = int.Parse(d[3] + Convertir_mois_chiffre(d[2]) + Convertir_string_2d(d[1]));
                    if (d[7].All(char.IsDigit)) an2 = int.Parse(d[7] + Convertir_mois_chiffre(d[6]) + Convertir_string_2d(d[5]));
                }
                // 0        1       2       3       4       5       6       7
                // FROM     24      DEC     1852    TO      24      DEC     1853    
                else if (d[0] == "FROM" && d[4] == "TO")
                {
                    if (d[3].All(char.IsDigit)) an1 = int.Parse(d[3] + Convertir_mois_chiffre(d[2]) + Convertir_string_2d(d[1]));
                    if (d[7].All(char.IsDigit)) an2 = int.Parse(d[7] + Convertir_mois_chiffre(d[6]) + Convertir_string_2d(d[5]));
                }
                else GEDCOMClass.DEBUG("Problème pour convertir la date " + date); // ### DEBUG ###
            }
            else GEDCOMClass.DEBUG("Problème pour convertir la date " + date); // ### DEBUG ###
            if (an1 > an2) return an2.ToString();
            else return an1.ToString();
        }
        private static string Convertir_mois_chiffre(string s)
        {
            s = s.ToUpper();
            switch (s)
            {
                case "JAN": case "JANUARY": return "01";
                case "FEB": case "FEBRUARY": return "02";
                case "MAR": case "MARS": return "03";
                case "APR": case "APRIL": return "04";
                case "MAY": return "05";
                case "JUN": case "JUNE": return "06";
                case "JUL": case "JULY": return "07";
                case "AUG": case "AUGUST": return "08";
                case "SEP": case "SEPTEMBER": return "09";
                case "OCT": case "OCTOBER": return "10";
                case "NOV": case "NOVEMBER": return "11";
                case "DEC": case "DECEMBER": return "12";
            }
            return "99";
        }

        private static int Ecrire_balise
            (
            int ligne_GEDCOM,
            string function,
            int ligne_code,
            List<Ligne_perdue> liste
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b>");
            Regler_code_erreur();
            System.Windows.Forms.Button Btn_balise = Application.OpenForms["GH"].Controls["Btn_balise"] as
                System.Windows.Forms.Button;
            try
            {
                if (liste == null) return ligne_GEDCOM++;

                {
                    if (!File.Exists(GH.GH.fichier_balise))
                    {
                        using (StreamWriter ligne = File.AppendText(GH.GH.fichier_balise))
                        {
                            ligne.WriteLine(
                                "<!DOCTYPE html>\n" +
                                "    <html lang=\"fr\" style=\"background-color:#FFF;\">\n" +
                                "        <head>\n" +
                                "           <title>Balise</title>" +
                                "           <style>\n" +
                                "               h1{color:#00F}\n" +
                                "               .col0{width:150px;vertical-align:top;}\n " +
                                "               .col1{width:90px;vertical-align:top;}\n " +
                                "               .col2{width:50px;vertical-align:top;}\n " +
                                "               .col3{width:330px;vertical-align:top;}\n " +
                                "               .col4{vertical-align:top;}\n " +
                                "           .navbar {\n" +
                                "               overflow: hidden;\n" +
                                "               position: fixed;\n" +
                                "               top: 0;\n" +
                                "               background-color: #FFF;\n" +
                                "               width: 100%;\n" +
                                "           }\n" +
                                "           </style>\n" +
                                "        </head>");
                            Btn_balise.Visible = true;
                            //ligne.WriteLine(DateTime.Now);
                            ligne.WriteLine("<div class=\"navbar\">");
                            ligne.WriteLine("<h1>Balise</h1>");
                            ligne.WriteLine("<table style=\"border:2px solid #000;width:100%\">");
                            ligne.WriteLine("\t<tr><td style=\"width:200px\">Nom</td><td>" + info_HEADER.N2_SOUR_NAME + "</td><td></tr>");
                            ligne.WriteLine("\t<tr><td>Version</td><td>" + info_HEADER.N2_SOUR_VERS + "<td></tr>");
                            ligne.WriteLine("\t<tr><td>Date</td><td>" + info_HEADER.N1_DATE + " " + info_HEADER.N2_DATE_TIME + "<td></tr>");
                            ligne.WriteLine("\t<tr><td>Copyright</td><td>" + info_HEADER.N1_COPR + "<td></tr>");
                            ligne.WriteLine("\t<tr><td>Version</td><td>" + info_HEADER.N2_GEDC_VERS + "<td></tr>");
                            ligne.WriteLine("\t<tr><td>Code charactère</td><td>" + info_HEADER.N1_CHAR + "<td></tr>");
                            ligne.WriteLine("\t<tr><td>Langue</td><td>" + info_HEADER.N1_LANG + "<td></tr>");
                            ligne.WriteLine("\t<tr><td>Fichier sur le disque</td><td>" + info_HEADER.Nom_fichier_disque + "<td></tr>");
                            System.Version version;
                            /*try
                            {
                                version = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                            }
                            catch
                            {
                            */
                            GC.Collect();
                            version = Assembly.GetExecutingAssembly().GetName().Version;
                            //}
                            ligne.WriteLine("\t<tr><td>Version de GH</td><td>" + version.Major + "." + version.Minor + "." + version.Build + "<td></tr>");
                            ligne.WriteLine("</table>");
                            ligne.WriteLine(
                                "<table>" +
                                "<tr>" +
                                "<td class=\"col0\">" +
                                "Date heure" +
                                "</td>" +
                                "<td class=\"col2\">" +
                                "Ligne" +
                                "</td>" +
                                "<td class=\"col3\">" +
                                "Routine" +
                                "</td>" +
                                "<td>" +
                                "Balise" +
                                "</td>" +
                                "</table>" +
                                "<hr>");
                            ligne.WriteLine("</div>");
                            ligne.WriteLine("<div style=\"margin-top: 325px;\">\n");
                            ligne.WriteLine("</div>\n");
                        }
                    }
                    using (StreamWriter ligne = File.AppendText(GH.GH.fichier_balise))
                    {
                        // battir le texte des balise
                        string texte = "";
                        for (int f = 0; f < liste.Count; f++)
                        {
                            texte += System.Environment.NewLine + String.Concat(Enumerable.Repeat("&emsp;", f));
                            texte += liste[f].ligne + " ►" + liste[f].texte + "<br />";
                        }
                        string s = String.Format(
                            "<table>" +
                            "<tr>" +
                            "<td class=\"col0\">" +
                            "{0}" +
                            "</td>" +
                            "<td class=\"col2\">" +
                            "{1}" +
                            "</td>" +
                            "<td class=\"col3\">" +
                            "{2}" +
                            "</td>" +
                            "<td>" +
                            "{3}" +
                            "</td>" +
                            "</tr>" +
                            "</table>" +
                            "<hr>", DateTime.Now, ligne_code, function,/* ligne_GEDCOM,*/ texte);
                        ligne.WriteLine(s);
                    }
                }
            }
            catch (Exception msg)
            {
                GC.Collect();
                DialogResult reponse;
                reponse = MessageBox.Show("Ne peut écrire le fichier " + GH.GH.fichier_balise + "\r\n\r\n" +
                    msg.Message +
                    "\r\n\r\n",
                    "Erreur " + GH.GH.erreur + " problème ?",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                if (reponse == System.Windows.Forms.DialogResult.Cancel)
                    GH.GH.annuler = true;
            }
            ligne_GEDCOM++;
            return ligne_GEDCOM;
        }

        public static string Avoir_balise(string ligne)
        {
            // retourne la balise
            // mette en majuscule
            ligne = ligne.ToUpper();
            // retire les espace d'extra
            ligne = String.Join(" ", ligne.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
            // divise en section 
            string[] s = ligne.Split(' ');
            // si niveau 0
            if (s[0] == "0")
            {
                if (s.Length > 2) return s[2];
            }
            // niveau > 0
            if (s.Length > 1) return s[1];
            return null;
        }
        public static string Avoir_2_balise(string ligne)
        {
            // retourne la balise
            // mette en majuscule
            ligne = ligne.ToUpper();
            // retire les espace d'extra
            ligne = String.Join(" ", ligne.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
            // divise en section 
            string[] s = ligne.Split(' ');
            // si niveau 0
            if (s[0] == "0")
            {
                if (s.Length > 2) return s[2];
            }
            // niveau > 0
            if (s.Length > 1)  return s[0] + " " + s[1];
            return null;
        }
        public static string Avoir_balise_p1(string ligne)
        {
            // le niveau doit être plus grand que 0
            // retourne la balise avec son niveau qui doit être en position 1
            // mette en majuscule
            ligne = ligne.ToUpper();
            // retire les espace d'extra
            ligne = String.Join(" ", ligne.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
            // divise en section 
            string[] s = ligne.Split(' ');
            // si plus qu'une section
            if (s.Length > 1)
                return s[0] + " " + s[1];
            return "";
        }
        public static string Extraire_ID(string s)
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

        private static string Extraire_texte(int numero_ligne)
        {
            string texte = dataGEDCOM[numero_ligne];
            texte = texte.TrimStart();
            texte = texte.TrimEnd();
            // retire les espace d'extra
            // string ligne_0 = Remplacer_double_espace(dataGEDCOM[ligne]);
            string texte2 = texte[0].ToString();
            for (int f = 1; f < texte.Count(); f++)
            {
                if (texte[f] == ' ' && texte[f - 1] == ' ')
                {
                    texte2 += "#A#A#A#";
                }
                else texte2 += texte[f].ToString();
            }
            char[] espace = { ' ' };
            string[] section = texte2.Split(espace);
            int nombre_section = section.Length;
            if (nombre_section < 3) return null;
            texte2 = texte.Substring(section[0].Length + section[1].Length + 2);
            texte2 = Retirer_espace_inutile(texte2);
            return texte2;
        }

        private static string Extraire_ligne(string ligne, int nombre_charactere_balise)
        {
            int nombre = nombre_charactere_balise + 3;
            if (ligne.Length > nombre)
            {
                return ligne.Substring(nombre, ligne.Length - nombre);
            }
            return null;
        }
        private static string Extraire_NAME(string s)
        {
            if (IsNullOrEmpty(s)) return null;
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
                deux = deux.TrimEnd('/');
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

        private static (NAME_STRUCTURE_53, int, List<Ligne_perdue>) Extraire_NAME_STRUCTURE_53(
            int ligne,
            int niveau,
            List<Ligne_perdue> ligne_perdue_liste
            //[CallerLineNumber] int callerLineNumber = 0
            )
        {
            //GEDCOMClass.ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b> GEDCOM=" + ligne + "&nbsp;&nbsp;niveau=" + niveau);
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            //string balise_0 = Avoir_balise(dataGEDCOM[ligne]);
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            string n0_name;
            (n0_name, ligne) = Extraire_texte_niveau_plus(ligne);
            NAME_STRUCTURE_53 info_NAME_STRUCTURE_53 = new NAME_STRUCTURE_53
            {
                N1_SOUR_citation_liste_ID = new List<string>(),
                N1_NOTE_STRUCTURE_liste_ID = new List<string>(),
                N0_NAME = n0_name
            };
            info_NAME_STRUCTURE_53.N0_NAME = Extraire_NAME(info_NAME_STRUCTURE_53.N0_NAME);
            ligne++;
            while (Extraire_niveau(ligne) > niveau)
            {
                string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                int balise_1_ligne = ligne;
                switch (balise_1)
                {
                    case "TYPE":                                                                        // TYPE
                        (info_NAME_STRUCTURE_53.N1_TYPE, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "SOUR":                                                                        // SOUR
                        string citation;
                        string source;
                        (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                        if (IsNotNullOrEmpty(citation)) info_NAME_STRUCTURE_53.N1_SOUR_citation_liste_ID.Add(citation);
                        if (IsNotNullOrEmpty(source) && IsNullOrEmpty(citation)) info_NAME_STRUCTURE_53.N1_SOUR_source_liste_ID.Add(source);
                        break;
                    case "NOTE":                                                                        // 1 NOTE
                        string note_ID;
                        (note_ID, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                        info_NAME_STRUCTURE_53.N1_NOTE_STRUCTURE_liste_ID.Add(note_ID);
                        break;
                    default:
                        // pour la page HTML
                        Ligne_perdue aa = new Ligne_perdue()
                        {
                            ligne = ligne,
                            texte = dataGEDCOM[ligne]
                        };
                        ligne_perdue_liste.Add(aa);
                        // rapport Balise
                        method = MethodBase.GetCurrentMethod().Name;
                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                        Ligne_perdue a0 = new Ligne_perdue()
                        {
                            ligne = balise_0_ligne,
                            texte = dataGEDCOM[balise_0_ligne]
                        };
                        groupe_ligne.Add(a0);
                        Ligne_perdue a1 = new Ligne_perdue()
                        {
                            ligne = balise_1_ligne,
                            texte = dataGEDCOM[balise_1_ligne]
                        };
                        groupe_ligne.Add(a1);
                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                        groupe_ligne.Clear();
                        break;
                }
            }
            return (info_NAME_STRUCTURE_53, ligne, ligne_perdue_liste);
        }

        private static int Extraire_HEADER(int ligne
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b> GEDCOM=" + ligne);

            Regler_code_erreur();
            List<Ligne_perdue> ligne_perdue_liste = new List<Ligne_perdue>();
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            string temp;
            info_HEADER.N1_SOUR = null;
            info_HEADER.N2_SOUR_VERS = null;
            info_HEADER.N2_SOUR_NAME = null;
            info_HEADER.N2_SOUR_CORP = null;
            info_HEADER.N3_SOUR_CORP_SITE = null;
            List<ADDRESS_STRUCTURE> N3_SOUR_CORP_ADDR_liste = new List<ADDRESS_STRUCTURE>();
            List<string> N3_SOUR_CORP_PHON_liste = new List<string>();
            List<string> N3_SOUR_CORP_EMAIL_liste = new List<string>();
            List<string> N3_SOUR_CORP_FAX_liste = new List<string>();
            List<string> N3_SOUR_CORP_WWW_liste = new List<string>();
            info_HEADER.N2_SOUR_DATA = null;
            info_HEADER.N3_SOUR_DATA_DATE = null;
            info_HEADER.N3_SOUR_DATA_CORP = null;
            info_HEADER.N1_DEST = null;
            info_HEADER.N1_DATE = null;
            info_HEADER.N2_DATE_TIME = null;
            info_HEADER.N1_SUBM_liste_ID = null;
            info_HEADER.N1_SUBN = null;
            info_HEADER.N1_FILE = null;
            info_HEADER.N1_COPR = null;
            info_HEADER.N2_GEDC_VERS = null;
            info_HEADER.N2_GEDC_FORM = null;
            info_HEADER.N3_GEDC_FORM_VERS = null;
            info_HEADER.N1_CHAR = null;
            info_HEADER.N2_CHAR_VERS = null;
            info_HEADER.N1_LANG = null;
            List<string> N1_NOTE_STRUCTURE_liste_ID = new List<string>();
            info_HEADER.N1__GUID = null;
            List<string> N1_SUBM_liste_ID = new List<string>();
            ligne++;
            try
            {

                while (Extraire_niveau(ligne) > 0)
                {
                    //string baliseN1 = Avoir_balise_p1(dataGEDCOM[ligne]);
                    string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                    int balise_1_ligne = ligne;
                    switch (balise_1)
                    {
                        case "DEST":                                                                    // 1 DEST
                            (info_HEADER.N1_DEST, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "SOUR":                                                                    // 1 SOUR
                            {
                                string balise_1_texte;
                                (balise_1_texte, ligne) = Extraire_texte_niveau_plus(ligne);
                                info_HEADER.N1_SOUR = balise_1_texte;
                                while (Extraire_niveau(ligne) > 1)
                                {
                                    string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                                    int balise_2_ligne = ligne;
                                    switch (balise_2)
                                    {
                                        case "VERS":                                                    // 2 VERS
                                            (info_HEADER.N2_SOUR_VERS, ligne) = Extraire_texte_niveau_plus(ligne);
                                            break;
                                        case "NAME":                                                    // 2 NAME
                                            (info_HEADER.N2_SOUR_NAME, ligne) = Extraire_texte_niveau_plus(ligne);
                                            break;
                                        case "CORP":                                                    // 2 CORP
                                            {
                                                (info_HEADER.N2_SOUR_CORP, ligne) = Extraire_texte_niveau_plus(ligne);
                                                while (Extraire_niveau(ligne) > 2)
                                                {
                                                    string balise_3 = Avoir_balise(dataGEDCOM[ligne]);
                                                    int balise_3_ligne = ligne;
                                                    switch (balise_3)
                                                    {
                                                        case "SITE":                                    // 3 SITE
                                                            (info_HEADER.N3_SOUR_CORP_SITE, ligne) = Extraire_texte_niveau_plus(ligne);
                                                            break;
                                                        case "ADDR":                                    // 3 ADDR
                                                            ADDRESS_STRUCTURE N3_SOUR_CORP_ADDR;
                                                            (N3_SOUR_CORP_ADDR, ligne, ligne_perdue_liste) = Extraire_ADDRESS_STRUCTURE(ligne, ligne_perdue_liste);
                                                            N3_SOUR_CORP_ADDR_liste.Add(N3_SOUR_CORP_ADDR);
                                                            break;
                                                        case "PHON":                                    // 3 PHON
                                                            (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                                                            N3_SOUR_CORP_PHON_liste.Add(temp);
                                                            break;
                                                        case "FAX":                                     // 3 FAX
                                                            (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                                                            N3_SOUR_CORP_FAX_liste.Add(temp);
                                                            break;
                                                        case "EMAIL":                                   // 3 EMAIL
                                                            (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                                                            N3_SOUR_CORP_EMAIL_liste.Add(temp);
                                                            break;
                                                        case "_EMAIL":                                  // 3 _EMAIL
                                                            (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                                                            N3_SOUR_CORP_EMAIL_liste.Add(temp);
                                                            break;
                                                        case "WWW":                                   // 3 WWW
                                                            (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                                                            N3_SOUR_CORP_WWW_liste.Add(temp);
                                                            break;
                                                        case "_WWW":                                   // 3 _WWW
                                                            (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                                                            N3_SOUR_CORP_WWW_liste.Add(temp);
                                                            break;
                                                        default:
                                                            // pour la page HTML
                                                            Ligne_perdue aa = new Ligne_perdue()
                                                            {
                                                                ligne = ligne,
                                                                texte = dataGEDCOM[ligne]
                                                            };
                                                            ligne_perdue_liste.Add(aa);
                                                            // rapport Balise
                                                            method = MethodBase.GetCurrentMethod().Name;
                                                            lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                                            Ligne_perdue a0 = new Ligne_perdue();
                                                            {
                                                                a0.ligne = balise_0_ligne;
                                                                a0.texte = dataGEDCOM[balise_0_ligne];
                                                            }
                                                            groupe_ligne.Add(a0);
                                                            Ligne_perdue a1 = new Ligne_perdue();
                                                            {
                                                                a1.ligne = balise_1_ligne;
                                                                a1.texte = dataGEDCOM[balise_1_ligne];
                                                            }
                                                            groupe_ligne.Add(a1);
                                                            Ligne_perdue a2 = new Ligne_perdue();
                                                            {
                                                                a2.ligne = balise_2_ligne;
                                                                a2.texte = dataGEDCOM[balise_2_ligne];
                                                            }
                                                            groupe_ligne.Add(a2);
                                                            Ligne_perdue a3 = new Ligne_perdue();
                                                            {
                                                                a3.ligne = balise_3_ligne;
                                                                a3.texte = dataGEDCOM[balise_3_ligne];
                                                            }
                                                            groupe_ligne.Add(a3);
                                                            ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                                            groupe_ligne.Clear();

                                                            ligne++;
                                                            break;
                                                    }
                                                }
                                            }
                                            break;
                                        case "DATA":                                                    // 2 DATA
                                            {
                                                (info_HEADER.N2_SOUR_DATA, ligne) = Extraire_texte_niveau_plus(ligne);
                                                while (Extraire_niveau(ligne) > 2)
                                                {
                                                    string balise_3 = Avoir_balise(dataGEDCOM[ligne]);
                                                    int balise_3_ligne = ligne;
                                                    switch (balise_3)
                                                    {
                                                        case "DATE":                                    // 3 DATA DATE
                                                            (info_HEADER.N3_SOUR_DATA_DATE, ligne) = Extraire_texte_niveau_plus(ligne);
                                                            break;
                                                        case "CORP":                                    // 3 DATA COPR
                                                            (info_HEADER.N3_SOUR_DATA_CORP, ligne) = Extraire_texte_niveau_plus(ligne);
                                                            break;
                                                        default:

                                                            // pour la page HTML
                                                            Ligne_perdue cc = new Ligne_perdue()
                                                            {
                                                                ligne = ligne,
                                                                texte = dataGEDCOM[ligne]
                                                            };
                                                            ligne_perdue_liste.Add(cc);
                                                            // rapport Balise
                                                            method = MethodBase.GetCurrentMethod().Name;
                                                            lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                                            Ligne_perdue c0 = new Ligne_perdue();
                                                            {
                                                                c0.ligne = balise_0_ligne;
                                                                c0.texte = dataGEDCOM[balise_0_ligne];
                                                            }
                                                            groupe_ligne.Add(c0);
                                                            Ligne_perdue c1 = new Ligne_perdue();
                                                            {
                                                                c1.ligne = balise_1_ligne;
                                                                c1.texte = dataGEDCOM[balise_1_ligne];
                                                            }
                                                            groupe_ligne.Add(c1);
                                                            Ligne_perdue c2 = new Ligne_perdue();
                                                            {
                                                                c2.ligne = balise_2_ligne;
                                                                c2.texte = dataGEDCOM[balise_2_ligne];
                                                            }
                                                            groupe_ligne.Add(c2);
                                                            Ligne_perdue c3 = new Ligne_perdue();
                                                            {
                                                                c3.ligne = balise_3_ligne;
                                                                c3.texte = dataGEDCOM[balise_3_ligne];
                                                            }
                                                            groupe_ligne.Add(c3);
                                                            ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                                            groupe_ligne.Clear();

                                                            ligne++;
                                                            break;
                                                    }
                                                }
                                            }
                                            break;
                                        default:

                                            // pour la page HTML
                                            Ligne_perdue dd = new Ligne_perdue()
                                            {
                                                ligne = ligne,
                                                texte = dataGEDCOM[ligne]
                                            };
                                            ligne_perdue_liste.Add(dd);
                                            // rapport Balise
                                            method = MethodBase.GetCurrentMethod().Name;
                                            lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                            Ligne_perdue d0 = new Ligne_perdue();
                                            {
                                                d0.ligne = balise_0_ligne;
                                                d0.texte = dataGEDCOM[balise_0_ligne];
                                            }
                                            groupe_ligne.Add(d0);
                                            Ligne_perdue d1 = new Ligne_perdue();
                                            {
                                                d1.ligne = balise_1_ligne;
                                                d1.texte = dataGEDCOM[balise_1_ligne];
                                            }
                                            groupe_ligne.Add(d1);
                                            Ligne_perdue d2 = new Ligne_perdue();
                                            {
                                                d2.ligne = balise_2_ligne;
                                                d2.texte = dataGEDCOM[balise_2_ligne];
                                            }
                                            groupe_ligne.Add(d2);
                                            ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                            groupe_ligne.Clear();

                                            ligne++;
                                            break;
                                    }
                                }
                            }
                            break;
                        case "DATE":                                                                    // 1 DATE
                            {
                                (info_HEADER.N1_DATE, ligne) = Extraire_texte_niveau_plus(ligne);
                                while (Extraire_niveau(ligne) > 1)
                                {
                                    string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                                    int balise_2_ligne = ligne;
                                    switch (balise_2)
                                    {
                                        case "TIME":                                                    // 2 TIME
                                            (info_HEADER.N2_DATE_TIME, ligne) = Extraire_texte_niveau_plus(ligne);
                                            break;
                                        default:
                                            // pour la page HTML
                                            Ligne_perdue ee = new Ligne_perdue()
                                            {
                                                ligne = ligne,
                                                texte = dataGEDCOM[ligne]
                                            };
                                            ligne_perdue_liste.Add(ee);
                                            // rapport Balise
                                            method = MethodBase.GetCurrentMethod().Name;
                                            lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                            Ligne_perdue e0 = new Ligne_perdue();
                                            {
                                                e0.ligne = balise_0_ligne;
                                                e0.texte = dataGEDCOM[balise_0_ligne];
                                            }
                                            groupe_ligne.Add(e0);
                                            Ligne_perdue e1 = new Ligne_perdue();
                                            {
                                                e1.ligne = balise_1_ligne;
                                                e1.texte = dataGEDCOM[balise_1_ligne];
                                            }
                                            groupe_ligne.Add(e1);
                                            Ligne_perdue e2 = new Ligne_perdue();
                                            {
                                                e2.ligne = balise_2_ligne;
                                                e2.texte = dataGEDCOM[balise_2_ligne];
                                            }
                                            groupe_ligne.Add(e2);
                                            ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                            groupe_ligne.Clear();
                                            ligne++;
                                            break;
                                    }
                                }
                            }
                            break;
                        case "SUBM":                                                                    // 1 SUBM
                            {
                                N1_SUBM_liste_ID.Add(Extraire_ID(dataGEDCOM[ligne]));
                                ligne++;
                            }
                            break;
                        case "FILE":                                                                    // 1 FILE
                            (info_HEADER.N1_FILE, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "SUBN":                                                                    // 1 SUBN
                            (info_HEADER.N1_SUBN, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "_HME":                                                                    // 1 _HME
                            {
                                ligne++;
                            }
                            break;
                        case "GEDC":                                                                    // 1 GEDC
                            {
                                ligne++;
                                while (Extraire_niveau(ligne) > 1)
                                {
                                    string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                                    int balise_2_ligne = ligne;
                                    switch (balise_2)
                                    {
                                        case "VERS":                                                    // 2 GEDC VERS
                                            {
                                                (info_HEADER.N2_GEDC_VERS, ligne) = Extraire_texte_niveau_plus(ligne);
                                            }
                                            break;
                                        case "FORM":                                                    // 2 GEDC FORM
                                            {
                                                (info_HEADER.N2_GEDC_FORM, ligne) = Extraire_texte_niveau_plus(ligne);
                                                while (Extraire_niveau(ligne) > 2)
                                                {
                                                    string balise_3 = Avoir_balise(dataGEDCOM[ligne]);
                                                    int balise_3_ligne = ligne;
                                                    switch (balise_3)
                                                    {
                                                        case "VERS":                                    // 3 GEDC FORM VERS
                                                            (info_HEADER.N3_GEDC_FORM_VERS, ligne) = Extraire_texte_niveau_plus(ligne);
                                                            break;
                                                        default:
                                                            // pour la page HTML
                                                            Ligne_perdue ff = new Ligne_perdue()
                                                            {
                                                                ligne = ligne,
                                                                texte = dataGEDCOM[ligne]
                                                            };
                                                            ligne_perdue_liste.Add(ff);
                                                            // rapport Balise
                                                            method = MethodBase.GetCurrentMethod().Name;
                                                            lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                                            Ligne_perdue f0 = new Ligne_perdue();
                                                            {
                                                                f0.ligne = balise_0_ligne;
                                                                f0.texte = dataGEDCOM[balise_0_ligne];
                                                            }
                                                            groupe_ligne.Add(f0);
                                                            Ligne_perdue f1 = new Ligne_perdue();
                                                            {
                                                                f1.ligne = balise_1_ligne;
                                                                f1.texte = dataGEDCOM[balise_1_ligne];
                                                            }
                                                            groupe_ligne.Add(f1);
                                                            Ligne_perdue f2 = new Ligne_perdue();
                                                            {
                                                                f2.ligne = balise_2_ligne;
                                                                f2.texte = dataGEDCOM[balise_2_ligne];
                                                            }
                                                            groupe_ligne.Add(f2);
                                                            Ligne_perdue f3 = new Ligne_perdue();
                                                            {
                                                                f3.ligne = balise_3_ligne;
                                                                f3.texte = dataGEDCOM[balise_3_ligne];
                                                            }
                                                            groupe_ligne.Add(f3);
                                                            ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                                            groupe_ligne.Clear();
                                                            ligne++;
                                                            break;
                                                    }
                                                }
                                            }
                                            break;
                                        default:
                                            // pour la page HTML
                                            Ligne_perdue gg = new Ligne_perdue()
                                            {
                                                ligne = ligne,
                                                texte = dataGEDCOM[ligne]
                                            };
                                            ligne_perdue_liste.Add(gg);
                                            // rapport Balise
                                            method = MethodBase.GetCurrentMethod().Name;
                                            lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                            Ligne_perdue g0 = new Ligne_perdue();
                                            {
                                                g0.ligne = balise_0_ligne;
                                                g0.texte = dataGEDCOM[balise_0_ligne];
                                            }
                                            groupe_ligne.Add(g0);
                                            Ligne_perdue g1 = new Ligne_perdue();
                                            {
                                                g1.ligne = balise_1_ligne;
                                                g1.texte = dataGEDCOM[balise_1_ligne];
                                            }
                                            groupe_ligne.Add(g1);
                                            Ligne_perdue g2 = new Ligne_perdue();
                                            {
                                                g2.ligne = balise_2_ligne;
                                                g2.texte = dataGEDCOM[balise_2_ligne];
                                            }
                                            groupe_ligne.Add(g2);
                                            ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                            groupe_ligne.Clear();
                                            ligne++;
                                            break;
                                    }
                                }
                            }
                            break;
                        case "CHAR":                                                                    // 1 CHAR
                            {
                                (info_HEADER.N1_CHAR, ligne) = Extraire_texte_niveau_plus(ligne);
                                while (Extraire_niveau(ligne) > 1)
                                {
                                    string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                                    int balise_2_ligne = ligne;
                                    switch (balise_2)
                                    {
                                        case "VERS":                                                    // 2 VERS
                                            (info_HEADER.N2_CHAR_VERS, ligne) = Extraire_texte_niveau_plus(ligne);
                                            break;
                                        default:
                                            // pour la page HTML
                                            Ligne_perdue hh = new Ligne_perdue()
                                            {
                                                ligne = ligne,
                                                texte = dataGEDCOM[ligne]
                                            };
                                            ligne_perdue_liste.Add(hh);
                                            // rapport Balise
                                            method = MethodBase.GetCurrentMethod().Name;
                                            lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                            Ligne_perdue h0 = new Ligne_perdue();
                                            {
                                                h0.ligne = balise_0_ligne;
                                                h0.texte = dataGEDCOM[balise_0_ligne];
                                            }
                                            groupe_ligne.Add(h0);
                                            Ligne_perdue h1 = new Ligne_perdue();
                                            {
                                                h1.ligne = balise_1_ligne;
                                                h1.texte = dataGEDCOM[balise_1_ligne];
                                            }
                                            groupe_ligne.Add(h1);
                                            Ligne_perdue h2 = new Ligne_perdue();
                                            {
                                                h2.ligne = balise_2_ligne;
                                                h2.texte = dataGEDCOM[balise_2_ligne];
                                            }
                                            groupe_ligne.Add(h2);
                                            ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                            groupe_ligne.Clear();
                                            ligne++;
                                            break;
                                    }
                                }
                            }
                            break;
                        case "PLAC":                                                                    // 1 PLAC
                            {
                                ligne++;
                                while (Extraire_niveau(ligne) > 1)
                                {
                                    string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                                    int balise_2_ligne = ligne;
                                    switch (balise_2)
                                    {
                                        case "FORM":                                                    // 2 FORM
                                            (info_HEADER.N2_PLAC_FORM, ligne) = Extraire_texte_niveau_plus(ligne);
                                            break;
                                        default:
                                            // pour la page HTML
                                            Ligne_perdue ii = new Ligne_perdue()
                                            {
                                                ligne = ligne,
                                                texte = dataGEDCOM[ligne]
                                            };
                                            ligne_perdue_liste.Add(ii);
                                            // rapport Balise
                                            method = MethodBase.GetCurrentMethod().Name;
                                            lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                            Ligne_perdue i0 = new Ligne_perdue();
                                            {
                                                i0.ligne = balise_0_ligne;
                                                i0.texte = dataGEDCOM[balise_0_ligne];
                                            }
                                            groupe_ligne.Add(i0);
                                            Ligne_perdue i1 = new Ligne_perdue();
                                            {
                                                i1.ligne = balise_1_ligne;
                                                i1.texte = dataGEDCOM[balise_1_ligne];
                                            }
                                            groupe_ligne.Add(i1);
                                            Ligne_perdue i2 = new Ligne_perdue();
                                            {
                                                i2.ligne = balise_2_ligne;
                                                i2.texte = dataGEDCOM[balise_2_ligne];
                                            }
                                            groupe_ligne.Add(i2);
                                            ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                            groupe_ligne.Clear();
                                            ligne++;
                                            break;
                                    }
                                }
                            }
                            break;
                        case "LANG":                                                                    // 1 LANG
                            (info_HEADER.N1_LANG, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "COPR":                                                                    // 1 COPR
                            (info_HEADER.N1_COPR, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "SCHEMA":                                                                  // 1 SHEMA
                            {
                                ligne++;
                                while (Extraire_niveau(ligne) > 1)
                                {
                                    // Un mécanisme pour créer des balises définies par l'utilisateur. Celles-ci 
                                    // sont définies dans une définition SCHEMA dans HEADER.
                                    // Elle sont ignorer par GH.
                                    ligne++;
                                }
                            }
                            break;
                        case "NOTE":                                                                    // 1 NOTE
                            {
                                string note_ID;
                                (note_ID, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                                N1_NOTE_STRUCTURE_liste_ID.Add(note_ID);
                            }
                            break;
                        case "_GUID":                                                                   // 1 _GUID
                            (info_HEADER.N1__GUID, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        default:
                            // pour la page HTML
                            Ligne_perdue kk = new Ligne_perdue();
                            {
                                kk.ligne = ligne;
                                kk.texte = dataGEDCOM[ligne];
                            }
                            ligne_perdue_liste.Add(kk);
                            // rapport Balise
                            method = MethodBase.GetCurrentMethod().Name;
                            lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                            Ligne_perdue k0 = new Ligne_perdue();
                            {
                                k0.ligne = balise_0_ligne;
                                k0.texte = dataGEDCOM[balise_0_ligne];
                            }
                            groupe_ligne.Add(k0);
                            Ligne_perdue k1 = new Ligne_perdue();
                            {
                                k1.ligne = balise_1_ligne;
                                k1.texte = dataGEDCOM[balise_1_ligne];
                            }
                            groupe_ligne.Add(k1);
                            ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                            groupe_ligne.Clear();
                            break;
                    }
                }
            }
            catch (Exception msg)
            {
                GC.Collect();
                string message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                Voir_message(message, msg.Message, GH.GH.erreur);
            }
            if (!GH.GH.annuler)
            {
                info_HEADER.N3_SOUR_CORP_ADDR_liste = N3_SOUR_CORP_ADDR_liste;
                info_HEADER.N3_SOUR_CORP_PHON_liste = N3_SOUR_CORP_PHON_liste;
                info_HEADER.N3_SOUR_CORP_FAX_liste = N3_SOUR_CORP_FAX_liste;
                info_HEADER.N3_SOUR_CORP_EMAIL_liste = N3_SOUR_CORP_EMAIL_liste;
                info_HEADER.N3_SOUR_CORP_WWW_liste = N3_SOUR_CORP_WWW_liste;
                info_HEADER.N1_NOTE_STRUCTURE_liste_ID = N1_NOTE_STRUCTURE_liste_ID;
                info_HEADER.N1_SUBM_liste_ID = N1_SUBM_liste_ID;
                info_HEADER.ligne_perdue_liste = ligne_perdue_liste;
            }
            return ligne;
        }
        public static void Erreur_log(
            string message = "")
        {
            Regler_code_erreur();
            System.Windows.Forms.Button Btn_erreur = Application.OpenForms["GH"].Controls["Btn_erreur"] as System.Windows.Forms.Button;
            try
            {
                if (!File.Exists(GH.GH.fichier_erreur))
                {
                    using (StreamWriter ligne = File.AppendText(GH.GH.fichier_erreur))
                    {
                        ligne.WriteLine(
                            "<!DOCTYPE html>\n" +
                            "    <html lang=\"fr\" style=\"background-color:#FFF;\">\n" +
                            "        <head>\n" +
                            "           <title>Erreur</title>" +
                            "           <style>\n" +
                            "               h1{color:#00F}\n" +
                            "               .col0{width:150px;vertical-align:top;}\n " +
                            "               .col1{width:90px;vertical-align:top;}\n " +
                            "               .col2{width:50px;vertical-align:top;}\n " +
                            "               .col3{width:330px;vertical-align:top;}\n " +
                            "               .col4{vertical-align:top;}\n " +
                            "               .navbar {\n" +
                            "                   overflow: hidden;\n" +
                            "                   position: fixed;\n" +
                            "                   top: 0;\n" +
                            "                   background-color: #FFF;\n" +
                            "                   width: 100%;\n" +
                            "               }\n" +
                            "           </style>\n" +
                            "        </head>");
                        Btn_erreur.Visible = true;
                        ligne.WriteLine("<div class=\"navbar\">");
                        ligne.WriteLine("<h1>Erreur</h1>");
                        ligne.WriteLine("<table style=\"border:2px solid #000;width:100%\">");
                        ligne.WriteLine("\t<tr><td style=\"width:200px\">Nom</td><td>" + info_HEADER.N2_SOUR_NAME + "</td><td></tr>");
                        ligne.WriteLine("\t<tr><td>Version</td><td>" + info_HEADER.N2_SOUR_VERS + "<td></tr>");
                        ligne.WriteLine("\t<tr><td>Date</td><td>" + info_HEADER.N1_DATE + " " + info_HEADER.N2_DATE_TIME + "<td></tr>");
                        ligne.WriteLine("\t<tr><td>Copyright</td><td>" + info_HEADER.N1_COPR + "<td></tr>");
                        ligne.WriteLine("\t<tr><td>Version</td><td>" + info_HEADER.N2_GEDC_VERS + "<td></tr>");
                        ligne.WriteLine("\t<tr><td>Code charactère</td><td>" + info_HEADER.N1_CHAR + "<td></tr>");
                        ligne.WriteLine("\t<tr><td>Langue</td><td>" + info_HEADER.N1_LANG + "<td></tr>");
                        ligne.WriteLine("\t<tr><td>Fichier sur le disque</td><td>" + info_HEADER.Nom_fichier_disque + "<td></tr>");
                        System.Version version;
                        try
                        {
                            version = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                        }
                        catch
                        {
                            GC.Collect();
                            version = Assembly.GetExecutingAssembly().GetName().Version;
                        }
                        ligne.WriteLine("\t<tr><td>Version de GH</td><td>" + version.Major + "." + version.Minor + "." + version.Build + "<td></tr>");
                        ligne.WriteLine("</table>");
                        ligne.WriteLine(
                            "<table>" +
                            "<tr>" +
                            "<td class=\"col0\">" +
                            "Date heure" +
                            "</td>" +
                            "<td>" +
                            "Message" +
                            "</td>" +
                            "</table>" +
                            "<hr>");
                        ligne.WriteLine("</div>");
                        ligne.WriteLine("<div style=\"margin-top: 325px;\">\n");
                        ligne.WriteLine("</div>\n");

                    }
                }
                using (StreamWriter ligne = File.AppendText(GH.GH.fichier_erreur))
                {

                    string s = String.Format(
                        "<table>" +
                        "<tr>" +
                        "<td class=\"col0\">" +
                        "{0}" +
                        "</td>" +
                        "<td>" +
                        "{1}" +
                        "</td>" +
                        "</tr>" +
                        "</table>" +
                        "<hr>",
                        DateTime.Now, message);
                    ligne.WriteLine(s);
                }
            }
            catch (Exception msg)
            {
                GC.Collect();
                MessageBox.Show("Ne peut pas écrire l'information sur le disque." + msg.Message, "Problème",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private static (ADDRESS_STRUCTURE, int, List<Ligne_perdue>) Extraire_ADDRESS_STRUCTURE(
            int ligne,
            List<Ligne_perdue> ligne_perdue_liste
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b> GEDCOM=" + ligne);

            Regler_code_erreur();
            ADDRESS_STRUCTURE info = new ADDRESS_STRUCTURE
            {
                N0_ADDR = null,
                N1_ADR1 = null,
                N1_ADR2 = null,
                N1_ADR3 = null,
                N1_CITY = null,
                N1_STAE = null,
                N1_POST = null,
                N1_CTRY = null
            };
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            List<string> N1_PHON_liste = new List<string>(); // V5.3
            List<string> N1_NOTE_STRUCTURE_liste_ID = new List<string>();
            int niveau = Extraire_niveau(ligne);
            string balise_0 = Avoir_balise(dataGEDCOM[ligne]);
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            string temp;
            switch (balise_0)
            {
                case "ADDR":                                                                            // ADDR
                    (info.N0_ADDR, ligne) = Extraire_texte_niveau_plus(ligne);
                    while (Extraire_niveau(ligne) > niveau)
                    {
                        string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                        int balise_1_ligne = ligne;
                        switch (balise_1)
                        {
                            case "ADR1":                                                                // +1 ADR1
                                (info.N1_ADR1, ligne) = Extraire_texte_niveau_plus(ligne);
                                break;
                            case "ADR2":                                                                // +1 ADR2
                                (info.N1_ADR2, ligne) = Extraire_texte_niveau_plus(ligne);
                                break;
                            case "ADR3":                                                                // +1 ADR3
                                (info.N1_ADR3, ligne) = Extraire_texte_niveau_plus(ligne);
                                break;
                            case "CITY":                                                                // +1 CITY
                                (info.N1_CITY, ligne) = Extraire_texte_niveau_plus(ligne);
                                break;
                            case "STAE":                                                                // +1 STAE
                                (info.N1_STAE, ligne) = Extraire_texte_niveau_plus(ligne);
                                break;
                            case "CTRY":                                                                // +1 CTRY
                                (info.N1_CTRY, ligne) = Extraire_texte_niveau_plus(ligne);
                                break;
                            case "POST":                                                                // +1 POST
                                (info.N1_POST, ligne) = Extraire_texte_niveau_plus(ligne);
                                break;
                            case "PHON":                                                                // +1 PHON V5.3
                                (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                                N1_PHON_liste.Add(temp);
                                break;
                            case "NOTE":                                                                // +1 NOTE pour GRAMPS
                                string NoteID;
                                (NoteID, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                                N1_NOTE_STRUCTURE_liste_ID.Add(NoteID);
                                break;
                            default:
                                // pour la page HTML
                                Ligne_perdue aa = new Ligne_perdue()
                                {
                                    ligne = ligne,
                                    texte = dataGEDCOM[ligne]
                                };
                                ligne_perdue_liste.Add(aa);
                                // rapport Balise
                                method = MethodBase.GetCurrentMethod().Name;
                                lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                Ligne_perdue a0 = new Ligne_perdue()
                                {
                                    ligne = balise_0_ligne,
                                    texte = dataGEDCOM[balise_0_ligne]
                                };
                                groupe_ligne.Add(a0);
                                Ligne_perdue a1 = new Ligne_perdue()
                                {
                                    ligne = balise_1_ligne,
                                    texte = dataGEDCOM[balise_1_ligne]
                                };
                                groupe_ligne.Add(a1);
                                ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                groupe_ligne.Clear();
                                break;
                        }
                    }
                    break;
                default:
                    // pour la page HTML
                    Ligne_perdue cc = new Ligne_perdue()
                    {
                        ligne = ligne,
                        texte = dataGEDCOM[ligne]
                    };
                    ligne_perdue_liste.Add(cc);
                    // rapport Balise
                    method = MethodBase.GetCurrentMethod().Name;
                    lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                    Ligne_perdue c0 = new Ligne_perdue()
                    {
                        ligne = balise_0_ligne,
                        texte = dataGEDCOM[balise_0_ligne]
                    };
                    groupe_ligne.Add(c0);
                    ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                    groupe_ligne.Clear();
                    break;
            }
            info.N1_PHON_liste = N1_PHON_liste;
            info.N1_NOTE_STRUCTURE_liste_ID = N1_NOTE_STRUCTURE_liste_ID;
            return (info, ligne, ligne_perdue_liste);
        }

        private static string Extraire_MULTIMEDIA_LINK(
            string fichier
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b> GEDCOM=" + ligne);

            MULTIMEDIA_LINK info_MULTIMEDIA_LINK = new MULTIMEDIA_LINK();
            conteur_MULTIMEDIA_LINK++;
            info_MULTIMEDIA_LINK.ID_LINK = "A-" + conteur_MULTIMEDIA_LINK.ToString();
            info_MULTIMEDIA_LINK.FILE = fichier;
            if (!GH.GH.annuler)
            {
                liste_MULTIMEDIA_LINK.Add(info_MULTIMEDIA_LINK);
            }
            return info_MULTIMEDIA_LINK.ID_LINK;
        }


        private static (ASSOCIATION_STRUCTURE, int, List<Ligne_perdue>) Extraire_ASSOCIATION_STRUCTURE(
            int ligne,
            List<Ligne_perdue> ligne_perdue_liste
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b> GEDCOM=" + ligne);
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            ASSOCIATION_STRUCTURE info = new ASSOCIATION_STRUCTURE();
            string N0_ASSO;
            string N1_TYPE = null;
            string N1_RELA = null;
            List<string> N1_SOUR_citation_liste_ID = new List<string>();
            List<string> N1_SOUR_source_liste_ID = new List<string>();
            List<string> N1_NOTE_STRUCTURE_liste_ID = new List<string>();
            string method;
            int lineNumber;
            int niveau = Extraire_niveau(ligne);
            N0_ASSO = Extraire_ID(dataGEDCOM[ligne]);                                                   // +0 ASSO
            //string balise_0 = "ASSO";
            int balise_0_ligne = ligne;
            ligne++;
            while (Extraire_niveau(ligne) > niveau)
            {
                string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                int balise_1_ligne = ligne;
                switch (balise_1)
                {
                    case "RELA":                                                                        // +1 RELA
                        (N1_RELA, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "TYPE":                                                                        // +1 TYPE 
                        (N1_TYPE, ligne) = Extraire_texte_niveau_plus(ligne);
                        switch (N1_TYPE.ToUpper())
                        {
                            case "FAM": N1_TYPE = "Famille"; break;
                            case "INDI": N1_TYPE = "Individu"; break;
                        }
                        break;
                    case "SOUR":                                                                        // +1 SOUR
                        string citation;
                        string source;
                        (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                        if (IsNotNullOrEmpty(citation)) N1_SOUR_citation_liste_ID.Add(citation);
                        if (IsNotNullOrEmpty(source) && IsNullOrEmpty(citation)) N1_SOUR_source_liste_ID.Add(source);
                        break;
                    case "NOTE":                                                                        // +1 NOTE
                        string NoteID;
                        (NoteID, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                        N1_NOTE_STRUCTURE_liste_ID.Add(NoteID);
                        break;
                    default:
                        // pour la page HTML
                        // pour la page HTML
                        Ligne_perdue aa = new Ligne_perdue()
                        {
                            ligne = ligne,
                            texte = dataGEDCOM[ligne]
                        };
                        ligne_perdue_liste.Add(aa);
                        // rapport Balise
                        method = MethodBase.GetCurrentMethod().Name;
                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                        Ligne_perdue a0 = new Ligne_perdue()
                        {
                            ligne = balise_0_ligne,
                            texte = dataGEDCOM[balise_0_ligne]
                        };
                        groupe_ligne.Add(a0);
                        Ligne_perdue a1 = new Ligne_perdue()
                        {
                            ligne = balise_1_ligne,
                            texte = dataGEDCOM[balise_1_ligne]
                        };
                        groupe_ligne.Add(a1);
                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                        groupe_ligne.Clear();
                        break;
                }
            }
            info.N0_ASSO = N0_ASSO;
            info.N1_TYPE = N1_TYPE;
            info.N1_RELA = N1_RELA;
            info.N1_SOUR_citation_liste_ID = N1_SOUR_citation_liste_ID;
            info.N1_SOUR_source_liste_ID = N1_SOUR_source_liste_ID;
            info.N1_NOTE_STRUCTURE_liste_ID = N1_NOTE_STRUCTURE_liste_ID;
            return (info, ligne, ligne_perdue_liste);
        }

        private static (CHANGE_DATE, int, List<Ligne_perdue>)
            Extraire_CHANGE_DATE(int ligne, List<Ligne_perdue> ligne_perdue_liste
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b>");

            Regler_code_erreur();
            CHANGE_DATE info = new CHANGE_DATE();
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            string N1_CHAN_DATE = null;
            string N2_CHAN_DATE_TIME = null;
            List<string> N1_NOTE_STRUCTURE_liste_ID = new List<string>();
            int niveau = Extraire_niveau(ligne);
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            ligne++;
            while (Extraire_niveau(ligne) > niveau)
            {
                string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                int balise_1_ligne = ligne;
                switch (balise_1)
                {
                    case "DATE":                                                                       // +1 DATE
                        (N1_CHAN_DATE, ligne) = Extraire_texte_niveau_plus(ligne);
                        while (Extraire_niveau(ligne) > niveau + 1) // > +1
                        {
                            string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                            int balise_2_ligne = ligne;
                            switch (balise_2)
                            {
                                case "TIME":                                                            // +2 TIME
                                    (N2_CHAN_DATE_TIME, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                default:
                                    // pour la page HTML
                                    Ligne_perdue aa = new Ligne_perdue()
                                    {
                                        ligne = ligne,
                                        texte = dataGEDCOM[ligne]
                                    };
                                    ligne_perdue_liste.Add(aa);
                                    // rapport Balise
                                    method = MethodBase.GetCurrentMethod().Name;
                                    lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                    Ligne_perdue a0 = new Ligne_perdue()
                                    {
                                        ligne = balise_0_ligne,
                                        texte = dataGEDCOM[balise_0_ligne]
                                    };
                                    groupe_ligne.Add(a0);
                                    Ligne_perdue a1 = new Ligne_perdue()
                                    {
                                        ligne = balise_1_ligne,
                                        texte = dataGEDCOM[balise_1_ligne]
                                    };
                                    groupe_ligne.Add(a1);
                                    Ligne_perdue a2 = new Ligne_perdue()
                                    {
                                        ligne = balise_2_ligne,
                                        texte = dataGEDCOM[balise_2_ligne]
                                    };
                                    groupe_ligne.Add(a2);
                                    ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                    groupe_ligne.Clear();
                                    break;
                            }
                        }
                        break;
                    case "NOTE":                                                                        // +1 NOTE
                        string IDNote;
                        (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                        N1_NOTE_STRUCTURE_liste_ID.Add(IDNote);
                        break;
                    default:
                        // pour la page HTML
                        Ligne_perdue bb = new Ligne_perdue()
                        {
                            ligne = ligne,
                            texte = dataGEDCOM[ligne]
                        };
                        ligne_perdue_liste.Add(bb);
                        // rapport Balise
                        method = MethodBase.GetCurrentMethod().Name;
                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                        Ligne_perdue b0 = new Ligne_perdue()
                        {
                            ligne = balise_0_ligne,
                            texte = dataGEDCOM[balise_0_ligne]
                        };
                        groupe_ligne.Add(b0);
                        Ligne_perdue b1 = new Ligne_perdue()
                        {
                            ligne = balise_1_ligne,
                            texte = dataGEDCOM[balise_1_ligne]
                        };
                        groupe_ligne.Add(b1);
                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                        groupe_ligne.Clear();
                        break;
                }
            }
            info.N1_CHAN_DATE = N1_CHAN_DATE;
            info.N2_CHAN_DATE_TIME = N2_CHAN_DATE_TIME;
            info.N1_CHAN_NOTE_STRUCTURE_ID_liste = N1_NOTE_STRUCTURE_liste_ID;
            return (info, ligne, ligne_perdue_liste);
        }

        private static (CHILD_TO_FAMILY_LINK, int, List<Ligne_perdue>) Extraire_CHILD_TO_FAMILY_LINK(
            int ligne,
            List<Ligne_perdue> ligne_perdue_liste)
        {
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            //string balise_0 = Avoir_balise(dataGEDCOM[ligne]);
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            int niveau = Extraire_niveau(ligne);
            List<string> N1_NOTE_STRUCTURE_liste_ID = new List<string>();
            CHILD_TO_FAMILY_LINK info = new CHILD_TO_FAMILY_LINK
            {
                N0_FAMC = Extraire_ID(dataGEDCOM[ligne]),
                N1_adop = false,
                N1_slgc = false
            };
            List<string> N2_ADOP_SOUR_citation_liste_ID = new List<string>();
            List<string> N2_ADOP_SOUR_source_liste_ID = new List<string>();
            List<string> N2_ADOP_NOTE_liste_ID = new List<string>();
            ligne++;
            while (Extraire_niveau(ligne) > niveau)
            {
                string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                int balise_1_ligne = ligne;
                //string baliseN1 = Avoir_balise_p1(dataGEDCOM[ligne]);
                switch (balise_1)
                {
                    case "ADOP":                                                                        // ADOP
                        info.N1_adop = true;
                        ligne++;
                        while (Extraire_niveau(ligne) > niveau + 1)
                        {
                            string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                            int balise_2_ligne = ligne;
                            switch (balise_2)
                            {
                                case "TYPE":                                                           // ADOP TYPE
                                    (info.N2_ADOP_TYPE, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                case "AGE":                                                             // ADOP AGE
                                    (info.N2_ADOP_AGE, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                case "DATE":                                                            // ADOP DATE
                                    (info.N2_ADOP_DATE, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                case "PLAC":                                                            // ADOP PLAC
                                    (info.N2_ADOP_PLAC, ligne, ligne_perdue_liste) = Extraire_PLACE_STRUCTURE(ligne, ligne_perdue_liste);
                                    break;
                                case "SOUR":                                                            // ADOP SOUR
                                    string citation;
                                    string source;
                                    (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                                    if (IsNotNullOrEmpty(citation))
                                    {
                                        N2_ADOP_SOUR_citation_liste_ID.Add(citation);
                                    }
                                    if (IsNotNullOrEmpty(source) && IsNullOrEmpty(citation))
                                    {
                                        N2_ADOP_SOUR_source_liste_ID.Add(source);
                                    }
                                    break;
                                case "NOTE":                                                            // ADOP MOTE
                                    string ID_Note;
                                    (ID_Note, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                                    N2_ADOP_NOTE_liste_ID.Add(ID_Note);
                                    break;
                                default:
                                    // pour la page HTML
                                    Ligne_perdue aa = new Ligne_perdue()
                                    {
                                        ligne = ligne,
                                        texte = dataGEDCOM[ligne]
                                    };
                                    ligne_perdue_liste.Add(aa);
                                    // rapport Balise
                                    //method = MethodBase.GetCurrentMethod().Name;
                                    //lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                    Ligne_perdue a0 = new Ligne_perdue
                                    {
                                        ligne = balise_0_ligne,
                                        texte = dataGEDCOM[balise_0_ligne]
                                    };
                                    groupe_ligne.Add(a0);
                                    Ligne_perdue a1 = new Ligne_perdue();
                                    {
                                        a1.ligne = balise_1_ligne;
                                        a1.texte = dataGEDCOM[balise_1_ligne];
                                    }
                                    groupe_ligne.Add(a1);
                                    Ligne_perdue a2 = new Ligne_perdue();
                                    {
                                        a2.ligne = balise_2_ligne;
                                        a2.texte = dataGEDCOM[balise_2_ligne];
                                    }
                                    groupe_ligne.Add(a2);
                                    break;
                            }
                        }
                        break;
                    case "SLGC":                                                                        // SLGC
                        ligne++;
                        info.N1_slgc = true;
                        while (Extraire_niveau(ligne) > niveau + 1)
                        {
                            string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                            int balise_2_ligne = ligne;
                            switch (balise_2)
                            {
                                case "TYPE":                                                             // SLGC TYPE
                                    (info.N2_SLGC_TYPE, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                case "DATE":                                                            // SLGC DATE
                                    (info.N2_SLGC_DATE, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                case "TEMP": // SLGC TEMP
                                    (info.N2_SLGC_TEMP, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                default:
                                    // pour la page HTML
                                    Ligne_perdue aa = new Ligne_perdue()
                                    {
                                        ligne = ligne,
                                        texte = dataGEDCOM[ligne]
                                    };
                                    ligne_perdue_liste.Add(aa);
                                    // rapport Balise
                                    method = MethodBase.GetCurrentMethod().Name;
                                    lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                    Ligne_perdue a0 = new Ligne_perdue()
                                    {
                                        ligne = balise_0_ligne,
                                        texte = dataGEDCOM[balise_0_ligne]
                                    };
                                    groupe_ligne.Add(a0);
                                    Ligne_perdue a1 = new Ligne_perdue()
                                    {
                                        ligne = balise_1_ligne,
                                        texte = dataGEDCOM[balise_1_ligne]
                                    };
                                    groupe_ligne.Add(a1);
                                    Ligne_perdue a2 = new Ligne_perdue();
                                    {
                                        a2.ligne = balise_2_ligne;
                                        a2.texte = dataGEDCOM[balise_2_ligne];
                                    }
                                    groupe_ligne.Add(a2);
                                    ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                    groupe_ligne.Clear();
                                    break;
                            }
                        }
                        break;
                    case "PEDI":                                                                        // +1 PEDI
                        (info.N1_PEDI, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "STAT":                                                                        // +1 STAT
                        (info.N1_STAT, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "NOTE":                                                                        // +1 NOTE
                        string IDNote;
                        (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                        N1_NOTE_STRUCTURE_liste_ID.Add(IDNote);
                        break;
                    default:
                        // pour la page HTML
                        Ligne_perdue bb = new Ligne_perdue()
                        {
                            ligne = ligne,
                            texte = dataGEDCOM[ligne]
                        };
                        ligne_perdue_liste.Add(bb);
                        // rapport Balise
                        method = MethodBase.GetCurrentMethod().Name;
                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                        Ligne_perdue b0 = new Ligne_perdue()
                        {
                            ligne = balise_0_ligne,
                            texte = dataGEDCOM[balise_0_ligne]
                        };
                        groupe_ligne.Add(b0);
                        Ligne_perdue b1 = new Ligne_perdue();
                        {
                            b1.ligne = balise_1_ligne;
                            b1.texte = dataGEDCOM[balise_1_ligne];
                        }
                        groupe_ligne.Add(b1);
                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                        groupe_ligne.Clear();
                        break;
                }
            }
            info.N2_ADOP_SOUR_citation_liste_ID = N2_ADOP_SOUR_citation_liste_ID;
            info.N2_ADOP_SOUR_source_liste_ID = N2_ADOP_SOUR_source_liste_ID;
            info.N2_ADOP_NOTE_STRUCTURE_liste_ID = N2_ADOP_NOTE_liste_ID;
            info.N1_NOTE_STRUCTURE_liste_ID = N1_NOTE_STRUCTURE_liste_ID;
            return (info, ligne, ligne_perdue_liste);
        }

        private static (EVEN_ATTRIBUTE_STRUCTURE, int) Extraire_EVEN_ATTRIBUTE_STRUCTURE(
            int ligne,
            int niveau
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name +" code " + callerLineNumber + "</b> GEDCOM="+ ligne);
            List<Ligne_perdue> ligne_perdue_liste = new List<Ligne_perdue>();
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            EVEN_ATTRIBUTE_STRUCTURE info = new EVEN_ATTRIBUTE_STRUCTURE
            {
                N2_DATE = "", // important pour classer par date;
                N3_DATE_TIME = null, // Herisis
                N2_TEMP = null,
                N2_PLAC = null,
                N2_SITE = null,
                N2_QUAY = null,
                N2_AGNC = null,
                N2_RELI = null,
                N2_CAUS = null,
                N2_RESN = null,
                N1_EVEN = null,
                N1_EVEN_texte = null,
                N3_HUSB_AGE = null,
                N3_WIFE_AGE = null,
                N2_AGE = null,
                N2_MSTAT = null,
                N2_FAMC = null,
                N2_FAMC_ADOP = null,
                //titre = null,
                description = null,
                N2__ANCES_ORDRE = null, // Ancestrologie
                N2__ANCES_XINSEE = null,  // Ancestrologie
                N2__FNA = null // Heredis
            };
            List<ADDRESS_STRUCTURE> N2_ADDR_liste = new List<ADDRESS_STRUCTURE>();
            List<string> N2_PHON_liste = new List<string>();
            List<string> N2_EMAIL_liste = new List<string>();
            List<string> N2_FAX_liste = new List<string>();
            List<string> N2_WWW_liste = new List<string>();
            List<string> N2_NOTE_STRUCTURE_liste_ID = new List<string>();
            List<string> N2_SOUR_citation_liste_ID = new List<string>();
            List<string> N2_SOUR_source_liste_ID = new List<string>();
            List<string> MULTIMEDIA_LINK_liste_ID = new List<string>();
            List<string> N2_TYPE_liste = new List<string>();
            List<TEXT_STRUCTURE> N2_TEXT_liste = new List<TEXT_STRUCTURE>();
            //CHANGE_DATE N2_CHAN = new CHANGE_DATE();
            CHANGE_DATE N2_CHAN = null;
            (_, _, info.N1_EVEN, info.N1_EVEN_texte, ligne) = Extraire_info_niveau_0(ligne);
            int balise_0_ligne = ligne;
            info.DATE_trier = "99999999";
            string method;
            int lineNumber;
            string temp;
            while (Extraire_niveau(ligne) > niveau)
            {
                string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                int balise_1_ligne = ligne;
                switch (balise_1)
                {
                    case "TYPE":                                                                        // TYPE
                        string type;
                        (type, ligne) = Extraire_texte_niveau_plus(ligne);
                        N2_TYPE_liste.Add(type);
                        break;
                    case "DATE":                                                                        // DATE
                        (info.N2_DATE, ligne) = Extraire_texte_niveau_plus(ligne);
                        info.DATE_trier = Convertir_date_trier(info.N2_DATE);
                        while (Extraire_niveau(ligne) > niveau + 1)
                        {
                            string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                            int balise_2_ligne = ligne;
                            
                            switch (balise_2)
                            {
                                case "TIME":                                                            // 3 TIME Heresis
                                    (info.N3_DATE_TIME, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                default:
                                    // pour la page HTML
                                    Ligne_perdue aa = new Ligne_perdue()
                                    {
                                        ligne = ligne,
                                        texte = dataGEDCOM[ligne]
                                    };
                                    ligne_perdue_liste.Add(aa);
                                    // rapport Balise
                                    method = MethodBase.GetCurrentMethod().Name;
                                    lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                    Ligne_perdue a0 = new Ligne_perdue()
                                    {
                                        ligne = balise_0_ligne,
                                        texte = dataGEDCOM[balise_0_ligne]
                                    };
                                    groupe_ligne.Add(a0);
                                    Ligne_perdue a1 = new Ligne_perdue();
                                    {
                                        a1.ligne = balise_1_ligne;
                                        a1.texte = dataGEDCOM[balise_1_ligne];
                                    }
                                    groupe_ligne.Add(a1);
                                    Ligne_perdue a2 = new Ligne_perdue();
                                    {
                                        a2.ligne = balise_2_ligne;
                                        a2.texte = dataGEDCOM[balise_2_ligne];
                                    }
                                    ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                    groupe_ligne.Clear();
                                    break;
                            }
                        }
                        break;
                    case "TEPM":                                                                        // 2 TEMP html.cs
                        (info.N2_TEMP, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "PLAC":                                                                        // 2 PLAC
                        if (info_HEADER.N2_GEDC_VERS == "5.3")
                        {
                            (info.N2_PLAC, ligne, ligne_perdue_liste) = Extraire_PLACE_STRUCTURE(ligne, ligne_perdue_liste);
                        }
                        else
                        {
                            (info.N2_PLAC, ligne, ligne_perdue_liste) = Extraire_PLACE_STRUCTURE(ligne, ligne_perdue_liste);
                        }
                        break;
                    case "SITE":                                                                        // 2 SITE V5.3
                        (info.N2_SITE, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "ADDR":                                                                        // 2 ADDR
                        ADDRESS_STRUCTURE N2_ADDR;
                        (N2_ADDR, ligne, ligne_perdue_liste) = Extraire_ADDRESS_STRUCTURE(ligne, ligne_perdue_liste);
                        N2_ADDR_liste.Add(N2_ADDR);
                        break;
                    case "PHON":                                                                        // 2 PHON
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        N2_PHON_liste.Add(temp);
                        break;
                    case "FAX":                                                                         // 2 FAX
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        N2_FAX_liste.Add(temp);
                        break;
                    case "EMAIL":                                                                       // 2 EMAIL
                    case "_EMAIL":
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        N2_EMAIL_liste.Add(temp);
                        break;
                    case "WWW":                                                                         // 2 WWW
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        N2_WWW_liste.Add(temp);
                        break;
                    case "QUAY":                                                                        // 2 QUAY
                        (info.N2_QUAY, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "AGNC":                                                                        // 2 AGNC
                        (info.N2_AGNC, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "RELI":                                                                        // 2 RELI
                        (info.N2_RELI, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "CAUS":                                                                        // 2 CAUS
                        (info.N2_CAUS, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "RESN":                                                                        // 2 RESN 
                        (info.N2_RESN, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "NOTE":                                                                        // 2 NOTE
                        string IDNote;
                        (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                        N2_NOTE_STRUCTURE_liste_ID.Add(IDNote);
                        break;
                    case "OBJE":                                                                        // 2 OBJE
                        (temp, ligne, ligne_perdue_liste) = Extraire_MULTIMEDIA_LINK(ligne, ligne_perdue_liste);
                        MULTIMEDIA_LINK_liste_ID.Add(temp);
                        break;
                    case "AUDIO":                                                                       // 2 AUDIO individu V5.3
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        MULTIMEDIA_LINK_liste_ID.Add(temp);
                        break;
                    case "PHOTO":                                                                       // 2 PHOTO individu  V5.3
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        MULTIMEDIA_LINK_liste_ID.Add(temp);
                        break;
                    case "VIDEO":                                                                       // 2 VIDEO individu V5.3
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        MULTIMEDIA_LINK_liste_ID.Add(temp);
                        break;
                    case "TEXT":                                                                        // 2 TEXT
                        TEXT_STRUCTURE temp2;
                        (temp2, ligne, ligne_perdue_liste) = Extraire_TEXT_STRUCTURE(ligne, ligne_perdue_liste);
                        N2_TEXT_liste.Add(temp2);
                        break;
                    case "SOUR":                                                                        // 2 SOUR
                        string citation;
                        string source;
                        (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                        if (IsNotNullOrEmpty(citation)) N2_SOUR_citation_liste_ID.Add(citation);
                        if (IsNotNullOrEmpty(source) && IsNullOrEmpty(citation)) N2_SOUR_source_liste_ID.Add(source);
                        break;
                    case "AGE":                                                                         // 2 AGE
                        (info.N2_AGE, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "MSTAT":                                                                       // 2 MSTAT
                        (info.N2_MSTAT, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "FAMC":                                                                        // 2 FAMC
                        info.N2_FAMC = Extraire_ID(dataGEDCOM[ligne]);
                        ligne++;
                        while (Extraire_niveau(ligne) > niveau + 1)
                        {
                            string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                            int balise_2_ligne = ligne;
                            switch (balise_2)
                            {
                                case "ADOP":                                                            // 3 ADOP
                                    (info.N2_FAMC_ADOP, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                default:
                                    // pour la page HTML
                                    Ligne_perdue bb = new Ligne_perdue()
                                    {
                                        ligne = ligne,
                                        texte = dataGEDCOM[ligne]
                                    };
                                    ligne_perdue_liste.Add(bb);
                                    // rapport Balise
                                    method = MethodBase.GetCurrentMethod().Name;
                                    lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                    Ligne_perdue b0 = new Ligne_perdue()
                                    {
                                        ligne = balise_0_ligne,
                                        texte = dataGEDCOM[balise_0_ligne]
                                    };
                                    groupe_ligne.Add(b0);
                                    Ligne_perdue b1 = new Ligne_perdue();
                                    {
                                        b1.ligne = balise_1_ligne;
                                        b1.texte = dataGEDCOM[balise_1_ligne];
                                    }
                                    groupe_ligne.Add(b1);
                                    Ligne_perdue b2 = new Ligne_perdue();
                                    {
                                        b2.ligne = balise_2_ligne;
                                        b2.texte = dataGEDCOM[balise_2_ligne];
                                    }
                                    groupe_ligne.Add(b2);
                                    ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                    groupe_ligne.Clear();
                                    break;
                            }
                        }
                        break;
                    case "HUSB":                                                                        // 2 HUSB
                        ligne++;
                        while (Extraire_niveau(ligne) > niveau + 1)
                        {
                            string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                            int balise_2_ligne = ligne;
                            switch (balise_2)
                            {
                                case "AGE":                                                             // 3  HUSB AGE
                                    (info.N3_HUSB_AGE, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                default:
                                    // pour la page HTML
                                    Ligne_perdue cc = new Ligne_perdue()
                                    {
                                        ligne = ligne,
                                        texte = dataGEDCOM[ligne]
                                    };
                                    ligne_perdue_liste.Add(cc);
                                    // rapport Balise
                                    method = MethodBase.GetCurrentMethod().Name;
                                    lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                    Ligne_perdue c0 = new Ligne_perdue()
                                    {
                                        ligne = balise_0_ligne,
                                        texte = dataGEDCOM[balise_0_ligne]
                                    };
                                    groupe_ligne.Add(c0);
                                    Ligne_perdue c1 = new Ligne_perdue();
                                    {
                                        c1.ligne = balise_1_ligne;
                                        c1.texte = dataGEDCOM[balise_1_ligne];
                                    }
                                    groupe_ligne.Add(c1);
                                    Ligne_perdue b2 = new Ligne_perdue();
                                    {
                                        b2.ligne = balise_2_ligne;
                                        b2.texte = dataGEDCOM[balise_2_ligne];
                                    }
                                    groupe_ligne.Add(b2);
                                    ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                    groupe_ligne.Clear();
                                    break;
                            }
                        }
                        break;
                    case "WIFE":                                                                        // 2 WIFE
                        ligne++;
                        while (Extraire_niveau(ligne) > niveau + 1)
                        {
                            string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                            int balise_2_ligne = ligne;
                            switch (balise_2)
                            {
                                case "AGE":                                                             // 3 WIFE AGE
                                    (info.N3_WIFE_AGE, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                default:
                                    // pour la page HTML
                                    Ligne_perdue cc = new Ligne_perdue()
                                    {
                                        ligne = ligne,
                                        texte = dataGEDCOM[ligne]
                                    };
                                    ligne_perdue_liste.Add(cc);
                                    // rapport Balise
                                    method = MethodBase.GetCurrentMethod().Name;
                                    lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                    Ligne_perdue c0 = new Ligne_perdue()
                                    {
                                        ligne = balise_0_ligne,
                                        texte = dataGEDCOM[balise_0_ligne]
                                    };
                                    groupe_ligne.Add(c0);
                                    Ligne_perdue c1 = new Ligne_perdue();
                                    {
                                        c1.ligne = balise_1_ligne;
                                        c1.texte = dataGEDCOM[balise_1_ligne];
                                    }
                                    groupe_ligne.Add(c1);
                                    Ligne_perdue c2 = new Ligne_perdue();
                                    {
                                        c2.ligne = balise_2_ligne;
                                        c2.texte = dataGEDCOM[balise_2_ligne];
                                    }
                                    groupe_ligne.Add(c2);
                                    ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                    groupe_ligne.Clear();
                                    break;
                            }
                        }
                        break;
                    case "_ANCES_ORDRE":                                                                // 2 ANCES_ORDRE
                        (info.N2__ANCES_ORDRE, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "CHAN":                                                                        // 2 CHAN
                        (N2_CHAN, ligne, ligne_perdue_liste) = Extraire_CHANGE_DATE(ligne, ligne_perdue_liste);
                        break;
                    case "_ANCES_XINSEE":                                                               // 2 ANCES_XINSEE
                        (info.N2__ANCES_XINSEE, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "_FNA":                                                                        // 1 _FNA Herisis
                        (info.N2__FNA, ligne) = Extraire_texte_niveau_plus(ligne);
                        while (Extraire_niveau(ligne) > niveau + 1)
                        {
                            //string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                            int balise_2_ligne = ligne;
                            // pour la page HTML
                            Ligne_perdue dd = new Ligne_perdue()
                            {
                                ligne = ligne,
                                texte = dataGEDCOM[ligne]
                            };
                            ligne_perdue_liste.Add(dd);
                            // rapport Balise
                            method = MethodBase.GetCurrentMethod().Name;
                            lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                            Ligne_perdue d0 = new Ligne_perdue()
                            {
                                ligne = balise_0_ligne,
                                texte = dataGEDCOM[balise_0_ligne]
                            };
                            groupe_ligne.Add(d0);
                            Ligne_perdue d1 = new Ligne_perdue();
                            {
                                d1.ligne = balise_1_ligne;
                                d1.texte = dataGEDCOM[balise_1_ligne];
                            }
                            groupe_ligne.Add(d1);
                            Ligne_perdue d2 = new Ligne_perdue();
                            {
                                d2.ligne = balise_2_ligne;
                                d2.texte = dataGEDCOM[balise_2_ligne];
                            }
                            groupe_ligne.Add(d2);
                            ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                            groupe_ligne.Clear();
                            break;
                        }
                        break;
                    default:
                        // pour la page HTML
                        Ligne_perdue ee = new Ligne_perdue()
                        {
                            ligne = ligne,
                            texte = dataGEDCOM[ligne]
                        };
                        ligne_perdue_liste.Add(ee);
                        // rapport Balise
                        method = MethodBase.GetCurrentMethod().Name;
                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                        Ligne_perdue e0 = new Ligne_perdue()
                        {
                            ligne = balise_0_ligne,
                            texte = dataGEDCOM[balise_0_ligne]
                        };
                        groupe_ligne.Add(e0);
                        Ligne_perdue e1 = new Ligne_perdue();
                        {
                            e1.ligne = balise_1_ligne;
                            e1.texte = dataGEDCOM[balise_1_ligne];
                        }
                        groupe_ligne.Add(e1);
                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                        groupe_ligne.Clear();
                        break;
                }
            }
            //if (extraEVEN != "") info.titre += extraEVEN;
            info.N2_TYPE_liste = N2_TYPE_liste;
            info.ligne_perdue_liste = ligne_perdue_liste;
            info.N2_PHON_liste = N2_PHON_liste;
            info.N2_FAX_liste = N2_FAX_liste;
            info.N2_EMAIL_liste = N2_EMAIL_liste;
            info.N2_WWW_liste = N2_WWW_liste;
            info.N2_ADDR_liste = N2_ADDR_liste;
            info.N2_NOTE_STRUCTURE_liste_ID = N2_NOTE_STRUCTURE_liste_ID;
            //info.N2_OBJE_LINK_liste_ID = N2_OBJE_LINK_liste_ID;
            //info.N2_AUDIO_liste = N2_AUDIO_liste;
            //info.N2_VIDEO_liste = N2_VIDEO_liste;
            //info.N2_PHOTO_liste = N2_PHOTO_liste;
            info.MULTIMEDIA_LINK_liste_ID = MULTIMEDIA_LINK_liste_ID;
            info.N2_SOUR_citation_liste_ID = N2_SOUR_citation_liste_ID;
            info.N2_SOUR_source_liste_ID = N2_SOUR_source_liste_ID;
            info.N2_TEXT_liste = N2_TEXT_liste;
            info.N2_CHAN = N2_CHAN;
            return (info, ligne);
        }
        private static string Extraire_EVEN_liste(string liste)
        {
            if (IsNullOrEmpty(liste)) return null;
            // séparer par ','
            if (liste.Contains(","))
                {
                char[] charactere = new char[] { ',' };
                string[] item_split = liste.Split(charactere);
                int nombre_item = item_split.Length;
                string grouper = null;
                for (int f = 0; f < nombre_item; f++)
                {
                    item_split[f] = item_split[f].Trim();
                    grouper += item_split[f] + ", ";
                }
                grouper = grouper.TrimEnd(' ', ',');
                return grouper;
            }
            // séparer par espace
            if (liste.Contains(" "))
            {
                char[] charactere = new char[] { ' ' };
                string[] item_split = liste.Split(charactere);
                int nombre_item = item_split.Length;
                string grouper = null;
                for (int f = 0; f < nombre_item; f++)
                {
                    if (item_split[f] != null || item_split[f] != "")
                    grouper += item_split[f] + ", ";
                }
                grouper = grouper.TrimEnd(' ', ',');
                return grouper;
            }
            return liste;
        }

        private static int Extraire_EVEN_RECORD_53(
            int ligne
            //, [CallerLineNumber] int callerLineNumber = 0
            ) // Version 5.3 seulement
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b><br>GEDCOM ligne=" + ligne + "<br>niveau=" + Extraire_niveau(ligne) + " " + dataGEDCOM[ligne]);

            List<Ligne_perdue> ligne_perdue_liste = new List<Ligne_perdue>();
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            bool trouver1;
            if (GH.GH.annuler) return (ligne);
            //string balise_0 = Avoir_balise(dataGEDCOM[ligne]);
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            string N0_ID = null;
            CHANGE_DATE N1_CHAN = null;
            string N1_EVEN = null;
            string N2_EVEN_TYPE = null;
            string N2_EVEN_DATE = null;
            string DATE_trier = "99999999";
            string N2_EVEN_SITE = null;
            PLACE_STRUCTURE N2_EVEN_PLAC = null;
            string N2_EVEN_PERI = null;
            string N2_EVEN_RELI = null;
            List<string> N2_EVEN_AUDIO_liste = new List<string>();
            List<string> N2_EVEN_PHOTO_liste = new List<string>();
            List<string> N2_EVEN_VIDEO_liste = new List<string>();
            List<string> MULTIMEDIA_LINK_liste_ID = new List<string>();
            List<TEXT_STRUCTURE> N2_EVEN_TEXT_liste = new List<TEXT_STRUCTURE>();
            List<string> N2_EVEN_SOUR_citation_liste_ID = new List<string>();
            List<string> N2_EVEN_SOUR_source_liste_ID = new List<string>();
            List<string> N2_EVEN_NOTE_STRUCTURE_liste_ID = new List<string>();
            string N2_EVEN_ROLE = null;
            string N3_EVEN_ROLE_TYPE = null;
            INDIVIDUAL_53 N3_EVEN_ROLE_INDIVIDUAL = new INDIVIDUAL_53
            {
                N0_NAME_liste = null
            };
            List<ASSOCIATION_STRUCTURE> N3_EVEN_ROLE_ASSO_liste = new List<ASSOCIATION_STRUCTURE>();
            //string N4_EVEN_ROLE_ASSO_TYPE = null;
            //List<NAME_STRUCTURE_53> N3_EVEN_ROLE_NAME_liste;
            string N3_EVEN_ROLE_RELATIONSHIP_tag = null;
            string N3_EVEN_ROLE_RELATIONSHIP_ID = null;
            string N4_EVEN_ROLE_RELATIONSHIP_TYPE = null;
            INDIVIDUAL_53 N4_EVEN_ROLE_RELATIONSHIP_INDIVIDUAL = new INDIVIDUAL_53();
            string temp;
            try
            {
                N0_ID = Extraire_ID(dataGEDCOM[ligne]);
                ligne++;
                while (Extraire_niveau(ligne) > 0)
                {
                    string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                    int balise_1_ligne = ligne;
                    switch (balise_1)
                    {
                        case "CHAN":                                                                    // 1 CHAN
                            (N1_CHAN, ligne, ligne_perdue_liste) = Extraire_CHANGE_DATE(ligne, ligne_perdue_liste);
                            break;
                        //IND_EVNT_TAG
                        case "ADOP":
                        case "BIRT":
                        case "BAPM":
                        case "BARM":
                        case "BASM":
                        case "BLES":
                        case "BURI":
                        case "CENS":
                        case "CHR":
                        case "CHRA":
                        case "CONF":
                        case "DEAT":
                        case "EVEN":
                        case "EMIG":
                        case "GRAD":
                        case "IMMI":
                        case "MARR":
                        case "NATU":
                        case "ORDN":
                        case "RETI":

                        // FAM_EVNT_TAG
                        // case "CENS": inclue dans individu
                        // case "MARR": inclue dans individu
                        case "MARB":
                        case "MARC":
                        case "MARL":
                        case "MARS":
                        case "ENGA":
                        // case "EVEN": b = true; break;

                        // DIV_EVNT_TAG
                        case "ANUL":
                        case "DIV":
                        case "DIVF":
                            N1_EVEN = dataGEDCOM[ligne].ToUpper().Substring(2);
                            ligne++;
                            while (Extraire_niveau(ligne) > 1)
                            {
                                string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                                int balise_2_ligne = ligne;
                                switch (balise_2)
                                {
                                    case "TYPE":                                                        // TYPE
                                        (N2_EVEN_TYPE, ligne) = Extraire_texte_niveau_plus(ligne);
                                        break;
                                    case "DATE":                                                        // DATE
                                        (N2_EVEN_DATE, ligne) = Extraire_texte_niveau_plus(ligne);
                                        DATE_trier = Convertir_date_trier(N2_EVEN_DATE);
                                        break;
                                    case "PLAC":                                                        // PLAC
                                        (N2_EVEN_PLAC, ligne, ligne_perdue_liste) = Extraire_PLACE_STRUCTURE(ligne, ligne_perdue_liste);
                                        break;
                                    case "PERI":                                                        // PERI
                                        (N2_EVEN_PERI, ligne) = Extraire_texte_niveau_plus(ligne);
                                        break;
                                    case "RELI":                                                        // RELI
                                        (N2_EVEN_RELI, ligne) = Extraire_texte_niveau_plus(ligne);
                                        break;
                                    case "AUDIO":                                                       // AUDIO individu V5.3
                                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                                        N2_EVEN_AUDIO_liste.Add(temp);
                                        break;
                                    case "PHOTO":                                                       // PHOTO individu  V5.3
                                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                                        N2_EVEN_PHOTO_liste.Add(temp);
                                        break;
                                    case "VIDEO":                                                       // VIDEO individu V5.3
                                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                                        N2_EVEN_VIDEO_liste.Add(temp);
                                        break;
                                    case "TEXT":                                                        // TEXT
                                        TEXT_STRUCTURE temp2;
                                        (temp2, ligne, ligne_perdue_liste) = Extraire_TEXT_STRUCTURE(ligne, ligne_perdue_liste);
                                        N2_EVEN_TEXT_liste.Add(temp2);
                                        break;
                                    case "SOUR":                                                        // SOUR
                                        string citation;
                                        string source;
                                        (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                                        if (IsNotNullOrEmpty(citation)) N2_EVEN_SOUR_citation_liste_ID.Add(citation);
                                        if (IsNotNullOrEmpty(source) && IsNullOrEmpty(citation)) N2_EVEN_SOUR_source_liste_ID.Add(source);
                                        break;
                                    case "NOTE":                                                        // NOTE
                                        string IDNote;
                                        (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                                        N2_EVEN_NOTE_STRUCTURE_liste_ID.Add(IDNote);
                                        break;
                                    case "BROT":
                                    case "BUYR":
                                    case "CHIL":
                                    case "FATH":
                                    case "GODP":
                                    case "HDOG":
                                    case "HDOH":
                                    case "HEIR":
                                    case "HFAT":
                                    case "HMOT":
                                    case "HUSB":
                                    case "INDI":
                                    case "INFT":
                                    case "LEGA":
                                    case "MEMBER":
                                    case "MOTH":
                                    case "OFFI":
                                    case "PARE":
                                    case "PHUS":
                                    case "PWIF":
                                    case "RECO":
                                    case "REL":
                                    case "ROLE":
                                    case "SELR":
                                    case "TXPY":
                                    case "WFAT":
                                    case "WIFE":
                                    case "WITN":
                                    case "WMOT":
                                        N2_EVEN_ROLE = dataGEDCOM[ligne].Substring(2);
                                        ligne++;
                                        while (Extraire_niveau(ligne) > 2)
                                        {
                                            string balise_3 = Avoir_balise(dataGEDCOM[ligne]);
                                            
                                            int balise_3_ligne = ligne;
                                            int niveau = 3;
                                            /*(N3_EVEN_ROLE_INDIVIDUAL, ligne, trouver1,
                                                ligne_perdue_liste) =                       // individual
                                                Extraire_INDIVIDUAL_53(N3_EVEN_ROLE_INDIVIDUAL, ligne,
                                                niveau,
                                                ligne_perdue_liste);
                                            if (trouver1)
                                            {
                                                balise_3 = "";
                                            }
                                            */
                                            switch (balise_3)
                                            {
                                                case "":
                                                    break;
                                                case "TYPE":                                                    // EVEN ROLE TYPE
                                                    (N3_EVEN_ROLE_TYPE, ligne) = Extraire_texte_niveau_plus(ligne);
                                                    break;
                                                case "ASSO":                                                    // EVEN ROLE ASSO
                                                    ASSOCIATION_STRUCTURE temp3 = null;
                                                    (temp3, ligne, ligne_perdue_liste) = Extraire_ASSOCIATION_STRUCTURE(ligne, ligne_perdue_liste);
                                                    N3_EVEN_ROLE_ASSO_liste.Add(temp3);
                                                    break;
                                                case "BROT":                                                    // RELATIONSHIP
                                                case "CHIL":
                                                case "FATH":
                                                case "HEIR":
                                                case "HUSB":
                                                case "MOTH":
                                                case "PARE":
                                                case "PHUS":
                                                case "PWIF":
                                                case "SIBL":
                                                case "SIST":
                                                case "WIFE":
                                                    (niveau, N3_EVEN_ROLE_RELATIONSHIP_ID, N3_EVEN_ROLE_RELATIONSHIP_tag, _, ligne) = Extraire_info_niveau_0(ligne);
                                                    //N3_EVEN_ROLE_RELATIONSHIP_tag = Convertir_RELATIONSHIP_ROLE_TAG(N3_EVEN_ROLE_RELATIONSHIP_tag);
                                                    while (Extraire_niveau(ligne) > 3)
                                                    {
                                                        string balise_4 = Avoir_balise(dataGEDCOM[ligne]);
                                                        int balise_4_ligne = ligne;
                                                        (N4_EVEN_ROLE_RELATIONSHIP_INDIVIDUAL, ligne, _,
                                                            ligne_perdue_liste) =             // individual
                                                            Extraire_INDIVIDUAL_53(
                                                                N4_EVEN_ROLE_RELATIONSHIP_INDIVIDUAL,
                                                                ligne, niveau + 1,
                                                                ligne_perdue_liste);
                                                        switch (balise_4)
                                                        {
                                                            case "NAME":
                                                            case "TITL":
                                                            case "SEX":
                                                            case "ADOP":                                                                            // événement
                                                            case "BIRT":
                                                            case "BAPM":
                                                            case "BARM":
                                                            case "BASM":
                                                            case "BLES":
                                                            case "BURI":
                                                            case "CENS":
                                                            case "CHR":
                                                            case "CHRA":
                                                            case "CONF":
                                                            case "DEAT":
                                                            case "EVEN":
                                                            case "EMIG":
                                                            case "GRAD":
                                                            case "IMMI":
                                                            case "MARR":
                                                            case "NATU":
                                                            case "ORDN":
                                                            case "RETI":
                                                            case "MARB":
                                                            case "MARC":
                                                            case "MARL":
                                                            case "MARS":
                                                            case "ENGA":
                                                            case "ANUL":
                                                            case "DIV":
                                                            case "DIVF":
                                                            case "ADDR":
                                                            case "RELI":
                                                            case "NAMR":
                                                            case "EDUC":
                                                            case "OCCU":
                                                            case "SSN":
                                                            case "IDNO":
                                                            case "PROP":
                                                            case "DSCR":
                                                            case "SIGN":
                                                            case "NMR":
                                                            case "NCHI":
                                                            case "NATI":
                                                            case "CAST":
                                                                (N4_EVEN_ROLE_RELATIONSHIP_INDIVIDUAL,
                                                                    ligne, _,
                                                                    ligne_perdue_liste) =             // individual
                                                            Extraire_INDIVIDUAL_53(
                                                                N4_EVEN_ROLE_RELATIONSHIP_INDIVIDUAL,
                                                                ligne,
                                                                niveau + 1,
                                                                ligne_perdue_liste);
                                                                break;
                                                            case "TYPE":                                // TYPE
                                                                (N4_EVEN_ROLE_RELATIONSHIP_TYPE, ligne) = Extraire_texte_niveau_plus(ligne);
                                                                break;
                                                            default:
                                                                // pour la page HTML
                                                                Ligne_perdue aa = new Ligne_perdue()
                                                                {
                                                                    ligne = ligne,
                                                                    texte = dataGEDCOM[ligne]
                                                                };
                                                                ligne_perdue_liste.Add(aa);
                                                                // rapport Balise
                                                                method = MethodBase.GetCurrentMethod().Name;
                                                                lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                                                Ligne_perdue a0 = new Ligne_perdue()
                                                                {
                                                                    ligne = balise_0_ligne,
                                                                    texte = dataGEDCOM[balise_0_ligne]
                                                                };
                                                                groupe_ligne.Add(a0);
                                                                Ligne_perdue a1 = new Ligne_perdue();
                                                                {
                                                                    a1.ligne = balise_1_ligne;
                                                                    a1.texte = dataGEDCOM[balise_1_ligne];
                                                                }
                                                                groupe_ligne.Add(a1);
                                                                Ligne_perdue a2 = new Ligne_perdue();
                                                                {
                                                                    a2.ligne = balise_2_ligne;
                                                                    a2.texte = dataGEDCOM[balise_2_ligne];
                                                                }
                                                                groupe_ligne.Add(a2);
                                                                Ligne_perdue a3 = new Ligne_perdue();
                                                                {
                                                                    a3.ligne = balise_3_ligne;
                                                                    a3.texte = dataGEDCOM[balise_3_ligne];
                                                                }
                                                                groupe_ligne.Add(a3);
                                                                Ligne_perdue a4 = new Ligne_perdue();
                                                                {
                                                                    a4.ligne = balise_4_ligne;
                                                                    a3.texte = dataGEDCOM[balise_4_ligne];
                                                                }
                                                                groupe_ligne.Add(a4);
                                                                ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                                                groupe_ligne.Clear();
                                                                break;
                                                        }
                                                    }
                                                    break;
                                                default:
                                                    // pour la page HTML
                                                    Ligne_perdue bb = new Ligne_perdue()
                                                    {
                                                        ligne = ligne,
                                                        texte = dataGEDCOM[ligne]
                                                    };
                                                    ligne_perdue_liste.Add(bb);
                                                    // rapport Balise
                                                    method = MethodBase.GetCurrentMethod().Name;
                                                    lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                                    Ligne_perdue b0 = new Ligne_perdue()
                                                    {
                                                        ligne = balise_0_ligne,
                                                        texte = dataGEDCOM[balise_0_ligne]
                                                    };
                                                    groupe_ligne.Add(b0);
                                                    Ligne_perdue b1 = new Ligne_perdue();
                                                    {
                                                        b1.ligne = balise_1_ligne;
                                                        b1.texte = dataGEDCOM[balise_1_ligne];
                                                    }
                                                    groupe_ligne.Add(b1);
                                                    Ligne_perdue b2 = new Ligne_perdue();
                                                    {
                                                        b2.ligne = balise_2_ligne;
                                                        b2.texte = dataGEDCOM[balise_2_ligne];
                                                    }
                                                    groupe_ligne.Add(b2);
                                                    Ligne_perdue b3 = new Ligne_perdue();
                                                    {
                                                        b3.ligne = balise_3_ligne;
                                                        b3.texte = dataGEDCOM[balise_3_ligne];
                                                    }
                                                    groupe_ligne.Add(b3);
                                                    ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                                    groupe_ligne.Clear();
                                                    break;
                                            }
                                        }
                                        break;
                                    default:
                                        // pour la page HTML
                                        Ligne_perdue cc = new Ligne_perdue()
                                        {
                                            ligne = ligne,
                                            texte = dataGEDCOM[ligne]
                                        };
                                        ligne_perdue_liste.Add(cc);
                                        // rapport Balise
                                        method = MethodBase.GetCurrentMethod().Name;
                                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                        Ligne_perdue c0 = new Ligne_perdue()
                                        {
                                            ligne = balise_0_ligne,
                                            texte = dataGEDCOM[balise_0_ligne]
                                        };
                                        groupe_ligne.Add(c0);
                                        Ligne_perdue c1 = new Ligne_perdue();
                                        {
                                            c1.ligne = balise_1_ligne;
                                            c1.texte = dataGEDCOM[balise_1_ligne];
                                        }
                                        groupe_ligne.Add(c1);
                                        Ligne_perdue c2 = new Ligne_perdue();
                                        {
                                            c2.ligne = balise_2_ligne;
                                            c2.texte = dataGEDCOM[balise_2_ligne];
                                        }
                                        groupe_ligne.Add(c2);
                                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                        groupe_ligne.Clear();
                                        break;
                                }
                            }
                            break;
                        default:
                            // pour la page HTML
                            Ligne_perdue dd = new Ligne_perdue()
                            {
                                ligne = ligne,
                                texte = dataGEDCOM[ligne]
                            };
                            ligne_perdue_liste.Add(dd);
                            // rapport Balise
                            method = MethodBase.GetCurrentMethod().Name;
                            lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                            Ligne_perdue d0 = new Ligne_perdue()
                            {
                                ligne = balise_0_ligne,
                                texte = dataGEDCOM[balise_0_ligne]
                            };
                            groupe_ligne.Add(d0);
                            Ligne_perdue d1 = new Ligne_perdue();
                            {
                                d1.ligne = balise_1_ligne;
                                d1.texte = dataGEDCOM[balise_1_ligne];
                            }
                            groupe_ligne.Add(d1);
                            ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                            groupe_ligne.Clear();
                            break;
                    }
                }
            }
            catch (Exception msg)
            {
                GC.Collect();
                string message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                Voir_message(message, msg.Message, GH.GH.erreur);
            }
            if (!GH.GH.annuler)
            {
                liste_EVEN_RECORD_53.Add(new EVEN_RECORD_53()
                {
                    ligne_perdue_liste = ligne_perdue_liste,
                    N0_ID = N0_ID,
                    N1_CHAN = N1_CHAN,
                    N1_EVEN = N1_EVEN,
                    N2_EVEN_TYPE = N2_EVEN_TYPE,
                    N2_EVEN_DATE = N2_EVEN_DATE,
                    DATE_trier = DATE_trier,
                    N2_EVEN_SITE = N2_EVEN_SITE,
                    N2_EVEN_PLAC = N2_EVEN_PLAC,
                    N2_EVEN_PERI = N2_EVEN_PERI,
                    N2_EVEN_RELI = N2_EVEN_RELI,
                    N2_EVEN_AUDIO_liste = N2_EVEN_AUDIO_liste,

                    N2_EVEN_PHOTO_liste = N2_EVEN_PHOTO_liste,
                    N2_EVEN_VIDEO_liste = N2_EVEN_VIDEO_liste,
                    MULTIMEDIA_LINK_liste_ID = MULTIMEDIA_LINK_liste_ID,
                    N2_EVEN_TEXT_liste = N2_EVEN_TEXT_liste,
                    N2_EVEN_SOUR_citation_liste_ID = N2_EVEN_SOUR_citation_liste_ID,
                    N2_EVEN_SOUR_source_liste_ID = N2_EVEN_SOUR_source_liste_ID,
                    N2_EVEN_NOTE_STRUCTURE_liste_ID = N2_EVEN_NOTE_STRUCTURE_liste_ID,
                    N2_EVEN_ROLE = N2_EVEN_ROLE,
                    N3_EVEN_ROLE_TYPE = N3_EVEN_ROLE_TYPE,
                    N3_EVEN_ROLE_INDIVIDUAL = N3_EVEN_ROLE_INDIVIDUAL,
                    N3_EVEN_ROLE_ASSO_liste = N3_EVEN_ROLE_ASSO_liste,
                    //N4_EVEN_ROLE_ASSO_TYPE = N4_EVEN_ROLE_ASSO_TYPE,
                    N3_EVEN_ROLE_RELATIONSHIP_tag = N3_EVEN_ROLE_RELATIONSHIP_tag,
                    N3_EVEN_ROLE_RELATIONSHIP_ID = N3_EVEN_ROLE_RELATIONSHIP_ID,
                    N4_EVEN_ROLE_RELATIONSHIP_TYPE = N4_EVEN_ROLE_RELATIONSHIP_TYPE,
                    N4_EVEN_ROLE_RELATIONSHIP_INDIVIDUAL = N4_EVEN_ROLE_RELATIONSHIP_INDIVIDUAL
                });
            }
            return ligne;
        }

        private static (EVEN_STRUCTURE_53, int) Extraire_EVEN_STRUCTURE_53(int ligne, int niveau)
        {
            List<Ligne_perdue> ligne_perdue_liste = new List<Ligne_perdue>();
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            string N0_EVEN;
            (_, _, N0_EVEN, _, ligne) = Extraire_info_niveau_0(ligne);
            //string balise_0 = N0_EVEN;
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            EVEN_STRUCTURE_53 info_EVEN_STRUCTURE_53 = new EVEN_STRUCTURE_53
            {
                N1_NOTE_STRUCTURE_liste_ID = new List<string>(),
                N1_SOUR_citation_liste_ID = new List<string>(),
                N1_SOUR_source_liste_ID = new List<string>(),
                N1_TEXT_liste = new List<TEXT_STRUCTURE>(),
                N0_EVEN = N0_EVEN                                                                       // EVEN
            };
            while (Extraire_niveau(ligne) > niveau)
            {
                string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                switch (balise_1)
                {
                    case "TYPE":                                                                        // TYPE
                        (info_EVEN_STRUCTURE_53.N1_TYPE, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "DATE":                                                                        // DATE
                        (info_EVEN_STRUCTURE_53.N1_DATE, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "PLAC":                                                                        // PLAC
                        (info_EVEN_STRUCTURE_53.N1_PLAC, ligne, ligne_perdue_liste) = Extraire_PLACE_STRUCTURE(ligne, ligne_perdue_liste);
                        break;
                    case "AGE":                                                                         // AGE
                        (info_EVEN_STRUCTURE_53.N1_AGE, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "MSTAT":                                                                       // MSTAT
                        (info_EVEN_STRUCTURE_53.N1_MSTAT, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "CAUS":                                                                        // CAUS
                        (info_EVEN_STRUCTURE_53.N1_CAUS, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "RELI":                                                                        // RELI
                        (info_EVEN_STRUCTURE_53.N1_RELI, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "AGNC":                                                                        // AGNC
                        (info_EVEN_STRUCTURE_53.N1_AGNC, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "TEXT":                                                                        // TEXT
                        TEXT_STRUCTURE temp;
                        (temp, ligne, ligne_perdue_liste) = Extraire_TEXT_STRUCTURE(ligne, ligne_perdue_liste);
                        info_EVEN_STRUCTURE_53.N1_TEXT_liste.Add(temp);
                        break;
                    case "SOUR":                                                                        // 1 SOUR
                        string citation;
                        string source;
                        (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                        if (IsNotNullOrEmpty(citation)) info_EVEN_STRUCTURE_53.N1_SOUR_citation_liste_ID.Add(citation);
                        if (IsNotNullOrEmpty(source) && IsNullOrEmpty(citation)) info_EVEN_STRUCTURE_53.N1_SOUR_source_liste_ID.Add(source);
                        break;
                    case "NOTE":                                                                        // 1 NOTE
                        string IDNote;
                        (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                        info_EVEN_STRUCTURE_53.N1_NOTE_STRUCTURE_liste_ID.Add(IDNote);
                        break;
                    case "CHAN":                                                                        // 1 CHAN
                        (info_EVEN_STRUCTURE_53.N1_CHAN, ligne, _) = Extraire_CHANGE_DATE(ligne, null);
                        break;
                    default:
                        // pour la page HTML
                        Ligne_perdue aa = new Ligne_perdue()
                        {
                            ligne = ligne,
                            texte = dataGEDCOM[ligne]
                        };
                        ligne_perdue_liste.Add(aa);
                        // rapport Balise
                        method = MethodBase.GetCurrentMethod().Name;
                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                        Ligne_perdue a0 = new Ligne_perdue()
                        {
                            ligne = balise_0_ligne,
                            texte = dataGEDCOM[balise_0_ligne]
                        };
                        groupe_ligne.Add(a0);
                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                        groupe_ligne.Clear();
                        break;
                }
            }
            info_EVEN_STRUCTURE_53.ligne_perdue_liste = ligne_perdue_liste;

            return (info_EVEN_STRUCTURE_53, ligne);
        }

        private static int Extraire_FAM_RECORD(int ligne
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b> GEDCOM=" + ligne);

            Regler_code_erreur();
            if (GH.GH.annuler) return (ligne);
            List<Ligne_perdue> ligne_perdue_liste = new List<Ligne_perdue>();
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            //string balise_0 = Avoir_balise(dataGEDCOM[ligne]);
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            //string baliseN0 = Avoir_balise_p1(dataGEDCOM[ligne]).ToUpper();
            string N0_ID;
            string N1_RESN = null;
            List<EVEN_ATTRIBUTE_STRUCTURE> N1_Event_liste = new List<EVEN_ATTRIBUTE_STRUCTURE>();
            List<EVEN_ATTRIBUTE_STRUCTURE> N1_ATTRIBUTE_liste = new List<EVEN_ATTRIBUTE_STRUCTURE>();
            string N1_HUSB = null;
            string N1_WIFE = null;
            List<string> N1_CHIL_liste_ID = new List<string>();
            string N1_NCHI = null;
            List<string> N1_SUBM_liste_ID = new List<string>();
            LDS_SPOUSE_SEALING N1_SLGS = new LDS_SPOUSE_SEALING();
            List<USER_REFERENCE_NUMBER> N1_REFN_liste = new List<USER_REFERENCE_NUMBER>();
            string N1_RIN = null;
            CHANGE_DATE N1_CHAN = null;
            List<string> N1_NOTE_STRUCTURE_liste_ID = new List<string>();
            List<string> N1_SOUR_citation_liste_ID = new List<string>();
            List<string> N1_SOUR_source_liste_ID = new List<string>();
            //List<string> N1_OBJE_LINK_liste_ID = new List<string>();
            List<string> N1_AUDIO_liste;
            List<string> N1_PHOTO_liste;
            List<string> N1_VIDEO_liste;
            List<string> MULTIMEDIA_LINK_liste_ID = new List<string>();
            List<ASSOCIATION_STRUCTURE> N1_ASSO_liste = new List<ASSOCIATION_STRUCTURE>();
            string N1_TYPU = null; // Ancestrologie
            string N1__UST = null; // Heridis
            string temp;
            int niveau = 0;
            N0_ID = Extraire_ID(dataGEDCOM[ligne]);
            ligne++;
            // extraction
            try
            {
                while (Extraire_niveau(ligne) > niveau)
                {
                    string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                    int balise_1_ligne = ligne;
                    switch (balise_1)
                    {
                        // evenement famille
                        case "ANUL":
                        case "CENS":
                        case "DIV":
                        case "DIVF":
                        case "ENGA":
                        case "EVEN":
                        case "MARB":
                        case "MARC":
                        case "MARL":
                        case "MARR":
                        case "MARS":
                        case "RESI":
                        case "_ANCES_ORDRE": // ancestrologie
                        case "_ANCES_XINSEE":// ancestrologie
                            EVEN_ATTRIBUTE_STRUCTURE Event;
                            (Event, ligne) = Extraire_EVEN_ATTRIBUTE_STRUCTURE(ligne, niveau + 1);
                            N1_Event_liste.Add(Event);
                            break;
                        //attribute_famille
                        case "FACT":
                            EVEN_ATTRIBUTE_STRUCTURE Attribute;
                            (Attribute, ligne) = Extraire_EVEN_ATTRIBUTE_STRUCTURE(ligne, niveau + 1);
                            N1_ATTRIBUTE_liste.Add(Attribute);
                            break;
                        case "ASSO":                                                                    //1 ASSO V5.3
                            ASSOCIATION_STRUCTURE info;
                            (info, ligne, ligne_perdue_liste) = Extraire_ASSOCIATION_STRUCTURE(ligne, ligne_perdue_liste);
                            N1_ASSO_liste.Add(info);
                            break;
                        case "RESN":                                                                    //1 RESN
                            (N1_RESN, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "TYPU":                                                                    // 1 TYPU pour Ancestrologie
                            (N1_TYPU, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "_UST":                                                                    // 1 _UST Heridis
                            (N1__UST, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "HUSB":                                                                    // 1 HUSB
                            N1_HUSB = Extraire_ID(dataGEDCOM[ligne]);
                            ligne++;
                            break;
                        case "WIFE":                                                                    // 1 WIFE
                            N1_WIFE = Extraire_ID(dataGEDCOM[ligne]);
                            ligne++;
                            break;
                        case "CHIL":                                                                    // 1 CHIL
                            if (dataGEDCOM[ligne].Length > 7)
                            {
                                string IDEnfant = Extraire_ID(dataGEDCOM[ligne]);
                                N1_CHIL_liste_ID.Add(IDEnfant);
                            }
                            ligne++;
                            break;
                        case "NCHI":                                                                    // 1 NCHI
                            (N1_NCHI, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "SUBM":                                                                    // 1 SUBM
                            if (dataGEDCOM[ligne].Length > 7)
                            {
                                N1_SUBM_liste_ID.Add(Extraire_ID(dataGEDCOM[ligne]));
                                ligne++;
                            }
                            else
                            {
                                // pour la page HTML
                                Ligne_perdue aa = new Ligne_perdue()
                                {
                                    ligne = ligne,
                                    texte = dataGEDCOM[ligne]
                                };
                                ligne_perdue_liste.Add(aa);
                                // rapport Balise
                                method = MethodBase.GetCurrentMethod().Name;
                                lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                Ligne_perdue a0 = new Ligne_perdue()
                                {
                                    ligne = balise_0_ligne,
                                    texte = dataGEDCOM[balise_0_ligne]
                                };
                                groupe_ligne.Add(a0);
                                Ligne_perdue a1 = new Ligne_perdue();
                                {
                                    a1.ligne = balise_1_ligne;
                                    a1.texte = dataGEDCOM[balise_1_ligne];
                                }
                                groupe_ligne.Add(a1);
                                ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                groupe_ligne.Clear();
                            }
                            break;
                        case "SLGS":                                                                    // 1 SLGS
                            (N1_SLGS, ligne, ligne_perdue_liste) = Extraire_LDS_SPOUSE_SEALING(ligne, ligne_perdue_liste);
                            break;
                        case "REFN":                                                                    // 1 REFN
                            USER_REFERENCE_NUMBER N1_REFN;
                            (N1_REFN, ligne, ligne_perdue_liste) = Extraire_USER_REFERENCE_NUMBER(ligne, ligne_perdue_liste);
                            N1_REFN_liste.Add(N1_REFN);
                            break;
                        case "RIN":                                                                     // 1 RIN
                            (N1_RIN, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "CHAN":                                                                    // 1 CHAN
                            (N1_CHAN, ligne, ligne_perdue_liste) = Extraire_CHANGE_DATE(ligne, ligne_perdue_liste);
                            break;
                        case "NOTE":                                                                    // 1 NOTE
                            string IDNote;
                            (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                            N1_NOTE_STRUCTURE_liste_ID.Add(IDNote);
                            break;
                        case "OBJE":                                                                    // 1 OBJE Famille
                            (temp, ligne, ligne_perdue_liste) = Extraire_MULTIMEDIA_LINK(ligne, ligne_perdue_liste);
                            MULTIMEDIA_LINK_liste_ID.Add(temp);
                            break;
                        case "AUDIO":                                                                   // 1 AUDIO individu V5.3
                            (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                            MULTIMEDIA_LINK_liste_ID.Add(temp);
                            break;
                        case "PHOTO":                                                                // 1 PHOTO individu  V5.3
                            (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                            MULTIMEDIA_LINK_liste_ID.Add(temp);
                            break;
                        case "VIDEO":                                                               // 1 VIDEO individu V5.3
                            (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                            MULTIMEDIA_LINK_liste_ID.Add(temp);
                            break;
                        case "SOUR":                                                                // 1 SOUR
                            string citation;
                            string source;
                            (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                            if (IsNotNullOrEmpty(citation)) N1_SOUR_citation_liste_ID.Add(citation);
                            if (IsNotNullOrEmpty(source) && IsNullOrEmpty(citation))
                            {
                                N1_SOUR_source_liste_ID.Add(source);
                            }
                            break;
                        default:
                            // pour la page HTML
                            Ligne_perdue bb = new Ligne_perdue()
                            {
                                ligne = ligne,
                                texte = dataGEDCOM[ligne]
                            };
                            ligne_perdue_liste.Add(bb);
                            // rapport Balise
                            method = MethodBase.GetCurrentMethod().Name;
                            lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                            Ligne_perdue b0 = new Ligne_perdue()
                            {
                                ligne = balise_0_ligne,
                                texte = dataGEDCOM[balise_0_ligne]
                            };
                            groupe_ligne.Add(b0);
                            Ligne_perdue b1 = new Ligne_perdue();
                            {
                                b1.ligne = balise_1_ligne;
                                b1.texte = dataGEDCOM[balise_1_ligne];
                            }
                            groupe_ligne.Add(b1);
                            ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                            groupe_ligne.Clear();
                            break;
                    }
                }
            }
            catch (Exception msg)
            {
                GC.Collect();
                string message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                Voir_message(message, msg.Message, GH.GH.erreur);
            }
            if (!GH.GH.annuler)
            {
                liste_FAM_RECORD.Add(new FAM_RECORD()
                {
                    ligne_perdue_liste = ligne_perdue_liste,
                    N0_ID = N0_ID,
                    N1_EVEN_Liste = N1_Event_liste,
                    N1_ATTRIBUTE_liste = N1_ATTRIBUTE_liste, // pour GRAMPS
                    N1_ASSO_liste = N1_ASSO_liste, // V5.3
                    N1_RESN = N1_RESN,
                    N1_HUSB = N1_HUSB,
                    N1_WIFE = N1_WIFE,
                    N1_NCHI = N1_NCHI,
                    N1_CHIL_liste_ID = N1_CHIL_liste_ID,
                    N1_SUBM_liste_ID = N1_SUBM_liste_ID,
                    N1_SLGS = N1_SLGS,
                    N1_REFN_liste = N1_REFN_liste,
                    N1_RIN = N1_RIN,
                    //N1_OBJE_LINK_liste_ID = N1_OBJE_LINK_liste_ID,
                    //N1_AUDIO_liste = N1_AUDIO_liste,
                    //N1_PHOTO_liste = N1_PHOTO_liste,
                    //N1_VIDEO_liste = N1_VIDEO_liste,
                    MULTIMEDIA_LINK_liste_ID = MULTIMEDIA_LINK_liste_ID,
                    N1_NOTE_STRUCTURE_liste_ID = N1_NOTE_STRUCTURE_liste_ID,
                    N1_SOUR_citation_liste_ID = N1_SOUR_citation_liste_ID,
                    N1_SOUR_source_liste_ID = N1_SOUR_source_liste_ID,
                    N1_CHAN = N1_CHAN,
                    N1_TYPU = N1_TYPU,
                    N1__UST = N1__UST,
                });
            }
            return ligne;
        }

        private static int Extraire_INDIVIDUAL_RECORD(
            int ligne
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b> GEDCOM=" + ligne );

            Regler_code_erreur();
            List<Ligne_perdue> ligne_perdue_liste = new List<Ligne_perdue>();
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            string N0_ID;
            string N1_RESN = null;
            List<PERSONAL_NAME_STRUCTURE> N1_NAME_liste = new List<PERSONAL_NAME_STRUCTURE>();
            string N1_SEX = null;
            List<EVEN_ATTRIBUTE_STRUCTURE> N1_Event_liste = new List<EVEN_ATTRIBUTE_STRUCTURE>();
            List<EVEN_ATTRIBUTE_STRUCTURE> N1_ATTRIBUTE_liste = new List<EVEN_ATTRIBUTE_STRUCTURE>();
            string N1_SITE = null;
            List<ADDRESS_STRUCTURE> N1_ADDR_liste = new List<ADDRESS_STRUCTURE>();
            string N1_RELI = null;
            string N1_NAMR = null;
            string N2_NAMR_RELI = null;
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
            //CHANGE_DATE N1_CHAN = new CHANGE_DATE();
            CHANGE_DATE N1_CHAN = null;
            List<string> N1_NOTE_STRUCTURE_liste_ID = new List<string>();
            List<string> N1_SOUR_citation_liste_ID = new List<string>();
            List<string> N1_SOUR_source_liste_ID = new List<string>();
            List<string> MULTIMEDIA_LINK_liste_ID= new List<string>();
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
            List<string> N1_PHON_liste = new List<string>();
            List<string> N1_EMAIL_liste = new List<string>();
            List<string> N1_FAX_liste = new List<string>();
            List<string> N1_WWW_liste = new List<string>();
            List<LDS_INDIVIDUAL_ORDINANCE> N1_LDS_liste = new List<LDS_INDIVIDUAL_ORDINANCE>();
            int niveau = 0;
            //int cn = 0;
            if (dataGEDCOM[ligne].Length == 5)
            {
                dataGEDCOM[ligne] = dataGEDCOM[ligne];
            }
            N0_ID = Extraire_ID(dataGEDCOM[ligne]);
            ligne++;
            try
            {
                while (Extraire_niveau(ligne) > niveau)
                {
                    string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                    int balise_1_ligne = ligne;
                    switch (balise_1)
                    {
                        case "RESN":                                                                    //1 RESN
                            (N1_RESN, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "NAME":                                                                    // 1 NAME
                            PERSONAL_NAME_STRUCTURE info_nom = new PERSONAL_NAME_STRUCTURE();
                            (info_nom, ligne, ligne_perdue_liste) = Extraire_PERSONAL_NAME_STRUCTURE(ligne, ligne_perdue_liste);
                            N1_NAME_liste.Add(info_nom);
                            break;
                        // événement 
                        case "TEST":
                        case "ADOP":
                        case "BAPM":
                        case "BARM":
                        case "BASM":
                        case "BIRT":
                        case "BLES":
                        case "BURI":
                        case "CENS":
                        case "CHR":
                        case "CHRA":
                        case "CONF":
                        case "CREM":
                        case "DEAT":
                        case "EMIG":
                        case "EVEN":
                        case "FCOM":
                        case "GRAD":
                        case "IMMI":
                        case "NATU":
                        case "ORDN":
                        case "PROB":
                        case "RETI":
                        case "WILL":
                        case "_MDCL": // oem
                        case "_MILT":
                        case "_ELEC": // GRAMPS
                            EVEN_ATTRIBUTE_STRUCTURE Event;
                            (Event, ligne) = Extraire_EVEN_ATTRIBUTE_STRUCTURE(ligne, niveau + 1);
                            N1_Event_liste.Add(Event);
                            break;
                        // attribut
                        case "CAST":
                        case "DSCR":
                        case "EDUC":
                        case "IDNO":
                        case "NATI":
                        case "NCHI":
                        case "NMR":
                        case "OCCU":
                        case "PROP":
                        case "RELI":
                        case "RESI":
                        case "SSN":
                        case "TITL":
                        case "FACT":
                            EVEN_ATTRIBUTE_STRUCTURE Attribute;
                            (Attribute, ligne) = Extraire_EVEN_ATTRIBUTE_STRUCTURE(ligne, niveau + 1);
                            N1_ATTRIBUTE_liste.Add(Attribute);
                            break;
                        // SITE V5.3
                        case "SITE":                                                                    // 1 SITE V5.3
                            (N1_SITE, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "ADDR":                                                                    // 1 ADDR V5.3
                            ADDRESS_STRUCTURE N1_ADDR;
                            (N1_ADDR, ligne, ligne_perdue_liste) = Extraire_ADDRESS_STRUCTURE(ligne, ligne_perdue_liste);
                            N1_ADDR_liste.Add(N1_ADDR);
                            break;
                        // RELIGIOUS_AFFILIATION V5.3 1 NAMR
                        case "NAMR":                                                                    // 1 NAMR V5.3
                            (N1_NAMR, ligne) = Extraire_texte_niveau_plus(ligne);
                            while (Extraire_niveau(ligne) > niveau + 1)
                            {
                                if (Avoir_balise_p1(dataGEDCOM[ligne].ToUpper()) == "2 RELI")               // 1 NAMR RELI V5.3
                                {
                                    (N2_NAMR_RELI, ligne) = Extraire_texte_niveau_plus(ligne);
                                }
                                else
                                {
                                    // pour la page HTML
                                    Ligne_perdue aa = new Ligne_perdue()
                                    {
                                        ligne = ligne,
                                        texte = dataGEDCOM[ligne]
                                    };
                                    ligne_perdue_liste.Add(aa);
                                    // rapport Balise
                                    method = MethodBase.GetCurrentMethod().Name;
                                    lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                    Ligne_perdue a0 = new Ligne_perdue()
                                    {
                                        ligne = balise_0_ligne,
                                        texte = dataGEDCOM[balise_0_ligne]
                                    };
                                    groupe_ligne.Add(a0);
                                    Ligne_perdue a1 = new Ligne_perdue();
                                    {
                                        a1.ligne = balise_1_ligne;
                                        a1.texte = dataGEDCOM[balise_1_ligne];
                                    }
                                    groupe_ligne.Add(a1);
                                    ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                    groupe_ligne.Clear();
                                }
                            }
                            break;
                        // Si ordinance
                        case "BAPL": //allowed in GEDCOM V5.3 V5.5.1
                        case "CONL": //allowed in GEDCOM V5.3 V5.5.1
                        case "ENDL": //allowed in GEDCOM V5.3 V5.5.1
                        case "SLGC": //allowed in GEDCOM V5.5.1
                        case "WAC":   //allowed in GEDCOM V5.3
                            (N1_LDS, ligne) = Extraire_LDS_INDIVIDUAL_ORDINANCE_even(ligne);
                            N1_LDS_liste.Add(N1_LDS);
                            break;
                        case "FILA":                                                                    // FILA dans Ancestrologie
                            (N1_FILA, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "SEX":                                                                     // SEX
                            (N1_SEX, ligne) = Extraire_texte_niveau_plus(ligne);
                            N1_SEX = N1_SEX.ToUpper();
                            break;
                        case "SUBM":                                                                    // SUBM
                            if (dataGEDCOM[ligne].Length > 7)
                                N1_SUBM_liste_ID.Add(Extraire_ID(dataGEDCOM[ligne]));
                            ligne++;
                            break;
                        case "ASSO":                                                                    // ASSO
                            ASSOCIATION_STRUCTURE info;
                            (info, ligne, ligne_perdue_liste) = Extraire_ASSOCIATION_STRUCTURE(ligne, ligne_perdue_liste);
                            N1_ASSO_liste.Add(info);
                            break;
                        case "ALIA":                                                                    // ALIA
                            N1_ALIA_liste_ID.Add(Extraire_ID(dataGEDCOM[ligne]));
                            ligne++;
                            break;
                        case "ANCI":                                                                    // ANCI
                            N1_ANCI_liste_ID.Add(Extraire_ID(dataGEDCOM[ligne]));
                            ligne++;
                            break;
                        case "DESI":                                                                    // DESI
                            N1_DESI_liste_ID.Add(Extraire_ID(dataGEDCOM[ligne]));
                            ligne++;
                            break;
                        case "RFN":                                                                     // RFN
                            (N1_RFN, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "AFN":                                                                     // AFN
                            (N1_AFN, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "FAMS":                                                                    // FAMS conjoint/spouse
                            SPOUSE_TO_FAMILY_LINK temp1;// = new SPOUSE_TO_FAMILY_LINK();
                            (temp1, ligne, ligne_perdue_liste) = Extraire_SPOUSE_TO_FAMILY_LINK(ligne, ligne_perdue_liste);
                            N1_FAMS_liste_Conjoint.Add(temp1);
                            break;
                        case "REFN":                                                                    // REFN
                            USER_REFERENCE_NUMBER N1_REFN;
                            (N1_REFN, ligne, ligne_perdue_liste) = Extraire_USER_REFERENCE_NUMBER(ligne, ligne_perdue_liste);
                            N1_REFN_liste.Add(N1_REFN);
                            break;
                        case "FAMC":                                                                    // FAMC enfant de la famille
                            (N1_FAMC, ligne, ligne_perdue_liste) = Extraire_CHILD_TO_FAMILY_LINK(ligne, ligne_perdue_liste);
                            break;
                        case "SOUR":                                                                    // SOUR
                            string citation;
                            string source;
                            (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                            if (IsNotNullOrEmpty(citation)) N1_SOUR_citation_liste_ID.Add(citation);
                            if (IsNotNullOrEmpty(source) && IsNullOrEmpty(citation)) N1_SOUR_source_liste_ID.Add(source);
                            break;
                        case "OBJE":                                                                    // OBJE individu
                            string temp_ID = null;
                            (temp_ID, ligne, ligne_perdue_liste) = Extraire_MULTIMEDIA_LINK(ligne, ligne_perdue_liste);
                            MULTIMEDIA_LINK_liste_ID.Add(temp_ID);
                            break;
                        case "AUDIO":                                                                   // AUDIO individu V5.3
                            string fichier_audio = null;
                            (fichier_audio, ligne) = Extraire_texte_niveau_plus(ligne);
                            string audio_ID = Extraire_MULTIMEDIA_LINK(fichier_audio);
                            MULTIMEDIA_LINK_liste_ID.Add(audio_ID);
                            break;
                        case "PHOTO":                                                                   // PHOTO individu  V5.3
                            string fichier_photo = null;
                            (fichier_photo, ligne) = Extraire_texte_niveau_plus(ligne);
                            string photo_ID = Extraire_MULTIMEDIA_LINK(fichier_photo);
                            MULTIMEDIA_LINK_liste_ID.Add(photo_ID);
                            break;
                        case "VIDEO":                                                                   // VIDEO individu V5.3
                            string fichier_video = null;
                            (fichier_video, ligne) = Extraire_texte_niveau_plus(ligne);
                            string video_ID = Extraire_MULTIMEDIA_LINK(fichier_video);
                            MULTIMEDIA_LINK_liste_ID.Add(video_ID);
                            break;
                        case "NOTE":                                                                    // NOTE
                            string IDNote;
                            (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(
                                ligne, ligne_perdue_liste);
                            N1_NOTE_STRUCTURE_liste_ID.Add(IDNote);
                            break;
                        case "CHAN":                                                                    // 1 CHAN
                            (N1_CHAN, ligne, ligne_perdue_liste) = Extraire_CHANGE_DATE(
                                ligne, ligne_perdue_liste);
                            break;
                        case "RIN":                                                                     // RIN
                            (N1_RIN, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "_ANCES_CLE_FIXE":                                                         // _ANCES_CLE_FIXE
                            (N1__ANCES_CLE_FIXE, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "PHON":                                                                    // PHON
                            string temp_phon = null;
                            (temp_phon, ligne) = Extraire_texte_niveau_plus(ligne);
                            N1_PHON_liste.Add(temp_phon);
                            break;
                        case "EMAIL":                                                                   // EMAIL
                            string temp_email = null;
                            (temp_email, ligne) = Extraire_texte_niveau_plus(ligne);
                            N1_EMAIL_liste.Add(temp_email);
                            break;
                        case "FAX":                                                                     // FAX
                            string temp_fax = null;
                            (temp_fax, ligne) = Extraire_texte_niveau_plus(ligne);
                            N1_FAX_liste.Add(temp_fax);
                            break;
                        case "WWW":                                                                     // WWW
                            string temp_www = null;
                            (temp_www, ligne) = Extraire_texte_niveau_plus(ligne);
                            N1_WWW_liste.Add(temp_www);
                            break;
                        case "SIGN":                                                                    // SIGN
                            (N1_SIGN, ligne) = Extraire_texte_niveau_plus(ligne);
                            N1_SIGN = N1_SIGN.ToUpper();
                            break;
                        case "_FIL":                                                                    // _FIL pour Heresis
                            (N1__FIL, ligne) = Extraire_texte_niveau_plus(ligne);
                            if (IsNotNullOrEmpty(N1__FIL)) N1__FIL = N1__FIL.ToUpper();
                            break;
                        case "_CLS":                                                                    // CLS pour Heresis
                            (N1__CLS, ligne) = Extraire_texte_niveau_plus(ligne);
                            if (IsNotNullOrEmpty(N1__CLS)) N1__CLS = N1__CLS.ToUpper();
                            break;
                        default:
                            // pour la page HTML
                            Ligne_perdue bb = new Ligne_perdue()
                            {
                                ligne = ligne,
                                texte = dataGEDCOM[ligne]
                            };
                            ligne_perdue_liste.Add(bb);
                            // rapport Balise
                            method = MethodBase.GetCurrentMethod().Name;
                            lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                            Ligne_perdue b0 = new Ligne_perdue()
                            {
                                ligne = balise_0_ligne,
                                texte = dataGEDCOM[balise_0_ligne]
                            };
                            groupe_ligne.Add(b0);
                            Ligne_perdue b1 = new Ligne_perdue();
                            {
                                b1.ligne = balise_1_ligne;
                                b1.texte = dataGEDCOM[balise_1_ligne];
                            }
                            groupe_ligne.Add(b1);
                            ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                            groupe_ligne.Clear();
                            break;
                    }
                }
            }
            catch (Exception msg)
            {
                GC.Collect();
                string message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                Voir_message(message, msg.Message, GH.GH.erreur);
            }
            if (!GH.GH.annuler)
            {
                liste_INDIVIDUAL_RECORD.Add(new INDIVIDUAL_RECORD()
                {
                    ligne_perdue_liste = ligne_perdue_liste,
                    N0_ID = N0_ID,
                    N1_RESN = N1_RESN,
                    N1__ANCES_CLE_FIXE = N1__ANCES_CLE_FIXE,
                    Titre = titre,
                    nom_section_1 = nom_section_1,
                    nom_section_2 = nom_section_2,
                    nom_section_3 = nom_section_3,
                    N1_NAME_liste = N1_NAME_liste,
                    //N1_AUDIO_liste = N1_AUDIO_liste,
                    //N1_PHOTO_liste = N1_PHOTO_liste,
                    //N1_VIDEO_liste = N1_VIDEO_liste,
                    MULTIMEDIA_LINK_liste_ID = MULTIMEDIA_LINK_liste_ID,
                    PhotoID = photoID,
                    N1_SEX = N1_SEX,
                    N1_EVEN_Liste = N1_Event_liste,
                    N1_Attribute_liste = N1_ATTRIBUTE_liste,
                    N1_SITE = N1_SITE,
                    N1_ADDR_liste = N1_ADDR_liste, // V5.3
                    N1_RELI = N1_RELI, // V5.3
                    N1_NAMR = N1_NAMR,
                    N2_NAMR_RELI = N2_NAMR_RELI,
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
                    N1_NOTE_STRUCTURE_liste_ID = N1_NOTE_STRUCTURE_liste_ID,
                    N1_SOUR_citation_liste_ID = N1_SOUR_citation_liste_ID,
                    N1_SOUR_source_liste_ID = N1_SOUR_source_liste_ID,
                    N1_RIN = N1_RIN,
                    N1_PHON_liste = N1_PHON_liste,
                    N1_EMAIL_liste = N1_EMAIL_liste,
                    N1_FAX_liste = N1_FAX_liste,
                    N1_WWW_liste = N1_WWW_liste,
                    N1_SIGN = N1_SIGN, // Heridis
                    N1__FIL = N1__FIL, // Heridis
                    N1__CLS = N1__CLS, // Heridis
                });
            }
            return ligne;
        }
        private static int Extraire_niveau(int ligne)
        {
            char[] espace = { ' ' };
            string[] section = dataGEDCOM[ligne].Split(espace);
            return Int32.Parse(section[0]);
        }

        private static int Extraire_NOTE_RECORD(int ligne
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b> GEDCOM=" + ligne);

            Regler_code_erreur();
            List<Ligne_perdue> ligne_perdue_liste = new List<Ligne_perdue>();
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            string N0_texte;
            string N0_ID;
            List<USER_REFERENCE_NUMBER> N1_REFN_liste = new List<USER_REFERENCE_NUMBER>();
            string N1_RIN = null;
            List<string> N1_SOUR_citation_liste_ID = new List<string>();
            List<string> N1_SOUR_source_liste_ID = new List<string>();
            List<string> N1_NOTE_STRUCTURE_liste_ID = new List<string>();
            List<CHANGE_DATE> N1_CHAN_liste = new List<CHANGE_DATE>();
            int niveau = 0;
            //string balise_0;
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            // extraire le texte et ID de la ligne niveau 0
            (_, N0_ID, _, N0_texte, ligne) = Extraire_info_niveau_0(ligne);
            try
            {
                while (Extraire_niveau(ligne) > niveau)
                {
                    string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                    int balise_1_ligne = ligne;
                    switch (balise_1)
                    {
                        case "REFN":                                                                    // REFN
                            USER_REFERENCE_NUMBER N1_REFN;
                            (N1_REFN, ligne, ligne_perdue_liste) = Extraire_USER_REFERENCE_NUMBER(ligne, ligne_perdue_liste);
                            N1_REFN_liste.Add(N1_REFN);
                            break;
                        case "RIN":                                                                     // RIN
                            (N1_RIN, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "SOUR":                                                                    // SOUR
                            string citation;
                            string source;
                            (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                            if (IsNotNullOrEmpty(citation)) N1_SOUR_citation_liste_ID.Add(citation);
                            if (IsNotNullOrEmpty(source) && IsNullOrEmpty(citation)) N1_SOUR_source_liste_ID.Add(source);
                            break;
                        case "CHAN":                                                                    // CHAN
                            CHANGE_DATE info_date = null;
                            (info_date, ligne, ligne_perdue_liste) = Extraire_CHANGE_DATE(ligne, ligne_perdue_liste);
                            N1_CHAN_liste.Add(info_date);
                            break;
                        case "NOTE":                                                                    // NOTE
                            string IDNote;
                            (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                            N1_NOTE_STRUCTURE_liste_ID.Add(IDNote);
                            break;
                        default:
                            // pour la page HTML
                            Ligne_perdue aa = new Ligne_perdue()
                            {
                                ligne = ligne,
                                texte = dataGEDCOM[ligne]
                            };
                            ligne_perdue_liste.Add(aa);
                            // rapport Balise
                            method = MethodBase.GetCurrentMethod().Name;
                            lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                            Ligne_perdue a0 = new Ligne_perdue()
                            {
                                ligne = balise_0_ligne,
                                texte = dataGEDCOM[balise_0_ligne]
                            };
                            groupe_ligne.Add(a0);
                            Ligne_perdue a1 = new Ligne_perdue();
                            {
                                a1.ligne = balise_1_ligne;
                                a1.texte = dataGEDCOM[balise_1_ligne];
                            }
                            groupe_ligne.Add(a1);
                            ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                            groupe_ligne.Clear();
                            break;
                    }
                }
            }
            catch (Exception msg)
            {
                GC.Collect();
                string message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                Voir_message(message, msg.Message, GH.GH.erreur);
                if (GH.GH.annuler) return ligne;
            }
            if (!GH.GH.annuler)
            {
                {
                    liste_NOTE_RECORD.Add(new NOTE_RECORD()
                    {
                        ligne_perdue_liste = ligne_perdue_liste,
                        N0_ID = N0_ID,
                        N0_texte = N0_texte,
                        N1_RIN = N1_RIN,
                        N1_REFN_liste = N1_REFN_liste,
                        N1_SOUR_citation_liste_ID = N1_SOUR_citation_liste_ID,
                        N1_SOUR_source_liste_ID = N1_SOUR_source_liste_ID,
                        N1_NOTE_STRUCTURE_liste_ID = N1_NOTE_STRUCTURE_liste_ID,
                        N1_CHAN_liste = N1_CHAN_liste,
                    });
                }
            }
            return ligne;
        }

        private static (string, int, List<Ligne_perdue>) Extraire_NOTE_STRUCTURE(
            int ligne,
            List<Ligne_perdue> ligne_perdue_liste
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            int niveau;
            string N0_ID_RECORD;
            string N0_texte;
            string N0_ID_STRUCTURE;
            (niveau, N0_ID_RECORD, _, N0_texte, ligne) = Extraire_info_niveau_0(ligne);
            // créer un ID_STRUCTURE
            N0_ID_STRUCTURE = "N-" + String.Format("{0:-00-00-00-00}", ++numero_ID);
            // DEBUG
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + "<br>N0_ID_RECORD=" + N0_ID_RECORD + "<br>N0_ID_STRUCTURE=" + N0_ID_STRUCTURE + "<br>N0_texte= " + N0_texte);
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            List<string> N1_SOUR_citation_liste_ID = new List<string>();
            List<string> N1_SOUR_source_liste_ID = new List<string>();
            List<string> N1_NOTE_STRUCTURE_liste_ID = new List<string>();
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            try
            {
                while (Extraire_niveau(ligne) > niveau)
                {
                    string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                    int balise_1_ligne = ligne;
                    switch (balise_1)
                    {
                        case "SOUR":                                                                    // SOUR
                            string citation;
                            string source;
                            (citation, source, ligne) = Extraire_SOURCE_CITATION(ligne);
                            if (IsNotNullOrEmpty(citation)) N1_SOUR_citation_liste_ID.Add(citation);
                            if (IsNotNullOrEmpty(source) && IsNullOrEmpty(citation)) N1_SOUR_source_liste_ID.Add(source);
                            break;
                        case "NOTE":                                                                    // NOTE
                            string IDNote;
                            (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                            N1_NOTE_STRUCTURE_liste_ID.Add(IDNote);
                            break;
                        default:
                            // pour la page HTML
                            Ligne_perdue aa = new Ligne_perdue()
                            {
                                ligne = ligne,
                                texte = dataGEDCOM[ligne]
                            };
                            ligne_perdue_liste.Add(aa);
                            // rapport Balise
                            method = MethodBase.GetCurrentMethod().Name;
                            lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                            Ligne_perdue a0 = new Ligne_perdue
                            {
                                ligne = balise_0_ligne,
                                texte = dataGEDCOM[balise_0_ligne]
                            };
                            groupe_ligne.Add(a0);
                            Ligne_perdue a1 = new Ligne_perdue();
                            {
                                a1.ligne = balise_1_ligne;
                                a1.texte = dataGEDCOM[balise_1_ligne];
                            }
                            groupe_ligne.Add(a1);
                            ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                            groupe_ligne.Clear();
                            break;
                    }
                }
            }
            catch (Exception msg)
            {
                GC.Collect();
                string message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                Voir_message(message, msg.Message, GH.GH.erreur);
                if (GH.GH.annuler) return (N0_ID_STRUCTURE, ligne, ligne_perdue_liste);
            }
            if (!GH.GH.annuler)
            {
                liste_NOTE_STRUCTURE.Add(new NOTE_STRUCTURE()
                {
                    N0_ID_STRUCTURE = N0_ID_STRUCTURE,
                    N0_ID_RECORD = N0_ID_RECORD,
                    N0_texte = N0_texte,
                    N1_SOUR_citation_liste_ID = N1_SOUR_citation_liste_ID,
                    N1_SOUR_source_liste_ID = N1_SOUR_source_liste_ID,
                    N1_NOTE_STRUCTURE_liste_ID = N1_NOTE_STRUCTURE_liste_ID,
                });
            }
            //R..Z("Retourne" + "<br>N0_ID_STRUCTURE=" + N0_ID_STRUCTURE + "<br>ligne=" + ligne);
            return (N0_ID_STRUCTURE, ligne, ligne_perdue_liste);
        }

        private static (SOURCE_REPOSITORY_CITATION, int) Extraire_SOURCE_REPOSITORY_CITATION(
            int ligne,
            List<Ligne_perdue> ligne_perdue_liste)
        {
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            SOURCE_REPOSITORY_CITATION info = new SOURCE_REPOSITORY_CITATION();
            int niveau;
            info.N0_ID = Extraire_ID(dataGEDCOM[ligne]);
            (niveau, info.N0_ID, _, _, ligne) = Extraire_info_niveau_0(ligne);
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            info.N1_ADDR_liste = new List<ADDRESS_STRUCTURE>();
            info.N1_NOTE_STRUCTURE_liste_ID = new List<string>();
            info.N1_PHON_liste = new List<string>();
            info.N1_EMAIL_liste = new List<string>();
            info.N1_FAX_liste = new List<string>();
            info.N1_WWW_liste = new List<string>();
            string temp;
            while (Extraire_niveau(ligne) > niveau)
            {
                string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                int balise_1_ligne = ligne;
                switch (balise_1)
                {
                    case "NAME":                                                                        // NAME
                        (info.N1_NAME, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "CNTC":                                                                        // CNTC
                        (info.N1_CNTC, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "SITE":                                                                        // SITE html.cs ?
                        (info.N1_SITE, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "ADDR":                                                                        // ADDR
                        ADDRESS_STRUCTURE N1_ADDR;
                        (N1_ADDR, ligne, ligne_perdue_liste) = Extraire_ADDRESS_STRUCTURE(ligne, ligne_perdue_liste);
                        info.N1_ADDR_liste.Add(N1_ADDR);
                        break;
                    case "PHON":                                                                            // PHON html.cs V
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        info.N1_PHON_liste.Add(temp);
                        break;
                    case "FAX":                                                                         // FAX
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        info.N1_FAX_liste.Add(temp);
                        break;
                    case "EMAIL":                                                                       // EMAIL
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        info.N1_EMAIL_liste.Add(temp);
                        break;
                    case "_EMAIL":                                                                      // _EMAIL
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        info.N1_EMAIL_liste.Add(temp);
                        break;
                    case "WWW":                                                                         // WWW
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        info.N1_WWW_liste.Add(temp);
                        break;
                    case "_WWW":                                                                        // _WWW
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        info.N1_WWW_liste.Add(temp);
                        break;
                    case "_URL":                                                                        // _URL html.cs A
                        (temp, ligne) = Extraire_texte_niveau_plus(ligne);
                        info.N1_WWW_liste.Add(temp);
                        break;
                    case "MEDI":                                                                        //  MEDI
                        (info.N1_MEDI, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "CALN":                                                                        // CALN
                        (info.N1_CALN, ligne) = Extraire_texte_niveau_plus(ligne);
                        while (Extraire_niveau(ligne) > niveau + 1)
                        {
                            string balise_2 = Avoir_balise(dataGEDCOM[ligne]);
                            int balise_2_ligne = ligne;
                            switch (balise_2)
                            {
                                case "ITEM":                                                            //  ITEM
                                    (info.N2_CALN_ITEM, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                case "SHEE":                                                            // SHEE
                                    (info.N2_CALN_SHEE, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                case "PAGE":                                                            // PAGE
                                    (info.N2_CALN_PAGE, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                case "MEDI":                                                            // MIDI html.cs
                                    (info.N2_CALN_MEDI, ligne) = Extraire_texte_niveau_plus(ligne);
                                    break;
                                default:
                                    // pour la page HTML
                                    Ligne_perdue aa = new Ligne_perdue()
                                    {
                                        ligne = ligne,
                                        texte = dataGEDCOM[ligne]
                                    };
                                    ligne_perdue_liste.Add(aa);
                                    // rapport Balise
                                    method = MethodBase.GetCurrentMethod().Name;
                                    lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                                    Ligne_perdue a0 = new Ligne_perdue()
                                    {
                                        ligne = balise_0_ligne,
                                        texte = dataGEDCOM[balise_0_ligne]
                                    };
                                    groupe_ligne.Add(a0);
                                    Ligne_perdue a1 = new Ligne_perdue();
                                    {
                                        a1.ligne = balise_1_ligne;
                                        a1.texte = dataGEDCOM[balise_1_ligne];
                                    }
                                    groupe_ligne.Add(a1);
                                    Ligne_perdue a2 = new Ligne_perdue();
                                    {
                                        a2.ligne = balise_2_ligne;
                                        a2.texte = dataGEDCOM[balise_2_ligne];
                                    }
                                    groupe_ligne.Add(a2);
                                    ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                                    groupe_ligne.Clear();
                                    break;
                            }
                        }
                        break;
                    case "REFN":                                                                        // REFN
                        (info.N1_REFN, ligne) = Extraire_texte_niveau_plus(ligne);
                        break;
                    case "NOTE":                                                                        // +1 NOTE
                        string IDNote;
                        (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                        info.N1_NOTE_STRUCTURE_liste_ID.Add(IDNote);
                        break;
                    default:
                        // pour la page HTML
                        Ligne_perdue bb = new Ligne_perdue()
                        {
                            ligne = ligne,
                            texte = dataGEDCOM[ligne]
                        };
                        ligne_perdue_liste.Add(bb);
                        // rapport Balise
                        method = MethodBase.GetCurrentMethod().Name;
                        lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                        Ligne_perdue b0 = new Ligne_perdue()
                        {
                            ligne = balise_0_ligne,
                            texte = dataGEDCOM[balise_0_ligne]
                        };
                        groupe_ligne.Add(b0);
                        Ligne_perdue b1 = new Ligne_perdue();
                        {
                            b1.ligne = balise_1_ligne;
                            b1.texte = dataGEDCOM[balise_1_ligne];
                        }
                        groupe_ligne.Add(b1);
                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                        groupe_ligne.Clear();
                        break;
                }
            }
            return (info, ligne);
        }

        private static (SPOUSE_TO_FAMILY_LINK, int, List<Ligne_perdue>) Extraire_SPOUSE_TO_FAMILY_LINK(
            int ligne,
            List<Ligne_perdue> ligne_perdue_liste)
        {
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            //string balise_0 = Avoir_balise(dataGEDCOM[ligne]);
            int balise_0_ligne = ligne;
            SPOUSE_TO_FAMILY_LINK info = new SPOUSE_TO_FAMILY_LINK();
            int niveau = Extraire_niveau(ligne);
            List<string> N1_NOTE_STRUCTURE_liste_ID = new List<string>();
            info.N0_ID = Extraire_ID(dataGEDCOM[ligne]);
            ligne++;
            while (Extraire_niveau(ligne) > niveau)
            {
                string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                int balise_1_ligne = ligne;
                switch (balise_1)
                {
                    case "NOTE":                                                                        // NOTE
                        string IDNote;
                        (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                        N1_NOTE_STRUCTURE_liste_ID.Add(IDNote);
                        break;
                    default:
                        // pour la page HTML
                        Ligne_perdue aa = new Ligne_perdue()
                        {
                            ligne = ligne,
                            texte = dataGEDCOM[ligne]
                        };
                        ligne_perdue_liste.Add(aa);
                        // rapport Balise
                        string method = MethodBase.GetCurrentMethod().Name;
                        int lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                        Ligne_perdue a0 = new Ligne_perdue()
                        {
                            ligne = balise_0_ligne,
                            texte = dataGEDCOM[balise_0_ligne]
                        };
                        groupe_ligne.Add(a0);
                        Ligne_perdue a1 = new Ligne_perdue();
                        {
                            a1.ligne = balise_1_ligne;
                            a1.texte = dataGEDCOM[balise_1_ligne];
                        }
                        groupe_ligne.Add(a1);
                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                        groupe_ligne.Clear();
                        break;
                }
            }
            info.N1_NOTE_STRUCTURE_liste_ID = N1_NOTE_STRUCTURE_liste_ID;
            return (info, ligne, ligne_perdue_liste);
        }

        private static int Extraire_SUBMISSION_RECORD(int ligne)
        {
            Regler_code_erreur();
            info_SUBMISSION_RECORD.N0_ID = null;
            info_SUBMISSION_RECORD.N1_SUBM_liste_ID = new List<string>();
            info_SUBMISSION_RECORD.N1_FAMF = null;
            info_SUBMISSION_RECORD.N1_TEMP = null;
            info_SUBMISSION_RECORD.N1_ANCE = null;
            info_SUBMISSION_RECORD.N1_DESC = null;
            info_SUBMISSION_RECORD.N1_ORDI = null;
            info_SUBMISSION_RECORD.N1_RIN = null;
            info_SUBMISSION_RECORD.N1_NOTE_STRUCTURE_liste_ID = null;
            info_SUBMISSION_RECORD.N1_CHAN = null;
            info_SUBMISSION_RECORD.N0_ID = Extraire_ID(dataGEDCOM[ligne]);
            List<Ligne_perdue> ligne_perdue_liste = new List<Ligne_perdue>();
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            List<string> listeNoteID = new List<string>();
            //string balise_0 = Avoir_balise(dataGEDCOM[ligne]);
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            ligne++;
            while (Extraire_niveau(ligne) > 0)
            {
                try
                {
                    string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                    int balise_1_ligne = ligne;
                    switch (balise_1)
                    {
                        case "SUBM":                                                                    // SUBM
                            info_SUBMISSION_RECORD.N1_SUBM_liste_ID.Add(Extraire_ID(dataGEDCOM[ligne]));
                            ligne++;
                            break;
                        case "FAMF":                                                                    // FAMF
                            info_SUBMISSION_RECORD.N1_FAMF = Extraire_texte_balise(4, dataGEDCOM[ligne]);
                            ligne++;
                            break;
                        case "TEMP":                                                                    // TEMP
                            info_SUBMISSION_RECORD.N1_TEMP = Extraire_texte_balise(4, dataGEDCOM[ligne]);
                            ligne++;
                            break;
                        case "ANCE":                                                                    // ANCE
                            info_SUBMISSION_RECORD.N1_ANCE = Extraire_texte_balise(4, dataGEDCOM[ligne]);
                            ligne++;
                            break;
                        case "DESC":                                                                    // DESC
                            info_SUBMISSION_RECORD.N1_DESC = Extraire_texte_balise(4, dataGEDCOM[ligne]);
                            ligne++;
                            break;
                        case "ORDI":                                                                    // ORDI
                            info_SUBMISSION_RECORD.N1_ORDI = Extraire_texte_balise(4, dataGEDCOM[ligne]).ToUpper();
                            ligne++;
                            break;
                        case "RIN":                                                                     // RIN
                            info_SUBMISSION_RECORD.N1_RIN = Extraire_texte_balise(3, dataGEDCOM[ligne]);
                            ligne++;
                            break;
                        case "NOTE":                                                                    // NOTE
                            string IDNote;
                            (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                            listeNoteID.Add(IDNote);
                            break;
                        case "CHAN":                                                                    // CHAN
                            (info_SUBMISSION_RECORD.N1_CHAN, ligne, _) = Extraire_CHANGE_DATE(ligne, null);
                            break;
                        default:
                            // pour la page HTML
                            Ligne_perdue aa = new Ligne_perdue()
                            {
                                ligne = ligne,
                                texte = dataGEDCOM[ligne]
                            };
                            ligne_perdue_liste.Add(aa);
                            // rapport Balise
                            method = MethodBase.GetCurrentMethod().Name;
                            lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                            Ligne_perdue a0 = new Ligne_perdue()
                            {
                                ligne = balise_0_ligne,
                                texte = dataGEDCOM[balise_0_ligne]
                            };
                            groupe_ligne.Add(a0);
                            Ligne_perdue a1 = new Ligne_perdue();
                            {
                                a1.ligne = balise_1_ligne;
                                a1.texte = dataGEDCOM[balise_1_ligne];
                            }
                            groupe_ligne.Add(a1);
                            ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                            groupe_ligne.Clear();
                            break;
                    }
                }
                catch (Exception msg)
                {
                    GC.Collect();
                    string message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                    Voir_message(message, msg.Message, GH.GH.erreur);
                }
            }
            info_SUBMISSION_RECORD.ligne_perdue_liste = ligne_perdue_liste;
            info_SUBMISSION_RECORD.N1_NOTE_STRUCTURE_liste_ID = listeNoteID;
            return (ligne);
        }

        private static int Extraire_SUBMITTER_RECORD(
            int ligne
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //ZXXCV("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b>");
            Regler_code_erreur();
            string N0_ID;
            //string N1_NAME = null;
            List<Ligne_perdue> ligne_perdue_liste = new List<Ligne_perdue>();
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            List<PERSONAL_NAME_STRUCTURE> N1_NAME_liste = new List<PERSONAL_NAME_STRUCTURE>();
            string N1_SITE = null; // v5.3
            List<ADDRESS_STRUCTURE> N1_ADDR_liste = new List<ADDRESS_STRUCTURE>();
            List<string> N1_PHON_liste = new List<string>();
            List<string> N1_FAX_liste = new List<string>();
            List<string> N1_EMAIL_liste = new List<string>();
            List<string> N1_WWW_liste = new List<string>();
            List<string> MULTIMEDIA_LINK_liste_ID = new List<string>();
            string N1_LANG = null;
            string N1_RIN = null;
            string N1_RFN = null;
            List<string> N1_NOTE_STRUCTURE_liste_ID = new List<string>();
            CHANGE_DATE N1_CHAN = new CHANGE_DATE
            {
                N1_CHAN_DATE = null,
                N2_CHAN_DATE_TIME = null
            };
            N0_ID = Extraire_ID(dataGEDCOM[ligne]);
            int balise_0_ligne = ligne;
            string method;
            int lineNumber;
            ligne++;
            try
            {
                while (Extraire_niveau(ligne) > 0)
                {
                    string balise_1 = Avoir_2_balise(dataGEDCOM[ligne]);
                    int balise_1_ligne = ligne;
                    switch (balise_1)
                    {
                        case "1 NAME":                                                                    // NAME
                            PERSONAL_NAME_STRUCTURE info_nom = new PERSONAL_NAME_STRUCTURE();
                            (info_nom, ligne, ligne_perdue_liste) = Extraire_PERSONAL_NAME_STRUCTURE(ligne, ligne_perdue_liste);
                            N1_NAME_liste.Add(info_nom);
                            break;
                        case "1 SITE":                                                                    // SITE V5.3
                            (N1_SITE, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "1 ADDR":                                                                    // ADDR
                            GEDCOMClass.ADDRESS_STRUCTURE N1_ADDR;
                            (N1_ADDR, ligne, ligne_perdue_liste) = Extraire_ADDRESS_STRUCTURE(ligne, ligne_perdue_liste);
                            N1_ADDR_liste.Add(N1_ADDR);
                            break;
                        case "1 PHON":                                                                    // PHON
                            string a;
                            (a, ligne) = Extraire_texte_niveau_plus(ligne);
                            N1_PHON_liste.Add(a);
                            break;
                        case "1 FAX":                                                                     // FAX
                            string b;
                            (b, ligne) = Extraire_texte_niveau_plus(ligne);
                            N1_FAX_liste.Add(b);
                            break;
                        case "1 EMAIL":                                                                   // EMAIL
                        case "1 _EMAIL":                                                                  // _EMAIL
                            string d;
                            (d, ligne) = Extraire_texte_niveau_plus(ligne);
                            N1_EMAIL_liste.Add(d);
                            break;
                        case "1 WWW":                                                                     // WWW
                        case "1_WWW":                                                                    // _WWW
                        case "1_URL":                                                                    // _URL 
                            string g;
                            (g, ligne) = Extraire_texte_niveau_plus(ligne);
                            N1_WWW_liste.Add(g);
                            break;
                        case "1 LANG":                                                                    // LANG
                            (N1_LANG, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "1 NOTE":                                                                    // NOTE
                            string IDNote;
                            (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                            N1_NOTE_STRUCTURE_liste_ID.Add(IDNote);
                            break;
                        case "1 OBJE":                                                                    // OBJE
                            string temp;
                            (temp, ligne, ligne_perdue_liste) = Extraire_MULTIMEDIA_LINK(ligne, ligne_perdue_liste);
                            MULTIMEDIA_LINK_liste_ID.Add(temp);
                            break;
                        case "1 AUDIO":                                                                   // AUDIO V5.3
                            string audio;
                            (audio, ligne) = Extraire_texte_niveau_plus(ligne);
                            MULTIMEDIA_LINK_liste_ID.Add(audio);
                            break;
                        case "1 PHOTO":                                                                   // PHOTO V5.3
                            string photo;
                            (photo, ligne) = Extraire_texte_niveau_plus(ligne);
                            MULTIMEDIA_LINK_liste_ID.Add(photo);
                            break;
                        case "1 VIDEO":                                                                   // VIDEO V5.3
                            string video;
                            (video, ligne) = Extraire_texte_niveau_plus(ligne);
                            MULTIMEDIA_LINK_liste_ID.Add(video);
                            break;
                        case "1 CHAN":                                                                    // CHAN
                            (N1_CHAN, ligne, ligne_perdue_liste) = Extraire_CHANGE_DATE(ligne, ligne_perdue_liste);
                            break;
                        case "1 RIN":                                                                     // RIN
                            (N1_RIN, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        case "1 RFN":                                                                     // RFN
                            (N1_RFN, ligne) = Extraire_texte_niveau_plus(ligne);
                            break;
                        default:
                            // pour la page HTML
                            Ligne_perdue aa = new Ligne_perdue()
                            {
                                ligne = ligne,
                                texte = dataGEDCOM[ligne]
                            };
                            ligne_perdue_liste.Add(aa);
                            // rapport Balise
                            method = MethodBase.GetCurrentMethod().Name;
                            lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                            Ligne_perdue a0 = new Ligne_perdue()
                            {
                                ligne = balise_0_ligne,
                                texte = dataGEDCOM[balise_0_ligne]
                            };
                            groupe_ligne.Add(a0);
                            Ligne_perdue a1 = new Ligne_perdue();
                            {
                                a1.ligne = balise_1_ligne;
                                a1.texte = dataGEDCOM[balise_1_ligne];
                            }
                            groupe_ligne.Add(a1);
                            ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                            groupe_ligne.Clear();
                            break;
                    }
                }
            }
            catch (Exception msg)
            {
                GC.Collect();
                string message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                Voir_message(message, msg.Message, GH.GH.erreur);
            }
            if (!GH.GH.annuler)
            {
                liste_SUBMITTER_RECORD.Add(new SUBMITTER_RECORD()
                {
                    ligne_perdue_liste = ligne_perdue_liste,
                    N0_ID = N0_ID,
                    N1_NAME_liste = N1_NAME_liste,
                    N1_SITE = N1_SITE,
                    N1_ADDR_liste = N1_ADDR_liste,
                    N1_PHON_liste = N1_PHON_liste,
                    N1_FAX_liste = N1_FAX_liste,
                    N1_EMAIL_liste = N1_EMAIL_liste,
                    N1_WWW_liste = N1_WWW_liste,
                    N1_LANG = N1_LANG,
                    //N1_OBJE_LINK_liste_ID = N1_OBJE_LINK_liste_ID,
                    //N1_AUDIO_liste = N1_AUDIO_liste,
                    //N1_VIDEO_liste = N1_VIDEO_liste,
                    //N1_PHOTO_liste = N1_PHOTO_liste,
                    MULTIMEDIA_LINK_liste_ID = MULTIMEDIA_LINK_liste_ID,
                    N1_RIN = N1_RIN,
                    N1_RFN = N1_RFN,
                    N1_NOTE_STRUCTURE_liste_ID = N1_NOTE_STRUCTURE_liste_ID,
                    N1_CHAN = N1_CHAN,
                });
            }
            return (ligne);
        }

        private static (TEXT_STRUCTURE, int, List<Ligne_perdue>) Extraire_TEXT_STRUCTURE(
            int ligne,
            List<Ligne_perdue> ligne_perdue_liste)
        {
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            TEXT_STRUCTURE info_texte = new TEXT_STRUCTURE();
            List<string> N1_NOTE_STRUCTURE_liste_ID = new List<string>();
            int niveau = Extraire_niveau(ligne);
            (info_texte.N0_TEXT, ligne) = Extraire_texte_niveau_plus(ligne);
            int balise_0_ligne = ligne;
            //ligne++;
            while (Extraire_niveau(ligne) > niveau)
            {
                string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                int balise_1_ligne = ligne;
                switch (balise_1)
                {
                    case "NOTE":                                                                        // +1 NOTE
                        string IDNote;
                        (IDNote, ligne, ligne_perdue_liste) = Extraire_NOTE_STRUCTURE(ligne, ligne_perdue_liste);
                        N1_NOTE_STRUCTURE_liste_ID.Add(IDNote);
                        break;
                    default:
                        // pour la page HTML
                        Ligne_perdue aa = new Ligne_perdue()
                        {
                            ligne = ligne,
                            texte = dataGEDCOM[ligne]
                        };
                        ligne_perdue_liste.Add(aa);
                        // rapport Balise
                        string method = MethodBase.GetCurrentMethod().Name;
                        int lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                        Ligne_perdue a0 = new Ligne_perdue()
                        {
                            ligne = balise_0_ligne,
                            texte = dataGEDCOM[balise_0_ligne]
                        };
                        groupe_ligne.Add(a0);
                        Ligne_perdue a1 = new Ligne_perdue();
                        {
                            a1.ligne = balise_1_ligne;
                            a1.texte = dataGEDCOM[balise_1_ligne];
                        }
                        groupe_ligne.Add(a1);
                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                        groupe_ligne.Clear();
                        break;
                }
            }
            info_texte.N1_NOTE_STRUCTURE_liste_ID = N1_NOTE_STRUCTURE_liste_ID;
            return (info_texte, ligne, ligne_perdue_liste);
        }

        private static (string, int) Extraire_texte_niveau_plus(
    int ligne
    //, [CallerLineNumber] int callerLineNumber = 0
    )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b> GEDCOM=" + ligne);
            Regler_code_erreur();
            string texte = Extraire_textuel(ligne);
            ligne++;
            int niveau = Extraire_niveau(ligne);
            string balise = Avoir_2_balise(dataGEDCOM[ligne]);
            while (balise == (niveau).ToString() + " CONT" || balise == (niveau).ToString() + " CONC")
            {
                if (balise == (niveau).ToString() + " CONT")
                {
                    texte += System.Environment.NewLine + Extraire_textuel(ligne);
                }
                if (balise == (niveau).ToString() + " CONC")
                {
                    texte += " " + Extraire_textuel(ligne);
                }
                ligne++;
                balise = Avoir_2_balise(dataGEDCOM[ligne]);
            }
            //R..Z("B retourne " + texte + " ligne=" + ligne);
            return (texte, ligne);
        }
        private static (string, int) Extraire_texte_niveau_plusxxx(
            int ligne
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //ZXXCV("====>De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b> GEDCOM=" + ligne);
            Regler_code_erreur();
            string texte;
            texte = Extraire_textuel(ligne);
            int niveau1 = Extraire_niveau(ligne);
            ligne++;
            string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
            int niveau2= Extraire_niveau(ligne);
            if (niveau2 <= niveau1)
            {
                return (texte, ligne);
            }
            while (balise_1 == "CONT" || balise_1 == "CONC")
            {
                balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                switch (balise_1)
                {
                    case "CONT":                                                                        // CONT
                        texte += System.Environment.NewLine + Extraire_textuel(ligne);
                        ligne++;
                        break;
                    case "CONC":                                                                        // CONC
                        texte += " " + Extraire_textuel(ligne);
                        ligne++;
                        break;
                }
            }
            return (texte, ligne);
        }

        private static string Extraire_textuel(int numero_ligne)
        {
            string texte = dataGEDCOM[numero_ligne];
            texte = texte.TrimStart();
            texte = texte.TrimEnd();
            // retire les espace d'extra
            string texte2 = texte[0].ToString();
            for (int f = 1; f < texte.Count(); f++)
            {
                if (texte[f] == ' ' && texte[f - 1] == ' ')
                {
                    texte2 += "#A#A#A#";
                }
                else texte2 += texte[f].ToString();
            }
            char[] espace = { ' ' };
            string[] section = texte2.Split(espace);
            int nombre_section = section.Length;
            if (nombre_section < 3) return null;
            texte2 = texte.Substring(section[0].Length + section[1].Length + 2);
            texte2 = Retirer_marque(texte2);
            return texte2;
        }

        private static (USER_REFERENCE_NUMBER, int, List<Ligne_perdue>) Extraire_USER_REFERENCE_NUMBER(
            int ligne,
            List<Ligne_perdue> ligne_perdue_liste)
        {
            List<Ligne_perdue> groupe_ligne = new List<Ligne_perdue>();
            int niveau = Extraire_niveau(ligne);
            //string balise_0 = Avoir_balise(dataGEDCOM[ligne]);
            int balise_0_ligne = ligne;
            USER_REFERENCE_NUMBER info = new USER_REFERENCE_NUMBER
            {
                N1_TYPE = null,
                N0_REFN = Extraire_texte(ligne),
            };
            info.N0_REFN = Extraire_texte(ligne);
            ligne++;

            while (Extraire_niveau(ligne) > niveau)
            {
                string balise_1 = Avoir_balise(dataGEDCOM[ligne]);
                int balise_1_ligne = ligne;
                switch (balise_1)
                {
                    case "TYPE":
                        info.N1_TYPE += Extraire_texte(ligne);
                        ligne++;
                        break;
                    default:
                        // pour la page HTML
                        Ligne_perdue aa = new Ligne_perdue()
                        {
                            ligne = ligne,
                            texte = dataGEDCOM[ligne]
                        };
                        ligne_perdue_liste.Add(aa);
                        // rapport Balise
                        string method = MethodBase.GetCurrentMethod().Name;
                        int lineNumber = (new System.Diagnostics.StackFrame(0, true)).GetFileLineNumber();
                        Ligne_perdue a0 = new Ligne_perdue()
                        {
                            ligne = balise_0_ligne,
                            texte = dataGEDCOM[balise_0_ligne]
                        };
                        groupe_ligne.Add(a0);
                        Ligne_perdue a1 = new Ligne_perdue();
                        {
                            a1.ligne = balise_1_ligne;
                            a1.texte = dataGEDCOM[balise_1_ligne];
                        }
                        groupe_ligne.Add(a1);
                        ligne = Ecrire_balise(ligne, method, lineNumber, groupe_ligne);
                        groupe_ligne.Clear();
                        break;
                }
            }
            return (info, ligne, ligne_perdue_liste);
        }

        public static bool IsNotNullOrEmpty(string s
        //, [CallerLineNumber] int callerLineNumber = 0
        )
        {
            // retourne true si pas vide ou vide ou null
            if (s == null)
            {
                return false;
            }
            s = s.Trim();
            if (s == null)
            {
                return false;
            }
            return true;
        }

        public static bool IsNotNullOrEmpty(List<string> list
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            if (list == null) return false;
            foreach (string a in list)
            {
                if (!String.IsNullOrWhiteSpace(a))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsNotNullOrEmpty<T>(
            List<T> list
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            // verifie si une liste est null ou vide
            // retourne true si n'est pas vide ou null
            if (list == null) return false;
            if (list.Count == 0) return false;
            return list.Any();
        }

        public static bool IsNullOrEmpty(string s)
        {
            // verifie si une liste est null ou vide
            // retourne true si vide ou null
            if (s == null)
            {
                return true;
            }
            return !s.Any();
        }

        public static bool IsNullOrEmpty<T>(List<T> list)
        {
            // verifie si une liste est null ou vide
            // retourne true si vide ou null
            if (list == null)
            {
                return true;
            }
            return !list.Any();
        }

        public static string Lire_entete_GEDCOM(string fichier
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b> GEDCOM=");

            try
            {
                info_HEADER.N1_CHAR = null;
                info_HEADER.N2_GEDC_FORM = null;
                ligne = 0;
                info_HEADER.N1_CHAR = null;
                List<string> entete = new List<string>();
                info_HEADER.N2_GEDC_FORM = "";
                System.IO.StreamReader fichierCodage = new System.IO.StreamReader(@fichier);
                entete.Add(fichierCodage.ReadLine().TrimStart());
                if (entete[0].Substring(0, 6) != "0 HEAD")
                {
                    MessageBox.Show("Le fichier ne semble pas être un fichier au format GEDCOM.\r\n\r\n" + "\r\n\r\n", "GEDCOM ?",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                    return null;
                }
                string text;
                do
                {
                    text = fichierCodage.ReadLine().TrimStart();
                    entete.Add(text);
                } while (text[0].ToString() != "0");
                entete.Add("0 TRLR");
                do
                {
                    if (Avoir_balise_p1(entete[ligne]) == "0 HEAD")                                     // 0 HEAD
                    {
                        ligne++;
                        while ((int)Char.GetNumericValue(entete[ligne][0]) > 0)
                        {
                            if (Avoir_balise_p1(entete[ligne]) == "1 CHAR")                             // 1 CHAR
                            {
                                info_HEADER.N1_CHAR = Extraire_ligne(entete[ligne], 4);
                                ligne++;

                                while ((int)Char.GetNumericValue(entete[ligne][0]) > 1)
                                {
                                    ligne++;
                                }
                            }
                            else if (Avoir_balise_p1(entete[ligne]) == "1 GEDC")                        // 1 GEDC
                            {
                                ligne++;
                                while ((int)Char.GetNumericValue(entete[ligne][0]) > 1)
                                {
                                    if (Avoir_balise_p1(entete[ligne]) == "2 FORM")                     // 2 FORM
                                    {
                                        info_HEADER.N2_GEDC_FORM = Extraire_ligne(entete[ligne], 4);
                                        ligne++;
                                    }
                                    else
                                    {
                                        ligne++;
                                    }
                                }
                            }
                            else
                            {
                                ligne++;
                            }
                        }
                    }
                    ligne++;
                }
                while (!entete[ligne].Contains("0 TRLR"));
                fichierCodage.Close();
                if (info_HEADER.N1_CHAR == "IBMPC")
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
                    info_HEADER.N1_CHAR == "ANSI" ||
                    info_HEADER.N1_CHAR == "ASCII" ||
                    info_HEADER.N1_CHAR == "ANSEL"
                   )
                {
                    string utf8String = "";
                    if (
                        info_HEADER.N1_CHAR == "ANSI" ||
                        info_HEADER.N1_CHAR == "ASCII" ||
                        info_HEADER.N1_CHAR == "ANSEL"
                       )
                    {
                        byte[] ansiBytes = File.ReadAllBytes(fichier);
                        utf8String = Encoding.Default.GetString(ansiBytes);
                    }
                    string sansExtention = Path.GetFileNameWithoutExtension(fichier);
                    fichier = Path.GetTempPath() + "UTF8-" + sansExtention + ".gedCopie";
                    File.WriteAllText(fichier, utf8String);
                }
                return fichier;
            }
            catch (Exception msg)
            {
                GC.Collect();
                string message;
                if (ligne > 0)
                {
                    message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                    Voir_message(message, msg.Message, GH.GH.erreur);
                    return null;
                }
                message = "Erreur dans la lecture du fichier GEDCOM.";
                Voir_message(message, msg.Message, GH.GH.erreur);
                return null;
            }
        }
        public static bool Lire_GEDCOM(
            string fichier
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b> fichier=" + fichier );
            try
            {
                Application .DoEvents();
                dataGEDCOM = new List<string>();
                dataGEDCOM.Clear();
                string text;
                long position = 0;
                System.IO.StreamReader file = new System.IO.StreamReader(@fichier);
                ligne = 0;
                dataGEDCOM.Add("");
                text = file.ReadLine().TrimStart();
                dataGEDCOM.Add(text);
                info_HEADER.Nom_fichier_disque = fichier;
                while ((text = file.ReadLine().TrimStart()) != null)
                {
                    if (GH.GH.annuler == true) return false;
                    position += text.Length;
                    if (IsNotNullOrEmpty(info_HEADER.N1_CHAR))
                        if (info_HEADER.N1_CHAR.ToLower() == "ansel") text = Convertir_ANSEL(text);
                    if (text == "") text = "99 ligne vide";
                    dataGEDCOM.Add(text);
                    if (text.Contains("0 TRLR") || file.EndOfStream)
                    {
                        file.Close();
                        break;
                    }
                    ligne++;
                    if (ligne % 1000 == 0) Application.DoEvents();
                }
                file.Close();
                // effacer fichier  si extention .gedCopie
                if (Path.GetExtension(fichier) == ".gedCopie")
                {
                    if (File.Exists(@fichier))
                    {
                        File.Delete(@fichier);
                    }
                }
                Application.DoEvents();
                return true;
            }
            catch (Exception msg)
            {
                GC.Collect();
                string message;
                if (ligne > 0)
                {
                    message = "Problème en lisant la ligne " + (ligne + 1).ToString() + " du fichier GEDCOM.";
                    Voir_message(message, msg.Message, GH.GH.erreur);
                    return false;
                }
                message = "Erreur dans la lecture du fichier GEDCOM.";
                Voir_message(message, msg.Message, GH.GH.erreur);
                return false;
            }
        }
        public static string Avoir_IDAdoption(string ID)
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
        public static HEADER Avoir_info_GEDCOM()
        {
            return info_HEADER;
        }
        public static SUBMITTER_RECORD Avoir_info_SUBMITTER_RECORD(string ID)
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
        public static SUBMISSION_RECORD Avoir_info_SUBMISSION_RECORD()
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
        public static EVEN_ATTRIBUTE_STRUCTURE Avoir_attribute_nombre_enfant(List<EVEN_ATTRIBUTE_STRUCTURE> liste)
        {
            foreach (EVEN_ATTRIBUTE_STRUCTURE info in liste)
            {
                if (info.N1_EVEN == "NCHI")
                    return info;
            }
            EVEN_ATTRIBUTE_STRUCTURE r = new EVEN_ATTRIBUTE_STRUCTURE();
            return r;
        }
        public static EVEN_ATTRIBUTE_STRUCTURE Avoir_attribute_nombre_mariage(List<EVEN_ATTRIBUTE_STRUCTURE> liste)
        {
            foreach (EVEN_ATTRIBUTE_STRUCTURE info in liste)
            {
                if (info.N1_EVEN == "NMR")
                    return info;
            }
            EVEN_ATTRIBUTE_STRUCTURE r = new EVEN_ATTRIBUTE_STRUCTURE();
            return r;
        }
        public static (bool, EVEN_ATTRIBUTE_STRUCTURE) Avoir_evenement_deces(List<EVEN_ATTRIBUTE_STRUCTURE> liste)
        {
            EVEN_ATTRIBUTE_STRUCTURE retourNull = new EVEN_ATTRIBUTE_STRUCTURE();
            if (IsNullOrEmpty(liste)) return (false, retourNull);
            foreach (EVEN_ATTRIBUTE_STRUCTURE info in liste)
            {
                if (info.N1_EVEN == "DEAT")
                    return (true, info);
            }

            return (false, retourNull);
        }
        public static EVEN_ATTRIBUTE_STRUCTURE AvoirEvenementMariage(List<EVEN_ATTRIBUTE_STRUCTURE> liste)
        {
            EVEN_ATTRIBUTE_STRUCTURE r = new EVEN_ATTRIBUTE_STRUCTURE();
            if (IsNullOrEmpty(liste)) return r;
            foreach (EVEN_ATTRIBUTE_STRUCTURE info in liste)
            {
                if (info.N1_EVEN == "MARR")
                {
                    return info;
                }
            }
            return r;
        }
        public static (bool, EVEN_ATTRIBUTE_STRUCTURE) Avoir_evenement_naissance(List<EVEN_ATTRIBUTE_STRUCTURE> liste)
        {

            EVEN_ATTRIBUTE_STRUCTURE retourNull = new EVEN_ATTRIBUTE_STRUCTURE();
            if (IsNullOrEmpty(liste)) return (false, retourNull);
            foreach (EVEN_ATTRIBUTE_STRUCTURE info in liste)
            {
                if (info.N1_EVEN == "BIRT")
                    return (true, info);
            }
            return (false, retourNull);
        }
        public static (bool, EVEN_STRUCTURE_53) AvoirEvenementNaissance_53(
            List<EVEN_STRUCTURE_53> liste)
        {

            EVEN_STRUCTURE_53 retourNull = new EVEN_STRUCTURE_53();
            if (IsNullOrEmpty(liste)) return (false, retourNull);
            foreach (EVEN_STRUCTURE_53 info in liste)
            {
                if (info.N0_EVEN == "BIRT")
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
        public static List<string> Avoir_liste_ID_famille()
        {
            List<string> ListeID = new List<string>();
            int numero = 0;
            foreach (FAM_RECORD info in liste_FAM_RECORD)
            {
                ListeID.Add(info.N0_ID);
                numero++;
                if (numero % 1000 == 0) Application.DoEvents();

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
        public static (string, string, string, string, string) Avoir_nom_naissance_deces(string ID)
        {
            (_, INDIVIDUAL_RECORD info_individu) = Avoir_info_individu(ID);
            (_, EVEN_ATTRIBUTE_STRUCTURE naissance) = Avoir_evenement_naissance(info_individu.N1_EVEN_Liste);
            (_, EVEN_ATTRIBUTE_STRUCTURE deces) = Avoir_evenement_deces(info_individu.N1_EVEN_Liste);
            string lieu_naissance;
            string lieu_deces;
            if (naissance.N2_PLAC != null) lieu_naissance = naissance.N2_PLAC.N0_PLAC; else lieu_naissance = null;
            if (deces.N2_PLAC != null) lieu_deces = deces.N2_PLAC.N0_PLAC; else lieu_deces = null;
            return (
                Avoir_premier_nom_individu(ID),
                naissance.N2_DATE,
                lieu_naissance,
                deces.N2_DATE,
                lieu_deces);
        }
        public static SOURCE_RECORD Avoir_info_source(string ID)
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
        public static List<string> Avoir_liste_ID_individu()
        {

            List<string> ListeID = new List<string>();
            ListeID.Clear();
            int numero = 0;
            foreach (INDIVIDUAL_RECORD info in liste_INDIVIDUAL_RECORD)
            {
                ListeID.Add(info.N0_ID);
                numero++;
                if (numero % 1000 == 0) Application.DoEvents();
            }
            return ListeID;
        }

        public static REPOSITORY_RECORD Avoir_info_repo(
            string ID
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b>ID=" + ID);
            foreach (REPOSITORY_RECORD info in liste_REPOSITORY_RECORD)
            {
                if (info.N0_ID == ID)
                {
                    return info;
                }
            }
            return null;
        }
        public static EVEN_RECORD_53 Avoir_info_event_53(string ID)
        {
            foreach (EVEN_RECORD_53 info in liste_EVEN_RECORD_53)
            {
                if (info.N0_ID == ID)
                {
                    return info;
                }
            }
            return null;
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
        public static List<PERSONAL_NAME_STRUCTURE> Avoir_liste_nom_chercheur(string ID)
        {
            foreach (SUBMITTER_RECORD info in liste_SUBMITTER_RECORD)
            {
                if (info.N0_ID == ID)
                {
                    if (IsNullOrEmpty(info.N1_NAME_liste))
                    {
                        return null;
                    }
                    else if (info.N1_NAME_liste.Count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return info.N1_NAME_liste;
                    }
                }
            }
            return null;
        }

        public static List<PERSONAL_NAME_STRUCTURE> Avoir_liste_nom_individu(string ID
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b> GEDCOM=" + ligne + "&nbsp;&nbsp;ID=" + ID);

            foreach (INDIVIDUAL_RECORD info in liste_INDIVIDUAL_RECORD)
            {
                if (info.N0_ID == ID)
                {
                    if (IsNullOrEmpty(info.N1_NAME_liste))
                    {
                        return null;
                    }
                    else if (info.N1_NAME_liste.Count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return info.N1_NAME_liste;
                    }
                }
            }
            return null;
        }

        public static string AvoirPrenomPatronymeIndividu(string ID)
        {
            if (IsNullOrEmpty(ID)) return null;
            List<PERSONAL_NAME_STRUCTURE> info;
            info = Avoir_liste_nom_individu(ID);
            if (R.IsNullOrEmpty(info)) return null;
            string patronyme = info[0].N1_PERSONAL_NAME_PIECES.Nn_SURN;
            string prenom = info[0].N1_PERSONAL_NAME_PIECES.Nn_GIVN;
            if (IsNullOrEmpty(prenom) && IsNullOrEmpty(patronyme)) return null;
            if (IsNullOrEmpty(prenom)) prenom = "?";
            if (IsNullOrEmpty(patronyme)) patronyme = "?";
            return prenom + " " + patronyme;
        }

        public static string Avoir_premier_nom_chercheur(
            string ID
            , [CallerLineNumber] int callerLineNumber = 0
            )
        {
            if (IsNullOrEmpty(ID)) return null;
            bool Ok;
            List<PERSONAL_NAME_STRUCTURE> info;
            info = Avoir_liste_nom_chercheur(ID);
            if (R.IsNullOrEmpty(info)) return null;
            string patronyme = null;
            string prenom = null;
            if (info[0].N1_PERSONAL_NAME_PIECES != null)
            {
                patronyme = info[0].N1_PERSONAL_NAME_PIECES.Nn_SURN;
                prenom = info[0].N1_PERSONAL_NAME_PIECES.Nn_GIVN;
            }
            if (IsNullOrEmpty(prenom) && IsNullOrEmpty(patronyme))
            {
                return info[0].N0_NAME;
            }
            if (IsNullOrEmpty(prenom)) prenom = "?";
            if (IsNullOrEmpty(patronyme)) patronyme = "?";
            return patronyme + ", " + prenom;
        }
        public static string Avoir_premier_nom_individu(
            string ID
            //, [CallerLineNumber] int callerLineNumber = 0
            )
        {
            //R..Z("De la methode <b>" + (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name + " code " + callerLineNumber + "</b> ID=" + ID );
            if (IsNullOrEmpty(ID)) return null;
            List<PERSONAL_NAME_STRUCTURE> info;
            info = Avoir_liste_nom_individu(ID);
            if (IsNullOrEmpty(info)) return null;
            string patronyme = null;
            string prenom = null;
            if (info[0].N1_PERSONAL_NAME_PIECES != null)
            {
                patronyme = info[0].N1_PERSONAL_NAME_PIECES.Nn_SURN;
                prenom = info[0].N1_PERSONAL_NAME_PIECES.Nn_GIVN;
            }
            if (IsNullOrEmpty(prenom) && IsNullOrEmpty(patronyme))
            {
                return info[0].N0_NAME;
            }
            if (IsNullOrEmpty(prenom)) prenom = "?";
            if (IsNullOrEmpty(patronyme)) patronyme = "?";
            return patronyme + ", " + prenom;
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
            if (info_HEADER.N2_CHAR_VERS == "ANSI Z39.47-1985")
            {
                texte = texte.Replace("Ã", "©");
            }
            return texte;
        }

        private static string Retirer_espace_inutile(string s)
        {
            s = s.Trim(); // retire espace au début et à la fin
            while (s.Contains("  ")) s = s.Replace("  ", " "); // remplace tout les double espace par un espace
            return s;
        }

        private static string Retirer_marque(string s)
        {
            while (s.Contains("#A#A#A#"))
                s = s.Replace("#A#A#A#", " ");
            return s;
        }
        public static bool Si_chercheur(string ID)
        {
            foreach (SUBMITTER_RECORD info in liste_SUBMITTER_RECORD)
            {
                if (info.N0_ID == ID)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool Si_individu(string ID)
        {
            foreach (INDIVIDUAL_RECORD info in liste_INDIVIDUAL_RECORD)
            {
                if (info.N0_ID == ID)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool Si_info_event(string ID)
        {
            foreach (EVEN_RECORD_53 info in liste_EVEN_RECORD_53)
            {
                if (info.N0_ID == ID)
                {
                    return true;
                }
            }
            return false;
        }
        
        public static bool Si_info_source(string ID)
        {
            foreach (SOURCE_RECORD info in liste_SOURCE_RECORD)
            {
                if (info.N0_ID == ID)
                {
                    return true;
                }
            }
            return false;
        }
        
        public static void Voir_message(string message, string raison, string erreur)
        {
            DialogResult reponse;
            string message_log = String.Format("Erreur {0,-7 } {1}\r\n{2,-15 }{3}", erreur, message, "",raison );
            reponse = MessageBox.Show("Erreur " + erreur + ". " + message + "\r\n" + raison +
                "\r\n\r\n" ,
                "Erreur " + erreur + " problème ?",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning);
            Erreur_log(message_log);
            if (reponse == System.Windows.Forms.DialogResult.Cancel)
                GH.GH.annuler = true;
        }

        public static void DEBUG(
            object message = null,
            [CallerFilePath] string code = "",
            [CallerLineNumber] int ligneCode = 0,
            [CallerMemberName] string fonction = null
            )
        {
            if (!GH.GH.DEBUG) return;
            // convertir message à string si n'est pas un string
            if (message != null)
                if (message.GetType() != typeof(string))
                    message.ToString();
            System.Windows.Forms.Button Btn_deboguer = Application.OpenForms["GH"].Controls["Btn_deboguer"] as System.Windows.Forms.Button;
            code = Path.GetFileName(code);
            try
            {
                Btn_deboguer.Visible = true;
                {
                    if (!File.Exists(GH.GH.fichier_debug))
                    {
                        using (StreamWriter ligne = File.AppendText(GH.GH.fichier_debug))
                        {
                            ligne.WriteLine(
                            "<!DOCTYPE html>\n" +
                            "<html lang=\"fr\" style=\"background-color:#FFF;\">\n" +
                            "    <head>\n" +
                            "       <title>Deboguer</title>" +
                            "       <style>\n" +
                            "           h1{color:#00F}\n" +
                            "           .col0{width:150px;vertical-align:top;}\n " +
                            "           .col1{width:90px;vertical-align:top;}\n " +
                            "           .col2{width:50px;vertical-align:top;}\n " +
                            "           .col3{width:330px;vertical-align:top;}\n " +
                            "           .col4{vertical-align:top;}\n " +
                            "           .navbar {\n" +
                            "               overflow: hidden;\n" +
                            "               position: fixed;\n" +
                            "               top: 0;\n" +
                            "               background-color: #FFF;\n" +
                            "               width: 100%;\n" +
                            "           }\n" +
                            "       </style>\n" +
                            "    </head>");
                            Btn_deboguer.Visible = true;
                            ligne.WriteLine("<div class=\"navbar\">");
                            ligne.WriteLine("<h1>Deboguer</h1>");
                            ligne.WriteLine("<table style=\"border:2px solid #000;width:100%\">");
                            ligne.WriteLine("\t<tr><td style=\"width:200px\">Nom</td><td>" + info_HEADER.N2_SOUR_NAME + "</td><td></tr>");
                            ligne.WriteLine("\t<tr><td>Version</td><td>" + info_HEADER.N2_SOUR_VERS + "<td></tr>");
                            ligne.WriteLine("\t<tr><td>Date</td><td>" + info_HEADER.N1_DATE + " " + info_HEADER.N2_DATE_TIME + "<td></tr>");
                            ligne.WriteLine("\t<tr><td>Copyright</td><td>" + info_HEADER.N1_COPR + "<td></tr>");
                            ligne.WriteLine("\t<tr><td>Version</td><td>" + info_HEADER.N2_GEDC_VERS + "<td></tr>");
                            ligne.WriteLine("\t<tr><td>Code charactère</td><td>" + info_HEADER.N1_CHAR + "<td></tr>");
                            ligne.WriteLine("\t<tr><td>Langue</td><td>" + info_HEADER.N1_LANG + "<td></tr>");
                            ligne.WriteLine("\t<tr><td>Fichier sur le disque</td><td>" + info_HEADER.Nom_fichier_disque + "<td></tr>");
                            System.Version version;
                            try
                            {
                                version = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                            }
                            catch
                            {
                                GC.Collect();
                                version = Assembly.GetExecutingAssembly().GetName().Version;
                            }
                            ligne.WriteLine("\t<tr><td>Version de GH</td><td>" + version.Major + "." + version.Minor + "." + version.Build + "<td></tr>");
                            ligne.WriteLine("</table>");
                            ligne.WriteLine(
                                "<table >" +
                                "<tr>" +
                                "<td class=\"col0\">" +
                                "Date heure" +
                                "</td>" +
                                "<td class=\"col1\">" +
                                "Code" +
                                "</td>" +
                                "<td class=\"col2\">" +
                                "Ligne" +
                                "</td>" +
                                "<td class=\"col3\">" +
                                "Routine" +
                                "</td>" +
                                "<td>" +
                                "Message" +
                                "</td>" +
                                "</table>" +
                                "<hr>");
                            ligne.WriteLine("</div>");
                            ligne.WriteLine("<div style=\"margin-top: 325px;\">\n");
                            ligne.WriteLine("</div>\n");
                        }
                    }
                    using (StreamWriter ligne = File.AppendText(GH.GH.fichier_debug))
                        {

                        string s = String.Format(
                            "<table style=\"background-color:#FFFF00;width:100%\">" +
                            "<tr>" +
                            "<td class=\"col0\">" +
                            "{0}" +
                            "</td>" +
                            "<td class=\"col1\">" +
                            "{1}" +
                            "</td>" +
                            "<td class=\"col2\">" +
                            "{2}" +
                            "</td>" +
                            "<td class=\"col3\">" +
                            "{3}" +
                            "</td>" +
                            "<td style=\"width:*\">" +
                            "{4}" +
                            "</td>" +
                            "</tr>" +
                            "</table>" +
                            "<hr>",
                            DateTime.Now, code, ligneCode, fonction, message);
                        ligne.WriteLine(s);
                    }
                }
            }
            catch (Exception msg)
            {
                GC.Collect();
                MessageBox.Show("Debub Actif dans GEDCOM.\r\n\r\n" + code + " " + ligneCode + " " +
                    fonction + "-> " + message + "\r\n\r\n" + msg.Message, GH.GH.erreur + " problème ?",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }
}
