(function () {
    function initial() {
        loadDataChartOrder();
        loadTotalAmount();
        registerEvents();
    }

    function loadDataChartOrder() {
        $.ajax({
            url: '/admin/dashboard/getchartdatabyproduct',
            method: 'GET',
            success: function (response) {
                if (!response) {
                    return;
                }
                initialChartOrder(response);
            }
        })
    }


    function loadTotalAmount() {
        $.ajax({
            url: '/admin/dashboard/gettotalamount',
            method: 'GET',
            success: function (res) {
                if (res) {
                    $('#total-amount').text(res.amount.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' }));
                    $('#total-order').text(res.order);
                } else {
                    $('#total-amount').text('Chưa có doanh thu')
                }

            }
        })
    }

    function initialChartOrder(dataSource) {
        var chartDom = document.getElementById('main');
        var myChart = echarts.init(chartDom);
        var option;

        option = {
            title: {
                text: 'Dữ liệu Sản Phẩm',
                left: 'center',
            },
            tooltip: {
                trigger: 'item'
            },
            legend: {
                orient: 'vertical',
                left: 'left'
            },
            series: [
                {
                    name: 'Access From',
                    type: 'pie',
                    radius: '70%',
                    data: dataSource,
                    emphasis: {
                        itemStyle: {
                            shadowBlur: 10,
                            shadowOffsetX: 0,
                            shadowColor: 'rgba(0, 0, 0, 0.5)'
                        }
                    }
                }
            ]
        };

        option && myChart.setOption(option);
    }
    function registerEvents() {

    }

    $(document).ready(function () {
        initial();
    })

})();