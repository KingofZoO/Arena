using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public static MainScript Instance { get; private set; }

    private ArenaGeneratorScript arenaGeneratorScript;
    private UnitGeneratorScript unitGeneratorScript;

    private void Awake()
    {
        Instance = this;

        arenaGeneratorScript = GetComponent<ArenaGeneratorScript>();
        unitGeneratorScript = GetComponent<UnitGeneratorScript>();
    }

    public ArenaGeneratorScript GetArenaGeneratorScript
    {
        get { return arenaGeneratorScript; }
    }

    public UnitGeneratorScript GetUnitGeneratorScript
    {
        get { return unitGeneratorScript; }
    }
}
