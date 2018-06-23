using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeStack.Community.DevTools.Sw.Testing.Services
{
    public class ApplicationService
    {
        private ISldWorks m_App;

        public ApplicationService(ISldWorks app)
        {
            m_App = app;
        }

        public TRes WithDocument<TRes>(string path, Func<IModelDoc2, TRes> func)
        {
            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location), path);
            }

            var ext = Path.GetExtension(path).ToLower();

            var docType = swDocumentTypes_e.swDocNONE;

            switch (ext)
            {
                case ".sldprt":
                    docType = swDocumentTypes_e.swDocPART;
                    break;

                case ".sldasm":
                    docType = swDocumentTypes_e.swDocASSEMBLY;
                    break;

                case ".slddrw":
                    docType = swDocumentTypes_e.swDocDRAWING;
                    break;
            }

            int err = -1;
            int warn = -1;
            var model = m_App.OpenDoc6(path, (int)docType,
                (int)swOpenDocOptions_e.swOpenDocOptions_Silent
                + (int)swOpenDocOptions_e.swOpenDocOptions_ReadOnly, "", 
                ref err, ref warn);

            if (model == null)
            {
                throw new NullReferenceException(
                    $"Failed to open model {path}. Error :{(swFileLoadError_e)err}. Warning: Errors :{(swFileLoadWarning_e)warn}");
            }

            var res = func.Invoke(model);

            m_App.CloseDoc(model.GetTitle());

            return res;
        }
    }
}
