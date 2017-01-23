using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This is the base data type for segments and contains information 
//about the segment
public class SegmentData
{
	public SegmentTypes type;

	//This stores the position of this segment at which it was spawned
	//realtive to all segemnts
	//This is NOT a world position but rather its position in the "array" of all spawned segments
	private int index = 0;

	public int GetIndex()
	{
		return index;
	}

	//This will store the name that is displayed in the editor
	//This should be a combination of its index and type
	private string name;

	public string GetName()
	{
		return name;
	}

	public void SetName(string nameToSet)
	{
		name = index+"_"+nameToSet;
	}

	//This is the GameObject that will be spawned against this segment
	public GameObject go;


}


public class SegmentGeneratorBase 
{
	//********************
	// DEPRECATED and moved to map manager
	//********************
	//This is the list that the Segment Selector will populate 
	//and the generator will generate segments from this
	//protected List<SegmentData> segmentList;

	//Since we might not want to generate all segments in the list at the same time
	//We want to keep a count on how much we want to generate each time the generate function is called
	private int segmentsToGenerate = 0;

	//Stores the position on which the next segment will be spawned
	protected Vector3 nextSegmentSpawnPoint;

	//Stores the forward of the next segment will be spawned
	protected Vector3 nextSegmentForward;

	//Stores the rotation of the next segment will be spawned
	protected Quaternion nextSegmentRotation;

	//Stores the number of segments that have been spawned
	protected int segmentsSpawned = 0;

	public void SetSegmentsToGenerate(int num)
	{
		segmentsToGenerate = num;
	}

	//This function should be used to assign segment GO's/Prefabs
	public virtual void InitializeSegments()
	{

	}

	//This function should be used to generate/spawn actual segments based on their logic
	public virtual void GenerateSegments( List<SegmentTypes> segmentList)
	{

	}

	//This function should be used to clear out existing segments
	public virtual void ResetAll()
	{
		
	}
}
