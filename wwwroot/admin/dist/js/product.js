(function () {
    const elementName = '#tbl-product';
    const urlApi = '/admin/product/getproductpagination';
    const columns = [
        { data: 'name', name: 'name' },
        { data: 'categoryName', name: 'categoryName' },
        { data: 'available', name: 'available' },
        {
            data: 'price', name: 'price', render: function (data) {
                return `<div class="text-left">${data.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' })}</div>`
            }
        },
        {
            data: 'createOn', name: 'createOn', render: function (data) {
                return `<div class="text-left">${(new Date(data)).toLocaleDateString('vi-VN', { day: '2-digit', month: '2-digit', year: 'numeric' })}</div>`
            }
        },
        {
            data: 'id', name: 'id', render: function (key) {
                return `<span data-key="${key}">
                            <a href="/admin/product/savedata?id=${key}">
                                <i class="fas fa-pen"></i>
                            </a> &nbsp 
                            <a href="#" class="btn-delete">
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
        const data = $(this).closest('span').data('key')
        console.log(typeof data)
    })

    $(document).on('click', '.btn-delete', function () {
        const key = $(this).closest('span').data('key')
        console.log(key)
        $.ajax({
            url: `/admin/product/delete/${key}`,
            dataType: 'json',
            type: 'POST',
            success: function () {
                $(elementName).DataTable().ajax.reload();
            }
        })
    })
})();