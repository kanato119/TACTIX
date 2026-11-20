using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class MainWeapon_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Excel/MainWeapon.xlsx";
	private static readonly string exportPath = "Assets/Excel/MainWeapon.asset";
	private static readonly string[] sheetNames = { "MainWeapon", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			Entity_MainWeapon data = (Entity_MainWeapon)AssetDatabase.LoadAssetAtPath (exportPath, typeof(Entity_MainWeapon));
			if (data == null) {
				data = ScriptableObject.CreateInstance<Entity_MainWeapon> ();
				AssetDatabase.CreateAsset ((ScriptableObject)data, exportPath);
				data.hideFlags = HideFlags.NotEditable;
			}
			
			data.sheets.Clear ();
			using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
				IWorkbook book = null;
				if (Path.GetExtension (filePath) == ".xls") {
					book = new HSSFWorkbook(stream);
				} else {
					book = new XSSFWorkbook(stream);
				}
				
				foreach(string sheetName in sheetNames) {
					ISheet sheet = book.GetSheet(sheetName);
					if( sheet == null ) {
						Debug.LogError("[QuestData] sheet not found:" + sheetName);
						continue;
					}

					Entity_MainWeapon.Sheet s = new Entity_MainWeapon.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						Entity_MainWeapon.Param p = new Entity_MainWeapon.Param ();
						
					cell = row.GetCell(0); p.ID = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.nameID = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(2); p.name = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(3); p.spriteName = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(4); p.Ammon = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(5); p.TotalAmmon = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.Damage = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(7); p.HeadshotDamage = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(8); p.ReloadTime = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(9); p.FireRate = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(10); p.RapidFire = (cell == null ? false : cell.BooleanCellValue);
						s.list.Add (p);
					}
					data.sheets.Add(s);
				}
			}

			ScriptableObject obj = AssetDatabase.LoadAssetAtPath (exportPath, typeof(ScriptableObject)) as ScriptableObject;
			EditorUtility.SetDirty (obj);
		}
	}
}
