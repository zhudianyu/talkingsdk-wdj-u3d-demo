using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System;
using System.Collections.Generic;

public class SDKManager : MonoBehaviour {
	
	//mono初始化会直接调用这个来初始化sdk
	static SDKManager Instance = null;
	AndroidJavaObject mainApplication  = null;
	AndroidJavaObject sdkBase  = null;
	AndroidJavaObject sdkObject = null;
	AndroidJavaObject gameActivity = null;
	static string tempText = "msg";
	static string sendText = "edit";
	//mono初始化会直接调用这个来初始化sdk
	void Awake () 
	{
		mainApplication = new AndroidJavaClass("com.talkingsdk.MainApplication").CallStatic<AndroidJavaObject>("getInstance");
		sdkBase = mainApplication.Call<AndroidJavaObject>("getSdkInstance");
		sdkBase.Call("setUnityGameObject", gameObject.name );
		Debug.LogError ("Awake:" +gameObject.name );
		if(Instance==null)
		{
			Instance=this;
		}
	}
	
	void OnInitComplete(string test)
	{
		Debug.LogError ("OnInitComplete:" + test);
		tempText = test;
	}
	
	
	public void Login()
	{
		Debug.LogError ("go to login....");
		sdkBase.Call("login");
	}
	
	public void Pay()
	{
		AndroidJavaObject payData = new AndroidJavaObject("com.talkingsdk.models.PayData");
		Debug.LogError ("go to pay....");
		payData.Call("setMyOrderId",Guid.NewGuid().ToString());
		payData.Call("setProductId",Guid.NewGuid().ToString());
		payData.Call("setProductRealPrice", 1.0f);
		payData.Call("setProductIdealPrice",1.0f);
		payData.Call("setProductName","BaoBi");
		payData.Call("setProductCount",1);
		payData.Call("setDescription","pay test demo");
		sdkBase.Call("pay", payData);
	}
	
	
	public void ShowToolBar()
	{
		Debug.LogError ("go to showToolBar....");
		sdkBase.Call("showToolBar");
	}
	
	public void DestroyToolBar()
	{
		Debug.LogError ("go to destroyToolBar....");
		sdkBase.Call("destroyToolBar");
	}
	
	public void ShowUserCenter()
	{
		Debug.LogError ("go to showUserCenter....");
		sdkBase.Call("showUserCenter");
	}
	
	public void ChangeAccount()
	{
		Debug.LogError ("go to change account....");
		sdkBase.Call("changeAccount");
	}
	
	public void Logout()
	{
		Debug.LogError ("go to logout....");
		sdkBase.Call("logout");
	}
	
	void OnLoginResult(string result)
	{
		Debug.LogError ("OnLoginSuccess:" + result);
		tempText = result;
	}
	
	
	void OnPayResult(string result)
	{
		Debug.LogError ("OnPayResult:" + result);
		tempText = result;
	}
	
	void OnLogoutResult(string result)
	{
		Debug.LogError ("OnLogoutResult: " + result);
		tempText = result;
	}
	
	
	
	void OnGUI()
	{
		//login
		if (GUI.Button (new Rect (200, 100, 300, 100), "Login")) {
			Instance.Login();
		}
		
		if (GUI.Button (new Rect (200, 250, 300, 100), "Pay")) {
			Instance.Pay();
		}
		
		if (GUI.Button (new Rect (200, 400, 300, 100), "ChangeAccount")) {
			Instance.ChangeAccount();
		}
		
		if (GUI.Button (new Rect (200, 550, 300, 100), "Logout")) {
			Instance.Logout();
		}
		
		if (GUI.Button (new Rect (200, 700, 300, 100), "ShowToolBar")) {
			Instance.ShowToolBar();
		}
		
		if (GUI.Button (new Rect (200, 850, 300, 100), "DestroyToolBar")) {
			Instance.DestroyToolBar();
		}
		
		if (GUI.Button (new Rect (200, 1000, 300, 100), "ShowUserCenter")) {
			Instance.ShowUserCenter();
		}
		
		GUI.Label (new Rect (200, 1150, 300, 300), tempText);
	}
}
