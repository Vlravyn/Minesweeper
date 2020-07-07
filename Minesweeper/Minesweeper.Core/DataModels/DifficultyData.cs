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
        public DifficultyLevel Level { get; private set; }

        /// <summary>
        /// Stores the number of rows in the difficulty
        /// </summary>
        public int Rows { get; private set; }

        /// <summary>
        /// Stores the number of columns in the difficulty
        /// </summary>
        public int Columns { get; private set; }

        /// <summary>
        /// Total number of mines in this difficulty
        /// </summary>
        public int TotalMines { get; private set; }

        #region Constructor

        public DifficultyData(DifficultyLevel level)
        {
            Level = level;
        }

        #endregion

    }
}
