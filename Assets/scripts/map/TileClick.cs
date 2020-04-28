using UnityEngine;

[System.Serializable]
public class TileClick : MonoBehaviour
{
    public Sprite markedGamefield;
    public Sprite emptyGamefield;
    public Sprite rechAbleSprit;

    private Vector2 tileCoordinatesInMatrix;
    private Vector3 coordinetsForDistanceCalculation;

    private static bool wasAnyTileClicked = false;

    private bool ithaveBeenClicked = false;

    private GameObject typeOfunitCurectlyHaving = null;
    private static GameObject lastClikedTile = null;
    private GameObject bufferUnit;

    private static Vector2 CoordinetsOfPreviusObject;

    protected Vector3 offsetOfunit = new Vector3(0, 0);

    int? groupNumber1 = 1;
    int? groupNumber2;
   
    private void mark()
    {
        lastClikedTile = this.gameObject;
        CoordinetsOfPreviusObject = this.getCoordinatesInMatrix();
        
        wasAnyTileClicked = true;

        ithaveBeenClicked = false;
        this.groupMark();
        Debug.Log("mark");

        if (this.getTypeOfUnitCurentlyHavin() as GameObject != null)
        {
            groupNumber1 = this.getTypeOfUnitCurentlyHavin().GetComponent<Unit>().getGroupNumber();
        }

    }
    private void unMark() {
        Debug.Log($"****************************************this unmark {this.getCoordinatesInMatrix()}");
        lastClikedTile = null;
    
        wasAnyTileClicked = (false);

        ithaveBeenClicked = true;

        groupNumber1 = -1;
        groupNumber2 = 0;

        this.groupUnMark();
        Debug.Log("unmark");
    }
    public void logicOfmarkingunits()
    {
        if (GameLogic.isItMyUnit(this.typeOfunitCurectlyHaving)){ 

        Debug.Log($"marking unit {!wasAnyTileClicked} || {this.ithaveBeenClicked}");
            if (!wasAnyTileClicked || this.ithaveBeenClicked)
            {
                //mark un mark game units
                if (this.ithaveBeenClicked){
                        Debug.Log("unmark");
                        unMark();
                }
                else{
                    Debug.Log("mark");
                    mark();
                }

            }
        }
    }
    public static Unit moveOfunitsFightMode(TileClick attackerTile, TileClick passiveunitTile)
    {
        /*Unit  = lastClikedTile.transform.GetComponent<TileClick>().getTypeOfUnitCurentlyHavin().transform.GetComponent<Unit>();
        Unit passiveunit = this.typeOfunitCurectlyHaving.transform.GetComponent<Unit>() as Unit;*/
        Unit attacker = attackerTile.getTypeOfUnitCurentlyHavin().GetComponent<Unit>();
        Unit passiveunit = passiveunitTile.getTypeOfUnitCurentlyHavin().GetComponent<Unit>();

        Debug.Log("attacker " + attacker);
        Debug.Log("passive" + passiveunit);

        if (attacker.getPlayer() == passiveunit.getPlayer())
        {
            Debug.Log("Same player unit");
            // this.changeToUnMark();
            //lastClikedTile.GetComponent<TileClick>().changeToUnMark();
            return null; //ends attack rutine becouse units are same team
        }

        Debug.Log("utok normalni utokkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk");
        Debug.Log("attacker name: " + attacker.gameObject.name);
        Debug.Log("passive unit name: " + passiveunit.gameObject.name);

        Unit winner = GameLogic.unitFight(attacker, passiveunit);
        Hrac attackerhrac1 = attacker.getPlayer();
        Hrac passivehrac2 = passiveunit.getPlayer();
        attackerhrac1.deSelectGroupOfUnits( (int)attacker.getGroupNumber());
        passivehrac2.deSelectGroupOfUnits((int)attacker.getGroupNumber());


        if (winner == null) {
            //bouth lose


            //odstraneni jednotek
            attackerhrac1.remuveUnit(attackerTile);
            passivehrac2.remuveUnit(passiveunitTile);
            /* Destroy(attacker.gameObject);
             Destroy(passiveunit.gameObject);*/
            return null;
        }
        Debug.Log("vitez je: " + winner.gameObject.name);
        return winner;
    }
    public void moveUnits(Unit actualUnit,TileClick lastClickedTileTile, Hrac actualPlayer)
    {
        Debug.Log("pohyb");
        Debug.Log(actualUnit.getGroupNumber() != null);
        if (actualUnit.getGroupNumber() != null)// it is not allowed to be null
        {
            Hrac h1 = actualPlayer;
            Hrac h2;
            try { 
                h2 = this.getTypeOfUnitCurentlyHavin().GetComponent<Unit>().getPlayer() as Hrac;
            }
            catch
            {
                h2 = null;
            }

            if (!(h1 == null || h2 == null)) { 
            if (h1 == h2)
                {
                    //pohyb neprobene
                    return;
                }
            }

            Debug.Log("pohyb jednotek a tentokrat doopravdy");
            Debug.Log(actualUnit);

            int groupNumber = (int)actualUnit.getGroupNumber();
            Vector2 direction = new Vector2(this.getCoordinatesInMatrix().x - lastClickedTileTile.getCoordinatesInMatrix().x,
                this.getCoordinatesInMatrix().y - lastClickedTileTile.getCoordinatesInMatrix().y);

            actualPlayer.moveUnitsInGroup(groupNumber, direction);
        }
        else
        {
            Debug.Log(actualUnit.ToString());
            throw new System.Exception("Každý tile by měl mít přidělenou skupinu");
        }
    }
    void OnMouseDown()//kliknuti na display nebo myší
    {
        Debug.Log("");
        Debug.Log("");
        Debug.Log("");
        Debug.Log("TILECLICK ");
        Debug.Log(this.typeOfunitCurectlyHaving);
 
        //tady opravdu nen9 spr8vne odliseno jestli se jedn8 o klik na prazn7 tzle klik jenda nebo jestli se jedna o klik dva nevim proc to tu je moc
        //if ktery aktivuje click jen kdyz je na aktulnim tzlu jednotka
        if (this.getTypeOfunitCurentlyHaving() as GameObject != null && lastClikedTile != null)
        {
            groupNumber1 = lastClikedTile.GetComponent<TileClick>().getTypeOfUnitCurentlyHavin().GetComponent<Unit>().getGroupNumber();
            groupNumber2 = this.getTypeOfUnitCurentlyHavin().GetComponent<Unit>().getGroupNumber();
        }
        Debug.Log($"markingUnit {groupNumber1 == groupNumber2 && lastClikedTile as GameObject != null}");

        Debug.Log($"ma se provest klik {!(this.getTypeOfUnitCurentlyHavin() as GameObject == null)} ");

        Debug.Log($"proc se jedna o pohyb a ne fight ! {this.getTypeOfunitCurentlyHaving() as GameObject == null} && {wasAnyTileClicked == false }");
        try
        {
            Debug.Log(this.typeOfunitCurectlyHaving);
            Debug.Log(this.typeOfunitCurectlyHaving.GetComponent<Unit>());

            Hrac hrac1 = this.typeOfunitCurectlyHaving.GetComponent<Unit>().getPlayer();//
            Hrac hrac2 = lastClikedTile.GetComponent<TileClick>().typeOfunitCurectlyHaving.GetComponent<Unit>().getPlayer();
            Debug.Log(hrac1);
            Debug.Log(hrac2);

            if (hrac1 != hrac2)
            {
                moveOfUnits();
                return;
            }
        }
        catch (System.Exception e) { /*Debug.Log("nejde pohnout jednotkami " + e.StackTrace.ToString()*/};
            
        if (!(this.getTypeOfUnitCurentlyHavin() as GameObject == null && wasAnyTileClicked == false )   )//klikam na prazno
        {  
            if ((lastClikedTile == null || groupNumber1 == groupNumber2)  ) {
                logicOfmarkingunits();//oznaceni jednotek
            }
            else
            {//pohyb jednotek
                moveOfUnits();
            }            
        }

    }
    public void moveOfUnits()
    {
        Debug.Log("pohyb jednotek");

        Hrac actualPlayer = lastClikedTile.GetComponent<TileClick>().typeOfunitCurectlyHaving.GetComponent<Unit>().getPlayer();
        Unit actualUnit = lastClikedTile.GetComponent<TileClick>().typeOfunitCurectlyHaving.GetComponent<Unit>();
        TileClick lastClickedTileTile = lastClikedTile.GetComponent<TileClick>();

        //je to moc daleko
        if (!GameLogic.isDistanceReacheble(lastClickedTileTile, this, actualPlayer.getNumberOfunitsInGroup(actualUnit.getGroupNumber())))
        {
            return;
        }

        //pokud je podminka splnena jedná se o souboj
        Debug.Log($"6666666666 fight {!this.doTileHaveUnit()}");
        /*if (!this.doTileHaveUnit())
        {
            moveOfunitsFightMode();
        }
        else
        {*/
          //  Debug.Log("pohyb jednotek");

            moveUnits(actualUnit, lastClickedTileTile, actualPlayer); 
        //}
    }
    public static void setLastClickedTile(GameObject lastClikedTile)
    {
        TileClick.lastClikedTile = lastClikedTile;
    }
            
