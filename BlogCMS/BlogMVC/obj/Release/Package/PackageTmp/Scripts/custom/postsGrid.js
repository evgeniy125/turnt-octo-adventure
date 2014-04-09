


$(function () {
    $("#post-grid").kendoGrid({
        sortable: true,
        pageable: true,
        dataSource: { pageSize: 5 },
        dataBound: function (e) {
            $(".deleteLink").click(function (e) {
                var postId = $(this).attr("data-postId");
                var link = $(this);
                $.ajax({
                    type: "Post",
                    url: "../../Posts/Delete/" + postId,
                    success: function () {
                        var grid = $("#post-grid").data("kendoGrid");
                        var dataItem = grid.dataItem(link.closest("tr"));
                        grid.dataSource.remove(dataItem);
                    },
                    error: function () {
                        alert('error');
                    }

                })
            })
        }
    });
})