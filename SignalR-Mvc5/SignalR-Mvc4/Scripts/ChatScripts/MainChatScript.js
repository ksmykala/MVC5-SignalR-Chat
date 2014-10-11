var chat;

$(function () {
    chat = $.connection.chatHub;

    chat.client.addNewMessage = function (name, message) {
        $('#discussion').append('<li class="alert list-group-item" style="padding: 1px; margin-bottom: 0;"><strong>'
            + htmlEncode(name) + '</strong>: '
            + htmlEncode(message) + '</li>');
    };

    chat.client.addPrivateMessage = function (name, message, senderId) {
        if (document.getElementById('private-' + senderId) == null) {
            var message = '<li class="alert list-group-item" style="padding: 1px; margin-bottom: 0;"><strong>'
                + htmlEncode(name) + '</strong>: '
                + htmlEncode(message) + '</li>';
            showPrivateChat(senderId, message);
        } else {
            $('#private-' + senderId + ' #private-discussion').append('<li class="alert list-group-item" style="padding: 1px; margin-bottom: 0;"><strong>'
                + htmlEncode(name) + '</strong>: '
                + htmlEncode(message) + '</li>');
        }
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

        function sendMessage(connectionId) {
            chat.server.send($('#message').val(), connectionId);
            $('#message').val('').focus();
        }
    });
});

function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}

function showPrivateChat(connectionId, message) {
    var $privateDiscussionHandler = $('#private-dicussions-handler');
    var url = $privateDiscussionHandler.data('url');

    $.get(url,
        {id: connectionId},
        function(result) {
            $privateDiscussionHandler.append(result);
            $('#private-' + connectionId + ' #private-discussion').append(message);
        },
        'html');
}

function removeElement(selector) {
    $(selector).remove();
}