    public void changeToMarked(){
        this.gameObject.GetComponent<SpriteRenderer>().sprite = markedGamefield;

        this.ithaveBeenClicked = true;
        wasAnyTileClicked = true;
    }
    public void setUnrechableSkin()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = emptyGamefield;
    }
    public void changeToUnMark(){

        this.gameObject.GetComponent<SpriteRenderer>().sprite = emptyGamefield;
        this.ithaveBeenClicked = false;
        Hrac h = this.getTypeOfUnitCurentlyHavin().GetComponent<Unit>().getPlayer();
        wasAnyTileClicked = ( false);
        lastClikedTile = null;
    }
    public void setTileCoordinates(Vector2 value)
    {
        //take spatne
        this.tileCoordinatesInMatrix = value;
    }
    public void setUnitCoordinates(Vector2 value)
    {
        this.typeOfunitCurectlyHaving.GetComponent<Unit>().setCoordinatesOfunit(value);
    }
    public Vector2 getCoordinatesInMatrix()
    {
        return this.tileCoordinatesInMatrix;
    }
    public GameObject getTypeOfunitCurentlyHaving()
    {
        return this.typeOfunitCurectlyHaving;
    }
    public void setTypeOfUnitCyrentlyHaving(GameObject typeOfunit)
    {
        //can be used only in terms of changin coordinates of unit
        this.typeOfunitCurectlyHaving = typeOfunit;
    }
    public void setBuffer(GameObject buffer)
    {
        this.bufferUnit = buffer;
    }
    public GameObject getBuffer()
    {
        return this.bufferUnit ;
    }

    public void moveAndSetTypeOfunitCurenlyHaving()
    {
       
        wasAnyTileClicked = false;

        //change of location of the unit
        //this.typeOfunitCurectlyHaving.transform.position = this.transform.position;
        this.bufferUnit.transform.position = this.transform.position;
    }

    private void initOfUnit(Unit unitC, Hrac player, bool isInSameGroupAsPreviousUnit)
    {
        unitC.setPlayer(player);
        int? unitsGroupNumber = null;
        if (isInSameGroupAsPreviousUnit)
        {
            unitsGroupNumber = player.addUnitToActualGroup(this, unitC);
        } else {
            player.initNewGroup();
            unitsGroupNumber = player.addUnitToActualGroup(this, unitC);

        }
        if (unitsGroupNumber == null) throw new System.Exception("unit group was not corectly initialized");

        unitC.setGroupNumber(unitsGroupNumber);
        Debug.Log($"unit init in tyleclick unitC {unitC.getGroupNumber()}");
    }

    public void setTypeOfunit(GameObject unit, Hrac player, bool isInSameGroupAsPreviousUnit) {
        //slouží k nastavení jednotek na zacatku hry a pro umistovaní nových jednotek        
        GameObject newUnit = Instantiate(unit, this.transform.position, Quaternion.Euler(0, 0, 0)); //,// this.transform);

        Unit unitC = newUnit.transform.GetComponent<Unit>();

        initOfUnit(unitC, player, isInSameGroupAsPreviousUnit);
        unitC.initUnit(player, (int)unitC.getGroupNumber(), isInSameGroupAsPreviousUnit);

        newUnit.GetComponent<Unit>().setCoordinatesOfunit(this.transform.position);                                                                                   

        this.typeOfunitCurectlyHaving = newUnit;

        Debug.Log("********************Checking of copied unit ******************");
        Debug.Log("name of new unit: " + newUnit.name);
        Debug.Log($"group number of unit {newUnit.GetComponent<Unit>().getGroupNumber().ToString()}");
        Debug.Log($"unit player {newUnit.GetComponent<Unit>().getPlayer().name.ToString()}");

    }

    public void unsetUnit()
    {
        this.typeOfunitCurectlyHaving = null;
    }

    public void unSetUnit()
    {
        this.typeOfunitCurectlyHaving = null;

    }

    public GameObject getTypeOfUnitCurentlyHavin()
    {
        return this.typeOfunitCurectlyHaving;
    }
    public Vector2 getRealWordCoordinates()
    {
        return this.transform.position;
    }
    public bool doTileHaveUnit()
    {
        return (this.getTypeOfUnitCurentlyHavin() == null);
    }
   
    public void setPlayer(Hrac player)
    {
        Unit currentUnit = this.getTypeOfUnitCurentlyHavin().GetComponent<Unit>();
        currentUnit.setPlayer(player);
    }
    public void setGroupnumber(int groupnumber)
    {
        //dodelat funci ve hraci ktera do konkretni skupiny prida hraci
        this.typeOfunitCurectlyHaving.GetComponent<Unit>().setGroupNumber(groupnumber);
    }
    public bool getisInGroup()
    {
        if (this.doTileHaveUnit())
        {
            return false;
        }
        if (this.typeOfunitCurectlyHaving.GetComponent<Unit>() as Unit == null) {
            return false;
        }
        else
        {
            if (!(this.typeOfunitCurectlyHaving.GetComponent<Unit>().getGroupNumber() == null))
            {
                return true;
            }
            throw new System.Exception("Unit must be in group");
        }

    }

    public void groupUnMark()
    {//hlidani null hodnoty
        if ( this.typeOfunitCurectlyHaving != null){

            Debug.Log($"*******************----------------{this.getCoordinatesInMatrix()}");
            Unit currentUnit = this.typeOfunitCurectlyHaving.GetComponent<Unit>();

            Debug.Log($"-------------------------------------");
            Debug.Log($"-------------------------------------group mark{this.typeOfunitCurectlyHaving}");

            Debug.Log($"-------------------------------------");
            Hrac player = currentUnit.getPlayer();

            player.deSelectGroupOfUnits((int)currentUnit.getGroupNumber());
        }
     }
    public void groupMark()
    {
        //hlidani null hodnoty
        Unit currentUnit = this.typeOfunitCurectlyHaving.GetComponent<Unit>();
        Hrac player = currentUnit.getPlayer();
      
        player.selectGroupOfUnits((int)currentUnit.getGroupNumber());

    }
    void setOffsetOfUnit(Vector3 offset)
    {
        this.offsetOfunit = offset;
    }
    public Vector3 getOffsetOfunit()
    {
        return this.offsetOfunit;
    }

   public void setCoordinetsForDistanceCalculation(Vector3 coordinates)
    {
        this.coordinetsForDistanceCalculation = coordinates;
    }

    public Vector3 getCoordinetsForDistanceCalculation()
    {
        return this.coordinetsForDistanceCalculation;
    }

    public void setReachebleSparit(){
        this.gameObject.GetComponent<SpriteRenderer>().sprite = this.rechAbleSprit;
    }
    public void setIthavebeenClicked(bool ithaveBeenclicked)
    {
        this.ithaveBeenClicked = ithaveBeenclicked;
    }
    public static void setWasAnyTileClicked(bool wasAnyTileClicked)
    {
        TileClick.wasAnyTileClicked = wasAnyTileClicked;
    }
}
