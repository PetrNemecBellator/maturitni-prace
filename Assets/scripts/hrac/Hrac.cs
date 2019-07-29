using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hrac : MonoBehaviour
{
    // Start is called before the first frame update

    //public for testing
    private readonly int maximalNumberOfMoves = 8; 

    public List<TileClick> placedBlocks;
    public List<List<Unit>> groupsOfUnits;// = new List<List<Unit>>();
    void Start()
    {
        
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
