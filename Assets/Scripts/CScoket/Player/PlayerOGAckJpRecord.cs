using UnityEngine;
using System.Collections;
using com.ourgame.texasSlots;
using System.Collections.Generic;
using System;

public class PlayerOGAckJpRecord : Framework.Singleton<PlayerOGAckJpRecord> {
	
	/// <summary>
	/// JP
	/// <summary>
	public List<OGAckJpRecord.JpRecord>  JpRecordList{ get; set;}
	
	/// <summary>
	/// tip开关0关1开
	/// <summary>
	public Int32  tipOn { get; set; }
	/// <summary>
	/// tip内容
	/// <summary>
	public string  tipText { get; set; }

	
	public void UpdateJpRecord(OGAckJpRecord record)
	{
		this.JpRecordList =record.JpRecordList;
		this.tipOn = record.tipOn;
		this.tipText = record.tipText;
	}
}
