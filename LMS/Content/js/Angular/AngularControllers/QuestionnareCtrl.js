app.controller('QuestionnareCtrl',
    [
        "$scope",
        "$rootScope",
        "$timeout",
        "$q",
        "$window",
        "$http",
        "toaster",
        function ($scope, $rootScope, $timeout, $q, $window, $http, toaster) {
            console.log("Connected to QuestionnareCtrl App");
            $scope.AddInit = function () {
                $scope.Questionnares = [];
                var Type =   $scope.GetUrlParameter("Type");
                var LeadId = $scope.GetUrlParameter("LeadId");//Fw,Email,Wsp
                $scope.IsViewOnly = $scope.GetUrlParameter("IsViewOnly");
                console.log($scope.IsViewOnly);
                $scope.AjaxGet("/api/LeadApi/GetQuestionnareData", { LeadId: LeadId}).then(
                    function (response) {
                        console.log(response);
                        $scope.Questionnares = response.data.Data;
                        $scope.IsQuestionnareFilled = response.data.IsQuestionnareFilled;
                        if ($scope.IsQuestionnareFilled == false) {
                            if (Type == 2) {//Firewall // fow now this hould work but later need to get these from another table
                                $scope.Questionnares = [{
                                    LeadId: LeadId,
                                    Requirements: "Are you currently using any firewall? (if yes kindly give details below)",
                                    HeadOffice: "",
                                    BranchOffice: "",
                                    Details: "",
                                    Type: 2
                                },
                                {
                                    LeadId: LeadId,
                                    Requirements: "Do you have any branch offices? If so then do they also need a firewall or site-to-site tunnel to the main office?",
                                    HeadOffice: "",
                                    BranchOffice: "",
                                    Details: "",
                                    Type: 2
                                },
                                {
                                    LeadId: LeadId,
                                    Requirements: "How many concurrent/Active  Users?",
                                    HeadOffice: "",
                                    BranchOffice: "",
                                    Details: "",
                                    Type: 2
                                },
                                {
                                    LeadId: LeadId,
                                    Requirements: "Do you have Microsoft Active Directory? If yes then do you want your users to be authenticated via the AD?",
                                    HeadOffice: "",
                                    BranchOffice: "",
                                    Details: "",
                                    Type: 2
                                },
                                {
                                    LeadId: LeadId,
                                    Requirements: "Any other Authentication Server? (eg RADIUS)",
                                    HeadOffice: "",
                                    BranchOffice: "",
                                    Details: "",
                                    Type: 2
                                },
                                {
                                    LeadId: LeadId,
                                    Requirements: "Single/ Multiple subnets (local) (if yes kindly give details)",
                                    HeadOffice: "",
                                    BranchOffice: "",
                                    Details: "",
                                    Type: 2
                                },
                                {
                                    LeadId: LeadId,
                                    Requirements: "How many ISP are you currently using?",
                                    HeadOffice: "",
                                    BranchOffice: "",
                                    Details: "",
                                    Type: 2
                                },
                                {
                                    LeadId: LeadId,
                                    Requirements: "If you are using multiple ISPs do you require WAN Link Balancing or Failover for ISP?",
                                    HeadOffice: "",
                                    BranchOffice: "",
                                    Details: "",
                                    Type: 2
                                },
                                {
                                    LeadId: LeadId,
                                    Requirements: "Is User Traffic Shapping required? (if yes kindly give details)",
                                    HeadOffice: "",
                                    BranchOffice: "",
                                    Details: "",
                                    Type: 2
                                },
                                {
                                    LeadId: LeadId,
                                    Requirements: "Do you need network protect i.e IPS?",
                                    HeadOffice: "",
                                    BranchOffice: "",
                                    Details: "",
                                    Type: 2
                                },
                                {
                                    LeadId: LeadId,
                                    Requirements: "Do you have Remote VPN users? (if yes kindly give details)",
                                    HeadOffice: "",
                                    BranchOffice: "",
                                    Details: "",
                                    Type: 2
                                },
                                {
                                    LeadId: LeadId,
                                    Requirements: "How many Interfaces (ports) do you require?",
                                    HeadOffice: "",
                                    BranchOffice: "",
                                    Details: "",
                                    Type: 2
                                },
                                {
                                    LeadId: LeadId,
                                    Requirements: "Web Filtering required, i.e blocking of URLs? (if yes kindly give details)",
                                    HeadOffice: "",
                                    BranchOffice: "",
                                    Details: "",
                                    Type: 2
                                },
                                {
                                    LeadId: LeadId,
                                    Requirements: "Application Filtering required? (if yes kindly give details)",
                                    HeadOffice: "",
                                    BranchOffice: "",
                                    Details: "",
                                    Type: 2
                                },
                                {
                                    LeadId: LeadId,
                                    Requirements: "Do you need sandbox solution to scan for attachments and internet downloads?",
                                    HeadOffice: "",
                                    BranchOffice: "",
                                    Details: "",
                                    Type: 2
                                },
                                {
                                    LeadId: LeadId,
                                    Requirements: "High Availability? If yes then do you require active/passive or active/active?",
                                    HeadOffice: "",
                                    BranchOffice: "",
                                    Details: "",
                                    Type: 2
                                }]
                            }
                            if (Type == 1) { //email
                                $scope.Questionnares = [{
                                    LeadId: LeadId,
                                    Requirements: "Do you have any Existing Mail server on-premise?",
                                    HeadOffice: "",
                                    BranchOffice: "",
                                    Details: "",
                                    Type: 1
                                },
                                {
                                    LeadId: LeadId,
                                    Requirements: "Is Email protection required? (applicable if you have on-premise mail servers)",
                                    HeadOffice: "",
                                    BranchOffice: "",
                                    Details: "",
                                    Type: 1
                                },
                                {
                                    LeadId: LeadId,
                                    Requirements: "How many domains do you have?",
                                    HeadOffice: "",
                                    BranchOffice: "",
                                    Details: "",
                                    Type: 1
                                },
                                {
                                    LeadId: LeadId,
                                    Requirements: "What is the maximum number of emails passing  through at peak time?",
                                    HeadOffice: "",
                                    BranchOffice: "",
                                    Details: "",
                                    Type: 1
                                }]
                            }
                            if (Type == 0) {//endpoint
                                $scope.Questionnares = [{
                                    LeadId: LeadId,
                                    Requirements: "Number of published web applications? (if yes kindly give details)",
                                    HeadOffice: "",
                                    BranchOffice: "",
                                    Details: "",
                                    Type: 0
                                },
                                {
                                    LeadId: LeadId,
                                    Requirements: "What are maximum request at peak time for your web servers (if applicable)",
                                    HeadOffice: "",
                                    BranchOffice: "",
                                    Details: "",
                                    Type: 0
                                },
                                {
                                    LeadId: LeadId,
                                    Requirements: "How many concurrent users access you web application? (if applicable)",
                                    HeadOffice: "",
                                    BranchOffice: "",
                                    Details: "",
                                    Type: 0
                                },
                                {
                                    LeadId: LeadId,
                                    Requirements: "Do you require web server protection?",
                                    HeadOffice: "",
                                    BranchOffice: "",
                                    Details: "",
                                    Type: 0
                                }]
                            }

                        }
                    });
              
             
            }
            $scope.AddQuestionnare = function (Questionnare) {
                console.log(Questionnare); 
                var data = {
                    Questionnares: Questionnare
                }
                $scope.AjaxPost("/api/LeadApi/SaveQuestionnareData", data).then(
                    function (response) {
                        if (response.status == 200) {
                            // alert("Lead has been Added Successfully!");
                            toaster.pop('success', "success", "Questionnare Been Updated Successfully");
                            $timeout(function () { window.location.href = '/Lead'; }, 2000);
                        } else {
                            //alert("Could Not Add new Lead");
                            toaster.pop('error', "error", "Could not add Questionnare!");
                        }
                    });
            }
        }
    ]);