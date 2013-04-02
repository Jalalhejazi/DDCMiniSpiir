﻿define(function() {
    var Overview = function () {
        var me = this;
        this.categories = ko.observableArray();

        this.activate = function() {
            return $.get('/api/overview').then(me.categories);
        };
    };

    Overview.prototype.viewAttached = function (view) {
        //you can get the view after it's bound and connected to it's parent dom node if you want
        
        function getCategoryBarChartOptions(options) {
            options = $.extend({
                data: [],
                onclick: function() {
                },
                animate: false,
                color: '#ff6d1e',
                renderTo: null
            }, options);

            var categories = options.data.map(function(p) { return p.name; });

            var chartOptions = {
                chart: {
                    defaultSeriesType: 'bar',
                    marginLeft: 10,
                    marginRight: 40,
                    renderTo: options.renderTo,
                    height: options.data.length * 45 + 20,
                    margin: [0, 20, 20, 60],
                    style: {
                        fontFamily: 'MuseoSans, sans-serif'
                    }
                },
                title: {
                    text: null
                },
                xAxis: {
                    categories: categories,
                    tickInterval: 1,
                    maxPadding: 0.1,
                    minPadding: 0.1,
                    labels: {
                        align: 'left',
                        y: -14,
                        x: 0,
                        style: { color: '#3f3f3f' }
                    },
                    lineColor: '#fff',
                    tickWidth: 0
                },
                yAxis: {
                    min: 0,
                    title: { text: null },
                    gridLineColor: '#eee'
                },
                plotOptions: {
                    series: {
                        animation: options.animate
                    },
                    bar: {
                        dataLabels: {
                            enabled: true,
                            formatter: function() {
                                if (this.y === 0)
                                    return '';


                                return this.y + ' kr.';
                            },
                            x: 2,
                            y: Highcharts.Renderer === Highcharts.VMLRenderer ? 8 : 5,
                            style: { fontSize: '14px', color: '#3f3f3f' }
                        },
                        pointWidth: 20
                    }
                },
                series: [{
                    data: options.data,
                    color: options.color
                }],
                credits: { enabled: false },
                legend: { enabled: false },
            };

            return chartOptions;
        }

        var chart = new Highcharts.Chart(getCategoryBarChartOptions({
            renderTo: 'chart',
            data: this.categories().map(function(cat) {
                return {
                    y: cat.Total,
                    name: cat.Name
                };
            })
        }));

    };

    return Overview;
});