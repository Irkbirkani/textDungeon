using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerCommands {

	private static string exitNorth = "north", exitEast = "east", exitSouth = "south", exitWest = "west";

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
}
