using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.ourgame.texasSlots;

public class PlayerRank : Framework.Singleton<PlayerRank> {

	/// <summary>
	/// 排行榜 A
	/// </summary>
	public List<OGAckRank.Rank> rankA{get;set;}
	/// <summary>
	/// 排行榜 B
	/// </summary>
	public List<OGAckRank.Rank> rankB{get;set;}
	/// <summary>
	/// Gets or sets the name of the role.
	/// </summary>
	/// <value>The name of the role.</value>
	public string roleName{get;set;}

	/// <summary>
	/// 在a榜名次
	/// </summary>
	/// <value>A number.</value>
	public int aNum {get;set;}
	/// <summary>
	/// 在a收分金额
	/// </summary>
	/// <value>A gold.</value>
	public long aGold{get;set;}
	//// <summary>
	/// 角色名称
	/// </summary>
	/// <value>The name of the a role.</value>
	public string aRoleName{get;set;}
	/// <summary>
	/// 在b榜名次
	/// </summary>
	/// <value>The b number.</value>
	public int bNum {get;set;}
	/// <summary>
	/// 在b收分金额
	/// </summary>
	/// <value>The b gold.</value>
	public long bGold{get;set;}
	/// <summary>
	/// Gets or sets the name of the b role.
	/// </summary>
	public string bRoleName{get;set;}

	/// <summary>
	/// 历史榜单A
	/// </summary>
	/// <value>The last week a.</value>
	public List<OGAckRank.Rank> lastWeekA{get;set;}
	/// <summary>
	/// 历史榜单B
	/// </summary>
	/// <value>The last week b.</value>
	public List<OGAckRank.Rank> lastWeekB{get;set;}
	/// <summary>
	/// 排行榜 C
	/// </summary>
	/// <value>The rank list c.</value>
	public List<OGAckRank.Rank> rankListC{get;set;}


	public string cRankName{get;set;}
	public string aRankTip{get;set;}
	public string bRankTip{get;set;}
	public string cRankTip{get;set;}
	public string laRankTip{get;set;}
	public string lbRankTip{get;set;}

	public int rankBtnOn{get;set;}


	public void ReturnCollectCharts(OGAckRank RankGo)
	{
		if (null==RankGo) {
			return;
				}
		this.rankA=RankGo.rankListA;
		this.rankB=RankGo.rankListB;
		this.roleName=RankGo.roleName;

		this.aNum=RankGo.aNum;
		this.aGold=RankGo.aGold;
		this.aRoleName=RankGo.aRoleName;
		this.bNum=RankGo.bNum;
		this.bGold=RankGo.bGold;
		this.bRoleName=RankGo.bRoleName;

		this.lastWeekA=RankGo.lastWeekA;
		this.lastWeekB=RankGo.lastWeekB;
		this.rankListC=RankGo.rankListC;

		this.cRankName=RankGo.cRankName;
		this.aRankTip=RankGo.aRankTip;
		this.bRankTip=RankGo.bRankTip;
		this.cRankTip=RankGo.cRankTip;
		this.laRankTip=RankGo.laRankTip;
		this.lbRankTip=RankGo.lbRankTip;
	}
}
