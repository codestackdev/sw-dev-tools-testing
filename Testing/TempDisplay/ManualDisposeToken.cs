using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeStack.Community.DevTools.Sw.Testing.TempDisplay
{
    public class ManualDisposeToken : IDisposeToken
    {
        public event DisposeFunction DisposePreview;

        public void Wait()
        {
        }

        public void Dispose()
        {
            DisposePreview?.Invoke();
        }
    }
}
