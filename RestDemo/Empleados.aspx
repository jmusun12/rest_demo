<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Empleados.aspx.cs" Inherits="RestDemo.Empleados" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-lg-12 col-sm-12 col-12">
                <h3>Listado de empleado</h3>
            </div>
        </div>

        <div class="row">
            <div class="col-lg-12 col-sm-12" style="margin-bottom: 1rem !important;">
                <asp:Button ID="btnNuevoEmpleado" runat="server" OnClick="btnNuevoEmpleado_Click" Text="Nuevo empleado" CssClass="btn btn-primary" />
            </div>
        </div>

        <div class="row">
            <div class="col-12 col-lg-12 col-sm-12">
                <asp:GridView ID="gvEmpleados" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="gvEmpleados_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" />
                        <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                        <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" />
                        <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                        <asp:BoundField DataField="Correo" HeaderText="Correo Electrónico" />
                        <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha de Nacimiento"/>
                        <asp:BoundField DataField="Salario" HeaderText="Sueldo" />
                        <asp:ButtonField CommandName="Editar" Text="Editar" HeaderText="Editar" ControlStyle-CssClass="btn btn-info" />
                        <asp:ButtonField CommandName="Eliminar" Text="Eliminar" HeaderText="Eliminar" ControlStyle-CssClass="btn btn-warning" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <div class="modal fade" id="detailModal" tabindex="-1" role="dialog" aria-labelledby="detailModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="detailModalLabel">Crear o editar empleado</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="txbId" runat="server" Value="0" />
                    <div class="row">
                        <div class="col-lg-6 col-md-6">
                            <div class="form-group">
                                <label>Nombres (*): </label>
                                <asp:TextBox ID="txbNombres" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                        </div>

                        <div class="col-lg-6 col-md-6">
                            <div class="form-group">
                                <label>Apellidos (*): </label>
                                <asp:TextBox ID="txbApellidos" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-lg-6 col-md-6">
                            <div class="form-group">
                                <label>Teléfono (*): </label>
                                <asp:TextBox ID="txbTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-lg-6 col-md-6">
                            <div class="form-group">
                                <label>Correo electrónico (*): </label>
                                <asp:TextBox ID="txbCorreo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-6 col-md-6">
                            <div class="form-group">
                                <label>Fecha de Nacimiento (*): </label>
                                <asp:TextBox ID="txbFechaNa" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6 col-md-6">
                            <div class="form-group">
                                <label>Salario mensual (*): </label>
                                <asp:TextBox ID="txbSalario" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row" style="display:none;" id="validation-form">
                        <div class="col-lg-12">
                            <div class="alert alert-warning" role="alert">
                                Todos los campos son obligatorios(*)
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardarEmpleado" runat="server" Text="Guardar datos" CssClass="btn btn-primary btn-send-form" OnClick="btnGuardarEmpleado_Click" />
                    <button type="button" id="btn-validate" class="btn btn-primary" onclick="validateForm()">Guardar datos</button>
                </div>
            </div>
        </div>
    </div>


    <!-- Modal delete -->
    <div class="modal fade" id="deleteModal" tabindex="-1" role="dialog" aria-labelledby="deleteModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="deleteModalLabel">Eliminar registro</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="txbEmpleadoId" runat="server" Value="0" />
                    <div class="alert alert-warning" role="alert">
                        <p class="text-delete"></p>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnEliminarEmpleado" runat="server" Text="Si, eliminar" CssClass="btn btn-primary" OnClick="btnEliminarEmpleado_Click" />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(() => {
            $('#MainContent_btnGuardarEmpleado').css('display', 'none');
        });

        function openModalNuevoEmpleado(message = "Crear nuevo empleado") {         
            $('#detailModal').modal('show');
        }

        function closeModalEmpleado() {
            $('#detailModal').modal('hide');
        }

        function openDeleteModal(message) {
            $('p.text-delete').text(message);
            $('#deleteModal').modal('show');
        }
        function closeDeleteModal() {
            $('#deleteModal').modal('hide');
        }

        function isNullOrEmpty(valor) {
            return valor === undefined || valor === null || valor === "";
        }

        function validateForm() {
            if (isNullOrEmpty($('input#MainContent_txbNombres').val().trim())
                || isNullOrEmpty($('input#MainContent_txbApellidos').val().trim())
                || isNullOrEmpty($('input#MainContent_txbTelefono').val().trim())
                || isNullOrEmpty($('input#MainContent_txbCorreo').val().trim())
                || isNullOrEmpty($('input#MainContent_txbFechaNa').val().trim())
                || isNullOrEmpty($('input#MainContent_txbSalario').val().trim())) {
                $('#validation-form').css("display","block");
                return false;
            }

            $('#MainContent_btnGuardarEmpleado').click();
        }
    </script>


</asp:Content>
