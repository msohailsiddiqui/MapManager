using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RuleSegment : State 
{
	List<SegmentTypes> nextPossibleSegments = new List<SegmentTypes>();

	public RuleSegment(string name): base(name) 
	{
		//name = name;
	}
	public RuleSegment(string name,int id) : base ( name, id)
	{
		//name = name;
		//stateId = id;
	}

	public void AddNextPossibleSegment(SegmentTypes nextSegment)
	{
		nextPossibleSegments.Add (nextSegment);

	}

	public SegmentTypes GetNextPossibleSegment()
	{
		//**************************************
		//EXPLANATION
		//**************************************
		// For the rule based segment we dont want to account for probabilities
		// and just select segments based on the rules
		// but we want to generate a random map therefore
		// we select a random segment from the next possible segments list
		//**************************************
		if (nextPossibleSegments.Count < 1) 
		{
			Debug.LogError("Segment: "+Name+" next possible segments have not been initialized!");
		}

		int randomIndex = Random.Range (0, nextPossibleSegments.Count);
		SegmentTypes temp = nextPossibleSegments [randomIndex];


		//To increase randomization we shuffle the list of possible next segments as well
		nextPossibleSegments.RemoveAt(randomIndex);
		//TODO : Need a better way to handle this
		//This check prevents a -1 index if there was only one element in the list
		if (nextPossibleSegments.Count <= 0) {
			nextPossibleSegments.Insert (0, temp);
		} 
		else 
		{
			nextPossibleSegments.Insert (nextPossibleSegments.Count - 1, temp);
		}



		return temp;
	}

	public void DebugNextPossibleSegments()
	{
		string nextSegments = " | ";
		for (int i=0; i<nextPossibleSegments.Count; i++) 
		{
			nextSegments +=  nextPossibleSegments[i];
			nextSegments +=  ", ";
		}
		nextSegments += " | ";
		Debug.Log ("Segment: " + Name + nextSegments);
	}
}
