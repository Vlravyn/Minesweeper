namespace Minesweeper.Core
{
    /// <summary>
    /// Represents the type of the tile
    /// </summary>
    public enum TileType
    {
        /// <summary>
        /// Represents Empty Tile
        /// </summary>
        None,

        /// <summary>
        /// Represents a Tile with a number
        /// </summary>
        Number,

        /// <summary>
        /// Represents a tile with bomb
        /// </summary>
        Bomb
    }
}