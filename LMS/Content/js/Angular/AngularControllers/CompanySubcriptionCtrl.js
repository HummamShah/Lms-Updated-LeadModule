app.controller('CompanySubcriptionCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",

        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to CompanySubcriptionCtrl App");
            
            $scope.initIndex = function () {
                var Id = $scope.GetUrlParameter("Id");
                console.log(Id);
                $scope.AjaxGet("/api/CompanySubcriptionApi/GetListData", { Id: Id }).then(
                    function (response) {
                        console.log(response);
                        $scope.Subcriptions = response.data.Data;
                    });
            }

            getCompaniesDropdown = function () {
                $scope.AjaxGet("/api/CompanyApi/GetAllCompaniesDropDown", ).then(
                    function (response) {
                        console.log(response);
                        $scope.Companies = response.data.Data;
                    });

            }
            $scope.GetCompanyData = function (Company) {
                console.log(Company.Id);
                $scope.Subcription.CompanyId = Company.Id;
            }
            $scope.AddInit = function () {
                getCompaniesDropdown();
                $scope.Subcription = {};
            }
            $scope.AddSubcription = function (sub) {
                if (sub.CompanyId == null || sub.CompanyId == 0) {
                    toaster.pop('error', "error", "Select Company!");
                }
                console.log(sub);
      
                $scope.AjaxPost("/api/CompanySubcriptionApi/AddCompanySubcription", sub).then(
                    function (response) {
                        if (response.status == 200) {
                            // alert("Lead has been Added Successfully!");
                            toaster.pop('success', "success", "Subcription Added Successfully");
                            $timeout(function () { window.location.href = '/CompanySubcription'; }, 2000);
                        } else {
                            //alert("Could Not Add new Lead");
                            toaster.pop('error', "error", "Could not add subcription!");
                        }
                    });
            }
            $scope.DeleteSubcription = function (sub) {
                console.log(sub);
                var Req = {
                    Id: Sub.Id,
                }
            }
            

        }
    ]);