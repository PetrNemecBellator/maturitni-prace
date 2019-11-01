using UnityEngine;

[System.Serializable]
public class TileClick : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite markedGamefield;
    public Sprite emptyGamefield;

    // private int ? groupNumber = null; //groupnumber Y number in player list
    //null == tile is not in group
    private Vector2 tileCoordinates;

    private static bool wasAnyTileClicked = false;


    private bool ithaveBeenClicked = false;
    private GameObject typeOfunitCurectlyHaving;
    private static GameObject lastClikedTile = null;

    private static Vector2 CoordinetsOfPreviusObject;



    public Hrac GetHrac()
    {
        if (this.getChildObject() == null)
        {
            Debug.Log("object nema zadnou jednotku");
            return null;
        }
        Debug.Log("ziskavam jmeno hrace " + this.getChildObject().getPlayer().name);
        return this.getChildObject().getPlayer();
    }
    public void setPlayer(Hrac player)
    {
        this.getChildObject().setPlayer(player);
    }
    public void setGroupnumber(int groupnumber)
    {
        //dodelat funci ve hraci ktera do konkretni skupiny prida hraci
        this.getChildObject().GetComponent<Unit>().setGroupNumber(groupnumber);
        //this.groupNumber = groupnumber;
        //   this.GetHrac().
    }
    public bool getisInGroup()
    {
        if (this.getChildObject() == null)
        {
            return false;
        }
        return (this.getChildObject().GetComponent<Unit>() as Unit == null ? false : !(this.getChildObject().GetComponent<Unit>().getGroupNumber() == null));
    }

    public void groupUnMark()
    {
        this.GetHrac().deSelectGroupOfUnits((int)this.getChildObject().GetComponent<Unit>().getGroupNumber());
    }
    public void groupMark()
    {
        this.GetHrac().selectGroupOfUnits((int)this.getChildObject().GetComponent<Unit>().getGroupNumber());

    }


    protected Vector3 offsetOfunit = new Vector3(0, 0);

    void setOffsetOfUnit(Vector3 offset)
    {
        this.offsetOfunit = offset;
    }
    public Vector3 getOffsetOfunit()
    {
        return this.offsetOfunit;
    }




    private void Awake()
    {
        typeOfunitCurectlyHaving = this.gameObject.transform.GetChild(0).gameObject;

        // Debug.Log("tile child name: "+ typeOfunitCurectlyHaving.name);
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
        Debug.Log("");
        Debug.Log("");
        Debug.Log("");
        Debug.Log("TILECLICK ");
        Debug.Log("tile je ve skupine: " + this.getisInGroup());
        // Debug.Log("******Souradnice jednotky: " + this.getCoordinatesOfTheChild() + " souradnice tilecliku "); //+ this.getCoordinates);
        try
        {


            Debug.Log("*****Jmeno child objectu: " + this.getChildObject());
            Debug.Log("**************JMENO HRACE: " + this.getChildObject().getPlayer().ToString());
            Debug.Log("**************cisloSkupiny: " + this.getChildObject().getGroupNumber());
            Debug.Log($"************sOURADNICE JEDNOTKY {this.getChildObject().getCoordinatesOfUnit()} ");

        }
        catch (System.Exception e)
        {
            Debug.Log("******Jeden z vypisu je null: \n " + e.Message.ToString() + "\n radek " + e.StackTrace.ToString());
        }

        if (lastClikedTile != null)
        {
            Debug.Log($"je tile ve skupine ?? {this.getisInGroup()} je na posledy clickly tile ve skupine {lastClikedTile.GetComponent<TileClick>().getisInGroup()}");

        }
        else
        {
            Debug.Log($"je tile ve skupine ?? {this.getisInGroup()} je na posledy clickly tile ve skupine NULL");

        }

        if (ithaveBeenClicked)
        {



            if (getisInGroup())
            {
                this.groupUnMark();
            }
            else
            {
                this.changeToUnMark();
            }




            try
            {
                Debug.Log("!!!!!!!!!!!!!!child object:" + this.transform.GetChild(1).name);
                Debug.Log("!!!!!!!!!!!!!!child object groupnumber:" + this.transform.GetChild(1).GetComponent<Unit>().getGroupNumber());
                Debug.Log("!!!!!!!!!!!!!!child object player:" + this.transform.GetChild(1).GetComponent<Unit>().getPlayer().name);
                Debug.Log("!!!!!!!!!!!!!!child typeOfUnitCurentlyHaving:" + this.typeOfunitCurectlyHaving);


            }
            catch { }
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
                Mathf.Abs((this.getCoordinates().y - CoordinetsOfPreviusObject.y)) <= 1) {

                Unit attacker = lastClikedTile.transform.GetChild(1).gameObject.GetComponent<Unit>() as Unit;
                Unit passiveunit = this.gameObject.transform.GetChild(this.gameObject.transform.childCount - 1).GetComponent<Unit>() as Unit;

                //  Debug.Log("attacke: " + attacker.ToString() + " passive unit: " + passiveunit.ToString());
                if (attacker != null && passiveunit != null)
                {
                    if (attacker.getPlayer() == passiveunit.getPlayer())
                    {
                        Debug.Log("Same player unit");
                        this.changeToUnMark();
                        lastClikedTile.GetComponent<TileClick>().changeToUnMark();
                        goto endOfClick; //ends attack rutine becouse units are same team
                    }

                    Debug.Log("utok normalni utokkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk");
                    Debug.Log("attacker name: " + attacker.gameObject.name);
                    Debug.Log("passive unit name: " + passiveunit.gameObject.name);


                    Unit winner = Unit.unitFight(attacker, passiveunit);
                    if (winner == null)
                    {
                        //bouth loses
                        Destroy(attacker.gameObject);
                        Destroy(passiveunit.gameObject);
                    }
                    else
                    {

                        Debug.Log("vitez je: " + winner.gameObject.name);

                        //spawnMovedUnit(winner.transform.parent.transform.gameObject);


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
                    Debug.Log("predchoziblock " + lastClikedTile.transform.GetChild(1).gameObject.name + " X " + lastClikedTile.transform.position.x + " y " + lastClikedTile.transform.position.y);
                    // Debug.Log("aktualni block click" + this.GetComponent<GameObject>().name);

                    if (lastClikedTile.GetComponent<TileClick>().getisInGroup())
                    {
                        Debug.Log("SKUPINOVY POHYB ZACINA");

                        Vector2 movementVector = new Vector2(this.getCoordinates().x - lastClikedTile.GetComponent<TileClick>().getCoordinates().x,
                        this.getCoordinates().y - lastClikedTile.GetComponent<TileClick>().getCoordinates().y);
                        Debug.Log("vsechnny jednotky se posunou o: " + movementVector);
                        spawnMovedUnit(lastClikedTile, movementVector, clickedTile: this.getCoordinates());

                       // mapCreation.helpFieldForTileActualization = new System.Collections.Generic.List<Vector2>();//vynulovani pomocneho pole
                        Debug.Log("KONEC PRESUNU JEDNOTEK");


                    }
                    else
                    {
                        spawnMovedUnit(lastClikedTile);

                    }



                }


            }
            if (getisInGroup())
            {
                this.groupMark();
            }
            else
            {
                this.changeToMarked();

            }

        }
    endOfClick:;
    }
    public void setTypeOfUnitCurentlyHavingAfterMovement()
    {
        if (this.transform.childCount > 2) throw new System.Exception($"this function is cooled only in situtation \n when all units have been moved. Named of object who made exeption" +
            $" {this.gameObject.name} cooridnate {this.transform.localPosition} chidl count {this.transform.childCount}");
        this.typeOfunitCurectlyHaving = this.gameObject.transform.GetChild(1).gameObject;
        Debug.Log($"new typeOfUnitCurentlyHaving is {this.gameObject.transform.GetChild(1).gameObject}");
        Debug.Log($"new typeOfUnitCurentlyHaving is {this.typeOfunitCurectlyHaving}");
    }
    public  void spawnMovedUnit(GameObject lastclikedTile, Vector2 vectorOfMovement,Vector2 clickedTile)
    {
        //funkce ktera ve3kere jednotky posune o velikost vectoru vectorOfNMovement
        lastclikedTile.GetComponent<TileClick>().GetHrac().moveUnitsInGroup((int)lastclikedTile.GetComponent<TileClick>().getChildObject().getGroupNumber(),vectorOfmovement:vectorOfMovement, clickedTile:clickedTile);
 
    }

    public void spawnMovedUnit(GameObject lastClikedTile)
    {
        Debug.Log("jmeno naposledy klikleho objektu " + lastClikedTile.name);

        
        TileClick scriptOfLastClickedTile = lastClikedTile.GetComponent<TileClick>();

        try
        {
            Debug.Log("name of actual child object" + this.transform.GetChild(1).gameObject.name);

            Debug.Log("jmeno naposledy klikleho objektu " + lastClikedTile.GetComponent<TileClick>().getChildObject().name);

            Debug.Log("child of last clicked object " + scriptOfLastClickedTile.GetComponent<TileClick>().getChildObject().name);


            this.setTypeOfunit(scriptOfLastClickedTile.getChildObject().gameObject);


            scriptOfLastClickedTile.unSetUnit();
            scriptOfLastClickedTile.changeToUnMark();
            lastClikedTile = null;

        }
        catch (System.Exception e)
        {
            Debug.Log("non child on current object " + e.Message);
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
    public void setUnitCoordinates(Vector2 value)
    {
       
        this.typeOfunitCurectlyHaving.GetComponent<Unit>().setCoordinatesOfunit(value);
    }
    public Vector2 getCoordinates()
    {
        return this.tileCoordinates;
    }
    public Vector2 getCoordinatesOfTheChild()
    {
        // Debug.Log($"typeOfunitCurectliHaving {this.typeOfunitCurectlyHaving.ToString()} ");
        //pokud je tu vice jednotek tak co jako
        Debug.Log($"getCoordinets child count {this.transform.GetChildCount()}");
        Debug.Log($"child n 0 {this.transform.GetChild(0).name}");
        Debug.Log($"child n 1 {this.transform.GetChild(1).name}");


        if (this.transform.GetChildCount() > 2)
        {
            Debug.Log(this.transform.GetChild(1).gameObject.name);
            Debug.Log(this.transform.GetChild(2).gameObject.name);
            return this.transform.GetChild(1).gameObject.GetComponent<Unit>().getCoordinatesOfUnit();
        }
        //Debug.Log($"jmeno objektu ze ktereho ziskavam souradnice {this.name}");
        return this.transform.GetChild(this.transform.childCount-1).GetComponent<Unit>().getCoordinatesOfUnit();
    }

    public  Vector3 offsetOfUnit(GameObject unit)
    {
          Debug.Log("check of offset: X " + unit.GetComponent<Unit>().getoffset().x + " Y:" + unit.GetComponent<Unit>().getoffset().y);
          return new Vector3(this.gameObject.transform.position.x + unit.GetComponent<Unit>().getoffset().x, this.gameObject.transform.position.y + unit.GetComponent<Unit>().getoffset().y, 0); //this.gameObject.transform.position.x)

    }
    public Unit getChildObject()
    {
        //child object ktery se ma posunout
        Unit childUnit = (Unit)this.typeOfunitCurectlyHaving.gameObject.GetComponent<Unit>() as Unit;
        Debug.Log("jameno child objectu: " + this.typeOfunitCurectlyHaving.name);
        if (childUnit == null)
        {
            
            Debug.Log("non child unit");
            return null;
        }
       
        return childUnit;
    }

    public void setTypeOfunitNOTForInit(GameObject attackingObject)
    {
        //this = destination object 
        //pridat manualni kopirovani vlastnosti
        Debug.Log($"stakovani jednotek kontrola {(this.gameObject.transform.GetComponent<Unit>() != null)} \n " +
            $"pocet deti {this.transform.childCount} get tile click {this.gameObject.transform.GetComponent<TileClick>() != null}");
        if (attackingObject.gameObject.transform.GetComponent<TileClick>() != null) {

        } else {


            Debug.Log("type of placed unit:" + attackingObject.name);
            Debug.Log("name of player: " + attackingObject.transform.parent.GetComponent<TileClick>().GetHrac().name);
            //Debug.Log("name of player: " + unit.GetComponent<Hrac>().name);


            GameObject newUnit = Instantiate(attackingObject, offsetOfUnit(attackingObject), Quaternion.Euler(0, 0, 0), this.transform);


            Unit unitTheNew = newUnit.GetComponent<Unit>();
            unitTheNew = attackingObject.transform.parent.GetComponent<TileClick>().typeOfunitCurectlyHaving.GetComponent<Unit>();


            newUnit.GetComponent<Unit>().setPlayer(attackingObject.transform.parent.GetComponent<TileClick>().GetHrac());
            newUnit.GetComponent<Unit>().setGroupNumber(attackingObject.GetComponent<Unit>().getGroupNumber());


            newUnit.transform.parent = this.transform;
            // this.typeOfunitCurectlyHaving = newUnit;//pokud se stackuje tak co se stane s tim druhym objektem
            //aktualizuje se az po presunu vsech jednotek

            Debug.Log("name of new unit: " + newUnit.name);

            Debug.Log($"55555555555555555555555555attacking object child count {attackingObject.transform.parent.transform.childCount} destination object chidl count {this.transform.childCount}");
            lastClikedTile.GetComponent<TileClick>().unSetUnit();//posledni jednotka
            Debug.Log($"mazani prebytecnych jednotek na attacking objectu {attackingObject.transform.childCount >= 2} \n childcount {attackingObject.transform.childCount}");
            if (attackingObject.transform.parent.transform.childCount >= 2 )//zmenil jsem to z 1 //componentOFWar + 1 = 2
            {
                Debug.Log($"7777777777777777777PODMINKA JE OPRAVDU TRUE attacking object {attackingObject.name} chidl object ktery ma byt znicen {attackingObject.transform.parent.transform.GetChild(1).gameObject.name} ");

                Debug.Log($"this destination Obj count {this.gameObject.transform.parent.GetChildCount()} ");
                Debug.Log($"attacking object {attackingObject.transform.parent.GetChildCount()} ");
                Debug.Log($" destination child 0 {this.transform.parent.GetChild(1).gameObject.name} typeOfUnitCurently having {this.typeOfunitCurectlyHaving}");
                Debug.Log($"attacking child 0 {attackingObject.transform.parent.GetChild(1).name} ");
              //  Debug.Log($"typeOfUnitCurenlyHaving {attackingObject.GetComponent<TileClick>().typeOfunitCurectlyHaving }");
                

                Destroy(attackingObject.transform.parent.transform.GetChild(1).gameObject);

            }
         



            return; 
        }/*
        else {
         //pozor jestli na danem policku uz neco neni potom by se mel v attacking objectu smazat last object
       
        Debug.Log("888888888888 SPECIAL UNIT PLACEMENT");
        Debug.Log("type of placed unit:" + attackingObject.name);
        Debug.Log("name of player: " + attackingObject.transform.parent.GetComponent<TileClick>().GetHrac().name);
        //Debug.Log("name of player: " + unit.GetComponent<Hrac>().name);


        GameObject newUnit = Instantiate(attackingObject, offsetOfUnit(attackingObject), Quaternion.Euler(0, 0, 0), this.transform);

        
        Unit unitTheNew = newUnit.GetComponent<Unit>();
        unitTheNew = attackingObject.transform.parent.GetComponent<TileClick>().typeOfunitCurectlyHaving.GetComponent<Unit>();
        
     
        newUnit.GetComponent<Unit>().setPlayer(attackingObject.transform.parent.GetComponent<TileClick>().GetHrac());
        newUnit.GetComponent<Unit>().setGroupNumber(attackingObject.GetComponent<Unit>().getGroupNumber());
           

        Destroy(this.typeOfunitCurectlyHaving);
        Destroy(this.transform.GetChild(1).gameObject);


        newUnit.transform.parent = this.transform;
        this.typeOfunitCurectlyHaving = newUnit;
        Debug.Log("name of new unit: " + newUnit.name);
        Debug.Log($"55555555555555555555555555attacking object child count {attackingObject.transform.childCount}");

        if (attackingObject.transform.childCount > 1)
            {
                Debug.Log($"7777777777777777777PODMINKA JE OPRAVDU TRUE attacking object {attackingObject.name} chidl object ktery ma byt znicen {attackingObject.transform.GetChild(1).name}");
                Destroy(attackingObject.transform.GetChild(1));

            }
        }*/
    }
    public void setTypeOfunit(GameObject unit){
        //slouží k nastavení jednotek na zacatku hry a pro umistovaní nových jednotek 

        Debug.Log("type of placed unit:" + unit.name);

        GameObject newUnit = Instantiate(unit, offsetOfUnit(unit), Quaternion.Euler(0, 0, 0), this.transform);
        newUnit.GetComponent<Unit>().setCoordinatesOfunit(this.tileCoordinates);
       

      //  Destroy(this.typeOfunitCurectlyHaving);
       // Destroy(this.transform.GetChild(1).gameObject);

     /*   unit.transform.SetParent(this.transform);
        newUnit.transform.parent = this.transform;
        */
        this.typeOfunitCurectlyHaving = newUnit; 
        Debug.Log("name of new unit: " + newUnit.name);
   
    }
    public static void moveUnitWithFightMode(GameObject attackingObj, GameObject destinationObject)//pokud vim kam budu utocit melo by to fungovat
    {
        bool doesTileHaveRedundantUnit = false;
        Unit attacker;
        Debug.Log("fight mode time function ");
        Debug.Log("Attacker " + attackingObj.transform.GetChild(1).name + "child count" + attackingObj.transform.childCount);
        Debug.Log("Pasive obj " + destinationObject.transform.GetChild((destinationObject.transform.childCount-1)).name);

        try
        {

            (attackingObj.transform.GetChild(2).gameObject.GetComponent<TileClick>() as TileClick == null).ToString();//it needs to be there 

            attacker = attackingObj.transform.GetChild(1).gameObject.GetComponent<Unit>();

            Debug.Log("     DO UNIT EXISTS?: " + attacker);
            Debug.Log("     DO UNIT EXISTS?: " + attacker.name);
            doesTileHaveRedundantUnit = true;
        }
        catch
        {
            Debug.Log("catch block there is no redundant thing");
            attacker = attackingObj.gameObject.GetComponent<TileClick>().getChildObject() as Unit;


        }
        Debug.Log($"\\//////////  wTF whay this is doing destinon object if {(destinationObject.GetComponent<Unit>() as Unit != null)}");
        if (destinationObject.GetComponent<Unit>() != null)
        {
            Debug.Log("88888888888888888 NA cilovem policku je jednotka");
            Debug.Log($"destinationObject {destinationObject.transform.GetChild(1).name} attacking object {attacker.transform.GetChild(1).name} ");
            //destinationObject.transform.GetChild(1).transform.SetParent(attackingObj.transform.GetChild(1).transform.parent, false);

            attackingObj.transform.GetChild(1).transform.SetParent(destinationObject.transform.GetChild(0).transform.parent, false);
            //destinationObject.transform.GetChi = attackingObj.transform.GetChild(1);
            attackingObj.GetComponent<TileClick>().unSetUnit();
            return;//snad to bude fungovat 
        }


        Unit passiveunit = destinationObject.gameObject.transform.GetChild(destinationObject.transform.childCount-1).GetComponent<Unit>() as Unit;///GetComponent<TileClick>().getChildObject() as Unit;
        //popremysej jestli neni chyba tady


        //  Debug.Log("attacke: " + attacker.ToString() + " passive unit: " + passiveunit.ToString());

        Debug.Log("attacker null" + (attacker != null).ToString() + " passive " + (passiveunit != null).ToString() );
            try
            {
                Debug.Log("STEJNY team?      ");
                Debug.Log("JETOSTEJNYTEAM??????? to stejny team?" + (attacker.getPlayer() == passiveunit.getPlayer()).ToString());
            }
            catch
            {

            }
            if (attacker != null && passiveunit != null && attacker.getPlayer() != passiveunit.getPlayer() )
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

              
                    TileClick destTile = destinationObject.GetComponent<TileClick>() as TileClick;
                    TileClick attackerTile = attackingObj.GetComponent<TileClick>() as TileClick;

                    attackerTile.unSetUnit();
                    destTile.setTypeOfunit(g);

                    Destroy(g);
               
                }

            }
            else{
               // unitMovementSwapOfGameUnits(attackingObj, destinationObject,doesTileHaveRedundantUnit);
               Debug.Log("Jen p5esun jednotky");


                 if (!doesTileHaveRedundantUnit){
                        destinationObject.gameObject.GetComponent<TileClick>().setTypeOfunitNOTForInit(attackingObj.GetComponent<TileClick>().getChildObject().gameObject);
                        //type of unit curently having je new i kdyz by nemel
                        attackingObj.gameObject.GetComponent<TileClick>().unSetUnit();
                }else {
                        destinationObject.gameObject.GetComponent<TileClick>().setTypeOfunitNOTForInit(attackingObj.transform.GetChild(1).gameObject);
                }
            }
        
    }
    

    


    public void setTypeOfunit(GameObject unit, Vector2 coordinates)//use less
    {
        Debug.Log("type of placed unit:" + unit.name);

        unit.transform.position = coordinates;

        GameObject newUnit = Instantiate(unit,offsetOfUnit(unit), Quaternion.Euler(0, 0, 0), this.transform);
        Destroy(this.typeOfunitCurectlyHaving);
        Destroy(this.transform.GetChild(1).gameObject);

        newUnit.transform.parent = this.transform;

        //this.gameObject.transform.GetChild(1).gameObject;

        Debug.Log("name of new unit: " + newUnit.name);
    }
    public void unSetUnit()
    {
        //pokud jsou na objektu uz jen dva objekty muze se nastavit type of unit curently having
        if (this.gameObject.transform.childCount >= 2) {
            Debug.Log($"unset name of unit: {this.gameObject.transform.GetChild(1).gameObject.name} ");
            Destroy(this.gameObject.transform.GetChild(1).gameObject);//smazani predchoziho objektu
            this.typeOfunitCurectlyHaving = this.gameObject.transform.GetChild(1).gameObject; //nastaveni aktualniho objektu
          
        }
       /* else if(this.gameObject.transform.childCount == 1)
        {
            this.typeOfunitCurectlyHaving = this.gameObject.transform.GetChild(0).gameObject;
        }

       /* try
        {
            Debug.Log("");
            Debug.Log("unset object: " + this.getChildObject().name);
            Destroy(this.getChildObject().gameObject);
        }
        catch (System.Exception e)
        {
            Debug.Log("exeption no child object is in exitence " + e.Message);
        }
        /*finally
        {
            
            this.typeOfunitCurectlyHaving = Instantiate(new GameObject("empty"),new Vector3(this.gameObject.GetComponent<Transform>().localPosition.x,
                this.gameObject.GetComponent<Transform>().localPosition.y,
                this.gameObject.GetComponent<Transform>().localPosition.z), new Quaternion(0,0,0,0) ,this.transform);
            
            this.typeOfunitCurectlyHaving.transform.parent = this.transform;
            this.typeOfunitCurectlyHaving.transform.position = new Vector3(0, 0, 0);//reset souradnic na 00000 sakr a nefunguje to
            
        }*/
    }

    private static void unitMovementSwapOfGameUnits(GameObject activeUnit, GameObject passiveUnit, bool doesTileHaveRedundantunit = true)
    {

        //prohodit souradnice a transform parent
        //odstranit niceni objektuuuuu
        //znicit objekt jen pokud se jedna o souboj


        GameObject activeChild = activeUnit.transform.GetChild(1).gameObject;
        GameObject passiveChild = passiveUnit.transform.GetChild(1).gameObject;



        Transform helpTransform;
        Vector2 helpCoordinates;
        if (!doesTileHaveRedundantunit)
        {
            Debug.Log("TADY nastane pad");
            Debug.Log(activeUnit.name);

            Debug.Log(activeUnit.GetComponent<TileClick>());
            Debug.Log(activeUnit.GetComponent<TileClick>().getChildObject());
            Debug.Log("Chiled object: " + activeUnit.transform.GetChild(1).gameObject);

            //toto je spatne
            activeChild = activeUnit.GetComponent<TileClick>().getChildObject().gameObject;//neni to prohozen get child 0 s getchildobject  
            //activeChild.GetComponent<TileClick>().typeOfunitCurectlyHaving = activeChild.GetComponent<TileClick>;
            //typeofunitCurentlyHaving je potreba poresit nejak
        }

        Debug.Log("prohazuju transform active: " + activeChild.name + " pasive: " + passiveChild.name);
        Debug.Log("active unit:" + activeChild.transform.parent.transform);
        Debug.Log("passive unit: " + passiveChild.transform.parent.transform);

        helpTransform = activeChild.transform.parent;
        //child kontorlo debug log

        activeChild.transform.SetParent(passiveChild.transform.parent, false);
        passiveChild.transform.SetParent(helpTransform, false);

        Debug.Log($"activeChild unit name {activeChild.name}");
        Debug.Log($"passive unit name {passiveChild.name}");


        Debug.Log($"*******************active chidl {activeChild.transform.parent.transform.localRotation}");
        Debug.Log($"*******************passive chidl {passiveChild.transform.parent.transform.localRotation}");

        Debug.Log($"*******************active chidl {activeChild.transform.parent.transform.rotation}");
        Debug.Log($"*******************passive chidl {passiveChild.transform.parent.transform.rotation}");



        if (activeChild.transform.parent.transform.rotation.eulerAngles.z == 180)
        {
            Debug.Log("Je tech 180 stupnu TRUE");
            activeChild.transform.rotation = Quaternion.Euler(
                0,
                0,
                180);
        }
        if (passiveChild.transform.parent.transform.rotation.z == 180)
        {
            Debug.Log("Je tech 180 stupnu TRUE");

            passiveChild.transform.rotation = Quaternion.Euler(0, 0, 180);
        }


        Debug.Log("Po prohozeni transformu");
        Debug.Log("active unit:" + activeChild.transform.parent.transform);
        Debug.Log("passive unit: " + passiveChild.transform.parent.transform);

        Debug.Log("");
        Debug.Log("");
        Debug.Log("");

        Debug.Log("**Pred prohozem souradnic");
        Debug.Log("active unit:" + activeChild.transform.localPosition);
        Debug.Log("passive unit: " + passiveChild.transform.localPosition);
        //zmena souradnic
        helpTransform = activeChild.transform;
        activeChild.transform.localPosition = passiveChild.transform.localPosition;
        passiveChild.transform.localPosition = helpTransform.transform.localPosition;

        Debug.Log("**PO prohozeni souradnic");
        Debug.Log("active unit:" + activeChild.transform.localPosition);
        Debug.Log("passive unit: " + passiveChild.transform.localPosition);
        Debug.Log("passive unit: " + passiveChild.name);

        //tohle to asi rozbije ale nwm
        helpCoordinates = activeUnit.gameObject.GetComponent<TileClick>().getCoordinates();

        activeUnit.GetComponent<TileClick>().setTileCoordinates(passiveUnit.GetComponent<TileClick>().getCoordinates());
        passiveUnit.GetComponent<TileClick>().setTileCoordinates(helpCoordinates);

        activeUnit.GetComponent<TileClick>().changeToUnMark();
        passiveUnit.GetComponent<TileClick>().changeToUnMark();
        // }
    }


}
