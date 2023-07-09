var chatId;
var id = document.cookie
    .split('; ')
    .find(row => row.startsWith('UserId='))
    .split('=')[1];



var pusher = new Pusher('1040badcf112c13f0948', {
    cluster: 'eu',
});

$('#sendMessageBtn').click(function (event) {
    event.preventDefault();

    var messageValue = $('#messageInput').val();

    $.ajax({
        url: '/Chat/SendMessage',
        type: 'POST',
        data: {
            message: messageValue,
            chatId: chatId,
            id: id
        },
        success: function (response) {
            $('#sendMessageForm input').val('');
        },
        error: function (error) {
            console.error('Form submission failed:', error);
        }
    });
});

$('#addFriendBtn').click(function (event) {
    event.preventDefault();

    var messageValue = $('#friendNameInput').val();

    console.log(messageValue);

    $.ajax({
        url: '/Chat/AddFriend',
        type: 'POST',
        data: {
            friendNick: messageValue,
            userId: id
        },
        success: function (response) {
            AddChat(response.chatName, response.chatId);
            $('#addFriendModal').style.display = 'none';
            $('#addFriend input').val('');
        },
        error: function (error) {
            console.error('Form submission failed:', error);
        }
    });
});

function ChooseChat(chatId) {
    event.preventDefault();

    this.chatId = chatId;

    channel = pusher.subscribe(chatId);
    channel.bind('user-send-message', function (data) {
        var isSended = false;
        if (data.id != id) isSended = true;
        UpdateMessage(data.message, isSended, data.userName);
    });

    var messagePlace = document.getElementById("messagePlace");

    messagePlace.innerHTML = "";

    $.ajax({
        url: '/Chat/GetMessanges',
        type: 'GET',
        data: {
            chatId: chatId,
        },
        success: function (data) {
            data.chatInfo.forEach(function (dataInfo) {
                var isSended = false;
                if (dataInfo.whoSendId != id) isSended = true;
                UpdateMessage(dataInfo.message, isSended, dataInfo.whoSendNick);
            });
        },
        error: function (error) {
            console.error('Form submission failed:', error);
        }
    });

    messagesForm.style.display = 'block';
}

function UpdateMessage(message, isSended, messageSender) {
    var newChild = document.createElement('div');


    var senderChild = document.createElement('div');
    senderChild.textContent = messageSender;
    senderChild.className = "message-sender m-2";

    var messageChild = document.createElement('div');
    messageChild.textContent = message;
    messageChild.className = "message-text text-wrap text-break p-2";

    newChild.appendChild(senderChild);
    newChild.appendChild(messageChild);

    if (isSended) {
        newChild.className = 'text-start message m-1';
    } else newChild.className = 'text-end message m-1'

    messagePlace.appendChild(newChild);
}

function AddChat(chatName, chatId) {
    var chatPlace = document.getElementById("newChats");

    var newChild = document.createElement('a');
    newChild.textContent = chatName;
    newChild.href = '#';
    newChild.className = 'list-group-item list-group-item-action';
    newChild.onclick = function () {
        ChooseChat(chatId);
    };

    chatPlace.appendChild(newChild);
}