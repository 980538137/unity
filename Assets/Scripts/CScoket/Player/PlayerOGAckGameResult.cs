using UnityEngine;
using System.Collections;
using com.ourgame.texasSlots;
using System.Collections.Generic;

public class PlayerOGAckGameResult : Framework.Singleton<PlayerOGAckGameResult>  {

	public bool onOff=false;
	/// <summary>
	/// JP
	/// <summary>
	public long  winGold{ get; set;}

	// tip开关0关1开

	/// <summary>
	/// 线上中奖金额
	/// <summary>
	public int  totalLinePayout { get; set; }

	/// <summary>
	/// 0未中奖1普通中奖2jp大奖
	/// <summary>
	public int  result { get; set; }

	/// <summary>
	/// 中线的结果，64位整数的数组，位置表示中线的index, 内容表示中了几个图标和线上的倍数
	/// <summary>
	public long[]  longWins { get; set; }

	/// <summary>
	/// 本次spin产生的矩阵结果
	/// <summary>
	public int[,]  matrix { get; set; }

	/// <summary>
	/// 用户当前经验
	/// <summary>
	public long  winExp { get; set; }

	/// <summary>
	/// 1老虎机2大厅3比倍
	/// <summary>
	public int  page { get; set; }

	/// <summary>
	/// 错误消息,测试时用于输出
	/// <summary>
	public string   wrongMsg { get; set; }

	/// <summary>
	/// 剩余免费小游戏转动次数
	/// <summary>
	public int   freeTimes { get; set; }

	/// <summary>
	/// 用户的万能豆数
	/// <summary>
	public long   userGold { get; set; }

	/// <summary>
	/// 小游戏结果
	/// <summary>
	public List<OGAckGameResult.MiniGame>   miniGameResults { get; set; }

	/// <summary>
	/// 角色等级
	/// </summary>
	public int lev{get;set;}

	public void UpdateGameResult(OGAckGameResult gameResult)
	{
		onOff = true;
		this.winGold=gameResult.winGold;
		this.totalLinePayout=gameResult.totalLinePayout;
		this.result=gameResult.result;

		this.longWins=longResult(gameResult.longWins);
		this.matrix=matrixResult(gameResult.matrix);
		this.winExp=gameResult.winExp;
		this.page=gameResult.page;
		this.wrongMsg=gameResult.wrongMsg;
		this.freeTimes=gameResult.freeTimes;
		
		this.userGold=gameResult.userGold;
		this.miniGameResults=gameResult.miniGameResults;
		this.lev=gameResult.lev;
	}
	long[] longResult(string result)
	{
		if(result!=null)
		{
			try {
				string longwin=result.Substring(1,result.Length-2);
				string[] longw=longwin.Split(',');
				long[] longwins=new long[longw.Length];
				for(int i=0;i<longw.Length;i++)
				{
					longwins[i]=long.Parse(longw[i]);
//					Debug.LogError("longWins:"+longwins[i]);
				}
				return longwins;
			} catch (System.Exception ex) {
				Debug.Log("游戏操作出现异常:"+ex.Message);
			}

		}
		return null;
	}
	int[,]  matrixResult(string matrix)
	{
		if (result != null) {
			try{
				string mat = matrix.Replace ("[", "");
				mat = mat.Replace ("]", "");
				string[] ma = mat.Split (',');
				int count = 0;
				int[,] matrixs = new int[3, 5];
				for (int i=0; i<3; i++) {
						for (int j=0; j<5; j++) {
								matrixs [i, j] = int.Parse (ma [count]);
								count++;
						}
				}
				return matrixs;
			}catch(System.Exception ex){
				Debug.Log("游戏操作出现异常:"+ex.Message);
			}
		}
		return null;
	}
//	public void glodDate(long glod)
//	{
//		this.userGold=glod;
//	}

}
