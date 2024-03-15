function registerDataTable(elementName, columns, urlApi) {
    $(elementName).DataTable({
        processing: true,
        serverSide: true,
        columns: columns,
        ajax: {
            url: urlApi,
            type: 'POST',
            dataType: 'json'
        }
    });
}