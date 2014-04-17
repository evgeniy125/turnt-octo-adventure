var viewModel = function () {
    var self = this;
    self.rows = [];
    self.roles = [];
    self.grid = {};

    self.filters = [
    { title: 'User name', filterPropertyName: "UserName", param: ko.observable("").extend({ rateLimit: { method: "notifyWhenChangesStop", timeout: 500 } }) },
    { title: 'Create date', filterPropertyName: "CreateDate", param: ko.observable("").extend({ rateLimit: { method: "notifyWhenChangesStop", timeout: 500 } }) },
    { title: 'Role name', filterPropertyName: "RoleName", param: ko.observable("").extend({ rateLimit: { method: "notifyWhenChangesStop", timeout: 500 } }) }];

    self.headers = [
    { title: 'USER NAME', sortPropertyName: 'UserName', asc: ko.observable(true), active: ko.observable(false) },
    { title: 'CREATE DATE', sortPropertyName: 'CreateDate', asc: ko.observable(true), active: ko.observable(false) },
    { title: 'ROLE NAME', sortPropertyName: 'RoleName', asc: ko.observable(true), active: ko.observable(false) },
    { title: 'ACTIONS', sortPropertyName: null, asc: ko.observable(true), active: ko.observable(false) }];
}

$.when($.getJSON("../../user/getusers"), $.getJSON("../../user/getroles")).done(function (usersArgs, rolesArgs) {
    var model = new viewModel();
    var usersData = usersArgs[0];
    $.each(usersData, function (key, val) {
        val["RoleId"] = ko.observable(val["RoleId"]);
        val["RoleName"] = ko.observable(val["RoleName"]);
        val["CreateDate"] = new Date(parseInt(val["CreateDate"].substr(6))).toDateString();
        val["SaveRequired"] = ko.observable(false);
    });
    var rows = usersData;
    var rolesData = rolesArgs[0];
    var roles = rolesData;
    model.grid = new Grid(rows, model.headers, model.filters, roles);
    ko.applyBindings(model);
});