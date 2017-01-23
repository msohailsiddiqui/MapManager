using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PregeneratedSegmentGenerator : SegmentGeneratorBase 
{

	//Used to temporarily store spawned prefab to set properties etc and avoid creating local variables in loops
	private GameObject temp;

	public override void InitializeSegments()
	{

	}
	
	public override void GenerateSegments(List<SegmentTypes> segmentList)
	{
		foreach (SegmentTypes segType in segmentList) 
		{
			//TODO: This is ugly need to get rid of this switch statement. Too much repitetive code
			switch(segType)
			{
			case SegmentTypes.SL:
				temp = GameObject.Instantiate(MapManager.instance.GetSegmentGO(SegmentTypes.SL), 
				                               nextSegmentSpawnPoint, Quaternion.identity) as GameObject;
				temp.transform.forward = nextSegmentForward;
				nextSegmentSpawnPoint  = temp.transform.position+temp.transform.forward;
				nextSegmentForward = temp.transform.forward;

				temp.name = segmentsSpawned+"_"+TileTypes.SL.ToString();
				temp.transform.parent = MapManager.instance.transform;
				//seg.go = temp;
				break;
				
			case SegmentTypes.SR:
				temp = GameObject.Instantiate(MapManager.instance.GetSegmentGO(SegmentTypes.SR), 
				                               nextSegmentSpawnPoint, Quaternion.identity) as GameObject;
				temp.transform.forward = nextSegmentForward;
				nextSegmentSpawnPoint  = temp.transform.position+temp.transform.forward;
				nextSegmentForward = temp.transform.forward;

				temp.name = segmentsSpawned+"_"+TileTypes.SR.ToString();
				temp.transform.parent = MapManager.instance.transform;
				//seg.go = temp;
				break;
				
			case SegmentTypes.L:
				temp = GameObject.Instantiate(MapManager.instance.GetSegmentGO(SegmentTypes.L), 
				                               nextSegmentSpawnPoint, Quaternion.identity) as GameObject;
				temp.transform.forward = nextSegmentForward;
				nextSegmentSpawnPoint  = temp.transform.position-temp.transform.right;
				nextSegmentForward = -temp.transform.right ;

				temp.name = segmentsSpawned+"_"+TileTypes.L.ToString();
				temp.transform.parent = MapManager.instance.transform;
				//seg.go = temp;
				break;
				
			case SegmentTypes.R:
				temp = GameObject.Instantiate(MapManager.instance.GetSegmentGO(SegmentTypes.R), 
				                               nextSegmentSpawnPoint, Quaternion.identity) as GameObject;
				temp.transform.forward = nextSegmentForward;
				nextSegmentSpawnPoint  = temp.transform.position+temp.transform.right;
				nextSegmentForward = temp.transform.right;

				temp.name = segmentsSpawned+"_"+TileTypes.R.ToString();
				temp.transform.parent = MapManager.instance.transform;
				//seg.go = temp;
				break;
				
			case SegmentTypes.J:
				temp = GameObject.Instantiate(MapManager.instance.GetSegmentGO(SegmentTypes.J), 
				                               nextSegmentSpawnPoint, Quaternion.identity) as GameObject;
				temp.transform.forward = nextSegmentForward;
				nextSegmentSpawnPoint  = temp.transform.position+ temp.transform.forward+temp.transform.up;
				nextSegmentForward = temp.transform.forward;

				temp.name = segmentsSpawned+"_"+TileTypes.J.ToString();
				temp.transform.parent = MapManager.instance.transform;
				//seg.go = temp;
				break;
				
				
			} // End Switch
			segmentsSpawned++;

		}// End For Each	
	}// End Generate Segments

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
