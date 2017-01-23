using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/******************************************************************/
// REQUIRED DATA TYPES
/******************************************************************/

//GENERAL INFO
//SEGMENTS are composed of TILES
//EACH TILE is composed of its type and its prefab

//These are the type of all segments
//TODO: This is an issue that all segment types need to be defined here 
//somehow need to add the ability to do this more robustly

//In general Custom segmets are composed of more than one "tile"
//simple segments should be single "Tiles"
public enum SegmentTypes
{
	SR,
	SL,
	L,
	R,
	J,
	Custom_FullToLeftToFull_Easy_WithObstacle,
	Custom_FullToRight_Medium_WithObstacle,
	Custom_RightToLeft_Medium_WithObstacleAndJump,
	Custom_SimpleLandAfterJump,
	Custom_FullToSingle_Medium_WithObstacleAndJump,
	Custom_JumpToJump_Medium,
	Custom_CenterOnly_Medium_WithObstacle,
	Custom_CenterToLeft_Hard_WithObstacle,
	Custom_Full_Easy,
	Custom2_Full_Start,
	Custom2_Right_Easy,
	Custom2_Left_Easy,
	Custom2_Right_Medium,
	Custom2_Left_Medium,
	Custom2_Center_Medium,
	Custom2_Center_Hard,
	Custom2_Center2_Hard,
	Custom2_Right_Hard
	
}

//TODO: Fix this
//This is an ugly way of doing things

public enum TileTypes
{
	//Tile types for simple generator
	SL,
	SR,
	L,
	R,
	J,
	//Tile types for custom generator
	Simple,
	Obstacle,
	Jump
	
}

// This will hold the info for each tile
public class MapTile
{
	public TileTypes type;
	public GameObject prefab;
}


public class MapManager : MonoBehaviour
{
	public static MapManager instance = null; 
	public static Dictionary<TileTypes, GameObject> tileObjDict;
	public static Dictionary<SegmentTypes, GameObject> segmentObjDict;

	//This is only used to organize structure/hierarchy of segmengts
	public GameObject emptyPrefab;

	//Pregenerated Segment Prefabs
	public GameObject SRSegmentPrefab;
	public GameObject SLSegmentPrefab;
	public GameObject LSegmentPrefab;
	public GameObject RSegmentPrefab;
	public GameObject JSegmentPrefab;

	//Custom Segment Prefabs
	public GameObject tilePrefab;
	public GameObject obstaclePrefab;
	public GameObject jumpPrefab;

	private SegmentSelectorBase currSegmentSelector;
	private SegmentGeneratorBase currSegmentGenerator;

	//This will hold the segments that the curr Map selector has chosen 
	private List<SegmentTypes> selectedSegments;
	//This will hold the segments that the curr Map generator has generated 
	private List<SegmentTypes> generatedSegments;

	//Awake is always called before any Start functions
	void Awake()
	{
		//Check if instance already exists
		if (instance == null) {		
			//if not, set instance to this
			instance = this;
		}
		//If instance already exists and it's not this:
		else if (instance != this) 
		{	
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy (gameObject);    
		}
	}

	public GameObject GetTileGo(TileTypes type)
	{
		if(tileObjDict.Count > 0)
		{
			return tileObjDict[type];
		}
		else
		{
			Debug.LogError ("MapManager::Tile List has not been initialized yet!");
			return null;
		}
	}

	public GameObject GetSegmentGO(SegmentTypes type)
	{
		if(segmentObjDict.Count > 0)
		{
			return segmentObjDict[type];
		}
		else
		{
			Debug.LogError ("MapManager::Segment List has not been initialized yet!");
			return null;
		}
	}

	public GameObject GetEmpty()
	{
		return emptyPrefab;
	}

	public void InitialzeMap()
	{
		#region SegmentGameObjectInitialization 
		//Segment initialization for pregenerated segments
		segmentObjDict = new Dictionary<SegmentTypes, GameObject> ();
		if (SLSegmentPrefab) 
		{
			segmentObjDict.Add (SegmentTypes.SL, SLSegmentPrefab);
		} 
		else 
		{
			Debug.LogError ("MapManager::SLSegmentPrefab has not been initialized yet!");
		}
		if (SRSegmentPrefab) 
		{
			segmentObjDict.Add (SegmentTypes.SR, SRSegmentPrefab);
		} 
		else 
		{
			Debug.LogError ("MapManager::SRSegmentPrefab has not been initialized yet!");
		}

		if (LSegmentPrefab) 
		{
			segmentObjDict.Add (SegmentTypes.L, LSegmentPrefab);
		} 
		else 
		{
			Debug.LogError ("MapManager::LSegmentPrefab has not been initialized yet!");
		}

		if (RSegmentPrefab) 
		{
			segmentObjDict.Add (SegmentTypes.R, RSegmentPrefab);
		} 
		else 
		{
			Debug.LogError ("MapManager::RSegmentPrefab has not been initialized yet!");
		}

		if (JSegmentPrefab) 
		{
			segmentObjDict.Add (SegmentTypes.J, JSegmentPrefab);
		} 
		else 
		{
			Debug.LogError ("MapManager::JSegmentPrefab has not been initialized yet!");
		}
		#endregion

		#region TileGameObjectInitialization 
		//Tile initialization for custom segments
		tileObjDict = new Dictionary<TileTypes, GameObject> ();
		if (tilePrefab) 
		{
			tileObjDict.Add (TileTypes.Simple, tilePrefab);
		} 
		else 
		{
			Debug.LogError ("MapManager::Simple Tile has not been initialized yet!");
		}
		if (obstaclePrefab) 
		{
			tileObjDict.Add (TileTypes.Obstacle, obstaclePrefab);
		}
		else 
		{
			Debug.LogError ("MapManager::Obstacle Tile has not been initialized yet!");
		}
		if (jumpPrefab) 
		{
			tileObjDict.Add (TileTypes.Jump, jumpPrefab);
		}
		else 
		{
			Debug.LogError ("MapManager::Jump Tile has not been initialized yet!");
		}
		#endregion

		//currSegmentSelector = new PregeneratedSegmentSelector ();
		//currSegmentSelector = new Mode3SegmentSelector ();
		currSegmentSelector = new CustomSegmentSelector ();
		currSegmentSelector.InitializeSegments ();

		//currSegmentGenerator = new PregeneratedSegmentGenerator ();
		//currSegmentGenerator = new Mode3SegmentGenerator ();
		currSegmentGenerator = new CustomSegmentGenerator ();
		currSegmentGenerator.InitializeSegments ();

	}
	public void SelectMap(int numSegments)
	{
		selectedSegments = currSegmentSelector.SelectSegments (numSegments);
	}
	//This function should be used to generate/spawn actual segments based on their logic
	public void GenerateMap()
	{
		currSegmentGenerator.GenerateSegments (selectedSegments);
	}
	
	//This function should be used to clear out existing segments
	public void ResetMap()
	{
		currSegmentGenerator.ResetAll ();
	}
}
