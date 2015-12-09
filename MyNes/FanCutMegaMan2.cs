using MyNes.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MyNes
{
    internal class FanCutMegaMan2
    {
        #region Memory Locations and Values

        private const ushort UNLOCKED_WEAPONS = 0x009A;
        private const ushort UNLOCKED_ITEMS = 0x009B;

        private const ushort PLAYER_HIT_POINTS = 0x06C0;

        #endregion

        private const string GAME_NAME = "Mega Man 2";
        private const string ASSETS_PATH = @"Assets";
        private const string SHA1_HASH = "6B5B9235C3F630486ED8F07A133B044EAA2E22B2";
        private const string TIMELINE_SAVE_FILE_EXTENSION = ".tls";
        private const string TIMELINE_SAVE_THUMBNAIL_FILE_EXTENSION = ".png";
        private const string CHECKPOINT_FILENAME_SUFFIX = " (checkpoint)";

        private FanCutCommon _fanCutCommon;

        private List<TimelineSave> _timelineSaves;

        private Dictionary<ushort, string> _changeTriggers;
        private Dictionary<ushort, string> _readTriggers;

        private List<string> _changeTriggerStrings = new List<string>()
        {
            "0028, 0029, 002A",
            "002B-00DF",
            PLAYER_HIT_POINTS.ToString("X4") + "(Player Hit Points)"
        };

        private List<string> _changeTriggerExclusionStrings = new List<string>()
        {
            "0029, 002B, 002D, 002E, 002F, 0032, 0033, 003A, 0043, 0044, 0047, 004A, 0068, 0069",
        };

        internal FanCutMegaMan2(FormMain formMain)
        {
            parseMemoryTriggers();

            // this will override all the current saves, only do this when regenerating saves
            //upateTimelineSaves();

            _fanCutCommon = new FanCutCommon(formMain, GAME_NAME, ASSETS_PATH, _timelineSaves, SHA1_HASH);
            _fanCutCommon.ResetThenLoadTimelineSave("levelSelect.tls");
        }

        #region Initialization Routines

        private void parseMemoryTriggers()
        {
            _changeTriggers = new Dictionary<ushort, string>();

            foreach (string changeTriggerString in _changeTriggerStrings)
            {
                List<string> separatedChangeTriggerStrings = changeTriggerString.Split(',').ToList();

                foreach (string separatedChangeTriggerString in separatedChangeTriggerStrings)
                {
                    string workingChangeTriggerString = separatedChangeTriggerString.Trim(' ');

                    if (workingChangeTriggerString.Contains('-'))
                    {
                        string[] changeTriggerRangeParts = workingChangeTriggerString.Split('-');
                        ushort changeTriggerRangeMin = Convert.ToUInt16(changeTriggerRangeParts[0], 16);
                        ushort changeTriggerRangeMax = Convert.ToUInt16(changeTriggerRangeParts[1], 16);

                        for (ushort changeTriggerAddress = changeTriggerRangeMin; changeTriggerAddress <= changeTriggerRangeMax; changeTriggerAddress++)
                            if (!_changeTriggers.ContainsKey(changeTriggerAddress))
                                _changeTriggers.Add(changeTriggerAddress, string.Empty);
                    }
                    else if (workingChangeTriggerString.Contains('('))
                    {
                        string[] annotatedChangeTriggerStringParts = workingChangeTriggerString.Split('(');
                        ushort changeTriggerAddress = Convert.ToUInt16(annotatedChangeTriggerStringParts[0], 16);
                        string changeTriggerDescription = annotatedChangeTriggerStringParts[1].TrimEnd(')');

                        if (_changeTriggers.ContainsKey(changeTriggerAddress))
                            _changeTriggers[changeTriggerAddress] = changeTriggerDescription;
                        else
                            _changeTriggers.Add(changeTriggerAddress, changeTriggerDescription);
                    }
                    else
                    {
                        ushort changeTriggerAddress = Convert.ToUInt16(workingChangeTriggerString, 16);

                        if (!_changeTriggers.ContainsKey(changeTriggerAddress))
                            _changeTriggers.Add(changeTriggerAddress, string.Empty);
                    }
                }
            }

            foreach (string changeTriggerExclusionString in _changeTriggerExclusionStrings)
            {
                List<string> separatedChangeTriggerStrings = changeTriggerExclusionString.Split(',').ToList();

                foreach (string separatedChangeTriggerString in separatedChangeTriggerStrings)
                {
                    string workingChangeTriggerString = separatedChangeTriggerString.Trim(' ');

                    if (workingChangeTriggerString.Contains('-'))
                    {
                        string[] changeTriggerRangeParts = workingChangeTriggerString.Split('-');
                        ushort changeTriggerRangeMin = Convert.ToUInt16(changeTriggerRangeParts[0], 16);
                        ushort changeTriggerRangeMax = Convert.ToUInt16(changeTriggerRangeParts[1], 16);

                        for (ushort changeTriggerAddress = changeTriggerRangeMin; changeTriggerAddress <= changeTriggerRangeMax; changeTriggerAddress++)
                            if (_changeTriggers.ContainsKey(changeTriggerAddress))
                                _changeTriggers.Remove(changeTriggerAddress);
                    }
                    else
                    {
                        ushort changeTriggerAddress = Convert.ToUInt16(workingChangeTriggerString, 16);

                        if (_changeTriggers.ContainsKey(changeTriggerAddress))
                            _changeTriggers.Remove(changeTriggerAddress);
                    }
                }
            }

            NesEmu.ChangeTriggers = new List<ushort>();
            NesEmu.ChangeTriggerHandler = onMemoryChanging;
            foreach (ushort changeTriggerAddress in _changeTriggers.Keys)
                NesEmu.ChangeTriggers.Add(changeTriggerAddress);

            _readTriggers = new Dictionary<ushort, string>();
            //_readTriggerAddresses.Add(0x0440, string.Empty);

            NesEmu.ReadTriggers = new List<ushort>();
            NesEmu.ReadTriggerHandler = onMemoryRead;
            foreach (ushort readTriggerAddress in _readTriggers.Keys)
                NesEmu.ReadTriggers.Add(readTriggerAddress);
        }

        #endregion

        #region Memory Trigger Handlers

        private void onMemoryChanging(ushort address, byte previousValue, byte newValue)
        {
            string label = "0x" + address.ToString("X4");

            if (!string.IsNullOrWhiteSpace(_changeTriggers[address]))
                label = _changeTriggers[address];

            //if (address == PLAYER_HIT_POINTS && newValue == 28)
            //{
            //    NesEmu.WRAM[PLAYER_HIT_POINTS & 0x7FF] = 28;
            //    NesEmu.EmulationPaused = true;
            //    NesEmu.SaveStateAs(ASSETS_PATH + "\\Metal Man (boss).tls");
            //    NesEmu.EmulationPaused = false;
            //}

            Debug.WriteLine(string.Format("{0}: {1} -> {2}", label, previousValue, newValue));
        }

        private void onMemoryRead(ushort address, byte previousValue)
        {
            string label = "0x" + address.ToString("X4");

            if (!string.IsNullOrWhiteSpace(_readTriggers[address]))
                label = _readTriggers[address];

            Debug.WriteLine(string.Format("{0}: {1}", label, previousValue));
        }

        #endregion

        #region Miscellaneous Routines

        private void upateTimelineSaves()
        {
            List<string> saveStateFilenames = (from f in Directory.GetFiles(ASSETS_PATH) where f.Contains("Flash Man") || f.Contains("Metal Man") || f.Contains("Quick Man") select f).ToList();

            foreach (string saveStateFilename in saveStateFilenames)
            {
                FileStream saveStateFileStream = new FileStream(saveStateFilename, FileMode.Open, FileAccess.Read);
                byte[] saveState = new byte[saveStateFileStream.Length];
                saveStateFileStream.Read(saveState, 0, saveState.Length);
                saveStateFileStream.Close();

                for (int loop = 0; loop < 11; loop++)
                    saveState[0x191BE + 0x009C + loop] = 0x1C;

                saveStateFileStream = new FileStream(saveStateFilename, FileMode.Create, FileAccess.Write);
                saveStateFileStream.Write(saveState, 0, saveState.Length);
                saveStateFileStream.Close();
            }
        }

        #endregion
    }
}
