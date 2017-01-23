using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//********************************************************************
//				DESCRIPTION
//********************************************************************
// Class to select segments based on their probabilities and given rules
// This treats the segment selection as a state machine, where each current segment can only
// be followed by a certain type of segments
//********************************************************************

//********************************************************************
//				EXPLANATION
//********************************************************************
//Basically we calculate the ratio of each state against the state with the highest probability
// and create an array of next possible states for each state
//EXAMPLE
//If the State probabilites are as follows
// Straight(S): 5
// Left(L): 10
// Right(R): 5
// Jump(J): 20
// This essentially means the following ratio 1:2:1:4
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
// The probability of each next possible segment is stored in the state
// Based on the example probabilities given above each state will have a probabilty array like follows:
// SL : Next state probabilities: R|1 SL|1 J|4 = R,SL,J,J,J,J
// The next state will be chosen randomly from the array 

//********************************************************************


public class ProbabilityAndRuleBasedSegmentSelector : StateImplementor 
{
	public GameObject SR_Segment;
	public GameObject SL_Segment;
	public GameObject L_Segment;
	public GameObject R_Segment;
	public GameObject J_Segment;
	
	Dictionary<SegmentTypes,ProbabiltySegment> segmentsDictionary = new Dictionary<SegmentTypes, ProbabiltySegment>();
	ProbabiltySegment ProbabiltySegment;
	
	private int minStateProbability;
	private int sumStateProbability;
	
	private List<SegmentTypes> generatedSegments;
	private List<GameObject> generatedSegmentsObjs;
	private Vector3 nextSegmentSpawnPoint;
	private Vector3 nextSegmentForward;
	
	// Use this for initialization
	protected override void  Start () 
	{
		InitializeStates();
		nextSegmentSpawnPoint = Vector3.zero;
		nextSegmentForward = Vector3.forward;
		generatedSegmentsObjs = new List<GameObject> ();
	}

	
	void InitializeStates()
	{
		Debug.Log ("InitializeStates");
		sumStateProbability = 0;
		minStateProbability = 1000000;
		Dictionary<SegmentTypes, int> temp = new Dictionary<SegmentTypes, int> ();
		temp.Add (SegmentTypes.SL, 5);
		temp.Add (SegmentTypes.SR, 5);
		temp.Add (SegmentTypes.L, 120);
		temp.Add (SegmentTypes.R, 5);
		temp.Add (SegmentTypes.J, 5);
		
		SetProbabilities (temp);
		SetNextSegments ();
		UpdateGameState (SegmentTypes.SR);
		UnityEngine.Random.seed = (int)System.DateTime.Now.Millisecond;
		generatedSegments = new List<SegmentTypes>( GenerateSegments (15));
		
		
		
	}
	
	public void GenerateMap ()
	{
		GameObject temp;
		Quaternion left = new Quaternion ();
		left.eulerAngles = new Vector3 (0, -90, 0);
		Quaternion right = new Quaternion ();
		right.eulerAngles = new Vector3 (0, 90, 0);
		foreach (SegmentTypes seg in generatedSegments) 
		{
			switch(seg)
			{
			case SegmentTypes.SL:
				temp = (GameObject)Instantiate(SL_Segment, nextSegmentSpawnPoint, Quaternion.identity);
				temp.transform.forward = nextSegmentForward;
				nextSegmentSpawnPoint  = temp.transform.position+temp.transform.forward;
				nextSegmentForward = temp.transform.forward;
				generatedSegmentsObjs.Add(temp);
				break;
				
			case SegmentTypes.SR:
				temp = (GameObject)Instantiate(SR_Segment, nextSegmentSpawnPoint, Quaternion.identity);
				temp.transform.forward = nextSegmentForward;
				nextSegmentSpawnPoint  = temp.transform.position+temp.transform.forward;
				nextSegmentForward = temp.transform.forward;
				generatedSegmentsObjs.Add(temp);
				break;
				
			case SegmentTypes.L:
				temp = (GameObject)Instantiate(L_Segment, nextSegmentSpawnPoint, Quaternion.identity);
				temp.transform.forward = nextSegmentForward;
				nextSegmentSpawnPoint  = temp.transform.position-temp.transform.right;
				nextSegmentForward = -temp.transform.right ;
				generatedSegmentsObjs.Add(temp);
				break;
				
			case SegmentTypes.R:
				temp = (GameObject)Instantiate(R_Segment, nextSegmentSpawnPoint, Quaternion.identity);
				temp.transform.forward = nextSegmentForward;
				nextSegmentSpawnPoint  = temp.transform.position+temp.transform.right;
				nextSegmentForward = temp.transform.right;
				generatedSegmentsObjs.Add(temp);
				break;
				
			case SegmentTypes.J:
				temp = (GameObject)Instantiate(J_Segment, nextSegmentSpawnPoint, Quaternion.identity);
				temp.transform.forward = nextSegmentForward;
				nextSegmentSpawnPoint  = temp.transform.position+ temp.transform.forward+temp.transform.up;
				nextSegmentForward = temp.transform.forward;
				generatedSegmentsObjs.Add(temp);
				break;
				
				
			}
		}
	}
	
