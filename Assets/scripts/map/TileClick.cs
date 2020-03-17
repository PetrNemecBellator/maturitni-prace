using UnityEngine;

[System.Serializable]
public class TileClick : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite markedGamefield;
    public Sprite emptyGamefield;

    private Vector2 tileCoordinatesInMatrix;
    private Vector3 coordinetsForDistanceCalculation;

    private static bool wasAnyTileClicked = false;


    private bool ithaveBeenClicked = false;
    private GameObject typeOfunitCurectlyHaving = null;
    private static GameObject lastClikedTile = null;

    private static Vector2 CoordinetsOfPreviusObject;

    protected Vector3 offsetOfunit = new Vector3(0, 0);


    private void Awake()
    {
        //typeOfunitCurectlyHaving = this.gameObject.transform.GetChild(0).gameObject;
    }


    void OnMouseDown()
    {
        Debug.Log("");
        Debug.Log("");
        Debug.Log("");
        Debug.Log("TILECLICK ");

        if (wasAnyTileClicked || (this.getTypeOfUnitCurentlyHavin() as GameObject) != null)
        {
            //nefunguje spravne kdyz kliknu na prazny tile a pak na prazny tile
            if (this.ithaveBeenClicked)//říká zda už není jednotka aktivovaná
            {

                this.groupUnMark();

                try
                {
                    Debug.Log("!!!!!!!!!!!!!!child object:" + this.typeOfunitCurectlyHaving.name);
                    Debug.Log("!!!!!!!!!!!!!!child object groupnumber:" + this.typeOfunitCurectlyHaving.GetComponent<Unit>().getGroupNumber());
                    Debug.Log("!!!!!!!!!!!!!!child object player:" + this.typeOfunitCurectlyHaving.GetComponent<Unit>().getPlayer().name);
                    Debug.Log("!!!!!!!!!!!!!!child typeOfUnitCurentlyHaving:" + this.typeOfunitCurectlyHaving);
                }
                catch {
                    Debug.Log("tile nema UNIT");
                }
            }
            else
            {
                Debug.Log("was any tile clicked? " + wasAnyTileClicked.ToString());
                if (!wasAnyTileClicked)
                {
                    lastClikedTile = this.gameObject;
                    CoordinetsOfPreviusObject = this.getCoordinatesInMatrix();
                    wasAnyTileClicked = true;
                }
                Debug.Log("wasAnyTileClicked: " + wasAnyTileClicked + " lastClicked: " + lastClikedTile != null + " this? " + lastClikedTile != this.gameObject);
                Debug.Log(wasAnyTileClicked && lastClikedTile != null && lastClikedTile != this.gameObject);


                if (wasAnyTileClicked && lastClikedTile != null && lastClikedTile != this.gameObject &&
                    Mathf.Abs(this.getCoordinatesInMatrix().x - CoordinetsOfPreviusObject.x) <= 1 && //zmenit
                    Mathf.Abs((this.getCoordinatesInMatrix().y - CoordinetsOfPreviusObject.y)) <= 1) {
                    Debug.Log("zacal pohyb jednotek");

                    Hrac actualPlayer = lastClikedTile.GetComponent<TileClick>().typeOfunitCurectlyHaving.GetComponent<Unit>().getPlayer();
                    Unit actualUnit = lastClikedTile.GetComponent<TileClick>().typeOfunitCurectlyHaving.GetComponent<Unit>();
                    TileClick lastClickedTileTile = lastClikedTile.GetComponent<TileClick>();

                    //pokud je podminka splnena jedná se o souboj
                    if (!this.doTileHaveUnit())
                    {

                        Unit attacker = lastClikedTile.transform.GetComponent<TileClick>().getTypeOfUnitCurentlyHavin().transform.GetComponent<Unit>();
                        Unit passiveunit = this.typeOfunitCurectlyHaving.transform.GetComponent<Unit>() as Unit;

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


                        Unit winner = GameLogic.unitFight(attacker, passiveunit);
                        if (winner == null)
                        {
                            //bouth loses
                            Destroy(attacker.gameObject);
                            Destroy(passiveunit.gameObject);
                        }
                        else
                        {
                            Debug.Log("vitez je: " + winner.gameObject.name);
                        }

                    }
                    else {

                        if (actualUnit.getGroupNumber() != null)// it is not allowed to be null
                        {
                            Debug.Log("pohyb jednotek");
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
                }
                if (getisInGroup())
                {

                    this.groupMark();
                }



            }
        }
    endOfClick:;
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
    public void moveAndSetTypeOfunitCurenlyHaving(GameObject typeOfunit)
    {
        this.setTypeOfUnitCyrentlyHaving(typeOfunit);
        wasAnyTileClicked = false;

        //change of location of the unit
        this.typeOfunitCurectlyHaving.transform.position = this.transform.position;
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

        newUnit.GetComponent<Unit>().setCoordinatesOfunit(this.transform.position);//TODO dodelejto
                                                                                   //znejakeho důvodu nesouhlasi group number

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
        Unit currentUnit = this.typeOfunitCurectlyHaving.GetComponent<Unit>();
        Hrac player = currentUnit.getPlayer();

        player.deSelectGroupOfUnits((int)currentUnit.getGroupNumber());
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
}
