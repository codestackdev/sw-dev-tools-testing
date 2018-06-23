using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeStack.Community.DevTools.Sw.Testing.TempDisplay
{
    public delegate void DisposeFunction();

    public interface IDisposeToken
    {
        void Wait();

        event DisposeFunction DisposePreview;
    }
}
