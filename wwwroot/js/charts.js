$(function () {
    var data = $("#chartValue").val();
    data = JSON.parse(data);
    console.log(data);
    var m = [25, 70, 50, 4],
        w = 375 - m[1] - m[3],
        h = 250 - m[0] - m[2],
        parse = d3.time.format("%d/%m/%Y").parse;

    var x = d3.time.scale().range([0, w]),
        y = d3.scale.linear().range([h, 0]),
        xAxis = d3.svg.axis().scale(x).tickSize(-h).tickFormat(d3.time.format("%d/%m")),
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
});