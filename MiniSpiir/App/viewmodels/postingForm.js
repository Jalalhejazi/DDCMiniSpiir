define(['durandal/http', 'durandal/app'], function (http, app) {

    var PostingForm = function() {
        this.date = ko.observable();
        this.text = ko.observable();
        this.amount = ko.observable();
        this.category = ko.observable();
    }
    
    PostingForm.prototype.submit = function() {
        http.post('/api/posting', this).then(function (posting) {
            console.log('created new posting', posting);
            app.trigger('posting:new', posting);
        });
    }

    return PostingForm;

});