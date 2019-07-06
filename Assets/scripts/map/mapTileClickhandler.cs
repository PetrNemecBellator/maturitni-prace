using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapTileClickhandler : MonoBehaviour
{
    private Camera thisTile ;

    private bool ithaveBeenClicked = false;



    // Start is called before the first frame update
     void Start()
     {
      
     }

    // Update is called once per frame

  /*  void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("nejaky ten click");
            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, new Vector2 (0,0));
            if (hit.collider != null )
            {
                Debug.Log("Player Detected");
              

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
        
       
    }*/
}
