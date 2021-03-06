﻿/// <reference path="../Scripts/jquery-1.8.3.js" />
/// <reference path="../Scripts/jquery.signalR-0.5.3.js" />

$(function () {
    var hub = $.connection.moveShape,
        $shape = $("#shape");

    $.extend(hub, {
        shapeMoved: function (cid, x, y) {
            if ($.connection.hub.id !== cid) {
                $shape.css({ left: x, top: y });
            }
        }
    });

    $.connection.hub.start().done(function () {
        $shape.draggable({
            drag: function () {
                hub.moveShape(this.offsetLeft, this.offsetTop || 0);
            }
        });
    })
});