using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClick : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite markedGamefield;
    public Sprite emptyGamefield;

    private bool ithaveBeenClicked = false;
    private GameObject thisTile; 
    private void Awake()
    {
   
    }
    void OnMouseDown()
    {
        Debug.Log("click on obejct rectangle");
        if (ithaveBeenClicked)
        {
            Debug.Log("loads hraciplochaPrazdna");
          //  Sprite tempTexture = (Sprite)Resources.Load<Sprite>("Assets/sprits/textures/gameFieldTextures/hraciPlochaPrazdna.png") as Sprite;

           Debug.Log(emptyGamefield);

            this.gameObject.GetComponent<SpriteRenderer>().sprite = emptyGamefield;

            ithaveBeenClicked = !ithaveBeenClicked;

        }
        else
        {
            Debug.Log("loads hraciplochaOznacena");       
          //  Sprite tempTexture = (Sprite)Resources.Load<Sprite>("Assets/sprits/textures/gameFieldTextures/hraciPlochaOznacena.png") as Sprite;
            Debug.Log(markedGamefield);

            this.gameObject.GetComponent<SpriteRenderer>().sprite = markedGamefield;
          

            ithaveBeenClicked = !ithaveBeenClicked;

        }
    }
}
