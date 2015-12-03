﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CreditScoreInterface.CreditScoreService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://service.web.credit.service.bank.org/", ConfigurationName="CreditScoreService.CreditScoreService")]
    public interface CreditScoreService {
        
        // CODEGEN: Generating message contract since element name ssn from namespace  is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://service.web.credit.service.bank.org/CreditScoreService/creditScoreRequest", ReplyAction="http://service.web.credit.service.bank.org/CreditScoreService/creditScoreResponse" +
            "")]
        CreditScoreInterface.CreditScoreService.creditScoreResponse creditScore(CreditScoreInterface.CreditScoreService.creditScoreRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://service.web.credit.service.bank.org/CreditScoreService/creditScoreRequest", ReplyAction="http://service.web.credit.service.bank.org/CreditScoreService/creditScoreResponse" +
            "")]
        System.Threading.Tasks.Task<CreditScoreInterface.CreditScoreService.creditScoreResponse> creditScoreAsync(CreditScoreInterface.CreditScoreService.creditScoreRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class creditScoreRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="creditScore", Namespace="http://service.web.credit.service.bank.org/", Order=0)]
        public CreditScoreInterface.CreditScoreService.creditScoreRequestBody Body;
        
        public creditScoreRequest() {
        }
        
        public creditScoreRequest(CreditScoreInterface.CreditScoreService.creditScoreRequestBody Body) {
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
        public CreditScoreInterface.CreditScoreService.creditScoreResponseBody Body;
        
        public creditScoreResponse() {
        }
        
        public creditScoreResponse(CreditScoreInterface.CreditScoreService.creditScoreResponseBody Body) {
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
    public interface CreditScoreServiceChannel : CreditScoreInterface.CreditScoreService.CreditScoreService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CreditScoreServiceClient : System.ServiceModel.ClientBase<CreditScoreInterface.CreditScoreService.CreditScoreService>, CreditScoreInterface.CreditScoreService.CreditScoreService {
        
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
        CreditScoreInterface.CreditScoreService.creditScoreResponse CreditScoreInterface.CreditScoreService.CreditScoreService.creditScore(CreditScoreInterface.CreditScoreService.creditScoreRequest request) {
            return base.Channel.creditScore(request);
        }
        
        public int creditScore(string ssn) {
            CreditScoreInterface.CreditScoreService.creditScoreRequest inValue = new CreditScoreInterface.CreditScoreService.creditScoreRequest();
            inValue.Body = new CreditScoreInterface.CreditScoreService.creditScoreRequestBody();
            inValue.Body.ssn = ssn;
            CreditScoreInterface.CreditScoreService.creditScoreResponse retVal = ((CreditScoreInterface.CreditScoreService.CreditScoreService)(this)).creditScore(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<CreditScoreInterface.CreditScoreService.creditScoreResponse> CreditScoreInterface.CreditScoreService.CreditScoreService.creditScoreAsync(CreditScoreInterface.CreditScoreService.creditScoreRequest request) {
            return base.Channel.creditScoreAsync(request);
        }
        
        public System.Threading.Tasks.Task<CreditScoreInterface.CreditScoreService.creditScoreResponse> creditScoreAsync(string ssn) {
            CreditScoreInterface.CreditScoreService.creditScoreRequest inValue = new CreditScoreInterface.CreditScoreService.creditScoreRequest();
            inValue.Body = new CreditScoreInterface.CreditScoreService.creditScoreRequestBody();
            inValue.Body.ssn = ssn;
            return ((CreditScoreInterface.CreditScoreService.CreditScoreService)(this)).creditScoreAsync(inValue);
        }
    }
}