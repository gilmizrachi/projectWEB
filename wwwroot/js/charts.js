


    var data = $("#chartValue").val();
    data = JSON.parse(data);
    //console.log(data);
    var m = [25, 70, 50, 4],
        w = 375 - m[1] - m[3],
        h = 250 - m[0] - m[2],
        parse = d3.time.format("%d/%m/%Y").parse;

    var x = d3.time.scale().range([0, w]),
        y = d3.scale.linear().range([h, 0]),
        xAxis = d3.svg.axis().scale(x).ticks(5).tickSize(-h).tickFormat(d3.time.format("%d/%m")),
        yAxis = d3.svg.axis().scale(y).ticks(4).orient("right").tickFormat(function (d) {
            if ((d / 1000) >= 1) {
                d = d / 1000 + "K";
            }
            return d;
        });

    var area = d3.svg.area()
        .interpolate("linear")
        .x(function (d) {
            return x(d.sale_time);
        })
        .y0(h)
        .y1(function (d) {
            return y(d.value);
        });

    var line = d3.svg.line()
        .interpolate("linear")
        .x(function (d) {
            return x(d.sale_time);
        })
        .y(function (d) {
            return y(d.value);
        });

    data.forEach(function (d) {
        d.sale_time = parse(d.sale_time);
        d.value = +d.value;
    });

    var total = 0
    for (var i = 0, len = data.length; i < len; i++) {
        total += data[i].value;
    }

    x.domain([data[0].sale_time, (Date.now() - 1)]); //data[data.length - 1].sale_time]);
    y.domain([0, d3.max(data, function (d) {
        return (d.value + d.value/10);
    })]).nice();
    
    var svg = d3.select("svg") //.append("svg:svg")
        .attr("width", w + m[1] + m[3])
        .attr("height", h + m[0] + m[2])
        .append("svg:g")
        .attr("transform", "translate(" + m[3] + "," + m[0] + ")");

    svg.append("svg:path")
        .attr("class", "area")
        .attr("d", area(data));

    svg.append("svg:g")
        .attr("class", "x axis")
        .attr("transform", "translate(1," + h + ")")
        .call(xAxis);

    svg.append("svg:g")
        .attr("class", "y axis")
        .attr("transform", "translate(" + w + ",0)")
        .call(yAxis);

    svg.selectAll("line.y")
        .data(y.ticks(5))
        .enter().append("line")
        .attr("x1", 0)
        .attr("x2", w)
        .attr("y1", y)
        .attr("y2", y)
        .style("stroke", "#000000")
        .style("stroke-opacity", 0.06);

    svg.append("svg:path")
        .attr("class", "line")
        .attr("d", line(data));

    svg.append("svg:text")
        .attr("x", 80)
        .attr("y", -10)
        .attr("text-anchor", "end")
        .text('Total Profit')
        .style("stroke", "#444")
        .style("fill", "#000")
        .style("stroke-width", .2)
        .style("font-size", "12px")
        .style("font-weight", "bold");

    svg.append("svg:text")
        .attr("x", w)
        .attr("y", -10)
        .attr("text-anchor", "end")
        .text('$' + total.toFixed(2) + " total")
        .style("stroke", "#008cdd")
        .style("fill", "#008cdd")
        .style("stroke-width", .2)
        .style("font-size", "12px")
        .style("font-weight", "bold");

    svg.selectAll("circle")
        .data(data)
        .enter().append("circle")
        .attr("fill", "#008cdd")
        .attr("r", 4)
        .attr('cx', function (d) {
            return x(d.sale_time);
        })
        .attr('cy', function (d) {
            return y(d.value);
        });




