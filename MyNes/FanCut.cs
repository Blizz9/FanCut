using MyNes.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MyNes
{
    internal class FanCut
    {
        // TODO: Move all new source into separate class
        // TODO: Add pictures for save states that don't have them
        // TODO: Check finishing the game and if anything fucks up
        // TODO: Implement save state injection here first? Not really required.
        // TODO: Add timeline highlights
        // TODO: Add SHA-1 check on the ROM
        // TODO: Move all save states to .ss, since they are really no longer mynes saves
        // TODO: Tell the user 2 players isnt supported
        // TODO: FanCut? - rename the containing folder based on this decision

        #region Memory Locations and Values

        private const ushort PLAYER_STATE_ADDRESS = 0x000E;
        private const byte PLAYER_STATE_LEFTMOST_OF_SCREEN = 0x00;
        private const byte PLAYER_STATE_DIED = 0x06;
        private const byte PLAYER_STATE_NORMAL = 0x08;

        private const ushort PLAYER_SIZE_STATE_ADDRESS = 0x0754;
        private const byte PLAYER_SIZE_STATE_BIG = 0x00;

        private const ushort PLAYER_POWERUP_STATE_ADDRESS = 0x0756;
        private const byte PLAYER_POWERUP_STATE_FIERY = 0x02;

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

        private const string ASSETS_PATH = "Assets";
        private const string TIMELINE_SAVE_FILE_EXTENSION = ".mns";
        private const string TIMELINE_SAVE_THUMBNAIL_FILE_EXTENSION = ".png";
        private const string CHECKPOINT_FILENAME_SUFFIX = " (checkpoint)";

        private FormMain _formMain;

        private List<SMBLevel> _levels;
        private List<TimelineSave> _timelineSaves;

        private bool _wasResetForTimelineLoad = false;
        private string _timelineSaveToLoad;

        internal FanCut(FormMain formMain)
        {
            _formMain = formMain;

            NesEmu.EMUHardReseted += onCoreHardReseted;

            _levels = new List<SMBLevel>();

            _levels.Add(new SMBLevel() { Name = "World 1-1", WorldNumber = 0, LevelDisplayNumber = 0, LevelNumber = 0, AreaLoadedValue = 0x25, CheckpointScreenNumber = 5 });
            _levels.Add(new SMBLevel() { Name = "World 1-2", WorldNumber = 0, LevelDisplayNumber = 1, LevelNumber = 2, AreaLoadedValue = 0x40, CheckpointScreenNumber = 6 });
            _levels.Add(new SMBLevel() { Name = "World 1-3", WorldNumber = 0, LevelDisplayNumber = 2, LevelNumber = 3, AreaLoadedValue = 0x26, CheckpointScreenNumber = 4 });
            _levels.Add(new SMBLevel() { Name = "World 1-4", WorldNumber = 0, LevelDisplayNumber = 3, LevelNumber = 4, AreaLoadedValue = 0x60, CheckpointScreenNumber = 0 });

            _levels.Add(new SMBLevel() { Name = "World 2-1", WorldNumber = 1, LevelDisplayNumber = 0, LevelNumber = 0, AreaLoadedValue = 0x28, CheckpointScreenNumber = 6 });
            _levels.Add(new SMBLevel() { Name = "World 2-2", WorldNumber = 1, LevelDisplayNumber = 1, LevelNumber = 2, AreaLoadedValue = 0x01, CheckpointScreenNumber = 5 });
            _levels.Add(new SMBLevel() { Name = "World 2-3", WorldNumber = 1, LevelDisplayNumber = 2, LevelNumber = 3, AreaLoadedValue = 0x27, CheckpointScreenNumber = 7 });
            _levels.Add(new SMBLevel() { Name = "World 2-4", WorldNumber = 1, LevelDisplayNumber = 3, LevelNumber = 4, AreaLoadedValue = 0x62, CheckpointScreenNumber = 0 });

            _levels.Add(new SMBLevel() { Name = "World 3-1", WorldNumber = 2, LevelDisplayNumber = 0, LevelNumber = 0, AreaLoadedValue = 0x24, CheckpointScreenNumber = 6 });
            _levels.Add(new SMBLevel() { Name = "World 3-2", WorldNumber = 2, LevelDisplayNumber = 1, LevelNumber = 1, AreaLoadedValue = 0x35, CheckpointScreenNumber = 6 });
            _levels.Add(new SMBLevel() { Name = "World 3-3", WorldNumber = 2, LevelDisplayNumber = 2, LevelNumber = 2, AreaLoadedValue = 0x20, CheckpointScreenNumber = 4 });
            _levels.Add(new SMBLevel() { Name = "World 3-4", WorldNumber = 2, LevelDisplayNumber = 3, LevelNumber = 3, AreaLoadedValue = 0x63, CheckpointScreenNumber = 0 });

            _levels.Add(new SMBLevel() { Name = "World 4-1", WorldNumber = 3, LevelDisplayNumber = 0, LevelNumber = 0, AreaLoadedValue = 0x22, CheckpointScreenNumber = 6 });
            _levels.Add(new SMBLevel() { Name = "World 4-2", WorldNumber = 3, LevelDisplayNumber = 1, LevelNumber = 2, AreaLoadedValue = 0x41, CheckpointScreenNumber = 6 });
            _levels.Add(new SMBLevel() { Name = "World 4-3", WorldNumber = 3, LevelDisplayNumber = 2, LevelNumber = 3, AreaLoadedValue = 0x2C, CheckpointScreenNumber = 4 });
            _levels.Add(new SMBLevel() { Name = "World 4-4", WorldNumber = 3, LevelDisplayNumber = 3, LevelNumber = 4, AreaLoadedValue = 0x61, CheckpointScreenNumber = 0 });

            _levels.Add(new SMBLevel() { Name = "World 5-1", WorldNumber = 4, LevelDisplayNumber = 0, LevelNumber = 0, AreaLoadedValue = 0x2A, CheckpointScreenNumber = 6 });
            _levels.Add(new SMBLevel() { Name = "World 5-2", WorldNumber = 4, LevelDisplayNumber = 1, LevelNumber = 1, AreaLoadedValue = 0x31, CheckpointScreenNumber = 6 });
            _levels.Add(new SMBLevel() { Name = "World 5-3", WorldNumber = 4, LevelDisplayNumber = 2, LevelNumber = 2, AreaLoadedValue = 0x26, CheckpointScreenNumber = 4 });
            _levels.Add(new SMBLevel() { Name = "World 5-4", WorldNumber = 4, LevelDisplayNumber = 3, LevelNumber = 3, AreaLoadedValue = 0x62, CheckpointScreenNumber = 0 });

            _levels.Add(new SMBLevel() { Name = "World 6-1", WorldNumber = 5, LevelDisplayNumber = 0, LevelNumber = 0, AreaLoadedValue = 0x2E, CheckpointScreenNumber = 6 });
            _levels.Add(new SMBLevel() { Name = "World 6-2", WorldNumber = 5, LevelDisplayNumber = 1, LevelNumber = 1, AreaLoadedValue = 0x23, CheckpointScreenNumber = 6 });
            _levels.Add(new SMBLevel() { Name = "World 6-3", WorldNumber = 5, LevelDisplayNumber = 2, LevelNumber = 2, AreaLoadedValue = 0x2D, CheckpointScreenNumber = 6 });
            _levels.Add(new SMBLevel() { Name = "World 6-4", WorldNumber = 5, LevelDisplayNumber = 3, LevelNumber = 3, AreaLoadedValue = 0x60, CheckpointScreenNumber = 0 });

            _levels.Add(new SMBLevel() { Name = "World 7-1", WorldNumber = 6, LevelDisplayNumber = 0, LevelNumber = 0, AreaLoadedValue = 0x33, CheckpointScreenNumber = 6 });
            _levels.Add(new SMBLevel() { Name = "World 7-2", WorldNumber = 6, LevelDisplayNumber = 1, LevelNumber = 2, AreaLoadedValue = 0x01, CheckpointScreenNumber = 5 });
            _levels.Add(new SMBLevel() { Name = "World 7-3", WorldNumber = 6, LevelDisplayNumber = 2, LevelNumber = 3, AreaLoadedValue = 0x27, CheckpointScreenNumber = 7 });
            _levels.Add(new SMBLevel() { Name = "World 7-4", WorldNumber = 6, LevelDisplayNumber = 3, LevelNumber = 4, AreaLoadedValue = 0x64, CheckpointScreenNumber = 0 });

            _levels.Add(new SMBLevel() { Name = "World 8-1", WorldNumber = 7, LevelDisplayNumber = 0, LevelNumber = 0, AreaLoadedValue = 0x30, CheckpointScreenNumber = 12 }); // no checkpoint in the actual game
            _levels.Add(new SMBLevel() { Name = "World 8-2", WorldNumber = 7, LevelDisplayNumber = 1, LevelNumber = 1, AreaLoadedValue = 0x32, CheckpointScreenNumber = 6 }); // no checkpoint in the actual game
            _levels.Add(new SMBLevel() { Name = "World 8-3", WorldNumber = 7, LevelDisplayNumber = 2, LevelNumber = 2, AreaLoadedValue = 0x21, CheckpointScreenNumber = 6 }); // no checkpoint in the actual game
            _levels.Add(new SMBLevel() { Name = "World 8-4", WorldNumber = 7, LevelDisplayNumber = 3, LevelNumber = 3, AreaLoadedValue = 0x65, CheckpointScreenNumber = 0 });
            
            // this will override all the current saves, only do this when regenerating saves
            //createTimelineSaves();

            _timelineSaves = new List<TimelineSave>();
            int index = 0;
            foreach (SMBLevel level in _levels)
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

            createTimelineControls();

            NesEmu.WriteChangeTriggers = new Dictionary<ushort, Action<byte, byte>>();
            NesEmu.WriteChangeTriggers.Add(PLAYER_STATE_ADDRESS, onPlayerStateChanging);
            NesEmu.WriteChangeTriggers.Add(WORLD_NUMBER_ADDRESS, onWorldNumberChanging);
            NesEmu.WriteChangeTriggers.Add(LEVEL_NUMBER_ADDRESS, onLevelNumberChanging);

            _formMain.OpenRom(@"Assets\Super Mario Bros..nes");
        }

        #region Initialization Routines

        private void createTimelineSaves()
        {
            foreach (SMBLevel level in _levels)
            {
                FileStream saveStateStream = new FileStream(@"Assets\base.mns", FileMode.Open, FileAccess.Read);
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

                saveStateStream = new FileStream(@"Assets\" + level.Name + ".mns", FileMode.Create, FileAccess.Write);
                saveStateStream.Write(saveState, 0, saveState.Length);
                saveStateStream.Close();

                if (level.CheckpointScreenNumber != 0)
                {
                    saveState[WRAM_OFFSET + STARTING_LEVEL_SCREEN_ADDRESS] = level.CheckpointScreenNumber;

                    saveStateStream = new FileStream(@"Assets\" + level.Name + " (checkpoint).mns", FileMode.Create, FileAccess.Write);
                    saveStateStream.Write(saveState, 0, saveState.Length);
                    saveStateStream.Close();
                }
            }
        }

        private void createTimelineControls()
        {
            const int MARGIN = 10;
            const int THUMBNAIL_WIDTH = 256;
            const int THUMBNAIL_HEIGHT = 224;
            const int TITLE_WIDTH = 100;
            const int TITLE_HEIGHT = 36;

            foreach (TimelineSave timelineSave in _timelineSaves)
            {
                PictureBox timelineSaveThumbnail = new PictureBox();
                Label timelineSaveTitle = new Label();

                timelineSaveThumbnail.Size = new Size(THUMBNAIL_WIDTH, THUMBNAIL_HEIGHT);
                timelineSaveThumbnail.Location = new Point(MARGIN, (((MARGIN + THUMBNAIL_HEIGHT) * timelineSave.ID) + MARGIN));
                timelineSaveThumbnail.Load(Path.Combine(ASSETS_PATH, timelineSave.ThumbnailFilename));
                timelineSaveThumbnail.Tag = timelineSave.ID;
                timelineSaveThumbnail.Click += new EventHandler(onTimelineSaveThumbnailClick);

                _formMain.timelinePanel.Controls.Add(timelineSaveThumbnail);
                _formMain.timelinePanel.Paint += onTimelinePanelPaint;

                timelineSaveTitle.Size = new Size(TITLE_WIDTH, TITLE_HEIGHT);
                timelineSaveTitle.Text = timelineSave.Name;
                timelineSaveTitle.Location = new Point((MARGIN + THUMBNAIL_WIDTH + MARGIN), (((MARGIN + THUMBNAIL_HEIGHT) * timelineSave.ID) + MARGIN + MARGIN));

                _formMain.timelinePanel.Controls.Add(timelineSaveTitle);
            }
        }

        #endregion

        #region Change Trigger Handlers

        private void onPlayerStateChanging(byte previousValue, byte newValue)
        {
            byte playerLives = NesEmu.WRAM[PLAYER_LIVES_ADDRESS & 0x7FF];

            if ((previousValue == PLAYER_STATE_NORMAL) && (newValue == PLAYER_STATE_LEFTMOST_OF_SCREEN) && (playerLives == 2))
            {
                writeLogMessage("Detected player start, loading first timeline save.");
                resetThenLoadTimelineSave(_timelineSaves.First().Filename);
            }

            if ((previousValue == PLAYER_STATE_DIED) && (newValue == PLAYER_STATE_LEFTMOST_OF_SCREEN))
            {
                byte worldNumber = NesEmu.WRAM[WORLD_NUMBER_ADDRESS & 0x7FF];
                byte levelNumber = NesEmu.WRAM[LEVEL_NUMBER_ADDRESS & 0x7FF];
                byte levelScreenNumber = NesEmu.WRAM[LEVEL_SCREEN_ADDRESS & 0x7FF];

                SMBLevel currentLevel = (from l in _levels where (l.WorldNumber == worldNumber) && (l.LevelNumber == levelNumber) select l).FirstOrDefault();
                if (currentLevel != null)
                {
                    bool isPastCheckpoint = (currentLevel.CheckpointScreenNumber != 0) && (levelScreenNumber >= currentLevel.CheckpointScreenNumber);

                    string timelineSaveName = currentLevel.Name + (isPastCheckpoint ? CHECKPOINT_FILENAME_SUFFIX : string.Empty);
                    string timelineSaveFilename = (from ts in _timelineSaves where ts.Name == timelineSaveName select ts.Filename).FirstOrDefault();

                    if (!string.IsNullOrWhiteSpace(timelineSaveFilename))
                    {
                        writeLogMessage(string.Format("Detected player death on {0}{1}.", currentLevel.Name, (isPastCheckpoint ? " (past checkpoint)" : string.Empty)));
                        resetThenLoadTimelineSave(timelineSaveFilename);
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
            SMBLevel currentLevel = (from l in _levels where (l.WorldNumber == worldNumber) && (l.LevelNumber == levelNumber) select l).FirstOrDefault();
            if (currentLevel != null)
                if (_levels.IndexOf(currentLevel) != 0)
                {
                    SMBLevel previousLevel = _levels[_levels.IndexOf(currentLevel) - 1];

                    writeLogMessage(string.Format("Detected completion of {0}", previousLevel.Name));

                    resetThenLoadTimelineSave((from ts in _timelineSaves where ts.Name == currentLevel.Name select ts.Filename).First());
                }
        }

        #endregion

        #region Core Control Routines and Handlers

        private void resetThenLoadTimelineSave(string timelineSaveFilename)
        {
            _timelineSaveToLoad = Path.Combine(ASSETS_PATH, timelineSaveFilename);
            _wasResetForTimelineLoad = true;
            NesEmu.EMUHardReset();
        }

        private void onCoreHardReseted(object sender, EventArgs e)
        {
            if (_formMain.InvokeRequired)
                _formMain.Invoke(new Action(() => onCoreHardReseted(sender, e)));
            else
            {
                if (_wasResetForTimelineLoad)
                {
                    _wasResetForTimelineLoad = false;
                    NesEmu.LoadStateAs(_timelineSaveToLoad);
                    writeLogMessage(string.Format("Loaded timeline save: {0}", Path.GetFileNameWithoutExtension(_timelineSaveToLoad)));
                }
            }
        }

        #endregion

        #region UI Routines and Handlers

        private void onTimelineSaveThumbnailClick(object sender, EventArgs e)
        {
            TimelineSave timelineSave = (from ts in _timelineSaves where ts.ID == (int)((Control)sender).Tag select ts).FirstOrDefault();

            if (timelineSave != null)
                resetThenLoadTimelineSave(timelineSave.Filename);
        }

        private void onTimelinePanelPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Control temp = ((Control)sender).Controls[0];

            Pen myPen = new Pen(Color.Red);
            myPen.Width = 3;

            g.DrawLine(myPen, (temp.Location.X - 3), (temp.Location.Y - 3), (temp.Location.X + temp.Width + 3), (temp.Location.Y - 3));
            g.DrawLine(myPen, (temp.Location.X - 3), (temp.Location.Y - 3), (temp.Location.X - 3), (temp.Location.Y + temp.Height + 3));
            g.DrawLine(myPen, (temp.Location.X + temp.Width + 3), (temp.Location.Y - 3), (temp.Location.X + temp.Width + 3), (temp.Location.Y + temp.Height + 3));
            g.DrawLine(myPen, (temp.Location.X - 3), (temp.Location.Y + temp.Height + 3), (temp.Location.X + temp.Width + 3), (temp.Location.Y + temp.Height + 3));
        }

        private void writeLogMessage(string message)
        {
            if (_formMain.InvokeRequired)
                _formMain.Invoke(new Action(() => writeLogMessage(message)));
            else
            {
                _formMain.logListBox.Items.Add(message);
                _formMain.logListBox.SelectedIndex = _formMain.logListBox.Items.Count - 1;
                _formMain.logListBox.SelectedIndex = -1;
            }
        }

        #endregion
    }
}
