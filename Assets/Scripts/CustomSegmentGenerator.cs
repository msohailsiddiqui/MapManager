using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class CustomSegmentData : SegmentData
{
	public string layout;

}

public class CustomSegmentGenerator : SegmentGeneratorBase 
{

	private int numRowsSpawned = 0;
	private int tilesSpawned = 0;

	//Used to temporarily store spawned prefab to set properties etc and avoid creating local variables in loops
	private GameObject temp;
	private GameObject currSegment;

	//Holds the definition of Custom Segments
	private List<CustomSegmentData> customSegmentList;

	public override void InitializeSegments()
	{
		customSegmentList = new List<CustomSegmentData> ();
		CustomSegmentData customSegment1 = new CustomSegmentData ();
		customSegment1.type = SegmentTypes.Custom_FullToLeftToFull_Easy_WithObstacle;
		customSegment1.layout = 
		"T" + "T" + "T" + "T" + "E"+
		"T" + "T" + "T" + "E" + "E" +
		"T" + "T" + "O" + "E" + "E" +
		"T" + "T" + "T" + "O" + "E" +
		"T" + "T" + "T" + "T" + "O" +
		"T" + "T" + "T" + "T" + "T";

		CustomSegmentData customSegment2 = new CustomSegmentData ();
		customSegment2.type = SegmentTypes.Custom_FullToRight_Medium_WithObstacle;
		customSegment2.layout = 
			"E" + "E" + "E" + "T" + "T" +
			"E" + "E" + "O" + "T" + "T" +
			"E" + "O" + "T" + "T" + "T" +
			"O" + "T" + "T" + "T" + "T" +
			"T" + "T" + "T" + "T" + "T";

		CustomSegmentData customSegment3 = new CustomSegmentData ();
		customSegment3.type = SegmentTypes.Custom_RightToLeft_Medium_WithObstacleAndJump;
		customSegment3.layout = 
			"E" + "E" + "E" + "E" + "E" +
			"E" + "E" + "E" + "E" + "E" +
			"J" + "J" + "O" + "E" + "E" +
			"T" + "T" + "T" + "O" + "E" +
			"T" + "T" + "T" + "T" + "O" +
			"T" + "T" + "T" + "T" + "T" +
			"E" + "T" + "T" + "T" + "T" +
			"E" + "E" + "T" + "T" + "T" +
			"E" + "E" + "E" + "T" + "T";

		CustomSegmentData customSegment4 = new CustomSegmentData ();
		customSegment4.type = SegmentTypes.Custom_SimpleLandAfterJump;
		customSegment4.layout = 
				"T" + "T" + "T" + "T" + "T" +
				"E" + "T" + "T" + "T" + "E";

		CustomSegmentData customSegment5 = new CustomSegmentData ();
		customSegment5.type = SegmentTypes.Custom_FullToSingle_Medium_WithObstacleAndJump;
		customSegment5.layout = 

			"E" + "E" + "E" + "E" + "E" +
			"E" + "E" + "E" + "E" + "E" +
			"E" + "E" + "J" + "E" + "E" +
			"E" + "E" + "T" + "E" + "E" +
			"E" + "E" + "T" + "E" + "E" +
			"E" + "E" + "T" + "E" + "E" +
			"O" + "O" + "T" + "O" + "O" +
			"T" + "T" + "T" + "T" + "T";

		CustomSegmentData customSegment6 = new CustomSegmentData ();
		customSegment6.type = SegmentTypes.Custom_JumpToJump_Medium;
		customSegment6.layout = 
			
			"E" + "E" + "E" + "E" + "E" +
			"E" + "E" + "E" + "E" + "E" +
			"E" + "E" + "E" + "E" + "J" +
			"E" + "E" + "E" + "E" + "E" +
			"E" + "E" + "E" + "E" + "E" +
			"E" + "J" + "E" + "E" + "E" ;

		CustomSegmentData customSegment7 = new CustomSegmentData ();
		customSegment7.type = SegmentTypes.Custom_CenterOnly_Medium_WithObstacle;
		customSegment7.layout = 
			
			"E" + "O" + "T" + "O" + "E" +
			"E" + "O" + "T" + "O" + "E" +
			"E" + "O" + "T" + "O" + "E" ;

		CustomSegmentData customSegment8 = new CustomSegmentData ();
		customSegment8.type = SegmentTypes.Custom_CenterToLeft_Hard_WithObstacle;
		customSegment8.layout = 

			"E" + "E" + "E" + "O" + "T" +
			"E" + "E" + "E" + "O" + "T" +
			"E" + "E" + "E" + "O" + "T" +
			"E" + "E" + "T" + "T" + "T" +
			"E" + "E" + "T" + "T" + "T" +
			"E" + "E" + "T" + "T" + "T" ;

		CustomSegmentData customSegment9 = new CustomSegmentData ();
		customSegment9.type = SegmentTypes.Custom_Full_Easy;
		customSegment9.layout = 
			
			"T" + "T" + "T" + "T" + "T" +
			"T" + "T" + "T" + "T" + "T" ;


		customSegmentList.Add (customSegment1);
		customSegmentList.Add (customSegment2);
		customSegmentList.Add (customSegment3);
		customSegmentList.Add (customSegment4);
		customSegmentList.Add (customSegment5);
		customSegmentList.Add (customSegment6);
		customSegmentList.Add (customSegment7);
		customSegmentList.Add (customSegment8);
		customSegmentList.Add (customSegment9);

		nextSegmentSpawnPoint = Vector3.zero;
	
	}

