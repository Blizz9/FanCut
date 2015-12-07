using MyNes.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace MyNes
{
    internal class FanCutCommon
    {
        private FormMain _formMain;
        private List<TimelineSave> _timelineSaves;
        private string _assetsPath;

        private ListBox _logListBox;

        private bool _wasResetForTimelineLoad = false;
        private string _timelineSaveToLoad;

        internal FanCutCommon(FormMain formMain, string assetsPath, List<TimelineSave> timelineSaves, string sha1Hash)
        {
            _formMain = formMain;
            _assetsPath = assetsPath;
            _timelineSaves = timelineSaves;

            NesEmu.EMUHardReseted += onCoreHardReseted;

            layoutFormMain();

            OpenFileDialog romOpenFileDialog = new OpenFileDialog();
            romOpenFileDialog.Title = Program.ResourceManager.GetString("Title_OpenRom");
            romOpenFileDialog.Filter = "NES ROM (*.nes) | *.nes";

            bool isROMSelected = false;

            while (!isROMSelected)
            {
                if (romOpenFileDialog.ShowDialog(formMain) == DialogResult.OK)
                {
                    StringBuilder romHashString;
                    using (FileStream romFileStream = new FileStream(romOpenFileDialog.FileName, FileMode.Open))
                    {
                        using (BufferedStream romBufferedStream = new BufferedStream(romFileStream))
                        {
                            using (SHA1Managed romSHA1 = new SHA1Managed())
                            {
                                byte[] romHash = romSHA1.ComputeHash(romBufferedStream);
                                romHashString = new StringBuilder(romHash.Length * 2);
                                foreach (byte b in romHash)
                                    romHashString.AppendFormat("{0:X2}", b);
                            }
                        }
                    }

                    if (romHashString.ToString() == sha1Hash)
                    {
                        isROMSelected = true;
                        formMain.OpenRom(romOpenFileDialog.FileName);
                    }
                    else
                        MessageBox.Show("You must load the proper ROM for this FanCut.", "ROM Mismatch");
                }
                else
                {
                    isROMSelected = true;
                    Application.Exit();
                }
            }
        }

        #region Initialization Routines

        private void layoutFormMain()
        {
            const int MARGIN = 10;
            const int THUMBNAIL_WIDTH = 256;
            const int THUMBNAIL_HEIGHT = 224;

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

            if (_timelineSaves != null)
                foreach (TimelineSave timelineSave in _timelineSaves)
                {
                    PictureBox timelineSaveThumbnail = new PictureBox();
                    Label timelineSaveTitle = new Label();

                    timelineSaveThumbnail.Size = new Size(THUMBNAIL_WIDTH, THUMBNAIL_HEIGHT);
                    timelineSaveThumbnail.Location = new Point(MARGIN, (((MARGIN + THUMBNAIL_HEIGHT) * timelineSave.ID) + MARGIN));
                    timelineSaveThumbnail.Load(Path.Combine(_assetsPath, timelineSave.ThumbnailFilename));
                    timelineSaveThumbnail.Tag = timelineSave.ID;
                    timelineSaveThumbnail.Click += new EventHandler(onTimelineSaveThumbnailClick);

                    timelinePanel.Controls.Add(timelineSaveThumbnail);

                    timelineSaveTitle.Size = new Size(100, 36);
                    timelineSaveTitle.Text = timelineSave.Name;
                    timelineSaveTitle.Location = new Point((MARGIN + THUMBNAIL_WIDTH + MARGIN), (((MARGIN + THUMBNAIL_HEIGHT) * timelineSave.ID) + MARGIN + MARGIN));

                    timelinePanel.Controls.Add(timelineSaveTitle);
                }
        }

        #endregion

        #region Core Control Routines and Handlers

        internal void ResetThenLoadTimelineSave(string timelineSaveFilename)
        {
            _timelineSaveToLoad = Path.Combine(_assetsPath, timelineSaveFilename);
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
                    WriteLogMessage(string.Format("Loaded timeline save: {0}", Path.GetFileNameWithoutExtension(_timelineSaveToLoad)));
                }
            }
        }

        #endregion

        #region UI Routines and Handlers

        private void onTimelineSaveThumbnailClick(object sender, EventArgs e)
        {
            TimelineSave timelineSave = (from ts in _timelineSaves where ts.ID == (int)((Control)sender).Tag select ts).FirstOrDefault();

            if (timelineSave != null)
                ResetThenLoadTimelineSave(timelineSave.Filename);
        }

        private void onTimelinePanelPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (((Control)sender).Controls.Count > 0)
            {
                Control temp = ((Control)sender).Controls[0];

                Pen myPen = new Pen(Color.Red);
                myPen.Width = 3;

                g.DrawLine(myPen, (temp.Location.X - 3), (temp.Location.Y - 3), (temp.Location.X + temp.Width + 3), (temp.Location.Y - 3));
                g.DrawLine(myPen, (temp.Location.X - 3), (temp.Location.Y - 3), (temp.Location.X - 3), (temp.Location.Y + temp.Height + 3));
                g.DrawLine(myPen, (temp.Location.X + temp.Width + 3), (temp.Location.Y - 3), (temp.Location.X + temp.Width + 3), (temp.Location.Y + temp.Height + 3));
                g.DrawLine(myPen, (temp.Location.X - 3), (temp.Location.Y + temp.Height + 3), (temp.Location.X + temp.Width + 3), (temp.Location.Y + temp.Height + 3));
            }
        }

        internal void WriteLogMessage(string message)
        {
            if (_formMain.InvokeRequired)
                _formMain.Invoke(new Action(() => WriteLogMessage(message)));
            else
            {
                _logListBox.Items.Add(message);
                _logListBox.SelectedIndex = _logListBox.Items.Count - 1;
                _logListBox.SelectedIndex = -1;

                Debug.WriteLine(message);
            }
        }

        #endregion
    }
}
