(function () {
    function initial() {
        getOrderDetail()
    }

    function getOrderDetail() {
        var currentUrl = window.location.href;
        var queryString = currentUrl.split('?')[1];

        var param = new URLSearchParams(queryString);
        var orderId = param.get('orderId');

        $.ajax({
            url: `/admin/order/getorderdetail?orderId=${orderId}`,
            type: 'GET',
            success: function (response) {
                var orderDetailObj = {}
                $.each(response, function (index, obj) {
                    var key = obj.fullName + '-' + obj.addressName + '-' + obj.city + '-' + obj.region;
                    if (!orderDetailObj[key]) {
                        orderDetailObj[key] = {
                            fullName: obj.fullName,
                            addressName: obj.addressName,
                            city: obj.city,
                            region: obj.region,
                            products: []
                        };
                    }
                    orderDetailObj[key].products.push({
                        productName: obj.productName,
                        price: obj.price,
                        code: obj.code,
                        quantity: obj.quantity
                    });
                })
                var groupedArray = [];
                $.each(orderDetailObj, function (key, obj) {
                    groupedArray.push(obj);
                });
                var tbody = $("#result tbody");
                $.each(groupedArray, function (index, obj) {
                    var rowSpan = obj.products.length;
                    $.each(obj.products, function (i, product) {
                        var tr = $("<tr>");
                        if (i === 0) {
                            tr.append("<td rowspan='" + rowSpan + "'>" + obj.fullName + "</td>");
                            tr.append("<td rowspan='" + rowSpan + "'>" + obj.addressName + ", " + obj.city + ", " + obj.region + "</td>");
                        }
                        tr.append("<td>" + product.productName + "</td>");
                        tr.append("<td>" + product.price + "</td>");
                        tr.append("<td>" + product.code + "</td>");
                        tr.append("<td>" + product.quantity + "</td>");
                        tbody.append(tr);
                    });
                });
            }
        })
    }

    initial()
})()