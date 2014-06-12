using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sketchball.Controls
{
    /// <summary>
    /// Stub WPF control used by WPFContainer. Adds some disposal logic.
    /// </summary>
    public partial class ManagedWPFControl : UserControl, IDisposable
    {
        protected bool isCancelled = false;

        public ManagedWPFControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Disposes this element.
        /// </summary>
        public void Dispose()
        {
            isCancelled = true;
            OnDispose();
        }

        protected virtual void OnDispose() { }
    }
}
