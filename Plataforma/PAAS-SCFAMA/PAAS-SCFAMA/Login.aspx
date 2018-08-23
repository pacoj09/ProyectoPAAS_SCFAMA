<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="~/Login.aspx.cs" Inherits="PAAS_SCFAMA.Login" MaintainScrollPositionOnPostback="true" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderSiteHead" runat="server">
    <script type="text/javascript">
        function ValidarFormulario() {
            if (document.getElementById("ContentPlaceHolderSite_txtEmpresa").value === '') {
              alert("Debe de digitar el Nombre de su Empresa para poder ingresar");
              return false;
          } else if (document.getElementById("ContentPlaceHolderSite_txtUser").value === '') {
              alert("Debe de digitar su Usuario para poder ingresar");
              return false;
          } else if (document.getElementById("ContentPlaceHolderSite_txtPassword").value === '') {
              alert("Por favor digite el Contraseña para poder ingresar");
              return false;
          }
            return true;
      }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderSite" runat="server">

    <script type="text/javascript">
        document.getElementById("lSesion").className = "current_page_item";
    </script>

    <br />
    <div align="center">
        
        <h1>Inicio Sesi&oacute;n</h1>
        <br />
        <fieldset>
            <legend>Informaci&oacute;n de Usuario:</legend>
            Nombre de Empresa:<br>
            <asp:TextBox ID="txtEmpresa" runat="server" Width="250px"></asp:TextBox>
            <br>
            Nombre de Usario:<br>
            <asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
            <br>
            Contrase&ntilde;a:<br>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            <br>
            <br>
            <asp:Button ID="btnInicio" runat="server" Text="Iniciar Sesi&oacute;n" OnClick="btnInicio_Click" OnClientClick="return ValidarFormulario()" />
        </fieldset>
        <br />
        <br />
        <p>NOTA: Para poder obtener acceso a la plataforma pongase en contacto con nosostros</p>
    </div>





</asp:Content>
