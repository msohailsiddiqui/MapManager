using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Mode3SegmentSelector : SegmentSelectorBase 
{

	private RuleSegment tempSegment;

	public override void InitializeSegments()
	{

		//Initialize the dictionary from which we will select the segments to generate from
		segmentsToChooseFrom = new Dictionary<SegmentTypes, RuleSegment> ();
		
		tempSegment = new RuleSegment (SegmentTypes.Custom2_Full_Start.ToString());
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom2_Right_Easy);
		tempSegment.DebugNextPossibleSegments();
		segmentsToChooseFrom.Add (SegmentTypes.Custom2_Full_Start, tempSegment);
		
		tempSegment = new RuleSegment (SegmentTypes.Custom2_Right_Easy.ToString());
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom2_Left_Easy);
		tempSegment.DebugNextPossibleSegments();
		segmentsToChooseFrom.Add (SegmentTypes.Custom2_Right_Easy, tempSegment);
		
		tempSegment = new RuleSegment (SegmentTypes.Custom2_Left_Easy.ToString());
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom2_Right_Medium);
		tempSegment.DebugNextPossibleSegments();
		segmentsToChooseFrom.Add (SegmentTypes.Custom2_Left_Easy, tempSegment);
		
		tempSegment = new RuleSegment (SegmentTypes.Custom2_Right_Medium.ToString());
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom2_Left_Medium);
		tempSegment.DebugNextPossibleSegments();
		segmentsToChooseFrom.Add (SegmentTypes.Custom2_Right_Medium, tempSegment);
		
		tempSegment = new RuleSegment (SegmentTypes.Custom2_Left_Medium.ToString());
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom2_Center_Medium);
		tempSegment.DebugNextPossibleSegments();
		segmentsToChooseFrom.Add (SegmentTypes.Custom2_Left_Medium, tempSegment);
		
		tempSegment = new RuleSegment (SegmentTypes.Custom2_Center_Medium.ToString ());
		tempSegment.AddNextPossibleSegment (SegmentTypes.Custom2_Center_Hard);
		tempSegment.DebugNextPossibleSegments();
		segmentsToChooseFrom.Add (SegmentTypes.Custom2_Center_Medium, tempSegment);
		
		tempSegment = new RuleSegment (SegmentTypes.Custom2_Center_Hard.ToString ());
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom2_Center2_Hard);
		tempSegment.DebugNextPossibleSegments();
		segmentsToChooseFrom.Add (SegmentTypes.Custom2_Center_Hard, tempSegment);
		
		tempSegment = new RuleSegment (SegmentTypes.Custom2_Center2_Hard.ToString ());
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom2_Right_Hard);
		tempSegment.DebugNextPossibleSegments();
		segmentsToChooseFrom.Add (SegmentTypes.Custom2_Center2_Hard, tempSegment);
		
		tempSegment = new RuleSegment (SegmentTypes.Custom2_Right_Hard.ToString ());
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom2_Full_Start);
		tempSegment.DebugNextPossibleSegments();
		segmentsToChooseFrom.Add (SegmentTypes.Custom2_Right_Hard, tempSegment);
		
		UpdateMapState (SegmentTypes.Custom2_Full_Start);
		
		//Provide a good seed for the random numner generator
		UnityEngine.Random.seed = (int)System.DateTime.Now.Millisecond;
	}

	public void UpdateMapState(SegmentTypes newState)
	{
		UpdateState(segmentsToChooseFrom[newState]);
	}

	public override void SetNextSegments()
	{
	}

	public override List<SegmentTypes> SelectSegments(int numSegments)
	{
		Debug.Log ("Selected Segments");
		List<SegmentTypes> selectedSegments = new List<SegmentTypes>();
		//all we need to do here is traverse the states and keep updating as per the returned next state
		for (int i=0; i<numSegments; i++) 
		{
			RuleSegment temp = segmentsToChooseFrom[(SegmentTypes) Enum.Parse(typeof(SegmentTypes), currentState.Name, true)];
			selectedSegments.Add ((SegmentTypes) Enum.Parse(typeof(SegmentTypes), temp.Name, true));
			Debug.Log (i+". "+temp.Name);
			UpdateState(segmentsToChooseFrom[temp.GetNextPossibleSegment()]);
			
		}
		
		return selectedSegments;
	}
}
