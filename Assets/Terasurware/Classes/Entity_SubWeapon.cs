using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity_SubWeapon : ScriptableObject
{	
	public List<Sheet> sheets = new List<Sheet> ();

	[System.SerializableAttribute]
	public class Sheet
	{
		public string name = string.Empty;
		public List<Param> list = new List<Param>();
	}

	[System.SerializableAttribute]
	public class Param
	{
		
		public int ID;
		public int nameID;
		public string name;
		public string spriteName;
		public int Ammon;
		public int TotalAmmon;
		public int Damage;
		public int HeadshotDamage;
		public int ReloadTime;
		public int FireRate;
		public bool RapidFire;
	}
}

