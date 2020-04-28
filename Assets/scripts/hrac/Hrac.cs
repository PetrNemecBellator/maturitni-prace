using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hrac : MonoBehaviour
{
    /*
     Třída která definuje hráče a funkce poro práci s jeho jednotkami.
     */
    private int maximumGroupNumber = -1;//last last given value to group

    private List<List<TileClick>> groupsOfUnits =new List<List<TileClick>>();
    
    private int actualMaximalGroupnumber = -1;

    //this value represent¨s number of remaing steps in curent round
    //it will be displayed in up right corner of the screen 
    private int numberOfRemainingSteps = GameLogic.maximalNumberOfMoves;





    //get set add unit in group
    public int ? addUnitToActualGroup(TileClick tileWithUnit, Unit unitC)
    {
      //  Debug.Log(tileWithUnit.getTypeOfunitCurentlyHaving().name);
        unitC.setGroupNumber(this.actualMaximalGroupnumber);

        Debug.Log($"4444444444444444444444aktualni index {this.actualMaximalGroupnumber}");
        this.groupsOfUnits[this.actualMaximalGroupnumber].Add(tileWithUnit);//error

        return this.actualMaximalGroupnumber;
    }
    public void initNewGroup()
    {
        this.groupsOfUnits.Add(new List<TileClick>());
        this.actualMaximalGroupnumber++;
    }
    public void selectGroupOfUnits(int groupNumberOfunit)
    {
        //oznaci vsechny jsednotky ve skupine
        Debug.Log($"selektovana skupine {groupNumberOfunit} hrac {this.name}");
        for (int x = 0; x < this.groupsOfUnits[groupNumberOfunit].Count; x++)
        {
            rechableTiles(this.groupsOfUnits[groupNumberOfunit][x], this.groupsOfUnits[groupNumberOfunit].Count);
        }

        for (int x = 0; x < this.groupsOfUnits[groupNumberOfunit].Count; x++)
        {
           this.groupsOfUnits[groupNumberOfunit][x].changeToMarked();
        }

    }
    private void rechableTiles(TileClick actualTile, int? count)
    {
        List<List<TileClick>> mapTiles = mapCreation.getMapTiles();
        Vector2 coor = actualTile.getCoordinatesInMatrix();

        Vector2 leftTopCorner = new Vector2( (Mathf.Abs(coor.x - (float)GameLogic.getMaximumNumberOfMovesByGroup(count) / 2) )-3  ,
           (Mathf.Abs(coor.y - ((float)GameLogic.getMaximumNumberOfMovesByGroup(count) / 2))  ) -3 );

        Vector2 rigtBottomCorner = new Vector2((Mathf.Abs(((float)GameLogic.getMaximumNumberOfMovesByGroup(count) )+ coor.x)) +2,
            ((Mathf.Abs((float)GameLogic.getMaximumNumberOfMovesByGroup(count) ) + coor.y) ) +2  ) ;

        TileClick destinationTile;
        
        for (int y=(int)leftTopCorner.y; y < (int)rigtBottomCorner.y ; y++)
        {
            for (int x = (int)leftTopCorner.x; x < (int)rigtBottomCorner.x ; x++)
            {
                try
                {
                    destinationTile = mapTiles[y][x];                          

                    if (!GameLogic.isDistanceReacheble(actualTile, destinationTile, (int)count))
                    {
                        continue;
                    }
                    mapTiles[y][x].setReachebleSparit();
                    
                }
                catch(System.IndexOutOfRangeException e)
                {
                    continue;
                }
                catch
                {
                    continue;
                }
          
            }
        }

    }
    private void unReachable(TileClick actualTile ,int count)
    {
        //tohle se da vyresit mnohem lepe
        List<List<TileClick>> mapTiles = mapCreation.getMapTiles();
        Vector2 coor = actualTile.getCoordinatesInMatrix();

        Vector2 leftTopCorner = new Vector2((Mathf.Abs(coor.x - (float)GameLogic.getMaximumNumberOfMovesByGroup(count) / 2)) - 3,
           (Mathf.Abs(coor.y - ((float)GameLogic.getMaximumNumberOfMovesByGroup(count) / 2))) - 3);

        Vector2 rigtBottomCorner = new Vector2((Mathf.Abs(((float)GameLogic.getMaximumNumberOfMovesByGroup(count)) + coor.x)) +2,
            ((Mathf.Abs((float)GameLogic.getMaximumNumberOfMovesByGroup(count)) + coor.y)) +2);

        TileClick destinationTile;

        for (int y = (int)leftTopCorner.y; y < (int)rigtBottomCorner.y; y++)
        {
            for (int x = (int)leftTopCorner.x; x < (int)rigtBottomCorner.x; x++)
            {
                try
                {
                    destinationTile = mapTiles[y][x];

                    if (!GameLogic.isDistanceReacheble(actualTile, destinationTile, (int)count))
                    {
                        continue;
                    }
                    mapTiles[y][x].setUnrechableSkin();
                  
                }
                catch (System.IndexOutOfRangeException e)
                {
                    continue;
                }
                catch
                {
                    continue;
                }

            }
        }
    }

   
    public void deSelectGroupOfUnits(int groupNumberY)
    {
        //oznaci vsechny jsednotky ve skupine
        for (int x = 0; x < this.groupsOfUnits[groupNumberY].Count; x++)
        {
            unReachable(groupsOfUnits[groupNumberY][x], this.groupsOfUnits[groupNumberY].Count);
          
        }
        for (int x = 0; x < this.groupsOfUnits[groupNumberY].Count; x++)
        {
            this.groupsOfUnits[groupNumberY][x].changeToUnMark();
        }
    }
    
    public void setNewGroupOfPlayerUnits(List<TileClick> units)
    {

        for (int x = 0; x < units.Count; x++)
        {
            units[x].setGroupnumber(this.groupsOfUnits.Count);
            units[x].setPlayer(this);
        }
        
        this.groupsOfUnits.Add(units);
        
    }
    public List<List<TileClick>> getAllPlayersUnits()
    {
        return this.groupsOfUnits;
    }

    //slozite
   
    public void moveUnitsInGroup(int groupNumber, Vector2 vectorOfmovement)
    {
       //funkce witch will iterate throuw unit group and call teleportationFunction for every unit (by destination vector)

        Vector2 finalCoordinates;
        
        deSelectGroupOfUnits(groupNumber);
        //fyzicky presun bojovych jednotek
        for (int x=0; x < this.groupsOfUnits[groupNumber].Count; x++)
        {
            //po pohybu je to null
            Unit originalUnitC = this.groupsOfUnits[groupNumber][x].getTypeOfUnitCurentlyHavin().GetComponent<Unit>() as Unit;// original unit
            Vector2 origCoorOfunit = this.groupsOfUnits[groupNumber][x].getCoordinatesInMatrix();

            finalCoordinates = new Vector2 (origCoorOfunit.x+ vectorOfmovement.x , origCoorOfunit.y + vectorOfmovement.y );

            //last clicked tile position, finalCoordinates
            //udelat teleportaci jednotek a inicializacni "kontruktor pro jednotky" potom otestovat
            Debug.Log($"vector pohybu {vectorOfmovement}");
            Debug.Log($"puvodni souradnice {origCoorOfunit} cilove souradnice {finalCoordinates} ");
           this.groupsOfUnits[groupNumber][x] = mapCreation.loadToBuffer( origCoorOfunit, finalCoordinates);
            
        }
        for (int x = 0; x < this.groupsOfUnits[groupNumber].Count; x++)
        {

            TileClick actualT = this.groupsOfUnits[groupNumber][x];
            Debug.Log("pocet jednotek ktery se aktualizuje " + this.groupsOfUnits[groupNumber].Count + "souradnice" + this.groupsOfUnits[groupNumber][x].getCoordinatesInMatrix());
            actualT.moveAndSetTypeOfunitCurenlyHaving();//musi se zmenit i v mapCreation
           

        
            mapCreation.setFromBuffer(actualT.getCoordinatesInMatrix());

        }
    }
    public void addUnitToGroup(TileClick tileWithUnit, Unit unitC, int groupNumber)
    {
        //  Debug.Log(tileWithUnit.getTypeOfunitCurentlyHaving().name);
        unitC.setGroupNumber(groupNumber);

        Debug.Log($"4444444444444444444444aktualni index {groupNumber}");
        this.groupsOfUnits[groupNumber].Add(tileWithUnit);//error

    }
    public void remuveUnit(TileClick tileUnit)
    {

        Unit unitC = tileUnit.getTypeOfUnitCurentlyHavin().GetComponent<Unit>();

        int indexOfUnit = this.groupsOfUnits[(int)unitC.getGroupNumber()].IndexOf(tileUnit);

        Debug.Log($"co se maze {this.groupsOfUnits[(int)unitC.getGroupNumber()][indexOfUnit].getCoordinatesInMatrix()}");

        mapCreation.removeUnit(tileUnit);

        Destroy(this.groupsOfUnits[(int)unitC.getGroupNumber()][indexOfUnit].getTypeOfUnitCurentlyHavin().gameObject);
        this.groupsOfUnits[(int)unitC.getGroupNumber()].Remove(tileUnit);

        tileUnit.setTypeOfUnitCyrentlyHaving(null);
    }
    public int  getNumberOfunitsInGroup(int? groupnumber)
    {
        return this.groupsOfUnits[(int)groupnumber].Count;
    }
    public Hrac getPlayer()
    {
        //mozna bude potreba instantiate
        return this;
    }
    public int getMaximumGroupNumber()
    {
        return actualMaximalGroupnumber;
    }
    public  void incrementGroupNumber()
    {
        //increments group number
        this.maximumGroupNumber++;
    }
   
}
