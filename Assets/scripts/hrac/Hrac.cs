using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hrac : MonoBehaviour
{
    /*
     Třída která definuje hráče a funkce poro práci s jeho jednotkami.
     */
        
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

        Debug.Log(this.actualMaximalGroupnumber);
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
        for (int x =0; x < this.groupsOfUnits[groupNumberOfunit].Count; x++)
        {

            groupsOfUnits[groupNumberOfunit][x].changeToMarked();
        }
    }
   
    public void deSelectGroupOfUnits(int y)
    {
        //oznaci vsechny jsednotky ve skupine
        for (int x = 0; x < this.groupsOfUnits[y].Count; x++)
        {

            groupsOfUnits[y][x].changeToUnMark();
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

        for (int x = 0; x < this.groupsOfUnits[groupNumber].Count; x++)
        {
            //po pohybu je to null
            Unit originalUnitC = this.groupsOfUnits[groupNumber][x].getTypeOfUnitCurentlyHavin().GetComponent<Unit>() as Unit;
            Debug.Log($"orginal unit {x} {this.groupsOfUnits[groupNumber][x].ToString()}");
        }

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
           this.groupsOfUnits[groupNumber][x] = mapCreation.moveUnits( origCoorOfunit, finalCoordinates);
        }      
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
}
