using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Threading;

namespace Minesweeper.Core
{
    /// <summary>
    /// View Model for the actual game
    /// </summary>
    public class GameViewModel : BindableBase
    {
        #region Private Members

        private int columns;
        private int rows;
        private int remainingMines;
        private bool closeTrigger;
        private string currentTime;

        /// <summary>
        /// The dialogService for our application
        /// </summary>
        private readonly IDialogService Service;

        /// <summary>
        /// a dispatcher timer to use raise the event to update the currentTime every second
        /// </summary>
        private readonly DispatcherTimer dispatcherTimer = new DispatcherTimer();

        /// <summary>
        /// The stopwatch which record the time taken
        /// </summary>
        private Stopwatch stopwatch = new Stopwatch();

        #endregion

        #region Public Properties

        /// <summary>
        /// Stores instances of all the tiles on the board
        /// </summary>
        public ObservableCollection<Tile> AllTiles { get; set; }

        /// <summary>
        /// Stores the total number of columns in the board
        /// </summary>
        public int Columns
        {
            get
            {
                return columns;
            }
            set
            {
                SetProperty(ref columns, value);

            }
        }

        /// <summary>
        /// Stores if the current game is a new game
        /// </summary>
        public GameProgress Progress { get; private set; }

        /// <summary>
        /// Stores the total number of rows in the board
        /// </summary>
        public int Rows
        {
            get
            {
                return rows;
            }
            set
            {
                SetProperty(ref rows, value);
            }
        }

        /// <summary>
        /// Stores the total number of mines in the game
        /// </summary>
        public int TotalMines { get; set; }

        /// <summary>
        /// Stores total mines minus the number of mines which are marked flagged
        /// </summary>
        public int RemainingMines
        {
            get
            {
                return remainingMines;
            }
            set
            {
                SetProperty(ref remainingMines, value);
            }
        }

        /// <summary>
        /// Stores the current time taken by the user in the current game
        /// </summary>
        public string CurrentTime
        {
            get { return currentTime; }
            set { SetProperty(ref currentTime, value); }
        }

        /// <summary>
        /// If true, the windows should be closed
        /// </summary>
        public bool CloseTrigger
        {
            get { return closeTrigger; }
            set { SetProperty(ref closeTrigger, value); }
        }

        #endregion

        #region Public ICommands

        public DelegateCommand<Tile> OpenTileCommand => new DelegateCommand<Tile>(OpenTile);

        public DelegateCommand<Tile> ToggleStateCommand => new DelegateCommand<Tile>(ToggleTileState);

        public DelegateCommand RestartGameCommand => new DelegateCommand(RestartGame);

        public DelegateCommand ExitGameCommand => new DelegateCommand(ExitGame);

        public DelegateCommand OpenSettingsCommand => new DelegateCommand(delegate()
        { 
            Service.ShowDialog("Settings", new DialogParameters(), callback => SettingsCallback(callback)); 
        });

        public DelegateCommand OpenStatisticsCommand => new DelegateCommand(delegate ()
        {
            Service.ShowDialog("Statistics", new DialogParameters(), callback => { });
        });

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public GameViewModel(IDialogService _service)
        {
            Service = _service;
            Progress = GameProgress.NewGame;
            AllTiles = new ObservableCollection<Tile>();
            Rows = 9;
            Columns = 9;
            TotalMines = 10;
            RemainingMines = TotalMines;
            CurrentTime = "0";
            dispatcherTimer.Tick += UpdateTimer;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            InitlializeTiles();
        }

        #endregion

        #region Event Methods

