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
    /// Interaction logic for ManagedWPFControl.xaml
    /// </summary>
    public partial class ManagedWPFControl : UserControl
    {
        protected bool isCancelled = false;

        public ManagedWPFControl()
        {
            InitializeComponent();
        }

        public void Exit()
        {
            isCancelled = true;
            this.Width = 0;
            this.Height = 0;

            
        }

        protected virtual void OnDispose() { }
    }
}
