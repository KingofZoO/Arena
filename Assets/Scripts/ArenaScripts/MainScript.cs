using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public static MainScript Instance { get; private set; }

    private ArenaGeneratorScript arenaGeneratorScript;
    private UnitGeneratorScript unitGeneratorScript;
    private TurnOrderController turnOrderController;

    private void Awake()
    {
        Instance = this;

        arenaGeneratorScript = GetComponent<ArenaGeneratorScript>();
        unitGeneratorScript = GetComponent<UnitGeneratorScript>();
        turnOrderController = GetComponent<TurnOrderController>();
    }

    public ArenaGeneratorScript GetArenaGeneratorScript
    {
        get { return arenaGeneratorScript; }
    }

    public UnitGeneratorScript GetUnitGeneratorScript
    {
        get { return unitGeneratorScript; }
    }

    public TurnOrderController GetTurnOrderController
    {
        get { return turnOrderController; }
    }
}
