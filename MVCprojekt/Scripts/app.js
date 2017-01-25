var ViewModel = function () {
    var self = this;
    self.bronie = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();
    self.sklepy = ko.observableArray();
    self.newBron = {
        Sklep: ko.observable(),
        Nazwa: ko.observable(),
        Model: ko.observable(),
        Numer: ko.observable()
    }

    var bronieUri = '/api/bron/';
    var sklepyUri = '/api/sklep/';

    function ajaxHelper(uri, method, data) {
        self.error(''); 
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllBronie() {
        ajaxHelper(bronieUri, 'GET').done(function (data) {
            self.bronie(data);
        });
    }

    self.getBronDetail = function (item) {
        ajaxHelper(booksUri + item.BronID, 'GET').done(function (data) {
            self.detail(data);
        });
    }

    function getSklepy() {
        ajaxHelper(sklepyUri, 'GET').done(function (data) {
            self.sklepy(data);
        });
    }


    self.addBron = function (formElement) {
        var bron = {
            SklepID: self.newBron.Sklep().SklepID,
            Nazwa: self.newBron.Nazwa(),
            Model: self.newBron.Model(),
            Numer: self.newBron.Numer()
        };

        ajaxHelper(bronieUri, 'POST', bron).done(function (item) {
            self.bronie.push(item);
        });
    }

    // Fetch the initial data.
    getAllBronie();
    getSklepy();
};

ko.applyBindings(new ViewModel());