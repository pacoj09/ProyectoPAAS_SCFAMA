<%@ Page Title="" Language="C#" MasterPageFile="~/PAAS.Master" AutoEventWireup="true" CodeBehind="Project.aspx.cs" Inherits="PAAS_SCFAMA.Project" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPAASHead" runat="server">

    <script type="text/javascript">
        $('#myTab a').click(function (e) {
            e.preventDefault()
            $(this).tab('show')
        })


        function validarInhabilitar() {
            if (document.getElementById("ContentPlaceHolderPAAS_txtIdProyecto").value === '') {
                alert("Debe seleccionar un proyecto para inhabilitar");
                return false;
            } else {
                var messange = confirm("Desea Inhabilitar este Proyecto");
                if (messange === true) {
                    return true;
                } else {
                    return false;
                }
            }
        }

        function validarEliminacionTarea() {
            if (document.getElementById("ContentPlaceHolderPAAS_txtIdTarea").value === '') {
                alert("Debe seleccionar una tarea para eliminar");
                return false;
            } else {
                var messange = confirm("Desea eliminar esta Tarea");
                if (messange === true) {
                    return true;
                } else {
                    return false;
                }
            }
        }

        function validarID_Proyecto() {
            if (document.getElementById("ContentPlaceHolderPAAS_txtIdProyecto_Tarea").value === '') {
                alert("Debe de digitar un Id de proyecto");
                return false;
            }
            return true;
        }

        function stayTabWork() {
            document.getElementById("works").className = "tab-pane active";
            document.getElementById("tabWorks").className = "active";
            document.getElementById("projects").className = "tab-pane";
            document.getElementById("tabProjects").className = "";
        }

        function validarCamposDetalles() {
            if (document.getElementById("ContentPlaceHolderPAAS_txtMontoRecurso").value === '') {
                alert("Debe llenar todos los campos del detalle");
                return false;
            } else if (document.getElementById("ContentPlaceHolderPAAS_txtCantidadRecurso").value === '') {
                alert("Debe llenar todos los campos del detalle");
                return false;
            }
            return true;
        }

    </script>

