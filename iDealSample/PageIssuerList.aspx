<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PageIssuerList.aspx.cs"
    Inherits="ING.iDealSample.PageIssuerList" %>
<%@ Register Assembly="iDealSample"  Namespace="ING.iDealSample.Custom"  TagPrefix="iDeal"  %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div>
        <asp:Panel ID="PanelFuncParms" runat="server" Width="800px" BorderStyle="Solid" Font-Bold="False">
            Functie-parameters:<br />
            (none)
        </asp:Panel>
        &nbsp;
        <asp:Panel ID="PanelConfParms" runat="server" Width="800px" BorderStyle="Solid">
            Vanuit configuratie:<br />
            <asp:Label ID="LabelMerchantIdText" runat="server" Text="Merchant Id:" Width="151px"></asp:Label>
            <asp:Label ID="LabelMerchantIdValue" runat="server" Width="351px"></asp:Label>
            <br />
            <asp:Label ID="LabelSubIdText" runat="server" Text="Sub Id:" Width="151px"></asp:Label>
            <asp:Label ID="LabelSubIdValue" runat="server" Width="351px"></asp:Label>
            <br />
            <asp:Label ID="LabelAcquirerUrlText" runat="server" Text="Acquirer Url:" Width="151px"></asp:Label>
            <asp:Label ID="LabelAcquirerUrlValue" runat="server" Width="351px"></asp:Label>
        </asp:Panel>
        &nbsp;
        <asp:Panel ID="PanelAction" runat="server" Width="800px" BorderStyle="Solid" HorizontalAlign="Center">
            <asp:Button ID="ButtonGetIssuerList" runat="server" OnClick="ButtonGetIssuerList_Click"
                Text="Get Issuer List" />
        </asp:Panel>
        &nbsp;
        <asp:Panel ID="PanelResult" runat="server" Width="800px" BorderStyle="Solid">
            Resultaat:<br />
            <asp:Label ID="LabelDateTimeStampText" runat="server" Text="DateTime:" Width="151px"></asp:Label>
            <asp:Label ID="LabelDateTimeStampValue" runat="server" Width="351px"></asp:Label>
            <br />
            <asp:Label ID="LabelIssuerList" runat="server" Text="Issuer List:" Width="151px"></asp:Label>
            
            <iDeal:GroupedDropDownList ID="DropDownListIssuers" runat="server" Width="200">
            </iDeal:GroupedDropDownList>
            <asp:RegularExpressionValidator ID="RegularExpressionIssuerList" runat="server" ControlToValidate="DropDownListIssuers"
                ErrorMessage="RegularExpressionValidator" ValidationExpression="[A-Z]{6,6}[A-Z2-9][A-NP-Z0-9]([A-Z0-9]{3,3}){0,1}"
                Display="Dynamic">Format for issuer id not correct.</asp:RegularExpressionValidator>
        </asp:Panel>
        &nbsp;
        <asp:Panel ID="PanelNext" runat="server" Width="800px" BorderStyle="Solid" HorizontalAlign="center">
            <asp:Label ID="LabelErrorValue" runat="server" Width="800px" />
            <asp:Button ID="ButtonTrxReq" runat="server" Enabled="False" OnClick="ButtonTrxReq_Click"
                Text="Transaction Request" />
        </asp:Panel>
    </div>
   
</asp:Content>
