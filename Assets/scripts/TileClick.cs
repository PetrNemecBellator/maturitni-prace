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

    public GameObject originalEmtyUnitLayer;
    private static Vector2 CoordinetsOfPreviusObject;


    private void Awake()
    {
        typeOfunitCurectlyHaving = this.gameObject.transform.GetChild(0).gameObject;
        
        Debug.Log("tile child name: "+ typeOfunitCurectlyHaving.name);
    }
    void OnMouseDown()
    {
        Debug.Log("click on obejct rectangle");
        Debug.Log("");
        Debug.Log("x: " + this.tileCoordinates.x + " y: " + this.tileCoordinates.y);
        Debug.Log("");
        if (ithaveBeenClicked)
        {           
            changeToUnMark();
             if (CoordinetsOfPreviusObject.x== this.getCoordinates().x && CoordinetsOfPreviusObject.y == this.getCoordinates().y)
                {
                    //restart previus coordinates
                }


            try
            {
                Debug.Log("child object:" + this.transform.GetChild(0).name);
            }
            finally
            {

            }
        }
        else
        {
            Debug.Log("co nefunguje? wasAnytileClicked" + wasAnyTileClicked.ToString() + "&&" + (lastClikedTile != null).ToString());

         

        
            Debug.Log("was any tile clicked? " + wasAnyTileClicked.ToString());
            if (!wasAnyTileClicked)
            {

                lastClikedTile = this.gameObject;
                CoordinetsOfPreviusObject = this.getCoordinates();
                wasAnyTileClicked = true;
            }
            Debug.Log("wasAnyTileClicked: " + wasAnyTileClicked + "lastClicked" + lastClikedTile != null + "this?" + lastClikedTile != this.gameObject);
            Debug.Log(wasAnyTileClicked && lastClikedTile != null && lastClikedTile != this.gameObject);
            Debug.Log("abs condititon " +   ( Mathf.Abs(this.getCoordinates().x - CoordinetsOfPreviusObject.x) <= 1).ToString() + " " +
                (Mathf.Abs((this.getCoordinates().y - CoordinetsOfPreviusObject.y)) <= 1).ToString());
            Debug.Log("abs condititon " + Mathf.Abs(this.getCoordinates().x - CoordinetsOfPreviusObject.x).ToString() + " " +
                (Mathf.Abs((this.getCoordinates().y - CoordinetsOfPreviusObject.y))).ToString());

            if (wasAnyTileClicked && lastClikedTile != null && lastClikedTile != this.gameObject &&
                Mathf.Abs(this.getCoordinates().x - CoordinetsOfPreviusObject.x) <= 1 &&
                Mathf.Abs((this.getCoordinates().y - CoordinetsOfPreviusObject.y)) <=1)
            {


                //staticky buffer na ukladani dat z naposledy klikleho bloku
                //nejednodusi metoda prenosu dat pravdepodobne
                Debug.Log("child object:" + this.transform.GetChild(0).name);//gets object storing war unit

                Debug.Log("bufferujeme na bloku X" + this.tileCoordinates.x + "y: " + this.tileCoordinates.y);
                Debug.Log("predchoziblock " + lastClikedTile.transform.GetChild(0).gameObject.name + " X " + lastClikedTile.transform.position.x + " y " + lastClikedTile.transform.position.y);
               // Debug.Log("aktualni block click" + this.GetComponent<GameObject>().name);
                TileClick scriptOfLastClickedTile = lastClikedTile.GetComponent<TileClick>();

                try
                {
                    scriptOfLastClickedTile.unSetUnit();
                    scriptOfLastClickedTile.changeToUnMark();
                    lastClikedTile = null;

                    this.setTypeOfunit(scriptOfLastClickedTile.transform.GetChild(0).gameObject);
                 
                    Debug.Log("name of actual child object" + this.transform.GetChild(0).gameObject);

                }
                catch(System.Exception e)
                {
                    Debug.Log("non child on current object");
                }
                finally
                {
                   


                }


            }
            
           
            
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
        //take spatne
        this.tileCoordinates = value;
    }
    public Vector2 getCoordinates()
    {
        //toto je spatne koordinaty jsou ulozeny pod this.transfom.position.x
        return this.tileCoordinates;
    }
    //x10 y9
    public void setTypeOfunit( GameObject unit)
    {
        
        GameObject newUnit =  Instantiate(unit,new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.x) 
            , Quaternion.Euler(0, 0, 0),this.transform);
        Destroy(typeOfunitCurectlyHaving); 
        newUnit.transform.parent = this.transform;
       
        //this.gameObject.transform.GetChild(0).gameObject;

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
            
            this.typeOfunitCurectlyHaving = Instantiate(new GameObject("empty"),new Vector3(this.gameObject.GetComponent<Transform>().localPosition.x, this.gameObject.GetComponent<Transform>().localPosition.y,
                this.gameObject.GetComponent<Transform>().localPosition.z), new Quaternion(0,0,0,0) ,this.transform);
            
           /* this.typeOfunitCurectlyHaving.transform.parent = this.transform;
            this.typeOfunitCurectlyHaving.transform.position = new Vector3(0, 0, 0);//reset souradnic na 00000 sakr a nefunguje to
            */
        }
    }



}
