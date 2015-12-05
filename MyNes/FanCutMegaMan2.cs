using MyNes.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MyNes
{
    internal class FanCutMegaMan2
    {
        #region Memory Locations and Values

        private const ushort UNLOCKED_WEAPONS = 0x009A;
        private const ushort UNLOCKED_ITEMS = 0x009B;

        private const ushort PLAYER_HIT_POINTS = 0x06C0;

        #endregion

        private const string ASSETS_PATH = @"Assets\Mega Man 2";
        private const string TIMELINE_SAVE_FILE_EXTENSION = ".mns";
        private const string TIMELINE_SAVE_THUMBNAIL_FILE_EXTENSION = ".png";
        private const string CHECKPOINT_FILENAME_SUFFIX = " (checkpoint)";

        private FormMain _formMain;
        private ListBox _logListBox;

        private List<TimelineSave> _timelineSaves;

        private Dictionary<ushort, string> _changeTriggerAddresses;
        private Dictionary<ushort, string> _readTriggerAddresses;

        private bool _wasResetForTimelineLoad = false;
        private string _timelineSaveToLoad;

        internal FanCutMegaMan2(FormMain formMain)
        {
            _formMain = formMain;

            NesEmu.EMUHardReseted += onCoreHardReseted;

            layoutFormMain();

            _changeTriggerAddresses = new Dictionary<ushort, string>();
            _changeTriggerAddresses.Add(PLAYER_HIT_POINTS, "Player Hit Points");
            _changeTriggerAddresses.Add(0x0440, string.Empty);
            //_changeTriggerAddresses.Add(0x0460, string.Empty);

            _readTriggerAddresses = new Dictionary<ushort, string>();
            //_readTriggerAddresses.Add(0x0440, string.Empty);

            NesEmu.ChangeTriggers = new List<ushort>();
            NesEmu.ChangeTriggerHandler = onMemoryChanging;
            foreach (ushort changeTriggerAddress in _changeTriggerAddresses.Keys)
                NesEmu.ChangeTriggers.Add(changeTriggerAddress);

            NesEmu.ReadTriggers = new List<ushort>();
            NesEmu.ReadTriggerHandler = onMemoryRead;
            foreach (ushort readTriggerAddress in _readTriggerAddresses.Keys)
                NesEmu.ReadTriggers.Add(readTriggerAddress);

            _formMain.OpenRom(Path.Combine(ASSETS_PATH, "Mega Man 2.nes"));
            resetThenLoadTimelineSave("levelSelect.mns");
        }

        #region Initialization Routines

        private void layoutFormMain()
        {
            _formMain.Size = new Size(1048, 682);
            _formMain.FormBorderStyle = FormBorderStyle.FixedSingle;
            _formMain.MaximizeBox = false;

            _formMain.panel_surface.Dock = DockStyle.None;
            _formMain.panel_surface.Location = new Point(12, 36);
            _formMain.panel_surface.Size = new Size(512, 448);

            GroupBox timelineGroupBox = new GroupBox();
            timelineGroupBox.Location = new Point(536, 26);
            timelineGroupBox.Size = new Size(486, 608);
            timelineGroupBox.Font = new Font("Arial", 12);
            timelineGroupBox.Text = "Timeline";
            _formMain.Controls.Add(timelineGroupBox);

            Panel timelinePanel = new Panel();
            timelinePanel.Location = new Point(3, 22);
            timelinePanel.Size = new Size(494, 618);
            timelinePanel.Dock = DockStyle.Fill;
            timelinePanel.AutoScroll = true;
            timelinePanel.Paint += onTimelinePanelPaint;
            timelineGroupBox.Controls.Add(timelinePanel);

            GroupBox logGroupBox = new GroupBox();
            logGroupBox.Location = new Point(12, 490);
            logGroupBox.Size = new Size(513, 144);
            logGroupBox.Font = new Font("Arial", 12);
            logGroupBox.Text = "Log";
            _formMain.Controls.Add(logGroupBox);

            _logListBox = new ListBox();
            _logListBox.Location = new Point(6, 23);
            _logListBox.Size = new Size(501, 125);
            logGroupBox.Controls.Add(_logListBox);
        }

        #endregion

        #region Change Trigger Handlers

        private void onMemoryChanging(ushort address, byte previousValue, byte newValue)
        {
            string label = "0x" + address.ToString("X4");

            if (!string.IsNullOrWhiteSpace(_changeTriggerAddresses[address]))
                label = _changeTriggerAddresses[address];

            //if (address == PLAYER_HIT_POINTS && newValue == 28)
            //{
            //    NesEmu.WRAM[PLAYER_HIT_POINTS & 0x7FF] = 28;
            //    NesEmu.EmulationPaused = true;
            //    NesEmu.SaveStateAs(ASSETS_PATH + "\\Metal Man (boss).mns");
            //    NesEmu.EmulationPaused = false;
            //}

            writeLogMessage(string.Format("{0}: {1} -> {2}", label, previousValue, newValue));
        }

        private void onMemoryRead(ushort address, byte previousValue)
        {
            string label = "0x" + address.ToString("X4");

            if (!string.IsNullOrWhiteSpace(_readTriggerAddresses[address]))
                label = _readTriggerAddresses[address];

            writeLogMessage(string.Format("{0}: {1}", label, previousValue));
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
                _logListBox.Items.Add(message);
                _logListBox.SelectedIndex = _logListBox.Items.Count - 1;
                _logListBox.SelectedIndex = -1;
            }
        }

        #endregion
    }
}
