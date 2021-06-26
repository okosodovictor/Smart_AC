$(function () {
    var $notification = $("#notificationBadge")
    if ($notification.length > 0) {
        fetchData();
    }

    function fetchData() {
        fetch('/api/notifications/pending')
            .then(response => response.json())
            .then(data => {
                if (data.length) {
                    $notification.text(data.length);
                    $notification.css("visibility", "visible");
                }
                else {
                    $notification.css("visibility", "hidden");
                }
            })
            .finally(() => {
                setTimeout(fetchData, 1000);
            })
    }
});