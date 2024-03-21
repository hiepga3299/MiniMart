(function () {
    const elementName = '#tbl-category';
    const urlApi = '/admin/category/getcategorypagination';
    const columns = [
        { data: 'name', name: 'name' },
        {
            data: 'id', name: 'id', width: 200, render: function (key) {
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
})()