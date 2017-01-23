using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProbabiltySegment : State 
{
	public int segmentProbability = -1;

	private int segmentCalculatedProbabilty = -1;

	List<SegmentTypes> nextPossibleSegments = new List<SegmentTypes>();
	List<SegmentTypes> currNextPossibleSegments = new List<SegmentTypes>();

	public ProbabiltySegment(string name): base(name) 
	{
		//name = name;
	}
	public ProbabiltySegment(string name,int id) : base ( name, id)
	{
		//name = name;
		//stateId = id;
	}

	public void SetCalculatedProbabilty(int probability)
	{
		segmentCalculatedProbabilty = probability;
	}

	public int GetCalculatedProbabilty()
	{
		return segmentCalculatedProbabilty;
	}

	public void AddNextPossibleState(SegmentTypes ProbabiltySegment, int number)
	{
		for (int i=0; i<number; i++) 
		{
			nextPossibleSegments.Add (ProbabiltySegment);
		}
		currNextPossibleSegments = new List<SegmentTypes>( nextPossibleSegments);
	}

	public SegmentTypes GetNextPossibleSegment()
	{
		if (nextPossibleSegments.Count < 1) 
		{
			Debug.LogError("State: "+Name+" next possible state have not been initialized!");
		}
		int random = Random.Range (0, currNextPossibleSegments.Count);
		SegmentTypes nextType = currNextPossibleSegments [random];
		Debug.Log ("Getting Next Possible state for : " + Name + ", random index: " + random+ ", selected: "+nextType);

		currNextPossibleSegments.RemoveAt(random);
		Debug.Log ("Remaining options: ");
		foreach (SegmentTypes entry in currNextPossibleSegments) 
		{
			Debug.Log (entry);
		}
		if (currNextPossibleSegments.Count < 1) 
		{
			currNextPossibleSegments = new List<SegmentTypes>( nextPossibleSegments);
		}
		return nextType;
	}

	public void DebugNextPossibleState()
	{
		string nextStates = " | ";
		for (int i=0; i<nextPossibleSegments.Count; i++) 
		{
			nextStates +=  nextPossibleSegments[i];
			nextStates +=  ", ";
		}
		nextStates += " | ";
		Debug.Log ("State: " + Name + nextStates);
	}

}