(function (window, document, $, undefined) {
    "use strict";
    $(function () {


        if ($('.ct-chart-pie').length) {
            var dataval = $('#piedata').val();
            dataval = JSON.parse(dataval);
            var data={
                series: dataval,
                labels: ["paid","active"]
            };
            console.log(data);
            var sum = function (a, b) { return a + b };

            new Chartist.Pie('.ct-chart-pie', data);/*, {
                labelInterpolationFnc: function (value) {
                    return Math.round(value / data.series.reduce(sum) * 100) + '%';
                }
            });*/
        }


        if ($('.ct-chart-animation').length) {
            var chart = new Chartist.Line('.ct-chart-animation', {
                labels: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
                series: [
                    [12, 9, 7, 8, 5, 4, 6, 2, 3, 3, 4, 6],
                    [4, 5, 3, 7, 3, 5, 5, 3, 4, 4, 5, 5],
                    [5, 3, 4, 5, 6, 3, 3, 4, 5, 6, 3, 4],
                    [3, 4, 5, 6, 7, 6, 4, 5, 6, 7, 6, 3]
                ]
            }, {
                low: 0
            });

            // Let's put a sequence number aside so we can use it in the event callbacks
            var seq = 0,
                delays = 80,
                durations = 500;

            // Once the chart is fully created we reset the sequence
            chart.on('created', function () {
                seq = 0;
            });

            // On each drawn element by Chartist we use the Chartist.Svg API to trigger SMIL animations
            chart.on('draw', function (data) {
                seq++;

                if (data.type === 'line') {
                    // If the drawn element is a line we do a simple opacity fade in. This could also be achieved using CSS3 animations.
                    data.element.animate({
                        opacity: {
                            // The delay when we like to start the animation
                            begin: seq * delays + 1000,
                            // Duration of the animation
                            dur: durations,
                            // The value where the animation should start
                            from: 0,
                            // The value where it should end
                            to: 1
                        }
                    });
                } else if (data.type === 'label' && data.axis === 'x') {
                    data.element.animate({
                        y: {
                            begin: seq * delays,
                            dur: durations,
                            from: data.y + 100,
                            to: data.y,
                            // We can specify an easing function from Chartist.Svg.Easing
                            easing: 'easeOutQuart'
                        }
                    });
                } else if (data.type === 'label' && data.axis === 'y') {
                    data.element.animate({
                        x: {
                            begin: seq * delays,
                            dur: durations,
                            from: data.x - 100,
                            to: data.x,
                            easing: 'easeOutQuart'
                        }
                    });
                } else if (data.type === 'point') {
                    data.element.animate({
                        x1: {
                            begin: seq * delays,
                            dur: durations,
                            from: data.x - 10,
                            to: data.x,
                            easing: 'easeOutQuart'
                        },
                        x2: {
                            begin: seq * delays,
                            dur: durations,
                            from: data.x - 10,
                            to: data.x,
                            easing: 'easeOutQuart'
                        },
                        opacity: {
                            begin: seq * delays,
                            dur: durations,
                            from: 0,
                            to: 1,
                            easing: 'easeOutQuart'
                        }
                    });
                } else if (data.type === 'grid') {
                    // Using data.axis we get x or y which we can use to construct our animation definition objects
                    var pos1Animation = {
                        begin: seq * delays,
                        dur: durations,
                        from: data[data.axis.units.pos + '1'] - 30,
                        to: data[data.axis.units.pos + '1'],
                        easing: 'easeOutQuart'
                    };

                    var pos2Animation = {
                        begin: seq * delays,
                        dur: durations,
                        from: data[data.axis.units.pos + '2'] - 100,
                        to: data[data.axis.units.pos + '2'],
                        easing: 'easeOutQuart'
                    };

                    var animations = {};
                    animations[data.axis.units.pos + '1'] = pos1Animation;
                    animations[data.axis.units.pos + '2'] = pos2Animation;
                    animations['opacity'] = {
                        begin: seq * delays,
                        dur: durations,
                        from: 0,
                        to: 1,
                        easing: 'easeOutQuart'
                    };

                    data.element.animate(animations);
                }
            });

            // For the sake of the example we update the chart every time it's created with a delay of 10 seconds
            chart.on('created', function () {
                if (window.__exampleAnimateTimeout) {
                    clearTimeout(window.__exampleAnimateTimeout);
                    window.__exampleAnimateTimeout = null;
                }
                window.__exampleAnimateTimeout = setTimeout(chart.update.bind(chart), 12000);
            });



        }

    });

})(window, document, window.jQuery);