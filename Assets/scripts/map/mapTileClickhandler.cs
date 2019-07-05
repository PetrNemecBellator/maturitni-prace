using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapTileClickhandler : MonoBehaviour
{
    public GameObject thisTile;

    private bool ithaveBeenClicked = false;



    // Start is called before the first frame update
    /* void Start()
     {

     }*/

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 20.0f))
            {
                Debug.Log("Aspon trochu sem klikl");

                if (hit.transform  != null)
                {
                    OnClick();
                }
            }
        }



    }
   
    private void OnClick()
    {
        Debug.Log("click");
        Destroy(thisTile);
        if (!ithaveBeenClicked)
        {
            Texture2D tempTexture = (Texture2D)Resources.Load("hraciPlochaPrazdna") as Texture2D;

            thisTile.GetComponent<Renderer>().material.mainTexture = tempTexture;

            ithaveBeenClicked = !ithaveBeenClicked;

        } else{
            Texture2D tempTexture = (Texture2D)Resources.Load("hraciPlochaOznacena") as Texture2D;

            thisTile.GetComponent<Renderer>().material.mainTexture = tempTexture;

            ithaveBeenClicked = !ithaveBeenClicked;

        }
        
       
    }
}
