var viewModel = function (itemsUrl, headers, filters) {
    var self = this;

    self.rows = ko.observableArray([]);

    self.filters = [
    { title: 'User name', filterPropertyName: "UserName", param: ko.observable("") },
    { title: 'Create date', filterPropertyName: "CreateDate", param: ko.observable("") },
    { title: 'Role name', filterPropertyName: "RoleName", param: ko.observable("") }];
    self.headers = [
    { title: 'User name', sortPropertyName: 'UserName', asc: ko.observable(true), active: ko.observable(false) },
    { title: 'Create date', sortPropertyName: 'CreateDate', asc: ko.observable(true), active: ko.observable(false) },
    { title: 'Role name', sortPropertyName: 'RoleName', asc: ko.observable(true), active: ko.observable(false) },
    { title: 'Actions', sortPropertyName: null, asc: ko.observable(true), active: ko.observable(false) }];

    //Sorting and filtering

    self.sort = function (header, event) {
        if (header.sortPropertyName == null)
            return;

        if (header.active())
            header.asc(!header.asc());
        ko.utils.arrayForEach(self.headers, function (item) { item.active(false) });
        header.active(true);

        var prop = header.sortPropertyName;
        var ascSort = function (a, b) { return a[prop] < b[prop] ? -1 : a[prop] > b[prop] ? 1 : 0 };
        var descSort = function (a, b) { return a[prop] > b[prop] ? -1 : a[prop] < b[prop] ? 1 : 0 };
        var sortFunc = header.asc() ? ascSort : descSort;

        self.activeSort(sortFunc);
    };

    self.activeSort = ko.observable(function () { return 0; });

    self.filteredRows = ko.computed(function () {
        var temp = self.rows();

        for (var i = 0; i < self.filters.length; i++) {
            var filter = self.filters[i];
            if (filter.param() != "") {
                temp = ko.utils.arrayFilter(temp, function (item) {

                    //Поменять для того, чтобы исчезала из отфильтрованного
                    //списка только после изменения и новой фильтрации
                    if (ko.isObservable(item[filter.filterPropertyName])) {
                        return item[filter.filterPropertyName]() == filter.param();
                    }
                    else {
                        return item[filter.filterPropertyName] == filter.param();
                    }
                    //return item[filter.filterPropertyName] == filter.param();
                });
            }
        }
        return temp.sort(self.activeSort());
    });


    //Select

    self.selectChanged = function (data, event) {
        if (event.originalEvent) {
            data["SaveRequired"](true);
        }
    };

    self.save = function (data, event) {
        var user = data["UserId"];
        var role = data["RoleId"];
        $.ajax({
            type: "Post",
            url: "../../User/Edit/" + user,
            data: {
                UserId: user,
                RoleId: role
            },
            success: function () {
                alert("success");
                for (var i = 0; i < self.roles.length; i++) {
                    if (self.roles[i]["Id"] == data["RoleId"]()) {

                        //Поменять для того, чтобы исчезала из отфильтрованного
                        //списка только после изменения и новой фильтрации
                        data["RoleName"](self.roles[i]["Name"]);
                        //data["RoleName"] = self.roles[i]["Name"];

                    }
                }
                data["SaveRequired"](false);
            },
            error: function () {
                alert('error');
            }
        })
    };

    self.roles = [];

    $.getJSON("../../user/getroles", function (data) {
        $.each(data, function (key, val) {
            self.roles.push(val);
        });
    });

    $.getJSON(headers, function (data) {
        self.headers = data;
    });

    $.getJSON(filters, function (data) {
        self.filters = data;
    });

    $.getJSON(itemsUrl, function (data) {
        $.each(data, function (key, val) {
            val["RoleId"] = ko.observable(val["RoleId"]);

            //Закомментировать для того, чтобы исчезала из отфильтрованного
            //списка только после изменения и новой фильтрации
            val["RoleName"] = ko.observable(val["RoleName"]);

            val["SaveRequired"] = ko.observable(false);
            self.rows.push(val);
        });
        self.rows(data);
    });
}

ko.applyBindings(new viewModel("../../user/getusers"));