namespace Minesweeper.Core
{
    public enum TileState
    {
        /// <summary>
        /// the tile is covered and yet to be interacted with
        /// </summary>
        Covered,

        /// <summary>
        /// the tile is question marked
        /// </summary>
        QuestionMarked,

        /// <summary>
        /// the tile has been flagged and cannot be interacted with
        /// </summary>
        Flagged,

        /// <summary>
        /// the tile has been opened and is disabled
        /// </summary>
        Uncovered
    }
}