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
                            <a class="btn btn-primary" type="button" href="/admin/product/savedata?id=${key}">
                                Sửa
                            </a> &nbsp 
                            <a href="#" class="btn btn-danger btn-delete" type="button">
                                Xóa
                            </a>
                         </span>`
            }
        }
    ];
    registerDataTable(elementName, columns, urlApi)


    $(document).on('click', '.btn-delete', function () {
        const key = $(this).closest('span').data('key')
        $('#del-product-modal').modal('show');
        $(document).on('click', '.del-category', function () {
            $.ajax({
                url: `/admin/product/delete/${key}`,
                dataType: 'json',
                type: 'POST',
                success: function () {
                    $(elementName).DataTable().ajax.reload();
                }
            })
            $('#del-product-modal').modal('hide');
            showToaster('Success', 'Xóa sản phẩm thành công')
        })
    })
})();