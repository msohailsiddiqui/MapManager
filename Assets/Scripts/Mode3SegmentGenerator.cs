using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mode3SegmentGenerator : SegmentGeneratorBase 
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
		customSegment1.type = SegmentTypes.Custom2_Full_Start;
		customSegment1.layout = 
			"T" + "T" + "T" + "T" + "T" +
			"T" + "T" + "T" + "T" + "T";


		CustomSegmentData customSegment2 = new CustomSegmentData ();
		customSegment2.type = SegmentTypes.Custom2_Right_Easy;
		customSegment2.layout = 
			"T" + "T" + "T" + "T" + "T" +
			"O" + "T" + "T" + "T" + "T" +
			"O" + "O" + "T" + "T" + "T" +
			"O" + "O" + "O" + "T" + "T" +
			"O" + "O" + "T" + "T" + "T" +
			"O" + "T" + "T" + "T" + "T" +
			"T" + "T" + "T" + "T" + "T";
			
		
		CustomSegmentData customSegment3 = new CustomSegmentData ();
		customSegment3.type = SegmentTypes.Custom2_Left_Easy;
		customSegment3.layout = 
			"T" + "T" + "T" + "T" + "T" +
			"T" + "T" + "T" + "T" + "O" +
			"T" + "T" + "T" + "O" + "O" +
			"T" + "T" + "O" + "O" + "O" +
			"T" + "T" + "T" + "O" + "O" +
			"T" + "T" + "T" + "T" + "O" +
			"T" + "T" + "T" + "T" + "T";

		
		CustomSegmentData customSegment4 = new CustomSegmentData ();
		customSegment4.type = SegmentTypes.Custom2_Right_Medium;
		customSegment4.layout = 
			"T" + "T" + "T" + "O" + "O" +
			"O" + "T" + "T" + "O" + "O" +
			"O" + "O" + "T" + "T" + "O" +
			"O" + "O" + "O" + "T" + "T" +
			"O" + "O" + "T" + "T" + "O" +
			"O" + "T" + "T" + "O" + "O" +
			"T" + "T" + "T" + "O" + "O";
		CustomSegmentData customSegment5 = new CustomSegmentData ();
		customSegment5.type = SegmentTypes.Custom2_Left_Medium;
		customSegment5.layout = 
			
			"O" + "O" + "T" + "T" + "T" +
			"O" + "O" + "T" + "T" + "O" +
			"O" + "T" + "T" + "O" + "O" +
			"T" + "T" + "O" + "O" + "O" +
			"O" + "T" + "T" + "O" + "O" +
			"O" + "O" + "T" + "T" + "O" +
			"O" + "O" + "T" + "T" + "T";
		
		CustomSegmentData customSegment6 = new CustomSegmentData ();
		customSegment6.type = SegmentTypes.Custom2_Center_Medium;
		customSegment6.layout = 
			
			"T" + "T" + "T" + "T" + "T" +
			"T" + "T" + "T" + "T" + "T" +
			"T" + "T" + "O" + "T" + "T" +
			"T" + "O" + "O" + "O" + "T" +
			"T" + "T" + "O" + "T" + "T" +
			"T" + "T" + "T" + "T" + "T" +
			"T" + "T" + "T" + "T" + "T";
		
		CustomSegmentData customSegment7 = new CustomSegmentData ();
		customSegment7.type = SegmentTypes.Custom2_Center_Hard;
		customSegment7.layout = 
			
			"T" + "T" + "T" + "T" + "T" +
			"T" + "T" + "T" + "T" + "T" +
			"T" + "T" + "O" + "T" + "T" +
			"T" + "O" + "O" + "O" + "T" +
			"T" + "T" + "O" + "T" + "T" +
			"T" + "T" + "T" + "T" + "T" +
			"O" + "O" + "T" + "O" + "O";
		
		CustomSegmentData customSegment8 = new CustomSegmentData ();
		customSegment8.type = SegmentTypes.Custom2_Center2_Hard;
		customSegment8.layout = 
			
			"T" + "T" + "T" + "T" + "T" +
			"T" + "O" + "O" + "O" + "T" +
			"T" + "T" + "O" + "T" + "T" +
			"O" + "T" + "O" + "T" + "O" +
			"T" + "T" + "O" + "T" + "T" +
			"T" + "O" + "O" + "O" + "T" +
			"T" + "T" + "O" + "T" + "T" +
			"T" + "T" + "T" + "T" + "T";
		
		CustomSegmentData customSegment9 = new CustomSegmentData ();
		customSegment9.type = SegmentTypes.Custom2_Right_Hard;
		customSegment9.layout = 
			
			"T" + "O" + "O" + "O" + "O" +
			"O" + "T" + "O" + "O" + "O" +
			"O" + "O" + "T" + "T" + "O" +
			"O" + "O" + "O" + "T" + "O" +
			"O" + "O" + "O" + "O" + "T" +
			"O" + "O" + "O" + "T" + "O" +
			"O" + "O" + "T" + "O" + "O" +
			"O" + "T" + "O" + "O" + "O";
		
		
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
				
				temp.name = segmentsSpawned+"_"+tilesSpawned+"_"+TileTypes.Simple.ToString();
				temp.transform.parent = currSegment.transform;
				tilesSpawned++;
				
				//Debug.Log (i+". nextSegmentSpawnPoint: "+nextSegmentSpawnPoint);
				
			}
			
			segmentsSpawned++;
			
			numRowsSpawned += segmentData.layout.Length/5;
		}
	}
	
	public override void ResetAll()
	{
	}
}
