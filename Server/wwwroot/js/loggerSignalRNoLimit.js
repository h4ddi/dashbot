var $logList = $("#LoggerWidgetList");

connection.on("NewLogAdded", addNewLog);

function addNewLog(message) {
    $logList.append("<li class='log-item'><i class='fas fa-angle-right'></i> " + message + "</li>");
}
