using UnityEngine;

[System.Serializable]
public class TileClick : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite markedGamefield;
    public Sprite emptyGamefield;

    private Vector2 tileCoordinatesInMatrix;

    private static bool wasAnyTileClicked = false;

   
    private bool ithaveBeenClicked = false;
    private GameObject typeOfunitCurectlyHaving = null;
    private static GameObject lastClikedTile = null;

    private static Vector2 CoordinetsOfPreviusObject;


    public Vector2 getRealWordCoordinates()
    {
        return this.transform.position;
    }
    public bool doTileHaveUnit()
    {
        return (this.getTypeOfUnitCurentlyHavin() == null);
    }
    public Hrac GetHrac()
    {
        if (doTileHaveUnit())
        {
            Debug.Log("object nema zadnou jednotku");
            return null;
        }
        Hrac player = this.getTypeOfUnitCurentlyHavin().transform.GetComponent<Hrac>();
        Debug.Log("ziskavam jmeno hrace " + player.name);
        return player;
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
        return (this.typeOfunitCurectlyHaving.GetComponent<Unit>() as Unit == null ? 
            false :
            !(this.typeOfunitCurectlyHaving.GetComponent<Unit>().getGroupNumber() == null));
    }

    public void groupUnMark()
    {//hlidani null hodnoty
        Unit currentUnit = this.typeOfunitCurectlyHaving.GetComponent<Unit>();

        this.GetHrac().deSelectGroupOfUnits((int)currentUnit.getGroupNumber());
    }
    public void groupMark()
    {
        //hlidani null hodnoty
        Unit currentUnit = this.typeOfunitCurectlyHaving.GetComponent<Unit>();

        this.GetHrac().selectGroupOfUnits((int)currentUnit.GetComponent<Unit>().getGroupNumber());

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
    }

 
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


            Debug.Log("*****Jmeno child objectu: " + this.typeOfunitCurectlyHaving.name);

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

        if (this.ithaveBeenClicked)
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
                CoordinetsOfPreviusObject = this.getCoordinatesInMatrix();
                wasAnyTileClicked = true;
            }
            Debug.Log("wasAnyTileClicked: " + wasAnyTileClicked + "lastClicked" + lastClikedTile != null + "this?" + lastClikedTile != this.gameObject);
            Debug.Log(wasAnyTileClicked && lastClikedTile != null && lastClikedTile != this.gameObject);


            if (wasAnyTileClicked && lastClikedTile != null && lastClikedTile != this.gameObject &&
                Mathf.Abs(this.getCoordinatesInMatrix().x - CoordinetsOfPreviusObject.x) <= 1 && //zmenit
                Mathf.Abs((this.getCoordinatesInMatrix().y - CoordinetsOfPreviusObject.y)) <= 1) {

                Hrac actualPlayer = lastClikedTile.GetComponent<TileClick>().typeOfunitCurectlyHaving.GetComponent<Unit>().getPlayer();
                Unit actualUnit = lastClikedTile.GetComponent<TileClick>().typeOfunitCurectlyHaving.GetComponent<Unit>();

                Unit attacker = lastClikedTile.transform.GetComponent<TileClick>().getTypeOfUnitCurentlyHavin().transform.GetComponent<Unit>();
                Unit passiveunit = this.typeOfunitCurectlyHaving.transform.GetComponent<Unit>();

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
                else
                {

                    if (lastClikedTile.GetComponent<TileClick>().getisInGroup())
                    {
                        

                    }
                    else
                    {
                        //spawnMovedUnit(lastClikedTile);

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

        //change of location of the unit
        this.typeOfunitCurectlyHaving.transform.position = this.transform.position;
    }
 
   

    
    private void initOfUnit(Unit unitC,Hrac player, bool isInSameGroupAsPreviousUnit)
    {
        unitC.setPlayer(player);

        if (isInSameGroupAsPreviousUnit)
        {
            player.addUnitToActualGroup(this);
        } else {
            player.initNewGroup();
            player.addUnitToActualGroup(this);
        }
    }
    public void setTypeOfunit(GameObject unit, Hrac player, bool isInSameGroupAsPreviousUnit){
        //slouží k nastavení jednotek na zacatku hry a pro umistovaní nových jednotek 
        Unit unitC = unit.transform.GetComponent<Unit>();

        initOfUnit(unitC,player ,isInSameGroupAsPreviousUnit);

        Debug.Log("type of placed unit:" + unit.name);
        GameObject newUnit = Instantiate(unit,this.transform.position, Quaternion.Euler(0, 0, 0)); //,// this.transform);

        newUnit.GetComponent<Unit>().setCoordinatesOfunit(this.transform.position);
             
        this.typeOfunitCurectlyHaving = newUnit;

        Debug.Log("name of new unit: " + newUnit.name);
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


}
