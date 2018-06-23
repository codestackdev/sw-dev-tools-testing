using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeStack.Community.DevTools.Sw.Testing.TempDisplay
{
    public class SyncFormDisposeToken : IDisposeToken
    {
        public event DisposeFunction DisposePreview;

        public void Wait()
        {
            MessageBox.Show("Click OK to exit the preview");

            DisposePreview?.Invoke();
        }
    }
}
