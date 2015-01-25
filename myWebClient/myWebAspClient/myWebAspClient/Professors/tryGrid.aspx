<%@ Page Title="" Language="C#" MasterPageFile="~/Professors/professor.Master" AutoEventWireup="true" CodeBehind="tryGrid.aspx.cs" Inherits="myWebAspClient.Professors.tryGrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblAnswer" runat="server" ></asp:Label>

    <input type="date" runat="server" id="txtDate" />

    <input type="time" runat="server" id="txtTime" />

    <asp:Button ID="btnClick" runat ="server" Text="Click" OnClick="btnClick_Click" />
    
        <input id="txtForm1" type="text" runat="server"  />
        <asp:Button ID="btnForm1" runat="server" Text="Form1" OnClick="btnForm1_Click" /> 
     
    <div id="myQues">
        <div class="form-group">
        <input id="txtForm2" type="text" runat="server" class="form-control"  />
        <input id="Text1" type="text" runat="server" class="form-control"  />
        <input id="Text2" type="text" runat="server" class="form-control"  />

        
        <asp:Button ID="btnForm2" runat="server" Text="Form2" OnClick="btnForm2_Click" OnClientClick="return quesCheck()" /> 
        </div> 
        </div> 
    <script type="text/javascript">
        function quesCheck()
        {
            //alert("ts");
            //var matches = [];
            var searchEles = document.getElementById("myQues").children;
            var q = 0;
            for (var i = 0; i < searchEles.length; i++)
            {
                
                if (searchEles[i].tagName == 'INPUT' && searchEles[i].getAttribute("type")== 'text')
                {
                    if (searchEles[i].value.toString().trim() == "")
                    {
                        searchEles[i].className = "alert alert-danger";
                        searchEles[i].setAttribute("placeholder", "Required");
                        q = 1;
                        
                    }

                }
            }
            if (q) return false;

        }
    </script>
</asp:Content>
