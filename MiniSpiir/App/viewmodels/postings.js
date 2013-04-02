define(['durandal/http', 'durandal/app'], function (http, app) {
    var Postings = function() {
        this.postings = ko.observableArray();
        var me = this;

        this.activate = function () {
            return $.get('/api/posting').then(function(postings) {
                me.postings(postings);
            });
        };

        var newPostingSubscription = app.on('posting:new').then(function(posting) {
            console.log('got new posting', posting);
            me.postings.push(posting);
        });

        this.deactivate = function() {
            newPostingSubscription.off();
        };
    };

    return Postings;
});