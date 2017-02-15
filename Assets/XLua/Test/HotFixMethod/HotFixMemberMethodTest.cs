using UnityEngine;
using System.Collections;
using XLua;
using System.Collections.Generic;
[Hotfix]
public class HotFixMemberMethodTest : MonoBehaviour {

	public int a = 10;
	public List<int> intList = new List<int>();
	public GameObject go;
	void Awake()
	{
		go = new GameObject("xxx");	
		LuaEnv luaenv = new LuaEnv();
		luaenv.DoString(@"
			local util = require 'xlua.util'
    		xlua.hotfix(CS.HotFixMemberMethodTest,{
     	   Log = function(self)
            print('----AfterFix--------')          
 		    print('Log In Lua')
			self.a = 100
			local intList = self.intList
			intList:Add(123)
			--print(self.strList)
			--self.strList.Add(2)
			print(self.a)
        end;
  		Co_Test = function(self)
            return util.cs_generator(
			function()
                --while true do
					print('lua coroutine')
                    coroutine.yield(CS.UnityEngine.WaitForSeconds(3))
                    print('Wait for 3 seconds')
                --end             
            end)
        end;
			
    })");
	}
	// Use this for initialization
	void Start () {
		Debug.Log("Start in C#");
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(intList.Count);
	}

	public void Log()
	{	
		Debug.Log("-----BeforeFix------");	
		Debug.Log("Log In C#");
	}

	IEnumerator Co_Test()
	{
		yield return new WaitForSeconds(3.0f);
		Debug.Log("Log in C# Cortotine");
	}

	void OnGUI()
	{
		if(GUI.Button(new Rect(Screen.width/2,200,100,100),"Log"))
		{
			Log();
			StartCoroutine(Co_Test());
		}	
	}
}