	public void ResetAll()
	{
		foreach (GameObject go in generatedSegmentsObjs) 
		{
			GameObject.Destroy(go);
		}
		generatedSegmentsObjs.Clear ();
		nextSegmentSpawnPoint = Vector3.zero;
		nextSegmentForward = Vector3.zero;
	}
	
	public void OnGUI()
	{
		if(GUI.Button(new Rect(Screen.width * 0.15f, Screen.height * 0.15f, Screen.width * 0.15f, Screen.height * 0.15f), "GenerateMap"))
		{
			GenerateMap ();
		}
		
		if(GUI.Button(new Rect(Screen.width * 0.85f, Screen.height * 0.15f, Screen.width * 0.15f, Screen.height * 0.15f), "ResetAll"))
		{
			ResetAll ();
		}
	}
	
	public void SetProbabilities(Dictionary<SegmentTypes, int> stateProbabilities)
	{
		
		foreach(KeyValuePair<SegmentTypes, int> entry in stateProbabilities)
		{
			//DEBUG CODE
			Debug.Log ("MapProbabilties, Key: "+entry.Key+", Value: "+entry.Value);
			
			
			sumStateProbability += entry.Value;
			
			
			if(entry.Value < minStateProbability)
			{
				minStateProbability = entry.Value;
			}
		}
		Debug.Log ("sumStateProbability: "+sumStateProbability+", minStateProbability: "+minStateProbability);
		
		// Now we initialize Map States based on the states passed
		foreach(KeyValuePair<SegmentTypes, int> entry in stateProbabilities)
		{
			ProbabiltySegment = new ProbabiltySegment (entry.Key.ToString ());
			ProbabiltySegment.segmentProbability = entry.Value;
			ProbabiltySegment.SetCalculatedProbabilty( (int) entry.Value/minStateProbability);
			segmentsDictionary.Add (entry.Key, ProbabiltySegment);
			Debug.Log("State: "+entry.Key+" , Calculated Probability: "+ProbabiltySegment.GetCalculatedProbabilty());
		}
		
	}
	public void SetNextSegments()
	{
		foreach (KeyValuePair<SegmentTypes, ProbabiltySegment> entry in segmentsDictionary) 
		{
			//Sanity check to see if set probabilities has beeb called
			if(entry.Value.segmentProbability == -1)
			{
				Debug.LogError("State: "+entry.Key+" has not been initialized yet");
			}
			
			//Here we will check for each possible state and set next states based on rules
			switch (entry.Key)
			{
			case SegmentTypes.SL:
				entry.Value.AddNextPossibleState(SegmentTypes.J, segmentsDictionary[SegmentTypes.J].GetCalculatedProbabilty());
				entry.Value.AddNextPossibleState(SegmentTypes.R, segmentsDictionary[SegmentTypes.R].GetCalculatedProbabilty());
				entry.Value.AddNextPossibleState(SegmentTypes.SL, segmentsDictionary[SegmentTypes.SL].GetCalculatedProbabilty());
				break;
				
			case SegmentTypes.SR:
				entry.Value.AddNextPossibleState(SegmentTypes.J, segmentsDictionary[SegmentTypes.J].GetCalculatedProbabilty());
				entry.Value.AddNextPossibleState(SegmentTypes.L, segmentsDictionary[SegmentTypes.L].GetCalculatedProbabilty());
				entry.Value.AddNextPossibleState(SegmentTypes.SR, segmentsDictionary[SegmentTypes.SR].GetCalculatedProbabilty());
				break;
				
			case SegmentTypes.L:
				entry.Value.AddNextPossibleState(SegmentTypes.SL, segmentsDictionary[SegmentTypes.SL].GetCalculatedProbabilty());
				entry.Value.AddNextPossibleState(SegmentTypes.R, segmentsDictionary[SegmentTypes.R].GetCalculatedProbabilty());
				break;
				
			case SegmentTypes.R:
				entry.Value.AddNextPossibleState(SegmentTypes.SR, segmentsDictionary[SegmentTypes.SR].GetCalculatedProbabilty());
				entry.Value.AddNextPossibleState(SegmentTypes.L, segmentsDictionary[SegmentTypes.L].GetCalculatedProbabilty());
				break;
				
			case SegmentTypes.J:
				entry.Value.AddNextPossibleState(SegmentTypes.SR, segmentsDictionary[SegmentTypes.SR].GetCalculatedProbabilty());
				entry.Value.AddNextPossibleState(SegmentTypes.SL, segmentsDictionary[SegmentTypes.SL].GetCalculatedProbabilty());
				entry.Value.AddNextPossibleState(SegmentTypes.J, segmentsDictionary[SegmentTypes.J].GetCalculatedProbabilty());
				break;
			}
			
			//Check if everything has been set properly
			entry.Value.DebugNextPossibleState();
			
		}
		
	}
	public void UpdateGameState(SegmentTypes newState)
	{
		UpdateState(segmentsDictionary[newState]);
	}
	
