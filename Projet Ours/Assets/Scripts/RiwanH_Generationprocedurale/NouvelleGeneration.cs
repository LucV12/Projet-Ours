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

    public Couloire couloireHorizontal;
    public Couloire couloireVertical;

    List<Connecteur> connecteurDisponiblePlacer = new List<Connecteur>();                       //Pour les portes des salles placer et disponibles
    Connecteur.orientation orientationConnecteurActuel;
    Connecteur.orientation orientationConnecteurDispo;

    Connecteur connecteurDispoObsolete;

    List<Vector3> positionsSallePlacer = new List<Vector3>();

    //public Vector2 intervaleNbDeSalle = new Vector2(5, 15);                                     //Nombre de salles à placer (USELESS)


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
        WaitForSeconds interval = new WaitForSeconds(0.5f);


        //5.2- Placement de la salle du Spawn

        PlacementSalleSpawn();



        //5.3- Selection du nombre de salles à placer


        //5.4- Boucle pour position les salles normales


        while (sallesDejaPlacer.Count < sallesPrefabs.Count)
        {

            PlacementSalleNormale();

        }

        //Debug.LogError("Toutes les salles sont placer");


        //5.5- Boucle pour position les salles d'objectifs

        for (int b = 0; b < 2; b++)                                                                  //La boucle doit s'éffectuer que deux fois
        {

            PlacementSalleObjectif();

        }

        //Debug.LogError("Toutes les salles objectif sont placer");


        //5.6- Placement des salles finals : L'Arène et la Salle du Boss
        for (int b = 0; b < 2; b++)                                                                  //La boucle doit s'éffectuer que deux fois
        {

            PlacementSalleAreneEtBoss(); 

        }

        //Debug.LogError("Toutes les salles finales sont placer");


        //5.7- Fin de la génération

        //Debug.LogError("La generation est terminer");


        //5.8- Re initialisation de la génération pour voire plusieurs ittérations

        yield return new WaitForSeconds(3);

        //ResetGeneration();
    }


    //5.2- Placement Spawn
    void PlacementSalleSpawn()
    {
        //Instantiation de la salle du spawn

        salleSpawn = Instantiate(salleSpawnPrefab) as SalleSpawn;
        salleSpawn.transform.parent = this.transform;                                             


        //Récupération des portes de la salle

        AjoutDesConnecteursSurListe(salleSpawn, ref connecteurDisponiblePlacer);


        //Place la salle

        salleSpawn.transform.position = Vector3.zero;

        positionsSallePlacer.Add(salleSpawn.transform.position);


        //Debug.LogError("Placement du spawn");
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
        int r = Random.Range(0, sallesPrefabs.Count);

        //Instantiation de la salle 1
        Salle salleActuelle = Instantiate(sallesPrefabs[r]) as Salle;
        salleActuelle.transform.parent = this.transform;
        Debug.LogError("Une salle a été choisi");


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

                //Verification que le connecteur est existant
                if (connecteurDispo == null)
                {

                    /*StopCoroutine("GenerationProcedural");

                    if (salleSpawn)
                    {
                        Destroy(salleSpawn.gameObject);
                    }

                    foreach (Salle salle in sallesDejaPlacer)
                    {
                        Destroy(salle.gameObject);
                    }

                    sallesDejaPlacer.Clear();
                    connecteurDisponiblePlacer.Clear();

                    Destroy(GameObject.FindGameObjectWithTag("Enemy"));

                    StartCoroutine("GenerationProcedural");*/

                    //Debug.Log("Connecteur dispo null)");

                    break; 


                }


                //Récupération de l'orientation de la porte

                orientationConnecteurActuel = connecteurActuel.GetComponent<Connecteur>().orientationConnecteur;
                orientationConnecteurDispo = connecteurDispo.GetComponent<Connecteur>().orientationConnecteur;
                


                //Vérification de l'orientation des connecteurs des salles

                if (!VerificationOrientationSalle(connecteurActuel, connecteurDispo))
                {
                    
                    //Debug.Log("Un Connecteur n'est pas bon");

                    //Positionnement de la salle au bout du couloir

                    PositionnementSalle(ref salleActuelle, connecteurActuel, connecteurDispo);
                    


                    if (!VerificationOverlapSalle(salleActuelle))
                    {

                        //Debug.Log("On valide la salle parce qu'il n'y a pas de contact");

                        PlacementCouloire(connecteurDispo, connecteurActuel);

                        sallePlacer = true;

                        sallesDejaPlacer.Add(salleActuelle);

                        //On retire tout les connecteurs utilisés

                        connecteurActuel.gameObject.SetActive(false);
                        connecteurDisponiblePlacer.Remove(connecteurActuel);

                        connecteurDispoObsolete = connecteurDispo;

                        connecteurDispoObsolete.gameObject.SetActive(false);
                        connecteurDisponiblePlacer.Remove(connecteurDispoObsolete);

                        positionsSallePlacer.Add(salleActuelle.transform.position);

                    }
                   
                    break;

                }

            }

            if (sallePlacer)
            {
                Debug.Log("Une salle normal est placer");
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
    void PositionnementSalle(ref Salle salleActuelle, Connecteur connecteurActuel, Connecteur connecteurVisé)
    {

        //Reset la position à 0

        salleActuelle.transform.position = Vector3.zero;


        //Position de la salle

        //Vector3 positionSalleOffset = connecteurActuel.transform.position - salleActuelle.transform.position;
        //salleActuelle.transform.position = connecteurVisé.transform.position - positionSalleOffset;


        if (connecteurVisé.orientationConnecteur == Connecteur.orientation.Est)
        {

            Vector3 positionSalleOffset = connecteurVisé.transform.parent.position;
            salleActuelle.transform.position = positionSalleOffset;
            salleActuelle.transform.Translate(new Vector3(12, 0, 0));

        }
        else if (connecteurVisé.orientationConnecteur == Connecteur.orientation.Ouest)
        {

            Vector3 positionSalleOffset = connecteurVisé.transform.parent.position;
            salleActuelle.transform.position = positionSalleOffset;
            salleActuelle.transform.Translate(new Vector3(-12, 0, 0));

        }
        else if (connecteurVisé.orientationConnecteur == Connecteur.orientation.Sud)
        {

            Vector3 positionSalleOffset = connecteurVisé.transform.parent.position;
            salleActuelle.transform.position = positionSalleOffset;
            salleActuelle.transform.Translate(new Vector3(0, -20, 0));

        }
        else if (connecteurVisé.orientationConnecteur == Connecteur.orientation.Nord)
        {

            Vector3 positionSalleOffset = connecteurVisé.transform.parent.position;
            salleActuelle.transform.position = positionSalleOffset;
            salleActuelle.transform.Translate(new Vector3(0, 20, 0));

        }

    }


    //5.4.3- Placement de couloire
    void PlacementCouloire (Connecteur connecteur, Connecteur connecteurActuelle)
    {

        Couloire couloire;

        if (connecteur.orientationConnecteur == Connecteur.orientation.Est)
        {

            couloire = Instantiate(couloireHorizontal) as Couloire;

            Vector3 positionSalleOffset = connecteur.transform.parent.position;
            couloire.transform.position = positionSalleOffset;
            couloire.transform.Translate(new Vector3(5.5f, 0, 5));

        }
        else if (connecteur.orientationConnecteur == Connecteur.orientation.Ouest)
        {

            couloire = Instantiate(couloireHorizontal) as Couloire;

            Vector3 positionSalleOffset = connecteur.transform.parent.position;
            couloire.transform.position = positionSalleOffset;
            couloire.transform.Translate(new Vector3(-5.5f, 0, 5));

        }
        else if (connecteur.orientationConnecteur == Connecteur.orientation.Sud)
        {

            couloire = Instantiate(couloireVertical) as Couloire;

            Vector3 positionSalleOffset = connecteur.transform.parent.position;
            couloire.transform.position = positionSalleOffset;
            couloire.transform.Translate(new Vector3(0, -10, 5));

        }
        else if (connecteur.orientationConnecteur == Connecteur.orientation.Nord)
        {

            couloire = Instantiate(couloireVertical) as Couloire;

            Vector3 positionSalleOffset = connecteur.transform.parent.position; 
            couloire.transform.position = positionSalleOffset;
            couloire.transform.Translate(new Vector3(0, 10, 5));

        }

    }


    //5.4.4- Verification d'un overlap entre les salles
    bool VerificationOverlapSalle(Salle salle)
    {

        List<Vector3> positionsAVerifier = new List<Vector3>(positionsSallePlacer);

        bool overlap = true;

        foreach (Vector3 positionsSallesAutres in positionsAVerifier)
        {

            if (positionsSallesAutres == salle.transform.position)
            {

                overlap = true;
                //Debug.Log(overlap);
                break;
            }
            else
            {

                overlap = false;
                //Debug.Log(overlap);
            }

        }
        
        if (overlap == true)
        {
            //Debug.Log("return true");
            return true;
        }
        else
        {
            //Debug.Log("return false");
            return false;
        }
       

    }

    
    //5.5- Placement des deux salles d'objectifs
    void PlacementSalleObjectif()
    {

        //Instantiation de la salle
        int r = Random.Range(0, salleObjectifPrefab.Count);

        SalleObjectif salleActuelle = Instantiate(salleObjectifPrefab[r]) as SalleObjectif;
        salleActuelle.transform.parent = this.transform;


        //Rajoute les connecteurs au fur et à mesure d'abord dans la liste des portes de la salle, puis dans la liste de toute les portes placer et disponibles

        List<Connecteur> tousConnecteursDispo = new List<Connecteur>(connecteurDisponiblePlacer);
        //print("Il y a " + tousConnecteursDispo.Count + " connecteurs disponibles *1*");
        List<Connecteur> connecteursSalleActuelle = new List<Connecteur>();

        AjoutDesConnecteursSurListe(salleActuelle, ref connecteursSalleActuelle);

        AjoutDesConnecteursSurListe(salleActuelle, ref connecteurDisponiblePlacer);

        bool sallePlacer = false;

        //Debug.Log(connecteursSalleActuelle.Count);
        //Test de toutes les portes disponibles

        foreach (Connecteur connecteurDispo in tousConnecteursDispo)
        {
            //Debug.Log(connecteursSalleActuelle.Count);
            //Test de toutes les portes de la salleActuelle

            foreach (Connecteur connecteurActuel in connecteursSalleActuelle)
            {

                //Verification que le connecteur est existant
                if (connecteurDispo == null)
                {     

                    break;
                }

                //Récupération de l'orientation de la porte

                orientationConnecteurActuel = connecteurActuel.GetComponent<Connecteur>().orientationConnecteur;
                orientationConnecteurDispo = connecteurDispo.GetComponent<Connecteur>().orientationConnecteur;


                //Vérification de l'orientation des connecteurs des salles

                if (!VerificationOrientationSalle(connecteurActuel, connecteurDispo))
                {
                    
                    //Positionnement de la salle au bout du couloir

                    PositionnementSalleObjectif(ref salleActuelle, connecteurDispo);


                    if (!VerificationOverlapSalle(salleActuelle))
                    {

                        PlacementCouloire(connecteurDispo, connecteurActuel);

                        sallePlacer = true;

                        sallesDejaPlacer.Add(salleActuelle);


                        //On retire tout les connecteurs utilisés

                        connecteurActuel.gameObject.SetActive(false);
                        connecteurDisponiblePlacer.Remove(connecteurActuel);

                        connecteurDispo.gameObject.SetActive(false);
                        connecteurDisponiblePlacer.Remove(connecteurDispo);

                        positionsSallePlacer.Add(salleActuelle.transform.position);

                        salleObjectifPrefab.RemoveAt(r);
                    }

                    //Debug.Log("Placement d'une salle objectif");

                    break;

                }
 

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
            salleActuelle.transform.Translate(new Vector3(12, 0, 0));

            //Debug.Log("La salle objectif va être placer à droite");

        }
        else if (connecteurVisé.orientationConnecteur == Connecteur.orientation.Ouest)
        {

            Vector3 positionSalleOffset = connecteurVisé.transform.parent.position;
            salleActuelle.transform.position = positionSalleOffset;
            salleActuelle.transform.Translate(new Vector3(-12, 0, 0));

            //Debug.Log("La salle objectif va être placer à gauche");

        }
        else if (connecteurVisé.orientationConnecteur == Connecteur.orientation.Sud)
        {

            Vector3 positionSalleOffset = connecteurVisé.transform.parent.position;
            salleActuelle.transform.position = positionSalleOffset;
            salleActuelle.transform.Translate(new Vector3(0, -20, 0));

            //Debug.Log("La salle objectif va être placer en haut");

        }
        else if (connecteurVisé.orientationConnecteur == Connecteur.orientation.Nord)
        {

            Vector3 positionSalleOffset = connecteurVisé.transform.parent.position;
            salleActuelle.transform.position = positionSalleOffset;
            salleActuelle.transform.Translate(new Vector3(0, 20, 0));

            //Debug.Log("La salle objectif va être placer en bas");

        }


    }


   //5.6- Placement de l'arène et dela salle du boss
    void PlacementSalleAreneEtBoss()
    {
        int r = Random.Range(0, salleBossArenesPrefab.Count);
        //Instantiation de la salle

        SalleBossArene salleActuelle = Instantiate(salleBossArenesPrefab[r]) as SalleBossArene;
        salleActuelle.transform.parent = this.transform;


        //Rajoute les connecteurs au fur et à mesure d'abord dans la liste des portes de la salle, puis dans la liste de toute les portes placer et disponibles

        List<Connecteur> tousConnecteursDispo = new List<Connecteur>(connecteurDisponiblePlacer);
        List<Connecteur> connecteursSalleActuelle = new List<Connecteur>();

        AjoutDesConnecteursSurListe(salleActuelle, ref connecteursSalleActuelle);

        AjoutDesConnecteursSurListe(salleActuelle, ref connecteurDisponiblePlacer);

        bool sallePlacer = false;

        //Debug.Log(connecteursSalleActuelle.Count);
        //Test de toutes les portes disponibles

        foreach (Connecteur connecteurDispo in tousConnecteursDispo)
        {
            //Debug.Log(connecteursSalleActuelle.Count);
            //Test de toutes les portes de la salleActuelle

            foreach (Connecteur connecteurActuel in connecteursSalleActuelle)
            {

                //Verification que le connecteur est existant
                if (connecteurDispo == null)
                {

                    /*StopCoroutine("GenerationProcedural");

                    if (salleSpawn)
                    {
                        Destroy(salleSpawn.gameObject);
                    }

                    foreach (Salle salle in sallesDejaPlacer)
                    {
                        Destroy(salle.gameObject);
                    }

                    Destroy(GameObject.FindGameObjectWithTag("Enemy"));

                    sallesDejaPlacer.Clear();
                    connecteurDisponiblePlacer.Clear();

                    StartCoroutine("GenerationProcedural");*/

                    break;
                }

                //Debug.Log(connecteursSalleActuelle.Count);
                //Récupération de l'orientation de la porte

                orientationConnecteurActuel = connecteurActuel.GetComponent<Connecteur>().orientationConnecteur;
                orientationConnecteurDispo = connecteurDispo.GetComponent<Connecteur>().orientationConnecteur;


                //Vérification de l'orientation des connecteurs des salles

                if (!VerificationOrientationSalle(connecteurActuel, connecteurDispo))
                {
                    PositionnementSalleBossArene(ref salleActuelle, connecteurDispo);


                    if (!VerificationOverlapSalle(salleActuelle))
                    {

                        PlacementCouloire(connecteurDispo, connecteurActuel);

                        sallePlacer = true;

                        sallesDejaPlacer.Add(salleActuelle);


                        //On retire tout les connecteurs utilisés

                        connecteurActuel.gameObject.SetActive(false);
                        connecteurDisponiblePlacer.Remove(connecteurActuel);

                        connecteurDispo.gameObject.SetActive(false);
                        connecteurDisponiblePlacer.Remove(connecteurDispo);

                        positionsSallePlacer.Add(salleActuelle.transform.position);

                        salleBossArenesPrefab.RemoveAt(r);
                    }

                    break;


                }
               

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
            salleActuelle.transform.Translate(new Vector3(12, 0, 0));

            //Debug.Log("La salle boss va être placer à droite");

        }
        else if (connecteurVisé.orientationConnecteur == Connecteur.orientation.Ouest)
        {

            Vector3 positionSalleOffset = connecteurVisé.transform.parent.position;
            salleActuelle.transform.position = positionSalleOffset;
            salleActuelle.transform.Translate(new Vector3(-12, 0, 0));

            //Debug.Log("La salle boss va être placer à gauche");

        }
        else if (connecteurVisé.orientationConnecteur == Connecteur.orientation.Sud)
        {

            Vector3 positionSalleOffset = connecteurVisé.transform.parent.position;
            salleActuelle.transform.position = positionSalleOffset;
            salleActuelle.transform.Translate(new Vector3(0, -20, 0));

            //Debug.Log("La salle va être placer en haut");

        }
        else if (connecteurVisé.orientationConnecteur == Connecteur.orientation.Nord)
        {

            Vector3 positionSalleOffset = connecteurVisé.transform.parent.position;
            salleActuelle.transform.position = positionSalleOffset;
            salleActuelle.transform.Translate(new Vector3(0, 20, 0));

            //Debug.Log("La salle va être placer en bas");

        }



    }




    /*5.8- Re initialisation de la génération pour voire plusieurs ittérations
    void ResetGeneration()
    {
        Debug.Log("Re initialisation de la génération");
    }*/
}
