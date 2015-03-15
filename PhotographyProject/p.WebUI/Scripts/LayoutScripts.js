

window.array = [];

var PICTURES_URL = "http://localhost:4221"; //"http://nicepicchat.azurewebsites.net";


function acceptRequest(button) {
    var id = $(button).attr('data-id');
    $(button).hide();
    var url = '/FriendsZone/AcceptRequest/' + id;
    $.ajax({
        url: url,
        cache: true,
        success: function (data) {
            var div = $('#conversationsPlace');
            appendConversation(data, div);
        }
    });
}

$.ajaxSetup({
    cache: true
});
$("#NewMessagesContainer").click(function () {
    if ($("#newVisits").is(":visible")) {
        $("#newVisits").hide("fast");
    }
    if ($("#newWatchers").is(":visible")) {
        $("#newWatchers").hide("fast");
    }
    if ($("#newMessages").is(":hidden")) {
        $.get('/MessMe/NewMessages/', function (data) {
            if(data != null)
            $("#newMessages").append(data);
        });
        $("#newMessages").show("fast");
    }
    else {
        $("#newMessages").hide("fast");
    }
});

$("#visitsImg").click(function () {
    if ($("#newMessages").is(":visible")) {
        $("#newMessages").hide("fast");
    }
    if ($("#newWatchers").is(":visible")) {
        $("#newWatchers").hide("fast");
    }
    if ($("#newVisits").is(":hidden")) {
        $("#newVisits").show("fast");
    }
    else {
        $("#newVisits").hide("fast");
    }

});

$("#watchersImg").click(function () {
    if ($("#newMessages").is(":visible")) {
        $("#newMessages").hide("fast");
    }
    if ($("#newVisits").is(":visible")) {
        $("#newVisits").hide("fast");
    }
    if ($("#newWatchers").is(":hidden")) {
        $.ajax({
            url: '/FriendsZone/FriendRequests/',
            success: function (data) {
                $("#newWatchers").append(data);
            }
        });
        $("#newWatchers").show("fast");
    }
    else {
        $("#newWatchers").hide("fast");
    }

});

$("#wrap").click(function () {
    $("#newMessages").hide();
    $("#newWatchers").hide();
    $("#newVisits").hide();
});

$("#newMessages").click(function () {
    $(this).hide("fast");
});
$("#newVisits").click(function () {
    $(this).hide("fast");
});