	public List<SegmentTypes> GenerateSegments(int numSegments)
	{
		
		//First we need to generate a sorted list of possible segments based on probability
		List<ProbabiltySegment> possibleSegments = new List<ProbabiltySegment>();
		foreach(KeyValuePair<SegmentTypes, ProbabiltySegment> state in segmentsDictionary)
		{
			
			int numThisTypeSegments = (int)(((float)state.Value.segmentProbability/sumStateProbability)*numSegments);
			for( int k=0; k<numThisTypeSegments; k++)
			{
				possibleSegments.Add(state.Value);
			}
		}
		//Due to floating point conversion to int there might still be some not filled segments
		//Just add straight segments 
		for(int moreSegments = possibleSegments.Count; moreSegments < numSegments; moreSegments++)
		{
			possibleSegments.Add(segmentsDictionary[SegmentTypes.SR]);
		}
		
		//DEBUG CODE
		Debug.Log ("Possible Segments");
		for (int l=0; l<possibleSegments.Count; l++) 
		{
			Debug.Log (l+". "+possibleSegments[l].Name);
		}
		//END DEBUG CODE
		
		Debug.Log ("Generated Segments");
		List<SegmentTypes> generatedSegments = new List<SegmentTypes>();
		//all we need to do here is traverse the states and keep updating as per the returned next state
		for (int i=0; i<numSegments; i++) 
		{
			ProbabiltySegment temp = segmentsDictionary[(SegmentTypes) Enum.Parse(typeof(SegmentTypes), currentState.Name, true)];
			generatedSegments.Add ((SegmentTypes) Enum.Parse(typeof(SegmentTypes), temp.Name, true));
			Debug.Log (i+". "+temp.Name);
			UpdateState(segmentsDictionary[temp.GetNextPossibleSegment()]);
			
		}
		
		
//		foreach(SegmentTypes entry in generatedSegments)
//		{
//			
//		}
		
		return generatedSegments;
	}
	
	void OnSRBegin ()
	{
		
		
	}
	bool OnSREnded (State nextState)
	{
		return true;
	}
}
