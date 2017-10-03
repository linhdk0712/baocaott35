var homeController = {
    init: function () {
        homeController.registerEvent();
        homeController.loadListFile();
    },

    registerEvent: function () {
        var report = ["001_DBTK", "Báo cáo dư nợ tín dụng theo ngành kinh tế (theo ngành kinh doanh chính của khách hàng)", "A00034", "Báo cáo theo tháng", "002_DBTK", "Báo cáo dư nợ tín dụng theo ngành kinh tế (theo mục đính sử dụng vốn vay đối với từng khoản vay)", "A00044", "Báo cáo theo tháng", "003_DBTK", "Báo cáo dư nợ tín dụng theo loại hình tổ chức và cá nhân", "A00054", "Báo cáo theo tháng", "005_DBTK", "Báo cáo doanh số cấp tín dụng, doanh số thu nợ tín dụng", "A00094", "Báo cáo theo tháng", "006_DBTK", "Báo cáo dư nợ tín dụng; đầu tư trái phiếu doanh nghiệp; cho vay, đầu tư theo hợp đồng nhận ủy thác và lãi suất cho vay đối với các lĩnh vực hỗ trợ ưu tiên phát triển", "A00064", "Báo cáo theo tháng", "013_DBTK", "Báo cáo cho vay xuất, nhập khẩu", "A00104", "Báo cáo theo tháng", "041_DBTK", "Báo cáo huy động vốn từ khách hàng theo ngành kinh tế", "A00014", "Báo cáo theo tháng", "042_DBTK", "Báo cáo huy động vốn từ khách hàng theo loại hình sản phẩm", "A00024", "Báo cáo theo tháng", "046_CSTT", "Báo cáo lãi suất tiền gửi và cho vay bình quân", "B00094", "Báo cáo theo tháng", "047_CSTT", "Báo cáo lãi suất đối với nền kinh tế", "B00084", "Báo cáo theo tháng", "053_TT", "Báo cáo giao dịch thanh toán nội địa phân theo phương tiện thanh toán, phương thức xử lý và các kênh giao dịch thanh toán", "D00024", "Báo cáo theo tháng", "092_TD", "Báo cáo tình hình bán nợ", "C00115", "Báo cáo theo quý", "01_TCVM_TTGS ", "Báo cáo dư nợ cho vay phân theo ngành kinh tế", "G01934", "Báo cáo theo tháng", "02_TCVM_TTGS", "Báo cáo phân loại nợ và trích lập dự phòng rủi ro", "G01204", "Báo cáo theo tháng", "03_TCVM_TTGS", "Báo cáo tình hình trích lập và sử dụng dự phòng để xử lý rủi ro trong hoạt động", "G01215", "Báo cáo theo quý", "04_TCVM_TTGS", "Báo cáo tình hình huy động tiền gửi từ các tổ chức, cá nhân", "G01254", "Báo cáo theo tháng", "05_TCVM_TTGS", "Báo cáo tình hình nhận vốn tài trợ, vốn uỷ thác từ các tổ chức, cá nhân, Chính phủ và ủy thác cho các tổ chức khác", "G01264", "Báo cáo theo tháng", "06_TCVM_TTGS", "Báo cáo tình hình vay vốn từ các tổ chức, cá nhân", "G01274", "Báo cáo theo tháng", "07_TCVM_TTGS", "Báo cáo tình hình thực hiện tỷ lệ an toàn vốn riêng lẻ", "G02824", "Báo cáo theo tháng", "08_TCVM_TTGS", "Báo cáo tỷ lệ khả năng chi trả", "G02832", "Báo cáo theo ngày", "09_TCVM_TTGS", "Báo cáo rủi ro lãi suất", "G02845", "Báo cáo theo quý", "09_TCVM-TTGS", "Báo cáo rủi ro lãi suất", "G02867", "Báo cáo theo năm", "09_TCVM_TTGS", "Báo cáo rủi ro lãi suất", "G02887", "Báo cáo theo năm đã kiểm toán", "10_TCVM_TTGS", "Báo cáo rủi ro tiền tệ", "G02905", "Báo cáo theo quý", "10_TCVM_TTGS", "Báo cáo rủi ro tiền tệ", "G02927", "Báo cáo theo năm", "10_TCVM_TTGS", "Báo cáo rủi ro tiền tệ", "G02947", "Báo cáo theo năm đã kiểm toán", "11_TCVM_TTGS", "Báo cáo rủi ro thanh khoản", "G02965", "Báo cáo theo quý", "11_TCVM_TTGS", "Báo cáo rủi ro thanh khoản", "G02987", "Báo cáo theo năm", "11_TCVM_TTGS", "Báo cáo rủi ro thanh khoản", "G03007", "Báo cáo theo năm đã kiểm toán", "12_TCVM_TTGS", "Báo cáo dư nợ cho vay đối với 100 khách hàng lớn nhất không phải khách hàng tài chính vi mô", "G01224", "Báo cáo theo tháng", "13_TCVM_TTGS", "Báo cáo dư nợ cho vay đối với hội đồng thành viên, ban kiểm soát, ban giám đốc và các cán bộ, nhân viên", "G01234", "Báo cáo theo tháng", "14_TCVM_TTGS", "Báo cáo dư nợ cho vay phân theo tài sản đảm bảo đối với khách hàng không phải khách hàng tài chính vi mô", "G01244", "Báo cáo theo tháng", "15_TCVM_TTGS", "Báo cáo mạng lưới hoạt động của TCTC vi mô", "G01285", "Báo cáo theo quý", "16_TCVM_TTGS", "Báo cáo mạng lưới chi nhánh của TCTC vi mô", "G01295", "Báo cáo theo quý", "17_TCVM_TTGS", "Báo cáo về ban quản trị, ban kiểm soát, ban điều hành", "G01915", "Báo cáo theo quý", "17_TCVM_TTGS", "Báo cáo về ban quản trị, ban kiểm soát, ban điều hành", "G01927", "Báo cáo theo năm", "18_TCVM_TTGS", "Bảng cân đối tài khoản kế toán", "G01304", "Báo cáo theo tháng", "19_TCVM_TTGS", "Bảng cân đối kế toán", "G01315", "Báo cáo theo quý", "19_TCVM_TTGS", "Bảng cân đối kế toán", "G01327", "Báo cáo theo năm", "19_TCVM_TTGS", "Bảng cân đối kế toán", "G01337", "Báo cáo theo năm đã kiểm toán", "20_TCVM_TTGS", "Báo cáo kết quả hoạt động kinh doanh", "G01345", "Báo cáo theo quý", "20_TCVM_TTGS", "Báo cáo kết quả hoạt động kinh doanh", "G01357", "Báo cáo theo năm", "20_TCVM_TTGS", "Báo cáo kết quả hoạt động kinh doanh", "G01367", "Báo cáo theo năm đã kiểm toán", "21_TCVM_TTGS", "Báo cáo lưu chuyển tiền tệ", "G01375", "Báo cáo theo quý", "21_TCVM_TTGS", "Báo cáo lưu chuyển tiền tệ", "G01387", "Báo cáo theo năm", "22_TCVM_TTGS", "Báo cáo chi phí hoạt động", "G02005", "Báo cáo theo quý", "22_TCVM_TTGS", "Báo cáo chi phí hoạt động", "G02017", "Báo cáo theo năm", "23_TCVM_TTGS", "Báo cáo tình hình thực hiện nghĩa vụ với ngân sách Nhà nước", "G02035", "Báo cáo theo quý", "23_TCVM_TTGS", "Báo cáo tình hình thực hiện nghĩa vụ với ngân sách Nhà nước", "G02047", "Báo cáo theo năm"];
        var states = new Bloodhound({
            datumTokenizer: Bloodhound.tokenizers.whitespace,
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            // `states` is an array of state names defined in "The Basics"
            local: report
        });
        $(document).ready(function () {
            $(document).ajaxStart(function () {
                $("#wait").css("display", "block");
            });
            $(document).ajaxComplete(function () {
                $("#wait").css("display", "none");
            });
            $(document).off('click').on('click', '.ajaxLink', function () {
                var controller = $(this).data('id');
                homeController.printData(controller);
            });
            $('#checkWorkingDay').off('click').on('click', function () {
                $('#myModal').modal('show');
                homeController.loadListFile();
            });
            $('#datasysn').off('click').on('click', function () {
                var url = '/Home/TongHopDuLieu';
                homeController.tonghopdulieu(url);
            });
            $('#datasysnOfQuater').off('click').on('click', function () {
                var url = '/Home/TongHopDuLieuQuy';
                homeController.tonghopdulieu(url);
            })

            $('#search.typeahead').typeahead({
                hint: true,
                highlight: true,
                minLength: 1
            },
            {
                name: 'states',
                source: states
            });
        });
    },
    printData: function (controllername) {
        var ngaydulieu = $('[name="dateNgayPsinhDlieu"]').val();
        if (!ngaydulieu.length) {
            alert("Phải chọn ngày dữ liệu trước");
        }
        else {
            var controllerName = controllername;
            $.ajax({
                cache: false,
                url: '/' + controllerName + '/GetXLSXReport',
                type: 'GET',
                contentType: 'application/json',
                dataType: 'json',
                data: {
                    denNgay: ngaydulieu
                },
                success: function (response) {
                    if (response.status) {
                        homeController.successAlert(controllerName, response.data);
                        homeController.loadListFile();
                    }
                    else {
                        homeController.errorAlert("Tạo báo cáo không thành công", controllerName);
                    }
                },
                error: function () {
                    homeController.errorAlert("Báo cáo đang trong quá trình xây dựng", null);
                }
            })
        }
    },
    successAlert: function (name, amount) {
        $.bootstrapGrowl("Đã tạo thành công " + amount + " báo cáo " + name);
    },
    successLoad: function () {
        $.bootstrapGrowl("Loaded success");
    },
    errorAlert: function (message, name) {
        $.bootstrapGrowl(message + name, {
            type: 'danger',
            width: 'auto',
            allow_dismiss: false
        });
    },
    loadListFile: function () {
        var url = '/Home/KiemTraNgayLamViec';
        $.ajax({
            url: url,
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var results = response.data;
                    var html = '';
                    var template = $('#template').html();
                    $.each(results, function (i, item) {
                        html += Mustache.render(template, {
                            TEN_GDICH: item.TEN_GDICH,
                            NGAY_LVIEC: item.NGAY_LVIEC,
                        });
                    });
                    $('#workingDay').html(html);
                }
            }
        })
    },
    tonghopdulieu: function (url) {
        var ngaydulieu = $('[name="dateNgayPsinhDlieu"]').val();
        if (!ngaydulieu.length) {
            alert("Phải chọn ngày dữ liệu trước");
        }
        else {
            $.ajax({
                url: url,
                type: 'POST',
                dataType: 'json',
                data: {
                    denNgay: ngaydulieu
                },
                success: function (response) {
                    if (response.status) {
                        alert(response.data);
                    }
                    else {
                        alert(response.data);
                    }
                },
            })
        }
    }
}
homeController.init();