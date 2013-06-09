var profile = {

    expertStatisticsBalance: function (buy, sell, profit, ticks) {
        $(function () {
            var zero = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
            var plot1 = $.jqplot('chartdiv', [buy, sell, profit, zero, ticks],
            {
                title: 'Bilans pieniężny',

                seriesDefaults:
                {
                    shadow: false,
                    markerOptions:
                    {
                        shadow: false
                    }
                },

                series:
                [
                    {
                        label: 'Sprzedaż',
                        color: '#468847',
                        markerOptions: { size: 7, color: "#468847" }
                    },
                    {
                        label: 'Zakup',
                        color: '#B94A48',
                        markerOptions: { size: 7, color: "#B94A48" }
                    },
                    {
                        label: 'Zysk',
                        color: 'DodgerBlue',
                        markerOptions: { size: 7, color: "DodgerBlue" }
                    },
                    {
                        showLabel: false,
                        lineWidth: 1,
                        color: 'Black',
                        markerOptions: { show: false }
                    },
                    {
                        showLabel: false
                    }
                ],

                axes:
                {
                    xaxis:
                    {
                        renderer: $.jqplot.CategoryAxisRenderer,
                        ticks: ticks
                    },

                    yaxis:
                    {
                        tickOptions:
                        {
                            formatString: '$%.2f'
                        }
                    }
                },

                grid:
                {
                    drawGridLines: false,
                    gridLineColor: 'White',
                    shadow: false,
                    borderWidth: 0.0,
                    borderColor: 'White'
                },

                highlighter:
                {
                    show: true,
                    sizeAdjust: 7.5
                },

                cursor:
                {
                    show: false
                },

                legend:
                {
                    show: true, location: 'ne'
                }
            });
        });
    },

    initShowAreaButton: function (buttonSelector, areaSelector, serverEventUrl) {
        $(buttonSelector).click(function () {
            $(this).hide();
            $(this).parent().hide();
            $(areaSelector).show();
            
            $.get(serverEventUrl);
        });
    }
}