using CodeStack.Community.DevTools.Sw.Testing.Parameters;
using CodeStack.Community.DevTools.Sw.Testing.Services;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.IO;

namespace CodeStack.Community.DevTools.Sw.Testing
{
    public class TestService : IDisposable
    {
        public ISldWorks SldWorks { get; private set; }

        private bool m_CloseApp;

        private ApplicationService m_App;
        private DocumentService m_Doc;

        public ApplicationService App
        {
            get
            {
                return m_App ?? (m_App = new ApplicationService(SldWorks));
            }
        }
        
        public DocumentService Doc
        {
            get
            {
                return m_Doc ?? (m_Doc = new DocumentService());
            }
        }

        public TestService() : this(null) //TODO: load parameters from file
        {
        }

        public TestService(TestServiceStartupParameters parameters)
        {
            switch (parameters.ConnectOption)
            {
                case AppConnectOption_e.ConnectToProcess:
                    ConnectToProcess(parameters.ConnectionDetails as ConnectToProcessConnectionDetails);
                    break;
                case AppConnectOption_e.LoadEmbeded:
                    LoadEmbeded(parameters.ConnectionDetails as LoadEmbededConnectionDetails);
                    break;
                case AppConnectOption_e.StartFromPath:
                    StartFromPath(parameters.ConnectionDetails as StartFromPathConnectionDetails);
                    break;
            }

            if (SldWorks == null)
            {
                throw new NullReferenceException("Failed to connect to SOLIDWORKS");
            }
        }
        
        private void ConnectToProcess(ConnectToProcessConnectionDetails opts)
        {
            SldWorks = AppStartService.GetSwAppFromProcess(opts.ProcessToConnect);
            m_CloseApp = false;
        }

        private void LoadEmbeded(LoadEmbededConnectionDetails opts)
        {
            AppStartService.CreateEmbeded(opts.UseAnyVersion ? -1 : opts.RevisionNumber);
        }

        private void StartFromPath(StartFromPathConnectionDetails opts)
        {
            AppStartService.StartSwApp(opts.ExecutablePath, opts.Timeout);
            m_CloseApp = true;
        }

        public void Dispose()
        {
            if (m_CloseApp)
            {
                if (SldWorks != null)
                {
                    SldWorks.ExitApp();
                }
            }
        }
    }
}
