﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CreditScoreImport.CreditScoreReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://service.web.credit.service.bank.org/", ConfigurationName="CreditScoreReference.CreditScoreService")]
    public interface CreditScoreService {
        
        // CODEGEN: Generating message contract since element name ssn from namespace  is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://service.web.credit.service.bank.org/CreditScoreService/creditScoreRequest", ReplyAction="http://service.web.credit.service.bank.org/CreditScoreService/creditScoreResponse" +
            "")]
        CreditScoreImport.CreditScoreReference.creditScoreResponse creditScore(CreditScoreImport.CreditScoreReference.creditScoreRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class creditScoreRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="creditScore", Namespace="http://service.web.credit.service.bank.org/", Order=0)]
        public CreditScoreImport.CreditScoreReference.creditScoreRequestBody Body;
        
        public creditScoreRequest() {
        }
        
        public creditScoreRequest(CreditScoreImport.CreditScoreReference.creditScoreRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class creditScoreRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ssn;
        
        public creditScoreRequestBody() {
        }
        
        public creditScoreRequestBody(string ssn) {
            this.ssn = ssn;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class creditScoreResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="creditScoreResponse", Namespace="http://service.web.credit.service.bank.org/", Order=0)]
        public CreditScoreImport.CreditScoreReference.creditScoreResponseBody Body;
        
        public creditScoreResponse() {
        }
        
        public creditScoreResponse(CreditScoreImport.CreditScoreReference.creditScoreResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class creditScoreResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public int @return;
        
        public creditScoreResponseBody() {
        }
        
        public creditScoreResponseBody(int @return) {
            this.@return = @return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface CreditScoreServiceChannel : CreditScoreImport.CreditScoreReference.CreditScoreService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CreditScoreServiceClient : System.ServiceModel.ClientBase<CreditScoreImport.CreditScoreReference.CreditScoreService>, CreditScoreImport.CreditScoreReference.CreditScoreService {
        
        public CreditScoreServiceClient() {
        }
        
        public CreditScoreServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CreditScoreServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CreditScoreServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CreditScoreServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        CreditScoreImport.CreditScoreReference.creditScoreResponse CreditScoreImport.CreditScoreReference.CreditScoreService.creditScore(CreditScoreImport.CreditScoreReference.creditScoreRequest request) {
            return base.Channel.creditScore(request);
        }
        
        public int creditScore(string ssn) {
            CreditScoreImport.CreditScoreReference.creditScoreRequest inValue = new CreditScoreImport.CreditScoreReference.creditScoreRequest();
            inValue.Body = new CreditScoreImport.CreditScoreReference.creditScoreRequestBody();
            inValue.Body.ssn = ssn;
            CreditScoreImport.CreditScoreReference.creditScoreResponse retVal = ((CreditScoreImport.CreditScoreReference.CreditScoreService)(this)).creditScore(inValue);
            return retVal.Body.@return;
        }
    }
}