</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderPAAS" runat="server">
    <script type="text/javascript">
        document.getElementById("lProyectos").className = "current_page_item";
    </script>

    <div align="center">
        <div class="col-md-5">
            <br />
            <div role="tabpanel">

                <ul id="myTab" class="nav nav-tabs" role="tablist">
                    <li id="tabProjects" role="presentation" class="active"><a href="#projects" aria-controls="projects" data-toggle="tab" role="tab">Proyectos</a></li>
                    <li id="tabWorks" role="presentation"><a href="#works" aria-controls="works" data-toggle="tab" role="tab">Tareas</a></li>
                </ul>

                <div class="tab-content">
                    <div role="tabpanel" class="tab-pane active" id="projects">
                        <h1>Proyectos</h1>
                        <br />
                        <fieldset>
                            <legend>Datos del Proyecto:</legend>
                            <asp:Button ID="btnNuevoProyecto" runat="server" Text="Nuevo Proyecto" CssClass="btn btn-default" OnClick="btnNuevoProyecto_Click" />
                            <br />
                            <br />
                            ID Proyecto:<br>
                            <asp:TextBox ID="txtIdProyecto" runat="server" Enabled="false"></asp:TextBox>
                            <br>
                            C&oacute;digo:<br>
                            <asp:TextBox ID="txtCodgioProyecto" runat="server" Enabled="false"></asp:TextBox>
                            <br>
                            Usuario Creador:<br>
                            <asp:TextBox ID="txtUsuarioCreador" runat="server" Enabled="false"></asp:TextBox>
                            <br>
                            Ultimo Usuario en Modificar:<br>
                            <asp:TextBox ID="txtUsuarioModificador" runat="server" Enabled="false"></asp:TextBox>
                            <br>
                            Descripci&oacute;n:<br>
                            <asp:TextBox ID="txtDescripcionProyecto" runat="server" TextMode="MultiLine" Height="125px" Width="350px"></asp:TextBox>
                            <br>
                            Costo:<br>
                            <asp:TextBox ID="txtCostoProyecto" runat="server" Enabled="false"></asp:TextBox>
                            <br>
                            Tiempo:<br>
                            <asp:TextBox ID="txtTiempoTotal" runat="server" Enabled="false"></asp:TextBox>
                            <br>
                            Estado:<br>
                            <asp:TextBox ID="txtEstado" runat="server"></asp:TextBox>
                            <br>
                            <asp:Button ID="btnActualizarProyecto" runat="server" Text="Actualizar Proyecto" OnClick="btnActualizarProyecto_Click" CssClass="btn btn-default" />
                            &nbsp;	&nbsp;
                            <asp:Button ID="btnInhabilitarProyecto" runat="server" Text="Inhabilitar Proyecto" OnClientClick="return validarInhabilitar();" OnClick="btnInhabilitarProyecto_Click" CssClass="btn btn-default" />
                        </fieldset>
                        <br />
                        <br />
                        <div style="width: 80%; height: 300px; overflow: scroll">
                            <asp:GridView ID="gvProyectos" runat="server" OnSelectedIndexChanged="gvProyectos_SelectedIndexChanged" OnRowDataBound="gvProyectos_RowDataBound">
                                <Columns>
                                    <asp:CommandField CausesValidation="False" InsertVisible="False" ShowCancelButton="False" ShowSelectButton="True" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                    <div role="tabpanel" class="tab-pane" id="works">
                        <h1>Tareas</h1>
                        <br />
                        <p>Tareas por Proyecto:</p>
                        <br>
                        <asp:TextBox ID="txtIdProyecto_Tarea" runat="server"></asp:TextBox>
                        &nbsp;	&nbsp;
                        <asp:Button ID="btnBuscarTareas" runat="server" Text="Buscar Tareas" CssClass="btn btn-default" OnClick="btnBuscarTareas_Click" OnClientClick="validarID_Proyecto();" />
                        <br />
                        <br />
                        <div style="width: 80%; height: 300px; overflow: scroll">
                            <asp:GridView ID="gvTareas" runat="server" OnSelectedIndexChanged="gvTareas_SelectedIndexChanged">
                                <Columns>
                                    <asp:CommandField CausesValidation="False" InsertVisible="False" ShowCancelButton="False" ShowSelectButton="True" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <fieldset>
                            <legend>Datos de Tarea:</legend>
                            <asp:Button ID="btnNuevaTarea" runat="server" Text="Nueva Tareas" Enabled="false" CssClass="btn btn-default" OnClick="btnNuevaTarea_Click" />
                            <br />
                            ID Tarea:<br>
                            <asp:TextBox ID="txtIdTarea" runat="server" Enabled="false"></asp:TextBox>
                            <br>
                            C&oacute;digo:<br>
                            <asp:TextBox ID="txtCodigoTarea" runat="server" Enabled="false"></asp:TextBox>
                            <br>
                            Proyecto:<br>
                            <asp:TextBox ID="txtProyecto" runat="server" Enabled="false"></asp:TextBox>
                            <br>
                            Descripci&oacute;n:<br>
                            <asp:TextBox ID="txtDescripcionTarea" runat="server" TextMode="MultiLine" Height="125px" Width="350px"></asp:TextBox>
                            <br>
                            Tiempo:<br>
                            <asp:TextBox ID="txtTiempo" runat="server"></asp:TextBox>
                            <br>
                            Costo:<br>
                            <asp:TextBox ID="txtCostoTarea" runat="server" Enabled="false"></asp:TextBox>
                            <br>
                            <asp:Button ID="btnActualizarTarea" runat="server" Text="Actualizar Tarea" OnClick="btnActualizarTarea_Click" Enabled="false" CssClass="btn btn-default" />
                            &nbsp;	&nbsp;
                            <asp:Button ID="btnEliminarTarea" runat="server" Text="Eliminar Tarea" OnClick="btnEliminarTarea_Click" OnClientClick="return validarEliminacionTarea();" Enabled="false" CssClass="btn btn-default" />
                            <br />
                            <hr />
                            <h3>Detalles de Tarea</h3>
                            <br />
                            <br>
                            Recurso:<br>
                            <asp:DropDownList ID="ddlRecursos" runat="server"></asp:DropDownList>
                            <br>
                            Monto:<br>
                            <asp:TextBox ID="txtMontoRecurso" runat="server"></asp:TextBox>
                            <br>
                            Cantidad:<br>
                            <asp:TextBox ID="txtCantidadRecurso" runat="server"></asp:TextBox>
                            <br>
                            <asp:Button ID="btnAgregarDetalle" runat="server" Text="Agregar Detalle" Enabled="false" CssClass="btn btn-default" OnClick="btnAgregarDetalle_Click" OnClientClick="return validarCamposDetalles();" />
                            <br />
                            <br />
                            <div style="width: 80%; height: 300px; overflow: scroll">
                                <asp:GridView ID="gvDetallesTarea" runat="server" OnSelectedIndexChanged="gvDetallesTarea_SelectedIndexChanged">
                                    <Columns>
                                        <asp:CommandField CausesValidation="False" InsertVisible="False" ShowCancelButton="False" ShowSelectButton="True" SelectText="Eliminar" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </fieldset>
                    </div>

                </div>

            </div>
        </div>
    </div>
</asp:Content>
