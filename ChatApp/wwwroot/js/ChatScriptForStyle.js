const addFriendLink = document.getElementById('addFriendLink');
const addFriendModal = document.getElementById('addFriendModal');

const messageForm = document.getElementById('messagesForm');
const messagePlace = document.getElementById('messagePlace');

setHeight();

function setHeight() {
    document.body.style.height = document.documentElement.scrollHeight;
    document.getElementById('PlaceForChats').style.height = (document.documentElement.scrollHeight - document.getElementById('NavBarContent').offsetHeight) + 'px';
    document.getElementById('messagesForm').style.height = (document.documentElement.scrollHeight - document.getElementById('NavBarContent').offsetHeight) + 'px';
    document.getElementById('messagePlace').style.height = (document.getElementById('messagesForm').offsetHeight - document.getElementById('sendMessageForm').offsetHeight - 10) + 'px';
}

addFriendLink.addEventListener('click', function (event) {
    event.preventDefault();

    addFriendModal.style.display = 'block';
});

window.addEventListener('click', function (event) {
    if (event.target === addFriendModal) {
        addFriendModal.style.display = 'none';
    }
});