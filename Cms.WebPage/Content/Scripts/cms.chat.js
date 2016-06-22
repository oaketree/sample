$(function () {
	chat = $.connection.chatHub;
	chat.client.broadcastMessage = function (name,message) {

	}

	$.connection.hub.start().done(function () {
		chat.server.connect(userid, username, deptname);

	})
})