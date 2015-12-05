using MyNes.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MyNes
{
    public partial class FormMain
    {
        // TODO: Move all new source into separate class
        // TODO: Add pictures for save states that don't have them
        // TODO: Check finishing the game and if anything fucks up
        // TODO: Implement save state injection here first? Not really required.
        // TODO: Add timeline highlight
        // TODO: FanCut?

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

        private const ushort WORLD_NUMBER_ADDRESS = 0x075F;
        private const ushort LEVEL_DISPLAY_NUMBER_ADDRESS = 0x075C;
        private const ushort LEVEL_NUMBER_ADDRESS = 0x0760;

        private const ushort LEVEL_SCREEN_ADDRESS = 0x071A;
        private const ushort STARTING_LEVEL_SCREEN_ADDRESS = 0x075B;

        private const ushort AREA_LOADED_ADDRESS = 0x0750;

        private const ushort COINS_DIGITS_ADDRESS = 0x07ED;
        private const ushort TIME_DIGITS_ADDRESS = 0x07F8;

        private const ushort LEVEL_RESTART_TRIGGER_ADDRESS = 0x0772;
        private const byte LEVEL_RESTART_TRIGGER_VALUE = 0x0;

        private List<SMBLevel> _levels;
        private List<TimelineSave> _timelineSaves;

        private bool _hardResettingPriorToLoadState = false;
        private string _loadStateFilename;

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //NesEmu.Write(PLAYER_SIZE_STATE_ADDRESS, PLAYER_SIZE_STATE_BIG);
            //NesEmu.Write(PLAYER_POWERUP_STATE_ADDRESS, PLAYER_POWERUP_STATE_FIERY);

            //byte[] areaLoadedValueBytes = BitConverter.GetBytes(AREA_LOADED_VALUE);
            //NesEmu.Write(AREA_LOADED_ADDRESS, areaLoadedValueBytes[1]);
            //NesEmu.Write((AREA_LOADED_ADDRESS + 1), areaLoadedValueBytes[0]);

            //NesEmu.Write(PLAYER_LIVES_ADDRESS, 4);
            //NesEmu.Write(PLAYER_COINS_ADDRESS, 90);
            //NesEmu.Write(WORLD_NUMBER_ADDRESS, 3);
            //NesEmu.Write(LEVEL_NUMBER_ADDRESS, 0);

            //NesEmu.Write(COINS_DIGITS_ADDRESS, 9);
            //NesEmu.Write((COINS_DIGITS_ADDRESS + 1), 0);

            //NesEmu.Write(TIME_DIGITS_ADDRESS, 4);
            //NesEmu.Write((TIME_DIGITS_ADDRESS + 1), 0);
            //NesEmu.Write((TIME_DIGITS_ADDRESS + 2), 1);

            //NesEmu.Write(LEVEL_RESTART_TRIGGER_ADDRESS, LEVEL_RESTART_TRIGGER_VALUE);

            NesEmu.Write(PLAYER_SIZE_STATE_ADDRESS, PLAYER_SIZE_STATE_BIG);
            NesEmu.Write(PLAYER_POWERUP_STATE_ADDRESS, PLAYER_POWERUP_STATE_FIERY);
        }

        private void test2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NesEmu.Write(PLAYER_LIVES_ADDRESS, 5);
            NesEmu.Write(PLAYER_COINS_ADDRESS, 90);

            NesEmu.Write(COINS_DIGITS_ADDRESS, 9);
            NesEmu.Write((COINS_DIGITS_ADDRESS + 1), 0);

            NesEmu.Write(TIME_DIGITS_ADDRESS, 4);
            NesEmu.Write((TIME_DIGITS_ADDRESS + 1), 0);
            NesEmu.Write((TIME_DIGITS_ADDRESS + 2), 1);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
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

            //foreach (SMBLevel level in levels)
            //{
            //    FileStream saveStateStream = new FileStream(@"Assets\base.mns", FileMode.Open, FileAccess.Read);
            //    byte[] saveState = new byte[saveStateStream.Length];
            //    saveStateStream.Read(saveState, 0, saveState.Length);
            //    saveStateStream.Close();

            //    const ushort WRAM_OFFSET = 0x510C;

            //    saveState[WRAM_OFFSET + PLAYER_SIZE_STATE_ADDRESS] = PLAYER_SIZE_STATE_BIG;
            //    saveState[WRAM_OFFSET + PLAYER_POWERUP_STATE_ADDRESS] = PLAYER_POWERUP_STATE_FIERY;

            //    saveState[WRAM_OFFSET + PLAYER_LIVES_ADDRESS] = 4;
            //    saveState[WRAM_OFFSET + PLAYER_COINS_ADDRESS] = 90;
            //    saveState[WRAM_OFFSET + COINS_DIGITS_ADDRESS] = 9;
            //    saveState[WRAM_OFFSET + COINS_DIGITS_ADDRESS + 1] = 0;

            //    saveState[WRAM_OFFSET + WORLD_NUMBER_ADDRESS] = level.WorldNumber;
            //    saveState[WRAM_OFFSET + LEVEL_DISPLAY_NUMBER_ADDRESS] = level.LevelDisplayNumber;
            //    saveState[WRAM_OFFSET + LEVEL_NUMBER_ADDRESS] = level.LevelNumber;
            //    saveState[WRAM_OFFSET + AREA_LOADED_ADDRESS] = level.AreaLoadedValue;

            //    saveStateStream = new FileStream(@"Assets\" + level.Name + ".mns", FileMode.Create, FileAccess.Write);
            //    saveStateStream.Write(saveState, 0, saveState.Length);
            //    saveStateStream.Close();
            //    //File.Copy(@"Assets\blank.png", @"Assets\" + level.Name + ".png");

            //    if (level.CheckpointScreenNumber != 0)
            //    {
            //        saveState[WRAM_OFFSET + STARTING_LEVEL_SCREEN_ADDRESS] = level.CheckpointScreenNumber;

            //        saveStateStream = new FileStream(@"Assets\" + level.Name + " (checkpoint).mns", FileMode.Create, FileAccess.Write);
            //        saveStateStream.Write(saveState, 0, saveState.Length);
            //        saveStateStream.Close();
            //        //File.Copy(@"Assets\blank.png", @"Assets\" + level.Name + " (checkpoint).png");
            //    }
            //}

            _timelineSaves = new List<TimelineSave>();
            int index = 0;
            foreach (SMBLevel level in _levels)
            {
                TimelineSave timelineSave = new TimelineSave()
                {
                    ID = index,
                    Name = level.Name,
                    SaveStateName = level.Name + ".mns",
                    ThumbnailName = level.Name + ".png"
                };

                _timelineSaves.Add(timelineSave);
                index++;

                if (level.CheckpointScreenNumber != 0)
                {
                    timelineSave = new TimelineSave()
                    {
                        ID = index,
                        Name = level.Name + " (checkpoint)",
                        SaveStateName = level.Name + " (checkpoint).mns",
                        ThumbnailName = level.Name + " (checkpoint).png"
                    };

                    _timelineSaves.Add(timelineSave);
                    index++;
                }
            }

            const int MARGIN = 10;
            const int SCREENSHOT_WIDTH = 256;
            const int SCREENSHOT_HEIGHT = 224;

            foreach (TimelineSave timelineSave in _timelineSaves)
            {
                PictureBox saveStateImage = new PictureBox();
                Label saveStateName = new Label();

                saveStateImage.Size = new Size(SCREENSHOT_WIDTH, SCREENSHOT_HEIGHT);
                saveStateImage.Location = new Point(MARGIN, (((MARGIN + SCREENSHOT_HEIGHT) * timelineSave.ID) + MARGIN));
                saveStateImage.Load(@"Assets\" + timelineSave.ThumbnailName);
                saveStateImage.Tag = timelineSave.ID;
                saveStateImage.Click += new EventHandler(saveState_Click);
                saveStatesPanel.Controls.Add(saveStateImage);
                saveStatesPanel.Paint += saveStatesPanel_Paint;

                saveStateName.Size = new Size(100, 36);
                saveStateName.Text = timelineSave.Name;
                saveStateName.Location = new Point((MARGIN + SCREENSHOT_WIDTH + MARGIN), (((MARGIN + SCREENSHOT_HEIGHT) * timelineSave.ID) + MARGIN + MARGIN));
                saveStatesPanel.Controls.Add(saveStateName);
            }

            NesEmu.WriteChangeTriggers = new Dictionary<ushort, Action<byte, byte>>();
            NesEmu.WriteChangeTriggers.Add(PLAYER_STATE_ADDRESS, handlePlayerStateChangeTrigger);

            NesEmu.WriteChangeTriggers.Add(LEVEL_SCREEN_ADDRESS, handleLevelScreenChangeTrigger);
            NesEmu.WriteChangeTriggers.Add(WORLD_NUMBER_ADDRESS, handleWorldNumberChangeTrigger);
            NesEmu.WriteChangeTriggers.Add(LEVEL_NUMBER_ADDRESS, handleLevelNumberChangeTrigger);

            OpenRom(@"Assets\Super Mario Bros..nes");
        }

        private void handlePlayerStateChangeTrigger(byte previousValue, byte newValue)
        {
            if ((previousValue == PLAYER_STATE_NORMAL) && (newValue == PLAYER_STATE_LEFTMOST_OF_SCREEN))
                if (NesEmu.WRAM[PLAYER_LIVES_ADDRESS] == 2)
                {
                    writeLogMessage("# Detected player start, loading initial save state");

                    NesEmu.EmulationPaused = true;
                    NesEmu.LoadStateAs(@"Assets\" + _timelineSaves.First().SaveStateName);
                    NesEmu.EmulationPaused = false;
                }

            if ((previousValue == PLAYER_STATE_DIED) && (newValue == PLAYER_STATE_LEFTMOST_OF_SCREEN))
            {
                //NesEmu.EmulationPaused = true;
                //NesEmu.SaveStateAs(@"Assets\test2.mns");
                //NesEmu.EmulationPaused = false;

                byte worldNumber = NesEmu.WRAM[WORLD_NUMBER_ADDRESS & 0x7FF];
                byte levelNumber = NesEmu.WRAM[LEVEL_NUMBER_ADDRESS & 0x7FF];
                byte levelScreenNumber = NesEmu.WRAM[LEVEL_SCREEN_ADDRESS & 0x7FF];

                SMBLevel currentLevel = (from l in _levels where (l.WorldNumber == worldNumber) && (l.LevelNumber == levelNumber) select l).FirstOrDefault();
                if (currentLevel != null)
                {
                    bool pastCheckpoint = (currentLevel.CheckpointScreenNumber != 0) && (levelScreenNumber >= currentLevel.CheckpointScreenNumber);

                    writeLogMessage(string.Format("# Detected player death on {0}", currentLevel.Name) + (pastCheckpoint ? " (past checkpoint)" : string.Empty));

                    string saveStateName = @"Assets\" + currentLevel.Name;

                    if (pastCheckpoint)
                        saveStateName += " (checkpoint)";

                    //NesEmu.EmulationPaused = true;
                    //NesEmu.LoadStateAs(saveStateName + ".mns");
                    //NesEmu.EmulationPaused = false;

                    _loadStateFilename = saveStateName + ".mns";
                    _hardResettingPriorToLoadState = true;
                    NesEmu.EMUHardReset();
                }
            }
        }

        private void handleLevelScreenChangeTrigger(byte previousValue, byte newValue)
        {
            //Console.WriteLine("{0} | Memory Write Change | Level Screen[0x{1}]: 0x{2}[{3}] -> 0x{4}[{5}]", DateTime.Now.Ticks, LEVEL_SCREEN_ADDRESS.ToString("X4"), previousValue.ToString("X2"), previousValue, newValue.ToString("X2"), newValue);
        }

        private void handleWorldNumberChangeTrigger(byte previousValue, byte newValue)
        {
            byte worldNumber = newValue;
            byte levelNumber = NesEmu.WRAM[LEVEL_NUMBER_ADDRESS & 0x7FF];

            SMBLevel currentLevel = (from l in _levels where (l.WorldNumber == worldNumber) && (l.LevelNumber == levelNumber) select l).FirstOrDefault();
            if (currentLevel != null)
            {
                SMBLevel previousLevel = _levels[_levels.IndexOf(currentLevel) - 1];
                writeLogMessage(string.Format("# Detected level completion on {0}", previousLevel.Name));

                string saveStateName = @"Assets\" + currentLevel.Name;

                _loadStateFilename = saveStateName + ".mns";
                _hardResettingPriorToLoadState = true;
                NesEmu.EMUHardReset();
            }
        }

        private void handleLevelNumberChangeTrigger(byte previousValue, byte newValue)
        {
            if (previousValue < newValue)
            {
                byte worldNumber = NesEmu.WRAM[WORLD_NUMBER_ADDRESS & 0x7FF];
                byte levelNumber = newValue;

                SMBLevel currentLevel = (from l in _levels where (l.WorldNumber == worldNumber) && (l.LevelNumber == levelNumber) select l).FirstOrDefault();
                if (currentLevel != null)
                {
                    SMBLevel previousLevel = _levels[_levels.IndexOf(currentLevel) - 1];
                    writeLogMessage(string.Format("# Detected level completion on {0}", previousLevel.Name));

                    string saveStateName = @"Assets\" + currentLevel.Name;

                    _loadStateFilename = saveStateName + ".mns";
                    _hardResettingPriorToLoadState = true;
                    NesEmu.EMUHardReset();
                }
            }
        }

        private void saveState_Click(object sender, EventArgs e)
        {
            TimelineSave timelineSave = (from ts in _timelineSaves where ts.ID == (int)((Control)sender).Tag select ts).FirstOrDefault();

            if (timelineSave != null)
            {
                //NesEmu.EmulationPaused = true;
                //NesEmu.LoadStateAs(@"Assets\" + timelineSave.SaveStateName);
                //NesEmu.EmulationPaused = false;

                //NesEmu.EmulationPaused = true;
                //MessageBox.Show("Would you like to move to the next timeline item?", "Suggestion", MessageBoxButtons.YesNo);
                //NesEmu.EmulationPaused = false;

                _loadStateFilename = @"Assets\" + timelineSave.SaveStateName;
                _hardResettingPriorToLoadState = true;
                NesEmu.EMUHardReset();
            }
        }

        private void writeLogMessage(string message)
        {
            if (InvokeRequired)
                Invoke(new Action(() => writeLogMessage(message)));
            else
            {
                logListBox.Items.Add(message);
                logListBox.SelectedIndex = logListBox.Items.Count - 1;
                logListBox.SelectedIndex = -1;
            }
        }

        private void NesEmu_EMUHardReseted(object sender, EventArgs e)
        {
            if (InvokeRequired)
                Invoke(new Action(() => NesEmu_EMUHardReseted(sender, e)));
            else
            {
                if (_hardResettingPriorToLoadState)
                {
                    _hardResettingPriorToLoadState = false;
                    NesEmu.LoadStateAs(_loadStateFilename);
                    writeLogMessage(string.Format("Loaded Save State: {0}", Path.GetFileNameWithoutExtension(_loadStateFilename)));
                }
            }
        }

        private void saveStatesPanel_Paint(object sender, PaintEventArgs e)
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Control temp = saveStatesPanel.Controls[0];
            Point locationOnForm = temp.FindForm().PointToClient(temp.Parent.PointToScreen(temp.Location));

            Graphics g = e.Graphics;

            Pen myPen = new Pen(Color.Red);
            myPen.Width = 3;

            //g.DrawLine(myPen, 30, 30, 45, 65);
            //g.DrawLine(myPen, 1, 1, 45, 65);
            g.DrawLine(myPen, locationOnForm.X - 3, locationOnForm.Y - 3, (locationOnForm.X + temp.Width + 3), locationOnForm.Y - 3);
        }

        //Console.WriteLine("{0} | Memory Write Change | 0x{1}[{2}]: 0x{3}[{4}] -> 0x{5}[{6}]", DateTime.Now.Ticks, triggerAddress.ToString("X4"), triggerAddress, previousValue.ToString("X2"), previousValue, newValue.ToString("X2"), newValue);
        //Console.WriteLine("{0} | Memory Read | 0x{1}[{2}]: 0x{3}[{4}]", DateTime.Now.Ticks, address.ToString("X4"), address, value.ToString("X2"), value);
    }
}
