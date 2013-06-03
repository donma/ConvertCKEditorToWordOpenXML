<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="demo.aspx.cs" Inherits="SimpleConvetCKEditorHTMLToOpenXML.demo" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/ckeditor/ckeditor.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <CKEditor:CKEditorControl ID="CKEditorControl1" runat="server"></CKEditor:CKEditorControl>
        </div>
        <asp:Button ID="btnConvertToWord" runat="server" OnClick="btnConvertToWord_Click" Text="轉Word." />

        <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
    </form>
</body>
</html>
