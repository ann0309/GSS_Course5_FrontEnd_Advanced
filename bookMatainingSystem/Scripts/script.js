function showRecord(e) {
    e.preventDefault();
    var tr = this.dataItem($(e.currentTarget).closest("tr"))//取到要刪除的那筆資料(object) kendo 取值方式
    var id = tr.BookID
    window.location.href = "/Book/LendRecord/?id=" + id;
}
$(function () {
    $("#book_category_id").kendoDropDownList({
        dataTextField: "Text",
        dataValueField: "Value",
        dataSource: {
            transport: {
                read: {
                    url: "/Book/GetCategoryData",
                    dataType: "json",
                    type: "post"
                }
            }
        },
        index: 0,
        optionLabel: "請選擇..."

    });
    $("#book_keeper").kendoDropDownList({
        dataTextField: "Text",
        dataValueField: "Value",
        dataSource: {
            transport: {
                read: {
                    url: "/Book/GetKeeperData",
                    dataType: "json",
                    type: "post"
                }
            }
        },
        index: 0,
        optionLabel: "請選擇...",
    });
    $("#book_status").kendoDropDownList({
        dataTextField: "Text",
        dataValueField: "Value",
        dataSource: {
            transport: {
                read: {
                    url: "/Book/GetStatusData",
                    dataType: "json",
                    type: "post"
                }
            }
        },
        index: 0,
        optionLabel: "請選擇...",
    });
    $("#bought_datepicker").kendoDatePicker({
        max: new Date(),
        dateInput: true,
        format: "yyyy-MM-dd",
        value: new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate())

    });
})

