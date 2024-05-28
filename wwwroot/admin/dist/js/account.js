(function () {
    const elementName = '#tbl-account';
    const urlApi = '/admin/user/getaccountpagination';
    const columns = [
        { data: 'username', name: 'username' },
        { data: 'fullname', name: 'fullname' },
        { data: 'email', name: 'email' },
        { data: 'phone', name: 'phone' },
        { data: 'isActive', name: 'isActive' },
        {
            data: 'id', name: 'id', render: function (key) {
                return `<span data-key="${key}">
                            <a class="btn btn-primary" type="button" href="/admin/user/savedata?id=${key}">Sửa
                            </a> &nbsp 
                            <a href="#" class="btn btn-danger" type="button">
                                Xóa
                            </a> &nbsp 
                            <a href="#" type="button" class="btn-disable btn btn-warning">
                                Khóa
                            </a>
                         </span>`
            }
        }
    ];
    registerDataTable(elementName, columns, urlApi)
    $(document).on('click', '.btn-disable', function () {
        const idUser = $(this).closest('span').data('key');
        $.ajax({
            url: `/admin/user/disableaccount/${idUser}`,
            dataType: 'json',
            type: 'POST',
            success: function (res) {
                $(elementName).DataTable().ajax.reload();
            }
        })
    });
})();