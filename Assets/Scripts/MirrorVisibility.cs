using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorVisibility : MonoBehaviour
{

    [SerializeField] // layers to determin visibility
    private int mirrorIqnoreLayer = 8, mirrorOnlyLayer = 9;
    [SerializeField] // bool to determin which layer to use
    private bool exist = true;
    private bool oldExist = true;
    [SerializeField]
    private bool reverseEffect = false;
    [SerializeField]
    private bool isActive = true;

    // list to hold this and all child gameobjects
    private List<GameObject> gameObjects = new List<GameObject>();
    // list to hold this and all child colliders
    private List<Collider> colliders = new List<Collider>();
    // MagicPortal
    MagicPortal magicportal;

    private void Start()
    {
        // subscribe to MagicPortals call list
        magicportal = FindObjectOfType<MagicPortal>();
        magicportal.callMirrorVisibilitys += this.Exist;

        // add this to both lists
        gameObjects.Add(this.gameObject);
        Collider[] collider = gameObject.GetComponents<Collider>();

        // to colliders list only if we have one
        if (collider != null)
        {
            colliders.AddRange(collider);
        }

        // get all childs gameobjects and colliders
        foreach (Transform child in transform)
        {
            gameObjects.Add(child.gameObject);

            collider = child.GetComponents<Collider>();
            if(collider != null)
            {
                colliders.AddRange(collider);
            }
        }

        // initialise the state of existance
        Exist(exist);
        gameObject.SetActive(isActive);
    }

    private void OnDestroy()
    {
        // un-subscribe from MagicPortals call list
        magicportal.callMirrorVisibilitys -= this.Exist;
    }

    // Toggle The State Of Existance!
    public void Exist(bool exist)
    {
        this.exist = exist;

        bool temp = (reverseEffect ? !exist : exist);

        foreach (GameObject go in gameObjects)
        {
            go.layer = (temp ? mirrorIqnoreLayer : mirrorOnlyLayer);
        }

        foreach(Collider col in colliders)
        {
            col.enabled = temp;
        }
    }

    // for inspector debugging, disable for final build
    private void Update()
    {
        if(exist != oldExist)
        {
            oldExist = exist;
            Exist(exist);
        }
    }

}
