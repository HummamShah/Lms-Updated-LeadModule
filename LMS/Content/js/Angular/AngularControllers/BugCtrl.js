app.controller('BugCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",

        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to Bug App");

            $scope.initIndex = function () {
                $scope.AjaxGet("/api/BugReportApi/GetListData", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Bugs = response.data.Data;
                    });
            }


            $scope.AddInit = function () {
                $scope.Bug = {};
            }
            
            $scope.AddBug = function (Bug) {
                console.log(Bug);
                var promise = $http.post("/api/BugReportApi/AddBug", Bug, { headers: { 'Accept': 'application/json' } });
                promise.then(
                    function (response) {
                        console.log(response);
                        if (response.status == 200) {
                            
                            toaster.pop('success', "success", "Bug Added Successfully");
                            $timeout(function () { window.location.href = '/BugReport'; }, 2000);
                        }

                    });

            }
            
        }
    ]);