namespace Minesweeper.Core
{
    /// <summary>
    /// A blueprint to store all the data of all <see cref="DifficultyLevel"/>
    /// </summary>
    public class DifficultyData
    {
        /// <summary>
        /// store the level of the difficulty
        /// </summary>
        public DifficultyLevel Level { get; set; }

        /// <summary>
        /// Stores the number of rows in the difficulty
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// Stores the number of columns in the difficulty
        /// </summary>
        public int Columns { get; set; }

        /// <summary>
        /// Total number of mines in this difficulty
        /// </summary>
        public int TotalMines { get; set; }

        /// <summary>
        /// Stores if the difficulty is the difficulty which is set to default
        /// </summary>
        public bool IsSelectedDifficilty { get; set; }

        #region Constructor

        public DifficultyData()
        {
        }

        #endregion

    }
}
