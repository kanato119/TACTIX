using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class RecoilPattern_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/Resources/Excel/RecoilPattern.xlsx";
	private static readonly string exportPath = "Assets/Resources/Excel/RecoilPattern.asset";
	private static readonly string[] sheetNames = { "RecoilPattern","RecoilPattern2", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			Entity_RecoilPattern data = (Entity_RecoilPattern)AssetDatabase.LoadAssetAtPath (exportPath, typeof(Entity_RecoilPattern));
			if (data == null) {
				data = ScriptableObject.CreateInstance<Entity_RecoilPattern> ();
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

					Entity_RecoilPattern.Sheet s = new Entity_RecoilPattern.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i<= sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);

                        // 空行ならスキップ
                        if (row == null)
                            continue;

                        ICell cell = null;
						
						Entity_RecoilPattern.Param p = new Entity_RecoilPattern.Param ();
						
					cell = row.GetCell(0); p.BulletNumber  = (int)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.CameraX = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(2); p.CameraY = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(3); p.BulletX = (float)(cell == null ? 0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.BulletY = (float)(cell == null ? 0 : cell.NumericCellValue);
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
