app.controller('NotificationCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",

        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to Notification App");

            $scope.initNotification = function () {
                
                $scope.AjaxGet("/api/NotificationApi/GetListData", null).then(
                    function (response) {
                        console.log(response);
                        $scope.notification = response.data.Data;
                    });
            }

        }
    ]);