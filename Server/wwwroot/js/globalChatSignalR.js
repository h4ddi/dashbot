var $chatList = $("#GlobalChatList");

connection.on("ChatGlobal", addNewMessage);

function addNewMessage(message) {
    $chatList.append("<li>" + message.senderUsername + " in " + message.channelName + " : " + message.message + "</li>");
}
