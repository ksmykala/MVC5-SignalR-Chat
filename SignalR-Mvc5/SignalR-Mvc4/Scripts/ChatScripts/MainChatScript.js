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

    chat.client.showLoggedUsers = function (ids) {
        var $loggedUsersContainer = $('#logged-users');
        $loggedUsersContainer.html('');

        var $listItemTemplateUrl = $loggedUsersContainer.data('url');

        $.each(ids, function (index, id) {
            $.get($listItemTemplateUrl,
            {id: id},
            function(result) {
                $loggedUsersContainer.append(result);
            },
            'html');
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
            var connectionId = $('#ConnectionId').val();
            chat.server.send($('#message').val(), connectionId);
            
            $('#message').val('').focus();
        }
    });
});

function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}

function showPrivateChat(url) {
    $.get(url,
        function(result) {
            $('#private-dicusssions-handler').append(result);
            result.find('#private-message').focus();
        },
        'html');
}

function removeElement(selector) {
    $(selector).remove();
}