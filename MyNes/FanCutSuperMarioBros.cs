using MyNes.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyNes
{
    internal class FanCutSuperMarioBros
    {
        // TODO: Add timeline highlights
        // TODO: Do a new checkout and do a final compare with the virgin mynes code
        // TODO: Change EXE build name
        // TODO: Add compiler directives for each build
        // TODO: Add assets to source control
        // TODO: Move MyNES menu items into one submenu

        // Things I Changed in virgin MyNES:
        // commented out some unused code that was causing compiler warnings
        // fixed mess of build configurations
        // added a hard reset event to core
        // changed a few accessibility levels
        // added memory change trigger functionality
        // added memory read trigger functionality
        // commented out zlib compression on save states (for easier save state file analysis)
        // commented out support for acrobat reading of manual (so we don't need the library)
        // Main Form:
        //     Removed sizing based on settings
        //     Added instantiation of FanCut on loaded
        //     Added locking of window and play surface
        //     Added injection of Timeline and Log into form
        //     Removed changing or window title
        //     Changed window title to "FanCut"

        #region Memory Locations and Values

        private const ushort PLAYER_STATE_ADDRESS = 0x000E;
        private const byte PLAYER_STATE_LEFTMOST_OF_SCREEN = 0x00;
        private const byte PLAYER_STATE_DIED = 0x06;
        private const byte PLAYER_STATE_NORMAL = 0x08;

        private const ushort PLAYER_SIZE_STATE_ADDRESS = 0x0754;
        private const byte PLAYER_SIZE_STATE_BIG = 0x00;

        private const ushort PLAYER_POWERUP_STATE_ADDRESS = 0x0756;
        private const byte PLAYER_POWERUP_STATE_FIERY = 0x02;

        private const ushort NUMBER_OF_PLAYERS_ADDRESS = 0x077A;

        private const ushort PLAYER_LIVES_ADDRESS = 0x075A;
        private const ushort PLAYER_COINS_ADDRESS = 0x075E;
        private const ushort COINS_DIGITS_ADDRESS = 0x07ED;
        private const ushort WORLD_NUMBER_ADDRESS = 0x075F;
        private const ushort LEVEL_DISPLAY_NUMBER_ADDRESS = 0x075C;
        private const ushort LEVEL_NUMBER_ADDRESS = 0x0760;
        private const ushort LEVEL_CODE_ADDRESS = 0x0750;
        private const ushort LEVEL_SCREEN_ADDRESS = 0x071A;
        private const ushort STARTING_LEVEL_SCREEN_ADDRESS = 0x075B;

        #endregion

        private const string GAME_NAME = "Super Mario Bros";
        private const string ASSETS_PATH = @"Assets\" + GAME_NAME;
        private const string SHA1_HASH = "EA343F4E445A9050D4B4FBAC2C77D0693B1D0922";
        private const string TIMELINE_SAVE_FILE_EXTENSION = ".tls";
        private const string TIMELINE_SAVE_THUMBNAIL_FILE_EXTENSION = ".png";
        private const string CHECKPOINT_FILENAME_SUFFIX = " (checkpoint)";

        private FanCutCommon _fanCutCommon;

        private List<SuperMarioBrosLevel> _levels;
        private List<TimelineSave> _timelineSaves;

        internal FanCutSuperMarioBros(FormMain formMain)
        {
            _levels = new List<SuperMarioBrosLevel>();

            _levels.Add(new SuperMarioBrosLevel() { Name = "World 1-1", WorldNumber = 0, LevelDisplayNumber = 0, LevelNumber = 0, AreaLoadedValue = 0x25, CheckpointScreenNumber = 5 });
            _levels.Add(new SuperMarioBrosLevel() { Name = "World 1-2", WorldNumber = 0, LevelDisplayNumber = 1, LevelNumber = 2, AreaLoadedValue = 0x40, CheckpointScreenNumber = 6 });
            _levels.Add(new SuperMarioBrosLevel() { Name = "World 1-3", WorldNumber = 0, LevelDisplayNumber = 2, LevelNumber = 3, AreaLoadedValue = 0x26, CheckpointScreenNumber = 4 });
            _levels.Add(new SuperMarioBrosLevel() { Name = "World 1-4", WorldNumber = 0, LevelDisplayNumber = 3, LevelNumber = 4, AreaLoadedValue = 0x60, CheckpointScreenNumber = 0 });

            _levels.Add(new SuperMarioBrosLevel() { Name = "World 2-1", WorldNumber = 1, LevelDisplayNumber = 0, LevelNumber = 0, AreaLoadedValue = 0x28, CheckpointScreenNumber = 6 });
            _levels.Add(new SuperMarioBrosLevel() { Name = "World 2-2", WorldNumber = 1, LevelDisplayNumber = 1, LevelNumber = 2, AreaLoadedValue = 0x01, CheckpointScreenNumber = 5 });
            _levels.Add(new SuperMarioBrosLevel() { Name = "World 2-3", WorldNumber = 1, LevelDisplayNumber = 2, LevelNumber = 3, AreaLoadedValue = 0x27, CheckpointScreenNumber = 7 });
            _levels.Add(new SuperMarioBrosLevel() { Name = "World 2-4", WorldNumber = 1, LevelDisplayNumber = 3, LevelNumber = 4, AreaLoadedValue = 0x62, CheckpointScreenNumber = 0 });

            _levels.Add(new SuperMarioBrosLevel() { Name = "World 3-1", WorldNumber = 2, LevelDisplayNumber = 0, LevelNumber = 0, AreaLoadedValue = 0x24, CheckpointScreenNumber = 6 });
            _levels.Add(new SuperMarioBrosLevel() { Name = "World 3-2", WorldNumber = 2, LevelDisplayNumber = 1, LevelNumber = 1, AreaLoadedValue = 0x35, CheckpointScreenNumber = 6 });
            _levels.Add(new SuperMarioBrosLevel() { Name = "World 3-3", WorldNumber = 2, LevelDisplayNumber = 2, LevelNumber = 2, AreaLoadedValue = 0x20, CheckpointScreenNumber = 4 });
            _levels.Add(new SuperMarioBrosLevel() { Name = "World 3-4", WorldNumber = 2, LevelDisplayNumber = 3, LevelNumber = 3, AreaLoadedValue = 0x63, CheckpointScreenNumber = 0 });

            _levels.Add(new SuperMarioBrosLevel() { Name = "World 4-1", WorldNumber = 3, LevelDisplayNumber = 0, LevelNumber = 0, AreaLoadedValue = 0x22, CheckpointScreenNumber = 6 });
            _levels.Add(new SuperMarioBrosLevel() { Name = "World 4-2", WorldNumber = 3, LevelDisplayNumber = 1, LevelNumber = 2, AreaLoadedValue = 0x41, CheckpointScreenNumber = 6 });
            _levels.Add(new SuperMarioBrosLevel() { Name = "World 4-3", WorldNumber = 3, LevelDisplayNumber = 2, LevelNumber = 3, AreaLoadedValue = 0x2C, CheckpointScreenNumber = 4 });
            _levels.Add(new SuperMarioBrosLevel() { Name = "World 4-4", WorldNumber = 3, LevelDisplayNumber = 3, LevelNumber = 4, AreaLoadedValue = 0x61, CheckpointScreenNumber = 0 });

            _levels.Add(new SuperMarioBrosLevel() { Name = "World 5-1", WorldNumber = 4, LevelDisplayNumber = 0, LevelNumber = 0, AreaLoadedValue = 0x2A, CheckpointScreenNumber = 6 });
            _levels.Add(new SuperMarioBrosLevel() { Name = "World 5-2", WorldNumber = 4, LevelDisplayNumber = 1, LevelNumber = 1, AreaLoadedValue = 0x31, CheckpointScreenNumber = 6 });
            _levels.Add(new SuperMarioBrosLevel() { Name = "World 5-3", WorldNumber = 4, LevelDisplayNumber = 2, LevelNumber = 2, AreaLoadedValue = 0x26, CheckpointScreenNumber = 4 });
            _levels.Add(new SuperMarioBrosLevel() { Name = "World 5-4", WorldNumber = 4, LevelDisplayNumber = 3, LevelNumber = 3, AreaLoadedValue = 0x62, CheckpointScreenNumber = 0 });

            _levels.Add(new SuperMarioBrosLevel() { Name = "World 6-1", WorldNumber = 5, LevelDisplayNumber = 0, LevelNumber = 0, AreaLoadedValue = 0x2E, CheckpointScreenNumber = 6 });
            _levels.Add(new SuperMarioBrosLevel() { Name = "World 6-2", WorldNumber = 5, LevelDisplayNumber = 1, LevelNumber = 1, AreaLoadedValue = 0x23, CheckpointScreenNumber = 6 });
            _levels.Add(new SuperMarioBrosLevel() { Name = "World 6-3", WorldNumber = 5, LevelDisplayNumber = 2, LevelNumber = 2, AreaLoadedValue = 0x2D, CheckpointScreenNumber = 6 });
            _levels.Add(new SuperMarioBrosLevel() { Name = "World 6-4", WorldNumber = 5, LevelDisplayNumber = 3, LevelNumber = 3, AreaLoadedValue = 0x60, CheckpointScreenNumber = 0 });

            _levels.Add(new SuperMarioBrosLevel() { Name = "World 7-1", WorldNumber = 6, LevelDisplayNumber = 0, LevelNumber = 0, AreaLoadedValue = 0x33, CheckpointScreenNumber = 6 });
            _levels.Add(new SuperMarioBrosLevel() { Name = "World 7-2", WorldNumber = 6, LevelDisplayNumber = 1, LevelNumber = 2, AreaLoadedValue = 0x01, CheckpointScreenNumber = 5 });
            _levels.Add(new SuperMarioBrosLevel() { Name = "World 7-3", WorldNumber = 6, LevelDisplayNumber = 2, LevelNumber = 3, AreaLoadedValue = 0x27, CheckpointScreenNumber = 7 });
            _levels.Add(new SuperMarioBrosLevel() { Name = "World 7-4", WorldNumber = 6, LevelDisplayNumber = 3, LevelNumber = 4, AreaLoadedValue = 0x64, CheckpointScreenNumber = 0 });

            _levels.Add(new SuperMarioBrosLevel() { Name = "World 8-1", WorldNumber = 7, LevelDisplayNumber = 0, LevelNumber = 0, AreaLoadedValue = 0x30, CheckpointScreenNumber = 12 }); // no checkpoint in the actual game
            _levels.Add(new SuperMarioBrosLevel() { Name = "World 8-2", WorldNumber = 7, LevelDisplayNumber = 1, LevelNumber = 1, AreaLoadedValue = 0x32, CheckpointScreenNumber = 6 }); // no checkpoint in the actual game
            _levels.Add(new SuperMarioBrosLevel() { Name = "World 8-3", WorldNumber = 7, LevelDisplayNumber = 2, LevelNumber = 2, AreaLoadedValue = 0x21, CheckpointScreenNumber = 6 }); // no checkpoint in the actual game
            _levels.Add(new SuperMarioBrosLevel() { Name = "World 8-4", WorldNumber = 7, LevelDisplayNumber = 3, LevelNumber = 3, AreaLoadedValue = 0x65, CheckpointScreenNumber = 0 });
            
            // this will override all the current saves, only do this when regenerating saves
            //upateTimelineSaves();

            _timelineSaves = new List<TimelineSave>();
            int index = 0;
            foreach (SuperMarioBrosLevel level in _levels)
            {
                TimelineSave timelineSave = new TimelineSave()
                {
                    ID = index,
                    Name = level.Name,
                    Filename = level.Name + TIMELINE_SAVE_FILE_EXTENSION,
                    ThumbnailFilename = level.Name + TIMELINE_SAVE_THUMBNAIL_FILE_EXTENSION
                };

                _timelineSaves.Add(timelineSave);
                index++;

                if (level.CheckpointScreenNumber != 0)
                {
                    timelineSave = new TimelineSave()
                    {
                        ID = index,
                        Name = level.Name + CHECKPOINT_FILENAME_SUFFIX,
                        Filename = level.Name + CHECKPOINT_FILENAME_SUFFIX + TIMELINE_SAVE_FILE_EXTENSION,
                        ThumbnailFilename = level.Name + CHECKPOINT_FILENAME_SUFFIX + TIMELINE_SAVE_THUMBNAIL_FILE_EXTENSION
                    };

                    _timelineSaves.Add(timelineSave);
                    index++;
                }
            }

            NesEmu.ChangeTriggers = new List<ushort>();
            NesEmu.ChangeTriggers.Add(PLAYER_STATE_ADDRESS);
            NesEmu.ChangeTriggers.Add(WORLD_NUMBER_ADDRESS);
            NesEmu.ChangeTriggers.Add(LEVEL_NUMBER_ADDRESS);
            NesEmu.ChangeTriggerHandler = onMemoryChanging;

            _fanCutCommon = new FanCutCommon(formMain, GAME_NAME, ASSETS_PATH, _timelineSaves, SHA1_HASH);
        }

        #region Memory Trigger Handlers

        private void onMemoryChanging(ushort address, byte previousValue, byte newValue)
        {
            switch (address)
            {
                case PLAYER_STATE_ADDRESS:
                    onPlayerStateChanging(previousValue, newValue);
                    break;

                case WORLD_NUMBER_ADDRESS:
                    onWorldNumberChanging(previousValue, newValue);
                    break;

                case LEVEL_NUMBER_ADDRESS:
                    onLevelNumberChanging(previousValue, newValue);
                    break;
            }
        }

        private void onPlayerStateChanging(byte previousValue, byte newValue)
        {
            byte playerLives = NesEmu.WRAM[PLAYER_LIVES_ADDRESS & 0x7FF];

            if ((previousValue == PLAYER_STATE_NORMAL) && (newValue == PLAYER_STATE_LEFTMOST_OF_SCREEN) && (playerLives == 2))
            {
                _fanCutCommon.WriteLogMessage("Detected player start, loading first timeline save.");

                if (NesEmu.WRAM[NUMBER_OF_PLAYERS_ADDRESS & 0x7FF] == 0x01)
                    _fanCutCommon.WriteLogMessage("This FanCut only supports single player.");

                _fanCutCommon.ResetThenLoadTimelineSave(_timelineSaves.First().Filename);
            }

            if ((previousValue == PLAYER_STATE_DIED) && (newValue == PLAYER_STATE_LEFTMOST_OF_SCREEN))
            {
                byte worldNumber = NesEmu.WRAM[WORLD_NUMBER_ADDRESS & 0x7FF];
                byte levelNumber = NesEmu.WRAM[LEVEL_NUMBER_ADDRESS & 0x7FF];
                byte levelScreenNumber = NesEmu.WRAM[LEVEL_SCREEN_ADDRESS & 0x7FF];

                SuperMarioBrosLevel currentLevel = (from l in _levels where (l.WorldNumber == worldNumber) && (l.LevelNumber == levelNumber) select l).FirstOrDefault();
                if (currentLevel != null)
                {
                    bool isPastCheckpoint = (currentLevel.CheckpointScreenNumber != 0) && (levelScreenNumber >= currentLevel.CheckpointScreenNumber);

                    string timelineSaveName = currentLevel.Name + (isPastCheckpoint ? CHECKPOINT_FILENAME_SUFFIX : string.Empty);
                    string timelineSaveFilename = (from ts in _timelineSaves where ts.Name == timelineSaveName select ts.Filename).FirstOrDefault();

                    if (!string.IsNullOrWhiteSpace(timelineSaveFilename))
                    {
                        _fanCutCommon.WriteLogMessage(string.Format("Detected player death on {0}{1}.", currentLevel.Name, (isPastCheckpoint ? " (past checkpoint)" : string.Empty)));
                        _fanCutCommon.ResetThenLoadTimelineSave(timelineSaveFilename);
                    }
                }
            }
        }

        private void onWorldNumberChanging(byte previousValue, byte newValue)
        {
            byte worldNumber = newValue;
            byte levelNumber = NesEmu.WRAM[LEVEL_NUMBER_ADDRESS & 0x7FF];
            loadTimelineSaveOnLevelCompletion(worldNumber, levelNumber);
        }

        private void onLevelNumberChanging(byte previousValue, byte newValue)
        {
            if (previousValue < newValue)
            {
                byte worldNumber = NesEmu.WRAM[WORLD_NUMBER_ADDRESS & 0x7FF];
                byte levelNumber = newValue;
                loadTimelineSaveOnLevelCompletion(worldNumber, levelNumber);
            }
        }

        private void loadTimelineSaveOnLevelCompletion(byte worldNumber, byte levelNumber)
        {
            SuperMarioBrosLevel currentLevel = (from l in _levels where (l.WorldNumber == worldNumber) && (l.LevelNumber == levelNumber) select l).FirstOrDefault();
            if (currentLevel != null)
                if (_levels.IndexOf(currentLevel) != 0)
                {
                    SuperMarioBrosLevel previousLevel = _levels[_levels.IndexOf(currentLevel) - 1];

                    _fanCutCommon.WriteLogMessage(string.Format("Detected completion of {0}", previousLevel.Name));

                    _fanCutCommon.ResetThenLoadTimelineSave((from ts in _timelineSaves where ts.Name == currentLevel.Name select ts.Filename).First());
                }
        }

        #endregion

        #region Miscellaneous Routines

        private void upateTimelineSaves()
        {
            foreach (SuperMarioBrosLevel level in _levels)
            {
                FileStream saveStateStream = new FileStream(@"Assets\base.tls", FileMode.Open, FileAccess.Read);
                byte[] saveState = new byte[saveStateStream.Length];
                saveStateStream.Read(saveState, 0, saveState.Length);
                saveStateStream.Close();

                const ushort WRAM_OFFSET = 0x510C;

                saveState[WRAM_OFFSET + PLAYER_SIZE_STATE_ADDRESS] = PLAYER_SIZE_STATE_BIG;
                saveState[WRAM_OFFSET + PLAYER_POWERUP_STATE_ADDRESS] = PLAYER_POWERUP_STATE_FIERY;

                saveState[WRAM_OFFSET + PLAYER_LIVES_ADDRESS] = 4;
                saveState[WRAM_OFFSET + PLAYER_COINS_ADDRESS] = 90;
                saveState[WRAM_OFFSET + COINS_DIGITS_ADDRESS] = 9;
                saveState[WRAM_OFFSET + COINS_DIGITS_ADDRESS + 1] = 0;

                saveState[WRAM_OFFSET + WORLD_NUMBER_ADDRESS] = level.WorldNumber;
                saveState[WRAM_OFFSET + LEVEL_DISPLAY_NUMBER_ADDRESS] = level.LevelDisplayNumber;
                saveState[WRAM_OFFSET + LEVEL_NUMBER_ADDRESS] = level.LevelNumber;
                saveState[WRAM_OFFSET + LEVEL_CODE_ADDRESS] = level.AreaLoadedValue;

                saveStateStream = new FileStream(@"Assets\" + level.Name + ".tls", FileMode.Create, FileAccess.Write);
                saveStateStream.Write(saveState, 0, saveState.Length);
                saveStateStream.Close();

                if (level.CheckpointScreenNumber != 0)
                {
                    saveState[WRAM_OFFSET + STARTING_LEVEL_SCREEN_ADDRESS] = level.CheckpointScreenNumber;

                    saveStateStream = new FileStream(@"Assets\" + level.Name + " (checkpoint).tls", FileMode.Create, FileAccess.Write);
                    saveStateStream.Write(saveState, 0, saveState.Length);
                    saveStateStream.Close();
                }
            }
        }

        #endregion
    }

    internal class SuperMarioBrosLevel
    {
        internal string Name { get; set; }
        internal byte WorldNumber { get; set; }
        internal byte LevelDisplayNumber { get; set; }
        internal byte LevelNumber { get; set; }
        internal byte AreaLoadedValue { get; set; }
        internal byte CheckpointScreenNumber { get; set; }
    }
}
