namespace MyNes
{
    internal class SMBLevel
    {
        internal string Name { get; set; }
        internal byte WorldNumber { get; set; }
        internal byte LevelDisplayNumber { get; set; }
        internal byte LevelNumber { get; set; }
        internal byte AreaLoadedValue { get; set; }
        internal byte CheckpointScreenNumber { get; set; }
    }
}
