var dataTable;

$(document).ready(() => {
    cargarDatatable();
});

function cargarDatatable() {
    dataTable = $("#booksTable").dataTable({
        "ajax": {
            "url": "/Books/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "bookId", "width": "5%" },
            { "data": "title", "width": "13%" },
            { "data": "year", "width": "13%" },
            { "data": "genre", "width": "13%" },
            { "data": "pages", "width": "13%" },
            { "data": "author", "width": "13%" },
            {
                "data": "bookId",
                "render": (data) => `<div class = "text-center">
                                        <a href="/Books/Edit/${data}" class="btn btn-success text-white" style="cursor: pointer; width: 100px;">
                                            <i class="fas fa-edit"></i> Editar
                                        </a>
                                        &nbsp;
                                        <a onclick=Delete("/Books/Delete/${data}") class="btn btn-danger text-white" style="cursor: pointer; width: 100px;">
                                            <i class="fas fa-trash-alt"></i> Borrar
                                        </a>
                                    </div>`,
                "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "No hay registros"
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Esta seguro de borrar?",
        text: "Este contenido no se puede recuperar!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, borrar!",
        closeOnconfirm: true
    }, async function () {
        const response = await fetch(url, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        const data = await response.json();
        if (data.success) {
            toastr.success(data.message);
            dataTable.api().ajax.reload()
        }
        else
            toastr.error(data.message)
    });


}