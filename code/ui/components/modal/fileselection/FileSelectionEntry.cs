using Sandbox.UI;
using Sandbox.UI.Construct;

namespace TTTReborn.UI
{
    public partial class FileSelectionEntry : Sandbox.UI.Panel
    {
        public readonly Label FileNameLabel;

        public readonly IconPanel FileTypeIcon;

        public bool IsFolder { get; set; } = false;

        private FileSelection _fileSelection;

        public FileSelectionEntry() : base()
        {
            FileTypeIcon = Add.Icon(null, "filetype");
            FileNameLabel = Add.Label("", "filename");
        }

        public void SetFileSelection(FileSelection fileSelection)
        {
            _fileSelection = fileSelection;
        }

        protected override void OnClick(MousePanelEvent e)
        {
            _fileSelection?.OnSelect(this);
        }

        protected override void OnDoubleClick(MousePanelEvent e)
        {
            _fileSelection?.OnConfirm(this);
        }
    }
}

namespace Sandbox.UI.Construct
{
    using TTTReborn.UI;

    public static class FileSelectionEntryConstructor
    {
        public static FileSelectionEntry FileSelectionEntry(this PanelCreator self, string text = "", string icon = null)
        {
            FileSelectionEntry fileSelectionEntry = self.panel.AddChild<FileSelectionEntry>();
            fileSelectionEntry.FileNameLabel.Text = text;
            fileSelectionEntry.FileTypeIcon.Text = icon;

            return fileSelectionEntry;
        }
    }
}
