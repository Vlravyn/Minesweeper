using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Minesweeper.Core
{
    internal static class Settings
    {
        #region Private Members

        private static bool continueSavedGames;
        private static bool showTips;
        private static bool saveOnExit;
        private static bool playSounds;
        private static bool displayAnimations;
        private static bool allowQuestionMarks;
        private static DifficultyLevel difficulty;

        /// <summary>
        /// Stores the folder location of the local app data
        /// </summary>
        private static readonly string FolderLocation = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/Minesweeper";

        /// <summary>
        /// stores the location of the settings file
        /// </summary>
        private static readonly string FileLocation = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/Minesweeper/Settings.ini";

        #endregion

        #region Public Properties

        public static bool DisplayAnimations
        {
            get { return displayAnimations; }
            private set { displayAnimations = value; }
        }
        public static bool PlaySounds
        {
            get { return playSounds; }
            private set { playSounds = value; }
        }
        public static bool AllowQuestionMark
        {
            get { return allowQuestionMarks; }
            private set { allowQuestionMarks = value; }
        }
        public static bool ShowTips
        {
            get { return showTips; }
            private set { showTips = value; }
        }
        public static bool SaveOnExit
        {
            get { return saveOnExit; }
            private set { saveOnExit = value; }
        }
        public static bool ContinueSavedGames
        {
            get { return continueSavedGames; }
            private set { continueSavedGames = value; }
        }
        public static DifficultyLevel Difficulty
        {
            get { return difficulty; }
            private set 
            { 
                difficulty = value; 
            }
        }

        #endregion


        internal static List<DifficultyData> AllDifficulties { get; private set; } = new List<DifficultyData>();


        /// <summary>
        /// Loads the Settings from the file
        /// </summary>
        public static void LoadSettings()
        {
            if(!Directory.Exists(FolderLocation))
            {
                Directory.CreateDirectory(FolderLocation);
            }

            if (!File.Exists(FileLocation))
            {
                //If the settings.ini does not exist at application start, create it and put default values for all the settings
                DisplayAnimations = true;
                PlaySounds = true;
                SaveOnExit = false;
                ContinueSavedGames = false;
                ShowTips = false;
                AllowQuestionMark = true;
                Difficulty = DifficultyLevel.Beginner;

                StreamWriter sw = new StreamWriter(File.Open(FileLocation, FileMode.OpenOrCreate, FileAccess.Write));
                sw.WriteLine($"{nameof(DisplayAnimations)} = {DisplayAnimations}");
                sw.WriteLine($"{nameof(PlaySounds)} = {PlaySounds}");
                sw.WriteLine($"{nameof(SaveOnExit)} = {SaveOnExit}");
                sw.WriteLine($"{nameof(ContinueSavedGames)} = {ContinueSavedGames}");
                sw.WriteLine($"{nameof(ShowTips)} = {ShowTips}");
                sw.WriteLine($"{nameof(AllowQuestionMark)} = {AllowQuestionMark}");
                sw.WriteLine($"{nameof(Difficulty)} = {Difficulty}");
                sw.Dispose();

                InitializeDifficulties();
                return;
            }

            StreamReader sr = new StreamReader(File.Open(FileLocation, FileMode.Open, FileAccess.Read));

            //Reading the settings from the file and storing them
            while(!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var settingName = line.Substring(0, line.IndexOf(' '));
                var value = line.Substring(line.LastIndexOf(' '), line.Length - line.LastIndexOf(' '));

                switch(settingName)
                {
                    case "DisplayAnimations":
                        DisplayAnimations = Convert.ToBoolean(value);
                        break;
                    case "PlaySounds":
                        PlaySounds = Convert.ToBoolean(value);
                        break;
                    case "SaveOnExit":
                        SaveOnExit = Convert.ToBoolean(value);
                        break;
                    case "ContinueSavedGames":
                        ContinueSavedGames = Convert.ToBoolean(value);
                        break;
                    case "ShowTips":
                        ShowTips = Convert.ToBoolean(value);
                        break;
                    case "AllowQuestionMark":
                        AllowQuestionMark = Convert.ToBoolean(value);
                        break;
                    case "Difficulty":
                        value = value.Substring(1, value.Length - 1);
                        if (string.Equals(value, DifficultyLevel.Beginner.ToString()))
                        {
                            Difficulty = DifficultyLevel.Beginner;
                        }
                        else if (string.Equals(value, DifficultyLevel.Intermediate.ToString()))
                        {
                            Difficulty = DifficultyLevel.Intermediate;
                        }
                        else if (string.Equals(value, DifficultyLevel.Advanced.ToString()))
                        {
                            Difficulty = DifficultyLevel.Advanced;
                        }
                        else if (string.Equals(value, DifficultyLevel.Custom.ToString()))
                        {
                            Difficulty = DifficultyLevel.Custom;
                        }
                        break;
                }
                
            }

            sr.Dispose();
            InitializeDifficulties();
        }

        /// <summary>
        /// populates the <see cref="AllDifficulties"/>
        /// </summary>
        private static void InitializeDifficulties()
        {
            AllDifficulties.Add(new DifficultyData()
            {
                Rows = 9,
                Columns = 9,
                TotalMines = 10,
                Level = DifficultyLevel.Beginner
            }); 
            AllDifficulties.Add(new DifficultyData()
            {
                Rows = 16,
                Columns = 16,
                TotalMines = 40,
                Level = DifficultyLevel.Intermediate
            }); 
            AllDifficulties.Add(new DifficultyData()
            {
                Rows = 16,
                Columns = 30,
                TotalMines = 99,
                Level = DifficultyLevel.Advanced
            });

            var customFileLocation = $"{FolderLocation}/CustomSettings.ini";

            if(!File.Exists(customFileLocation))
            {
                StreamWriter sw = new StreamWriter(File.Open(customFileLocation, FileMode.OpenOrCreate, FileAccess.Write));
                sw.WriteLine($"Rows = 9");
                sw.WriteLine($"Columns = 9");
                sw.WriteLine($"TotalMines = 10");
                sw.Dispose();
            }

            StreamReader sr = new StreamReader(File.Open(customFileLocation, FileMode.Open, FileAccess.Read));
            int row = 0, column = 0, totalMines = 0;

            while(!sr.EndOfStream)
            {
                var line = sr.ReadLine();
                var name = line.Substring(0, line.IndexOf(' '));
                var answer = line[line.LastIndexOf(' ')..];

                switch(name)
                {
                    case "Rows":
                        row = Convert.ToInt32(answer);
                        break;
                    case "Columns":
                        column = Convert.ToInt32(answer);
                        break;
                    case "TotalMines":
                        totalMines = Convert.ToInt32(answer);
                        break;
                }
            }

            AllDifficulties.Add(new DifficultyData()
            {
                Rows = row,
                Columns = column,
                TotalMines = totalMines,
                Level = DifficultyLevel.Custom
            });

            var index = AllDifficulties.IndexOf(AllDifficulties.Where(dif => dif.Level == Difficulty).ToList()[0]);

            AllDifficulties[index].IsSelectedDifficilty = true;

        }


        /// <summary>
        /// Saves the current settings into the file
        /// </summary>
        internal static void SaveSettings(bool da, bool ps, bool soe, bool csg, bool st, bool aqm, DifficultyLevel level)
        {
            displayAnimations = da;
            playSounds = ps;
            saveOnExit = soe;
            continueSavedGames = csg;
            showTips = st;
            allowQuestionMarks = aqm;
            difficulty = level;

            if(!Directory.Exists(FolderLocation))
            {
                Directory.CreateDirectory(FolderLocation);
                File.Create(FileLocation);
            }

            if(!File.Exists(FileLocation))
            {
                File.Create(FileLocation);
            }
            else
            {
                File.Delete(FileLocation);
            }


            StreamWriter sw = new StreamWriter(File.Open(FileLocation, FileMode.OpenOrCreate, FileAccess.Write));
            sw.WriteLine($"{nameof(DisplayAnimations)} = {DisplayAnimations}");
            sw.WriteLine($"{nameof(PlaySounds)} = {PlaySounds}");
            sw.WriteLine($"{nameof(SaveOnExit)} = {SaveOnExit}");
            sw.WriteLine($"{nameof(ContinueSavedGames)} = {ContinueSavedGames}");
            sw.WriteLine($"{nameof(ShowTips)} = {ShowTips}");
            sw.WriteLine($"{nameof(AllowQuestionMark)} = {AllowQuestionMark}");
            sw.WriteLine($"{nameof(Difficulty)} = {Difficulty}");
            sw.Dispose();
        }
    }
}