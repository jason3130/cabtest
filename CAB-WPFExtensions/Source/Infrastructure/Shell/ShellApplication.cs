//===============================================================================
// Microsoft patterns & practices
// Smart Client Software Factory 2010
//===============================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================
//----------------------------------------------------------------------------------------
// patterns & practices - Smart Client Software Factory - Guidance Package
//
// This file was generated by this guidance package as part of the solution template
//
// The ShellApplication class is the entry point for your application. ShellApplication 
// contains the Main method and derives from FormShellApplication base class which is
// provided by the Composite UI Application Block (CAB).
// 
// Note that the RootWorkItem is the default WorkItem provided by CAB.
// 
// It also implements basic exception handling using Enterprise Library Exception
// Handling Application Block.
//
// The basic shell in this Guidance Package (ShellForm) has a menu, a statusbar and
// the screen is divided in 2 workspaces (left and right panes) separated by a spliter
//
// For more information see: 
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-210-Creating%20a%20Smart%20Client%20Solution.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.WinForms;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Microsoft.Practices.QuickStarts.WPFIntegration.Infrastructure.Interface;
using Microsoft.Practices.QuickStarts.WPFIntegration.Infrastructure.Interface.Services;
using Microsoft.Practices.QuickStarts.WPFIntegration.Infrastructure.Shell.Constants;
using Microsoft.Practices.QuickStarts.WPFIntegration.Infrastructure.Library;


namespace Microsoft.Practices.QuickStarts.WPFIntegration.Infrastructure.Shell
{
    /// <summary>
    /// Main application entry point class.
    /// Note that the class derives from CAB supplied base class FormShellApplication, and the 
    /// main form will be ShellForm, also created by default by this solution template
    /// </summary>
    class ShellApplication : SmartClientApplication<WorkItem, ShellForm>
    {
        /// <summary>
        /// Application entry point.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if (DEBUG)
			RunInDebugMode();
#else
            RunInReleaseMode();
#endif
        }

        private static void RunInDebugMode()
        {
            Application.SetCompatibleTextRenderingDefault(false);
            new ShellApplication().Run();
        }

        private static void RunInReleaseMode()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(AppDomainUnhandledException);
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                new ShellApplication().Run();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        /// <summary>
        /// Sets the extension site registration after the shell has been created.
        /// </summary>
        protected override void AfterShellCreated()
        {
            base.AfterShellCreated();

            RootWorkItem.UIExtensionSites.RegisterSite(UIExtensionSiteNames.MainStatus, this.Shell.MainStatusStrip);
        }


        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private static void HandleException(Exception ex)
        {
            if (ex == null)
                return;

            ExceptionPolicy.HandleException(ex, "Default Policy");
            MessageBox.Show("An unhandled exception occurred, and the application is terminating. For more information, see your Application event log.");
            Application.Exit();
        }
    }
}
