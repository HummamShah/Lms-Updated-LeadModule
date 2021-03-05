app.controller('AgentAttendanceCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",

        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to AgentAttendanceCtrl App");
            $scope.initIndex = function () {
                $scope.Date = new Date();
                $scope.TodaysDate = $scope.GetDatePostFormat(new Date());
                console.log($scope.TodaysDate);
                $scope.AjaxGet("/api/AgentAttendanceApi/GetListing", { Date: $scope.TodaysDate }).then(
                    function (response) {
                        console.log(response);
                        $scope.AttendanceList = response.data.Data;
                    });
            }
            $scope.GetAttendance = function (date) {
                date = $scope.GetDatePostFormat(date);
                $scope.AjaxGet("/api/AgentAttendanceApi/GetListing", { Date: date }).then(
                    function (response) {
                        console.log(response);
                        $scope.AttendanceList = response.data.Data;
                    });
            }
        }
    ]);