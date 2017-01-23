using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SegmentSelectorBase :  StateImplementor  
{
	protected Dictionary<SegmentTypes,RuleSegment> segmentsToChooseFrom;


	public virtual void InitializeSegments()
	{
		
	}

	public virtual void SetNextSegments()
	{
	}

	public virtual List<SegmentTypes> SelectSegments(int numSegments)
	{
		return null;
	}

}
