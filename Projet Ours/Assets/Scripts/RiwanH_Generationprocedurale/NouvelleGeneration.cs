using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NouvelleGeneration : MonoBehaviour
{

    //1- Paramétrage de la génération, les salles, les portes et leur orientation, et le nombre de salle à générer

    public Salle salleSpawnPrefab;                                                              //Pour les salles Uniques

    public List<Salle> sallesPrefabs = new List<Salle>();                                       //Pour les salles lambdas
    public List<SalleObjectif> salleObjectifPrefab = new List<SalleObjectif>();                 //Pour les salles d'objectifs
    public List<SalleBossArene> salleBossArenesPrefab = new List<SalleBossArene>();

    List<Connecteur> connecteurDisponiblePlacer = new List<Connecteur>();                       //Pour les portes des salles placer et disponibles
    Connecteur.orientation orientationConnecteurActuel;
    Connecteur.orientation orientationConnecteurDispo;

    public Vector2 intervaleNbDeSalle = new Vector2(5, 15);                                     //Nombre de salles à placer


    //2- Référence pour toutes les salles et les couloirs

    SalleSpawn salleSpawn;
    SalleBossArene salleBoss;

    List<Salle> sallesDejaPlacer = new List<Salle>();
    List<SalleObjectif> sallesObjectifDejaPlacer = new List<SalleObjectif>();


    //3- LayerMask pour les salles (faut qu'on m'explique)

    LayerMask layerMaskSalle;


    //4- Création de la Coroutine
    private void Start()
    {
        layerMaskSalle = LayerMask.GetMask("Salle");                                            //Faut créer un layer "Salle"
        StartCoroutine("GenerationProcedural");
    }


    //5- Colone vertébrale
    IEnumerator GenerationProcedural()
    {

        //5.1- Initiation du startup et des interval

        WaitForSeconds startup = new WaitForSeconds(1);
        WaitForFixedUpdate interval = new WaitForFixedUpdate();

        yield return interval;


        //5.2- Placement de la salle du Spawn

        PlacementSalleSpawn();

        yield return interval;


        //5.3- Selection du nombre de salles à placer

        int nbDeSalle = Random.Range((int)intervaleNbDeSalle.x, (int)intervaleNbDeSalle.y);


        //5.4- Boucle pour position les salles normales

        for (int a = 0; a < nbDeSalle; a++)
        {

            PlacementSalleNormale();

            yield return interval;

        }

        Debug.LogError("Toutes les salles sont placer");


        //5.5- Boucle pour position les salles d'objectifs

        for (int b = 0; b < 2; b++)                                                                  //La boucle doit s'éffectuer que deux fois
        {

            PlacementSalleObjectif();

            yield return interval;

        }

        Debug.LogError("Toutes les salles objectif sont placer");


        //5.6- Placement des salles finals : L'Arène et la Salle du Boss
        for (int b = 0; b < 2; b++)                                                                  //La boucle doit s'éffectuer que deux fois
        {

            PlacementSalleAreneEtBoss(); 

            yield return interval;

        }

        Debug.LogError("Toutes les salles finales sont placer");


        //5.7- Fin de la génération

        Debug.LogError("La generation est terminer");


        //5.8- Re initialisation de la génération pour voire plusieurs ittérations

        yield return new WaitForSeconds(3);

        ResetGeneration();
    }


    //5.2- Placement Spawn
    void PlacementSalleSpawn()
    {
        //Instantiation de la salle du spawn

        salleSpawn = Instantiate(salleSpawnPrefab) as SalleSpawn;
        salleSpawn.transform.parent = this.transform;                                                   //Pas compris


        //Récupération des portes de la salle

        AjoutDesConnecteursSurListe(salleSpawn, ref connecteurDisponiblePlacer);


        //Place la salle

        salleSpawn.transform.position = Vector3.zero;


        Debug.Log("Placement du spawn");
    }


    //5.2.1- Ajout des Connecteurs sur Liste
    void AjoutDesConnecteursSurListe(Salle salle, ref List<Connecteur> list)
    {

        foreach(Connecteur connecteur in salle.connecteurs)
        {

            int r = Random.Range(0, list.Count);
            list.Insert(r, connecteur);

        }

    }


    //5.4- Place d'une salle random
    void PlacementSalleNormale()
    {

        //Instantiation de la salle

        Salle salleActuelle = Instantiate(sallesPrefabs[Random.Range(0, sallesPrefabs.Count)]) as Salle;
        salleActuelle.transform.parent = this.transform;
        Debug.Log("Une salle a été choisi");


        //Rajoute les connecteurs au fur et à mesure d'abord dans la liste des portes de la salle, puis dans la liste de toute les portes placer et disponibles

        List<Connecteur> tousConnecteursDispo = new List<Connecteur>(connecteurDisponiblePlacer);
        

        List<Connecteur> connecteursSalleActuelle = new List<Connecteur>();
        

        AjoutDesConnecteursSurListe(salleActuelle, ref connecteursSalleActuelle);
        print("Il y a dans la salle atuel  " + connecteursSalleActuelle.Count + "  Connecteurs");

        AjoutDesConnecteursSurListe(salleActuelle, ref connecteurDisponiblePlacer);
        

        bool sallePlacer = false;


        //Test de toutes les portes disponibles

        foreach (Connecteur connecteurDispo in tousConnecteursDispo)
        {

            //Test de toutes les portes de la salleActuelle

            foreach (Connecteur connecteurActuel in connecteursSalleActuelle)
            {
                
                //Récupération de l'orientation de la porte

                orientationConnecteurActuel = connecteurActuel.GetComponent<Connecteur>().orientationConnecteur;
                orientationConnecteurDispo = connecteurDispo.GetComponent<Connecteur>().orientationConnecteur;


                //Vérification de l'orientation des connecteurs des salles

                if (VerificationOrientationSalle(connecteurActuel, connecteurDispo))
                {

                    connecteurDisponiblePlacer.Remove(connecteurActuel);

                    Debug.Log("Un Connecteur n'est pas bon");

                    continue;
                }


                //Positionnement de la salle au bout du couloir

                PositionnementSalle(ref salleActuelle, connecteurDispo);


                sallePlacer = true;

                sallesDejaPlacer.Add(salleActuelle);


                //On retire tout les connecteurs utilisés

                connecteurActuel.gameObject.SetActive(false);
                connecteurDisponiblePlacer.Remove(connecteurActuel);

                connecteurDispo.gameObject.SetActive(false);
                connecteurDisponiblePlacer.Remove(connecteurDispo);

                Debug.Log("Une salle normal est placer");

                break;

            }

            if (sallePlacer)
            {
                break;
            }

        }

        //Si la salle a pas pu être placer on recommence

        if (!sallePlacer)
        {

            Destroy(salleActuelle.gameObject);

            //faire un truc pour juste recommencer avec une nouvelle salle et pas juste tout recommencer

        }

    }


    //5.4.1- Verification de l'Orientation de la Salle
    bool VerificationOrientationSalle(Connecteur connecteurAPlacer, Connecteur connecteurViser)
    {

        if (connecteurAPlacer.orientationConnecteur == Connecteur.orientation.Ouest && connecteurViser.orientationConnecteur == Connecteur.orientation.Est)
        {
            return false;

        }else if (connecteurAPlacer.orientationConnecteur == Connecteur.orientation.Est && connecteurViser.orientationConnecteur == Connecteur.orientation.Ouest)
        {
            return false;

        }else if (connecteurAPlacer.orientationConnecteur == Connecteur.orientation.Nord && connecteurViser.orientationConnecteur == Connecteur.orientation.Sud)
        {
            return false;

        }else if (connecteurAPlacer.orientationConnecteur == Connecteur.orientation.Sud && connecteurViser.orientationConnecteur == Connecteur.orientation.Nord)
        {
            return false;
        }
        else
        {
            return true;
        }

    }


    //5.4.2- Positionnement de la Salle
    void PositionnementSalle(ref Salle salleActuelle, Connecteur connecteurVisé)
    {

        //Reset la position à 0

        salleActuelle.transform.position = Vector3.zero;


        //Position de la salle

        if (connecteurVisé.orientationConnecteur == Connecteur.orientation.Est)
        {

            Vector3 positionSalleOffset = connecteurVisé.transform.parent.position;
            salleActuelle.transform.position = positionSalleOffset;
            salleActuelle.transform.Translate(new Vector3(10, 0, 0));

        }
        else if (connecteurVisé.orientationConnecteur == Connecteur.orientation.Ouest)
        {

            Vector3 positionSalleOffset = connecteurVisé.transform.parent.position;
            salleActuelle.transform.position = positionSalleOffset;
            salleActuelle.transform.Translate(new Vector3(-10, 0, 0));

        }
        else if (connecteurVisé.orientationConnecteur == Connecteur.orientation.Sud)
        {

            Vector3 positionSalleOffset = connecteurVisé.transform.parent.position;
            salleActuelle.transform.position = positionSalleOffset;
            salleActuelle.transform.Translate(new Vector3(0, -10, 0));

        }
        else if (connecteurVisé.orientationConnecteur == Connecteur.orientation.Nord)
        {

            Vector3 positionSalleOffset = connecteurVisé.transform.parent.position;
            salleActuelle.transform.position = positionSalleOffset;
            salleActuelle.transform.Translate(new Vector3(0, 10, 0));

        }

    }


    //5.5- Placement des deux salles d'objectifs
    void PlacementSalleObjectif()
    {

        //Rajoute les salles dans une nouvelle liste pour pouvoir les retirer

        List<SalleObjectif> salleObjectifAPlacer = new List<SalleObjectif>(salleObjectifPrefab);

        //Instantiation de la salle

        SalleObjectif salleActuelle = Instantiate(salleObjectifAPlacer[Random.Range(0, salleObjectifAPlacer.Count)]) as SalleObjectif;
        salleActuelle.transform.parent = this.transform;


        //Rajoute les connecteurs au fur et à mesure d'abord dans la liste des portes de la salle, puis dans la liste de toute les portes placer et disponibles

        List<Connecteur> tousConnecteursDispo = new List<Connecteur>(connecteurDisponiblePlacer);
        List<Connecteur> connecteursSalleActuelle = new List<Connecteur>();

        AjoutDesConnecteursSurListe(salleActuelle, ref connecteursSalleActuelle);

        AjoutDesConnecteursSurListe(salleActuelle, ref connecteurDisponiblePlacer);

        bool sallePlacer = false;


        //Test de toutes les portes disponibles

        foreach (Connecteur connecteurDispo in tousConnecteursDispo)
        {

            //Test de toutes les portes de la salleActuelle

            foreach (Connecteur connecteurActuel in connecteursSalleActuelle)
            {

                //Récupération de l'orientation de la porte

                orientationConnecteurActuel = connecteurActuel.GetComponent<Connecteur>().orientationConnecteur;
                orientationConnecteurDispo = connecteurDispo.GetComponent<Connecteur>().orientationConnecteur;


                //Vérification de l'orientation des connecteurs des salles

                if (VerificationOrientationSalle(connecteurActuel, connecteurDispo))
                {

                    connecteurDisponiblePlacer.Remove(connecteurActuel);

                    Debug.Log("Un Connecteur n'est pas bon");

                    continue;
                }


                //


                //Positionnement de la salle au bout du couloir

                PositionnementSalleObjectif(ref salleActuelle, connecteurDispo);


                sallePlacer = true;

                sallesDejaPlacer.Add(salleActuelle);


                //On retire tout les connecteurs utilisés

                connecteurActuel.gameObject.SetActive(false);
                connecteurDisponiblePlacer.Remove(connecteurActuel);

                connecteurDispo.gameObject.SetActive(false);
                connecteurDisponiblePlacer.Remove(connecteurDispo);

                salleObjectifAPlacer.Remove(salleActuelle);

                Debug.Log("Placement d'une salle objectif");

                break;

            }

            if (sallePlacer)
            {
                break;
            }

        }

        //Si la salle a pas pu être placer on recommence

        if (!sallePlacer)
        {

            Destroy(salleActuelle.gameObject);

        }


        
    }


    //5.5.1- Positionnement de la Salle
    void PositionnementSalleObjectif(ref SalleObjectif salleActuelle, Connecteur connecteurVisé)
    {

        //Reset la position à 0

        salleActuelle.transform.position = Vector3.zero;


        //Position de la salle

        if (connecteurVisé.orientationConnecteur == Connecteur.orientation.Est)
        {

            Vector3 positionSalleOffset = connecteurVisé.transform.parent.position;
            salleActuelle.transform.position = positionSalleOffset;
            salleActuelle.transform.Translate(new Vector3(10, 0, 0));

        }
        else if (connecteurVisé.orientationConnecteur == Connecteur.orientation.Ouest)
        {

            Vector3 positionSalleOffset = connecteurVisé.transform.parent.position;
            salleActuelle.transform.position = positionSalleOffset;
            salleActuelle.transform.Translate(new Vector3(-10, 0, 0));

        }
        else if (connecteurVisé.orientationConnecteur == Connecteur.orientation.Sud)
        {

            Vector3 positionSalleOffset = connecteurVisé.transform.parent.position;
            salleActuelle.transform.position = positionSalleOffset;
            salleActuelle.transform.Translate(new Vector3(0, -10, 0));

        }
        else if (connecteurVisé.orientationConnecteur == Connecteur.orientation.Nord)
        {

            Vector3 positionSalleOffset = connecteurVisé.transform.parent.position;
            salleActuelle.transform.position = positionSalleOffset;
            salleActuelle.transform.Translate(new Vector3(0, 10, 0));

        }


    }


    //5.6- Placement de l'arène et dela salle du boss
    void PlacementSalleAreneEtBoss()
    {

        //Rajoute les salles dans une nouvelle liste pour pouvoir les retirer

        List<SalleBossArene> salleBossAreneAPlacer = new List<SalleBossArene>(salleBossArenesPrefab);

        //Instantiation de la salle

        SalleBossArene salleActuelle = Instantiate(salleBossArenesPrefab[Random.Range(0, salleBossArenesPrefab.Count)]) as SalleBossArene;
        salleActuelle.transform.parent = this.transform;


        //Rajoute les connecteurs au fur et à mesure d'abord dans la liste des portes de la salle, puis dans la liste de toute les portes placer et disponibles

        List<Connecteur> tousConnecteursDispo = new List<Connecteur>(connecteurDisponiblePlacer);
        List<Connecteur> connecteursSalleActuelle = new List<Connecteur>();

        AjoutDesConnecteursSurListe(salleActuelle, ref connecteursSalleActuelle);

        AjoutDesConnecteursSurListe(salleActuelle, ref connecteurDisponiblePlacer);

        bool sallePlacer = false;


        //Test de toutes les portes disponibles

        foreach (Connecteur connecteurDispo in tousConnecteursDispo)
        {

            //Test de toutes les portes de la salleActuelle

            foreach (Connecteur connecteurActuel in connecteursSalleActuelle)
            {

                //Récupération de l'orientation de la porte

                orientationConnecteurActuel = connecteurActuel.GetComponent<Connecteur>().orientationConnecteur;
                orientationConnecteurDispo = connecteurDispo.GetComponent<Connecteur>().orientationConnecteur;


                //Vérification de l'orientation des connecteurs des salles

                if (VerificationOrientationSalle(connecteurActuel, connecteurDispo))
                {

                    connecteurDisponiblePlacer.Remove(connecteurActuel);

                    Debug.Log("Un Connecteur n'est pas bon");

                    continue;
                }


                //


                //Positionnement de la salle au bout du couloir

                PositionnementSalleBossArene(ref salleActuelle, connecteurDispo);


                sallePlacer = true;

                sallesDejaPlacer.Add(salleActuelle);


                //On retire tout les connecteurs utilisés

                connecteurActuel.gameObject.SetActive(false);
                connecteurDisponiblePlacer.Remove(connecteurActuel);

                connecteurDispo.gameObject.SetActive(false);
                connecteurDisponiblePlacer.Remove(connecteurDispo);

                salleBossAreneAPlacer.Remove(salleActuelle);

                Debug.Log("Placement de l'arène et dela salle du boss");

                break;

            }

            if (sallePlacer)
            {
                break;
            }

        }

        //Si la salle a pas pu être placer on recommence

        if (!sallePlacer)
        {

            Destroy(salleActuelle.gameObject);

            //faire un truc pour juste recommencer avec une nouvelle salle et pas juste tout recommencer

        }

    }


    //5.6.1- Positionnement de la Salle
    void PositionnementSalleBossArene(ref SalleBossArene salleActuelle, Connecteur connecteurVisé)
    {

        //Reset la position à 0

        salleActuelle.transform.position = Vector3.zero;


        //Position de la salle

        if (connecteurVisé.orientationConnecteur == Connecteur.orientation.Est)
        {

            Vector3 positionSalleOffset = connecteurVisé.transform.parent.position;
            salleActuelle.transform.position = positionSalleOffset;
            salleActuelle.transform.Translate(new Vector3(10, 0, 0));

        }
        else if (connecteurVisé.orientationConnecteur == Connecteur.orientation.Ouest)
        {

            Vector3 positionSalleOffset = connecteurVisé.transform.parent.position;
            salleActuelle.transform.position = positionSalleOffset;
            salleActuelle.transform.Translate(new Vector3(-10, 0, 0));

        }
        else if (connecteurVisé.orientationConnecteur == Connecteur.orientation.Sud)
        {

            Vector3 positionSalleOffset = connecteurVisé.transform.parent.position;
            salleActuelle.transform.position = positionSalleOffset;
            salleActuelle.transform.Translate(new Vector3(0, -10, 0));

        }
        else if (connecteurVisé.orientationConnecteur == Connecteur.orientation.Nord)
        {

            Vector3 positionSalleOffset = connecteurVisé.transform.parent.position;
            salleActuelle.transform.position = positionSalleOffset;
            salleActuelle.transform.Translate(new Vector3(0, 10, 0));

        }



    }


    //5.8- Re initialisation de la génération pour voire plusieurs ittérations
    void ResetGeneration()
    {
        Debug.Log("Re initialisation de la génération");
    }
}
