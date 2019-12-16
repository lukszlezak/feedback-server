Feedback = {
    start: function (apiKey, nodeId) {
        console.log('started');
        Feedback.apiKey = apiKey;
        document.getElementById(nodeId).onclick = Feedback.showFeedbackWindow;
    },
    showFeedbackWindow() {
        var feedbackText = prompt("Please provide your feedback", "");
        var feedbackSignature = prompt("Please write your signature", "");

        if (feedbackText != null && feedbackText != "" && feedbackSignature != null && feedbackSignature != "") {
            Feedback.sendFeedback(feedbackText, feedbackSignature);
        }
    },
    sendFeedback(feedbackText, feedbackSignature) {
        console.log('sending data to server: ' + feedbackText + " - " + feedbackSignature);

        var http = new XMLHttpRequest();
        var url = 'https://feedback.gemotial.com/api/feedbackapi/submit';

        if (location.hostname === "localhost" || location.hostname === "" || location.hostname === "127.0.0.1") {
            url = 'https://localhost:5001/api/feedbackapi/submit';
        }

        var params = JSON.stringify({ "Message": feedbackText, "Signature": feedbackSignature });
        http.open('POST', url, true);

        http.setRequestHeader('Content-type', 'application/json; charset=UTF-8');
        http.setRequestHeader('apiKey', Feedback.apiKey);

        http.onreadystatechange = function () {
            if (http.readyState == 4 && http.status == 200) {
                console.log("Feedback summited successfully");

                var responseJson = JSON.parse(http.response);
                var responseText = ((responseJson.status == "true") ? "Success!\n\n" : "Error!\n\n") + responseJson.message + "\n\n";
                alert(responseText);
            } else if (http.status != 200) {
                console.log("Server response error:" + http.status);
                alert("Error connection to server");
            }
        }
        http.send(params);
    }
}