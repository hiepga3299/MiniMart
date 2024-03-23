(function () {
    const elementName = '#tbl-category';
    const urlApi = '/admin/category/getcategorypagination';
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
        $('#category-modal').modal('show');
    });

    $(document).on('click', '.btn-edit', function () {
        $('#category-modal').modal('show');
        var key = $(this).closest('span').data('key')
        $.ajax({
            url: `/admin/category/getbyid?id=${key}`,
            method: 'GET',
            success: function (response) {
                mapObjectToControllerView(response);
            }
        })
    })

    $('#formCategory').submit(function (e) {
        e.preventDefault();

        var data = $(this).serialize();
        $.ajax({
            url: $(this).attr('action'),
            method: $(this).attr('method'),
            data: data,
            success: function (response) {
                $(elementName).DataTable().ajax.reload();
                $('#category-modal').modal('hide');
            }
        })
    })
})()