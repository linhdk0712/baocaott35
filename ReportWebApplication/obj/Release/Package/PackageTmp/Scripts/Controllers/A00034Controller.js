var A00034Controller = {
    init: function () {
        A00034Controller.registerEvent();
    },
    registerEvent: function () {
        $('#btnGetData').off('click').on('click', function () {
            A00034Controller.loadData();
        })
        $('#btnCreateReport').off('click').on('click', function () {
            A00034Controller.printData();
        })
    },
    loadData: function () {
        var machinhanh = $('#danhmuc_chinhanh option:selected').val();
        var ngaydulieu = $('input[type="date"]').val();
        $.ajax({
            url: "/A00034/GetData",
            type: "GET",
            dataType: 'json',
            data: {
                maChiNhanh: machinhanh,
                denNgay: ngaydulieu
            },
            success: function (response) {
                if (response.status) {
                    var results = response.data;
                    var html = '';
                    var template = $('#template').html();
                    $.each(results, function (i, item) {
                        html += Mustache.render(template, {
                            MUC_DICH_VAY: item.MUC_DICH_VAY,
                            TGIAN_VAY: item.TGIAN_VAY,
                            SO_DU: item.SO_DU,
                            LAI_DU_THU: item.LAI_DU_THU
                        });
                    });
                    $('#tblData').html(html);
                }
            }
        })
    },
    printData: function () {
        var machinhanh = $('#danhmuc_chinhanh option:selected').val();
        var ngaydulieu = $('input[type="date"]').val();
        $.ajax({
            cache: false,
            url: "/A00034/GetXLSXReport",
            //type: "GET",
            dataType: 'json',
            data: {
                maChiNhanh: machinhanh,
                denNgay: ngaydulieu
            },
            success: function (response) {
                window.location = '/A00034/DownloadFile?fileGuid=' + response.FileGuid + '&fileName=' + response.FileName;
            }
        })
    }
}
A00034Controller.init();