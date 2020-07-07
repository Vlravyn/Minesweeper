using System;
using System.IO;

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
        /// Stores the folder location of the local appdata
        /// </summary>
        private static string FolderLocation = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/Minesweeper";

        /// <summary>
        /// stores the location of the settings file
        /// </summary>
        private static string FileLocation = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/Minesweeper/Settings.ini";

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
            private set { difficulty = value; }
        }

        #endregion

        /// <summary>
        /// Loads the Settings from the file
        /// </summary>
        public static void LoadSettings()
        {
            if(!Directory.Exists(FolderLocation))
            {
                Directory.CreateDirectory(FolderLocation);
                File.Create(FileLocation);
            }

            if (!File.Exists(FileLocation))
            {
                //If the settings.ini does not exist at application start, create it and put default values for all the settings
                File.Create(FileLocation);

                DisplayAnimations = true;
                PlaySounds = true;
                SaveOnExit = false;
                ContinueSavedGames = false;
                ShowTips = false;
                AllowQuestionMark = true;
                Difficulty = DifficultyLevel.Beginner;

                StreamWriter sw = new StreamWriter(File.Open(FileLocation, FileMode.Open, FileAccess.Write));
                sw.WriteLine($"{nameof(DisplayAnimations)} = {DisplayAnimations}");
                sw.WriteLine($"{nameof(PlaySounds)} = {PlaySounds}");
                sw.WriteLine($"{nameof(SaveOnExit)} = {SaveOnExit}");
                sw.WriteLine($"{nameof(ContinueSavedGames)} = {ContinueSavedGames}");
                sw.WriteLine($"{nameof(ShowTips)} = {ShowTips}");
                sw.WriteLine($"{nameof(AllowQuestionMark)} = {AllowQuestionMark}");
                sw.WriteLine($"{nameof(Difficulty)} = {Difficulty}");
                sw.Dispose();

                return;
            }

            StreamReader sr = new StreamReader(File.Open(FileLocation, FileMode.Open, FileAccess.Write));

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
                        if (nameof(DifficultyLevel.Beginner) == value)
                            Difficulty = DifficultyLevel.Beginner; 
                        else if (nameof(DifficultyLevel.Intermediate) == value)
                            Difficulty = DifficultyLevel.Intermediate;
                        else if (nameof(DifficultyLevel.Advanced) == value)
                            Difficulty = DifficultyLevel.Advanced;
                        else if (nameof(DifficultyLevel.Custom) == value)
                            Difficulty = DifficultyLevel.Custom;
                        break;
                }
            }

            sr.Dispose();
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

            StreamWriter sw = new StreamWriter(File.Open(FileLocation, FileMode.Open, FileAccess.Write));
            sw.WriteLine($"{nameof(DisplayAnimations)} = {DisplayAnimations}");
            sw.WriteLine($"{nameof(PlaySounds)} = {PlaySounds}");
            sw.WriteLine($"{nameof(SaveOnExit)} = {SaveOnExit}");
            sw.WriteLine($"{nameof(ContinueSavedGames)} = {ContinueSavedGames}");
            sw.WriteLine($"{nameof(ShowTips)} = {ShowTips}");
            sw.WriteLine($"{nameof(AllowQuestionMark)} = {AllowQuestionMark}");
            sw.Dispose();
        }
    }
}