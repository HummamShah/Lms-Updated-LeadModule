app.controller('UserCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",
        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to User App");
            //$scope.TryingAjaxService = function () {
            //    $scope.AjaxGet("/api/UserApi/GetListData", null).then(
            //        function (response) {
            //            console.log(response);
            //        })
            //}
            $scope.initIndex = function () {
                $scope.AjaxGet("/api/UserApi/GetListData", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Users = response.data.Data;
                    });
            }
            $scope.AddUser = function (user) {
                console.log(user);
                if (user.DepartmentId == null || user.DepartmentId == 0) {
                    //alert("Please Select Department");
                    toaster.pop('error', "error", "Please Select Department");
                    return;
                }
                if (user.FirstName == null || user.FirstName == "") {
                    //alert("Please Enter First Name");
                    toaster.pop('error', "error", "Please Enter First Name");
                    return;
                }
                if (user.Email == null || user.Email == "") {
                    //alert("Please Enter Email!");
                    toaster.pop('error', "error", "Please Enter Email!");
                    return;
                }
                if (user.Contact1 == null) {
                    //alert("Please Enter Primary Contact");
                    toaster.pop('error', "error", "Please Enter Primary Contact!");
                    return;
                }
                if (user.Password == null || user.Password == "") {
                    //alert("Enter Password");
                    toaster.pop('error', "error", "Please Enter Password!");
                    return;
                }
                if (user.Password != user.ConfirmPassword) {
                    //alert("Password and Confirm Password shoul match");
                    toaster.pop('error', "error", "Password and Confirm Password should match!");
                    return;
                }
                if (user.HasSupervisor == true) {
                    if (user.SupervisorId == null) {
                        toaster.pop('error', "error", "Please Select Supervisor");
                        return;
                    }
                }
                //Loader need to make it generic so we could use this in a function
                $scope.IsServiceRunning = true;
                //var promise = $http.post("/api/UserApi/RegisterUser", user, {headers: { 'Accept': 'application/json' } });
                //promise.then(
                //    function (response) {
                //        $scope.IsServiceRunning = false;
                //        console.log(response);
                //        if (response.status == 200) {
                //            //alert("User has been Added Successfully!");
                //            toaster.pop('success', "success", "Password and Confirm Password shoul match!");
                //            $timeout(function () { window.location.href = '/User'; }, 2000);
                //        } else {
                //            toaster.pop('error', "error", "Could Not Add User!");
                //        }

                //    });
                $scope.AjaxPost("/api/UserApi/RegisterUser", user).then(
                    function (response) {
                        console.log(response);
                
                        if (response.status == 200) {
                            if (response.data.Success) {
                                //alert("User has been Added Successfully!");
                                toaster.pop('success', "success", "User Has Been Added!");
                                $timeout(function () { window.location.href = '/User'; }, 2000);
                            } else {
                                angular.forEach(response.data.ValidationErrors, function (error, key) {
                                    toaster.pop('error', "error", error);
                                });
                                
                            }
                           
                        } else {
                            toaster.pop('error', "error", "Could Not Add User!");
                        }
                    });

            }
            $scope.GetUsersByDepartmentId = function (DepartmentId) {
                if (DepartmentId != null) {
                    var data = {
                        Id: DepartmentId
                    }
                    $scope.AjaxGet("/api/UserApi/GetUsersByDepartment", data).then(
                        function (response) {
                            console.log(response);
                            $scope.Supervisors = response.data.Data;
                        });
                }
            }
            $scope.CheckForDepartment = function (Id) {
                if (Id == null) {
                    //alert("Please Select Department First");
                    toaster.pop('error', "error", "Please Select Department First");
                    $scope.User.HasSupervisor = false;
                }
            }
            $scope.AddAdmin = function (user) {
                console.log(user);
                //var promise = $http.post("/api/UserApi/RegisterAdmin", user, { headers: { 'Accept': 'application/json' } });
                //promise.then(
                //    function (response) {
                //        console.log(response);
                //        if (response.status == 200) {
                //            alert("Admin has been added Successfully!");
                //            $timeout(function () { window.location.href = '/User'; }, 2000);
                //        } else {
                //            alert("Could Not Add new admin");
                //        }

                //    });

                $scope.AjaxPost("/api/UserApi/RegisterAdmin", user).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "Admin Has Been Added Successfully");
                            $timeout(function () { window.location.href = '/User'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could Not Add Admin!");
                        }
                    });

            }
            $scope.EditUser = function (User) {
                console.log(User);
                $scope.AjaxPost("/api/UserApi/EditUser", User).then(
                    function (response) {
                        if (response.status == 200) {
                            toaster.pop('success', "success", "User Has Been Updated Successfully");
                            $timeout(function () { window.location.href = '/User'; }, 2000);
                        } else {
                            toaster.pop('error', "error", "Could Not Update User!");
                        }
                    });
            }
            $scope.AddAdminInit = function () {
                $scope.Admin = {};
            }
            function GetDepartments() {
                $scope.AjaxGet("/api/DepartmentApi/GetDepartmentsDropdown", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Departments = response.data.Data;
                    });
            }
            $scope.EditInit = function () {
                $scope.User = {};
                GetDepartments();
                var Id = $scope.GetUrlParameter("Id");
                $scope.AjaxGet("/api/UserApi/GetUser", { Id:Id}).then(
                    function (response) {
                        console.log(response);
                        $scope.User = response.data;
                        $scope.GetUsersByDepartmentId($scope.User.DepartmentId);
                    });
            }
            $scope.AddInit = function () {
                $scope.User = {};
                $scope.AjaxGet("/api/DepartmentApi/GetDepartmentsDropdown", null).then(
                    function (response) {
                        console.log(response);
                        $scope.Departments = response.data.Data;
                    });
            }
        }
    ]);