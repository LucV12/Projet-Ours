using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salle : MonoBehaviour
{
    public Connecteur[] connecteurs; //Tableau contenat toutes les portes de la salle
    public MeshCollider meshCollider; //meshcollider pour check si il y a des supperposition

    public Bounds LimiteSalle
    {
        get { return meshCollider.bounds; } //easy access to the meshcollider
    }
}
