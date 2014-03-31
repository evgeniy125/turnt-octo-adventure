


var template = kendo.template($("#template").html());

$("#users-grid").kendoGrid({
    sortable: true,
    filterable: true,
    pageable: true,
    editable: true,
    groupable: true,
    columns: [
    { field: "UserName", type: "text", title: "User name", sortable: true, editable: false },
    {
        field: "CreateDate", title: "Create date", sortable: true,
        type: "date",
        format: "{0:dd-MMM-yyyy hh:mm:ss tt}",
        editable: false
    },
    {
        hidden: true, field: "RoleName"
    },
    {
        field: "RoleId",
        title: "Edit Role",
        //template: '<span class="role">#= RoleName #</span>',
        editable: true,
        editor: function (container, options) {
            var input = $("<input/>");
            input.attr("name", "Id");
                
            input.appendTo(container);

            input.kendoDropDownList({
                dataTextField: "Name",
                dataValueField: "Id",
                dataSource: {
                    transport: {
                        read: "../../user/getroles",
                        dataType: "json"
                    }
                },
                change: function (e) {
                    var value = this.value();

                    var tr = $(this.element).closest("tr");
                    var grid = $("#users-grid").data("kendoGrid");
                    var data = grid.dataItem(tr);
                    var user = data.UserId;

                    $.ajax({
                        type: "Post",
                        url: "../../User/Edit/" + user,
                        data: {
                            UserId: user,
                            SelectedRoleId: value
                        },
                        error: function () {
                            alert('error');
                        }
                    })

                },
            })
        }
    },
    {
        field: "UserId", type: "text", title: "Actions", sortable: false,
        editable: false,
        template: template({ UserId: "#= UserId #" }),
    }],
    dataSource: {
        transport: {
            read: "../../user/getusers",
            dataType: "json"
        },
        pageSize: 10
    }
})






