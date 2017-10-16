﻿myApp.service('sharedService', ['$http', '$q', 'apiService', function ($http, $q, apiService) {

    var sharedService = {};


    //Service for get Customer Types
    var getListOfProjects = function () {
        var deferred = $q.defer();
        apiService.get("projects").then(function (response) {
            deferred.resolve(response);
        },
        function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    var getListOfTypes = function () {
        var deferred = $q.defer();
        apiService.get("Types").then(function (response) {
            deferred.resolve(response);
        },
        function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };

    var getListOfUsers = function () {
        var deferred = $q.defer();
        apiService.get("allUsers").then(function (response) {
            deferred.resolve(response);
        },
        function (err) {
            deferred.reject(err);
        });
        return deferred.promise;
    };


    sharedService.getListOfProjects = getListOfProjects;
    sharedService.getListOfTypes = getListOfTypes;
    sharedService.getListOfUsers = getListOfUsers;

    return sharedService;

}]);