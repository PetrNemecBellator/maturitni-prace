using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class TileClick : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite markedGamefield;
    public Sprite emptyGamefield;
    private Vector2 tileCoordinates;
    private bool isInGroop;

    public bool getisInGroup()
    {
        return this.isInGroop;
    }
    public void setIsInGroup(bool isInGroup)
    {
        this.isInGroop = isInGroup;
    }
    
    protected Vector3 offsetOfunit =new Vector3(0,0);

     void setOffsetOfUnit(Vector3 offset)
    {
        this.offsetOfunit = offset;
    }
    public Vector3 getOffsetOfunit()
    {
        return this.offsetOfunit;
    }


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

   /*public Unit getClassOfUnit(GameObject unit)
    {
        if (unit.GetComponent<Vez>() as Vez == null)
        {
            if (unit.GetComponent<Stit>() as Stit == null)
            {
                if(unit.GetComponent<Sip>() as Sip)
                {
                    Debug.Log("empty game field");
                    return null;
                }
                Debug.Log("je to sip");
                return unit.GetComponent<Sip>();
            }
            Debug.Log("je to stit");
            return unit.GetComponent<Stit>();

        }
        Debug.Log("je to vez");
        return unit.GetComponent<Vez>();
    }*/
    void OnMouseDown()
    {
    
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
        

            Debug.Log("was any tile clicked? " + wasAnyTileClicked.ToString());
            if (!wasAnyTileClicked)
            {

                lastClikedTile = this.gameObject;
                CoordinetsOfPreviusObject = this.getCoordinates();
                wasAnyTileClicked = true;
            }
            Debug.Log("wasAnyTileClicked: " + wasAnyTileClicked + "lastClicked" + lastClikedTile != null + "this?" + lastClikedTile != this.gameObject);
            Debug.Log(wasAnyTileClicked && lastClikedTile != null && lastClikedTile != this.gameObject);
         

            if (wasAnyTileClicked && lastClikedTile != null && lastClikedTile != this.gameObject &&
                Mathf.Abs(this.getCoordinates().x - CoordinetsOfPreviusObject.x) <= 1 &&
                Mathf.Abs((this.getCoordinates().y - CoordinetsOfPreviusObject.y)) <= 1)
            {
                Unit attacker = lastClikedTile.transform.GetChild(0).gameObject.GetComponent<Unit>() as Unit;
                Unit passiveunit = this.gameObject.transform.GetChild(0).GetComponent<Unit>() as Unit;
                //  Debug.Log("attacke: " + attacker.ToString() + " passive unit: " + passiveunit.ToString());
                if (attacker != null && passiveunit != null)
                {

                    Debug.Log("utok normalni utokkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk");
                    Debug.Log("attacker name: " + attacker.gameObject.name );
                    Debug.Log("passive unit name: " + passiveunit.gameObject.name);

                    
                    Unit  winner =  Unit.unitFight(attacker, passiveunit);
                    if (winner == null)
                    {
                        //bouth loses
                        Destroy(attacker.gameObject);
                        Destroy(passiveunit.gameObject);
                    }
                    else
                    {
                      
                        Debug.Log("vitez je: " + winner.gameObject.name);

                        spawnMovedUnit(winner.transform.parent.transform.gameObject);


                    }

                }
                else
                {



                    Debug.Log("this object type ");
                    Debug.Log(this.gameObject.GetType().ToString());

                        //staticky buffer na ukladani dat z naposledy klikleho bloku
                        //nejednodusi metoda prenosu dat pravdepodobne
                        Debug.Log("child object:" + this.transform.GetChild(0).name);//gets object storing war unit

                        Debug.Log("bufferujeme na bloku X" + this.tileCoordinates.x + "y: " + this.tileCoordinates.y);
                        Debug.Log("predchoziblock " + lastClikedTile.transform.GetChild(0).gameObject.name + " X " + lastClikedTile.transform.position.x + " y " + lastClikedTile.transform.position.y);
                        // Debug.Log("aktualni block click" + this.GetComponent<GameObject>().name);
                        spawnMovedUnit(lastClikedTile);





                }

            
          }
            changeToMarked();
        }
    }
    public  void spawnMovedUnit(GameObject lastclikedTile, Vector2 coordinates)
    {
        Debug.Log("jmeno naposledy klikleho objektu " + lastClikedTile.name);


        TileClick scriptOfLastClickedTile = lastClikedTile.GetComponent<TileClick>();

        try
        {
            Debug.Log("name of actual child object" + this.transform.GetChild(0).gameObject.name);

            Debug.Log("jmeno naposledy klikleho objektu " + lastClikedTile.name);

            Debug.Log("Jmeno bug objektu: " + scriptOfLastClickedTile.transform.gameObject.transform.GetChild(0).gameObject.name);


            this.setTypeOfunit(scriptOfLastClickedTile.transform.GetChild(0).gameObject,coordinates);


            scriptOfLastClickedTile.unSetUnit();
            scriptOfLastClickedTile.changeToUnMark();
            lastClikedTile = null;

        }
        catch (System.Exception e)
        {
            Debug.Log("non child on current object");
        }
    }











   
    public void spawnMovedUnit(GameObject lastClikedTile)
    {
        Debug.Log("jmeno naposledy klikleho objektu " + lastClikedTile.name);

        
        TileClick scriptOfLastClickedTile = lastClikedTile.GetComponent<TileClick>();

        try
        {
            Debug.Log("name of actual child object" + this.transform.GetChild(0).gameObject.name);

            Debug.Log("jmeno naposledy klikleho objektu " + lastClikedTile.name);

            Debug.Log("child of last clicked object " + scriptOfLastClickedTile.transform.gameObject.transform.GetChild(0).gameObject.name);


            this.setTypeOfunit(scriptOfLastClickedTile.transform.GetChild(0).gameObject);


            scriptOfLastClickedTile.unSetUnit();
            scriptOfLastClickedTile.changeToUnMark();
            lastClikedTile = null;

        }
        catch (System.Exception e)
        {
            Debug.Log("non child on current object");
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
        return this.tileCoordinates;
    }

    public  Vector3 offsetOfUnit(GameObject unit)
    {
          Debug.Log("check of offset: X " + unit.GetComponent<Unit>().getoffset().x + " Y:" + unit.GetComponent<Unit>().getoffset().y);
          return new Vector3(this.gameObject.transform.position.x + unit.GetComponent<Unit>().getoffset().x, this.gameObject.transform.position.y + unit.GetComponent<Unit>().getoffset().y, 0); //this.gameObject.transform.position.x)


    }
    public Unit getChildObject()
    {
        Unit childUnit = (Unit)this.typeOfunitCurectlyHaving.gameObject.GetComponent<Unit>() as Unit;
        Debug.Log("jameno child objectu: " + this.typeOfunitCurectlyHaving.name);
        if (childUnit == null)
        {
            
            Debug.Log("non child unit");
            return null;
        }
        Debug.Log("name of nonchild: " + childUnit.name);
        return childUnit;
    }


    public void setTypeOfunit(GameObject unit)
    {
        Debug.Log("type of placed unit:" + unit.name);
        
        GameObject newUnit = Instantiate(unit, offsetOfUnit(unit), Quaternion.Euler(0, 0, 0), this.transform);
        Destroy(this.typeOfunitCurectlyHaving);
        Destroy(this.transform.GetChild(0).gameObject);

        newUnit.transform.parent = this.transform;
        this.typeOfunitCurectlyHaving = newUnit; //nove pridano netestovano

        //this.gameObject.transform.GetChild(0).gameObject;

        Debug.Log("name of new unit: " + newUnit.name);
      //  Debug.Log("x: " + newUnit.gameObject.GetComponent<TileClick>().getCoordinates().x + "y: " + newUnit.gameObject.GetComponent<TileClick>().getCoordinates().y);
    }
    public static void moveUnitWithFightMode(GameObject attackingObj, GameObject destinationObject)//pokud vim kam budu utocit melo by to fungovat
    {
        /*
        chyby konrola vzdalenosi
    */
        Debug.Log("fight mode time function ");
        Debug.Log("Attacker " + attackingObj.transform.GetChild(0).name);
        Debug.Log("Pasive obj " + destinationObject.transform.GetChild(0).name);

        Unit attacker = attackingObj.gameObject.GetComponent<TileClick>().getChildObject() as Unit;
        Unit passiveunit = destinationObject.gameObject.GetComponent<TileClick>().getChildObject() as Unit;
        //  Debug.Log("attacke: " + attacker.ToString() + " passive unit: " + passiveunit.ToString());

        Debug.Log("attacker null" + (attacker != null).ToString() + " passive " + (passiveunit != null).ToString() );

        if (attacker != null && passiveunit != null)
        {

            Debug.Log("utok AUTOMATICKY UTOKkkkkkkkkkkkkk");
            Debug.Log("Attacker " + attacker.name);
            Debug.Log("Pasive obj " + passiveunit.name);

            Unit unit = Unit.unitFight(attacker, passiveunit);
            if (unit == null)
            {
                //bouth loses
                Destroy(attacker);
                Destroy(passiveunit);
                
            }
            else
            {
               
                //Debug.Log("vitez je: " + winner.gameObject.name);
                GameObject g = unit.gameObject;//\.transform.parent.transform.gameObject;

                Debug.Log("type of placed unit:" + g.name);
                Debug.Log(g.transform.parent.GetComponent<TileClick>().offsetOfUnit(g));
                Debug.Log(destinationObject.GetComponentInParent<Transform>());


                /* GameObject newUnit = Instantiate(g, g.GetComponentInParent<TileClick>().offsetOfUnit(g), Quaternion.Euler(0, 0, 0), destinationObject.transform);
               //  Destroy(destinationObject.gameObject.GetComponent<TileClick>().typeOfunitCurectlyHaving);

                 destinationObject.GetComponent<TileClick>().typeOfunitCurectlyHaving = newUnit;
                 newUnit.transform.parent = destinationObject.transform; //posunu objekt na pozici destination tile

                 Destroy(destinationObject.gameObject.transform.GetChild(0).gameObject);
                 Destroy(g);


                 Debug.Log("name by fight move new unit: " + newUnit.name);
             */
                TileClick destTile = destinationObject.GetComponent<TileClick>() as TileClick;
                TileClick attackerTile = attackingObj.GetComponent<TileClick>() as TileClick;

                attackerTile.unSetUnit();
                destTile.setTypeOfunit(g);

                Destroy(g);
               
            }

        }
        else{
            Debug.Log("Jen p5esun jednotky");
           // destinationObject.gameObject.GetComponent<TileClick>().setTypeOfunit(attackingObj.transform.GetChild(0).gameObject);
            attackingObj.gameObject.GetComponent<TileClick>().unSetUnit();

        }
        
    }



    public void setTypeOfunit(GameObject unit, Vector2 coordinates)//use less
    {
        Debug.Log("type of placed unit:" + unit.name);

        unit.transform.position = coordinates;

        GameObject newUnit = Instantiate(unit,offsetOfUnit(unit), Quaternion.Euler(0, 0, 0), this.transform);
        Destroy(this.typeOfunitCurectlyHaving);
        Destroy(this.transform.GetChild(0).gameObject);

        newUnit.transform.parent = this.transform;

        //this.gameObject.transform.GetChild(0).gameObject;

        Debug.Log("name of new unit: " + newUnit.name);
    }
    public void unSetUnit()
    {
        Debug.Log("unset");
        try
        {
            Debug.Log("unset object: " + this.getChildObject().name);
            Destroy(this.getChildObject().gameObject);
        }
        catch (System.Exception e)
        {
            Debug.Log("exeption no child object is in exitence");
        }
        finally
        {
            
            this.typeOfunitCurectlyHaving = Instantiate(new GameObject("empty"),new Vector3(this.gameObject.GetComponent<Transform>().localPosition.x, this.gameObject.GetComponent<Transform>().localPosition.y,
                this.gameObject.GetComponent<Transform>().localPosition.z), new Quaternion(0,0,0,0) ,this.transform);
            
            this.typeOfunitCurectlyHaving.transform.parent = this.transform;
            this.typeOfunitCurectlyHaving.transform.position = new Vector3(0, 0, 0);//reset souradnic na 00000 sakr a nefunguje to
            
        }
    }
  



}
