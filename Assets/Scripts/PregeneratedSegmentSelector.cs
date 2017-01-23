using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//********************************************************************
//				DESCRIPTION
//********************************************************************
// Class to select pregenerated segments based on their given rules
// This treats the segment selection as a state machine, where each current segment can only
// be followed by a certain type of segments
//********************************************************************

//********************************************************************
//				EXPLANATION
//********************************************************************
// What we want to do is to create an array of possible options for the next state based on their probabilites
// This array is create based on the rules for possible next segments
// Current rules are as follows:
// Straight: This can be followed by anything
// Left: Can be immediately followed by a straight or Right but not immediately by a jump
//       Furthermore a jump can occur as the second next option
//       Another left cannot occur until a right has appeared to avoid creating a U turn
// Right: Can be immediately followed by a straight or Left but not immediately by a jump
//       Furthermore a jump can occur as the second next option
//       Another right cannot occur until a left has appeared to avoid creating a U turn
// Jump: Cannot be followed by Left or Right since that results in a un escapeable obstacle
//       Can only be followed by a straight or another jump
// 
// To resolve the issue of avoiding a left after a left we need to create two special straigt states
// Which basically keep track of what last turn appeared
// So have the following additional states:
// Straight Left (SL): Has same probability as straight, can be followed by anything except a left
// Straight Right (SR): Has same probability as straight, can be followed by anything except a right
// This results in the following possible scenarios
//						SL
//					/	|	\
//					R	SL	J
//
//						SR
//					/	|	\
//					L	SR	J
//
//						L
//					/	|	
//					R	SL
//
//						R
//					/	|	
//					L	SR	
//
//						J
//					/	|	\	
//					SR	SL	J
//
// This gives us the ability to move from one segment to another based on the rules
//********************************************************************

public class PregeneratedSegmentSelector : SegmentSelectorBase
{

	private RuleSegment tempSegment;

	public override void InitializeSegments()
	{
		//Initialize the dictionary from which we will select the segments to generate from
		segmentsToChooseFrom = new Dictionary<SegmentTypes, RuleSegment> ();

		tempSegment = new RuleSegment (SegmentTypes.SL.ToString());
		tempSegment.AddNextPossibleSegment(SegmentTypes.J);
		tempSegment.AddNextPossibleSegment(SegmentTypes.R);
		tempSegment.AddNextPossibleSegment(SegmentTypes.SL);
		tempSegment.DebugNextPossibleSegments();
		segmentsToChooseFrom.Add (SegmentTypes.SL, tempSegment);

		tempSegment = new RuleSegment (SegmentTypes.SR.ToString());
		tempSegment.AddNextPossibleSegment(SegmentTypes.J);
		tempSegment.AddNextPossibleSegment(SegmentTypes.L);
		tempSegment.AddNextPossibleSegment(SegmentTypes.SR);
		tempSegment.DebugNextPossibleSegments();
		segmentsToChooseFrom.Add (SegmentTypes.SR, tempSegment);

		tempSegment = new RuleSegment (SegmentTypes.L.ToString());
		tempSegment.AddNextPossibleSegment(SegmentTypes.R);
		tempSegment.AddNextPossibleSegment(SegmentTypes.SL);
		tempSegment.DebugNextPossibleSegments();
		segmentsToChooseFrom.Add (SegmentTypes.L, tempSegment);

		tempSegment = new RuleSegment (SegmentTypes.R.ToString());
		tempSegment.AddNextPossibleSegment(SegmentTypes.L);
		tempSegment.AddNextPossibleSegment(SegmentTypes.SR);
		tempSegment.DebugNextPossibleSegments();
		segmentsToChooseFrom.Add (SegmentTypes.R, tempSegment);

		tempSegment = new RuleSegment (SegmentTypes.J.ToString());
		tempSegment.AddNextPossibleSegment(SegmentTypes.SL);
		tempSegment.AddNextPossibleSegment(SegmentTypes.SR);
		tempSegment.AddNextPossibleSegment(SegmentTypes.J);
		tempSegment.DebugNextPossibleSegments();
		segmentsToChooseFrom.Add (SegmentTypes.J, tempSegment);

		UpdateMapState (SegmentTypes.SR);

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