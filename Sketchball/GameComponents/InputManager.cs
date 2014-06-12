using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchball.GameComponents
{
    /// <summary>
    /// Listens to Windows' application-wide input messages and lets you bind event handlers to them.
    /// </summary>
    class InputManager : IDisposable
    {
        /// <summary>
        /// Initializes a new InputManager with a given control to listen to.
        /// </summary>
        /// <param name="control"></param>
        private InputManager()
        {

            keyboardListener = new KeyboardHookListener(new MouseKeyboardActivityMonitor.WinApi.AppHooker());
            keyboardListener.Enabled = true;
        }

        /// <summary>
        /// Gets or sets the listening status.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return keyboardListener.Enabled;
            }
            set
            {
                keyboardListener.Enabled = value;
            }
        }

        private static InputManager instance = null;

        /// <summary>
        /// Gets an instance of the manager.
        /// </summary>
        /// <returns></returns>
        public static InputManager Instance()
        {
            if (instance == null) instance = new InputManager();
            return instance;
        }

        private KeyboardHookListener keyboardListener;

        public event KeyEventHandler KeyDown {
            add    { keyboardListener.KeyDown += value; }
            remove { keyboardListener.KeyDown -= value; }
        }

        public event KeyEventHandler KeyUp
        {
            add { keyboardListener.KeyUp += value; }
            remove { keyboardListener.KeyUp -= value; }
        }

        public event KeyPressEventHandler KeyPress
        {
            add { keyboardListener.KeyPress += value; }
            remove { keyboardListener.KeyPress -= value; }
        }


        public void Dispose()
        {
            keyboardListener.Dispose();
        }
    }
}
