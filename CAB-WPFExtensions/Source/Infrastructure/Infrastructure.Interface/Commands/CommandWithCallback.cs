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
// For more information see: 
// ms-help://MS.VSCC.v80/MS.VSIPCC.v80/ms.scsf.2006jun/SCSF/html/03-210-Creating%20a%20Smart%20Client%20Solution.htm
//
// Latest version of this Guidance Package: http://go.microsoft.com/fwlink/?LinkId=62182
//----------------------------------------------------------------------------------------

using System;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Microsoft.Practices.QuickStarts.WPFIntegration.Infrastructure.Interface.Commands
{
    public abstract class CommandWithCallback<TService, TReturnType> : Command<TService>
        where TService : ISupportsTimeout
    {
        public delegate void CallbackType(bool success, TReturnType returnValue);

        private CallbackType _callback;

        protected CommandWithCallback(TService service, int timeout, CallbackType callback)
            : base(service, timeout)
        {
            _callback = callback;
        }

        protected override sealed void DoExecute()
        {
            bool success = false;
            TReturnType returnValue = default(TReturnType);

            try
            {
                returnValue = DoCallService();
                success = true;
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Default Policy");
                returnValue = default(TReturnType);
                success = false;
            }

            _callback(success, returnValue);
        }

        protected abstract TReturnType DoCallService();
    }
}
