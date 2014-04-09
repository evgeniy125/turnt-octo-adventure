//Универсальная сортировка, фильтры, стрелки
//Сменить иконку, иконку по центру
//Поиск по части слова через 0.5 сек
//Сделать, чтобы не удалялось при фильтрации


function User(data, initialRole) {
    var self = this;
    self.username = data.UserName;
    self.userId = data.UserId;
    self.createDate = new Date(parseInt(data.CreateDate.substr(6))).toDateString();
    self.role = ko.observable(initialRole);
    self.saveRequired = ko.observable(false);
};

function Role(data) {
    var self = this;
    self.roleName = data.Name;
    self.roleId = data.Id;
};

function resetSorting(model) {
    model.users.sort(function (left, right) {
        return 0;
    });
    model.dateArrow('none');
    model.roleNameArrow('none');
    model.userNameArrow('none');
}

function UsersViewModel() {
    var self = this;
    userNameDirection = -1;
    dateDirection = -1;
    roleNameDirection = -1;

    self.users = ko.observableArray([]);

    self.userNameArrow = ko.observable('none');
    self.dateArrow = ko.observable('none');
    self.roleNameArrow = ko.observable('none');


    //this.addSort = function (field) {
    //    self['sortBy_' + field] = function () {
    //        self.users.sort(function (a, b) {
    //            if (a[field] > b[field]) return 1;
    //            if (a[field] < b[field]) return -1;
    //            return 0;
    //        });
    //    };
    //};

    //for (var field in self.users[0]) {
    //    self.addSort(field);
    //}

    self.sortByUserName = function () {
        resetSorting(self);
        userNameDirection = -userNameDirection;
        self.users.sort(function (left, right) {
            return left.username == right.username ? 0 :
                (left.username < right.username ? -1 * userNameDirection : 1 * userNameDirection);
        });
        if (userNameDirection > 0)
            self.userNameArrow('down');
        else
            self.userNameArrow('up');
    };

    self.sortByDate = function () {
        resetSorting(self);
        dateDirection = -dateDirection;
        self.users.sort(function (left, right) {
            return left.createDate == right.createDate ? 0 :
                (left.createDate < right.createDate ? -1 * dateDirection : 1 * dateDirection);
        });
        if (dateDirection > 0)
            self.dateArrow('down');
        else
            self.dateArrow('up');
    };

    self.sortByRoleName = function () {
        resetSorting(self);
        roleNameDirection = -roleNameDirection;
        self.users.sort(function (left, right) {
            return left.role().roleName == right.role().roleName ? 0 :
                (left.role().roleName < right.role().roleName ? -1 * roleNameDirection : 1 * roleNameDirection);
        });
        if (roleNameDirection > 0)
            self.roleNameArrow('down');
        else
            self.roleNameArrow('up');
    };

    self.read = function (data) {
        document.location.href = '../../posts/index/' + data.userId;
    };


    //Filtering

    self.userNameFilter = ko.observable("");
    self.roleNameFilter = ko.observable("");

    self.filteredUsers = ko.computed(function () {
        return ko.utils.arrayFilter(self.users(), function (User) {
            return ((User.username == self.userNameFilter() || self.userNameFilter() == "") &&
               (User.role().roleName == self.roleNameFilter() || self.roleNameFilter() == ""));
        });
    });


    //Select

    self.changed = function (data, event) {
        if (event.originalEvent) {
            data.saveRequired(true);
        }
    };

    self.selectRole = function (data, event) {
        var user = data.userId;
        var role = data.role().roleId;
        $.ajax({
            type: "Post",
            url: "../../User/Edit/" + user,
            data: {
                UserId: user,
                RoleId: role
            },
            success: function () {
                alert("success");
                data.saveRequired(false);
            },
            error: function () {
                alert('error');
            }
        })
    };

    self.roles = [];

    $.getJSON("../../user/getroles", function (data) {
        $.each(data, function (key, val) {
            self.roles.push(new Role(val));
        });
    });

    $.getJSON("../../user/getusers", function (data) {
        $.each(data, function (key, val) {
            for (var i = 0; i < self.roles.length; i++) {
                if (self.roles[i].roleName == val.RoleName)
                    self.users.push(new User(val, self.roles[i]));
            }
        });
    });

};

ko.applyBindings(new UsersViewModel());
