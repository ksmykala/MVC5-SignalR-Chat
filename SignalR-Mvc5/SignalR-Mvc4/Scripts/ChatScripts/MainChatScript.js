$(function () {
    var chat = $.connection.chatHub;

    chat.client.addNewMessage = function (name, message, alertStyle) {
        if (typeof (alertStyle) === "undefined") alertStyle = 'default';
        $('#discussion').append('<li class="alert alert-' + alertStyle + ' list-group-item" style="padding: 1px; margin-bottom: 0;"><strong>'
            + htmlEncode(name) + '</strong>: '
            + htmlEncode(message) + '</li>');
    };

    chat.client.showClientsCount = function (count) {
        $('#connectedUsers').html(count);
    };

    chat.client.showLoggedUsers = function (names) {
        var $loggedUsersContainer = $('#logged-users');
        $loggedUsersContainer.html('');

        $.each(names, function (index, name) {
            var $userListElement = '<li class="alert alert-default list-group-item" style="padding: 1px; margin-bottom: 0;">'
                + htmlEncode(name) + '</li>';
            $loggedUsersContainer.append($userListElement);
        });
    };

    $('#message').focus();

    $.connection.hub.start().done(function () {
        chat.server.getClientsCount();

        $('#message').keypress(function (e) {
            if (e.keyCode == 13)
                sendMessage();
        });
        $('#sendmessage').click(function () {
            sendMessage();
        });

        function sendMessage() {
            chat.server.send($('#message').val());
            $('#message').val('').focus();
        }
    });
});

function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}