let hubConnection;

$(document).ready(function () {
    $('#f-recipient').autocomplete({
        source: '/user/SearchAutocomplete',
        delay: 500
    });

    hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("/chat")
        .build();

    hubConnection.on("Receive", receiveMsg)
});

function sendMessage() {
    let sub = document.getElementById('');
    let recipient = document.getElementById('');
    let content = document.getElementById('');

    hubConnection.invoke("Send", sub, content, recipient);
}

function receiveMsg(subject, content, recipient, sender) {

}