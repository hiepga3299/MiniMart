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
                            <a href="/admin/user/savedata?id=${key}">
                                <i class="fas fa-pen"></i>
                            </a> &nbsp 
                            <a href="#">
                                <i class="fas fa-trash"></i>
                            </a> &nbsp 
                            <a href="#" class="btn-disable">
                                <i class="fas fa-user-alt-slash"></i>
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