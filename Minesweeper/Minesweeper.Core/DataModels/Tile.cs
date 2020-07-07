using Prism.Mvvm;
using System.Collections.Generic;

namespace Minesweeper.Core
{
    public class Tile : BindableBase
    {
        #region Private Members

        private TileState state;
        private int content;
        private string imageName;
        private TileType type;

        #endregion

        /// <summary>
        /// Stores the Type of the tile
        /// </summary>
        public TileType Type
        {
            get
            {
                return type;
            }
            set
            {
                SetProperty(ref type, value);
            }
        }

        /// <summary>
        /// Stores the current state of the tile
        /// </summary>
        public TileState State
        {
            get
            {
                return state;
            }
            set
            {
                SetProperty(ref state, value);

                if (state == TileState.Flagged)
                    ImageName = "flag.png";
                else if (state == TileState.QuestionMarked)
                    ImageName = "questionMark.png";
                else if (State == TileState.Covered)
                {
                    //Remove the image if the tile state becomes covered
                    ImageName = "";
                }
            }
        }

        /// <summary>
        /// Returns if the tile can be interacted with
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                if (State == TileState.Uncovered)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Stores the number that the tile has to show
        /// Is null if there is no number to show, in case of empty or bomb tiles
        /// </summary>
        public int Content
        {
            get
            {
                return content;
            }
            set
            {
                SetProperty(ref content, value);
            }
        }

        /// <summary>
        /// Stores the image that the tile has to show
        /// Is null if there is no image to show, in case of Covered or Uncovered <see cref="TileState"/>
        /// </summary>
        public string ImageName
        {
            get
            {
                return imageName;
            }
            set
            {
                imageName = value;
                RaisePropertyChanged(nameof(ImageName));
            }
        }

        /// <summary>
        /// Stores the instances of all the tiles
        /// </summary>
        public List<Tile> AdjacentTiles { get; set; }

        /// <summary>
        /// Stores which row this tile is in
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// stores which column this tile is in
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Stores if this tile is being opened because it is an adjacent tile or not
        /// will contain false if this tile is not being opened because of an adjacent tile
        /// </summary>
        public bool IsItAdjacentTile { get; set; }

        #region Constructor

        internal void InitializeAdjacentTile(GameViewModel vm)
        {
            //Do nothing if this tile is bomb, as adjacent tiles aren't needed for bombs
            if (Type == TileType.Bomb)
            {
                return;
            }

            //Adding adjacent tiles
            for (int r = Row - 1; r <= Row + 1; r++)
            {
                for (int c = Column - 1; c <= Column + 1; c++)
                {
                    foreach (var tile in vm.AllTiles)
                    {
                        if (tile.Row == r && tile.Column == c)
                        {
                            if (!(tile.Row == Row && tile.Column == Column))
                            {
                                AdjacentTiles.Add(tile);
                            }
                            break;
                        }
                    }
                }
            }

            //Setting which type of tile this is based on the count of mines near it
            var count = 0;
            foreach (var tile in AdjacentTiles)
            {
                if (tile.Type == TileType.Bomb)
                    count++;
            }

            Content = count;

            //Setting the Type of tile based on the number of bomb mines near it
            if (count == 0)
                Type = TileType.None;
            else
                Type = TileType.Number;
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Tile()
        {
            AdjacentTiles = new List<Tile>();
            State = TileState.Covered;
        }

        /// <summary>
        /// Changes the content of the tile to the count of bomb tiles in the <see cref="AdjacentTiles"/>
        /// </summary>
        internal void ChangeContent()
        {
            if (State == TileState.Uncovered)
            {
                ImageName = string.Empty;

                if (Type == TileType.Number)
                {
                    foreach (var tile in AdjacentTiles)
                    {
                        if (tile.Type == TileType.Bomb)
                        {
                            Content++;
                        }
                    }

                }
                else if (Type == TileType.Bomb)
                {
                    ImageName = "mine.png";
                }

                //Since the IsEnabled Property is Dependant on the State, we call property changed event every time state changes
                RaisePropertyChanged(nameof(IsEnabled));
            }
        }

        #endregion
    }
}