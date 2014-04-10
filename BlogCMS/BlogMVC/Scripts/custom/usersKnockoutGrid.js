//Универсальная сортировка, фильтры, стрелки
//Сменить иконку, иконку по центру
//Поиск по части слова через 0.5 сек
//Сделать, чтобы не удалялось при фильтрации


function UsersViewModel() {
    var self = this;

    self.items = ko.observableArray([]);


    

    self.roles = [];

    $.getJSON("../../user/getroles", function (data) {
        $.each(data, function (key, val) {
            self.roles.push(new Role(val));
        });
    });



};

ko.applyBindings(new UsersViewModel());


var GridViewModel = function () {
    var self = this;

    this.rows = ko.observableArray();


};