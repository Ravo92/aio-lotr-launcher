namespace LauncherGUI.Helpers
{
    public enum DataGridKindOf { Patch, Mod }

    public class PatchesAndModsVM
    {
        public string DataGridVerionNumber { get; set; }
        public DataGridKindOf DataGridKindOf { get; set; }

        public void SetDataGridKindOf(DataGridKindOf value)
        {
            DataGridKindOf = value;
        }

        public DataGridKindOf GetDataGridKindOf()
        {
            return DataGridKindOf;
        }

        public string? DataGridDescription { get; set; }
        public bool DataGridActivated { get; set; }
    }
}