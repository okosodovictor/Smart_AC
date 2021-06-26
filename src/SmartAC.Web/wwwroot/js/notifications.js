var app = angular.module("NotificationApp", []);

class NotificationsCtrl {
    notifications = [];
    http;
    constructor($http) {
        this.http = $http;
        this.refresh();
        
    }

    refresh() {
        this.http.get("/api/notifications")
            .then(d => d.data)
            .then(d => {
                this.notifications = d;
                //setTimeout(() => this.refresh(), 2000);
            });
    }

    dismiss(notification) {
        this.http.post(`/api/notifications/${notification.notificationId}/dismiss`)
            .then(() => {
                notification.isDismissed = true;
            })
    }

    getPending() {
        return this.notifications.filter(n => !n.isDismissed);
    }

    getDismissed() {
        return this.notifications.filter(n => n.isDismissed);
    }
}

app.controller("NotificationsCtrl", NotificationsCtrl);