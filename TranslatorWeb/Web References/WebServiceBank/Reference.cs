﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace TranslatorWeb.WebServiceBank {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WebServiceBankSoap", Namespace="http://SharkLoans.com")]
    public partial class WebServiceBank : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ProcessLoanRequestOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WebServiceBank() {
            this.Url = global::TranslatorWeb.Properties.Settings.Default.TranslatorWeb_WebServiceBank_WebServiceBank;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event ProcessLoanRequestCompletedEventHandler ProcessLoanRequestCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://SharkLoans.com/ProcessLoanRequest", RequestNamespace="http://SharkLoans.com", ResponseNamespace="http://SharkLoans.com", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public decimal ProcessLoanRequest(string ssn, int creditScore, decimal amount, int duration) {
            object[] results = this.Invoke("ProcessLoanRequest", new object[] {
                        ssn,
                        creditScore,
                        amount,
                        duration});
            return ((decimal)(results[0]));
        }
        
        /// <remarks/>
        public void ProcessLoanRequestAsync(string ssn, int creditScore, decimal amount, int duration) {
            this.ProcessLoanRequestAsync(ssn, creditScore, amount, duration, null);
        }
        
        /// <remarks/>
        public void ProcessLoanRequestAsync(string ssn, int creditScore, decimal amount, int duration, object userState) {
            if ((this.ProcessLoanRequestOperationCompleted == null)) {
                this.ProcessLoanRequestOperationCompleted = new System.Threading.SendOrPostCallback(this.OnProcessLoanRequestOperationCompleted);
            }
            this.InvokeAsync("ProcessLoanRequest", new object[] {
                        ssn,
                        creditScore,
                        amount,
                        duration}, this.ProcessLoanRequestOperationCompleted, userState);
        }
        
        private void OnProcessLoanRequestOperationCompleted(object arg) {
            if ((this.ProcessLoanRequestCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ProcessLoanRequestCompleted(this, new ProcessLoanRequestCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void ProcessLoanRequestCompletedEventHandler(object sender, ProcessLoanRequestCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ProcessLoanRequestCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ProcessLoanRequestCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public decimal Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((decimal)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591