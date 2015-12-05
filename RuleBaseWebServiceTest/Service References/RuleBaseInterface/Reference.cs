﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RuleBaseWebServiceTest.RuleBaseInterface {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Bank", Namespace="http://WorldOfRule.org/")]
    [System.SerializableAttribute()]
    public partial class Bank : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LoanRequest", Namespace="http://WorldOfRule.org/")]
    [System.SerializableAttribute()]
    public partial class LoanRequest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int CreditScoreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SSNField;
        
        private decimal AmountField;
        
        private int DurationField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int CreditScore {
            get {
                return this.CreditScoreField;
            }
            set {
                if ((this.CreditScoreField.Equals(value) != true)) {
                    this.CreditScoreField = value;
                    this.RaisePropertyChanged("CreditScore");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string SSN {
            get {
                return this.SSNField;
            }
            set {
                if ((object.ReferenceEquals(this.SSNField, value) != true)) {
                    this.SSNField = value;
                    this.RaisePropertyChanged("SSN");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public decimal Amount {
            get {
                return this.AmountField;
            }
            set {
                if ((this.AmountField.Equals(value) != true)) {
                    this.AmountField = value;
                    this.RaisePropertyChanged("Amount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=3)]
        public int Duration {
            get {
                return this.DurationField;
            }
            set {
                if ((this.DurationField.Equals(value) != true)) {
                    this.DurationField = value;
                    this.RaisePropertyChanged("Duration");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://WorldOfRule.org/", ConfigurationName="RuleBaseInterface.RuleBaseSoap")]
    public interface RuleBaseSoap {
        
        // CODEGEN: Generating message contract since element name bank from namespace http://WorldOfRule.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://WorldOfRule.org/AddABank", ReplyAction="*")]
        RuleBaseWebServiceTest.RuleBaseInterface.AddABankResponse AddABank(RuleBaseWebServiceTest.RuleBaseInterface.AddABankRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WorldOfRule.org/AddABank", ReplyAction="*")]
        System.Threading.Tasks.Task<RuleBaseWebServiceTest.RuleBaseInterface.AddABankResponse> AddABankAsync(RuleBaseWebServiceTest.RuleBaseInterface.AddABankRequest request);
        
        // CODEGEN: Generating message contract since element name bank from namespace http://WorldOfRule.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://WorldOfRule.org/RemoveABank", ReplyAction="*")]
        RuleBaseWebServiceTest.RuleBaseInterface.RemoveABankResponse RemoveABank(RuleBaseWebServiceTest.RuleBaseInterface.RemoveABankRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WorldOfRule.org/RemoveABank", ReplyAction="*")]
        System.Threading.Tasks.Task<RuleBaseWebServiceTest.RuleBaseInterface.RemoveABankResponse> RemoveABankAsync(RuleBaseWebServiceTest.RuleBaseInterface.RemoveABankRequest request);
        
        // CODEGEN: Generating message contract since element name loanRequest from namespace http://WorldOfRule.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://WorldOfRule.org/GetBanks", ReplyAction="*")]
        RuleBaseWebServiceTest.RuleBaseInterface.GetBanksResponse GetBanks(RuleBaseWebServiceTest.RuleBaseInterface.GetBanksRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WorldOfRule.org/GetBanks", ReplyAction="*")]
        System.Threading.Tasks.Task<RuleBaseWebServiceTest.RuleBaseInterface.GetBanksResponse> GetBanksAsync(RuleBaseWebServiceTest.RuleBaseInterface.GetBanksRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AddABankRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AddABank", Namespace="http://WorldOfRule.org/", Order=0)]
        public RuleBaseWebServiceTest.RuleBaseInterface.AddABankRequestBody Body;
        
        public AddABankRequest() {
        }
        
        public AddABankRequest(RuleBaseWebServiceTest.RuleBaseInterface.AddABankRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://WorldOfRule.org/")]
    public partial class AddABankRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public RuleBaseWebServiceTest.RuleBaseInterface.Bank bank;
        
        public AddABankRequestBody() {
        }
        
        public AddABankRequestBody(RuleBaseWebServiceTest.RuleBaseInterface.Bank bank) {
            this.bank = bank;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AddABankResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AddABankResponse", Namespace="http://WorldOfRule.org/", Order=0)]
        public RuleBaseWebServiceTest.RuleBaseInterface.AddABankResponseBody Body;
        
        public AddABankResponse() {
        }
        
        public AddABankResponse(RuleBaseWebServiceTest.RuleBaseInterface.AddABankResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class AddABankResponseBody {
        
        public AddABankResponseBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class RemoveABankRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="RemoveABank", Namespace="http://WorldOfRule.org/", Order=0)]
        public RuleBaseWebServiceTest.RuleBaseInterface.RemoveABankRequestBody Body;
        
        public RemoveABankRequest() {
        }
        
        public RemoveABankRequest(RuleBaseWebServiceTest.RuleBaseInterface.RemoveABankRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://WorldOfRule.org/")]
    public partial class RemoveABankRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public RuleBaseWebServiceTest.RuleBaseInterface.Bank bank;
        
        public RemoveABankRequestBody() {
        }
        
        public RemoveABankRequestBody(RuleBaseWebServiceTest.RuleBaseInterface.Bank bank) {
            this.bank = bank;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class RemoveABankResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="RemoveABankResponse", Namespace="http://WorldOfRule.org/", Order=0)]
        public RuleBaseWebServiceTest.RuleBaseInterface.RemoveABankResponseBody Body;
        
        public RemoveABankResponse() {
        }
        
        public RemoveABankResponse(RuleBaseWebServiceTest.RuleBaseInterface.RemoveABankResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class RemoveABankResponseBody {
        
        public RemoveABankResponseBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetBanksRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetBanks", Namespace="http://WorldOfRule.org/", Order=0)]
        public RuleBaseWebServiceTest.RuleBaseInterface.GetBanksRequestBody Body;
        
        public GetBanksRequest() {
        }
        
        public GetBanksRequest(RuleBaseWebServiceTest.RuleBaseInterface.GetBanksRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://WorldOfRule.org/")]
    public partial class GetBanksRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public RuleBaseWebServiceTest.RuleBaseInterface.LoanRequest loanRequest;
        
        public GetBanksRequestBody() {
        }
        
        public GetBanksRequestBody(RuleBaseWebServiceTest.RuleBaseInterface.LoanRequest loanRequest) {
            this.loanRequest = loanRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetBanksResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetBanksResponse", Namespace="http://WorldOfRule.org/", Order=0)]
        public RuleBaseWebServiceTest.RuleBaseInterface.GetBanksResponseBody Body;
        
        public GetBanksResponse() {
        }
        
        public GetBanksResponse(RuleBaseWebServiceTest.RuleBaseInterface.GetBanksResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://WorldOfRule.org/")]
    public partial class GetBanksResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public RuleBaseWebServiceTest.RuleBaseInterface.Bank[] GetBanksResult;
        
        public GetBanksResponseBody() {
        }
        
        public GetBanksResponseBody(RuleBaseWebServiceTest.RuleBaseInterface.Bank[] GetBanksResult) {
            this.GetBanksResult = GetBanksResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface RuleBaseSoapChannel : RuleBaseWebServiceTest.RuleBaseInterface.RuleBaseSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RuleBaseSoapClient : System.ServiceModel.ClientBase<RuleBaseWebServiceTest.RuleBaseInterface.RuleBaseSoap>, RuleBaseWebServiceTest.RuleBaseInterface.RuleBaseSoap {
        
        public RuleBaseSoapClient() {
        }
        
        public RuleBaseSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RuleBaseSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RuleBaseSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RuleBaseSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        RuleBaseWebServiceTest.RuleBaseInterface.AddABankResponse RuleBaseWebServiceTest.RuleBaseInterface.RuleBaseSoap.AddABank(RuleBaseWebServiceTest.RuleBaseInterface.AddABankRequest request) {
            return base.Channel.AddABank(request);
        }
        
        public void AddABank(RuleBaseWebServiceTest.RuleBaseInterface.Bank bank) {
            RuleBaseWebServiceTest.RuleBaseInterface.AddABankRequest inValue = new RuleBaseWebServiceTest.RuleBaseInterface.AddABankRequest();
            inValue.Body = new RuleBaseWebServiceTest.RuleBaseInterface.AddABankRequestBody();
            inValue.Body.bank = bank;
            RuleBaseWebServiceTest.RuleBaseInterface.AddABankResponse retVal = ((RuleBaseWebServiceTest.RuleBaseInterface.RuleBaseSoap)(this)).AddABank(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<RuleBaseWebServiceTest.RuleBaseInterface.AddABankResponse> RuleBaseWebServiceTest.RuleBaseInterface.RuleBaseSoap.AddABankAsync(RuleBaseWebServiceTest.RuleBaseInterface.AddABankRequest request) {
            return base.Channel.AddABankAsync(request);
        }
        
        public System.Threading.Tasks.Task<RuleBaseWebServiceTest.RuleBaseInterface.AddABankResponse> AddABankAsync(RuleBaseWebServiceTest.RuleBaseInterface.Bank bank) {
            RuleBaseWebServiceTest.RuleBaseInterface.AddABankRequest inValue = new RuleBaseWebServiceTest.RuleBaseInterface.AddABankRequest();
            inValue.Body = new RuleBaseWebServiceTest.RuleBaseInterface.AddABankRequestBody();
            inValue.Body.bank = bank;
            return ((RuleBaseWebServiceTest.RuleBaseInterface.RuleBaseSoap)(this)).AddABankAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        RuleBaseWebServiceTest.RuleBaseInterface.RemoveABankResponse RuleBaseWebServiceTest.RuleBaseInterface.RuleBaseSoap.RemoveABank(RuleBaseWebServiceTest.RuleBaseInterface.RemoveABankRequest request) {
            return base.Channel.RemoveABank(request);
        }
        
        public void RemoveABank(RuleBaseWebServiceTest.RuleBaseInterface.Bank bank) {
            RuleBaseWebServiceTest.RuleBaseInterface.RemoveABankRequest inValue = new RuleBaseWebServiceTest.RuleBaseInterface.RemoveABankRequest();
            inValue.Body = new RuleBaseWebServiceTest.RuleBaseInterface.RemoveABankRequestBody();
            inValue.Body.bank = bank;
            RuleBaseWebServiceTest.RuleBaseInterface.RemoveABankResponse retVal = ((RuleBaseWebServiceTest.RuleBaseInterface.RuleBaseSoap)(this)).RemoveABank(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<RuleBaseWebServiceTest.RuleBaseInterface.RemoveABankResponse> RuleBaseWebServiceTest.RuleBaseInterface.RuleBaseSoap.RemoveABankAsync(RuleBaseWebServiceTest.RuleBaseInterface.RemoveABankRequest request) {
            return base.Channel.RemoveABankAsync(request);
        }
        
        public System.Threading.Tasks.Task<RuleBaseWebServiceTest.RuleBaseInterface.RemoveABankResponse> RemoveABankAsync(RuleBaseWebServiceTest.RuleBaseInterface.Bank bank) {
            RuleBaseWebServiceTest.RuleBaseInterface.RemoveABankRequest inValue = new RuleBaseWebServiceTest.RuleBaseInterface.RemoveABankRequest();
            inValue.Body = new RuleBaseWebServiceTest.RuleBaseInterface.RemoveABankRequestBody();
            inValue.Body.bank = bank;
            return ((RuleBaseWebServiceTest.RuleBaseInterface.RuleBaseSoap)(this)).RemoveABankAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        RuleBaseWebServiceTest.RuleBaseInterface.GetBanksResponse RuleBaseWebServiceTest.RuleBaseInterface.RuleBaseSoap.GetBanks(RuleBaseWebServiceTest.RuleBaseInterface.GetBanksRequest request) {
            return base.Channel.GetBanks(request);
        }
        
        public RuleBaseWebServiceTest.RuleBaseInterface.Bank[] GetBanks(RuleBaseWebServiceTest.RuleBaseInterface.LoanRequest loanRequest) {
            RuleBaseWebServiceTest.RuleBaseInterface.GetBanksRequest inValue = new RuleBaseWebServiceTest.RuleBaseInterface.GetBanksRequest();
            inValue.Body = new RuleBaseWebServiceTest.RuleBaseInterface.GetBanksRequestBody();
            inValue.Body.loanRequest = loanRequest;
            RuleBaseWebServiceTest.RuleBaseInterface.GetBanksResponse retVal = ((RuleBaseWebServiceTest.RuleBaseInterface.RuleBaseSoap)(this)).GetBanks(inValue);
            return retVal.Body.GetBanksResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<RuleBaseWebServiceTest.RuleBaseInterface.GetBanksResponse> RuleBaseWebServiceTest.RuleBaseInterface.RuleBaseSoap.GetBanksAsync(RuleBaseWebServiceTest.RuleBaseInterface.GetBanksRequest request) {
            return base.Channel.GetBanksAsync(request);
        }
        
        public System.Threading.Tasks.Task<RuleBaseWebServiceTest.RuleBaseInterface.GetBanksResponse> GetBanksAsync(RuleBaseWebServiceTest.RuleBaseInterface.LoanRequest loanRequest) {
            RuleBaseWebServiceTest.RuleBaseInterface.GetBanksRequest inValue = new RuleBaseWebServiceTest.RuleBaseInterface.GetBanksRequest();
            inValue.Body = new RuleBaseWebServiceTest.RuleBaseInterface.GetBanksRequestBody();
            inValue.Body.loanRequest = loanRequest;
            return ((RuleBaseWebServiceTest.RuleBaseInterface.RuleBaseSoap)(this)).GetBanksAsync(inValue);
        }
    }
}
