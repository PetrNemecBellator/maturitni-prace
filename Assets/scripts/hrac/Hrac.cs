using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hrac : MonoBehaviour
{
    // Start is called before the first frame update

    //public for testing
    private readonly int maximalNumberOfMoves = 8; 

   // public List<TileClick> placedBlocks;//asi by stacil int nwm proc tu je list

    private List<List<TileClick>> groupsOfUnits =new List<List<TileClick>>();
    void Start()
    {
        
    }
    public void selectGroupOfUnits(int y)
    {
        //oznaci vsechny jsednotky ve skupine
        for (int x =0; x < this.groupsOfUnits[y].Count; x++)
        {
/*Debug.Log("jmeno rodice selectle jednotky " + groupsOfUnits[y][x].gameObject.transform.parent.transform);
            Debug.Log("jmeno selectle jednotky " + groupsOfUnits[y][x].gameObject.name);

            Debug.Log(groupsOfUnits[y][x].gameObject.GetComponentInParent<TileClick>() as TileClick);*/
            groupsOfUnits[y][x].changeToMarked();
        }
    }

    public void setNewGroupOfPlayerUnits(List<TileClick> units)
    {
        this.groupsOfUnits.Add(units);
    }
    public List<List<TileClick>> getAllPlayersUnits()
    {
        return this.groupsOfUnits;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int numberOfPossibleMovesByCurUnit()
    {
        return 0;
    }
}
