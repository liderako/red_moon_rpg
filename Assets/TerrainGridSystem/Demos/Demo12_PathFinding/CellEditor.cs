using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using Newtonsoft.Json;

namespace TGS
{
	public class CellEditor : MonoBehaviour
	{
        private TerrainGridSystem tgs;
        string path; 

        [SerializeField] private string _levelName;
		[SerializeField] private List<int> _cells = new List<int>();
        public Color colorClose;

		// Use this for initialization
		private void Start ()
		{
            path = Application.dataPath + "/Resources/LevelSettings/GridMap/";
            tgs = TerrainGridSystem.instance;
            Load();
			tgs.OnCellClick += (cellIndex, buttonIndex) => changeCellOwner(cellIndex);
        }

		private void changeCellOwner(int cellIndex)
        {
            if (!_cells.Contains(cellIndex))
            {
                _cells.Add(cellIndex);
                tgs.CellSetColor(cellIndex, colorClose);
                tgs.CellSetCanCross(cellIndex, false);
            }
            else
            {
                _cells.RemoveAt(_cells.IndexOf(cellIndex));
                tgs.CellSetColor(cellIndex, Color.green);
                tgs.CellSetCanCross(cellIndex, true);
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("Save");
                Save();
            }
        }

        private void ChangeColor()
        {
            for (int i = 0; i < _cells.Count; i++)
            {
                tgs.CellSetColor(_cells[i], colorClose);
                tgs.CellSetCanCross(_cells[i], false);
            }
        }

        private void Load()
        {
            if (!ExistsLevelMap(_levelName + ".json"))
            {
                return;
            }
            string fileName = _levelName;
            string s = LoadDataFromFile(fileName);
            _cells = JsonConvert.DeserializeObject<List<int>>(s);
            ChangeColor();
        }

        private void Save()
        {
            string fileName = _levelName + ".json";
            WriteDataToFile(JsonConvert.SerializeObject(_cells), fileName);
        }

        private string LoadDataFromFile(string fileName)
        {
            TextAsset asset = Resources.Load("LevelSettings/GridMap/" + fileName) as TextAsset;
            return asset.text;
        }

        private void WriteDataToFile(string jsonString, string filename)
        {
            File.WriteAllText(path + filename, jsonString);
        }

        public bool ExistsLevelMap(string fileName)
        {
            if ((File.Exists(path + fileName)) == false)
            {
                return false;
            }
            return true;
        }
    }
}