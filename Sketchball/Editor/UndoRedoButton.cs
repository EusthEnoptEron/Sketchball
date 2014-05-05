using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchball.Editor
{


    public class UndoButton : ToolStripButton
    {
        private History history;

        public UndoButton(History history)
        {
            this.history = history;
            history.Change += OnChange;
            this.Image = Properties.Resources.Undo_icon;

            OnChange();
        }

        protected override void OnClick(EventArgs e)
        {
            history.Undo();
        }

        void OnChange()
        {
            Enabled = history.CanUndo();
        }

    }

    public class UndoItem : ToolStripMenuItem
    {
        private History history;

        public UndoItem(History history)
        {
            this.history = history;
            history.Change += OnChange;
            this.Image = Properties.Resources.Undo_icon;
            this.Text = "Undo";
            this.ShortcutKeys = Keys.Control | Keys.Z;
            OnChange();
        }

        protected override void OnClick(EventArgs e)
        {
            history.Undo();
        }

        void OnChange()
        {
            Enabled = history.CanUndo();
        }

    }


    public class RedoButton : ToolStripButton
    {
        private History history;

        public RedoButton(History history)
        {
            this.history = history;
            history.Change += OnChange;
            this.Image = Properties.Resources.Redo_icon;

            OnChange();
        }

        protected override void OnClick(EventArgs e)
        {
            history.Redo();
        }

        void OnChange()
        {
            Enabled = history.CanRedo();
        }

    }

    public class RedoItem : ToolStripMenuItem
    {
        private History history;

        public RedoItem(History history)
        {
            this.history = history;
            history.Change += OnChange;
            this.Image = Properties.Resources.Redo_icon;
            this.Text = "Redo";
            this.ShortcutKeys = Keys.Control | Keys.Y;
            OnChange();
        }

        protected override void OnClick(EventArgs e)
        {
            history.Redo();
        }

        void OnChange()
        {
            Enabled = history.CanRedo();
        }

    }
}