        /// <summary>
        /// Opens the tile that is passed in
        /// </summary>
        /// <param name="tile">the tile to open</param>
        private void OpenTile(Tile tile)
        {
            //If it is a new game, set up the board and all it's tiles
            if (Progress == GameProgress.NewGame)
            {
                PlaceMines(tile);
            }

            //Since we cannot open any tile which is flagged or question marked or uncovered, we just don't do anything and return
            if (tile.State != TileState.Covered)
                return;

            //Show the dialog that the player lost since s/he clicked on a mine
            if (tile.Type == TileType.Bomb)
            {
                bool isWin = false;
                dispatcherTimer.Stop();
                stopwatch.Stop();
                Service.ShowDialog("GameEnd", new DialogParameters($"isWin={isWin}"), result => GameEndCallback(result));
                return;
            }

            tile.State = TileState.Uncovered;

            //If the tile has a number in it, do not open any adjacent tiles
            if (tile.Type == TileType.Number)
                return;

            //Here is the logic of opening all the relevant tiles.
            //First when the tiles are initialized, all the tile's IsItAdjacentTile property will be false
            //So when the user clicks on the tile, it checks if it is false and then turns the property to true for all of it's adjacent tiles
            //If the adjacent tile is empty tile, it will do the same loop for them too until all of the empty tiles are opened and the tiles with numbers are left
            if (!tile.IsItAdjacentTile || (tile.IsItAdjacentTile && tile.Type == TileType.None))
            {
                for (int index = 0; index < AllTiles[AllTiles.IndexOf(tile)].AdjacentTiles.Count; index++)
                {
                    //Since AllTiles and the AdjacentTiles(of the Tile instance that is passed in) have the same instances of objects
                    //We are making changes only to the instances in AllTiles to keep track of all of them
                    AllTiles[AllTiles.IndexOf(tile)].AdjacentTiles[index].IsItAdjacentTile = true;
                }
                foreach (var adjacentTile in tile.AdjacentTiles)
                {
                    OpenTile(adjacentTile);
                }
            }
        }

        /// <summary>
        /// Toggles the <see cref="TileState"/> of the passed in tile
        /// </summary>
        /// <param name="tile">the tile to toggle the state of</param>
        private void ToggleTileState(Tile tile)
        {
            var index = AllTiles.IndexOf(tile);

            if (tile.State == TileState.Covered)
            {
                AllTiles[index].State = TileState.Flagged;
                --RemainingMines;
            }
            else if (tile.State == TileState.Flagged)
            {
                AllTiles[index].State = TileState.QuestionMarked;
                ++RemainingMines;
            }
            else if (tile.State == TileState.QuestionMarked)
                AllTiles[index].State = TileState.Covered;
        }

        /// <summary>
        /// Exits the game
        /// </summary>
        private void ExitGame()
        {
            CloseTrigger = true;
        }

        #endregion

        #region Dialog Callbacks

        /// <summary>
        /// Method to run after a GameEnd dialog is closed
        /// </summary>
        /// <param name="result">the result of the dialog</param>
        private void GameEndCallback(IDialogResult result)
        {
            if (result.Result == ButtonResult.Abort)
            {
                ExitGame();
            }
            else if (result.Result == ButtonResult.OK)
            {
                RestartGame();
            }
        }

        /// <summary>
        /// Method to run after settings window has been closed
        /// </summary>
        /// <param name="callback"></param>
        private void SettingsCallback(IDialogResult callback)
        {
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Populates the <see cref="AllTiles"/>
        /// </summary>
        private void InitlializeTiles()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    AllTiles.Add(new Tile()
                    {
                        Row = i,
                        Column = j,
                        State = TileState.Covered
                    });
                }
            }
        }

        /// <summary>
        /// Places the miens randomly on the board
        /// </summary>
        /// <param name="tile">the tile to not put bomb in because it is the first tile clicked</param>
        private void PlaceMines(Tile tile)
        {
            Progress = GameProgress.InProgress;

            //Place mines after the first mine has been uncovered
            var placedMines = 0;
            Random rand = new Random();

            while (placedMines != TotalMines)
            {
                int xRow = rand.Next(0, Rows - 1);
                int xCol = rand.Next(0, Columns - 1);
                foreach (var t in AllTiles.Where(singleTile => singleTile.Row == xRow && singleTile.Column == xCol))
                {
                    if (t != tile)
                    {
                        AllTiles[AllTiles.IndexOf(t)].Type = TileType.Bomb;
                        placedMines++;
                    }
                }
            }

            //Storing instances of adjacentTiles in every tile and changing
            foreach (var singleTile in AllTiles)
            {
                singleTile.InitializeAdjacentTile(this);
                singleTile.ChangeContent();
            }

            stopwatch.Start();
            dispatcherTimer.Start();
        }

        /// <summary>
        /// Restarts the game
        /// </summary>
        private void RestartGame()
        {
            Service.Show("TestView",new DialogParameters(), hi => { });
            AllTiles.Clear();
            RemainingMines = TotalMines;
            CurrentTime = "0";
            Progress = GameProgress.NewGame;
            InitlializeTiles();
            stopwatch = new Stopwatch();
        }

        /// <summary>
        /// Updates the CurrentTime every second
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateTimer(object sender, EventArgs e)
        {
            if (stopwatch.IsRunning)
            {
                TimeSpan ts = stopwatch.Elapsed;
                CurrentTime = ts.Seconds.ToString();
            }
        }

        #endregion
    }
}