using UnityEngine;
using System.Collections;

public class PlayerWebData :Framework.Singleton<PlayerWebData> {

	/// <summary>
	/// 账号(渠道Id!=0使用)
	/// </summary>
	/// <value>The username.</value>
	public string username{get;set;}
	/// <summary>
	/// 360角色名
	/// </summary>
	/// <value>The nickname.</value>
	public string nickname{get;set;}

	public string rolename{get;set;}
	/// <summary>
	/// 长证书
	/// </summary>
	/// <value>The cert.</value>
	public string Cert{get;set;}
	/// <summary>
	/// 渠道Id
	/// </summary>
	/// <value>The comfrom identifier.</value>
	public int comfromId{get;set;}

	public void Webdata(string userName,string nickName,string roleName,string cert,int comfromID)
	{
		this.username=userName;
		this.nickname=nickName;
		this.rolename=roleName;
		this.Cert=cert;
		this.comfromId=comfromID;
	}
}
