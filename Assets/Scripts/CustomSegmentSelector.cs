using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CustomSegmentSelector : SegmentSelectorBase 
{

	private RuleSegment tempSegment;

	public override void InitializeSegments()
	{
		//Initialize the dictionary from which we will select the segments to generate from
		segmentsToChooseFrom = new Dictionary<SegmentTypes, RuleSegment> ();
		
		tempSegment = new RuleSegment (SegmentTypes.Custom_FullToLeftToFull_Easy_WithObstacle.ToString());
		//tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_FullToRight_Medium_WithObstacle);
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_FullToSingle_Medium_WithObstacleAndJump);
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_SimpleLandAfterJump);
		tempSegment.AddNextPossibleSegment (SegmentTypes.Custom_CenterToLeft_Hard_WithObstacle);
		tempSegment.AddNextPossibleSegment (SegmentTypes.Custom_Full_Easy);
		tempSegment.DebugNextPossibleSegments();
		segmentsToChooseFrom.Add (SegmentTypes.Custom_FullToLeftToFull_Easy_WithObstacle, tempSegment);
		
		tempSegment = new RuleSegment (SegmentTypes.Custom_FullToRight_Medium_WithObstacle.ToString());
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_FullToLeftToFull_Easy_WithObstacle);
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_RightToLeft_Medium_WithObstacleAndJump);
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_FullToSingle_Medium_WithObstacleAndJump);
		tempSegment.AddNextPossibleSegment (SegmentTypes.Custom_Full_Easy);
		tempSegment.DebugNextPossibleSegments();
		segmentsToChooseFrom.Add (SegmentTypes.Custom_FullToRight_Medium_WithObstacle, tempSegment);
		
		tempSegment = new RuleSegment (SegmentTypes.Custom_RightToLeft_Medium_WithObstacleAndJump.ToString());
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_SimpleLandAfterJump);
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_FullToSingle_Medium_WithObstacleAndJump);
		tempSegment.AddNextPossibleSegment (SegmentTypes.Custom_FullToLeftToFull_Easy_WithObstacle);
		tempSegment.AddNextPossibleSegment (SegmentTypes.Custom_FullToRight_Medium_WithObstacle);
		tempSegment.AddNextPossibleSegment (SegmentTypes.Custom_RightToLeft_Medium_WithObstacleAndJump);
		tempSegment.AddNextPossibleSegment (SegmentTypes.Custom_FullToSingle_Medium_WithObstacleAndJump);
		tempSegment.AddNextPossibleSegment (SegmentTypes.Custom_JumpToJump_Medium);
		tempSegment.AddNextPossibleSegment (SegmentTypes.Custom_CenterToLeft_Hard_WithObstacle);
		tempSegment.DebugNextPossibleSegments();
		segmentsToChooseFrom.Add (SegmentTypes.Custom_RightToLeft_Medium_WithObstacleAndJump, tempSegment);
		
		tempSegment = new RuleSegment (SegmentTypes.Custom_SimpleLandAfterJump.ToString());
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_FullToLeftToFull_Easy_WithObstacle);
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_FullToRight_Medium_WithObstacle);
		tempSegment.AddNextPossibleSegment (SegmentTypes.Custom_RightToLeft_Medium_WithObstacleAndJump);
		tempSegment.AddNextPossibleSegment (SegmentTypes.Custom_FullToSingle_Medium_WithObstacleAndJump);
		tempSegment.AddNextPossibleSegment (SegmentTypes.Custom_JumpToJump_Medium);
		tempSegment.AddNextPossibleSegment (SegmentTypes.Custom_Full_Easy);
		tempSegment.DebugNextPossibleSegments();
		segmentsToChooseFrom.Add (SegmentTypes.Custom_SimpleLandAfterJump, tempSegment);
		
		tempSegment = new RuleSegment (SegmentTypes.Custom_FullToSingle_Medium_WithObstacleAndJump.ToString());
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_FullToLeftToFull_Easy_WithObstacle);
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_FullToRight_Medium_WithObstacle);
		tempSegment.AddNextPossibleSegment (SegmentTypes.Custom_JumpToJump_Medium);
		tempSegment.AddNextPossibleSegment (SegmentTypes.Custom_RightToLeft_Medium_WithObstacleAndJump);
		tempSegment.AddNextPossibleSegment (SegmentTypes.Custom_Full_Easy);
		segmentsToChooseFrom.Add (SegmentTypes.Custom_FullToSingle_Medium_WithObstacleAndJump, tempSegment);

		tempSegment = new RuleSegment (SegmentTypes.Custom_JumpToJump_Medium.ToString ());
		tempSegment.AddNextPossibleSegment (SegmentTypes.Custom_CenterOnly_Medium_WithObstacle);
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_FullToLeftToFull_Easy_WithObstacle);
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_FullToRight_Medium_WithObstacle);
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_SimpleLandAfterJump);
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_FullToSingle_Medium_WithObstacleAndJump);
		tempSegment.AddNextPossibleSegment (SegmentTypes.Custom_Full_Easy);
		tempSegment.DebugNextPossibleSegments();
		segmentsToChooseFrom.Add (SegmentTypes.Custom_JumpToJump_Medium, tempSegment);

		tempSegment = new RuleSegment (SegmentTypes.Custom_CenterOnly_Medium_WithObstacle.ToString ());
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_FullToLeftToFull_Easy_WithObstacle);
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_FullToRight_Medium_WithObstacle);
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_SimpleLandAfterJump);
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_FullToSingle_Medium_WithObstacleAndJump);
		tempSegment.AddNextPossibleSegment (SegmentTypes.Custom_Full_Easy);
		tempSegment.DebugNextPossibleSegments();
		segmentsToChooseFrom.Add (SegmentTypes.Custom_CenterOnly_Medium_WithObstacle, tempSegment);

		tempSegment = new RuleSegment (SegmentTypes.Custom_CenterToLeft_Hard_WithObstacle.ToString ());
		//tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_FullToLeftToFull_Easy_WithObstacle);
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_FullToRight_Medium_WithObstacle);
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_RightToLeft_Medium_WithObstacleAndJump);
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_CenterToLeft_Hard_WithObstacle);
		tempSegment.AddNextPossibleSegment (SegmentTypes.Custom_Full_Easy);
		tempSegment.DebugNextPossibleSegments();
		segmentsToChooseFrom.Add (SegmentTypes.Custom_CenterToLeft_Hard_WithObstacle, tempSegment);

		tempSegment = new RuleSegment (SegmentTypes.Custom_Full_Easy.ToString ());
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_FullToRight_Medium_WithObstacle);
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_FullToSingle_Medium_WithObstacleAndJump);
		tempSegment.AddNextPossibleSegment(SegmentTypes.Custom_CenterOnly_Medium_WithObstacle);
		tempSegment.AddNextPossibleSegment (SegmentTypes.Custom_CenterToLeft_Hard_WithObstacle);
		//tempSegment.AddNextPossibleSegment (SegmentTypes.Custom_Full_Easy);
		tempSegment.DebugNextPossibleSegments();
		segmentsToChooseFrom.Add (SegmentTypes.Custom_Full_Easy, tempSegment);

		UpdateMapState (SegmentTypes.Custom_FullToLeftToFull_Easy_WithObstacle);
		
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
