using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationCapaEtRage : MonoBehaviour
{

    //1- Paramétrage de la génération

    public List<CapaEtRage> capaEtRacePrefab = new List<CapaEtRage>();

    public PointSpawn pointDeSpawn;


    //2- Création de la coroutine
    private void start()
    {

        StartCoroutine("GenerationCapaEtRage");

    }


    //3- Colone Vertébrale
    IEnumerator Generation()
    {

        //3.1- Initiation de la coroutine

        WaitForSeconds startup = new WaitForSeconds(1);
        WaitForFixedUpdate interval = new WaitForFixedUpdate();

        yield return interval;


        //3.2- Generation d'une capacité

        GenerationPrefab();

        yield return interval;


    }


    //3.2- Generation du Prefab
    void GenerationPrefab()
    {

        //3.2.1- Instantiation d'un prefab de capa ou rage

        CapaEtRage prefabActuel = Instantiate(capaEtRacePrefab[Random.Range(0, capaEtRacePrefab.Count)]) as CapaEtRage;
        prefabActuel.transform.parent = this.transform;


        //3.2.2- Positionement du prefab

        PositionementPrefab(ref prefabActuel, pointDeSpawn);


        //3.2.3- On retire la Capa ou Rage de la list

        capaEtRacePrefab.Remove(prefabActuel);

    }


    //3.2.2- Positionement du Prefab
    void PositionementPrefab(ref CapaEtRage prefabAPlacer, PointSpawn pointSpawn)
    {

        //Reset la position à zero

        prefabAPlacer.transform.position = Vector3.zero;


        //Positionement du prefab sur le point de spawn

        Vector3 positionPrefabOffset = pointSpawn.transform.parent.position;
        prefabAPlacer.transform.position = positionPrefabOffset;

    }
}
