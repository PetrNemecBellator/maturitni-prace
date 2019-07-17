using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClick : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite markedGamefield;
    public Sprite emptyGamefield;
    private Vector2 tileCoordinates;




    private static bool wasAnyTileClicked = false;
    private static GameObject lastClikedTile = null;

    private bool ithaveBeenClicked = false;
    private GameObject typeOfunitCurectlyHaving;


    private void Awake()
    {
        typeOfunitCurectlyHaving = this.gameObject.transform.GetChild(0).gameObject;
        Debug.Log("tile child name: "+ typeOfunitCurectlyHaving.name);
    }
    void OnMouseDown()
    {
        Debug.Log("click on obejct rectangle");
        if (ithaveBeenClicked)
        {           
            changeToUnMark();
            Debug.Log("x: " + this.tileCoordinates.x + " y: " + this.tileCoordinates.y);
         

        }
        else
        {
            Debug.Log("co nefunguje? wasAnytileClicked" + wasAnyTileClicked.ToString() + "&&" + (lastClikedTile != null).ToString());
            byte numberOfchecks = 0;
            needNewCheck:
            if (wasAnyTileClicked && lastClikedTile != null)
            {

                //staticky buffer na ukladani dat z naposledy klikleho bloku
                //nejednodusi metoda prenosu dat pravdepodobne

                Debug.Log("bufferujeme na bloku X" + this.tileCoordinates.x + "y: " + this.tileCoordinates.y);
                Debug.Log("predchoziblock " + lastClikedTile.name + " X " + lastClikedTile.transform.position.x + " y " + lastClikedTile.transform.position.y);

                TileClick scriptOfLastClickedTile = lastClikedTile.GetComponent<TileClick>();
                try
                {
                    this.setTypeOfunit(scriptOfLastClickedTile.transform.GetChild(0).gameObject);

                }
                catch(System.Exception e)
                {
                    Debug.Log("non child on current object");
                }
                finally
                {
                    scriptOfLastClickedTile.unSetUnit();

                }


            }
            else
            {
                lastClikedTile = this.gameObject;
                numberOfchecks++;
                if(numberOfchecks > 1)
                {

                }
                else
                {
                    goto needNewCheck;

                }
            }
            lastClikedTile = null;
            
            changeToMarked();
        }
    }

    public void changeToMarked()
    {
        //Debug.Log("loads hraciplochaOznacena");
        //  Sprite tempTexture = (Sprite)Resources.Load<Sprite>("Assets/sprits/textures/gameFieldTextures/hraciPlochaOznacena.png") as Sprite;
       // Debug.Log(markedGamefield);

        this.gameObject.GetComponent<SpriteRenderer>().sprite = markedGamefield;


        this.ithaveBeenClicked = !this.ithaveBeenClicked;
        wasAnyTileClicked = true;
    }
    public void changeToUnMark()
    {
       // Debug.Log("loads hraciplocha neoznacena");
        //  Sprite tempTexture = (Sprite)Resources.Load<Sprite>("Assets/sprits/textures/gameFieldTextures/hraciPlochaPrazdna.png") as Sprite;

       // Debug.Log(emptyGamefield);

        this.gameObject.GetComponent<SpriteRenderer>().sprite = emptyGamefield;
        this.ithaveBeenClicked = !this.ithaveBeenClicked;
        wasAnyTileClicked = false;
    }

    public bool getWasAnyTileClicked()
    {
        return wasAnyTileClicked;
    }
    public void setWasAnyTileClicked(bool value)
    {
        wasAnyTileClicked = value;
    }
    public void setTileCoordinates(Vector2 value)
    {
        this.tileCoordinates = value;
    }
    public Vector2 getCoordinates()
    {
        return this.tileCoordinates;
    }
    //x10 y9
    public void setTypeOfunit( GameObject unit)
    {
        
        GameObject newUnit =  Instantiate(unit,this.typeOfunitCurectlyHaving.transform.position, Quaternion.Euler(0, 0, 0),this.typeOfunitCurectlyHaving.transform);
        Destroy(typeOfunitCurectlyHaving);
        newUnit.transform.parent = this.transform;
        this.typeOfunitCurectlyHaving = this.gameObject.transform.GetChild(0).gameObject;

        Debug.Log("name of new unit: " +newUnit.name);
    }
    public void unSetUnit()
    {
        Debug.Log("unset");
        try
        {
            Destroy(this.gameObject.transform.GetChild(0).gameObject);

        }
        catch (System.Exception e)
        {
            Debug.Log("exeption no child object is in exitence");
        }
        finally
        {
            this.typeOfunitCurectlyHaving = Instantiate( new GameObject("empty"));
            this.typeOfunitCurectlyHaving.transform.parent = this.transform;
        }
    }



}
