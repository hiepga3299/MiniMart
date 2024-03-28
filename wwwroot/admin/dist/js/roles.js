(function () {
    const elementName = '#tbl-role';
    const urlApi = '/admin/role/getrolepagination';
    const columns = [
        { data: 'name', name: 'name' },
        {
            data: 'id', name: 'id', width: 200, render: function (key) {
                return `<span data-key="${key}">
                            <a href="#" class="btn-edit">
                                <i class="fas fa-pen"></i>
                            </a> &nbsp 
                            <a href="#">
                                <i class="fas fa-trash"></i>
                            </a>
                         </span>`
            }
        }
    ];
    registerDataTable(elementName, columns, urlApi)

    $(document).on('click', '#btn-save', function () {
        $('#role-modal').modal('show');
    });

    $('#formRole').submit(function (e) {
        e.preventDefault();

        var data = $(this).serialize();
        $.ajax({
            url: $(this).attr('action'),
            method: $(this).attr('method'),
            data: data,
            success: function (response) {
                $(elementName).DataTable().ajax.reload();
                $('#role-modal').modal('hide');
            }
        })
    })
})()