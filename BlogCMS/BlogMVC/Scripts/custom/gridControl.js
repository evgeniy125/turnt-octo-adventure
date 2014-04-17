function Grid(rows, headers, filters, roles) {
    var self = this;
    self.roles = roles;
    self.rows = ko.observable(rows);
    self.filters = filters;
    self.headers = headers;

    //Sorting and filtering

    self.sort = function (header, event) {
        if (header.sortPropertyName == null)
            return;

        if (header.active())
            header.asc(!header.asc());
        ko.utils.arrayForEach(self.headers, function (item) { item.active(false) });
        header.active(true);

        var prop = header.sortPropertyName;
        var ascSort = function (a, b) {
            if (ko.isObservable(a[prop]))
                return a[prop]() < b[prop]() ? -1 : a[prop]() > b[prop]() ? 1 : 0;
            else {
                return a[prop] < b[prop] ? -1 : a[prop] > b[prop] ? 1 : 0;
            }
        };
        var descSort = function (a, b) {
            if (ko.isObservable(a[prop]))
                return a[prop]() > b[prop]() ? -1 : a[prop]() < b[prop]() ? 1 : 0;
            else {
                return a[prop] > b[prop] ? -1 : a[prop] < b[prop] ? 1 : 0;
            }
        };
        var sortFunc = header.asc() ? ascSort : descSort;

        self.activeSort(sortFunc);
    };

    self.activeSort = ko.observable(function () { return 0; });

    //Paging

    self.page = ko.observable(1);

    self.itemsPerPage = ko.observable(10);

    self.pagesCount = ko.observable(self.rows().length / self.itemsPerPage());

    self.pagesTotal = ko.computed(function () {
            return Math.ceil(self.pagesCount() / self.itemsPerPage());
    });

    self.nextPage = function () {
        self.page(self.page() + 1);
    };

    self.previousPage = function () {
        self.page(self.page() - 1);
    };

    self.firstPage = function () {
        self.page(1);
    };

    self.lastPage = function () {
        self.page(self.pagesTotal());
    };

    self.previousPageEnabled = ko.computed(function () {
        if (self.page() != 1) {
            return true;
        }
        return false;
    });

    self.nextPageEnabled = ko.computed(function () {
        if (self.page() != self.pagesTotal()) {
            return true;
        }
        return false;
    });

    self.filteredRows = ko.computed(function () {
        self.page(1);
        var temp = self.rows();
        for (var i = 0; i < self.filters.length; i++) {
            var filter = self.filters[i];
            if (filter.param() != "") {
                temp = ko.utils.arrayFilter(temp, function (item) {
                    if (ko.isObservable(item[filter.filterPropertyName])) {
                        return item[filter.filterPropertyName]().substr(0, filter.param().length) == filter.param();
                    }
                    else {
                        return item[filter.filterPropertyName].substr(0, filter.param().length) == filter.param();
                    }
                });
            }
        }
        return temp;
    });

    self.pagedAndSortedRows = ko.computed(function () {
        var temp = self.filteredRows().sort(self.activeSort());
        self.pagesCount(temp.length);
        var indexOfFirstItemOnCurrentPage = (self.page() - 1) * (self.itemsPerPage());
        return temp.slice(indexOfFirstItemOnCurrentPage, indexOfFirstItemOnCurrentPage + self.itemsPerPage());
    });

    //Read

    self.read = function (data) {
        document.location.href = '../../posts/index/' + data.UserId;
    };

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
                        data["RoleName"](self.roles[i]["Name"]);
                    }
                }
                data["SaveRequired"](false);
            },
            error: function () {
                alert('error');
            }
        })
    };
}