	public override void GenerateSegments(List<SegmentTypes> segmentList)
	{
		foreach (SegmentTypes segmentType in segmentList)
		{
			CustomSegmentData segmentData = customSegmentList.Find(item => item.type == segmentType);
			temp = GameObject.Instantiate(MapManager.instance.GetEmpty());
			currSegment = temp;
			temp.name = segmentsSpawned+"_"+segmentData.type.ToString();
			temp.transform.parent = MapManager.instance.transform;

			segmentData.go = temp;


			if(segmentData.layout.Length % 5 != 0)
			{
				Debug.LogError("Incorrectly Defined Layout - Must have 5 entires per row");
			}

			//for(int i=0;i<segment.layout.Length;i++)
			for(int i=segmentData.layout.Length-1;i>=0;i--)
			{
				nextSegmentSpawnPoint.x = i%5;
				nextSegmentSpawnPoint.z =  (segmentData.layout.Length/5)-((int)(i/5)) + numRowsSpawned;

				if(segmentData.layout[i] == 'T')
				{
					temp = GameObject.Instantiate(MapManager.instance.GetTileGo(TileTypes.Simple),
					                              nextSegmentSpawnPoint, Quaternion.identity) as GameObject;
					if(temp.GetComponentInChildren<TextMesh>() != null)
					{
						temp.GetComponentInChildren<TextMesh>().text = nextSegmentSpawnPoint.x+","+nextSegmentSpawnPoint.z;
					}
				}
				else if(segmentData.layout[i] == 'O')
				{
					temp = GameObject.Instantiate(MapManager.instance.GetTileGo(TileTypes.Obstacle),
					                              nextSegmentSpawnPoint, Quaternion.identity) as GameObject;
				}
				else if(segmentData.layout[i] == 'J')
				{
					temp = GameObject.Instantiate(MapManager.instance.GetTileGo(TileTypes.Jump), 
					                              nextSegmentSpawnPoint, Quaternion.identity)as GameObject;
				}

				//This check avoids messing up segment names.
				//Basically if the current tile is T, temp is still pointing
				//to the last tile created, which causes issues
				// The error is that Segment names become tile names 
				if(segmentData.layout[i] != 'E')
				{
					temp.name = segmentsSpawned+"_"+tilesSpawned+"_"+TileTypes.Simple.ToString();
					temp.transform.parent = currSegment.transform;
				}


				tilesSpawned++;

				//Debug.Log (i+". nextSegmentSpawnPoint: "+nextSegmentSpawnPoint);

			}

			segmentsSpawned++;

			numRowsSpawned += segmentData.layout.Length/5;
		}
	}

	public override void ResetAll()
	{
//		foreach (SegmentData segment in segmentList) 
//		{
//			if(segment.go)
//			{
//				GameObject.Destroy(segment.go);
//			}
//			else
//			{
//				Debug.LogError("Segment: "+segment.type+" was not associated with a GameObject");
//			}
//		}
//		segmentList.Clear ();
	}


}
