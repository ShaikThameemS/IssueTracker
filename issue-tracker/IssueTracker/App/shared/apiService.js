

myApp.service('apiService', ['$http', '$q', function ($http, $q) {

    var apiService = {};
    //var apiBase = "http://192.168.1.190:9801/api/";
    //var apiBase = "http://192.168.1.244:8083/api/"
    var apiBase = "http://localhost:59595/api/"

    //===========================GET RESOURCE==============================
    var get = function (module, parameter) {
        var deferred = $q.defer(); 
        $http.get(apiBase + module, { params: parameter }, {
            headers: {
                'Content-Type': 'application/json',
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Credentials": "true",
                "Access-Control-Allow-Methods": "GET,PUT,POST,DELETE"
            }
        }).success(function (response) {
            deferred.resolve(response); 
        }).catch(function (data, status, headers, config) { // <--- catch instead error
            deferred.reject(data.statusText);
        });

       return deferred.promise;
    }; 

    //===========================CREATE RESOURCE==============================
    var create = function (module, parameter) {
        var deferred = $q.defer();
        jQuery.post(apiBase + module, parameter, {
            headers: {
                'Content-Type': 'application/json',
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Credentials": "true",
                "Access-Control-Allow-Methods": "GET,PUT,POST,DELETE"
            }
        }).success(function (response) {
            deferred.resolve(response);
        }).error(function (data, status, headers, config) { // <--- catch instead error
            deferred.reject(data.statusText);
        });
        return deferred.promise;
    };

    //===========================UPDATE RESOURCE==============================
    var update = function (module, parameter) {
        var deferred = $q.defer();
        jQuery.post(apiBase + module + '/' + parameter.Id, parameter, { headers: { 'Content-Type': 'application/json' } }).success(function (response) {
            deferred.resolve(response);
        }).error(function (data, status, headers, config) { // <--- catch instead error
            deferred.reject(data.statusText);
        });
        return deferred.promise;
    };

    //===========================DELETE RESOURCE==============================
    var delet = function (module, parameter) {        
        var deferred = $q.defer();
        jQuery.post(apiBase + module + '/' + parameter, parameter, { headers: { 'Content-Type': 'application/json' } }).success(function (response) {
            deferred.resolve(response);
        }).error(function (data, status, headers, config) { // <--- catch instead error
            deferred.reject(data.statusText);
        });
        return deferred.promise;
    };

    apiService.get = get;
    apiService.create = create;
    apiService.update = update;
    apiService.delet = delet;

    return apiService;

}]);
