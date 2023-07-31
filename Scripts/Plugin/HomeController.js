var PageConstant = {
    pagesize: 10,
    pageIndex:1
    }
var homeController = {
    innit: function () {
        homeController.registerEvent();
        homeController.loadData();
    },
    registerEvent: function () {
        $('.txtSalary').on('keypress', function (e) { ///đặt thẻ input, keypress là khi focus vào thẻ
            if (e.which==13) {
                var id = $(this).data('id');
                var salary = $(this).val();
                $.ajax({
                    url: '/Employees/UpdateSalary',
                    type: 'post',
                    data: { id: id, salary: salary },
                    success: function (data) {
                        if (data.status) {
                            alert("Cập nhật thành công");s
                        }else {
                            alert("Cập nhật thất bại");
                        }
                    }
                    
                });
            }
        })
    },
    
        
    loadData: function () {
        $.ajax({
            url: '/Employees/getData',
            type: 'get',
            data: { page: PageConstant.pageIndex, pagesize: PageConstant.pagesize },
            success: function (result) {
                //debugger;
                if (result.status) {
                    var html = '';
                    $.each(result.data, function (i, item) {
                      
                        var template = $('#data-template').html();  //get template của thư viện mustache
                        html+= Mustache.render(template, {
                            eid: item.eid,
                            name: item.name,
                            age: item.age,
                            addr: item.addr,
                            salary: item.salary,
                            image: item.image,
                            deptid: item.deptid,
                            status: item.status ? "<span class=\"btn btn-success\">Active</span>" : "<span class=\"btn btn-danger\">Locked</span>"
                        });
                        //console.log(html);
                       
                    });
                    $('#table-data').html(html);
                    //call back pagination
                    homeController.paging(result.total, function () {
                        homeController.loadData();
                    });
                    homeController.registerEvent();
                }
            }
        });
    },
    paging: function (totalRow, callBack) {
        var totalpage = Math.ceil(totalRow / PageConstant.pagesize)
        $('#pagination').twbsPagination({
            totalPages: totalpage,
            visiblePages: 5,
            onPageClick: function (event, page) {
                /* $('#page-content').text('Page ' + page);*/
                //homeController.loadData(); lỗi ở đây là khi chuyển page sẽ ko kịp callback => settiomeouut
                PageConstant.pageIndex = page   //đặt lại số trang
                setTimeout(callBack,100)
            }
        });
    }

};
homeController.innit();