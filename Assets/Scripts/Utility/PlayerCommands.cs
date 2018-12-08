using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerCommands {

	private static string exitNorth = "north", exitEast = "east", exitSouth = "south", exitWest = "west", attack = "attack", flee = "flee", rest = "rest", inspect = "inspect", target = "target";
    private static string get = "get", drop = "drop", character = "character", inventory = "inventory";
    private static string step ="step";

	public static string ExitNorth {
		get { return exitNorth; }
		set { exitNorth = value; }
	}

	public static string ExitEast {
		get { return exitEast; }
		set { exitEast = value; }
	}

	public static string ExitSouth {
		get { return exitSouth; }
		set { exitSouth = value; }
	}

	public static string ExitWest {
		get { return exitWest; }
		set { exitWest = value; }
	}

	public static string Attack {
		get { return attack; }
		set { attack = value; }
	}

    public static string Flee
    {
        get { return flee; }
        set { flee = value; }
    }

    public static string Rest
    {
        get { return rest; }
        set { rest = value; }
    }

    public static string Inspect
    {
        get { return inspect; }
        set { inspect = value; }
    }

    public static string Target
    {
        get { return target; }
        set { target = value; }
    }

    public static string Get
    {
        get { return get; }
        set { get = value; }
    }

    public static string Drop
    {
        get { return drop; }
        set { drop = value; }
    }

    public static string Character
    {
        get { return character; }
        set { character = value; }
    }

    public static string Inventory
    {
        get { return inventory; }
        set { inventory = value; }
    }

    public static string Step
    {
        get { return step; }
        set { step = value; }
    }

}
