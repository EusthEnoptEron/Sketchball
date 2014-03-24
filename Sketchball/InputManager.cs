using MouseKeyboardActivityMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchball
{
    class InputManager : IDisposable
    {
        private KeyboardHookListener keyboardListener;

        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyUp;
        public event KeyPressEventHandler KeyPress;


        /// <summary>
        /// Initializes a new InputManager with a given control to listen to.
        /// </summary>
        /// <param name="control"></param>
        public InputManager()
        {
            keyboardListener = new KeyboardHookListener(new MouseKeyboardActivityMonitor.WinApi.AppHooker());
            keyboardListener.Enabled = true;

            keyboardListener.KeyDown += KeyDown;
            keyboardListener.KeyUp += KeyUp;
            keyboardListener.KeyPress += KeyPress;
        }

        public void Dispose()
        {
            keyboardListener.Dispose();
        }
    }
}
