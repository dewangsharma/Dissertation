﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace myWebAspClient.student_Report_WCFLib {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="student_Report_WCFLib.IStudentReport")]
    public interface IStudentReport {
        
        // CODEGEN: Parameter 'getAllQuizByStudentIdResult' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudentReport/getAllQuizByStudentId", ReplyAction="http://tempuri.org/IStudentReport/getAllQuizByStudentIdResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        myWebAspClient.student_Report_WCFLib.getAllQuizByStudentIdResponse getAllQuizByStudentId(myWebAspClient.student_Report_WCFLib.getAllQuizByStudentIdRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudentReport/getAllQuizByStudentId", ReplyAction="http://tempuri.org/IStudentReport/getAllQuizByStudentIdResponse")]
        System.Threading.Tasks.Task<myWebAspClient.student_Report_WCFLib.getAllQuizByStudentIdResponse> getAllQuizByStudentIdAsync(myWebAspClient.student_Report_WCFLib.getAllQuizByStudentIdRequest request);
        
        // CODEGEN: Parameter 'getAllQuesByQuizIdResult' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudentReport/getAllQuesByQuizId", ReplyAction="http://tempuri.org/IStudentReport/getAllQuesByQuizIdResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        myWebAspClient.student_Report_WCFLib.getAllQuesByQuizIdResponse getAllQuesByQuizId(myWebAspClient.student_Report_WCFLib.getAllQuesByQuizIdRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStudentReport/getAllQuesByQuizId", ReplyAction="http://tempuri.org/IStudentReport/getAllQuesByQuizIdResponse")]
        System.Threading.Tasks.Task<myWebAspClient.student_Report_WCFLib.getAllQuesByQuizIdResponse> getAllQuesByQuizIdAsync(myWebAspClient.student_Report_WCFLib.getAllQuesByQuizIdRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getAllQuizByStudentId", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class getAllQuizByStudentIdRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public int studentId;
        
        public getAllQuizByStudentIdRequest() {
        }
        
        public getAllQuizByStudentIdRequest(int studentId) {
            this.studentId = studentId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getAllQuizByStudentIdResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class getAllQuizByStudentIdResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Data.DataSet getAllQuizByStudentIdResult;
        
        public getAllQuizByStudentIdResponse() {
        }
        
        public getAllQuizByStudentIdResponse(System.Data.DataSet getAllQuizByStudentIdResult) {
            this.getAllQuizByStudentIdResult = getAllQuizByStudentIdResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getAllQuesByQuizId", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class getAllQuesByQuizIdRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public int studentId;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public int quizId;
        
        public getAllQuesByQuizIdRequest() {
        }
        
        public getAllQuesByQuizIdRequest(int studentId, int quizId) {
            this.studentId = studentId;
            this.quizId = quizId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getAllQuesByQuizIdResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class getAllQuesByQuizIdResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Data.DataSet getAllQuesByQuizIdResult;
        
        public getAllQuesByQuizIdResponse() {
        }
        
        public getAllQuesByQuizIdResponse(System.Data.DataSet getAllQuesByQuizIdResult) {
            this.getAllQuesByQuizIdResult = getAllQuesByQuizIdResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IStudentReportChannel : myWebAspClient.student_Report_WCFLib.IStudentReport, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class StudentReportClient : System.ServiceModel.ClientBase<myWebAspClient.student_Report_WCFLib.IStudentReport>, myWebAspClient.student_Report_WCFLib.IStudentReport {
        
        public StudentReportClient() {
        }
        
        public StudentReportClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public StudentReportClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StudentReportClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StudentReportClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        myWebAspClient.student_Report_WCFLib.getAllQuizByStudentIdResponse myWebAspClient.student_Report_WCFLib.IStudentReport.getAllQuizByStudentId(myWebAspClient.student_Report_WCFLib.getAllQuizByStudentIdRequest request) {
            return base.Channel.getAllQuizByStudentId(request);
        }
        
        public System.Data.DataSet getAllQuizByStudentId(int studentId) {
            myWebAspClient.student_Report_WCFLib.getAllQuizByStudentIdRequest inValue = new myWebAspClient.student_Report_WCFLib.getAllQuizByStudentIdRequest();
            inValue.studentId = studentId;
            myWebAspClient.student_Report_WCFLib.getAllQuizByStudentIdResponse retVal = ((myWebAspClient.student_Report_WCFLib.IStudentReport)(this)).getAllQuizByStudentId(inValue);
            return retVal.getAllQuizByStudentIdResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<myWebAspClient.student_Report_WCFLib.getAllQuizByStudentIdResponse> myWebAspClient.student_Report_WCFLib.IStudentReport.getAllQuizByStudentIdAsync(myWebAspClient.student_Report_WCFLib.getAllQuizByStudentIdRequest request) {
            return base.Channel.getAllQuizByStudentIdAsync(request);
        }
        
        public System.Threading.Tasks.Task<myWebAspClient.student_Report_WCFLib.getAllQuizByStudentIdResponse> getAllQuizByStudentIdAsync(int studentId) {
            myWebAspClient.student_Report_WCFLib.getAllQuizByStudentIdRequest inValue = new myWebAspClient.student_Report_WCFLib.getAllQuizByStudentIdRequest();
            inValue.studentId = studentId;
            return ((myWebAspClient.student_Report_WCFLib.IStudentReport)(this)).getAllQuizByStudentIdAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        myWebAspClient.student_Report_WCFLib.getAllQuesByQuizIdResponse myWebAspClient.student_Report_WCFLib.IStudentReport.getAllQuesByQuizId(myWebAspClient.student_Report_WCFLib.getAllQuesByQuizIdRequest request) {
            return base.Channel.getAllQuesByQuizId(request);
        }
        
        public System.Data.DataSet getAllQuesByQuizId(int studentId, int quizId) {
            myWebAspClient.student_Report_WCFLib.getAllQuesByQuizIdRequest inValue = new myWebAspClient.student_Report_WCFLib.getAllQuesByQuizIdRequest();
            inValue.studentId = studentId;
            inValue.quizId = quizId;
            myWebAspClient.student_Report_WCFLib.getAllQuesByQuizIdResponse retVal = ((myWebAspClient.student_Report_WCFLib.IStudentReport)(this)).getAllQuesByQuizId(inValue);
            return retVal.getAllQuesByQuizIdResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<myWebAspClient.student_Report_WCFLib.getAllQuesByQuizIdResponse> myWebAspClient.student_Report_WCFLib.IStudentReport.getAllQuesByQuizIdAsync(myWebAspClient.student_Report_WCFLib.getAllQuesByQuizIdRequest request) {
            return base.Channel.getAllQuesByQuizIdAsync(request);
        }
        
        public System.Threading.Tasks.Task<myWebAspClient.student_Report_WCFLib.getAllQuesByQuizIdResponse> getAllQuesByQuizIdAsync(int studentId, int quizId) {
            myWebAspClient.student_Report_WCFLib.getAllQuesByQuizIdRequest inValue = new myWebAspClient.student_Report_WCFLib.getAllQuesByQuizIdRequest();
            inValue.studentId = studentId;
            inValue.quizId = quizId;
            return ((myWebAspClient.student_Report_WCFLib.IStudentReport)(this)).getAllQuesByQuizIdAsync(inValue);
        }
    }
}