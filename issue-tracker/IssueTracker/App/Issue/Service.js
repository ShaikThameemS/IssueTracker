issue.service('issueService', ['$http', '$q', 'apiService', function ($http, $q, apiService) {

    var issueService = {};


    //Service for get Customer Types
    var getListOfIssues = function () {
        var deferred = $q.defer();
        apiService.get("allissue").then(function (response) {
            deferred.resolve(response);
        },
        function (err) {
            deferred.reject(err);
        });
        return deferred.promise;

    };


    
    issueService.getListOfIssues = getListOfIssues;

    return issueService;

}]);