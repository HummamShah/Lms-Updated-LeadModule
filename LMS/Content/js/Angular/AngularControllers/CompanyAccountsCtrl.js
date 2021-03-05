app.controller('CompanyAccountsCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",
        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to CompanyBilling App");
            $scope.initIndex = function () {
                $scope.CompanyAccounts = [];
                $scope.AjaxGet("/api/CompanyAccountsApi/GetListData", null).then(
                    function (response) {
                        console.log(response);
                        $scope.CompanyAccounts = response.data.Data;
                    });
            }

            $scope.InitDetails = function () {
                $scope.CompanyAccounts = [];
                $scope.TotalCredit = 0; $scope.TotalDebit = 0;
                var Id = $scope.GetUrlParameter("Id");
                $scope.AjaxGet("/api/CompanyAccountsApi/GetCompanyAccountsDetail", {Id:Id}).then(
                    function (response) {
                        console.log(response);
                        $scope.CompanyAccounts = response.data;
                        angular.forEach($scope.CompanyAccounts.Data, function (value, key) {
                            $scope.TotalCredit = $scope.TotalCredit + value.Credit;
                            $scope.TotalDebit = $scope.TotalDebit + value.Debit;
                        }); 
                        //AccountEntriesData
                    });
            }
            $scope.InitChildIndex = function () {
                $scope.CompanyAccounts = [];
                var Id = $scope.GetUrlParameter("Id");
                $scope.AjaxGet("/api/CompanyAccountsApi/GetChildListData", { Id: Id }).then(
                    function (response) {
                        console.log(response);
                        $scope.CompanyAccounts = response.data.Data;
                    });
            }
            $scope.InitParentIndex = function () {
                $scope.CompanyAccounts = []; 
                $scope.AjaxGet("/api/CompanyAccountsApi/GetParentListData", null).then(
                    function (response) {
                        console.log(response);
                        $scope.CompanyAccounts = response.data.Data;
                    });
            }



        }]);