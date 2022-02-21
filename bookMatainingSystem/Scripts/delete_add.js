function deleteBook(e) {
    e.preventDefault();
    var tr = this.dataItem($(e.currentTarget).closest("tr"))//取到要刪除的那筆資料(object) kendo 取值方式
    var id = tr.BookID
    var status = tr.BookStatus
    if (status == "已借出") {
        alert("書本已借出，不可刪除資料");
    }
    else {
        if (confirm("是否要刪除此筆資料?")) {
            $.ajax({
                type: "POST",
                url: "/Book/Delete",
                data: "id=" + id,//傳給server的資料
                dataType: "json",//server傳回的資料類型
                success: function (response) {
                    $(tr).remove();
                    alert("刪除成功");
                    $("#book_grid").data("kendoGrid").dataSource.read();
                }, error: function (error) {
                    alert("系統發生錯誤");
                }
            });
        }
        return false;
    }
}
function editBook(e) {
    e.preventDefault();
    var tr = this.dataItem($(e.currentTarget).closest("tr"))//取到要刪除的那筆資料(object) kendo 取值方式
    var id = tr.BookID
    window.location.href = "/Book/Edit/?id=" + id;    
}
 






