
function NotiSuccess(title, message) {
    $.gritter.add({
        // (string | mandatory) the heading of the notification
        title: title,
        time: 2000,
        sticky: false,
        // (string | mandatory) the text inside the notification
        text: message,
        class_name: 'gritter-success'
    });
}

function NotiError(title, message) {
    $.gritter.add({
        // (string | mandatory) the heading of the notification
        title: title,
        time: 2000,
        sticky: false,
        // (string | mandatory) the text inside the notification
        text: message,
        class_name: 'gritter-error'
    });
}
function AfterSussessActionAjaxform() {
    location.reload();
}

function AjaxSearchSuccess(rs) {
    location.reload();
}

function AjaxFormSuccess(rs) {
    if (rs.Status) {

        $("#MasterModal").modal("hide");
        $("#MasterModal").empty();
        NotiSuccess("Thành công", "Cập nhật dữ liệu thành công");
        AfterSussessActionAjaxform();
    } else {
        NotiError("Lỗi xử lý", rs.Message);
    }
}